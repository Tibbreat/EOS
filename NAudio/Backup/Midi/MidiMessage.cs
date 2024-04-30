// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiMessage
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Midi
{
  public class MidiMessage
  {
    private int rawData;

    public MidiMessage(int status, int data1, int data2)
    {
      this.rawData = status + (data1 << 8) + (data2 << 16);
    }

    public MidiMessage(int rawData) => this.rawData = rawData;

    public static MidiMessage StartNote(int note, int volume, int channel)
    {
      return new MidiMessage(144 + channel - 1, note, volume);
    }

    public static MidiMessage StopNote(int note, int volume, int channel)
    {
      return new MidiMessage(128 + channel - 1, note, volume);
    }

    public static MidiMessage ChangePatch(int patch, int channel)
    {
      return new MidiMessage(192 + channel - 1, patch, 0);
    }

    public static MidiMessage ChangeControl(int controller, int value, int channel)
    {
      return new MidiMessage(176 + channel - 1, controller, value);
    }

    public int RawData => this.rawData;
  }
}
