// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.SFVersion
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.SoundFont
{
  public class SFVersion
  {
    private short major;
    private short minor;

    public short Major
    {
      get => this.major;
      set => this.major = value;
    }

    public short Minor
    {
      get => this.minor;
      set => this.minor = value;
    }
  }
}
