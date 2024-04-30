// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.SmpteOffsetEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  internal class SmpteOffsetEvent : MetaEvent
  {
    private byte hours;
    private byte minutes;
    private byte seconds;
    private byte frames;
    private byte subFrames;

    public SmpteOffsetEvent(BinaryReader br, int length)
    {
      if (length != 5)
        throw new FormatException(string.Format("Invalid SMPTE Offset length: Got {0}, expected 5", (object) length));
      this.hours = br.ReadByte();
      this.minutes = br.ReadByte();
      this.seconds = br.ReadByte();
      this.frames = br.ReadByte();
      this.subFrames = br.ReadByte();
    }

    public int Hours => (int) this.hours;

    public int Minutes => (int) this.minutes;

    public int Seconds => (int) this.seconds;

    public int Frames => (int) this.frames;

    public int SubFrames => (int) this.subFrames;

    public override string ToString()
    {
      return string.Format("{0} {1}:{2}:{3}:{4}:{5}", (object) base.ToString(), (object) this.hours, (object) this.minutes, (object) this.seconds, (object) this.frames, (object) this.subFrames);
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write(this.hours);
      writer.Write(this.minutes);
      writer.Write(this.seconds);
      writer.Write(this.frames);
      writer.Write(this.subFrames);
    }
  }
}
