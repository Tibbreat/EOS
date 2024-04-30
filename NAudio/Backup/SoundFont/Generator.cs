// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.Generator
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.SoundFont
{
  public class Generator
  {
    private GeneratorEnum generatorType;
    private ushort rawAmount;
    private Instrument instrument;
    private SampleHeader sampleHeader;

    public GeneratorEnum GeneratorType
    {
      get => this.generatorType;
      set => this.generatorType = value;
    }

    public ushort UInt16Amount
    {
      get => this.rawAmount;
      set => this.rawAmount = value;
    }

    public short Int16Amount
    {
      get => (short) this.rawAmount;
      set => this.rawAmount = (ushort) value;
    }

    public byte LowByteAmount
    {
      get => (byte) ((uint) this.rawAmount & (uint) byte.MaxValue);
      set
      {
        this.rawAmount &= (ushort) 65280;
        this.rawAmount += (ushort) value;
      }
    }

    public byte HighByteAmount
    {
      get => (byte) (((int) this.rawAmount & 65280) >> 8);
      set
      {
        this.rawAmount &= (ushort) byte.MaxValue;
        this.rawAmount += (ushort) ((uint) value << 8);
      }
    }

    public Instrument Instrument
    {
      get => this.instrument;
      set => this.instrument = value;
    }

    public SampleHeader SampleHeader
    {
      get => this.sampleHeader;
      set => this.sampleHeader = value;
    }

    public override string ToString()
    {
      if (this.generatorType == GeneratorEnum.Instrument)
        return string.Format("Generator Instrument {0}", (object) this.instrument.Name);
      return this.generatorType == GeneratorEnum.SampleID ? string.Format("Generator SampleID {0}", (object) this.sampleHeader) : string.Format("Generator {0} {1}", (object) this.generatorType, (object) this.rawAmount);
    }
  }
}
