﻿// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.AcmMetrics
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave
{
  internal enum AcmMetrics
  {
    CountDrivers = 1,
    CountCodecs = 2,
    CountConverters = 3,
    CountFilters = 4,
    CountDisabled = 5,
    CountHardware = 6,
    CountLocalDrivers = 20, // 0x00000014
    CountLocalCodecs = 21, // 0x00000015
    CountLocalConverters = 22, // 0x00000016
    CountLocalFilters = 23, // 0x00000017
    CountLocalDisabled = 24, // 0x00000018
    HardwareWaveInput = 30, // 0x0000001E
    HardwareWaveOutput = 31, // 0x0000001F
    MaxSizeFormat = 50, // 0x00000032
    MaxSizeFilter = 51, // 0x00000033
    DriverSupport = 100, // 0x00000064
    DriverPriority = 101, // 0x00000065
  }
}
