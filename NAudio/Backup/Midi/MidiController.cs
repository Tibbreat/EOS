// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiController
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Midi
{
  public enum MidiController : byte
  {
    BankSelect = 0,
    Modulation = 1,
    BreathController = 2,
    FootController = 4,
    MainVolume = 7,
    Pan = 10, // 0x0A
    Expression = 11, // 0x0B
    BankSelectLsb = 32, // 0x20
    Sustain = 64, // 0x40
    Portamento = 65, // 0x41
    Sostenuto = 66, // 0x42
    SoftPedal = 67, // 0x43
    LegatoFootswitch = 68, // 0x44
    ResetAllControllers = 121, // 0x79
    AllNotesOff = 123, // 0x7B
  }
}
