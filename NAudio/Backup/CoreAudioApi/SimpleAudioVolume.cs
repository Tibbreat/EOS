// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.SimpleAudioVolume
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class SimpleAudioVolume : IDisposable
  {
    private readonly ISimpleAudioVolume simpleAudioVolume;

    internal SimpleAudioVolume(ISimpleAudioVolume realSimpleVolume)
    {
      this.simpleAudioVolume = realSimpleVolume;
    }

    public void Dispose() => GC.SuppressFinalize((object) this);

    ~SimpleAudioVolume() => this.Dispose();

    public float Volume
    {
      get
      {
        float levelNorm;
        Marshal.ThrowExceptionForHR(this.simpleAudioVolume.GetMasterVolume(out levelNorm));
        return levelNorm;
      }
      set
      {
        if ((double) value < 0.0 || (double) value > 1.0)
          return;
        Marshal.ThrowExceptionForHR(this.simpleAudioVolume.SetMasterVolume(value, Guid.Empty));
      }
    }

    public bool Mute
    {
      get
      {
        bool isMuted;
        Marshal.ThrowExceptionForHR(this.simpleAudioVolume.GetMute(out isMuted));
        return isMuted;
      }
      set => Marshal.ThrowExceptionForHR(this.simpleAudioVolume.SetMute(value, Guid.Empty));
    }
  }
}
