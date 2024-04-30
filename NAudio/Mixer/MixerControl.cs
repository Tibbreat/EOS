// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.MixerControl
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Mixer
{
  public abstract class MixerControl
  {
    internal MixerInterop.MIXERCONTROL mixerControl;
    internal MixerInterop.MIXERCONTROLDETAILS mixerControlDetails;
    protected IntPtr mixerHandle;
    protected int nChannels;
    protected MixerFlags mixerHandleType;

    public static IList<MixerControl> GetMixerControls(
      IntPtr mixerHandle,
      MixerLine mixerLine,
      MixerFlags mixerHandleType)
    {
      List<MixerControl> mixerControls = new List<MixerControl>();
      if (mixerLine.ControlsCount > 0)
      {
        int num = Marshal.SizeOf(typeof (MixerInterop.MIXERCONTROL));
        MixerInterop.MIXERLINECONTROLS mixerLineControls = new MixerInterop.MIXERLINECONTROLS();
        IntPtr hglobal = Marshal.AllocHGlobal(num * mixerLine.ControlsCount);
        mixerLineControls.cbStruct = Marshal.SizeOf((object) mixerLineControls);
        mixerLineControls.dwLineID = mixerLine.LineId;
        mixerLineControls.cControls = mixerLine.ControlsCount;
        mixerLineControls.pamxctrl = hglobal;
        mixerLineControls.cbmxctrl = Marshal.SizeOf(typeof (MixerInterop.MIXERCONTROL));
        try
        {
          MmResult lineControls = MixerInterop.mixerGetLineControls(mixerHandle, ref mixerLineControls, mixerHandleType);
          if (lineControls != MmResult.NoError)
            throw new MmException(lineControls, "mixerGetLineControls");
          for (int index = 0; index < mixerLineControls.cControls; ++index)
          {
            MixerInterop.MIXERCONTROL structure = (MixerInterop.MIXERCONTROL) Marshal.PtrToStructure((IntPtr) (hglobal.ToInt64() + (long) (num * index)), typeof (MixerInterop.MIXERCONTROL));
            MixerControl mixerControl = MixerControl.GetMixerControl(mixerHandle, mixerLine.LineId, structure.dwControlID, mixerLine.Channels, mixerHandleType);
            mixerControls.Add(mixerControl);
          }
        }
        finally
        {
          Marshal.FreeHGlobal(hglobal);
        }
      }
      return (IList<MixerControl>) mixerControls;
    }

    public static MixerControl GetMixerControl(
      IntPtr mixerHandle,
      int nLineID,
      int controlId,
      int nChannels,
      MixerFlags mixerFlags)
    {
      MixerInterop.MIXERLINECONTROLS mixerLineControls = new MixerInterop.MIXERLINECONTROLS();
      MixerInterop.MIXERCONTROL structure1 = new MixerInterop.MIXERCONTROL();
      IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf((object) structure1));
      mixerLineControls.cbStruct = Marshal.SizeOf((object) mixerLineControls);
      mixerLineControls.cControls = 1;
      mixerLineControls.dwControlID = controlId;
      mixerLineControls.cbmxctrl = Marshal.SizeOf((object) structure1);
      mixerLineControls.pamxctrl = ptr;
      mixerLineControls.dwLineID = nLineID;
      MmResult lineControls = MixerInterop.mixerGetLineControls(mixerHandle, ref mixerLineControls, MixerFlags.ListText | mixerFlags);
      if (lineControls != MmResult.NoError)
      {
        Marshal.FreeCoTaskMem(ptr);
        throw new MmException(lineControls, "mixerGetLineControls");
      }
      MixerInterop.MIXERCONTROL structure2 = (MixerInterop.MIXERCONTROL) Marshal.PtrToStructure(mixerLineControls.pamxctrl, typeof (MixerInterop.MIXERCONTROL));
      Marshal.FreeCoTaskMem(ptr);
      if (MixerControl.IsControlBoolean(structure2.dwControlType))
        return (MixerControl) new BooleanMixerControl(structure2, mixerHandle, mixerFlags, nChannels);
      if (MixerControl.IsControlSigned(structure2.dwControlType))
        return (MixerControl) new SignedMixerControl(structure2, mixerHandle, mixerFlags, nChannels);
      if (MixerControl.IsControlUnsigned(structure2.dwControlType))
        return (MixerControl) new UnsignedMixerControl(structure2, mixerHandle, mixerFlags, nChannels);
      if (MixerControl.IsControlListText(structure2.dwControlType))
        return (MixerControl) new ListTextMixerControl(structure2, mixerHandle, mixerFlags, nChannels);
      if (MixerControl.IsControlCustom(structure2.dwControlType))
        return (MixerControl) new CustomMixerControl(structure2, mixerHandle, mixerFlags, nChannels);
      throw new InvalidOperationException(string.Format("Unknown mixer control type {0}", (object) structure2.dwControlType));
    }

    protected void GetControlDetails()
    {
      this.mixerControlDetails.cbStruct = Marshal.SizeOf((object) this.mixerControlDetails);
      this.mixerControlDetails.dwControlID = this.mixerControl.dwControlID;
      this.mixerControlDetails.cChannels = !this.IsCustom ? (((int) this.mixerControl.fdwControl & 1) == 0 ? this.nChannels : 1) : 0;
      this.mixerControlDetails.hwndOwner = ((int) this.mixerControl.fdwControl & 2) == 0 ? (!this.IsCustom ? IntPtr.Zero : IntPtr.Zero) : (IntPtr) (long) this.mixerControl.cMultipleItems;
      this.mixerControlDetails.cbDetails = !this.IsBoolean ? (!this.IsListText ? (!this.IsSigned ? (!this.IsUnsigned ? this.mixerControl.Metrics.customData : Marshal.SizeOf((object) new MixerInterop.MIXERCONTROLDETAILS_UNSIGNED())) : Marshal.SizeOf((object) new MixerInterop.MIXERCONTROLDETAILS_SIGNED())) : Marshal.SizeOf((object) new MixerInterop.MIXERCONTROLDETAILS_LISTTEXT())) : Marshal.SizeOf((object) new MixerInterop.MIXERCONTROLDETAILS_BOOLEAN());
      int cb = this.mixerControlDetails.cbDetails * this.mixerControlDetails.cChannels;
      if (((int) this.mixerControl.fdwControl & 2) != 0)
        cb *= (int) this.mixerControl.cMultipleItems;
      IntPtr ptr = Marshal.AllocCoTaskMem(cb);
      this.mixerControlDetails.paDetails = ptr;
      MmResult controlDetails = MixerInterop.mixerGetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType);
      if (controlDetails == MmResult.NoError)
        this.GetDetails(this.mixerControlDetails.paDetails);
      Marshal.FreeCoTaskMem(ptr);
      if (controlDetails != MmResult.NoError)
        throw new MmException(controlDetails, "mixerGetControlDetails");
    }

    protected abstract void GetDetails(IntPtr pDetails);

    public string Name => this.mixerControl.szName;

    public MixerControlType ControlType => this.mixerControl.dwControlType;

    private static bool IsControlBoolean(MixerControlType controlType)
    {
      switch (controlType)
      {
        case MixerControlType.BooleanMeter:
        case MixerControlType.Boolean:
        case MixerControlType.OnOff:
        case MixerControlType.Mute:
        case MixerControlType.Mono:
        case MixerControlType.Loudness:
        case MixerControlType.StereoEnhance:
        case MixerControlType.Button:
        case MixerControlType.SingleSelect:
        case MixerControlType.Mux:
        case MixerControlType.MultipleSelect:
        case MixerControlType.Mixer:
          return true;
        default:
          return false;
      }
    }

    public bool IsBoolean => MixerControl.IsControlBoolean(this.mixerControl.dwControlType);

    private static bool IsControlListText(MixerControlType controlType)
    {
      switch (controlType)
      {
        case MixerControlType.Equalizer:
        case MixerControlType.SingleSelect:
        case MixerControlType.Mux:
        case MixerControlType.MultipleSelect:
        case MixerControlType.Mixer:
          return true;
        default:
          return false;
      }
    }

    public bool IsListText => MixerControl.IsControlListText(this.mixerControl.dwControlType);

    private static bool IsControlSigned(MixerControlType controlType)
    {
      switch (controlType)
      {
        case MixerControlType.SignedMeter:
        case MixerControlType.PeakMeter:
        case MixerControlType.Signed:
        case MixerControlType.Decibels:
        case MixerControlType.Slider:
        case MixerControlType.Pan:
        case MixerControlType.QSoundPan:
          return true;
        default:
          return false;
      }
    }

    public bool IsSigned => MixerControl.IsControlSigned(this.mixerControl.dwControlType);

    private static bool IsControlUnsigned(MixerControlType controlType)
    {
      switch (controlType)
      {
        case MixerControlType.UnsignedMeter:
        case MixerControlType.Unsigned:
        case MixerControlType.Percent:
        case MixerControlType.Fader:
        case MixerControlType.Volume:
        case MixerControlType.Bass:
        case MixerControlType.Treble:
        case MixerControlType.Equalizer:
        case MixerControlType.MicroTime:
        case MixerControlType.MilliTime:
          return true;
        default:
          return false;
      }
    }

    public bool IsUnsigned => MixerControl.IsControlUnsigned(this.mixerControl.dwControlType);

    private static bool IsControlCustom(MixerControlType controlType)
    {
      return controlType == MixerControlType.Custom;
    }

    public bool IsCustom => MixerControl.IsControlCustom(this.mixerControl.dwControlType);

    public override string ToString()
    {
      return string.Format("{0} {1}", (object) this.Name, (object) this.ControlType);
    }
  }
}
