// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioMeterInformation
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064")]
  internal interface IAudioMeterInformation
  {
    int GetPeakValue(out float pfPeak);

    int GetMeteringChannelCount(out int pnChannelCount);

    int GetChannelsPeakValues(int u32ChannelCount, [In] IntPtr afPeakValues);

    int QueryHardwareSupport(out int pdwHardwareSupportMask);
  }
}
