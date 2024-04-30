// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.SampleProviders.SampleProviderConverterBase
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;

#nullable disable
namespace NAudio.Wave.SampleProviders
{
  public abstract class SampleProviderConverterBase : ISampleProvider
  {
    protected IWaveProvider source;
    private WaveFormat waveFormat;
    protected byte[] sourceBuffer;

    public SampleProviderConverterBase(IWaveProvider source)
    {
      this.source = source;
      this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, source.WaveFormat.Channels);
    }

    public WaveFormat WaveFormat => this.waveFormat;

    public abstract int Read(float[] buffer, int offset, int count);

    protected void EnsureSourceBuffer(int sourceBytesRequired)
    {
      this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, sourceBytesRequired);
    }
  }
}
