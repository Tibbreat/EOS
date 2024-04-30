// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation._MFT_PROCESS_OUTPUT_STATUS
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Flags]
  public enum _MFT_PROCESS_OUTPUT_STATUS
  {
    None = 0,
    MFT_PROCESS_OUTPUT_STATUS_NEW_STREAMS = 256, // 0x00000100
  }
}
