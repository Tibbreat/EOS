// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.EEndpointHardwareSupport
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.CoreAudioApi
{
  [Flags]
  public enum EEndpointHardwareSupport
  {
    Volume = 1,
    Mute = 2,
    Meter = 4,
  }
}
