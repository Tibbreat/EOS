// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Wave32To16Stream
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave
{
  public class Wave32To16Stream : WaveStream
  {
    private WaveStream sourceStream;
    private readonly WaveFormat waveFormat;
    private readonly long length;
    private long position;
    private bool clip;
    private float volume;
    private readonly object lockObject = new object();

    public Wave32To16Stream(WaveStream sourceStream)
    {
      if (sourceStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
        throw new ArgumentException("Only 32 bit Floating point supported");
      if (sourceStream.WaveFormat.BitsPerSample != 32)
        throw new ArgumentException("Only 32 bit Floating point supported");
      this.waveFormat = new WaveFormat(sourceStream.WaveFormat.SampleRate, 16, sourceStream.WaveFormat.Channels);
      this.volume = 1f;
      this.sourceStream = sourceStream;
      this.length = sourceStream.Length / 2L;
      this.position = sourceStream.Position / 2L;
    }

    public float Volume
    {
      get => this.volume;
      set => this.volume = value;
    }

    public override int BlockAlign => this.sourceStream.BlockAlign / 2;

    public override long Length => this.length;

    public override long Position
    {
      get => this.position;
      set
      {
        lock (this.lockObject)
        {
          value -= value % (long) this.BlockAlign;
          this.sourceStream.Position = value * 2L;
          this.position = value;
        }
      }
    }

    public override int Read(byte[] destBuffer, int offset, int numBytes)
    {
      lock (this.lockObject)
      {
        byte[] numArray = new byte[numBytes * 2];
        int bytesRead = this.sourceStream.Read(numArray, 0, numBytes * 2);
        this.Convert32To16(destBuffer, offset, numArray, bytesRead);
        this.position += (long) (bytesRead / 2);
        return bytesRead / 2;
      }
    }

    private unsafe void Convert32To16(
      byte[] destBuffer,
      int offset,
      byte[] sourceBuffer,
      int bytesRead)
    {
      fixed (byte* numPtr1 = &destBuffer[offset])
        fixed (byte* numPtr2 = &sourceBuffer[0])
        {
          short* numPtr3 = (short*) numPtr1;
          float* numPtr4 = (float*) numPtr2;
          int num1 = bytesRead / 4;
          for (int index = 0; index < num1; ++index)
          {
            float num2 = numPtr4[index] * this.volume;
            if ((double) num2 > 1.0)
            {
              numPtr3[index] = short.MaxValue;
              this.clip = true;
            }
            else if ((double) num2 < -1.0)
            {
              numPtr3[index] = short.MinValue;
              this.clip = true;
            }
            else
              numPtr3[index] = (short) ((double) num2 * (double) short.MaxValue);
          }
        }
    }

    public override WaveFormat WaveFormat => this.waveFormat;

    public bool Clip
    {
      get => this.clip;
      set => this.clip = value;
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
  }
}
