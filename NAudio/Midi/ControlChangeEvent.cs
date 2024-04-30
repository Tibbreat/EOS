// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.ControlChangeEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class ControlChangeEvent : MidiEvent
  {
    private MidiController controller;
    private byte controllerValue;

    public ControlChangeEvent(BinaryReader br)
    {
      byte num = br.ReadByte();
      this.controllerValue = br.ReadByte();
      this.controller = ((int) num & 128) == 0 ? (MidiController) num : throw new InvalidDataException("Invalid controller");
      if (((int) this.controllerValue & 128) != 0)
        throw new InvalidDataException(string.Format("Invalid controllerValue {0} for controller {1}, Pos 0x{2:X}", (object) this.controllerValue, (object) this.controller, (object) br.BaseStream.Position));
    }

    public ControlChangeEvent(
      long absoluteTime,
      int channel,
      MidiController controller,
      int controllerValue)
      : base(absoluteTime, channel, MidiCommandCode.ControlChange)
    {
      this.Controller = controller;
      this.ControllerValue = controllerValue;
    }

    public override string ToString()
    {
      return string.Format("{0} Controller {1} Value {2}", (object) base.ToString(), (object) this.controller, (object) this.controllerValue);
    }

    public override int GetAsShortMessage()
    {
      byte controller = (byte) this.controller;
      return base.GetAsShortMessage() + ((int) controller << 8) + ((int) this.controllerValue << 16);
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write((byte) this.controller);
      writer.Write(this.controllerValue);
    }

    public MidiController Controller
    {
      get => this.controller;
      set
      {
        this.controller = value >= MidiController.BankSelect && value <= (MidiController.AllNotesOff | MidiController.FootController) ? value : throw new ArgumentOutOfRangeException(nameof (value), "Controller number must be in the range 0-127");
      }
    }

    public int ControllerValue
    {
      get => (int) this.controllerValue;
      set
      {
        this.controllerValue = value >= 0 && value <= (int) sbyte.MaxValue ? (byte) value : throw new ArgumentOutOfRangeException(nameof (value), "Controller Value must be in the range 0-127");
      }
    }
  }
}
