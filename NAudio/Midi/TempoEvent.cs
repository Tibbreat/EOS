// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.TempoEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class TempoEvent : MetaEvent
  {
    private int microsecondsPerQuarterNote;

    public TempoEvent(BinaryReader br, int length)
    {
      if (length != 3)
        throw new FormatException("Invalid tempo length");
      this.microsecondsPerQuarterNote = ((int) br.ReadByte() << 16) + ((int) br.ReadByte() << 8) + (int) br.ReadByte();
    }

    public TempoEvent(int microsecondsPerQuarterNote, long absoluteTime)
      : base(MetaEventType.SetTempo, 3, absoluteTime)
    {
      this.microsecondsPerQuarterNote = microsecondsPerQuarterNote;
    }

    public override string ToString()
    {
      return string.Format("{0} {2}bpm ({1})", (object) base.ToString(), (object) this.microsecondsPerQuarterNote, (object) (60000000 / this.microsecondsPerQuarterNote));
    }

    public int MicrosecondsPerQuarterNote
    {
      get => this.microsecondsPerQuarterNote;
      set => this.microsecondsPerQuarterNote = value;
    }

    public double Tempo
    {
      get => 60000000.0 / (double) this.microsecondsPerQuarterNote;
      set => this.microsecondsPerQuarterNote = (int) (60000000.0 / value);
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write((byte) (this.microsecondsPerQuarterNote >> 16 & (int) byte.MaxValue));
      writer.Write((byte) (this.microsecondsPerQuarterNote >> 8 & (int) byte.MaxValue));
      writer.Write((byte) (this.microsecondsPerQuarterNote & (int) byte.MaxValue));
    }
  }
}
