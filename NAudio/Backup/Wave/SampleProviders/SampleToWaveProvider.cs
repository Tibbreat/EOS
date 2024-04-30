// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.SampleToWaveProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class SampleToWaveProvider : IWaveProvider
  {
    private ISampleProvider source;

    public SampleToWaveProvider(ISampleProvider source)
    {
      if (source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
        throw new ArgumentException("Must be already floating point");
      this.source = source;
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      int count1 = count / 4;
      return this.source.Read(new WaveBuffer(buffer).FloatBuffer, offset / 4, count1) * 4;
    }

    public WaveFormat WaveFormat => this.source.WaveFormat;
  }
}
