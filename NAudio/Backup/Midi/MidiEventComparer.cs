// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiEventComparer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Collections.Generic;

#nullable disable
namespace NAudio.Midi
{
  public class MidiEventComparer : IComparer<MidiEvent>
  {
    public int Compare(MidiEvent x, MidiEvent y)
    {
      long num1 = x.AbsoluteTime;
      long num2 = y.AbsoluteTime;
      if (num1 == num2)
      {
        MetaEvent metaEvent1 = x as MetaEvent;
        MetaEvent metaEvent2 = y as MetaEvent;
        if (metaEvent1 != null)
          num1 = metaEvent1.MetaEventType != MetaEventType.EndTrack ? long.MinValue : long.MaxValue;
        if (metaEvent2 != null)
          num2 = metaEvent2.MetaEventType != MetaEventType.EndTrack ? long.MinValue : long.MaxValue;
      }
      return num1.CompareTo(num2);
    }
  }
}
