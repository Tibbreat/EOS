// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveFormatExtensible
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Dmo;
using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public class WaveFormatExtensible : WaveFormat
  {
    private short wValidBitsPerSample;
    private int dwChannelMask;
    private Guid subFormat;

    private WaveFormatExtensible()
    {
    }

    public WaveFormatExtensible(int rate, int bits, int channels)
      : base(rate, bits, channels)
    {
      this.waveFormatTag = WaveFormatEncoding.Extensible;
      this.extraSize = (short) 22;
      this.wValidBitsPerSample = (short) bits;
      for (int index = 0; index < channels; ++index)
        this.dwChannelMask |= 1 << index;
      if (bits == 32)
        this.subFormat = AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT;
      else
        this.subFormat = AudioMediaSubtypes.MEDIASUBTYPE_PCM;
    }

    public WaveFormat ToStandardWaveFormat()
    {
      if (this.subFormat == AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT && this.bitsPerSample == (short) 32)
        return WaveFormat.CreateIeeeFloatWaveFormat(this.sampleRate, (int) this.channels);
      if (this.subFormat == AudioMediaSubtypes.MEDIASUBTYPE_PCM)
        return new WaveFormat(this.sampleRate, (int) this.bitsPerSample, (int) this.channels);
      throw new InvalidOperationException("Not a recognised PCM or IEEE float format");
    }

    public Guid SubFormat => this.subFormat;

    public override void Serialize(BinaryWriter writer)
    {
      base.Serialize(writer);
      writer.Write(this.wValidBitsPerSample);
      writer.Write(this.dwChannelMask);
      byte[] byteArray = this.subFormat.ToByteArray();
      writer.Write(byteArray, 0, byteArray.Length);
    }

    public override string ToString()
    {
      return string.Format("{0} wBitsPerSample:{1} dwChannelMask:{2} subFormat:{3} extraSize:{4}", (object) base.ToString(), (object) this.wValidBitsPerSample, (object) this.dwChannelMask, (object) this.subFormat, (object) this.extraSize);
    }
  }
}
