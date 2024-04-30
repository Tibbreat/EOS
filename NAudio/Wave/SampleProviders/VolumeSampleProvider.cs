// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.VolumeSampleProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class VolumeSampleProvider : ISampleProvider
  {
    private readonly ISampleProvider source;
    private float volume;

    public VolumeSampleProvider(ISampleProvider source)
    {
      this.source = source;
      this.volume = 1f;
    }

    public WaveFormat WaveFormat => this.source.WaveFormat;

    public int Read(float[] buffer, int offset, int sampleCount)
    {
      int num = this.source.Read(buffer, offset, sampleCount);
      if ((double) this.volume != 1.0)
      {
        for (int index = 0; index < sampleCount; ++index)
          buffer[offset + index] *= this.volume;
      }
      return num;
    }

    public float Volume
    {
      get => this.volume;
      set => this.volume = value;
    }
  }
}
