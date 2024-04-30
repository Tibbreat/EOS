// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.IWavePlayer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave
{
  public interface IWavePlayer : IDisposable
  {
    void Play();

    void Stop();

    void Pause();

    void Init(IWaveProvider waveProvider);

    PlaybackState PlaybackState { get; }

    [Obsolete("Not intending to keep supporting this going forward: set the volume on your input WaveProvider instead")]
    float Volume { get; set; }

    event EventHandler<StoppedEventArgs> PlaybackStopped;
  }
}
