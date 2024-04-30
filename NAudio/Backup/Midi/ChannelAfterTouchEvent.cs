// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.ChannelAfterTouchEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class ChannelAfterTouchEvent : MidiEvent
  {
    private byte afterTouchPressure;

    public ChannelAfterTouchEvent(BinaryReader br)
    {
      this.afterTouchPressure = br.ReadByte();
      if (((int) this.afterTouchPressure & 128) != 0)
        throw new FormatException("Invalid afterTouchPressure");
    }

    public ChannelAfterTouchEvent(long absoluteTime, int channel, int afterTouchPressure)
      : base(absoluteTime, channel, MidiCommandCode.ChannelAfterTouch)
    {
      this.AfterTouchPressure = afterTouchPressure;
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write(this.afterTouchPressure);
    }

    public int AfterTouchPressure
    {
      get => (int) this.afterTouchPressure;
      set
      {
        this.afterTouchPressure = value >= 0 && value <= (int) sbyte.MaxValue ? (byte) value : throw new ArgumentOutOfRangeException(nameof (value), "After touch pressure must be in the range 0-127");
      }
    }
  }
}
