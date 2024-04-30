// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveProvider32
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave
{
  public abstract class WaveProvider32 : IWaveProvider, ISampleProvider
  {
    private WaveFormat waveFormat;

    public WaveProvider32()
      : this(44100, 1)
    {
    }

    public WaveProvider32(int sampleRate, int channels) => this.SetWaveFormat(sampleRate, channels);

    public void SetWaveFormat(int sampleRate, int channels)
    {
      this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      WaveBuffer waveBuffer = new WaveBuffer(buffer);
      int sampleCount = count / 4;
      return this.Read(waveBuffer.FloatBuffer, offset / 4, sampleCount) * 4;
    }

    public abstract int Read(float[] buffer, int offset, int sampleCount);

    public WaveFormat WaveFormat => this.waveFormat;
  }
}
