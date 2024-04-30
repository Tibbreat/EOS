// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.PitchWheelChangeEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class PitchWheelChangeEvent : MidiEvent
  {
    private int pitch;

    public PitchWheelChangeEvent(BinaryReader br)
    {
      byte num1 = br.ReadByte();
      byte num2 = br.ReadByte();
      if (((int) num1 & 128) != 0)
        throw new FormatException("Invalid pitchwheelchange byte 1");
      if (((int) num2 & 128) != 0)
        throw new FormatException("Invalid pitchwheelchange byte 2");
      this.pitch = (int) num1 + ((int) num2 << 7);
    }

    public PitchWheelChangeEvent(long absoluteTime, int channel, int pitchWheel)
      : base(absoluteTime, channel, MidiCommandCode.PitchWheelChange)
    {
      this.Pitch = pitchWheel;
    }

    public override string ToString()
    {
      return string.Format("{0} Pitch {1} ({2})", (object) base.ToString(), (object) this.pitch, (object) (this.pitch - 8192));
    }

    public int Pitch
    {
      get => this.pitch;
      set
      {
        this.pitch = value >= 0 && value <= 16384 ? value : throw new ArgumentOutOfRangeException(nameof (value), "Pitch value must be in the range 0 - 0x4000");
      }
    }

    public override int GetAsShortMessage()
    {
      return base.GetAsShortMessage() + ((this.pitch & (int) sbyte.MaxValue) << 8) + ((this.pitch >> 7 & (int) sbyte.MaxValue) << 16);
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write((byte) (this.pitch & (int) sbyte.MaxValue));
      writer.Write((byte) (this.pitch >> 7 & (int) sbyte.MaxValue));
    }
  }
}
