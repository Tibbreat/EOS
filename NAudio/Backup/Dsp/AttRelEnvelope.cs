// Decompiled with JetBrains decompiler
// Type: NAudio.Dsp.AttRelEnvelope
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Dsp
{
  internal class AttRelEnvelope
  {
    protected const double DC_OFFSET = 1E-25;
    private readonly EnvelopeDetector attack;
    private readonly EnvelopeDetector release;

    public AttRelEnvelope(double attackMilliseconds, double releaseMilliseconds, double sampleRate)
    {
      this.attack = new EnvelopeDetector(attackMilliseconds, sampleRate);
      this.release = new EnvelopeDetector(releaseMilliseconds, sampleRate);
    }

    public double Attack
    {
      get => this.attack.TimeConstant;
      set => this.attack.TimeConstant = value;
    }

    public double Release
    {
      get => this.release.TimeConstant;
      set => this.release.TimeConstant = value;
    }

    public double SampleRate
    {
      get => this.attack.SampleRate;
      set => this.attack.SampleRate = this.release.SampleRate = value;
    }

    public void Run(double inValue, ref double state)
    {
      if (inValue > state)
        this.attack.Run(inValue, ref state);
      else
        this.release.Run(inValue, ref state);
    }
  }
}
