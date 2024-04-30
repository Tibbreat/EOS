// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.Modulator
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.SoundFont
{
  public class Modulator
  {
    private ModulatorType sourceModulationData;
    private GeneratorEnum destinationGenerator;
    private short amount;
    private ModulatorType sourceModulationAmount;
    private TransformEnum sourceTransform;

    public ModulatorType SourceModulationData
    {
      get => this.sourceModulationData;
      set => this.sourceModulationData = value;
    }

    public GeneratorEnum DestinationGenerator
    {
      get => this.destinationGenerator;
      set => this.destinationGenerator = value;
    }

    public short Amount
    {
      get => this.amount;
      set => this.amount = value;
    }

    public ModulatorType SourceModulationAmount
    {
      get => this.sourceModulationAmount;
      set => this.sourceModulationAmount = value;
    }

    public TransformEnum SourceTransform
    {
      get => this.sourceTransform;
      set => this.sourceTransform = value;
    }

    public override string ToString()
    {
      return string.Format("Modulator {0} {1} {2} {3} {4}", (object) this.sourceModulationData, (object) this.destinationGenerator, (object) this.amount, (object) this.sourceModulationAmount, (object) this.sourceTransform);
    }
  }
}
