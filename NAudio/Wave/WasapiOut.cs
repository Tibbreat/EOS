// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WasapiOut
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi;
using System;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace NAudio.Wave
{
  public class WasapiOut : IWavePlayer, IDisposable, IWavePosition
  {
    private AudioClient audioClient;
    private readonly MMDevice mmDevice;
    private readonly AudioClientShareMode shareMode;
    private AudioRenderClient renderClient;
    private IWaveProvider sourceProvider;
    private int latencyMilliseconds;
    private int bufferFrameCount;
    private int bytesPerFrame;
    private readonly bool isUsingEventSync;
    private EventWaitHandle frameEventWaitHandle;
    private byte[] readBuffer;
    private volatile PlaybackState playbackState;
    private Thread playThread;
    private WaveFormat outputFormat;
    private bool dmoResamplerNeeded;
    private readonly SynchronizationContext syncContext;

    public event EventHandler<StoppedEventArgs> PlaybackStopped;

    public WasapiOut(AudioClientShareMode shareMode, int latency)
      : this(WasapiOut.GetDefaultAudioEndpoint(), shareMode, true, latency)
    {
    }

    public WasapiOut(AudioClientShareMode shareMode, bool useEventSync, int latency)
      : this(WasapiOut.GetDefaultAudioEndpoint(), shareMode, useEventSync, latency)
    {
    }

    public WasapiOut(
      MMDevice device,
      AudioClientShareMode shareMode,
      bool useEventSync,
      int latency)
    {
      this.audioClient = device.AudioClient;
      this.mmDevice = device;
      this.shareMode = shareMode;
      this.isUsingEventSync = useEventSync;
      this.latencyMilliseconds = latency;
      this.syncContext = SynchronizationContext.Current;
    }

    private static MMDevice GetDefaultAudioEndpoint()
    {
      if (Environment.OSVersion.Version.Major < 6)
        throw new NotSupportedException("WASAPI supported only on Windows Vista and above");
      return new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
    }

    private void PlayThread()
    {
      ResamplerDmoStream resamplerDmoStream = (ResamplerDmoStream) null;
      IWaveProvider playbackProvider = this.sourceProvider;
      Exception e = (Exception) null;
      try
      {
        if (this.dmoResamplerNeeded)
        {
          resamplerDmoStream = new ResamplerDmoStream(this.sourceProvider, this.outputFormat);
          playbackProvider = (IWaveProvider) resamplerDmoStream;
        }
        this.bufferFrameCount = this.audioClient.BufferSize;
        this.bytesPerFrame = this.outputFormat.Channels * this.outputFormat.BitsPerSample / 8;
        this.readBuffer = new byte[this.bufferFrameCount * this.bytesPerFrame];
        this.FillBuffer(playbackProvider, this.bufferFrameCount);
        WaitHandle[] waitHandles = new WaitHandle[1]
        {
          (WaitHandle) this.frameEventWaitHandle
        };
        this.audioClient.Start();
        while (this.playbackState != PlaybackState.Stopped)
        {
          int num = 0;
          if (this.isUsingEventSync)
            num = WaitHandle.WaitAny(waitHandles, 3 * this.latencyMilliseconds, false);
          else
            Thread.Sleep(this.latencyMilliseconds / 2);
          if (this.playbackState == PlaybackState.Playing && num != 258)
          {
            int frameCount = this.bufferFrameCount - (!this.isUsingEventSync ? this.audioClient.CurrentPadding : (this.shareMode == AudioClientShareMode.Shared ? this.audioClient.CurrentPadding : 0));
            if (frameCount > 10)
              this.FillBuffer(playbackProvider, frameCount);
          }
        }
        Thread.Sleep(this.latencyMilliseconds / 2);
        this.audioClient.Stop();
        if (this.playbackState != PlaybackState.Stopped)
          return;
        this.audioClient.Reset();
      }
      catch (Exception ex)
      {
        e = ex;
      }
      finally
      {
        resamplerDmoStream?.Dispose();
        this.RaisePlaybackStopped(e);
      }
    }

    private void RaisePlaybackStopped(Exception e)
    {
      EventHandler<StoppedEventArgs> handler = this.PlaybackStopped;
      if (handler == null)
        return;
      if (this.syncContext == null)
        handler((object) this, new StoppedEventArgs(e));
      else
        this.syncContext.Post((SendOrPostCallback) (state => handler((object) this, new StoppedEventArgs(e))), (object) null);
    }

    private void FillBuffer(IWaveProvider playbackProvider, int frameCount)
    {
      IntPtr buffer = this.renderClient.GetBuffer(frameCount);
      int count = frameCount * this.bytesPerFrame;
      int length = playbackProvider.Read(this.readBuffer, 0, count);
      if (length == 0)
        this.playbackState = PlaybackState.Stopped;
      Marshal.Copy(this.readBuffer, 0, buffer, length);
      this.renderClient.ReleaseBuffer(length / this.bytesPerFrame, AudioClientBufferFlags.None);
    }

    public long GetPosition()
    {
      return this.playbackState == PlaybackState.Stopped ? 0L : (long) this.audioClient.AudioClockClient.AdjustedPosition;
    }

    public WaveFormat OutputWaveFormat => this.outputFormat;

    public void Play()
    {
      if (this.playbackState == PlaybackState.Playing)
        return;
      if (this.playbackState == PlaybackState.Stopped)
      {
        this.playThread = new Thread(new ThreadStart(this.PlayThread));
        this.playbackState = PlaybackState.Playing;
        this.playThread.Start();
      }
      else
        this.playbackState = PlaybackState.Playing;
    }

    public void Stop()
    {
      if (this.playbackState == PlaybackState.Stopped)
        return;
      this.playbackState = PlaybackState.Stopped;
      this.playThread.Join();
      this.playThread = (Thread) null;
    }

    public void Pause()
    {
      if (this.playbackState != PlaybackState.Playing)
        return;
      this.playbackState = PlaybackState.Paused;
    }

    public void Init(IWaveProvider waveProvider)
    {
      long num = (long) (this.latencyMilliseconds * 10000);
      this.outputFormat = waveProvider.WaveFormat;
      WaveFormatExtensible closestMatchFormat;
      if (!this.audioClient.IsFormatSupported(this.shareMode, this.outputFormat, out closestMatchFormat))
      {
        if (closestMatchFormat == null)
        {
          WaveFormat desiredFormat = this.audioClient.MixFormat;
          if (!this.audioClient.IsFormatSupported(this.shareMode, desiredFormat))
          {
            WaveFormatExtensible[] formatExtensibleArray = new WaveFormatExtensible[3]
            {
              new WaveFormatExtensible(this.outputFormat.SampleRate, 32, this.outputFormat.Channels),
              new WaveFormatExtensible(this.outputFormat.SampleRate, 24, this.outputFormat.Channels),
              new WaveFormatExtensible(this.outputFormat.SampleRate, 16, this.outputFormat.Channels)
            };
            foreach (WaveFormat waveFormat in formatExtensibleArray)
            {
              desiredFormat = waveFormat;
              if (!this.audioClient.IsFormatSupported(this.shareMode, desiredFormat))
                desiredFormat = (WaveFormat) null;
              else
                break;
            }
            if (desiredFormat == null)
            {
              desiredFormat = (WaveFormat) new WaveFormatExtensible(this.outputFormat.SampleRate, 16, 2);
              if (!this.audioClient.IsFormatSupported(this.shareMode, desiredFormat))
                throw new NotSupportedException("Can't find a supported format to use");
            }
          }
          this.outputFormat = desiredFormat;
        }
        else
          this.outputFormat = (WaveFormat) closestMatchFormat;
        using (new ResamplerDmoStream(waveProvider, this.outputFormat))
          ;
        this.dmoResamplerNeeded = true;
      }
      else
        this.dmoResamplerNeeded = false;
      this.sourceProvider = waveProvider;
      if (this.isUsingEventSync)
      {
        if (this.shareMode == AudioClientShareMode.Shared)
        {
          this.audioClient.Initialize(this.shareMode, AudioClientStreamFlags.EventCallback, 0L, 0L, this.outputFormat, Guid.Empty);
          this.latencyMilliseconds = (int) (this.audioClient.StreamLatency / 10000L);
        }
        else
          this.audioClient.Initialize(this.shareMode, AudioClientStreamFlags.EventCallback, num, num, this.outputFormat, Guid.Empty);
        this.frameEventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        this.audioClient.SetEventHandle(this.frameEventWaitHandle.SafeWaitHandle.DangerousGetHandle());
      }
      else
        this.audioClient.Initialize(this.shareMode, AudioClientStreamFlags.None, num, 0L, this.outputFormat, Guid.Empty);
      this.renderClient = this.audioClient.AudioRenderClient;
    }

    public PlaybackState PlaybackState => this.playbackState;

    public float Volume
    {
      get => this.mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
      set
      {
        if ((double) value < 0.0)
          throw new ArgumentOutOfRangeException(nameof (value), "Volume must be between 0.0 and 1.0");
        this.mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar = (double) value <= 1.0 ? value : throw new ArgumentOutOfRangeException(nameof (value), "Volume must be between 0.0 and 1.0");
      }
    }

    public AudioStreamVolume AudioStreamVolume
    {
      get
      {
        if (this.shareMode == AudioClientShareMode.Exclusive)
          throw new InvalidOperationException("AudioStreamVolume is ONLY supported for shared audio streams.");
        return this.audioClient.AudioStreamVolume;
      }
    }

    public void Dispose()
    {
      if (this.audioClient == null)
        return;
      this.Stop();
      this.audioClient.Dispose();
      this.audioClient = (AudioClient) null;
      this.renderClient = (AudioRenderClient) null;
    }
  }
}
