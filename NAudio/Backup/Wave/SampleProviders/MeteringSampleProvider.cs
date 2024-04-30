// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.MeteringSampleProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class MeteringSampleProvider : ISampleProvider
  {
    private readonly ISampleProvider source;
    private readonly float[] maxSamples;
    private int sampleCount;
    private readonly int channels;
    private readonly StreamVolumeEventArgs args;

    public int SamplesPerNotification { get; set; }

    public event EventHandler<StreamVolumeEventArgs> StreamVolume;

    public MeteringSampleProvider(ISampleProvider source)
      : this(source, source.WaveFormat.SampleRate / 10)
    {
    }

    public MeteringSampleProvider(ISampleProvider source, int samplesPerNotification)
    {
      this.source = source;
      this.channels = source.WaveFormat.Channels;
      this.maxSamples = new float[this.channels];
      this.SamplesPerNotification = samplesPerNotification;
      this.args = new StreamVolumeEventArgs()
      {
        MaxSampleValues = this.maxSamples
      };
    }

    public WaveFormat WaveFormat => this.source.WaveFormat;

    public int Read(float[] buffer, int offset, int count)
    {
      int num = this.source.Read(buffer, offset, count);
      if (this.StreamVolume != null)
      {
        for (int index1 = 0; index1 < num; index1 += this.channels)
        {
          for (int index2 = 0; index2 < this.channels; ++index2)
          {
            float val2 = Math.Abs(buffer[offset + index1 + index2]);
            this.maxSamples[index2] = Math.Max(this.maxSamples[index2], val2);
          }
          ++this.sampleCount;
          if (this.sampleCount >= this.SamplesPerNotification)
          {
            this.StreamVolume((object) this, this.args);
            this.sampleCount = 0;
            Array.Clear((Array) this.maxSamples, 0, this.channels);
          }
        }
      }
      return num;
    }
  }
}
