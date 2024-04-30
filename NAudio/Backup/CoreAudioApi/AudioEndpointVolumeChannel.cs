// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioEndpointVolumeChannel
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioEndpointVolumeChannel
  {
    private readonly uint channel;
    private readonly IAudioEndpointVolume audioEndpointVolume;

    internal AudioEndpointVolumeChannel(IAudioEndpointVolume parent, int channel)
    {
      this.channel = (uint) channel;
      this.audioEndpointVolume = parent;
    }

    public float VolumeLevel
    {
      get
      {
        float pfLevelDB;
        Marshal.ThrowExceptionForHR(this.audioEndpointVolume.GetChannelVolumeLevel(this.channel, out pfLevelDB));
        return pfLevelDB;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this.audioEndpointVolume.SetChannelVolumeLevel(this.channel, value, Guid.Empty));
      }
    }

    public float VolumeLevelScalar
    {
      get
      {
        float pfLevel;
        Marshal.ThrowExceptionForHR(this.audioEndpointVolume.GetChannelVolumeLevelScalar(this.channel, out pfLevel));
        return pfLevel;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this.audioEndpointVolume.SetChannelVolumeLevelScalar(this.channel, value, Guid.Empty));
      }
    }
  }
}
