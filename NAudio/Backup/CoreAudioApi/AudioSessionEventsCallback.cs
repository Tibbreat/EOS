// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioSessionEventsCallback
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioSessionEventsCallback : IAudioSessionEvents
  {
    private readonly IAudioSessionEventsHandler audioSessionEventsHandler;

    public AudioSessionEventsCallback(IAudioSessionEventsHandler handler)
    {
      this.audioSessionEventsHandler = handler;
    }

    public int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr), In] string displayName, [In] ref Guid eventContext)
    {
      this.audioSessionEventsHandler.OnDisplayNameChanged(displayName);
      return 0;
    }

    public int OnIconPathChanged([MarshalAs(UnmanagedType.LPWStr), In] string iconPath, [In] ref Guid eventContext)
    {
      this.audioSessionEventsHandler.OnIconPathChanged(iconPath);
      return 0;
    }

    public int OnSimpleVolumeChanged([MarshalAs(UnmanagedType.R4), In] float volume, [MarshalAs(UnmanagedType.Bool), In] bool isMuted, [In] ref Guid eventContext)
    {
      this.audioSessionEventsHandler.OnVolumeChanged(volume, isMuted);
      return 0;
    }

    public int OnChannelVolumeChanged(
      [MarshalAs(UnmanagedType.U4), In] uint channelCount,
      [MarshalAs(UnmanagedType.SysInt), In] IntPtr newVolumes,
      [MarshalAs(UnmanagedType.U4), In] uint channelIndex,
      [In] ref Guid eventContext)
    {
      this.audioSessionEventsHandler.OnChannelVolumeChanged(channelCount, newVolumes, channelIndex);
      return 0;
    }

    public int OnGroupingParamChanged([In] ref Guid groupingId, [In] ref Guid eventContext)
    {
      this.audioSessionEventsHandler.OnGroupingParamChanged(ref groupingId);
      return 0;
    }

    public int OnStateChanged([In] AudioSessionState state)
    {
      this.audioSessionEventsHandler.OnStateChanged(state);
      return 0;
    }

    public int OnSessionDisconnected([In] AudioSessionDisconnectReason disconnectReason)
    {
      this.audioSessionEventsHandler.OnSessionDisconnected(disconnectReason);
      return 0;
    }
  }
}
