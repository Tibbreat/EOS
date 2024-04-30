// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.SignedMixerControl
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Mixer
{
  public class SignedMixerControl : MixerControl
  {
    private MixerInterop.MIXERCONTROLDETAILS_SIGNED signedDetails;

    internal SignedMixerControl(
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
      this.signedDetails = (MixerInterop.MIXERCONTROLDETAILS_SIGNED) Marshal.PtrToStructure(this.mixerControlDetails.paDetails, typeof (MixerInterop.MIXERCONTROLDETAILS_SIGNED));
    }

    public int Value
    {
      get
      {
        this.GetControlDetails();
        return this.signedDetails.lValue;
      }
      set
      {
        this.signedDetails.lValue = value;
        this.mixerControlDetails.paDetails = Marshal.AllocHGlobal(Marshal.SizeOf((object) this.signedDetails));
        Marshal.StructureToPtr((object) this.signedDetails, this.mixerControlDetails.paDetails, false);
        MmException.Try(MixerInterop.mixerSetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType), "mixerSetControlDetails");
        Marshal.FreeHGlobal(this.mixerControlDetails.paDetails);
      }
    }

    public int MinValue => this.mixerControl.Bounds.minimum;

    public int MaxValue => this.mixerControl.Bounds.maximum;

    public double Percent
    {
      get
      {
        return 100.0 * (double) (this.Value - this.MinValue) / (double) (this.MaxValue - this.MinValue);
      }
      set
      {
        this.Value = (int) ((double) this.MinValue + value / 100.0 * (double) (this.MaxValue - this.MinValue));
      }
    }

    public override string ToString()
    {
      return string.Format("{0} {1}%", (object) base.ToString(), (object) this.Percent);
    }
  }
}
