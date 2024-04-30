// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiFile
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace NAudio.Midi
{
  public class MidiFile
  {
    private MidiEventCollection events;
    private ushort fileFormat;
    private ushort deltaTicksPerQuarterNote;
    private bool strictChecking;

    public MidiFile(string filename)
      : this(filename, true)
    {
    }

    public int FileFormat => (int) this.fileFormat;

    public MidiFile(string filename, bool strictChecking)
    {
      this.strictChecking = strictChecking;
      BinaryReader br = new BinaryReader((Stream) File.OpenRead(filename));
      using (br)
      {
        if (Encoding.UTF8.GetString(br.ReadBytes(4)) != "MThd")
          throw new FormatException("Not a MIDI file - header chunk missing");
        this.fileFormat = MidiFile.SwapUInt32(br.ReadUInt32()) == 6U ? MidiFile.SwapUInt16(br.ReadUInt16()) : throw new FormatException("Unexpected header chunk length");
        int num1 = (int) MidiFile.SwapUInt16(br.ReadUInt16());
        this.deltaTicksPerQuarterNote = MidiFile.SwapUInt16(br.ReadUInt16());
        this.events = new MidiEventCollection(this.fileFormat == (ushort) 0 ? 0 : 1, (int) this.deltaTicksPerQuarterNote);
        for (int index = 0; index < num1; ++index)
          this.events.AddTrack();
        long num2 = 0;
        for (int trackNumber = 0; trackNumber < num1; ++trackNumber)
        {
          if (this.fileFormat == (ushort) 1)
            num2 = 0L;
          uint num3 = !(Encoding.UTF8.GetString(br.ReadBytes(4)) != "MTrk") ? MidiFile.SwapUInt32(br.ReadUInt32()) : throw new FormatException("Invalid chunk header");
          long position = br.BaseStream.Position;
          MidiEvent midiEvent = (MidiEvent) null;
          List<NoteOnEvent> outstandingNoteOns = new List<NoteOnEvent>();
          while (br.BaseStream.Position < position + (long) num3)
          {
            midiEvent = MidiEvent.ReadNextEvent(br, midiEvent);
            num2 += (long) midiEvent.DeltaTime;
            midiEvent.AbsoluteTime = num2;
            this.events[trackNumber].Add(midiEvent);
            if (midiEvent.CommandCode == MidiCommandCode.NoteOn)
            {
              NoteEvent offEvent = (NoteEvent) midiEvent;
              if (offEvent.Velocity > 0)
                outstandingNoteOns.Add((NoteOnEvent) offEvent);
              else
                this.FindNoteOn(offEvent, outstandingNoteOns);
            }
            else if (midiEvent.CommandCode == MidiCommandCode.NoteOff)
              this.FindNoteOn((NoteEvent) midiEvent, outstandingNoteOns);
            else if (midiEvent.CommandCode == MidiCommandCode.MetaEvent && ((MetaEvent) midiEvent).MetaEventType == MetaEventType.EndTrack && strictChecking && br.BaseStream.Position < position + (long) num3)
              throw new FormatException(string.Format("End Track event was not the last MIDI event on track {0}", (object) trackNumber));
          }
          if (outstandingNoteOns.Count > 0 && strictChecking)
            throw new FormatException(string.Format("Note ons without note offs {0} (file format {1})", (object) outstandingNoteOns.Count, (object) this.fileFormat));
          if (br.BaseStream.Position != position + (long) num3)
            throw new FormatException(string.Format("Read too far {0}+{1}!={2}", (object) num3, (object) position, (object) br.BaseStream.Position));
        }
      }
    }

    public MidiEventCollection Events => this.events;

    public int Tracks => this.events.Tracks;

    public int DeltaTicksPerQuarterNote => (int) this.deltaTicksPerQuarterNote;

    private void FindNoteOn(NoteEvent offEvent, List<NoteOnEvent> outstandingNoteOns)
    {
      bool flag = false;
      foreach (NoteOnEvent outstandingNoteOn in outstandingNoteOns)
      {
        if (outstandingNoteOn.Channel == offEvent.Channel && outstandingNoteOn.NoteNumber == offEvent.NoteNumber)
        {
          outstandingNoteOn.OffEvent = offEvent;
          outstandingNoteOns.Remove(outstandingNoteOn);
          flag = true;
          break;
        }
      }
      if (!flag && this.strictChecking)
        throw new FormatException(string.Format("Got an off without an on {0}", (object) offEvent));
    }

    private static uint SwapUInt32(uint i)
    {
      return (uint) ((int) ((i & 4278190080U) >> 24) | (int) ((i & 16711680U) >> 8) | ((int) i & 65280) << 8 | ((int) i & (int) byte.MaxValue) << 24);
    }

    private static ushort SwapUInt16(ushort i)
    {
      return (ushort) (((int) i & 65280) >> 8 | ((int) i & (int) byte.MaxValue) << 8);
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("Format {0}, Tracks {1}, Delta Ticks Per Quarter Note {2}\r\n", (object) this.fileFormat, (object) this.Tracks, (object) this.deltaTicksPerQuarterNote);
      for (int trackNumber = 0; trackNumber < this.Tracks; ++trackNumber)
      {
        foreach (MidiEvent midiEvent in (IEnumerable<MidiEvent>) this.events[trackNumber])
          stringBuilder.AppendFormat("{0}\r\n", (object) midiEvent);
      }
      return stringBuilder.ToString();
    }

    public static void Export(string filename, MidiEventCollection events)
    {
      if (events.MidiFileType == 0 && events.Tracks > 1)
        throw new ArgumentException("Can't export more than one track to a type 0 file");
      using (BinaryWriter writer = new BinaryWriter((Stream) File.Create(filename)))
      {
        writer.Write(Encoding.UTF8.GetBytes("MThd"));
        writer.Write(MidiFile.SwapUInt32(6U));
        writer.Write(MidiFile.SwapUInt16((ushort) events.MidiFileType));
        writer.Write(MidiFile.SwapUInt16((ushort) events.Tracks));
        writer.Write(MidiFile.SwapUInt16((ushort) events.DeltaTicksPerQuarterNote));
        for (int trackNumber = 0; trackNumber < events.Tracks; ++trackNumber)
        {
          IList<MidiEvent> list = events[trackNumber];
          writer.Write(Encoding.UTF8.GetBytes("MTrk"));
          long position = writer.BaseStream.Position;
          writer.Write(MidiFile.SwapUInt32(0U));
          long startAbsoluteTime = events.StartAbsoluteTime;
          MergeSort.Sort<MidiEvent>(list, (IComparer<MidiEvent>) new MidiEventComparer());
          int count = list.Count;
          foreach (MidiEvent midiEvent in (IEnumerable<MidiEvent>) list)
            midiEvent.Export(ref startAbsoluteTime, writer);
          uint i = (uint) (writer.BaseStream.Position - position) - 4U;
          writer.BaseStream.Position = position;
          writer.Write(MidiFile.SwapUInt32(i));
          writer.BaseStream.Position += (long) i;
        }
      }
    }
  }
}
