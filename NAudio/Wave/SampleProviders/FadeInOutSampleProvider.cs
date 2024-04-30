// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.FadeInOutSampleProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public class FadeInOutSampleProvider : ISampleProvider
  {
    private readonly object lockObject = new object();
    private readonly ISampleProvider source;
    private int fadeSamplePosition;
    private int fadeSampleCount;
    private FadeInOutSampleProvider.FadeState fadeState;

    public FadeInOutSampleProvider(ISampleProvider source, bool initiallySilent = false)
    {
      this.source = source;
      this.fadeState = initiallySilent ? FadeInOutSampleProvider.FadeState.Silence : FadeInOutSampleProvider.FadeState.FullVolume;
    }

    public void BeginFadeIn(double fadeDurationInMilliseconds)
    {
      lock (this.lockObject)
      {
        this.fadeSamplePosition = 0;
        this.fadeSampleCount = (int) (fadeDurationInMilliseconds * (double) this.source.WaveFormat.SampleRate / 1000.0);
        this.fadeState = FadeInOutSampleProvider.FadeState.FadingIn;
      }
    }

    public void BeginFadeOut(double fadeDurationInMilliseconds)
    {
      lock (this.lockObject)
      {
        this.fadeSamplePosition = 0;
        this.fadeSampleCount = (int) (fadeDurationInMilliseconds * (double) this.source.WaveFormat.SampleRate / 1000.0);
        this.fadeState = FadeInOutSampleProvider.FadeState.FadingOut;
      }
    }

    public int Read(float[] buffer, int offset, int count)
    {
      int sourceSamplesRead = this.source.Read(buffer, offset, count);
      lock (this.lockObject)
      {
        if (this.fadeState == FadeInOutSampleProvider.FadeState.FadingIn)
          this.FadeIn(buffer, offset, sourceSamplesRead);
        else if (this.fadeState == FadeInOutSampleProvider.FadeState.FadingOut)
          this.FadeOut(buffer, offset, sourceSamplesRead);
        else if (this.fadeState == FadeInOutSampleProvider.FadeState.Silence)
          FadeInOutSampleProvider.ClearBuffer(buffer, offset, count);
      }
      return sourceSamplesRead;
    }

    private static void ClearBuffer(float[] buffer, int offset, int count)
    {
      for (int index = 0; index < count; ++index)
        buffer[index + offset] = 0.0f;
    }

    private void FadeOut(float[] buffer, int offset, int sourceSamplesRead)
    {
      int num1 = 0;
      while (num1 < sourceSamplesRead)
      {
        float num2 = (float) (1.0 - (double) this.fadeSamplePosition / (double) this.fadeSampleCount);
        for (int index = 0; index < this.source.WaveFormat.Channels; ++index)
          buffer[offset + num1++] *= num2;
        ++this.fadeSamplePosition;
        if (this.fadeSamplePosition > this.fadeSampleCount)
        {
          this.fadeState = FadeInOutSampleProvider.FadeState.Silence;
          FadeInOutSampleProvider.ClearBuffer(buffer, num1 + offset, sourceSamplesRead - num1);
          break;
        }
      }
    }

    private void FadeIn(float[] buffer, int offset, int sourceSamplesRead)
    {
      int num1 = 0;
      while (num1 < sourceSamplesRead)
      {
        float num2 = (float) this.fadeSamplePosition / (float) this.fadeSampleCount;
        for (int index = 0; index < this.source.WaveFormat.Channels; ++index)
          buffer[offset + num1++] *= num2;
        ++this.fadeSamplePosition;
        if (this.fadeSamplePosition > this.fadeSampleCount)
        {
          this.fadeState = FadeInOutSampleProvider.FadeState.FullVolume;
          break;
        }
      }
    }

    public WaveFormat WaveFormat => this.source.WaveFormat;

    private enum FadeState
    {
      Silence,
      FadingIn,
      FullVolume,
      FadingOut,
    }
  }
}
