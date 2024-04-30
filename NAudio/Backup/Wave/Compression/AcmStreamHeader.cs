// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmStreamHeader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Compression
{
  internal class AcmStreamHeader : IDisposable
  {
    private AcmStreamHeaderStruct streamHeader;
    private byte[] sourceBuffer;
    private GCHandle hSourceBuffer;
    private byte[] destBuffer;
    private GCHandle hDestBuffer;
    private IntPtr streamHandle;
    private bool firstTime;
    private bool disposed;

    public AcmStreamHeader(IntPtr streamHandle, int sourceBufferLength, int destBufferLength)
    {
      this.streamHeader = new AcmStreamHeaderStruct();
      this.sourceBuffer = new byte[sourceBufferLength];
      this.hSourceBuffer = GCHandle.Alloc((object) this.sourceBuffer, GCHandleType.Pinned);
      this.destBuffer = new byte[destBufferLength];
      this.hDestBuffer = GCHandle.Alloc((object) this.destBuffer, GCHandleType.Pinned);
      this.streamHandle = streamHandle;
      this.firstTime = true;
    }

    private void Prepare()
    {
      this.streamHeader.cbStruct = Marshal.SizeOf((object) this.streamHeader);
      this.streamHeader.sourceBufferLength = this.sourceBuffer.Length;
      this.streamHeader.sourceBufferPointer = this.hSourceBuffer.AddrOfPinnedObject();
      this.streamHeader.destBufferLength = this.destBuffer.Length;
      this.streamHeader.destBufferPointer = this.hDestBuffer.AddrOfPinnedObject();
      MmException.Try(AcmInterop.acmStreamPrepareHeader(this.streamHandle, this.streamHeader, 0), "acmStreamPrepareHeader");
    }

    private void Unprepare()
    {
      this.streamHeader.sourceBufferLength = this.sourceBuffer.Length;
      this.streamHeader.sourceBufferPointer = this.hSourceBuffer.AddrOfPinnedObject();
      this.streamHeader.destBufferLength = this.destBuffer.Length;
      this.streamHeader.destBufferPointer = this.hDestBuffer.AddrOfPinnedObject();
      MmResult result = AcmInterop.acmStreamUnprepareHeader(this.streamHandle, this.streamHeader, 0);
      if (result != MmResult.NoError)
        throw new MmException(result, "acmStreamUnprepareHeader");
    }

    public void Reposition() => this.firstTime = true;

    public int Convert(int bytesToConvert, out int sourceBytesConverted)
    {
      this.Prepare();
      try
      {
        this.streamHeader.sourceBufferLength = bytesToConvert;
        this.streamHeader.sourceBufferLengthUsed = bytesToConvert;
        MmException.Try(AcmInterop.acmStreamConvert(this.streamHandle, this.streamHeader, this.firstTime ? AcmStreamConvertFlags.BlockAlign | AcmStreamConvertFlags.Start : AcmStreamConvertFlags.BlockAlign), "acmStreamConvert");
        this.firstTime = false;
        sourceBytesConverted = this.streamHeader.sourceBufferLengthUsed;
      }
      finally
      {
        this.Unprepare();
      }
      return this.streamHeader.destBufferLengthUsed;
    }

    public byte[] SourceBuffer => this.sourceBuffer;

    public byte[] DestBuffer => this.destBuffer;

    public void Dispose()
    {
      GC.SuppressFinalize((object) this);
      this.Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        this.sourceBuffer = (byte[]) null;
        this.destBuffer = (byte[]) null;
        this.hSourceBuffer.Free();
        this.hDestBuffer.Free();
      }
      this.disposed = true;
    }

    ~AcmStreamHeader() => this.Dispose(false);
  }
}
