// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.ASIOChannelInfo
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Asio
{
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  internal struct ASIOChannelInfo
  {
    public int channel;
    public bool isInput;
    public bool isActive;
    public int channelGroup;
    [MarshalAs(UnmanagedType.U4)]
    public AsioSampleType type;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string name;
  }
}
