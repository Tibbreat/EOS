// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmFormatSuggestFlags
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.Compression
{
  [Flags]
  internal enum AcmFormatSuggestFlags
  {
    FormatTag = 65536, // 0x00010000
    Channels = 131072, // 0x00020000
    SamplesPerSecond = 262144, // 0x00040000
    BitsPerSample = 524288, // 0x00080000
    TypeMask = 16711680, // 0x00FF0000
  }
}
