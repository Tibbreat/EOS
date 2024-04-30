// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.IWMResamplerProps
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Dmo
{
  [Guid("E7E9984F-F09F-4da4-903F-6E2E0EFE56B5")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IWMResamplerProps
  {
    int SetHalfFilterLength(int outputQuality);

    int SetUserChannelMtx([In] float[] channelConversionMatrix);
  }
}
