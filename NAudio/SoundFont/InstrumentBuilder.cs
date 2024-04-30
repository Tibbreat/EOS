// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.InstrumentBuilder
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;
using System.Text;

#nullable disable
namespace NAudio.SoundFont
{
  internal class InstrumentBuilder : StructureBuilder<Instrument>
  {
    private Instrument lastInstrument;

    public override Instrument Read(BinaryReader br)
    {
      Instrument instrument = new Instrument();
      string str = Encoding.UTF8.GetString(br.ReadBytes(20), 0, 20);
      if (str.IndexOf(char.MinValue) >= 0)
        str = str.Substring(0, str.IndexOf(char.MinValue));
      instrument.Name = str;
      instrument.startInstrumentZoneIndex = br.ReadUInt16();
      if (this.lastInstrument != null)
        this.lastInstrument.endInstrumentZoneIndex = (ushort) ((uint) instrument.startInstrumentZoneIndex - 1U);
      this.data.Add(instrument);
      this.lastInstrument = instrument;
      return instrument;
    }

    public override void Write(BinaryWriter bw, Instrument instrument)
    {
    }

    public override int Length => 22;

    public void LoadZones(Zone[] zones)
    {
      for (int index = 0; index < this.data.Count - 1; ++index)
      {
        Instrument instrument = this.data[index];
        instrument.Zones = new Zone[(int) instrument.endInstrumentZoneIndex - (int) instrument.startInstrumentZoneIndex + 1];
        Array.Copy((Array) zones, (int) instrument.startInstrumentZoneIndex, (Array) instrument.Zones, 0, instrument.Zones.Length);
      }
      this.data.RemoveAt(this.data.Count - 1);
    }

    public Instrument[] Instruments => this.data.ToArray();
  }
}
