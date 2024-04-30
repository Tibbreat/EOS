// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveOutBuffer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  internal class WaveOutBuffer : IDisposable
  {
    private readonly WaveHeader header;
    private readonly int bufferSize;
    private readonly byte[] buffer;
    private readonly IWaveProvider waveStream;
    private readonly object waveOutLock;
    private GCHandle hBuffer;
    private IntPtr hWaveOut;
    private GCHandle hHeader;
    private GCHandle hThis;

    public WaveOutBuffer(
      IntPtr hWaveOut,
      int bufferSize,
      IWaveProvider bufferFillStream,
      object waveOutLock)
    {
      this.bufferSize = bufferSize;
      this.buffer = new byte[bufferSize];
      this.hBuffer = GCHandle.Alloc((object) this.buffer, GCHandleType.Pinned);
      this.hWaveOut = hWaveOut;
      this.waveStream = bufferFillStream;
      this.waveOutLock = waveOutLock;
      this.header = new WaveHeader();
      this.hHeader = GCHandle.Alloc((object) this.header, GCHandleType.Pinned);
      this.header.dataBuffer = this.hBuffer.AddrOfPinnedObject();
      this.header.bufferLength = bufferSize;
      this.header.loops = 1;
      this.hThis = GCHandle.Alloc((object) this);
      this.header.userData = (IntPtr) this.hThis;
      lock (waveOutLock)
        MmException.Try(WaveInterop.waveOutPrepareHeader(hWaveOut, this.header, Marshal.SizeOf((object) this.header)), "waveOutPrepareHeader");
    }

    ~WaveOutBuffer() => this.Dispose(false);

    public void Dispose()
    {
      GC.SuppressFinalize((object) this);
      this.Dispose(true);
    }

    protected void Dispose(bool disposing)
    {
      int num1 = disposing ? 1 : 0;
      if (this.hHeader.IsAllocated)
        this.hHeader.Free();
      if (this.hBuffer.IsAllocated)
        this.hBuffer.Free();
      if (this.hThis.IsAllocated)
        this.hThis.Free();
      if (!(this.hWaveOut != IntPtr.Zero))
        return;
      lock (this.waveOutLock)
      {
        int num2 = (int) WaveInterop.waveOutUnprepareHeader(this.hWaveOut, this.header, Marshal.SizeOf((object) this.header));
      }
      this.hWaveOut = IntPtr.Zero;
    }

    internal bool OnDone()
    {
      int num;
      lock (this.waveStream)
        num = this.waveStream.Read(this.buffer, 0, this.buffer.Length);
      if (num == 0)
        return false;
      for (int index = num; index < this.buffer.Length; ++index)
        this.buffer[index] = (byte) 0;
      this.WriteToWaveOut();
      return true;
    }

    public bool InQueue => (this.header.flags & WaveHeaderFlags.InQueue) == WaveHeaderFlags.InQueue;

    public int BufferSize => this.bufferSize;

    private void WriteToWaveOut()
    {
      MmResult result;
      lock (this.waveOutLock)
        result = WaveInterop.waveOutWrite(this.hWaveOut, this.header, Marshal.SizeOf((object) this.header));
      if (result != MmResult.NoError)
        throw new MmException(result, "waveOutWrite");
      GC.KeepAlive((object) this);
    }
  }
}
