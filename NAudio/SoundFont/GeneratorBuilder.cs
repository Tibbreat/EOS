// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.GeneratorBuilder
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;

#nullable disable
namespace NAudio.SoundFont
{
  internal class GeneratorBuilder : StructureBuilder<Generator>
  {
    public override Generator Read(BinaryReader br)
    {
      Generator generator = new Generator();
      generator.GeneratorType = (GeneratorEnum) br.ReadUInt16();
      generator.UInt16Amount = br.ReadUInt16();
      this.data.Add(generator);
      return generator;
    }

    public override void Write(BinaryWriter bw, Generator o)
    {
    }

    public override int Length => 4;

    public Generator[] Generators => this.data.ToArray();

    public void Load(Instrument[] instruments)
    {
      foreach (Generator generator in this.Generators)
      {
        if (generator.GeneratorType == GeneratorEnum.Instrument)
          generator.Instrument = instruments[(int) generator.UInt16Amount];
      }
    }

    public void Load(SampleHeader[] sampleHeaders)
    {
      foreach (Generator generator in this.Generators)
      {
        if (generator.GeneratorType == GeneratorEnum.SampleID)
          generator.SampleHeader = sampleHeaders[(int) generator.UInt16Amount];
      }
    }
  }
}
