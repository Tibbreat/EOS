// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MetaEventType
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Midi
{
  public enum MetaEventType : byte
  {
    TrackSequenceNumber = 0,
    TextEvent = 1,
    Copyright = 2,
    SequenceTrackName = 3,
    TrackInstrumentName = 4,
    Lyric = 5,
    Marker = 6,
    CuePoint = 7,
    ProgramName = 8,
    DeviceName = 9,
    MidiChannel = 32, // 0x20
    MidiPort = 33, // 0x21
    EndTrack = 47, // 0x2F
    SetTempo = 81, // 0x51
    SmpteOffset = 84, // 0x54
    TimeSignature = 88, // 0x58
    KeySignature = 89, // 0x59
    SequencerSpecific = 127, // 0x7F
  }
}
