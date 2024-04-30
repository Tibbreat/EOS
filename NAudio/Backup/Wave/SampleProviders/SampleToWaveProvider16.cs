// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.SampleToWaveProvider16
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class SampleToWaveProvider16 : IWaveProvider
  {
    private readonly ISampleProvider sourceProvider;
    private readonly WaveFormat waveFormat;
    private volatile float volume;
    private float[] sourceBuffer;

    public SampleToWaveProvider16(ISampleProvider sourceProvider)
    {
      if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
        throw new ArgumentException("Input source provider must be IEEE float", nameof (sourceProvider));
      if (sourceProvider.WaveFormat.BitsPerSample != 32)
        throw new ArgumentException("Input source provider must be 32 bit", nameof (sourceProvider));
      this.waveFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 16, sourceProvider.WaveFormat.Channels);
      this.sourceProvider = sourceProvider;
      this.volume = 1f;
    }

    public int Read(byte[] destBuffer, int offset, int numBytes)
    {
      int num1 = numBytes / 2;
      this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num1);
      int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num1);
      WaveBuffer waveBuffer = new WaveBuffer(destBuffer);
      int num3 = offset / 2;
      for (int index = 0; index < num2; ++index)
      {
        float num4 = this.sourceBuffer[index] * this.volume;
        if ((double) num4 > 1.0)
          num4 = 1f;
        if ((double) num4 < -1.0)
          num4 = -1f;
        waveBuffer.ShortBuffer[num3++] = (short) ((double) num4 * (double) short.MaxValue);
      }
      return num2 * 2;
    }

    public WaveFormat WaveFormat => this.waveFormat;

    public float Volume
    {
      get => this.volume;
      set => this.volume = value;
    }
  }
}
