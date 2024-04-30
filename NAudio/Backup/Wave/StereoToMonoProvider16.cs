// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.StereoToMonoProvider16
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System;

#nullable disable
namespace NAudio.Wave
{
  public class StereoToMonoProvider16 : IWaveProvider
  {
    private IWaveProvider sourceProvider;
    private WaveFormat outputFormat;
    private byte[] sourceBuffer;

    public StereoToMonoProvider16(IWaveProvider sourceProvider)
    {
      if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
        throw new ArgumentException("Source must be PCM");
      if (sourceProvider.WaveFormat.Channels != 2)
        throw new ArgumentException("Source must be stereo");
      if (sourceProvider.WaveFormat.BitsPerSample != 16)
        throw new ArgumentException("Source must be 16 bit");
      this.sourceProvider = sourceProvider;
      this.outputFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 1);
    }

    public float LeftVolume { get; set; }

    public float RightVolume { get; set; }

    public WaveFormat WaveFormat => this.outputFormat;

    public int Read(byte[] buffer, int offset, int count)
    {
      int num1 = count * 2;
      this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num1);
      WaveBuffer waveBuffer1 = new WaveBuffer(this.sourceBuffer);
      WaveBuffer waveBuffer2 = new WaveBuffer(buffer);
      int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num1);
      int num3 = num2 / 2;
      int num4 = offset / 2;
      for (int index = 0; index < num3; index += 2)
      {
        float num5 = (float) ((double) waveBuffer1.ShortBuffer[index] * (double) this.LeftVolume + (double) waveBuffer1.ShortBuffer[index + 1] * (double) this.RightVolume);
        if ((double) num5 > (double) short.MaxValue)
          num5 = (float) short.MaxValue;
        if ((double) num5 < (double) short.MinValue)
          num5 = (float) short.MinValue;
        waveBuffer2.ShortBuffer[num4++] = (short) num5;
      }
      return num2 / 2;
    }
  }
}
