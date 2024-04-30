// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveProvider16
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave
{
  public abstract class WaveProvider16 : IWaveProvider
  {
    private WaveFormat waveFormat;

    public WaveProvider16()
      : this(44100, 1)
    {
    }

    public WaveProvider16(int sampleRate, int channels) => this.SetWaveFormat(sampleRate, channels);

    public void SetWaveFormat(int sampleRate, int channels)
    {
      this.waveFormat = new WaveFormat(sampleRate, 16, channels);
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      WaveBuffer waveBuffer = new WaveBuffer(buffer);
      int sampleCount = count / 2;
      return this.Read(waveBuffer.ShortBuffer, offset / 2, sampleCount) * 2;
    }

    public abstract int Read(short[] buffer, int offset, int sampleCount);

    public WaveFormat WaveFormat => this.waveFormat;
  }
}
