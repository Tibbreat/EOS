// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.AudioVolumeNotificationDataStruct
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  internal struct AudioVolumeNotificationDataStruct
  {
    public Guid guidEventContext;
    public bool bMuted;
    public float fMasterVolume;
    public uint nChannels;
    public float ChannelVolume;

    private void FixCS0649()
    {
      this.guidEventContext = Guid.Empty;
      this.bMuted = false;
      this.fMasterVolume = 0.0f;
      this.nChannels = 0U;
      this.ChannelVolume = 0.0f;
    }
  }
}
