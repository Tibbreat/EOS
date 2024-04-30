// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation._MFT_ENUM_FLAG
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Flags]
  public enum _MFT_ENUM_FLAG
  {
    None = 0,
    MFT_ENUM_FLAG_SYNCMFT = 1,
    MFT_ENUM_FLAG_ASYNCMFT = 2,
    MFT_ENUM_FLAG_HARDWARE = 4,
    MFT_ENUM_FLAG_FIELDOFUSE = 8,
    MFT_ENUM_FLAG_LOCALMFT = 16, // 0x00000010
    MFT_ENUM_FLAG_TRANSCODE_ONLY = 32, // 0x00000020
    MFT_ENUM_FLAG_SORTANDFILTER = 64, // 0x00000040
    MFT_ENUM_FLAG_ALL = MFT_ENUM_FLAG_TRANSCODE_ONLY | MFT_ENUM_FLAG_LOCALMFT | MFT_ENUM_FLAG_FIELDOFUSE | MFT_ENUM_FLAG_HARDWARE | MFT_ENUM_FLAG_ASYNCMFT | MFT_ENUM_FLAG_SYNCMFT, // 0x0000003F
  }
}
