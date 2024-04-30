// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.Pcm16BitToSampleProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class Pcm16BitToSampleProvider : SampleProviderConverterBase
  {
    public Pcm16BitToSampleProvider(IWaveProvider source)
      : base(source)
    {
    }

    public override int Read(float[] buffer, int offset, int count)
    {
      int num1 = count * 2;
      this.EnsureSourceBuffer(num1);
      int num2 = this.source.Read(this.sourceBuffer, 0, num1);
      int num3 = offset;
      for (int startIndex = 0; startIndex < num2; startIndex += 2)
        buffer[num3++] = (float) BitConverter.ToInt16(this.sourceBuffer, startIndex) / 32768f;
      return num2 / 2;
    }
  }
}
