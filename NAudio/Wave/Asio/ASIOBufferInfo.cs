// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.ASIOBufferInfo
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Asio
{
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  internal struct ASIOBufferInfo
  {
    public bool isInput;
    public int channelNum;
    public IntPtr pBuffer0;
    public IntPtr pBuffer1;

    public IntPtr Buffer(int bufferIndex) => bufferIndex != 0 ? this.pBuffer1 : this.pBuffer0;
  }
}
