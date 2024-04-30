// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WasapiLoopbackCapture
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi;
using System;

#nullable disable
namespace NAudio.Wave
{
  public class WasapiLoopbackCapture : WasapiCapture
  {
    public WasapiLoopbackCapture()
      : this(WasapiLoopbackCapture.GetDefaultLoopbackCaptureDevice())
    {
    }

    public WasapiLoopbackCapture(MMDevice captureDevice)
      : base(captureDevice)
    {
    }

    public static MMDevice GetDefaultLoopbackCaptureDevice()
    {
      return new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
    }

    public override WaveFormat WaveFormat
    {
      get => base.WaveFormat;
      set
      {
        throw new InvalidOperationException("WaveFormat cannot be set for WASAPI Loopback Capture");
      }
    }

    protected override AudioClientStreamFlags GetAudioClientStreamFlags()
    {
      return AudioClientStreamFlags.Loopback;
    }
  }
}
