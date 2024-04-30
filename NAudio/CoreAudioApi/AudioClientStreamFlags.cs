// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioClientStreamFlags
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.CoreAudioApi
{
  [Flags]
  public enum AudioClientStreamFlags
  {
    None = 0,
    CrossProcess = 65536, // 0x00010000
    Loopback = 131072, // 0x00020000
    EventCallback = 262144, // 0x00040000
    NoPersist = 524288, // 0x00080000
  }
}
