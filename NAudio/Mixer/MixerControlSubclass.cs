// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.MixerControlSubclass
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Mixer
{
  [Flags]
  internal enum MixerControlSubclass
  {
    SwitchBoolean = 0,
    SwitchButton = 16777216, // 0x01000000
    MeterPolled = 0,
    TimeMicrosecs = 0,
    TimeMillisecs = SwitchButton, // 0x01000000
    ListSingle = 0,
    ListMultiple = TimeMillisecs, // 0x01000000
    Mask = 251658240, // 0x0F000000
  }
}
