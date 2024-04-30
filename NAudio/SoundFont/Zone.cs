// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.Zone
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.SoundFont
{
  public class Zone
  {
    internal ushort generatorIndex;
    internal ushort modulatorIndex;
    internal ushort generatorCount;
    internal ushort modulatorCount;
    private Modulator[] modulators;
    private Generator[] generators;

    public override string ToString()
    {
      return string.Format("Zone {0} Gens:{1} {2} Mods:{3}", (object) this.generatorCount, (object) this.generatorIndex, (object) this.modulatorCount, (object) this.modulatorIndex);
    }

    public Modulator[] Modulators
    {
      get => this.modulators;
      set => this.modulators = value;
    }

    public Generator[] Generators
    {
      get => this.generators;
      set => this.generators = value;
    }
  }
}
