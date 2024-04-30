// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.UnsignedMixerControl
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Mixer
{
  public class UnsignedMixerControl : MixerControl
  {
    private MixerInterop.MIXERCONTROLDETAILS_UNSIGNED[] unsignedDetails;

    internal UnsignedMixerControl(
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
      this.unsignedDetails = new MixerInterop.MIXERCONTROLDETAILS_UNSIGNED[this.nChannels];
      for (int index = 0; index < this.nChannels; ++index)
        this.unsignedDetails[index] = (MixerInterop.MIXERCONTROLDETAILS_UNSIGNED) Marshal.PtrToStructure(this.mixerControlDetails.paDetails, typeof (MixerInterop.MIXERCONTROLDETAILS_UNSIGNED));
    }

    public uint Value
    {
      get
      {
        this.GetControlDetails();
        return this.unsignedDetails[0].dwValue;
      }
      set
      {
        int num = Marshal.SizeOf((object) this.unsignedDetails[0]);
        this.mixerControlDetails.paDetails = Marshal.AllocHGlobal(num * this.nChannels);
        for (int index = 0; index < this.nChannels; ++index)
        {
          this.unsignedDetails[index].dwValue = value;
          long ptr = this.mixerControlDetails.paDetails.ToInt64() + (long) (num * index);
          Marshal.StructureToPtr((object) this.unsignedDetails[index], (IntPtr) ptr, false);
        }
        MmException.Try(MixerInterop.mixerSetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType), "mixerSetControlDetails");
        Marshal.FreeHGlobal(this.mixerControlDetails.paDetails);
      }
    }

    public uint MinValue => (uint) this.mixerControl.Bounds.minimum;

    public uint MaxValue => (uint) this.mixerControl.Bounds.maximum;

    public double Percent
    {
      get
      {
        return 100.0 * (double) (this.Value - this.MinValue) / (double) (this.MaxValue - this.MinValue);
      }
      set
      {
        this.Value = (uint) ((double) this.MinValue + value / 100.0 * (double) (this.MaxValue - this.MinValue));
      }
    }

    public override string ToString()
    {
      return string.Format("{0} {1}%", (object) base.ToString(), (object) this.Percent);
    }
  }
}
