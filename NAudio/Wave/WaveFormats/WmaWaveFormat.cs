// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveFormats.WmaWaveFormat
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.WaveFormats
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  internal class WmaWaveFormat : WaveFormat
  {
    private short wValidBitsPerSample;
    private int dwChannelMask;
    private int dwReserved1;
    private int dwReserved2;
    private short wEncodeOptions;
    private short wReserved3;

    public WmaWaveFormat(int sampleRate, int bitsPerSample, int channels)
      : base(sampleRate, bitsPerSample, channels)
    {
      this.wValidBitsPerSample = (short) bitsPerSample;
      switch (channels)
      {
        case 1:
          this.dwChannelMask = 1;
          break;
        case 2:
          this.dwChannelMask = 3;
          break;
      }
      this.waveFormatTag = WaveFormatEncoding.WindowsMediaAudio;
    }
  }
}
