// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.TrueSpeechWaveFormat
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public class TrueSpeechWaveFormat : WaveFormat
  {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    private short[] unknown;

    public TrueSpeechWaveFormat()
    {
      this.waveFormatTag = WaveFormatEncoding.DspGroupTrueSpeech;
      this.channels = (short) 1;
      this.averageBytesPerSecond = 1067;
      this.bitsPerSample = (short) 1;
      this.blockAlign = (short) 32;
      this.sampleRate = 8000;
      this.extraSize = (short) 32;
      this.unknown = new short[16];
      this.unknown[0] = (short) 1;
      this.unknown[1] = (short) 240;
    }

    public override void Serialize(BinaryWriter writer)
    {
      base.Serialize(writer);
      foreach (short num in this.unknown)
        writer.Write(num);
    }
  }
}
