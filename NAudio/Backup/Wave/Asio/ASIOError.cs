// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.ASIOError
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.Asio
{
  internal enum ASIOError
  {
    ASE_NotPresent = -1000, // 0xFFFFFC18
    ASE_HWMalfunction = -999, // 0xFFFFFC19
    ASE_InvalidParameter = -998, // 0xFFFFFC1A
    ASE_InvalidMode = -997, // 0xFFFFFC1B
    ASE_SPNotAdvancing = -996, // 0xFFFFFC1C
    ASE_NoClock = -995, // 0xFFFFFC1D
    ASE_NoMemory = -994, // 0xFFFFFC1E
    ASE_OK = 0,
    ASE_SUCCESS = 1061701536, // 0x3F4847A0
  }
}
