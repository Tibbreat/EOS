﻿// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmFormatChooseStyleFlags
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.Compression
{
  [Flags]
  internal enum AcmFormatChooseStyleFlags
  {
    None = 0,
    ShowHelp = 4,
    EnableHook = 8,
    EnableTemplate = 16, // 0x00000010
    EnableTemplateHandle = 32, // 0x00000020
    InitToWfxStruct = 64, // 0x00000040
    ContextHelp = 128, // 0x00000080
  }
}
