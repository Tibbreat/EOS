// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MetaEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;
using System.Text;

#nullable disable
namespace NAudio.Midi
{
  public class MetaEvent : MidiEvent
  {
    private MetaEventType metaEvent;
    internal int metaDataLength;
    private byte[] data;

    public MetaEventType MetaEventType => this.metaEvent;

    protected MetaEvent()
    {
    }

    public MetaEvent(MetaEventType metaEventType, int metaDataLength, long absoluteTime)
      : base(absoluteTime, 1, MidiCommandCode.MetaEvent)
    {
      this.metaEvent = metaEventType;
      this.metaDataLength = metaDataLength;
    }

    public static MetaEvent ReadMetaEvent(BinaryReader br)
    {
      MetaEventType metaEventType1 = (MetaEventType) br.ReadByte();
      int num = MidiEvent.ReadVarInt(br);
      MetaEvent metaEvent = new MetaEvent();
      MetaEventType metaEventType2 = metaEventType1;
      if ((uint) metaEventType2 <= 81U)
      {
        switch (metaEventType2)
        {
          case MetaEventType.TrackSequenceNumber:
            metaEvent = (MetaEvent) new TrackSequenceNumberEvent(br, num);
            goto label_14;
          case MetaEventType.TextEvent:
          case MetaEventType.Copyright:
          case MetaEventType.SequenceTrackName:
          case MetaEventType.TrackInstrumentName:
          case MetaEventType.Lyric:
          case MetaEventType.Marker:
          case MetaEventType.CuePoint:
          case MetaEventType.ProgramName:
          case MetaEventType.DeviceName:
            metaEvent = (MetaEvent) new TextEvent(br, num);
            goto label_14;
          case MetaEventType.EndTrack:
            if (num != 0)
              throw new FormatException("End track length");
            goto label_14;
          case MetaEventType.SetTempo:
            metaEvent = (MetaEvent) new TempoEvent(br, num);
            goto label_14;
        }
      }
      else
      {
        switch (metaEventType2)
        {
          case MetaEventType.SmpteOffset:
            metaEvent = (MetaEvent) new SmpteOffsetEvent(br, num);
            goto label_14;
          case MetaEventType.TimeSignature:
            metaEvent = (MetaEvent) new TimeSignatureEvent(br, num);
            goto label_14;
          case MetaEventType.KeySignature:
            metaEvent = (MetaEvent) new KeySignatureEvent(br, num);
            goto label_14;
          case MetaEventType.SequencerSpecific:
            metaEvent = (MetaEvent) new SequencerSpecificEvent(br, num);
            goto label_14;
        }
      }
      metaEvent.data = br.ReadBytes(num);
      if (metaEvent.data.Length != num)
        throw new FormatException("Failed to read metaevent's data fully");
label_14:
      metaEvent.metaEvent = metaEventType1;
      metaEvent.metaDataLength = num;
      return metaEvent;
    }

    public override string ToString()
    {
      if (this.data == null)
        return string.Format("{0} {1}", (object) this.AbsoluteTime, (object) this.metaEvent);
      StringBuilder stringBuilder = new StringBuilder();
      foreach (byte num in this.data)
        stringBuilder.AppendFormat("{0:X2} ", (object) num);
      return string.Format("{0} {1}\r\n{2}", (object) this.AbsoluteTime, (object) this.metaEvent, (object) stringBuilder.ToString());
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write((byte) this.metaEvent);
      MidiEvent.WriteVarInt(writer, this.metaDataLength);
      if (this.data == null)
        return;
      writer.Write(this.data, 0, this.data.Length);
    }
  }
}
