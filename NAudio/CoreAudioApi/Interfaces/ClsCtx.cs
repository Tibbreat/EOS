﻿// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.ClsCtx
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [Flags]
  internal enum ClsCtx
  {
    INPROC_SERVER = 1,
    INPROC_HANDLER = 2,
    LOCAL_SERVER = 4,
    INPROC_SERVER16 = 8,
    REMOTE_SERVER = 16, // 0x00000010
    INPROC_HANDLER16 = 32, // 0x00000020
    NO_CODE_DOWNLOAD = 1024, // 0x00000400
    NO_CUSTOM_MARSHAL = 4096, // 0x00001000
    ENABLE_CODE_DOWNLOAD = 8192, // 0x00002000
    NO_FAILURE_LOG = 16384, // 0x00004000
    DISABLE_AAA = 32768, // 0x00008000
    ENABLE_AAA = 65536, // 0x00010000
    FROM_DEFAULT_CONTEXT = 131072, // 0x00020000
    ACTIVATE_32_BIT_SERVER = 262144, // 0x00040000
    ACTIVATE_64_BIT_SERVER = 524288, // 0x00080000
    ENABLE_CLOAKING = 1048576, // 0x00100000
    PS_DLL = -2147483648, // 0x80000000
    INPROC = INPROC_HANDLER | INPROC_SERVER, // 0x00000003
    SERVER = REMOTE_SERVER | LOCAL_SERVER | INPROC_SERVER, // 0x00000015
    ALL = SERVER | INPROC_HANDLER, // 0x00000017
  }
}
