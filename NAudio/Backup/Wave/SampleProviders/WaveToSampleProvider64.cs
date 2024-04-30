// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.WaveToSampleProvider64
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class WaveToSampleProvider64 : SampleProviderConverterBase
  {
    public WaveToSampleProvider64(IWaveProvider source)
      : base(source)
    {
      if (source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
        throw new ArgumentException("Must be already floating point");
    }

    public override int Read(float[] buffer, int offset, int count)
    {
      int num1 = count * 8;
      this.EnsureSourceBuffer(num1);
      int num2 = this.source.Read(this.sourceBuffer, 0, num1);
      int num3 = num2 / 8;
      int num4 = offset;
      for (int startIndex = 0; startIndex < num2; startIndex += 8)
      {
        long int64 = BitConverter.ToInt64(this.sourceBuffer, startIndex);
        buffer[num4++] = (float) BitConverter.Int64BitsToDouble(int64);
      }
      return num3;
    }
  }
}
