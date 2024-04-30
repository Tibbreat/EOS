﻿// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.PatchChangeEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class PatchChangeEvent : MidiEvent
  {
    private byte patch;
    private static readonly string[] patchNames = new string[128]
    {
      "Acoustic Grand",
      "Bright Acoustic",
      "Electric Grand",
      "Honky-Tonk",
      "Electric Piano 1",
      "Electric Piano 2",
      "Harpsichord",
      "Clav",
      "Celesta",
      "Glockenspiel",
      "Music Box",
      "Vibraphone",
      "Marimba",
      "Xylophone",
      "Tubular Bells",
      "Dulcimer",
      "Drawbar Organ",
      "Percussive Organ",
      "Rock Organ",
      "Church Organ",
      "Reed Organ",
      "Accoridan",
      "Harmonica",
      "Tango Accordian",
      "Acoustic Guitar(nylon)",
      "Acoustic Guitar(steel)",
      "Electric Guitar(jazz)",
      "Electric Guitar(clean)",
      "Electric Guitar(muted)",
      "Overdriven Guitar",
      "Distortion Guitar",
      "Guitar Harmonics",
      "Acoustic Bass",
      "Electric Bass(finger)",
      "Electric Bass(pick)",
      "Fretless Bass",
      "Slap Bass 1",
      "Slap Bass 2",
      "Synth Bass 1",
      "Synth Bass 2",
      "Violin",
      "Viola",
      "Cello",
      "Contrabass",
      "Tremolo Strings",
      "Pizzicato Strings",
      "Orchestral Strings",
      "Timpani",
      "String Ensemble 1",
      "String Ensemble 2",
      "SynthStrings 1",
      "SynthStrings 2",
      "Choir Aahs",
      "Voice Oohs",
      "Synth Voice",
      "Orchestra Hit",
      "Trumpet",
      "Trombone",
      "Tuba",
      "Muted Trumpet",
      "French Horn",
      "Brass Section",
      "SynthBrass 1",
      "SynthBrass 2",
      "Soprano Sax",
      "Alto Sax",
      "Tenor Sax",
      "Baritone Sax",
      "Oboe",
      "English Horn",
      "Bassoon",
      "Clarinet",
      "Piccolo",
      "Flute",
      "Recorder",
      "Pan Flute",
      "Blown Bottle",
      "Skakuhachi",
      "Whistle",
      "Ocarina",
      "Lead 1 (square)",
      "Lead 2 (sawtooth)",
      "Lead 3 (calliope)",
      "Lead 4 (chiff)",
      "Lead 5 (charang)",
      "Lead 6 (voice)",
      "Lead 7 (fifths)",
      "Lead 8 (bass+lead)",
      "Pad 1 (new age)",
      "Pad 2 (warm)",
      "Pad 3 (polysynth)",
      "Pad 4 (choir)",
      "Pad 5 (bowed)",
      "Pad 6 (metallic)",
      "Pad 7 (halo)",
      "Pad 8 (sweep)",
      "FX 1 (rain)",
      "FX 2 (soundtrack)",
      "FX 3 (crystal)",
      "FX 4 (atmosphere)",
      "FX 5 (brightness)",
      "FX 6 (goblins)",
      "FX 7 (echoes)",
      "FX 8 (sci-fi)",
      "Sitar",
      "Banjo",
      "Shamisen",
      "Koto",
      "Kalimba",
      "Bagpipe",
      "Fiddle",
      "Shanai",
      "Tinkle Bell",
      "Agogo",
      "Steel Drums",
      "Woodblock",
      "Taiko Drum",
      "Melodic Tom",
      "Synth Drum",
      "Reverse Cymbal",
      "Guitar Fret Noise",
      "Breath Noise",
      "Seashore",
      "Bird Tweet",
      "Telephone Ring",
      "Helicopter",
      "Applause",
      "Gunshot"
    };

    public static string GetPatchName(int patchNumber) => PatchChangeEvent.patchNames[patchNumber];

    public PatchChangeEvent(BinaryReader br)
    {
      this.patch = br.ReadByte();
      if (((int) this.patch & 128) != 0)
        throw new FormatException("Invalid patch");
    }

    public PatchChangeEvent(long absoluteTime, int channel, int patchNumber)
      : base(absoluteTime, channel, MidiCommandCode.PatchChange)
    {
      this.Patch = patchNumber;
    }

    public int Patch
    {
      get => (int) this.patch;
      set
      {
        this.patch = value >= 0 && value <= (int) sbyte.MaxValue ? (byte) value : throw new ArgumentOutOfRangeException(nameof (value), "Patch number must be in the range 0-127");
      }
    }

    public override string ToString()
    {
      return string.Format("{0} {1}", (object) base.ToString(), (object) PatchChangeEvent.GetPatchName((int) this.patch));
    }

    public override int GetAsShortMessage() => base.GetAsShortMessage() + ((int) this.patch << 8);

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write(this.patch);
    }
  }
}
