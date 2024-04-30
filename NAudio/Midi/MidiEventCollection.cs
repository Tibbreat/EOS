// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiEventCollection
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace NAudio.Midi
{
  public class MidiEventCollection : IEnumerable<IList<MidiEvent>>, IEnumerable
  {
    private int midiFileType;
    private List<IList<MidiEvent>> trackEvents;
    private int deltaTicksPerQuarterNote;
    private long startAbsoluteTime;

    public MidiEventCollection(int midiFileType, int deltaTicksPerQuarterNote)
    {
      this.midiFileType = midiFileType;
      this.deltaTicksPerQuarterNote = deltaTicksPerQuarterNote;
      this.startAbsoluteTime = 0L;
      this.trackEvents = new List<IList<MidiEvent>>();
    }

    public int Tracks => this.trackEvents.Count;

    public long StartAbsoluteTime
    {
      get => this.startAbsoluteTime;
      set => this.startAbsoluteTime = value;
    }

    public int DeltaTicksPerQuarterNote => this.deltaTicksPerQuarterNote;

    public IList<MidiEvent> GetTrackEvents(int trackNumber) => this.trackEvents[trackNumber];

    public IList<MidiEvent> this[int trackNumber] => this.trackEvents[trackNumber];

    public IList<MidiEvent> AddTrack() => this.AddTrack((IList<MidiEvent>) null);

    public IList<MidiEvent> AddTrack(IList<MidiEvent> initialEvents)
    {
      List<MidiEvent> midiEventList = new List<MidiEvent>();
      if (initialEvents != null)
        midiEventList.AddRange((IEnumerable<MidiEvent>) initialEvents);
      this.trackEvents.Add((IList<MidiEvent>) midiEventList);
      return (IList<MidiEvent>) midiEventList;
    }

    public void RemoveTrack(int track) => this.trackEvents.RemoveAt(track);

    public void Clear() => this.trackEvents.Clear();

    public int MidiFileType
    {
      get => this.midiFileType;
      set
      {
        if (this.midiFileType == value)
          return;
        this.midiFileType = value;
        if (value == 0)
          this.FlattenToOneTrack();
        else
          this.ExplodeToManyTracks();
      }
    }

    public void AddEvent(MidiEvent midiEvent, int originalTrack)
    {
      if (this.midiFileType == 0)
      {
        this.EnsureTracks(1);
        this.trackEvents[0].Add(midiEvent);
      }
      else if (originalTrack == 0)
      {
        MidiCommandCode commandCode = midiEvent.CommandCode;
        if ((uint) commandCode <= 160U)
        {
          if (commandCode != MidiCommandCode.NoteOff && commandCode != MidiCommandCode.NoteOn && commandCode != MidiCommandCode.KeyAfterTouch)
            goto label_9;
        }
        else if ((uint) commandCode <= 192U)
        {
          if (commandCode != MidiCommandCode.ControlChange && commandCode != MidiCommandCode.PatchChange)
            goto label_9;
        }
        else if (commandCode != MidiCommandCode.ChannelAfterTouch && commandCode != MidiCommandCode.PitchWheelChange)
          goto label_9;
        this.EnsureTracks(midiEvent.Channel + 1);
        this.trackEvents[midiEvent.Channel].Add(midiEvent);
        return;
label_9:
        this.EnsureTracks(1);
        this.trackEvents[0].Add(midiEvent);
      }
      else
      {
        this.EnsureTracks(originalTrack + 1);
        this.trackEvents[originalTrack].Add(midiEvent);
      }
    }

    private void EnsureTracks(int count)
    {
      for (int count1 = this.trackEvents.Count; count1 < count; ++count1)
        this.trackEvents.Add((IList<MidiEvent>) new List<MidiEvent>());
    }

    private void ExplodeToManyTracks()
    {
      IList<MidiEvent> trackEvent = this.trackEvents[0];
      this.Clear();
      foreach (MidiEvent midiEvent in (IEnumerable<MidiEvent>) trackEvent)
        this.AddEvent(midiEvent, 0);
      this.PrepareForExport();
    }

    private void FlattenToOneTrack()
    {
      bool flag = false;
      for (int index = 1; index < this.trackEvents.Count; ++index)
      {
        foreach (MidiEvent midiEvent in (IEnumerable<MidiEvent>) this.trackEvents[index])
        {
          if (!MidiEvent.IsEndTrack(midiEvent))
          {
            this.trackEvents[0].Add(midiEvent);
            flag = true;
          }
        }
      }
      for (int track = this.trackEvents.Count - 1; track > 0; --track)
        this.RemoveTrack(track);
      if (!flag)
        return;
      this.PrepareForExport();
    }

    public void PrepareForExport()
    {
      MidiEventComparer midiEventComparer = new MidiEventComparer();
      foreach (List<MidiEvent> trackEvent in this.trackEvents)
      {
        MergeSort.Sort<MidiEvent>((IList<MidiEvent>) trackEvent, (IComparer<MidiEvent>) midiEventComparer);
        int index = 0;
        while (index < trackEvent.Count - 1)
        {
          if (MidiEvent.IsEndTrack(trackEvent[index]))
            trackEvent.RemoveAt(index);
          else
            ++index;
        }
      }
      int num = 0;
      while (num < this.trackEvents.Count)
      {
        IList<MidiEvent> trackEvent = this.trackEvents[num];
        if (trackEvent.Count == 0)
          this.RemoveTrack(num);
        else if (trackEvent.Count == 1 && MidiEvent.IsEndTrack(trackEvent[0]))
        {
          this.RemoveTrack(num);
        }
        else
        {
          if (!MidiEvent.IsEndTrack(trackEvent[trackEvent.Count - 1]))
            trackEvent.Add((MidiEvent) new MetaEvent(MetaEventType.EndTrack, 0, trackEvent[trackEvent.Count - 1].AbsoluteTime));
          ++num;
        }
      }
    }

    public IEnumerator<IList<MidiEvent>> GetEnumerator()
    {
      return (IEnumerator<IList<MidiEvent>>) this.trackEvents.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.trackEvents.GetEnumerator();
  }
}
