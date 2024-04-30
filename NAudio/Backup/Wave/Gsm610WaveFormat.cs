// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Gsm610WaveFormat
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public class Gsm610WaveFormat : WaveFormat
  {
    private short samplesPerBlock;

    public Gsm610WaveFormat()
    {
      this.waveFormatTag = WaveFormatEncoding.Gsm610;
      this.channels = (short) 1;
      this.averageBytesPerSecond = 1625;
      this.bitsPerSample = (short) 0;
      this.blockAlign = (short) 65;
      this.sampleRate = 8000;
      this.extraSize = (short) 2;
      this.samplesPerBlock = (short) 320;
    }

    public short SamplesPerBlock => this.samplesPerBlock;

    public override void Serialize(BinaryWriter writer)
    {
      base.Serialize(writer);
      writer.Write(this.samplesPerBlock);
    }
  }
}
