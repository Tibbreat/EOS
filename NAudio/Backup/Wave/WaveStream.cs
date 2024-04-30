// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveStream
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Wave
{
  public abstract class WaveStream : Stream, IWaveProvider
  {
    public abstract WaveFormat WaveFormat { get; }

    public override bool CanRead => true;

    public override bool CanSeek => true;

    public override bool CanWrite => false;

    public override void Flush()
    {
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      switch (origin)
      {
        case SeekOrigin.Begin:
          this.Position = offset;
          break;
        case SeekOrigin.Current:
          this.Position += offset;
          break;
        default:
          this.Position = this.Length + offset;
          break;
      }
      return this.Position;
    }

    public override void SetLength(long length)
    {
      throw new NotSupportedException("Can't set length of a WaveFormatString");
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException("Can't write to a WaveFormatString");
    }

    public virtual int BlockAlign => this.WaveFormat.BlockAlign;

    public void Skip(int seconds)
    {
      long num = this.Position + (long) (this.WaveFormat.AverageBytesPerSecond * seconds);
      if (num > this.Length)
        this.Position = this.Length;
      else if (num < 0L)
        this.Position = 0L;
      else
        this.Position = num;
    }

    public virtual TimeSpan CurrentTime
    {
      get
      {
        return TimeSpan.FromSeconds((double) this.Position / (double) this.WaveFormat.AverageBytesPerSecond);
      }
      set
      {
        this.Position = (long) (value.TotalSeconds * (double) this.WaveFormat.AverageBytesPerSecond);
      }
    }

    public virtual TimeSpan TotalTime
    {
      get
      {
        return TimeSpan.FromSeconds((double) this.Length / (double) this.WaveFormat.AverageBytesPerSecond);
      }
    }

    public virtual bool HasData(int count) => this.Position < this.Length;
  }
}
