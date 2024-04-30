// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.AudioFileReader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave.SampleProviders;
using System;

#nullable disable
namespace NAudio.Wave
{
  public class AudioFileReader : WaveStream, ISampleProvider
  {
    private string fileName;
    private WaveStream readerStream;
    private readonly SampleChannel sampleChannel;
    private readonly int destBytesPerSample;
    private readonly int sourceBytesPerSample;
    private readonly long length;
    private readonly object lockObject;

    public AudioFileReader(string fileName)
    {
      this.lockObject = new object();
      this.fileName = fileName;
      this.CreateReaderStream(fileName);
      this.sourceBytesPerSample = this.readerStream.WaveFormat.BitsPerSample / 8 * this.readerStream.WaveFormat.Channels;
      this.sampleChannel = new SampleChannel((IWaveProvider) this.readerStream, false);
      this.destBytesPerSample = 4 * this.sampleChannel.WaveFormat.Channels;
      this.length = this.SourceToDest(this.readerStream.Length);
    }

    private void CreateReaderStream(string fileName)
    {
      if (fileName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
      {
        this.readerStream = (WaveStream) new WaveFileReader(fileName);
        if (this.readerStream.WaveFormat.Encoding == WaveFormatEncoding.Pcm || this.readerStream.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
          return;
        this.readerStream = WaveFormatConversionStream.CreatePcmStream(this.readerStream);
        this.readerStream = (WaveStream) new BlockAlignReductionStream(this.readerStream);
      }
      else if (fileName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
        this.readerStream = (WaveStream) new Mp3FileReader(fileName);
      else if (fileName.EndsWith(".aiff"))
        this.readerStream = (WaveStream) new AiffFileReader(fileName);
      else
        this.readerStream = (WaveStream) new MediaFoundationReader(fileName);
    }

    public override WaveFormat WaveFormat => this.sampleChannel.WaveFormat;

    public override long Length => this.length;

    public override long Position
    {
      get => this.SourceToDest(this.readerStream.Position);
      set
      {
        lock (this.lockObject)
          this.readerStream.Position = this.DestToSource(value);
      }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      WaveBuffer waveBuffer = new WaveBuffer(buffer);
      int count1 = count / 4;
      return this.Read(waveBuffer.FloatBuffer, offset / 4, count1) * 4;
    }

    public int Read(float[] buffer, int offset, int count)
    {
      lock (this.lockObject)
        return this.sampleChannel.Read(buffer, offset, count);
    }

    public float Volume
    {
      get => this.sampleChannel.Volume;
      set => this.sampleChannel.Volume = value;
    }

    private long SourceToDest(long sourceBytes)
    {
      return (long) this.destBytesPerSample * (sourceBytes / (long) this.sourceBytesPerSample);
    }

    private long DestToSource(long destBytes)
    {
      return (long) this.sourceBytesPerSample * (destBytes / (long) this.destBytesPerSample);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.readerStream.Dispose();
        this.readerStream = (WaveStream) null;
      }
      base.Dispose(disposing);
    }
  }
}
