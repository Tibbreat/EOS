// Decompiled with JetBrains decompiler
// Type: NAudio.Codecs.G722CodecState
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Codecs
{
  public class G722CodecState
  {
    public bool ItuTestMode { get; set; }

    public bool Packed { get; private set; }

    public bool EncodeFrom8000Hz { get; private set; }

    public int BitsPerSample { get; private set; }

    public int[] QmfSignalHistory { get; private set; }

    public NAudio.Codecs.Band[] Band { get; private set; }

    public uint InBuffer { get; internal set; }

    public int InBits { get; internal set; }

    public uint OutBuffer { get; internal set; }

    public int OutBits { get; internal set; }

    public G722CodecState(int rate, G722Flags options)
    {
      this.Band = new NAudio.Codecs.Band[2]{ new NAudio.Codecs.Band(), new NAudio.Codecs.Band() };
      this.QmfSignalHistory = new int[24];
      this.ItuTestMode = false;
      switch (rate)
      {
        case 48000:
          this.BitsPerSample = 6;
          break;
        case 56000:
          this.BitsPerSample = 7;
          break;
        case 64000:
          this.BitsPerSample = 8;
          break;
        default:
          throw new ArgumentException("Invalid rate, should be 48000, 56000 or 64000");
      }
      if ((options & G722Flags.SampleRate8000) == G722Flags.SampleRate8000)
        this.EncodeFrom8000Hz = true;
      this.Packed = (options & G722Flags.Packed) == G722Flags.Packed && this.BitsPerSample != 8;
      this.Band[0].det = 32;
      this.Band[1].det = 8;
    }
  }
}
