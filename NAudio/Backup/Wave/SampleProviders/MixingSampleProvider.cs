// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.MixingSampleProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System;
using System.Collections.Generic;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class MixingSampleProvider : ISampleProvider
  {
    private const int maxInputs = 1024;
    private List<ISampleProvider> sources;
    private WaveFormat waveFormat;
    private float[] sourceBuffer;

    public MixingSampleProvider(WaveFormat waveFormat)
    {
      if (waveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
        throw new ArgumentException("Mixer wave format must be IEEE float");
      this.sources = new List<ISampleProvider>();
      this.waveFormat = waveFormat;
    }

    public MixingSampleProvider(IEnumerable<ISampleProvider> sources)
    {
      this.sources = new List<ISampleProvider>();
      foreach (ISampleProvider source in sources)
        this.AddMixerInput(source);
      if (this.sources.Count == 0)
        throw new ArgumentException("Must provide at least one input in this constructor");
    }

    public bool ReadFully { get; set; }

    public void AddMixerInput(IWaveProvider mixerInput)
    {
      this.AddMixerInput(SampleProviderConverters.ConvertWaveProviderIntoSampleProvider(mixerInput));
    }

    public void AddMixerInput(ISampleProvider mixerInput)
    {
      lock (this.sources)
      {
        if (this.sources.Count >= 1024)
          throw new InvalidOperationException("Too many mixer inputs");
        this.sources.Add(mixerInput);
      }
      if (this.waveFormat == null)
        this.waveFormat = mixerInput.WaveFormat;
      else if (this.WaveFormat.SampleRate != mixerInput.WaveFormat.SampleRate || this.WaveFormat.Channels != mixerInput.WaveFormat.Channels)
        throw new ArgumentException("All mixer inputs must have the same WaveFormat");
    }

    public void RemoveMixerInput(ISampleProvider mixerInput)
    {
      lock (this.sources)
        this.sources.Remove(mixerInput);
    }

    public void RemoveAllMixerInputs()
    {
      lock (this.sources)
        this.sources.Clear();
    }

    public WaveFormat WaveFormat => this.waveFormat;

    public int Read(float[] buffer, int offset, int count)
    {
      int val2 = 0;
      this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, count);
      lock (this.sources)
      {
        for (int index1 = this.sources.Count - 1; index1 >= 0; --index1)
        {
          int val1 = this.sources[index1].Read(this.sourceBuffer, 0, count);
          int num = offset;
          for (int index2 = 0; index2 < val1; ++index2)
          {
            if (index2 >= val2)
              buffer[num++] = this.sourceBuffer[index2];
            else
              buffer[num++] += this.sourceBuffer[index2];
          }
          val2 = Math.Max(val1, val2);
          if (val1 == 0)
            this.sources.RemoveAt(index1);
        }
      }
      if (this.ReadFully && val2 < count)
      {
        int num = offset + val2;
        while (num < offset + count)
          buffer[num++] = 0.0f;
        val2 = count;
      }
      return val2;
    }
  }
}
