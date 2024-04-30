// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.NoteOnEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class NoteOnEvent : NoteEvent
  {
    private NoteEvent offEvent;

    public NoteOnEvent(BinaryReader br)
      : base(br)
    {
    }

    public NoteOnEvent(
      long absoluteTime,
      int channel,
      int noteNumber,
      int velocity,
      int duration)
      : base(absoluteTime, channel, MidiCommandCode.NoteOn, noteNumber, velocity)
    {
      this.OffEvent = new NoteEvent(absoluteTime, channel, MidiCommandCode.NoteOff, noteNumber, 0);
      this.NoteLength = duration;
    }

    public NoteEvent OffEvent
    {
      get => this.offEvent;
      set
      {
        if (!MidiEvent.IsNoteOff((MidiEvent) value))
          throw new ArgumentException("OffEvent must be a valid MIDI note off event");
        if (value.NoteNumber != this.NoteNumber)
          throw new ArgumentException("Note Off Event must be for the same note number");
        this.offEvent = value.Channel == this.Channel ? value : throw new ArgumentException("Note Off Event must be for the same channel");
      }
    }

    public override int NoteNumber
    {
      get => base.NoteNumber;
      set
      {
        base.NoteNumber = value;
        if (this.OffEvent == null)
          return;
        this.OffEvent.NoteNumber = this.NoteNumber;
      }
    }

    public override int Channel
    {
      get => base.Channel;
      set
      {
        base.Channel = value;
        if (this.OffEvent == null)
          return;
        this.OffEvent.Channel = this.Channel;
      }
    }

    public int NoteLength
    {
      get => (int) (this.offEvent.AbsoluteTime - this.AbsoluteTime);
      set
      {
        if (value < 0)
          throw new ArgumentException("NoteLength must be 0 or greater");
        this.offEvent.AbsoluteTime = this.AbsoluteTime + (long) value;
      }
    }

    public override string ToString()
    {
      return this.Velocity == 0 && this.OffEvent == null ? string.Format("{0} (Note Off)", (object) base.ToString()) : string.Format("{0} Len: {1}", (object) base.ToString(), this.OffEvent == null ? (object) "?" : (object) this.NoteLength.ToString());
    }
  }
}
