// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.AsioSampleType
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.Asio
{
  public enum AsioSampleType
  {
    Int16MSB = 0,
    Int24MSB = 1,
    Int32MSB = 2,
    Float32MSB = 3,
    Float64MSB = 4,
    Int32MSB16 = 8,
    Int32MSB18 = 9,
    Int32MSB20 = 10, // 0x0000000A
    Int32MSB24 = 11, // 0x0000000B
    Int16LSB = 16, // 0x00000010
    Int24LSB = 17, // 0x00000011
    Int32LSB = 18, // 0x00000012
    Float32LSB = 19, // 0x00000013
    Float64LSB = 20, // 0x00000014
    Int32LSB16 = 24, // 0x00000018
    Int32LSB18 = 25, // 0x00000019
    Int32LSB20 = 26, // 0x0000001A
    Int32LSB24 = 27, // 0x0000001B
    DSDInt8LSB1 = 32, // 0x00000020
    DSDInt8MSB1 = 33, // 0x00000021
    DSDInt8NER8 = 40, // 0x00000028
  }
}
