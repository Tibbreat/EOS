// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.MixerControlClass
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Mixer
{
  [Flags]
  internal enum MixerControlClass
  {
    Custom = 0,
    Meter = 268435456, // 0x10000000
    Switch = 536870912, // 0x20000000
    Number = Switch | Meter, // 0x30000000
    Slider = 1073741824, // 0x40000000
    Fader = Slider | Meter, // 0x50000000
    Time = Slider | Switch, // 0x60000000
    List = Time | Meter, // 0x70000000
    Mask = List, // 0x70000000
  }
}
