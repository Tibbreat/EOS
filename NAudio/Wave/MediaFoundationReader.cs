// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.MediaFoundationReader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using NAudio.MediaFoundation;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  public class MediaFoundationReader : WaveStream
  {
    private WaveFormat waveFormat;
    private readonly long length;
    private readonly MediaFoundationReader.MediaFoundationReaderSettings settings;
    private readonly string file;
    private IMFSourceReader pReader;
    private long position;
    private byte[] decoderOutputBuffer;
    private int decoderOutputOffset;
    private int decoderOutputCount;
    private long repositionTo = -1;

    public MediaFoundationReader(string file)
      : this(file, new MediaFoundationReader.MediaFoundationReaderSettings())
    {
    }

    public MediaFoundationReader(
      string file,
      MediaFoundationReader.MediaFoundationReaderSettings settings)
    {
      MediaFoundationApi.Startup();
      this.settings = settings;
      this.file = file;
      IMFSourceReader reader = this.CreateReader(settings);
      this.waveFormat = this.GetCurrentWaveFormat(reader);
      reader.SetStreamSelection(-3, true);
      this.length = this.GetLength(reader);
      if (!settings.SingleReaderObject)
        return;
      this.pReader = reader;
    }

    private WaveFormat GetCurrentWaveFormat(IMFSourceReader reader)
    {
      IMFMediaType ppMediaType;
      reader.GetCurrentMediaType(-3, out ppMediaType);
      MediaType mediaType = new MediaType(ppMediaType);
      Guid majorType = mediaType.MajorType;
      Guid subType = mediaType.SubType;
      int channelCount = mediaType.ChannelCount;
      int bitsPerSample = mediaType.BitsPerSample;
      int sampleRate = mediaType.SampleRate;
      return !(subType == AudioSubtypes.MFAudioFormat_PCM) ? WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount) : new WaveFormat(sampleRate, bitsPerSample, channelCount);
    }

    protected virtual IMFSourceReader CreateReader(
      MediaFoundationReader.MediaFoundationReaderSettings settings)
    {
      IMFSourceReader ppSourceReader;
      MediaFoundationInterop.MFCreateSourceReaderFromURL(this.file, (IMFAttributes) null, out ppSourceReader);
      ppSourceReader.SetStreamSelection(-2, false);
      ppSourceReader.SetStreamSelection(-3, true);
      ppSourceReader.SetCurrentMediaType(-3, IntPtr.Zero, new MediaType()
      {
        MajorType = MediaTypes.MFMediaType_Audio,
        SubType = (settings.RequestFloatOutput ? AudioSubtypes.MFAudioFormat_Float : AudioSubtypes.MFAudioFormat_PCM)
      }.MediaFoundationObject);
      return ppSourceReader;
    }

    private long GetLength(IMFSourceReader reader)
    {
      PropVariant pvarAttribute;
      int presentationAttribute = reader.GetPresentationAttribute(-1, MediaFoundationAttributes.MF_PD_DURATION, out pvarAttribute);
      switch (presentationAttribute)
      {
        case -1072875802:
          return 0;
        case 0:
          long length = (long) pvarAttribute.Value * (long) this.waveFormat.AverageBytesPerSecond / 10000000L;
          pvarAttribute.Clear();
          return length;
        default:
          Marshal.ThrowExceptionForHR(presentationAttribute);
          goto case 0;
      }
    }

    private void EnsureBuffer(int bytesRequired)
    {
      if (this.decoderOutputBuffer != null && this.decoderOutputBuffer.Length >= bytesRequired)
        return;
      this.decoderOutputBuffer = new byte[bytesRequired];
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this.pReader == null)
        this.pReader = this.CreateReader(this.settings);
      if (this.repositionTo != -1L)
        this.Reposition(this.repositionTo);
      int num = 0;
      if (this.decoderOutputCount > 0)
        num += this.ReadFromDecoderBuffer(buffer, offset, count - num);
      while (num < count)
      {
        MF_SOURCE_READER_FLAG pdwStreamFlags;
        IMFSample ppSample;
        this.pReader.ReadSample(-3, 0, out int _, out pdwStreamFlags, out ulong _, out ppSample);
        if ((pdwStreamFlags & MF_SOURCE_READER_FLAG.MF_SOURCE_READERF_ENDOFSTREAM) == MF_SOURCE_READER_FLAG.None)
        {
          if ((pdwStreamFlags & MF_SOURCE_READER_FLAG.MF_SOURCE_READERF_CURRENTMEDIATYPECHANGED) != MF_SOURCE_READER_FLAG.None)
          {
            this.waveFormat = this.GetCurrentWaveFormat(this.pReader);
            this.OnWaveFormatChanged();
          }
          else if (pdwStreamFlags != MF_SOURCE_READER_FLAG.None)
            throw new InvalidOperationException(string.Format("MediaFoundationReadError {0}", (object) pdwStreamFlags));
          IMFMediaBuffer ppBuffer;
          ppSample.ConvertToContiguousBuffer(out ppBuffer);
          IntPtr ppbBuffer;
          int pcbCurrentLength;
          ppBuffer.Lock(out ppbBuffer, out int _, out pcbCurrentLength);
          this.EnsureBuffer(pcbCurrentLength);
          Marshal.Copy(ppbBuffer, this.decoderOutputBuffer, 0, pcbCurrentLength);
          this.decoderOutputOffset = 0;
          this.decoderOutputCount = pcbCurrentLength;
          num += this.ReadFromDecoderBuffer(buffer, offset + num, count - num);
          ppBuffer.Unlock();
          Marshal.ReleaseComObject((object) ppBuffer);
          Marshal.ReleaseComObject((object) ppSample);
        }
        else
          break;
      }
      this.position += (long) num;
      return num;
    }

    private int ReadFromDecoderBuffer(byte[] buffer, int offset, int needed)
    {
      int length = Math.Min(needed, this.decoderOutputCount);
      Array.Copy((Array) this.decoderOutputBuffer, this.decoderOutputOffset, (Array) buffer, offset, length);
      this.decoderOutputOffset += length;
      this.decoderOutputCount -= length;
      if (this.decoderOutputCount == 0)
        this.decoderOutputOffset = 0;
      return length;
    }

    public override WaveFormat WaveFormat => this.waveFormat;

    public override long Length => this.length;

    public override long Position
    {
      get => this.position;
      set
      {
        if (value < 0L)
          throw new ArgumentOutOfRangeException(nameof (value), "Position cannot be less than 0");
        if (this.settings.RepositionInRead)
        {
          this.repositionTo = value;
          this.position = value;
        }
        else
          this.Reposition(value);
      }
    }

    private void Reposition(long desiredPosition)
    {
      PropVariant varPosition = PropVariant.FromLong(10000000L * this.repositionTo / (long) this.waveFormat.AverageBytesPerSecond);
      this.pReader.SetCurrentPosition(Guid.Empty, ref varPosition);
      this.decoderOutputCount = 0;
      this.decoderOutputOffset = 0;
      this.position = desiredPosition;
      this.repositionTo = -1L;
    }

    protected override void Dispose(bool disposing)
    {
      if (this.pReader != null)
      {
        Marshal.ReleaseComObject((object) this.pReader);
        this.pReader = (IMFSourceReader) null;
      }
      base.Dispose(disposing);
    }

    public event EventHandler WaveFormatChanged;

    private void OnWaveFormatChanged()
    {
      EventHandler waveFormatChanged = this.WaveFormatChanged;
      if (waveFormatChanged == null)
        return;
      waveFormatChanged((object) this, EventArgs.Empty);
    }

    public class MediaFoundationReaderSettings
    {
      public MediaFoundationReaderSettings() => this.RepositionInRead = true;

      public bool RequestFloatOutput { get; set; }

      public bool SingleReaderObject { get; set; }

      public bool RepositionInRead { get; set; }
    }
  }
}
