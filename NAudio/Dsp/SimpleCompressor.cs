// Decompiled with JetBrains decompiler
// Type: NAudio.Dsp.SimpleCompressor
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System;

#nullable disable
namespace NAudio.Dsp
{
  internal class SimpleCompressor : AttRelEnvelope
  {
    private double envdB;

    public SimpleCompressor(double attackTime, double releaseTime, double sampleRate)
      : base(attackTime, releaseTime, sampleRate)
    {
      this.Threshold = 0.0;
      this.Ratio = 1.0;
      this.MakeUpGain = 0.0;
      this.envdB = 1E-25;
    }

    public SimpleCompressor()
      : base(10.0, 10.0, 44100.0)
    {
      this.Threshold = 0.0;
      this.Ratio = 1.0;
      this.MakeUpGain = 0.0;
      this.envdB = 1E-25;
    }

    public double MakeUpGain { get; set; }

    public double Threshold { get; set; }

    public double Ratio { get; set; }

    public void InitRuntime() => this.envdB = 1E-25;

    public void Process(ref double in1, ref double in2)
    {
      double num1 = Decibels.LinearToDecibels(Math.Max(Math.Abs(in1), Math.Abs(in2)) + 1E-25) - this.Threshold;
      if (num1 < 0.0)
        num1 = 0.0;
      this.Run(num1 + 1E-25, ref this.envdB);
      double num2 = Decibels.DecibelsToLinear((this.envdB - 1E-25) * (this.Ratio - 1.0)) * Decibels.DecibelsToLinear(this.MakeUpGain);
      in1 *= num2;
      in2 *= num2;
    }
  }
}
