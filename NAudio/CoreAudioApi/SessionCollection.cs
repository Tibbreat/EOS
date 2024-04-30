// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.SessionCollection
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class SessionCollection
  {
    private readonly IAudioSessionEnumerator audioSessionEnumerator;

    internal SessionCollection(IAudioSessionEnumerator realEnumerator)
    {
      this.audioSessionEnumerator = realEnumerator;
    }

    public AudioSessionControl this[int index]
    {
      get
      {
        IAudioSessionControl session;
        Marshal.ThrowExceptionForHR(this.audioSessionEnumerator.GetSession(index, out session));
        return new AudioSessionControl(session);
      }
    }

    public int Count
    {
      get
      {
        int sessionCount;
        Marshal.ThrowExceptionForHR(this.audioSessionEnumerator.GetCount(out sessionCount));
        return sessionCount;
      }
    }
  }
}
