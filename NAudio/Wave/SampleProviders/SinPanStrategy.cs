// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.SinPanStrategy
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class SinPanStrategy : IPanStrategy
  {
    private const float HalfPi = 1.57079637f;

    public StereoSamplePair GetMultipliers(float pan)
    {
      float num1 = (float) ((-(double) pan + 1.0) / 2.0);
      float num2 = (float) Math.Sin((double) num1 * 1.5707963705062866);
      float num3 = (float) Math.Cos((double) num1 * 1.5707963705062866);
      return new StereoSamplePair()
      {
        Left = num2,
        Right = num3
      };
    }
  }
}
