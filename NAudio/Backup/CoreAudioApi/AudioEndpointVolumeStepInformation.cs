// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioEndpointVolumeStepInformation
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioEndpointVolumeStepInformation
  {
    private readonly uint step;
    private readonly uint stepCount;

    internal AudioEndpointVolumeStepInformation(IAudioEndpointVolume parent)
    {
      Marshal.ThrowExceptionForHR(parent.GetVolumeStepInfo(out this.step, out this.stepCount));
    }

    public uint Step => this.step;

    public uint StepCount => this.stepCount;
  }
}
