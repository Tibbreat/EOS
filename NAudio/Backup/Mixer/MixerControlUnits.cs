// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.MixerControlUnits
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Mixer
{
  [Flags]
  internal enum MixerControlUnits
  {
    Custom = 0,
    Boolean = 65536, // 0x00010000
    Signed = 131072, // 0x00020000
    Unsigned = Signed | Boolean, // 0x00030000
    Decibels = 262144, // 0x00040000
    Percent = Decibels | Boolean, // 0x00050000
    Mask = 16711680, // 0x00FF0000
  }
}
