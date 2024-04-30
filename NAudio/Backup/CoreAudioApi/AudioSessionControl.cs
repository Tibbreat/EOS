// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioSessionControl
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioSessionControl : IDisposable
  {
    private readonly IAudioSessionControl audioSessionControlInterface;
    private readonly IAudioSessionControl2 audioSessionControlInterface2;
    private AudioSessionEventsCallback audioSessionEventCallback;
    internal AudioMeterInformation audioMeterInformation;
    internal SimpleAudioVolume simpleAudioVolume;

    public AudioSessionControl(IAudioSessionControl audioSessionControl)
    {
      this.audioSessionControlInterface = audioSessionControl;
      this.audioSessionControlInterface2 = audioSessionControl as IAudioSessionControl2;
      IAudioMeterInformation controlInterface1 = this.audioSessionControlInterface as IAudioMeterInformation;
      ISimpleAudioVolume controlInterface2 = this.audioSessionControlInterface as ISimpleAudioVolume;
      if (controlInterface1 != null)
        this.audioMeterInformation = new AudioMeterInformation(controlInterface1);
      if (controlInterface2 == null)
        return;
      this.simpleAudioVolume = new SimpleAudioVolume(controlInterface2);
    }

    public void Dispose()
    {
      if (this.audioSessionEventCallback != null)
        Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.UnregisterAudioSessionNotification((IAudioSessionEvents) this.audioSessionEventCallback));
      GC.SuppressFinalize((object) this);
    }

    ~AudioSessionControl() => this.Dispose();

    public AudioMeterInformation AudioMeterInformation => this.audioMeterInformation;

    public SimpleAudioVolume SimpleAudioVolume => this.simpleAudioVolume;

    public AudioSessionState State
    {
      get
      {
        AudioSessionState state;
        Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetState(out state));
        return state;
      }
    }

    public string DisplayName
    {
      get
      {
        string displayName = string.Empty;
        Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetDisplayName(out displayName));
        return displayName;
      }
      set
      {
        if (!(value != string.Empty))
          return;
        Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.SetDisplayName(value, Guid.Empty));
      }
    }

    public string IconPath
    {
      get
      {
        string iconPath = string.Empty;
        Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetIconPath(out iconPath));
        return iconPath;
      }
      set
      {
        if (!(value != string.Empty))
          return;
        Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.SetIconPath(value, Guid.Empty));
      }
    }

    public string GetSessionIdentifier
    {
      get
      {
        if (this.audioSessionControlInterface2 == null)
          throw new InvalidOperationException("Not supported on this version of Windows");
        string retVal;
        Marshal.ThrowExceptionForHR(this.audioSessionControlInterface2.GetSessionIdentifier(out retVal));
        return retVal;
      }
    }

    public string GetSessionInstanceIdentifier
    {
      get
      {
        if (this.audioSessionControlInterface2 == null)
          throw new InvalidOperationException("Not supported on this version of Windows");
        string retVal;
        Marshal.ThrowExceptionForHR(this.audioSessionControlInterface2.GetSessionInstanceIdentifier(out retVal));
        return retVal;
      }
    }

    public uint GetProcessID
    {
      get
      {
        if (this.audioSessionControlInterface2 == null)
          throw new InvalidOperationException("Not supported on this version of Windows");
        uint retVal;
        Marshal.ThrowExceptionForHR(this.audioSessionControlInterface2.GetProcessId(out retVal));
        return retVal;
      }
    }

    public bool IsSystemSoundsSession
    {
      get
      {
        if (this.audioSessionControlInterface2 == null)
          throw new InvalidOperationException("Not supported on this version of Windows");
        return this.audioSessionControlInterface2.IsSystemSoundsSession() == 0;
      }
    }

    public Guid GetGroupingParam()
    {
      Guid groupingId = Guid.Empty;
      Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.GetGroupingParam(out groupingId));
      return groupingId;
    }

    public void SetGroupingParam(Guid groupingId, Guid context)
    {
      Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.SetGroupingParam(groupingId, context));
    }

    public void RegisterEventClient(IAudioSessionEventsHandler eventClient)
    {
      this.audioSessionEventCallback = new AudioSessionEventsCallback(eventClient);
      Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.RegisterAudioSessionNotification((IAudioSessionEvents) this.audioSessionEventCallback));
    }

    public void UnRegisterEventClient(IAudioSessionEventsHandler eventClient)
    {
      if (this.audioSessionEventCallback == null)
        return;
      Marshal.ThrowExceptionForHR(this.audioSessionControlInterface.UnregisterAudioSessionNotification((IAudioSessionEvents) this.audioSessionEventCallback));
    }
  }
}
