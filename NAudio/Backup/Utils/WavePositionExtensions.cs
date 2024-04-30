// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.WavePositionExtensions
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave;
using System;

#nullable disable
namespace NAudio.Utils
{
  public static class WavePositionExtensions
  {
    public static TimeSpan GetPositionTimeSpan(this IWavePosition @this)
    {
      return TimeSpan.FromMilliseconds((double) (@this.GetPosition() / (long) (@this.OutputWaveFormat.Channels * @this.OutputWaveFormat.BitsPerSample / 8)) * 1000.0 / (double) @this.OutputWaveFormat.SampleRate);
    }
  }
}
