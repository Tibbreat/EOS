// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Mp3FileReader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace NAudio.Wave
{
  public class Mp3FileReader : WaveStream
  {
    private readonly WaveFormat waveFormat;
    private Stream mp3Stream;
    private readonly long mp3DataLength;
    private readonly long dataStartPosition;
    private readonly Id3v2Tag id3v2Tag;
    private readonly XingHeader xingHeader;
    private readonly byte[] id3v1Tag;
    private readonly bool ownInputStream;
    private List<Mp3Index> tableOfContents;
    private int tocIndex;
    private long totalSamples;
    private readonly int bytesPerSample;
    private IMp3FrameDecompressor decompressor;
    private readonly byte[] decompressBuffer;
    private int decompressBufferOffset;
    private int decompressLeftovers;
    private bool repositionedFlag;
    private readonly object repositionLock = new object();

    public Mp3WaveFormat Mp3WaveFormat { get; private set; }

    public Mp3FileReader(string mp3FileName)
      : this((Stream) File.OpenRead(mp3FileName))
    {
      this.ownInputStream = true;
    }

    public Mp3FileReader(
      string mp3FileName,
      Mp3FileReader.FrameDecompressorBuilder frameDecompressorBuilder)
      : this((Stream) File.OpenRead(mp3FileName), frameDecompressorBuilder)
    {
      this.ownInputStream = true;
    }

    public Mp3FileReader(Stream inputStream)
      : this(inputStream, new Mp3FileReader.FrameDecompressorBuilder(Mp3FileReader.CreateAcmFrameDecompressor))
    {
    }

    public Mp3FileReader(
      Stream inputStream,
      Mp3FileReader.FrameDecompressorBuilder frameDecompressorBuilder)
    {
      if (inputStream == null)
        throw new ArgumentNullException(nameof (inputStream));
      try
      {
        this.mp3Stream = inputStream;
        this.id3v2Tag = Id3v2Tag.ReadTag(this.mp3Stream);
        this.dataStartPosition = this.mp3Stream.Position;
        Mp3Frame frame = Mp3Frame.LoadFromStream(this.mp3Stream);
        double bitRate1 = frame != null ? (double) frame.BitRate : throw new InvalidDataException("Invalid MP3 file - no MP3 Frames Detected");
        this.xingHeader = XingHeader.LoadXingHeader(frame);
        if (this.xingHeader != null)
          this.dataStartPosition = this.mp3Stream.Position;
        Mp3Frame mp3Frame = Mp3Frame.LoadFromStream(this.mp3Stream);
        if (mp3Frame != null && (mp3Frame.SampleRate != frame.SampleRate || mp3Frame.ChannelMode != frame.ChannelMode))
        {
          this.dataStartPosition = mp3Frame.FileOffset;
          frame = mp3Frame;
        }
        this.mp3DataLength = this.mp3Stream.Length - this.dataStartPosition;
        this.mp3Stream.Position = this.mp3Stream.Length - 128L;
        byte[] buffer = new byte[128];
        this.mp3Stream.Read(buffer, 0, 128);
        if (buffer[0] == (byte) 84 && buffer[1] == (byte) 65 && buffer[2] == (byte) 71)
        {
          this.id3v1Tag = buffer;
          this.mp3DataLength -= 128L;
        }
        this.mp3Stream.Position = this.dataStartPosition;
        this.Mp3WaveFormat = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2, frame.FrameLength, (int) bitRate1);
        this.CreateTableOfContents();
        this.tocIndex = 0;
        double bitRate2 = (double) this.mp3DataLength * 8.0 / this.TotalSeconds();
        this.mp3Stream.Position = this.dataStartPosition;
        this.Mp3WaveFormat = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2, frame.FrameLength, (int) bitRate2);
        this.decompressor = frameDecompressorBuilder((WaveFormat) this.Mp3WaveFormat);
        this.waveFormat = this.decompressor.OutputFormat;
        this.bytesPerSample = this.decompressor.OutputFormat.BitsPerSample / 8 * this.decompressor.OutputFormat.Channels;
        this.decompressBuffer = new byte[1152 * this.bytesPerSample * 2];
      }
      catch (Exception ex)
      {
        if (this.ownInputStream)
          inputStream.Dispose();
        throw;
      }
    }

    public static IMp3FrameDecompressor CreateAcmFrameDecompressor(WaveFormat mp3Format)
    {
      return (IMp3FrameDecompressor) new AcmMp3FrameDecompressor(mp3Format);
    }

    private void CreateTableOfContents()
    {
      try
      {
        this.tableOfContents = new List<Mp3Index>((int) (this.mp3DataLength / 400L));
        Mp3Frame frame;
        do
        {
          Mp3Index mp3Index = new Mp3Index();
          mp3Index.FilePosition = this.mp3Stream.Position;
          mp3Index.SamplePosition = this.totalSamples;
          frame = this.ReadNextFrame(false);
          if (frame != null)
          {
            this.ValidateFrameFormat(frame);
            this.totalSamples += (long) frame.SampleCount;
            mp3Index.SampleCount = frame.SampleCount;
            mp3Index.ByteCount = (int) (this.mp3Stream.Position - mp3Index.FilePosition);
            this.tableOfContents.Add(mp3Index);
          }
        }
        while (frame != null);
      }
      catch (EndOfStreamException ex)
      {
      }
    }

    private void ValidateFrameFormat(Mp3Frame frame)
    {
      if (frame.SampleRate != this.Mp3WaveFormat.SampleRate)
        throw new InvalidOperationException(string.Format("Got a frame at sample rate {0}, in an MP3 with sample rate {1}. Mp3FileReader does not support sample rate changes.", (object) frame.SampleRate, (object) this.Mp3WaveFormat.SampleRate));
      if ((frame.ChannelMode == ChannelMode.Mono ? 1 : 2) != this.Mp3WaveFormat.Channels)
        throw new InvalidOperationException(string.Format("Got a frame with channel mode {0}, in an MP3 with {1} channels. Mp3FileReader does not support changes to channel count.", (object) frame.ChannelMode, (object) this.Mp3WaveFormat.Channels));
    }

    private double TotalSeconds()
    {
      return (double) this.totalSamples / (double) this.Mp3WaveFormat.SampleRate;
    }

    public Id3v2Tag Id3v2Tag => this.id3v2Tag;

    public byte[] Id3v1Tag => this.id3v1Tag;

    public Mp3Frame ReadNextFrame() => this.ReadNextFrame(true);

    private Mp3Frame ReadNextFrame(bool readData)
    {
      Mp3Frame mp3Frame = (Mp3Frame) null;
      try
      {
        mp3Frame = Mp3Frame.LoadFromStream(this.mp3Stream, readData);
        if (mp3Frame != null)
          ++this.tocIndex;
      }
      catch (EndOfStreamException ex)
      {
      }
      return mp3Frame;
    }

    public override long Length => this.totalSamples * (long) this.bytesPerSample;

    public override WaveFormat WaveFormat => this.waveFormat;

    public override long Position
    {
      get
      {
        return this.tocIndex >= this.tableOfContents.Count ? this.Length : this.tableOfContents[this.tocIndex].SamplePosition * (long) this.bytesPerSample + (long) this.decompressBufferOffset;
      }
      set
      {
        lock (this.repositionLock)
        {
          value = Math.Max(Math.Min(value, this.Length), 0L);
          long num = value / (long) this.bytesPerSample;
          Mp3Index mp3Index = (Mp3Index) null;
          for (int index = 0; index < this.tableOfContents.Count; ++index)
          {
            if (this.tableOfContents[index].SamplePosition >= num)
            {
              mp3Index = this.tableOfContents[index];
              this.tocIndex = index;
              break;
            }
          }
          this.mp3Stream.Position = mp3Index == null ? this.mp3DataLength + this.dataStartPosition : mp3Index.FilePosition;
          this.decompressBufferOffset = 0;
          this.decompressLeftovers = 0;
          this.repositionedFlag = true;
        }
      }
    }

    public override int Read(byte[] sampleBuffer, int offset, int numBytes)
    {
      int num = 0;
      lock (this.repositionLock)
      {
        if (this.decompressLeftovers != 0)
        {
          int length = Math.Min(this.decompressLeftovers, numBytes);
          Array.Copy((Array) this.decompressBuffer, this.decompressBufferOffset, (Array) sampleBuffer, offset, length);
          this.decompressLeftovers -= length;
          if (this.decompressLeftovers == 0)
            this.decompressBufferOffset = 0;
          else
            this.decompressBufferOffset += length;
          num += length;
          offset += length;
        }
        int length1;
        for (; num < numBytes; num += length1)
        {
          Mp3Frame frame = this.ReadNextFrame();
          if (frame != null)
          {
            if (this.repositionedFlag)
            {
              this.decompressor.Reset();
              this.repositionedFlag = false;
            }
            int val1 = this.decompressor.DecompressFrame(frame, this.decompressBuffer, 0);
            length1 = Math.Min(val1, numBytes - num);
            Array.Copy((Array) this.decompressBuffer, 0, (Array) sampleBuffer, offset, length1);
            if (length1 < val1)
            {
              this.decompressBufferOffset = length1;
              this.decompressLeftovers = val1 - length1;
            }
            else
            {
              this.decompressBufferOffset = 0;
              this.decompressLeftovers = 0;
            }
            offset += length1;
          }
          else
            break;
        }
      }
      return num;
    }

    public XingHeader XingHeader => this.xingHeader;

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.mp3Stream != null)
        {
          if (this.ownInputStream)
            this.mp3Stream.Dispose();
          this.mp3Stream = (Stream) null;
        }
        if (this.decompressor != null)
        {
          this.decompressor.Dispose();
          this.decompressor = (IMp3FrameDecompressor) null;
        }
      }
      base.Dispose(disposing);
    }

    public delegate IMp3FrameDecompressor FrameDecompressorBuilder(WaveFormat mp3Format);
  }
}
