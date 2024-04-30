// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioSessionNotification
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System.Runtime.CompilerServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  internal class AudioSessionNotification : IAudioSessionNotification
  {
    private AudioSessionManager parent;

    internal AudioSessionNotification(AudioSessionManager parent) => this.parent = parent;

    [MethodImpl(MethodImplOptions.PreserveSig)]
    public int OnSessionCreated(IAudioSessionControl newSession)
    {
      this.parent.FireSessionCreated(newSession);
      return 0;
    }
  }
}
