// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.TimeSignatureEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class TimeSignatureEvent : MetaEvent
  {
    private byte numerator;
    private byte denominator;
    private byte ticksInMetronomeClick;
    private byte no32ndNotesInQuarterNote;

    public TimeSignatureEvent(BinaryReader br, int length)
    {
      if (length != 4)
        throw new FormatException(string.Format("Invalid time signature length: Got {0}, expected 4", (object) length));
      this.numerator = br.ReadByte();
      this.denominator = br.ReadByte();
      this.ticksInMetronomeClick = br.ReadByte();
      this.no32ndNotesInQuarterNote = br.ReadByte();
    }

    public TimeSignatureEvent(
      long absoluteTime,
      int numerator,
      int denominator,
      int ticksInMetronomeClick,
      int no32ndNotesInQuarterNote)
      : base(MetaEventType.TimeSignature, 4, absoluteTime)
    {
      this.numerator = (byte) numerator;
      this.denominator = (byte) denominator;
      this.ticksInMetronomeClick = (byte) ticksInMetronomeClick;
      this.no32ndNotesInQuarterNote = (byte) no32ndNotesInQuarterNote;
    }

    [Obsolete("Use the constructor that has absolute time first")]
    public TimeSignatureEvent(
      int numerator,
      int denominator,
      int ticksInMetronomeClick,
      int no32ndNotesInQuarterNote,
      long absoluteTime)
      : base(MetaEventType.TimeSignature, 4, absoluteTime)
    {
      this.numerator = (byte) numerator;
      this.denominator = (byte) denominator;
      this.ticksInMetronomeClick = (byte) ticksInMetronomeClick;
      this.no32ndNotesInQuarterNote = (byte) no32ndNotesInQuarterNote;
    }

    public int Numerator => (int) this.numerator;

    public int Denominator => (int) this.denominator;

    public int TicksInMetronomeClick => (int) this.ticksInMetronomeClick;

    public int No32ndNotesInQuarterNote => (int) this.no32ndNotesInQuarterNote;

    public string TimeSignature
    {
      get
      {
        string str = string.Format("Unknown ({0})", (object) this.denominator);
        switch (this.denominator)
        {
          case 1:
            str = "2";
            break;
          case 2:
            str = "4";
            break;
          case 3:
            str = "8";
            break;
          case 4:
            str = "16";
            break;
          case 5:
            str = "32";
            break;
        }
        return string.Format("{0}/{1}", (object) this.numerator, (object) str);
      }
    }

    public override string ToString()
    {
      return string.Format("{0} {1} TicksInClick:{2} 32ndsInQuarterNote:{3}", (object) base.ToString(), (object) this.TimeSignature, (object) this.ticksInMetronomeClick, (object) this.no32ndNotesInQuarterNote);
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write(this.numerator);
      writer.Write(this.denominator);
      writer.Write(this.ticksInMetronomeClick);
      writer.Write(this.no32ndNotesInQuarterNote);
    }
  }
}
