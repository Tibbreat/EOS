// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveFormatExtraData
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public class WaveFormatExtraData : WaveFormat
  {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    private byte[] extraData = new byte[100];

    public byte[] ExtraData => this.extraData;

    internal WaveFormatExtraData()
    {
    }

    public WaveFormatExtraData(BinaryReader reader)
      : base(reader)
    {
      this.ReadExtraData(reader);
    }

    internal void ReadExtraData(BinaryReader reader)
    {
      if (this.extraSize <= (short) 0)
        return;
      reader.Read(this.extraData, 0, (int) this.extraSize);
    }

    public override void Serialize(BinaryWriter writer)
    {
      base.Serialize(writer);
      if (this.extraSize <= (short) 0)
        return;
      writer.Write(this.extraData, 0, (int) this.extraSize);
    }
  }
}
