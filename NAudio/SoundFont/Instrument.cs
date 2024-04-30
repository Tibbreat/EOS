// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.Instrument
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.SoundFont
{
  public class Instrument
  {
    private string name;
    internal ushort startInstrumentZoneIndex;
    internal ushort endInstrumentZoneIndex;
    private Zone[] zones;

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public Zone[] Zones
    {
      get => this.zones;
      set => this.zones = value;
    }

    public override string ToString() => this.name;
  }
}
