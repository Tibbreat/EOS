// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.ASIOMessageSelector
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.Asio
{
  internal enum ASIOMessageSelector
  {
    kAsioSelectorSupported = 1,
    kAsioEngineVersion = 2,
    kAsioResetRequest = 3,
    kAsioBufferSizeChange = 4,
    kAsioResyncRequest = 5,
    kAsioLatenciesChanged = 6,
    kAsioSupportsTimeInfo = 7,
    kAsioSupportsTimeCode = 8,
    kAsioMMCCommand = 9,
    kAsioSupportsInputMonitor = 10, // 0x0000000A
    kAsioSupportsInputGain = 11, // 0x0000000B
    kAsioSupportsInputMeter = 12, // 0x0000000C
    kAsioSupportsOutputGain = 13, // 0x0000000D
    kAsioSupportsOutputMeter = 14, // 0x0000000E
    kAsioOverload = 15, // 0x0000000F
  }
}
