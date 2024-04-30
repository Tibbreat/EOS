// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.MonoToStereoSampleProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class MonoToStereoSampleProvider : ISampleProvider
  {
    private readonly ISampleProvider source;
    private readonly WaveFormat waveFormat;
    private float[] sourceBuffer;

    public MonoToStereoSampleProvider(ISampleProvider source)
    {
      if (source.WaveFormat.Channels != 1)
        throw new ArgumentException("Source must be mono");
      this.source = source;
      this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, 2);
    }

    public WaveFormat WaveFormat => this.waveFormat;

    public int Read(float[] buffer, int offset, int count)
    {
      int count1 = count / 2;
      int num1 = offset;
      this.EnsureSourceBuffer(count1);
      int num2 = this.source.Read(this.sourceBuffer, 0, count1);
      for (int index1 = 0; index1 < num2; ++index1)
      {
        float[] numArray1 = buffer;
        int index2 = num1;
        int num3 = index2 + 1;
        double num4 = (double) this.sourceBuffer[index1];
        numArray1[index2] = (float) num4;
        float[] numArray2 = buffer;
        int index3 = num3;
        num1 = index3 + 1;
        double num5 = (double) this.sourceBuffer[index1];
        numArray2[index3] = (float) num5;
      }
      return num2 * 2;
    }

    private void EnsureSourceBuffer(int count)
    {
      if (this.sourceBuffer != null && this.sourceBuffer.Length >= count)
        return;
      this.sourceBuffer = new float[count];
    }
  }
}
