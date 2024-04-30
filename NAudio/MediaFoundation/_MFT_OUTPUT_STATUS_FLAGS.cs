// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation._MFT_OUTPUT_STATUS_FLAGS
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Flags]
  public enum _MFT_OUTPUT_STATUS_FLAGS
  {
    None = 0,
    MFT_OUTPUT_STATUS_SAMPLE_READY = 1,
  }
}
