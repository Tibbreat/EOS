// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.CircularBuffer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Utils
{
  public class CircularBuffer
  {
    private readonly byte[] buffer;
    private readonly object lockObject;
    private int writePosition;
    private int readPosition;
    private int byteCount;

    public CircularBuffer(int size)
    {
      this.buffer = new byte[size];
      this.lockObject = new object();
    }

    public int Write(byte[] data, int offset, int count)
    {
      lock (this.lockObject)
      {
        int num1 = 0;
        if (count > this.buffer.Length - this.byteCount)
          count = this.buffer.Length - this.byteCount;
        int length = Math.Min(this.buffer.Length - this.writePosition, count);
        Array.Copy((Array) data, offset, (Array) this.buffer, this.writePosition, length);
        this.writePosition += length;
        this.writePosition %= this.buffer.Length;
        int num2 = num1 + length;
        if (num2 < count)
        {
          Array.Copy((Array) data, offset + num2, (Array) this.buffer, this.writePosition, count - num2);
          this.writePosition += count - num2;
          num2 = count;
        }
        this.byteCount += num2;
        return num2;
      }
    }

    public int Read(byte[] data, int offset, int count)
    {
      lock (this.lockObject)
      {
        if (count > this.byteCount)
          count = this.byteCount;
        int num1 = 0;
        int length = Math.Min(this.buffer.Length - this.readPosition, count);
        Array.Copy((Array) this.buffer, this.readPosition, (Array) data, offset, length);
        int num2 = num1 + length;
        this.readPosition += length;
        this.readPosition %= this.buffer.Length;
        if (num2 < count)
        {
          Array.Copy((Array) this.buffer, this.readPosition, (Array) data, offset + num2, count - num2);
          this.readPosition += count - num2;
          num2 = count;
        }
        this.byteCount -= num2;
        return num2;
      }
    }

    public int MaxLength => this.buffer.Length;

    public int Count => this.byteCount;

    public void Reset()
    {
      this.byteCount = 0;
      this.readPosition = 0;
      this.writePosition = 0;
    }

    public void Advance(int count)
    {
      if (count >= this.byteCount)
      {
        this.Reset();
      }
      else
      {
        this.byteCount -= count;
        this.readPosition += count;
        this.readPosition %= this.MaxLength;
      }
    }
  }
}
