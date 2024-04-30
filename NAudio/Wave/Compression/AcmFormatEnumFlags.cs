// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmFormatEnumFlags
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.Compression
{
  [Flags]
  public enum AcmFormatEnumFlags
  {
    None = 0,
    Convert = 1048576, // 0x00100000
    Hardware = 4194304, // 0x00400000
    Input = 8388608, // 0x00800000
    Channels = 131072, // 0x00020000
    SamplesPerSecond = 262144, // 0x00040000
    Output = 16777216, // 0x01000000
    Suggest = 2097152, // 0x00200000
    BitsPerSample = 524288, // 0x00080000
    FormatTag = 65536, // 0x00010000
  }
}
