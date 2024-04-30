// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.ModulatorBuilder
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;

#nullable disable
namespace NAudio.SoundFont
{
  internal class ModulatorBuilder : StructureBuilder<Modulator>
  {
    public override Modulator Read(BinaryReader br)
    {
      Modulator modulator = new Modulator();
      modulator.SourceModulationData = new ModulatorType(br.ReadUInt16());
      modulator.DestinationGenerator = (GeneratorEnum) br.ReadUInt16();
      modulator.Amount = br.ReadInt16();
      modulator.SourceModulationAmount = new ModulatorType(br.ReadUInt16());
      modulator.SourceTransform = (TransformEnum) br.ReadUInt16();
      this.data.Add(modulator);
      return modulator;
    }

    public override void Write(BinaryWriter bw, Modulator o)
    {
    }

    public override int Length => 10;

    public Modulator[] Modulators => this.data.ToArray();
  }
}
