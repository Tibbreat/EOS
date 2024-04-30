// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation._MFT_OUTPUT_DATA_BUFFER_FLAGS
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Flags]
  public enum _MFT_OUTPUT_DATA_BUFFER_FLAGS
  {
    None = 0,
    MFT_OUTPUT_DATA_BUFFER_INCOMPLETE = 16777216, // 0x01000000
    MFT_OUTPUT_DATA_BUFFER_FORMAT_CHANGE = 256, // 0x00000100
    MFT_OUTPUT_DATA_BUFFER_STREAM_END = 512, // 0x00000200
    MFT_OUTPUT_DATA_BUFFER_NO_SAMPLE = MFT_OUTPUT_DATA_BUFFER_STREAM_END | MFT_OUTPUT_DATA_BUFFER_FORMAT_CHANGE, // 0x00000300
  }
}
