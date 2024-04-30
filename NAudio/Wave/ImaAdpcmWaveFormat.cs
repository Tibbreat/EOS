// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.ImaAdpcmWaveFormat
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public class ImaAdpcmWaveFormat : WaveFormat
  {
    private short samplesPerBlock;

    private ImaAdpcmWaveFormat()
    {
    }

    public ImaAdpcmWaveFormat(int sampleRate, int channels, int bitsPerSample)
    {
      this.waveFormatTag = WaveFormatEncoding.DviAdpcm;
      this.sampleRate = sampleRate;
      this.channels = (short) channels;
      this.bitsPerSample = (short) bitsPerSample;
      this.extraSize = (short) 2;
      this.blockAlign = (short) 0;
      this.averageBytesPerSecond = 0;
      this.samplesPerBlock = (short) 0;
    }
  }
}
