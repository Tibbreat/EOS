// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.MixerFlags
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Mixer
{
  [Flags]
  public enum MixerFlags
  {
    Handle = -2147483648, // 0x80000000
    Mixer = 0,
    MixerHandle = Handle, // 0x80000000
    WaveOut = 268435456, // 0x10000000
    WaveOutHandle = WaveOut | MixerHandle, // 0x90000000
    WaveIn = 536870912, // 0x20000000
    WaveInHandle = WaveIn | MixerHandle, // 0xA0000000
    MidiOut = WaveIn | WaveOut, // 0x30000000
    MidiOutHandle = MidiOut | MixerHandle, // 0xB0000000
    MidiIn = 1073741824, // 0x40000000
    MidiInHandle = MidiIn | MixerHandle, // 0xC0000000
    Aux = MidiIn | WaveOut, // 0x50000000
    Value = 0,
    ListText = 1,
    QueryMask = 15, // 0x0000000F
    All = 0,
    OneById = ListText, // 0x00000001
    OneByType = 2,
    GetLineInfoOfDestination = 0,
    GetLineInfoOfSource = OneById, // 0x00000001
    GetLineInfoOfLineId = OneByType, // 0x00000002
    GetLineInfoOfComponentType = GetLineInfoOfLineId | GetLineInfoOfSource, // 0x00000003
    GetLineInfoOfTargetType = 4,
    GetLineInfoOfQueryMask = 15, // 0x0000000F
  }
}
