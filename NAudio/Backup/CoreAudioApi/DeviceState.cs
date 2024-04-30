// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.DeviceState
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.CoreAudioApi
{
  [Flags]
  public enum DeviceState
  {
    Active = 1,
    Disabled = 2,
    NotPresent = 4,
    Unplugged = 8,
    All = Unplugged | NotPresent | Disabled | Active, // 0x0000000F
  }
}
