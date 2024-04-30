// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.BufferedWaveProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System;

#nullable disable
namespace NAudio.Wave
{
  public class BufferedWaveProvider : IWaveProvider
  {
    private CircularBuffer circularBuffer;
    private readonly WaveFormat waveFormat;

    public BufferedWaveProvider(WaveFormat waveFormat)
    {
      this.waveFormat = waveFormat;
      this.BufferLength = waveFormat.AverageBytesPerSecond * 5;
    }

    public int BufferLength { get; set; }

    public TimeSpan BufferDuration
    {
      get
      {
        return TimeSpan.FromSeconds((double) this.BufferLength / (double) this.WaveFormat.AverageBytesPerSecond);
      }
      set
      {
        this.BufferLength = (int) (value.TotalSeconds * (double) this.WaveFormat.AverageBytesPerSecond);
      }
    }

    public bool DiscardOnBufferOverflow { get; set; }

    public int BufferedBytes => this.circularBuffer != null ? this.circularBuffer.Count : 0;

    public TimeSpan BufferedDuration
    {
      get
      {
        return TimeSpan.FromSeconds((double) this.BufferedBytes / (double) this.WaveFormat.AverageBytesPerSecond);
      }
    }

    public WaveFormat WaveFormat => this.waveFormat;

    public void AddSamples(byte[] buffer, int offset, int count)
    {
      if (this.circularBuffer == null)
        this.circularBuffer = new CircularBuffer(this.BufferLength);
      if (this.circularBuffer.Write(buffer, offset, count) < count && !this.DiscardOnBufferOverflow)
        throw new InvalidOperationException("Buffer full");
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      int num = 0;
      if (this.circularBuffer != null)
        num = this.circularBuffer.Read(buffer, offset, count);
      if (num < count)
        Array.Clear((Array) buffer, offset + num, count - num);
      return count;
    }

    public void ClearBuffer()
    {
      if (this.circularBuffer == null)
        return;
      this.circularBuffer.Reset();
    }
  }
}
