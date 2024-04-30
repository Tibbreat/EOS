// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.Preset
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.SoundFont
{
  public class Preset
  {
    private string name;
    private ushort patchNumber;
    private ushort bank;
    internal ushort startPresetZoneIndex;
    internal ushort endPresetZoneIndex;
    internal uint library;
    internal uint genre;
    internal uint morphology;
    private Zone[] zones;

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public ushort PatchNumber
    {
      get => this.patchNumber;
      set => this.patchNumber = value;
    }

    public ushort Bank
    {
      get => this.bank;
      set => this.bank = value;
    }

    public Zone[] Zones
    {
      get => this.zones;
      set => this.zones = value;
    }

    public override string ToString()
    {
      return string.Format("{0}-{1} {2}", (object) this.bank, (object) this.patchNumber, (object) this.name);
    }
  }
}
