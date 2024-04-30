// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioEndpointVolumeVolumeRange
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioEndpointVolumeVolumeRange
  {
    private readonly float volumeMinDecibels;
    private readonly float volumeMaxDecibels;
    private readonly float volumeIncrementDecibels;

    internal AudioEndpointVolumeVolumeRange(IAudioEndpointVolume parent)
    {
      Marshal.ThrowExceptionForHR(parent.GetVolumeRange(out this.volumeMinDecibels, out this.volumeMaxDecibels, out this.volumeIncrementDecibels));
    }

    public float MinDecibels => this.volumeMinDecibels;

    public float MaxDecibels => this.volumeMaxDecibels;

    public float IncrementDecibels => this.volumeIncrementDecibels;
  }
}
