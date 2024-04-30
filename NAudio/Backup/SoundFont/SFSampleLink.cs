// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.SFSampleLink
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.SoundFont
{
  public enum SFSampleLink : ushort
  {
    MonoSample = 1,
    RightSample = 2,
    LeftSample = 4,
    LinkedSample = 8,
    RomMonoSample = 32769, // 0x8001
    RomRightSample = 32770, // 0x8002
    RomLeftSample = 32772, // 0x8004
    RomLinkedSample = 32776, // 0x8008
  }
}
