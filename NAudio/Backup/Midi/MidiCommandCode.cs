// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiCommandCode
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Midi
{
  public enum MidiCommandCode : byte
  {
    NoteOff = 128, // 0x80
    NoteOn = 144, // 0x90
    KeyAfterTouch = 160, // 0xA0
    ControlChange = 176, // 0xB0
    PatchChange = 192, // 0xC0
    ChannelAfterTouch = 208, // 0xD0
    PitchWheelChange = 224, // 0xE0
    Sysex = 240, // 0xF0
    Eox = 247, // 0xF7
    TimingClock = 248, // 0xF8
    StartSequence = 250, // 0xFA
    ContinueSequence = 251, // 0xFB
    StopSequence = 252, // 0xFC
    AutoSensing = 254, // 0xFE
    MetaEvent = 255, // 0xFF
  }
}
