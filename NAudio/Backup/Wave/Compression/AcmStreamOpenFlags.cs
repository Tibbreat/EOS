﻿// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmStreamOpenFlags
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.Compression
{
  [Flags]
  internal enum AcmStreamOpenFlags
  {
    Query = 1,
    Async = 2,
    NonRealTime = 4,
    CallbackTypeMask = 458752, // 0x00070000
    CallbackNull = 0,
    CallbackWindow = 65536, // 0x00010000
    CallbackTask = 131072, // 0x00020000
    CallbackFunction = CallbackTask | CallbackWindow, // 0x00030000
    CallbackThread = CallbackTask, // 0x00020000
    CallbackEvent = 327680, // 0x00050000
  }
}
