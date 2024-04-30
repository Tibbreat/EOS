// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.DmoOutputDataBuffer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Dmo
{
  [StructLayout(LayoutKind.Sequential, Pack = 8)]
  public struct DmoOutputDataBuffer : IDisposable
  {
    [MarshalAs(UnmanagedType.Interface)]
    private IMediaBuffer pBuffer;
    private DmoOutputDataBufferFlags dwStatus;
    private long rtTimestamp;
    private long referenceTimeDuration;

    public DmoOutputDataBuffer(int maxBufferSize)
    {
      this.pBuffer = (IMediaBuffer) new NAudio.Dmo.MediaBuffer(maxBufferSize);
      this.dwStatus = DmoOutputDataBufferFlags.None;
      this.rtTimestamp = 0L;
      this.referenceTimeDuration = 0L;
    }

    public void Dispose()
    {
      if (this.pBuffer == null)
        return;
      ((NAudio.Dmo.MediaBuffer) this.pBuffer).Dispose();
      this.pBuffer = (IMediaBuffer) null;
      GC.SuppressFinalize((object) this);
    }

    public IMediaBuffer MediaBuffer
    {
      get => this.pBuffer;
      internal set => this.pBuffer = value;
    }

    public int Length => ((NAudio.Dmo.MediaBuffer) this.pBuffer).Length;

    public DmoOutputDataBufferFlags StatusFlags
    {
      get => this.dwStatus;
      internal set => this.dwStatus = value;
    }

    public long Timestamp
    {
      get => this.rtTimestamp;
      internal set => this.rtTimestamp = value;
    }

    public long Duration
    {
      get => this.referenceTimeDuration;
      internal set => this.referenceTimeDuration = value;
    }

    public void RetrieveData(byte[] data, int offset)
    {
      ((NAudio.Dmo.MediaBuffer) this.pBuffer).RetrieveData(data, offset);
    }

    public bool MoreDataAvailable
    {
      get
      {
        return (this.StatusFlags & DmoOutputDataBufferFlags.Incomplete) == DmoOutputDataBufferFlags.Incomplete;
      }
    }
  }
}
