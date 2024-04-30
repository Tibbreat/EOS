// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioVolumeNotificationData
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioVolumeNotificationData
  {
    private readonly Guid eventContext;
    private readonly bool muted;
    private readonly float masterVolume;
    private readonly int channels;
    private readonly float[] channelVolume;

    public Guid EventContext => this.eventContext;

    public bool Muted => this.muted;

    public float MasterVolume => this.masterVolume;

    public int Channels => this.channels;

    public float[] ChannelVolume => this.channelVolume;

    public AudioVolumeNotificationData(
      Guid eventContext,
      bool muted,
      float masterVolume,
      float[] channelVolume)
    {
      this.eventContext = eventContext;
      this.muted = muted;
      this.masterVolume = masterVolume;
      this.channels = channelVolume.Length;
      this.channelVolume = channelVolume;
    }
  }
}
