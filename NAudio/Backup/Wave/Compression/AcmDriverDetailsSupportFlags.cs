// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmDriverDetailsSupportFlags
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.Compression
{
  [Flags]
  public enum AcmDriverDetailsSupportFlags
  {
    Codec = 1,
    Converter = 2,
    Filter = 4,
    Hardware = 8,
    Async = 16, // 0x00000010
    Local = 1073741824, // 0x40000000
    Disabled = -2147483648, // 0x80000000
  }
}
