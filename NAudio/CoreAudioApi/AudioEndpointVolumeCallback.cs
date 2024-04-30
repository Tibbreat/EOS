// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioEndpointVolumeCallback
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  internal class AudioEndpointVolumeCallback : IAudioEndpointVolumeCallback
  {
    private readonly AudioEndpointVolume parent;

    internal AudioEndpointVolumeCallback(AudioEndpointVolume parent) => this.parent = parent;

    public void OnNotify(IntPtr notifyData)
    {
      AudioVolumeNotificationDataStruct structure = (AudioVolumeNotificationDataStruct) Marshal.PtrToStructure(notifyData, typeof (AudioVolumeNotificationDataStruct));
      IntPtr num = Marshal.OffsetOf(typeof (AudioVolumeNotificationDataStruct), "ChannelVolume");
      IntPtr ptr = (IntPtr) ((long) notifyData + (long) num);
      float[] channelVolume = new float[(IntPtr) structure.nChannels];
      for (int index = 0; (long) index < (long) structure.nChannels; ++index)
        channelVolume[index] = (float) Marshal.PtrToStructure(ptr, typeof (float));
      this.parent.FireNotification(new AudioVolumeNotificationData(structure.guidEventContext, structure.bMuted, structure.fMasterVolume, channelVolume));
    }
  }
}
