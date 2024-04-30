// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.ControllerSourceEnum
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.SoundFont
{
  public enum ControllerSourceEnum
  {
    NoController = 0,
    NoteOnVelocity = 2,
    NoteOnKeyNumber = 3,
    PolyPressure = 10, // 0x0000000A
    ChannelPressure = 13, // 0x0000000D
    PitchWheel = 14, // 0x0000000E
    PitchWheelSensitivity = 16, // 0x00000010
  }
}
