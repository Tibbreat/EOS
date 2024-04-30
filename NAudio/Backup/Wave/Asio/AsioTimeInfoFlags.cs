// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.AsioTimeInfoFlags
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.Asio
{
  [Flags]
  internal enum AsioTimeInfoFlags
  {
    kSystemTimeValid = 1,
    kSamplePositionValid = 2,
    kSampleRateValid = 4,
    kSpeedValid = 8,
    kSampleRateChanged = 16, // 0x00000010
    kClockSourceChanged = 32, // 0x00000020
  }
}
