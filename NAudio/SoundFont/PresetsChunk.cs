// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.PresetsChunk
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;
using System.Text;

#nullable disable
namespace NAudio.SoundFont
{
  public class PresetsChunk
  {
    private PresetBuilder presetHeaders = new PresetBuilder();
    private ZoneBuilder presetZones = new ZoneBuilder();
    private ModulatorBuilder presetZoneModulators = new ModulatorBuilder();
    private GeneratorBuilder presetZoneGenerators = new GeneratorBuilder();
    private InstrumentBuilder instruments = new InstrumentBuilder();
    private ZoneBuilder instrumentZones = new ZoneBuilder();
    private ModulatorBuilder instrumentZoneModulators = new ModulatorBuilder();
    private GeneratorBuilder instrumentZoneGenerators = new GeneratorBuilder();
    private SampleHeaderBuilder sampleHeaders = new SampleHeaderBuilder();

    internal PresetsChunk(RiffChunk chunk)
    {
      string str = chunk.ReadChunkID();
      if (str != "pdta")
        throw new InvalidDataException(string.Format("Not a presets data chunk ({0})", (object) str));
      RiffChunk nextSubChunk;
      while ((nextSubChunk = chunk.GetNextSubChunk()) != null)
      {
        switch (nextSubChunk.ChunkID)
        {
          case "PHDR":
          case "phdr":
            nextSubChunk.GetDataAsStructureArray<Preset>((StructureBuilder<Preset>) this.presetHeaders);
            continue;
          case "PBAG":
          case "pbag":
            nextSubChunk.GetDataAsStructureArray<Zone>((StructureBuilder<Zone>) this.presetZones);
            continue;
          case "PMOD":
          case "pmod":
            nextSubChunk.GetDataAsStructureArray<Modulator>((StructureBuilder<Modulator>) this.presetZoneModulators);
            continue;
          case "PGEN":
          case "pgen":
            nextSubChunk.GetDataAsStructureArray<Generator>((StructureBuilder<Generator>) this.presetZoneGenerators);
            continue;
          case "INST":
          case "inst":
            nextSubChunk.GetDataAsStructureArray<Instrument>((StructureBuilder<Instrument>) this.instruments);
            continue;
          case "IBAG":
          case "ibag":
            nextSubChunk.GetDataAsStructureArray<Zone>((StructureBuilder<Zone>) this.instrumentZones);
            continue;
          case "IMOD":
          case "imod":
            nextSubChunk.GetDataAsStructureArray<Modulator>((StructureBuilder<Modulator>) this.instrumentZoneModulators);
            continue;
          case "IGEN":
          case "igen":
            nextSubChunk.GetDataAsStructureArray<Generator>((StructureBuilder<Generator>) this.instrumentZoneGenerators);
            continue;
          case "SHDR":
          case "shdr":
            nextSubChunk.GetDataAsStructureArray<SampleHeader>((StructureBuilder<SampleHeader>) this.sampleHeaders);
            continue;
          default:
            throw new InvalidDataException(string.Format("Unknown chunk type {0}", (object) nextSubChunk.ChunkID));
        }
      }
      this.instrumentZoneGenerators.Load(this.sampleHeaders.SampleHeaders);
      this.instrumentZones.Load(this.instrumentZoneModulators.Modulators, this.instrumentZoneGenerators.Generators);
      this.instruments.LoadZones(this.instrumentZones.Zones);
      this.presetZoneGenerators.Load(this.instruments.Instruments);
      this.presetZones.Load(this.presetZoneModulators.Modulators, this.presetZoneGenerators.Generators);
      this.presetHeaders.LoadZones(this.presetZones.Zones);
      this.sampleHeaders.RemoveEOS();
    }

    public Preset[] Presets => this.presetHeaders.Presets;

    public Instrument[] Instruments => this.instruments.Instruments;

    public SampleHeader[] SampleHeaders => this.sampleHeaders.SampleHeaders;

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("Preset Headers:\r\n");
      foreach (Preset preset in this.presetHeaders.Presets)
        stringBuilder.AppendFormat("{0}\r\n", (object) preset);
      stringBuilder.Append("Instruments:\r\n");
      foreach (Instrument instrument in this.instruments.Instruments)
        stringBuilder.AppendFormat("{0}\r\n", (object) instrument);
      return stringBuilder.ToString();
    }
  }
}
