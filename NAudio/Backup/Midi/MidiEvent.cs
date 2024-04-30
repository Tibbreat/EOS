// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class MidiEvent
  {
    private MidiCommandCode commandCode;
    private int channel;
    private int deltaTime;
    private long absoluteTime;

    public static MidiEvent FromRawMessage(int rawMessage)
    {
      long absoluteTime = 0;
      int num1 = rawMessage & (int) byte.MaxValue;
      int num2 = rawMessage >> 8 & (int) byte.MaxValue;
      int num3 = rawMessage >> 16 & (int) byte.MaxValue;
      int channel = 1;
      MidiCommandCode commandCode;
      if ((num1 & 240) == 240)
      {
        commandCode = (MidiCommandCode) num1;
      }
      else
      {
        commandCode = (MidiCommandCode) (num1 & 240);
        channel = (num1 & 15) + 1;
      }
      MidiCommandCode midiCommandCode = commandCode;
      if ((uint) midiCommandCode <= 176U)
      {
        if ((uint) midiCommandCode <= 144U)
        {
          if (midiCommandCode != MidiCommandCode.NoteOff && midiCommandCode != MidiCommandCode.NoteOn)
            goto label_20;
        }
        else if (midiCommandCode != MidiCommandCode.KeyAfterTouch)
        {
          if (midiCommandCode == MidiCommandCode.ControlChange)
            return (MidiEvent) new ControlChangeEvent(absoluteTime, channel, (MidiController) num2, num3);
          goto label_20;
        }
        return num3 > 0 && commandCode == MidiCommandCode.NoteOn ? (MidiEvent) new NoteOnEvent(absoluteTime, channel, num2, num3, 0) : (MidiEvent) new NoteEvent(absoluteTime, channel, commandCode, num2, num3);
      }
      if ((uint) midiCommandCode <= 208U)
      {
        if (midiCommandCode == MidiCommandCode.PatchChange)
          return (MidiEvent) new PatchChangeEvent(absoluteTime, channel, num2);
        if (midiCommandCode == MidiCommandCode.ChannelAfterTouch)
          return (MidiEvent) new ChannelAfterTouchEvent(absoluteTime, channel, num2);
      }
      else
      {
        switch (midiCommandCode)
        {
          case MidiCommandCode.PitchWheelChange:
            return (MidiEvent) new PitchWheelChangeEvent(absoluteTime, channel, num2 + (num3 << 7));
          case MidiCommandCode.TimingClock:
          case MidiCommandCode.StartSequence:
          case MidiCommandCode.ContinueSequence:
          case MidiCommandCode.StopSequence:
          case MidiCommandCode.AutoSensing:
            return new MidiEvent(absoluteTime, channel, commandCode);
        }
      }
label_20:
      throw new FormatException(string.Format("Unsupported MIDI Command Code for Raw Message {0}", (object) commandCode));
    }

    public static MidiEvent ReadNextEvent(BinaryReader br, MidiEvent previous)
    {
      int num1 = MidiEvent.ReadVarInt(br);
      int num2 = 1;
      byte num3 = br.ReadByte();
      MidiCommandCode midiCommandCode1;
      if (((int) num3 & 128) == 0)
      {
        midiCommandCode1 = previous.CommandCode;
        num2 = previous.Channel;
        --br.BaseStream.Position;
      }
      else if (((int) num3 & 240) == 240)
      {
        midiCommandCode1 = (MidiCommandCode) num3;
      }
      else
      {
        midiCommandCode1 = (MidiCommandCode) ((uint) num3 & 240U);
        num2 = ((int) num3 & 15) + 1;
      }
      MidiCommandCode midiCommandCode2 = midiCommandCode1;
      MidiEvent midiEvent;
      if ((uint) midiCommandCode2 <= 176U)
      {
        if ((uint) midiCommandCode2 <= 144U)
        {
          if (midiCommandCode2 != MidiCommandCode.NoteOff)
          {
            if (midiCommandCode2 == MidiCommandCode.NoteOn)
            {
              midiEvent = (MidiEvent) new NoteOnEvent(br);
              goto label_25;
            }
            else
              goto label_24;
          }
        }
        else if (midiCommandCode2 != MidiCommandCode.KeyAfterTouch)
        {
          if (midiCommandCode2 == MidiCommandCode.ControlChange)
          {
            midiEvent = (MidiEvent) new ControlChangeEvent(br);
            goto label_25;
          }
          else
            goto label_24;
        }
        midiEvent = (MidiEvent) new NoteEvent(br);
        goto label_25;
      }
      else if ((uint) midiCommandCode2 <= 208U)
      {
        if (midiCommandCode2 != MidiCommandCode.PatchChange)
        {
          if (midiCommandCode2 == MidiCommandCode.ChannelAfterTouch)
          {
            midiEvent = (MidiEvent) new ChannelAfterTouchEvent(br);
            goto label_25;
          }
        }
        else
        {
          midiEvent = (MidiEvent) new PatchChangeEvent(br);
          goto label_25;
        }
      }
      else
      {
        switch (midiCommandCode2)
        {
          case MidiCommandCode.PitchWheelChange:
            midiEvent = (MidiEvent) new PitchWheelChangeEvent(br);
            goto label_25;
          case MidiCommandCode.Sysex:
            midiEvent = (MidiEvent) SysexEvent.ReadSysexEvent(br);
            goto label_25;
          case MidiCommandCode.TimingClock:
          case MidiCommandCode.StartSequence:
          case MidiCommandCode.ContinueSequence:
          case MidiCommandCode.StopSequence:
            midiEvent = new MidiEvent();
            goto label_25;
          case MidiCommandCode.MetaEvent:
            midiEvent = (MidiEvent) MetaEvent.ReadMetaEvent(br);
            goto label_25;
        }
      }
label_24:
      throw new FormatException(string.Format("Unsupported MIDI Command Code {0:X2}", (object) (byte) midiCommandCode1));
label_25:
      midiEvent.channel = num2;
      midiEvent.deltaTime = num1;
      midiEvent.commandCode = midiCommandCode1;
      return midiEvent;
    }

    public virtual int GetAsShortMessage() => (int) ((byte) (this.channel - 1) + this.commandCode);

    protected MidiEvent()
    {
    }

    public MidiEvent(long absoluteTime, int channel, MidiCommandCode commandCode)
    {
      this.absoluteTime = absoluteTime;
      this.Channel = channel;
      this.commandCode = commandCode;
    }

    public virtual int Channel
    {
      get => this.channel;
      set
      {
        this.channel = value >= 1 && value <= 16 ? value : throw new ArgumentOutOfRangeException(nameof (value), (object) value, string.Format("Channel must be 1-16 (Got {0})", (object) value));
      }
    }

    public int DeltaTime => this.deltaTime;

    public long AbsoluteTime
    {
      get => this.absoluteTime;
      set => this.absoluteTime = value;
    }

    public MidiCommandCode CommandCode => this.commandCode;

    public static bool IsNoteOff(MidiEvent midiEvent)
    {
      if (midiEvent == null)
        return false;
      return midiEvent.CommandCode == MidiCommandCode.NoteOn ? ((NoteEvent) midiEvent).Velocity == 0 : midiEvent.CommandCode == MidiCommandCode.NoteOff;
    }

    public static bool IsNoteOn(MidiEvent midiEvent)
    {
      return midiEvent != null && midiEvent.CommandCode == MidiCommandCode.NoteOn && ((NoteEvent) midiEvent).Velocity > 0;
    }

    public static bool IsEndTrack(MidiEvent midiEvent)
    {
      return midiEvent != null && midiEvent is MetaEvent metaEvent && metaEvent.MetaEventType == MetaEventType.EndTrack;
    }

    public override string ToString()
    {
      return this.commandCode >= MidiCommandCode.Sysex ? string.Format("{0} {1}", (object) this.absoluteTime, (object) this.commandCode) : string.Format("{0} {1} Ch: {2}", (object) this.absoluteTime, (object) this.commandCode, (object) this.channel);
    }

    public static int ReadVarInt(BinaryReader br)
    {
      int num1 = 0;
      for (int index = 0; index < 4; ++index)
      {
        byte num2 = br.ReadByte();
        num1 = (num1 << 7) + ((int) num2 & (int) sbyte.MaxValue);
        if (((int) num2 & 128) == 0)
          return num1;
      }
      throw new FormatException("Invalid Var Int");
    }

    public static void WriteVarInt(BinaryWriter writer, int value)
    {
      if (value < 0)
        throw new ArgumentOutOfRangeException(nameof (value), (object) value, "Cannot write a negative Var Int");
      if (value > 268435455)
        throw new ArgumentOutOfRangeException(nameof (value), (object) value, "Maximum allowed Var Int is 0x0FFFFFFF");
      int index = 0;
      byte[] numArray = new byte[4];
      do
      {
        numArray[index++] = (byte) (value & (int) sbyte.MaxValue);
        value >>= 7;
      }
      while (value > 0);
      while (index > 0)
      {
        --index;
        if (index > 0)
          writer.Write((byte) ((uint) numArray[index] | 128U));
        else
          writer.Write(numArray[index]);
      }
    }

    public virtual void Export(ref long absoluteTime, BinaryWriter writer)
    {
      if (this.absoluteTime < absoluteTime)
        throw new FormatException("Can't export unsorted MIDI events");
      MidiEvent.WriteVarInt(writer, (int) (this.absoluteTime - absoluteTime));
      absoluteTime = this.absoluteTime;
      int commandCode = (int) this.commandCode;
      if (this.commandCode != MidiCommandCode.MetaEvent)
        commandCode += this.channel - 1;
      writer.Write((byte) commandCode);
    }
  }
}
