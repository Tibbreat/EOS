// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioEndpointVolume
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioEndpointVolume : IDisposable
  {
    private readonly IAudioEndpointVolume audioEndPointVolume;
    private readonly AudioEndpointVolumeChannels channels;
    private readonly AudioEndpointVolumeStepInformation stepInformation;
    private readonly AudioEndpointVolumeVolumeRange volumeRange;
    private readonly EEndpointHardwareSupport hardwareSupport;
    private AudioEndpointVolumeCallback callBack;

    public event AudioEndpointVolumeNotificationDelegate OnVolumeNotification;

    public AudioEndpointVolumeVolumeRange VolumeRange => this.volumeRange;

    public EEndpointHardwareSupport HardwareSupport => this.hardwareSupport;

    public AudioEndpointVolumeStepInformation StepInformation => this.stepInformation;

    public AudioEndpointVolumeChannels Channels => this.channels;

    public float MasterVolumeLevel
    {
      get
      {
        float pfLevelDB;
        Marshal.ThrowExceptionForHR(this.audioEndPointVolume.GetMasterVolumeLevel(out pfLevelDB));
        return pfLevelDB;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this.audioEndPointVolume.SetMasterVolumeLevel(value, Guid.Empty));
      }
    }

    public float MasterVolumeLevelScalar
    {
      get
      {
        float pfLevel;
        Marshal.ThrowExceptionForHR(this.audioEndPointVolume.GetMasterVolumeLevelScalar(out pfLevel));
        return pfLevel;
      }
      set
      {
        Marshal.ThrowExceptionForHR(this.audioEndPointVolume.SetMasterVolumeLevelScalar(value, Guid.Empty));
      }
    }

    public bool Mute
    {
      get
      {
        bool pbMute;
        Marshal.ThrowExceptionForHR(this.audioEndPointVolume.GetMute(out pbMute));
        return pbMute;
      }
      set => Marshal.ThrowExceptionForHR(this.audioEndPointVolume.SetMute(value, Guid.Empty));
    }

    public void VolumeStepUp()
    {
      Marshal.ThrowExceptionForHR(this.audioEndPointVolume.VolumeStepUp(Guid.Empty));
    }

    public void VolumeStepDown()
    {
      Marshal.ThrowExceptionForHR(this.audioEndPointVolume.VolumeStepDown(Guid.Empty));
    }

    internal AudioEndpointVolume(IAudioEndpointVolume realEndpointVolume)
    {
      this.audioEndPointVolume = realEndpointVolume;
      this.channels = new AudioEndpointVolumeChannels(this.audioEndPointVolume);
      this.stepInformation = new AudioEndpointVolumeStepInformation(this.audioEndPointVolume);
      uint pdwHardwareSupportMask;
      Marshal.ThrowExceptionForHR(this.audioEndPointVolume.QueryHardwareSupport(out pdwHardwareSupportMask));
      this.hardwareSupport = (EEndpointHardwareSupport) pdwHardwareSupportMask;
      this.volumeRange = new AudioEndpointVolumeVolumeRange(this.audioEndPointVolume);
      this.callBack = new AudioEndpointVolumeCallback(this);
      Marshal.ThrowExceptionForHR(this.audioEndPointVolume.RegisterControlChangeNotify((IAudioEndpointVolumeCallback) this.callBack));
    }

    internal void FireNotification(AudioVolumeNotificationData notificationData)
    {
      AudioEndpointVolumeNotificationDelegate volumeNotification = this.OnVolumeNotification;
      if (volumeNotification == null)
        return;
      volumeNotification(notificationData);
    }

    public void Dispose()
    {
      if (this.callBack != null)
      {
        Marshal.ThrowExceptionForHR(this.audioEndPointVolume.UnregisterControlChangeNotify((IAudioEndpointVolumeCallback) this.callBack));
        this.callBack = (AudioEndpointVolumeCallback) null;
      }
      GC.SuppressFinalize((object) this);
    }

    ~AudioEndpointVolume() => this.Dispose();
  }
}
