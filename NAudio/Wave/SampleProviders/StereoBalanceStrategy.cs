﻿// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.StereoBalanceStrategy
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class StereoBalanceStrategy : IPanStrategy
  {
    public StereoSamplePair GetMultipliers(float pan)
    {
      float num1 = (double) pan <= 0.0 ? 1f : (float) ((1.0 - (double) pan) / 2.0);
      float num2 = (double) pan >= 0.0 ? 1f : (float) (((double) pan + 1.0) / 2.0);
      return new StereoSamplePair()
      {
        Left = num1,
        Right = num2
      };
    }
  }
}
