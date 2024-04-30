// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.Decibels
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Utils
{
  public class Decibels
  {
    private const double LOG_2_DB = 8.6858896380650368;
    private const double DB_2_LOG = 0.11512925464970228;

    public static double LinearToDecibels(double lin) => Math.Log(lin) * 8.6858896380650368;

    public static double DecibelsToLinear(double dB) => Math.Exp(dB * 0.11512925464970228);
  }
}
