// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveExtensionMethods
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave.SampleProviders;

#nullable disable
namespace NAudio.Wave
{
  public static class WaveExtensionMethods
  {
    public static ISampleProvider ToSampleProvider(this IWaveProvider waveProvider)
    {
      return SampleProviderConverters.ConvertWaveProviderIntoSampleProvider(waveProvider);
    }

    public static void Init(
      this IWavePlayer wavePlayer,
      ISampleProvider sampleProvider,
      bool convertTo16Bit = false)
    {
      IWaveProvider waveProvider = convertTo16Bit ? (IWaveProvider) new SampleToWaveProvider16(sampleProvider) : (IWaveProvider) new SampleToWaveProvider(sampleProvider);
      wavePlayer.Init(waveProvider);
    }
  }
}
