// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.BooleanMixerControl
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Mixer
{
  public class BooleanMixerControl : MixerControl
  {
    private MixerInterop.MIXERCONTROLDETAILS_BOOLEAN boolDetails;

    internal BooleanMixerControl(
      MixerInterop.MIXERCONTROL mixerControl,
      IntPtr mixerHandle,
      MixerFlags mixerHandleType,
      int nChannels)
    {
      this.mixerControl = mixerControl;
      this.mixerHandle = mixerHandle;
      this.mixerHandleType = mixerHandleType;
      this.nChannels = nChannels;
      this.mixerControlDetails = new MixerInterop.MIXERCONTROLDETAILS();
      this.GetControlDetails();
    }

    protected override void GetDetails(IntPtr pDetails)
    {
      this.boolDetails = (MixerInterop.MIXERCONTROLDETAILS_BOOLEAN) Marshal.PtrToStructure(pDetails, typeof (MixerInterop.MIXERCONTROLDETAILS_BOOLEAN));
    }

    public bool Value
    {
      get
      {
        this.GetControlDetails();
        return this.boolDetails.fValue == 1;
      }
      set
      {
        this.boolDetails.fValue = value ? 1 : 0;
        this.mixerControlDetails.paDetails = Marshal.AllocHGlobal(Marshal.SizeOf((object) this.boolDetails));
        Marshal.StructureToPtr((object) this.boolDetails, this.mixerControlDetails.paDetails, false);
        MmException.Try(MixerInterop.mixerSetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType), "mixerSetControlDetails");
        Marshal.FreeHGlobal(this.mixerControlDetails.paDetails);
      }
    }
  }
}
