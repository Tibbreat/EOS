// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SimpleCompressorStream
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Dsp;
using System;

#nullable disable
namespace NAudio.Wave
{
  public class SimpleCompressorStream : WaveStream
  {
    private WaveStream sourceStream;
    private readonly SimpleCompressor simpleCompressor;
    private byte[] sourceBuffer;
    private bool enabled;
    private readonly int channels;
    private readonly int bytesPerSample;
    private readonly object lockObject = new object();

    public SimpleCompressorStream(WaveStream sourceStream)
    {
      this.sourceStream = sourceStream;
      this.channels = sourceStream.WaveFormat.Channels;
      this.bytesPerSample = sourceStream.WaveFormat.BitsPerSample / 8;
      this.simpleCompressor = new SimpleCompressor(5.0, 10.0, (double) sourceStream.WaveFormat.SampleRate);
      this.simpleCompressor.Threshold = 16.0;
      this.simpleCompressor.Ratio = 6.0;
      this.simpleCompressor.MakeUpGain = 16.0;
    }

    public double MakeUpGain
    {
      get => this.simpleCompressor.MakeUpGain;
      set
      {
        lock (this.lockObject)
          this.simpleCompressor.MakeUpGain = value;
      }
    }

    public double Threshold
    {
      get => this.simpleCompressor.Threshold;
      set
      {
        lock (this.lockObject)
          this.simpleCompressor.Threshold = value;
      }
    }

    public double Ratio
    {
      get => this.simpleCompressor.Ratio;
      set
      {
        lock (this.lockObject)
          this.simpleCompressor.Ratio = value;
      }
    }

    public double Attack
    {
      get => this.simpleCompressor.Attack;
      set
      {
        lock (this.lockObject)
          this.simpleCompressor.Attack = value;
      }
    }

    public double Release
    {
      get => this.simpleCompressor.Release;
      set
      {
        lock (this.lockObject)
          this.simpleCompressor.Release = value;
      }
    }

    public override bool HasData(int count) => this.sourceStream.HasData(count);

    public bool Enabled
    {
      get => this.enabled;
      set => this.enabled = value;
    }

    public override long Length => this.sourceStream.Length;

    public override long Position
    {
      get => this.sourceStream.Position;
      set
      {
        lock (this.lockObject)
          this.sourceStream.Position = value;
      }
    }

    public override WaveFormat WaveFormat => this.sourceStream.WaveFormat;

    private void ReadSamples(byte[] buffer, int start, out double left, out double right)
    {
      if (this.bytesPerSample == 4)
      {
        left = (double) BitConverter.ToSingle(buffer, start);
        if (this.channels > 1)
          right = (double) BitConverter.ToSingle(buffer, start + this.bytesPerSample);
        else
          right = left;
      }
      else
      {
        if (this.bytesPerSample != 2)
          throw new InvalidOperationException(string.Format("Unsupported bytes per sample: {0}", (object) this.bytesPerSample));
        left = (double) BitConverter.ToInt16(buffer, start) / 32768.0;
        if (this.channels > 1)
          right = (double) BitConverter.ToInt16(buffer, start + this.bytesPerSample) / 32768.0;
        else
          right = left;
      }
    }

    private void WriteSamples(byte[] buffer, int start, double left, double right)
    {
      if (this.bytesPerSample == 4)
      {
        Array.Copy((Array) BitConverter.GetBytes((float) left), 0, (Array) buffer, start, this.bytesPerSample);
        if (this.channels <= 1)
          return;
        Array.Copy((Array) BitConverter.GetBytes((float) right), 0, (Array) buffer, start + this.bytesPerSample, this.bytesPerSample);
      }
      else
      {
        if (this.bytesPerSample != 2)
          return;
        Array.Copy((Array) BitConverter.GetBytes((short) (left * 32768.0)), 0, (Array) buffer, start, this.bytesPerSample);
        if (this.channels <= 1)
          return;
        Array.Copy((Array) BitConverter.GetBytes((short) (right * 32768.0)), 0, (Array) buffer, start + this.bytesPerSample, this.bytesPerSample);
      }
    }

    public override int Read(byte[] array, int offset, int count)
    {
      lock (this.lockObject)
      {
        if (!this.Enabled)
          return this.sourceStream.Read(array, offset, count);
        if (this.sourceBuffer == null || this.sourceBuffer.Length < count)
          this.sourceBuffer = new byte[count];
        int num = this.sourceStream.Read(this.sourceBuffer, 0, count) / (this.bytesPerSample * this.channels);
        for (int index = 0; index < num; ++index)
        {
          int start = index * this.bytesPerSample * this.channels;
          double left;
          double right;
          this.ReadSamples(this.sourceBuffer, start, out left, out right);
          this.simpleCompressor.Process(ref left, ref right);
          this.WriteSamples(array, offset + start, left, right);
        }
        return count;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.sourceStream != null)
      {
        this.sourceStream.Dispose();
        this.sourceStream = (WaveStream) null;
      }
      base.Dispose(disposing);
    }

    public override int BlockAlign => this.sourceStream.BlockAlign;
  }
}
