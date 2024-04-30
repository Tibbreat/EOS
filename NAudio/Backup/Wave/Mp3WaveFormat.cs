// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Mp3WaveFormat
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public class Mp3WaveFormat : WaveFormat
  {
    private const short Mp3WaveFormatExtraBytes = 12;
    public Mp3WaveFormatId id;
    public Mp3WaveFormatFlags flags;
    public ushort blockSize;
    public ushort framesPerBlock;
    public ushort codecDelay;

    public Mp3WaveFormat(int sampleRate, int channels, int blockSize, int bitRate)
    {
      this.waveFormatTag = WaveFormatEncoding.MpegLayer3;
      this.channels = (short) channels;
      this.averageBytesPerSecond = bitRate / 8;
      this.bitsPerSample = (short) 0;
      this.blockAlign = (short) 1;
      this.sampleRate = sampleRate;
      this.extraSize = (short) 12;
      this.id = Mp3WaveFormatId.Mpeg;
      this.flags = Mp3WaveFormatFlags.PaddingIso;
      this.blockSize = (ushort) blockSize;
      this.framesPerBlock = (ushort) 1;
      this.codecDelay = (ushort) 0;
    }
  }
}
