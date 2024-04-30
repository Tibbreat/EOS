// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.KeySignatureEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class KeySignatureEvent : MetaEvent
  {
    private byte sharpsFlats;
    private byte majorMinor;

    public KeySignatureEvent(BinaryReader br, int length)
    {
      if (length != 2)
        throw new FormatException("Invalid key signature length");
      this.sharpsFlats = br.ReadByte();
      this.majorMinor = br.ReadByte();
    }

    public KeySignatureEvent(int sharpsFlats, int majorMinor, long absoluteTime)
      : base(MetaEventType.KeySignature, 2, absoluteTime)
    {
      this.sharpsFlats = (byte) sharpsFlats;
      this.majorMinor = (byte) majorMinor;
    }

    public int SharpsFlats => (int) this.sharpsFlats;

    public int MajorMinor => (int) this.majorMinor;

    public override string ToString()
    {
      return string.Format("{0} {1} {2}", (object) base.ToString(), (object) this.sharpsFlats, (object) this.majorMinor);
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write(this.sharpsFlats);
      writer.Write(this.majorMinor);
    }
  }
}
