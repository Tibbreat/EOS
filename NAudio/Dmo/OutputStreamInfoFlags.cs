// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.OutputStreamInfoFlags
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Dmo
{
  [Flags]
  internal enum OutputStreamInfoFlags
  {
    DMO_OUTPUT_STREAMF_WHOLE_SAMPLES = 1,
    DMO_OUTPUT_STREAMF_SINGLE_SAMPLE_PER_BUFFER = 2,
    DMO_OUTPUT_STREAMF_FIXED_SAMPLE_SIZE = 4,
    DMO_OUTPUT_STREAMF_DISCARDABLE = 8,
    DMO_OUTPUT_STREAMF_OPTIONAL = 16, // 0x00000010
  }
}
