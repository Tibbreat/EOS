// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.Pcm8BitToSampleProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class Pcm8BitToSampleProvider : SampleProviderConverterBase
  {
    public Pcm8BitToSampleProvider(IWaveProvider source)
      : base(source)
    {
    }

    public override int Read(float[] buffer, int offset, int count)
    {
      int num1 = count;
      this.EnsureSourceBuffer(num1);
      int num2 = this.source.Read(this.sourceBuffer, 0, num1);
      int num3 = offset;
      for (int index = 0; index < num2; ++index)
        buffer[num3++] = (float) ((double) this.sourceBuffer[index] / 128.0 - 1.0);
      return num2;
    }
  }
}
