// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.ModulatorType
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.SoundFont
{
  public class ModulatorType
  {
    private bool polarity;
    private bool direction;
    private bool midiContinuousController;
    private ControllerSourceEnum controllerSource;
    private SourceTypeEnum sourceType;
    private ushort midiContinuousControllerNumber;

    internal ModulatorType(ushort raw)
    {
      this.polarity = ((int) raw & 512) == 512;
      this.direction = ((int) raw & 256) == 256;
      this.midiContinuousController = ((int) raw & 128) == 128;
      this.sourceType = (SourceTypeEnum) (((int) raw & 64512) >> 10);
      this.controllerSource = (ControllerSourceEnum) ((int) raw & (int) sbyte.MaxValue);
      this.midiContinuousControllerNumber = (ushort) ((uint) raw & (uint) sbyte.MaxValue);
    }

    public override string ToString()
    {
      return this.midiContinuousController ? string.Format("{0} CC{1}", (object) this.sourceType, (object) this.midiContinuousControllerNumber) : string.Format("{0} {1}", (object) this.sourceType, (object) this.controllerSource);
    }
  }
}
