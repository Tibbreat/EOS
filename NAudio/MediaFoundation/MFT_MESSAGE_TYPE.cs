﻿// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.MFT_MESSAGE_TYPE
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.MediaFoundation
{
  public enum MFT_MESSAGE_TYPE
  {
    MFT_MESSAGE_COMMAND_FLUSH = 0,
    MFT_MESSAGE_COMMAND_DRAIN = 1,
    MFT_MESSAGE_SET_D3D_MANAGER = 2,
    MFT_MESSAGE_DROP_SAMPLES = 3,
    MFT_MESSAGE_COMMAND_TICK = 4,
    MFT_MESSAGE_NOTIFY_BEGIN_STREAMING = 268435456, // 0x10000000
    MFT_MESSAGE_NOTIFY_END_STREAMING = 268435457, // 0x10000001
    MFT_MESSAGE_NOTIFY_END_OF_STREAM = 268435458, // 0x10000002
    MFT_MESSAGE_NOTIFY_START_OF_STREAM = 268435459, // 0x10000003
    MFT_MESSAGE_COMMAND_MARKER = 536870912, // 0x20000000
  }
}
