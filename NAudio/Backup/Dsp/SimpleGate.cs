// Decompiled with JetBrains decompiler
// Type: NAudio.Dsp.SimpleGate
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System;

#nullable disable
namespace NAudio.Dsp
{
  internal class SimpleGate : AttRelEnvelope
  {
    private double threshdB;
    private double thresh;
    private double env;

    public SimpleGate()
      : base(10.0, 10.0, 44100.0)
    {
      this.threshdB = 0.0;
      this.thresh = 1.0;
      this.env = 1E-25;
    }

    public void Process(ref double in1, ref double in2)
    {
      this.Run((Math.Max(Math.Abs(in1), Math.Abs(in2)) > this.thresh ? 1.0 : 0.0) + 1E-25, ref this.env);
      double num = this.env - 1E-25;
      in1 *= num;
      in2 *= num;
    }

    public double Threshold
    {
      get => this.threshdB;
      set
      {
        this.threshdB = value;
        this.thresh = Decibels.DecibelsToLinear(value);
      }
    }
  }
}
