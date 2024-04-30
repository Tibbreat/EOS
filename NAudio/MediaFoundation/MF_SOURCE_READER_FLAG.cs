// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.MF_SOURCE_READER_FLAG
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Flags]
  public enum MF_SOURCE_READER_FLAG
  {
    None = 0,
    MF_SOURCE_READERF_ERROR = 1,
    MF_SOURCE_READERF_ENDOFSTREAM = 2,
    MF_SOURCE_READERF_NEWSTREAM = 4,
    MF_SOURCE_READERF_NATIVEMEDIATYPECHANGED = 16, // 0x00000010
    MF_SOURCE_READERF_CURRENTMEDIATYPECHANGED = 32, // 0x00000020
    MF_SOURCE_READERF_STREAMTICK = 256, // 0x00000100
    MF_SOURCE_READERF_ALLEFFECTSREMOVED = 512, // 0x00000200
  }
}
