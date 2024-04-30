// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.CustomMixerControl
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Mixer
{
  public class CustomMixerControl : MixerControl
  {
    internal CustomMixerControl(
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
    }
  }
}
