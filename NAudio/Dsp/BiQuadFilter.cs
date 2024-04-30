// Decompiled with JetBrains decompiler
// Type: NAudio.Dsp.BiQuadFilter
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Dsp
{
  public class BiQuadFilter
  {
    private double a0;
    private double a1;
    private double a2;
    private double a3;
    private double a4;
    private float x1;
    private float x2;
    private float y1;
    private float y2;

    public float Transform(float inSample)
    {
      double num = this.a0 * (double) inSample + this.a1 * (double) this.x1 + this.a2 * (double) this.x2 - this.a3 * (double) this.y1 - this.a4 * (double) this.y2;
      this.x2 = this.x1;
      this.x1 = inSample;
      this.y2 = this.y1;
      this.y1 = (float) num;
      return this.y1;
    }

    private void SetCoefficients(
      double aa0,
      double aa1,
      double aa2,
      double b0,
      double b1,
      double b2)
    {
      this.a0 = b0 / aa0;
      this.a1 = b1 / aa0;
      this.a2 = b2 / aa0;
      this.a3 = aa1 / aa0;
      this.a4 = aa2 / aa0;
    }

    public void SetLowPassFilter(float sampleRate, float cutoffFrequency, float q)
    {
      double num1 = 2.0 * Math.PI * (double) cutoffFrequency / (double) sampleRate;
      double num2 = Math.Cos(num1);
      double num3 = Math.Sin(num1) / (2.0 * (double) q);
      double b0 = (1.0 - num2) / 2.0;
      double b1 = 1.0 - num2;
      double b2 = (1.0 - num2) / 2.0;
      this.SetCoefficients(1.0 + num3, -2.0 * num2, 1.0 - num3, b0, b1, b2);
    }

    public void SetPeakingEq(float sampleRate, float centreFrequency, float q, float dbGain)
    {
      double num1 = 2.0 * Math.PI * (double) centreFrequency / (double) sampleRate;
      double num2 = Math.Cos(num1);
      double num3 = Math.Sin(num1) / (2.0 * (double) q);
      double num4 = Math.Pow(10.0, (double) dbGain / 40.0);
      double b0 = 1.0 + num3 * num4;
      double b1 = -2.0 * num2;
      double b2 = 1.0 - num3 * num4;
      this.SetCoefficients(1.0 + num3 / num4, -2.0 * num2, 1.0 - num3 / num4, b0, b1, b2);
    }

    public void SetHighPassFilter(float sampleRate, float cutoffFrequency, float q)
    {
      double num1 = 2.0 * Math.PI * (double) cutoffFrequency / (double) sampleRate;
      double num2 = Math.Cos(num1);
      double num3 = Math.Sin(num1) / (2.0 * (double) q);
      double b0 = (1.0 + num2) / 2.0;
      double b1 = -(1.0 + num2);
      double b2 = (1.0 + num2) / 2.0;
      this.SetCoefficients(1.0 + num3, -2.0 * num2, 1.0 - num3, b0, b1, b2);
    }

    public static BiQuadFilter LowPassFilter(float sampleRate, float cutoffFrequency, float q)
    {
      BiQuadFilter biQuadFilter = new BiQuadFilter();
      biQuadFilter.SetLowPassFilter(sampleRate, cutoffFrequency, q);
      return biQuadFilter;
    }

    public static BiQuadFilter HighPassFilter(float sampleRate, float cutoffFrequency, float q)
    {
      BiQuadFilter biQuadFilter = new BiQuadFilter();
      biQuadFilter.SetHighPassFilter(sampleRate, cutoffFrequency, q);
      return biQuadFilter;
    }

    public static BiQuadFilter BandPassFilterConstantSkirtGain(
      float sampleRate,
      float centreFrequency,
      float q)
    {
      double num1 = 2.0 * Math.PI * (double) centreFrequency / (double) sampleRate;
      double num2 = Math.Cos(num1);
      double num3 = Math.Sin(num1);
      double num4 = num3 / (2.0 * (double) q);
      double b0 = num3 / 2.0;
      int b1 = 0;
      double b2 = -num3 / 2.0;
      return new BiQuadFilter(1.0 + num4, -2.0 * num2, 1.0 - num4, b0, (double) b1, b2);
    }

    public static BiQuadFilter BandPassFilterConstantPeakGain(
      float sampleRate,
      float centreFrequency,
      float q)
    {
      double num1 = 2.0 * Math.PI * (double) centreFrequency / (double) sampleRate;
      double num2 = Math.Cos(num1);
      double num3 = Math.Sin(num1) / (2.0 * (double) q);
      double b0 = num3;
      int b1 = 0;
      double b2 = -num3;
      return new BiQuadFilter(1.0 + num3, -2.0 * num2, 1.0 - num3, b0, (double) b1, b2);
    }

    public static BiQuadFilter NotchFilter(float sampleRate, float centreFrequency, float q)
    {
      double num1 = 2.0 * Math.PI * (double) centreFrequency / (double) sampleRate;
      double num2 = Math.Cos(num1);
      double num3 = Math.Sin(num1) / (2.0 * (double) q);
      int b0 = 1;
      double b1 = -2.0 * num2;
      int b2 = 1;
      return new BiQuadFilter(1.0 + num3, -2.0 * num2, 1.0 - num3, (double) b0, b1, (double) b2);
    }

    public static BiQuadFilter AllPassFilter(float sampleRate, float centreFrequency, float q)
    {
      double num1 = 2.0 * Math.PI * (double) centreFrequency / (double) sampleRate;
      double num2 = Math.Cos(num1);
      double num3 = Math.Sin(num1) / (2.0 * (double) q);
      double b0 = 1.0 - num3;
      double b1 = -2.0 * num2;
      double b2 = 1.0 + num3;
      return new BiQuadFilter(1.0 + num3, -2.0 * num2, 1.0 - num3, b0, b1, b2);
    }

    public static BiQuadFilter PeakingEQ(
      float sampleRate,
      float centreFrequency,
      float q,
      float dbGain)
    {
      BiQuadFilter biQuadFilter = new BiQuadFilter();
      biQuadFilter.SetPeakingEq(sampleRate, centreFrequency, q, dbGain);
      return biQuadFilter;
    }

    public static BiQuadFilter LowShelf(
      float sampleRate,
      float cutoffFrequency,
      float shelfSlope,
      float dbGain)
    {
      double num1 = 2.0 * Math.PI * (double) cutoffFrequency / (double) sampleRate;
      double num2 = Math.Cos(num1);
      double num3 = Math.Sin(num1);
      double d = Math.Pow(10.0, (double) dbGain / 40.0);
      double num4 = num3 / 2.0 * Math.Sqrt((d + 1.0 / d) * (1.0 / (double) shelfSlope - 1.0) + 2.0);
      double num5 = 2.0 * Math.Sqrt(d) * num4;
      double b0 = d * (d + 1.0 - (d - 1.0) * num2 + num5);
      double b1 = 2.0 * d * (d - 1.0 - (d + 1.0) * num2);
      double b2 = d * (d + 1.0 - (d - 1.0) * num2 - num5);
      return new BiQuadFilter(d + 1.0 + (d - 1.0) * num2 + num5, -2.0 * (d - 1.0 + (d + 1.0) * num2), d + 1.0 + (d - 1.0) * num2 - num5, b0, b1, b2);
    }

    public static BiQuadFilter HighShelf(
      float sampleRate,
      float cutoffFrequency,
      float shelfSlope,
      float dbGain)
    {
      double num1 = 2.0 * Math.PI * (double) cutoffFrequency / (double) sampleRate;
      double num2 = Math.Cos(num1);
      double num3 = Math.Sin(num1);
      double d = Math.Pow(10.0, (double) dbGain / 40.0);
      double num4 = num3 / 2.0 * Math.Sqrt((d + 1.0 / d) * (1.0 / (double) shelfSlope - 1.0) + 2.0);
      double num5 = 2.0 * Math.Sqrt(d) * num4;
      double b0 = d * (d + 1.0 + (d - 1.0) * num2 + num5);
      double b1 = -2.0 * d * (d - 1.0 + (d + 1.0) * num2);
      double b2 = d * (d + 1.0 + (d - 1.0) * num2 - num5);
      return new BiQuadFilter(d + 1.0 - (d - 1.0) * num2 + num5, 2.0 * (d - 1.0 - (d + 1.0) * num2), d + 1.0 - (d - 1.0) * num2 - num5, b0, b1, b2);
    }

    private BiQuadFilter()
    {
      this.x1 = this.x2 = 0.0f;
      this.y1 = this.y2 = 0.0f;
    }

    private BiQuadFilter(double a0, double a1, double a2, double b0, double b1, double b2)
    {
      this.SetCoefficients(a0, a1, a2, b0, b1, b2);
      this.x1 = this.x2 = 0.0f;
      this.y1 = this.y2 = 0.0f;
    }
  }
}
