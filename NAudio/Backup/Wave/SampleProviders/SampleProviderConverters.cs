// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.SampleProviderConverters
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  internal static class SampleProviderConverters
  {
    public static ISampleProvider ConvertWaveProviderIntoSampleProvider(IWaveProvider waveProvider)
    {
      if (waveProvider.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
      {
        if (waveProvider.WaveFormat.BitsPerSample == 8)
          return (ISampleProvider) new Pcm8BitToSampleProvider(waveProvider);
        if (waveProvider.WaveFormat.BitsPerSample == 16)
          return (ISampleProvider) new Pcm16BitToSampleProvider(waveProvider);
        if (waveProvider.WaveFormat.BitsPerSample == 24)
          return (ISampleProvider) new Pcm24BitToSampleProvider(waveProvider);
        if (waveProvider.WaveFormat.BitsPerSample == 32)
          return (ISampleProvider) new Pcm32BitToSampleProvider(waveProvider);
        throw new InvalidOperationException("Unsupported bit depth");
      }
      if (waveProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
        throw new ArgumentException("Unsupported source encoding");
      return waveProvider.WaveFormat.BitsPerSample == 64 ? (ISampleProvider) new WaveToSampleProvider64(waveProvider) : (ISampleProvider) new WaveToSampleProvider(waveProvider);
    }
  }
}
