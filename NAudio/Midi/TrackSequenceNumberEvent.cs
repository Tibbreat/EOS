// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.TrackSequenceNumberEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class TrackSequenceNumberEvent : MetaEvent
  {
    private ushort sequenceNumber;

    public TrackSequenceNumberEvent(BinaryReader br, int length)
    {
      if (length != 2)
        throw new FormatException("Invalid sequence number length");
      this.sequenceNumber = (ushort) (((uint) br.ReadByte() << 8) + (uint) br.ReadByte());
    }

    public override string ToString()
    {
      return string.Format("{0} {1}", (object) base.ToString(), (object) this.sequenceNumber);
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write((byte) ((int) this.sequenceNumber >> 8 & (int) byte.MaxValue));
      writer.Write((byte) ((uint) this.sequenceNumber & (uint) byte.MaxValue));
    }
  }
}
