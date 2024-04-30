// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation._MFT_OUTPUT_STREAM_INFO_FLAGS
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Flags]
  public enum _MFT_OUTPUT_STREAM_INFO_FLAGS
  {
    None = 0,
    MFT_OUTPUT_STREAM_WHOLE_SAMPLES = 1,
    MFT_OUTPUT_STREAM_SINGLE_SAMPLE_PER_BUFFER = 2,
    MFT_OUTPUT_STREAM_FIXED_SAMPLE_SIZE = 4,
    MFT_OUTPUT_STREAM_DISCARDABLE = 8,
    MFT_OUTPUT_STREAM_OPTIONAL = 16, // 0x00000010
    MFT_OUTPUT_STREAM_PROVIDES_SAMPLES = 256, // 0x00000100
    MFT_OUTPUT_STREAM_CAN_PROVIDE_SAMPLES = 512, // 0x00000200
    MFT_OUTPUT_STREAM_LAZY_READ = 1024, // 0x00000400
    MFT_OUTPUT_STREAM_REMOVABLE = 2048, // 0x00000800
  }
}
