// Decompiled with JetBrains decompiler
// Type: NAudio.Dsp.EnvelopeDetector
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Dsp
{
  internal class EnvelopeDetector
  {
    private double sampleRate;
    private double ms;
    private double coeff;

    public EnvelopeDetector()
      : this(1.0, 44100.0)
    {
    }

    public EnvelopeDetector(double ms, double sampleRate)
    {
      this.sampleRate = sampleRate;
      this.ms = ms;
      this.SetCoef();
    }

    public double TimeConstant
    {
      get => this.ms;
      set
      {
        this.ms = value;
        this.SetCoef();
      }
    }

    public double SampleRate
    {
      get => this.sampleRate;
      set
      {
        this.sampleRate = value;
        this.SetCoef();
      }
    }

    public void Run(double inValue, ref double state)
    {
      state = inValue + this.coeff * (state - inValue);
    }

    private void SetCoef() => this.coeff = Math.Exp(-1.0 / (0.001 * this.ms * this.sampleRate));
  }
}
