// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioClient
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using NAudio.Wave;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioClient : IDisposable
  {
    private IAudioClient audioClientInterface;
    private WaveFormat mixFormat;
    private AudioRenderClient audioRenderClient;
    private AudioCaptureClient audioCaptureClient;
    private AudioClockClient audioClockClient;
    private AudioStreamVolume audioStreamVolume;
    private AudioClientShareMode shareMode;

    internal AudioClient(IAudioClient audioClientInterface)
    {
      this.audioClientInterface = audioClientInterface;
    }

    public WaveFormat MixFormat
    {
      get
      {
        if (this.mixFormat == null)
        {
          IntPtr deviceFormatPointer;
          Marshal.ThrowExceptionForHR(this.audioClientInterface.GetMixFormat(out deviceFormatPointer));
          WaveFormat waveFormat = WaveFormat.MarshalFromPtr(deviceFormatPointer);
          Marshal.FreeCoTaskMem(deviceFormatPointer);
          this.mixFormat = waveFormat;
        }
        return this.mixFormat;
      }
    }

    public void Initialize(
      AudioClientShareMode shareMode,
      AudioClientStreamFlags streamFlags,
      long bufferDuration,
      long periodicity,
      WaveFormat waveFormat,
      Guid audioSessionGuid)
    {
      this.shareMode = shareMode;
      Marshal.ThrowExceptionForHR(this.audioClientInterface.Initialize(shareMode, streamFlags, bufferDuration, periodicity, waveFormat, ref audioSessionGuid));
      this.mixFormat = (WaveFormat) null;
    }

    public int BufferSize
    {
      get
      {
        uint bufferSize;
        Marshal.ThrowExceptionForHR(this.audioClientInterface.GetBufferSize(out bufferSize));
        return (int) bufferSize;
      }
    }

    public long StreamLatency => this.audioClientInterface.GetStreamLatency();

    public int CurrentPadding
    {
      get
      {
        int currentPadding;
        Marshal.ThrowExceptionForHR(this.audioClientInterface.GetCurrentPadding(out currentPadding));
        return currentPadding;
      }
    }

    public long DefaultDevicePeriod
    {
      get
      {
        long defaultDevicePeriod;
        Marshal.ThrowExceptionForHR(this.audioClientInterface.GetDevicePeriod(out defaultDevicePeriod, out long _));
        return defaultDevicePeriod;
      }
    }

    public long MinimumDevicePeriod
    {
      get
      {
        long minimumDevicePeriod;
        Marshal.ThrowExceptionForHR(this.audioClientInterface.GetDevicePeriod(out long _, out minimumDevicePeriod));
        return minimumDevicePeriod;
      }
    }

    public AudioStreamVolume AudioStreamVolume
    {
      get
      {
        if (this.shareMode == AudioClientShareMode.Exclusive)
          throw new InvalidOperationException("AudioStreamVolume is ONLY supported for shared audio streams.");
        if (this.audioStreamVolume == null)
        {
          object interfacePointer;
          Marshal.ThrowExceptionForHR(this.audioClientInterface.GetService(new Guid("93014887-242D-4068-8A15-CF5E93B90FE3"), out interfacePointer));
          this.audioStreamVolume = new AudioStreamVolume((IAudioStreamVolume) interfacePointer);
        }
        return this.audioStreamVolume;
      }
    }

    public AudioClockClient AudioClockClient
    {
      get
      {
        if (this.audioClockClient == null)
        {
          object interfacePointer;
          Marshal.ThrowExceptionForHR(this.audioClientInterface.GetService(new Guid("CD63314F-3FBA-4a1b-812C-EF96358728E7"), out interfacePointer));
          this.audioClockClient = new AudioClockClient((IAudioClock) interfacePointer);
        }
        return this.audioClockClient;
      }
    }

    public AudioRenderClient AudioRenderClient
    {
      get
      {
        if (this.audioRenderClient == null)
        {
          object interfacePointer;
          Marshal.ThrowExceptionForHR(this.audioClientInterface.GetService(new Guid("F294ACFC-3146-4483-A7BF-ADDCA7C260E2"), out interfacePointer));
          this.audioRenderClient = new AudioRenderClient((IAudioRenderClient) interfacePointer);
        }
        return this.audioRenderClient;
      }
    }

    public AudioCaptureClient AudioCaptureClient
    {
      get
      {
        if (this.audioCaptureClient == null)
        {
          object interfacePointer;
          Marshal.ThrowExceptionForHR(this.audioClientInterface.GetService(new Guid("c8adbd64-e71e-48a0-a4de-185c395cd317"), out interfacePointer));
          this.audioCaptureClient = new AudioCaptureClient((IAudioCaptureClient) interfacePointer);
        }
        return this.audioCaptureClient;
      }
    }

    public bool IsFormatSupported(AudioClientShareMode shareMode, WaveFormat desiredFormat)
    {
      return this.IsFormatSupported(shareMode, desiredFormat, out WaveFormatExtensible _);
    }

    public bool IsFormatSupported(
      AudioClientShareMode shareMode,
      WaveFormat desiredFormat,
      out WaveFormatExtensible closestMatchFormat)
    {
      int errorCode = this.audioClientInterface.IsFormatSupported(shareMode, desiredFormat, out closestMatchFormat);
      switch (errorCode)
      {
        case -2004287480:
          return false;
        case 0:
          return true;
        case 1:
          return false;
        default:
          Marshal.ThrowExceptionForHR(errorCode);
          throw new NotSupportedException("Unknown hresult " + (object) errorCode);
      }
    }

    public void Start() => this.audioClientInterface.Start();

    public void Stop() => this.audioClientInterface.Stop();

    public void SetEventHandle(IntPtr eventWaitHandle)
    {
      this.audioClientInterface.SetEventHandle(eventWaitHandle);
    }

    public void Reset() => this.audioClientInterface.Reset();

    public void Dispose()
    {
      if (this.audioClientInterface == null)
        return;
      if (this.audioClockClient != null)
      {
        this.audioClockClient.Dispose();
        this.audioClockClient = (AudioClockClient) null;
      }
      if (this.audioRenderClient != null)
      {
        this.audioRenderClient.Dispose();
        this.audioRenderClient = (AudioRenderClient) null;
      }
      if (this.audioCaptureClient != null)
      {
        this.audioCaptureClient.Dispose();
        this.audioCaptureClient = (AudioCaptureClient) null;
      }
      if (this.audioStreamVolume != null)
      {
        this.audioStreamVolume.Dispose();
        this.audioStreamVolume = (AudioStreamVolume) null;
      }
      Marshal.ReleaseComObject((object) this.audioClientInterface);
      this.audioClientInterface = (IAudioClient) null;
      GC.SuppressFinalize((object) this);
    }
  }
}
