// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioSessionManager
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioSessionManager
  {
    private readonly IAudioSessionManager audioSessionInterface;
    private readonly IAudioSessionManager2 audioSessionInterface2;
    private AudioSessionNotification audioSessionNotification;
    private SessionCollection sessions;
    private SimpleAudioVolume simpleAudioVolume;
    private AudioSessionControl audioSessionControl;

    public event AudioSessionManager.SessionCreatedDelegate OnSessionCreated;

    internal AudioSessionManager(IAudioSessionManager audioSessionManager)
    {
      this.audioSessionInterface = audioSessionManager;
      this.audioSessionInterface2 = audioSessionManager as IAudioSessionManager2;
      this.RefreshSessions();
    }

    public SimpleAudioVolume SimpleAudioVolume
    {
      get
      {
        if (this.simpleAudioVolume == null)
        {
          ISimpleAudioVolume audioVolume;
          this.audioSessionInterface.GetSimpleAudioVolume(Guid.Empty, 0U, out audioVolume);
          this.simpleAudioVolume = new SimpleAudioVolume(audioVolume);
        }
        return this.simpleAudioVolume;
      }
    }

    public AudioSessionControl AudioSessionControl
    {
      get
      {
        if (this.audioSessionControl == null)
        {
          IAudioSessionControl sessionControl;
          this.audioSessionInterface.GetAudioSessionControl(Guid.Empty, 0U, out sessionControl);
          this.audioSessionControl = new AudioSessionControl(sessionControl);
        }
        return this.audioSessionControl;
      }
    }

    internal void FireSessionCreated(IAudioSessionControl newSession)
    {
      if (this.OnSessionCreated == null)
        return;
      this.OnSessionCreated((object) this, newSession);
    }

    public void RefreshSessions()
    {
      this.UnregisterNotifications();
      if (this.audioSessionInterface2 == null)
        return;
      IAudioSessionEnumerator sessionEnum;
      Marshal.ThrowExceptionForHR(this.audioSessionInterface2.GetSessionEnumerator(out sessionEnum));
      this.sessions = new SessionCollection(sessionEnum);
      this.audioSessionNotification = new AudioSessionNotification(this);
      Marshal.ThrowExceptionForHR(this.audioSessionInterface2.RegisterSessionNotification((IAudioSessionNotification) this.audioSessionNotification));
    }

    public SessionCollection Sessions => this.sessions;

    public void Dispose()
    {
      this.UnregisterNotifications();
      GC.SuppressFinalize((object) this);
    }

    private void UnregisterNotifications()
    {
      if (this.sessions != null)
        this.sessions = (SessionCollection) null;
      if (this.audioSessionNotification == null)
        return;
      Marshal.ThrowExceptionForHR(this.audioSessionInterface2.UnregisterSessionNotification((IAudioSessionNotification) this.audioSessionNotification));
    }

    ~AudioSessionManager() => this.Dispose();

    public delegate void SessionCreatedDelegate(object sender, IAudioSessionControl newSession);
  }
}
