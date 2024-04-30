// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioSessionEventsHandler
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  public interface IAudioSessionEventsHandler
  {
    void OnVolumeChanged(float volume, bool isMuted);

    void OnDisplayNameChanged(string displayName);

    void OnIconPathChanged(string iconPath);

    void OnChannelVolumeChanged(uint channelCount, IntPtr newVolumes, uint channelIndex);

    void OnGroupingParamChanged(ref Guid groupingId);

    void OnStateChanged(AudioSessionState state);

    void OnSessionDisconnected(AudioSessionDisconnectReason disconnectReason);
  }
}
