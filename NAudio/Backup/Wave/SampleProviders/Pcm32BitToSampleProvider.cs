// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.Pcm32BitToSampleProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class Pcm32BitToSampleProvider : SampleProviderConverterBase
  {
    public Pcm32BitToSampleProvider(IWaveProvider source)
      : base(source)
    {
    }

    public override int Read(float[] buffer, int offset, int count)
    {
      int num1 = count * 4;
      this.EnsureSourceBuffer(num1);
      int num2 = this.source.Read(this.sourceBuffer, 0, num1);
      int num3 = offset;
      for (int index = 0; index < num2; index += 4)
        buffer[num3++] = (float) ((int) (sbyte) this.sourceBuffer[index + 3] << 24 | (int) this.sourceBuffer[index + 2] << 16 | (int) this.sourceBuffer[index + 1] << 8 | (int) this.sourceBuffer[index]) / (float) int.MaxValue;
      return num2 / 4;
    }
  }
}
