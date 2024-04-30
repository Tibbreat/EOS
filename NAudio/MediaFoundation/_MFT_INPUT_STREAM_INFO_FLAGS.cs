// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation._MFT_INPUT_STREAM_INFO_FLAGS
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Flags]
  public enum _MFT_INPUT_STREAM_INFO_FLAGS
  {
    None = 0,
    MFT_INPUT_STREAM_WHOLE_SAMPLES = 1,
    MFT_INPUT_STREAM_SINGLE_SAMPLE_PER_BUFFER = 2,
    MFT_INPUT_STREAM_FIXED_SAMPLE_SIZE = 4,
    MFT_INPUT_STREAM_HOLDS_BUFFERS = 8,
    MFT_INPUT_STREAM_DOES_NOT_ADDREF = 256, // 0x00000100
    MFT_INPUT_STREAM_REMOVABLE = 512, // 0x00000200
    MFT_INPUT_STREAM_OPTIONAL = 1024, // 0x00000400
    MFT_INPUT_STREAM_PROCESSES_IN_PLACE = 2048, // 0x00000800
  }
}
