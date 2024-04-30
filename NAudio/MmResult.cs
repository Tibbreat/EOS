// Decompiled with JetBrains decompiler
// Type: NAudio.MmResult
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio
{
  public enum MmResult
  {
    NoError = 0,
    UnspecifiedError = 1,
    BadDeviceId = 2,
    NotEnabled = 3,
    AlreadyAllocated = 4,
    InvalidHandle = 5,
    NoDriver = 6,
    MemoryAllocationError = 7,
    NotSupported = 8,
    BadErrorNumber = 9,
    InvalidFlag = 10, // 0x0000000A
    InvalidParameter = 11, // 0x0000000B
    HandleBusy = 12, // 0x0000000C
    InvalidAlias = 13, // 0x0000000D
    BadRegistryDatabase = 14, // 0x0000000E
    RegistryKeyNotFound = 15, // 0x0000000F
    RegistryReadError = 16, // 0x00000010
    RegistryWriteError = 17, // 0x00000011
    RegistryDeleteError = 18, // 0x00000012
    RegistryValueNotFound = 19, // 0x00000013
    NoDriverCallback = 20, // 0x00000014
    MoreData = 21, // 0x00000015
    WaveBadFormat = 32, // 0x00000020
    WaveStillPlaying = 33, // 0x00000021
    WaveHeaderUnprepared = 34, // 0x00000022
    WaveSync = 35, // 0x00000023
    AcmNotPossible = 512, // 0x00000200
    AcmBusy = 513, // 0x00000201
    AcmHeaderUnprepared = 514, // 0x00000202
    AcmCancelled = 515, // 0x00000203
    MixerInvalidLine = 1024, // 0x00000400
    MixerInvalidControl = 1025, // 0x00000401
    MixerInvalidValue = 1026, // 0x00000402
  }
}
