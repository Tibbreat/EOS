// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.MediaBuffer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Dmo
{
  public class MediaBuffer : IMediaBuffer, IDisposable
  {
    private IntPtr buffer;
    private int length;
    private int maxLength;

    public MediaBuffer(int maxLength)
    {
      this.buffer = Marshal.AllocCoTaskMem(maxLength);
      this.maxLength = maxLength;
    }

    public void Dispose()
    {
      if (!(this.buffer != IntPtr.Zero))
        return;
      Marshal.FreeCoTaskMem(this.buffer);
      this.buffer = IntPtr.Zero;
      GC.SuppressFinalize((object) this);
    }

    ~MediaBuffer() => this.Dispose();

    int IMediaBuffer.SetLength(int length)
    {
      if (length > this.maxLength)
        return -2147483645;
      this.length = length;
      return 0;
    }

    int IMediaBuffer.GetMaxLength(out int maxLength)
    {
      maxLength = this.maxLength;
      return 0;
    }

    int IMediaBuffer.GetBufferAndLength(IntPtr bufferPointerPointer, IntPtr validDataLengthPointer)
    {
      if (bufferPointerPointer != IntPtr.Zero)
        Marshal.WriteIntPtr(bufferPointerPointer, this.buffer);
      if (validDataLengthPointer != IntPtr.Zero)
        Marshal.WriteInt32(validDataLengthPointer, this.length);
      return 0;
    }

    public int Length
    {
      get => this.length;
      set
      {
        this.length = this.length <= this.maxLength ? value : throw new ArgumentException("Cannot be greater than maximum buffer size");
      }
    }

    public void LoadData(byte[] data, int bytes)
    {
      this.Length = bytes;
      Marshal.Copy(data, 0, this.buffer, bytes);
    }

    public void RetrieveData(byte[] data, int offset)
    {
      Marshal.Copy(this.buffer, data, offset, this.Length);
    }
  }
}
