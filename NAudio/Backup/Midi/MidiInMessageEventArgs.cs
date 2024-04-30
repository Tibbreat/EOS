// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiInMessageEventArgs
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Midi
{
  public class MidiInMessageEventArgs : EventArgs
  {
    public MidiInMessageEventArgs(int message, int timestamp)
    {
      this.RawMessage = message;
      this.Timestamp = timestamp;
      try
      {
        this.MidiEvent = MidiEvent.FromRawMessage(message);
      }
      catch (Exception ex)
      {
      }
    }

    public int RawMessage { get; private set; }

    public MidiEvent MidiEvent { get; private set; }

    public int Timestamp { get; private set; }
  }
}
