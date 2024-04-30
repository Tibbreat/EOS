// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveOut
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace NAudio.Wave
{
  public class WaveOut : IWavePlayer, IDisposable, IWavePosition
  {
    private IntPtr hWaveOut;
    private WaveOutBuffer[] buffers;
    private IWaveProvider waveStream;
    private volatile PlaybackState playbackState;
    private WaveInterop.WaveCallback callback;
    private float volume = 1f;
    private WaveCallbackInfo callbackInfo;
    private object waveOutLock;
    private int queuedBuffers;
    private SynchronizationContext syncContext;

    public event EventHandler<StoppedEventArgs> PlaybackStopped;

    public static WaveOutCapabilities GetCapabilities(int devNumber)
    {
      WaveOutCapabilities waveOutCaps = new WaveOutCapabilities();
      int waveOutCapsSize = Marshal.SizeOf((object) waveOutCaps);
      MmException.Try(WaveInterop.waveOutGetDevCaps((IntPtr) devNumber, out waveOutCaps, waveOutCapsSize), "waveOutGetDevCaps");
      return waveOutCaps;
    }

    public static int DeviceCount => WaveInterop.waveOutGetNumDevs();

    public int DesiredLatency { get; set; }

    public int NumberOfBuffers { get; set; }

    public int DeviceNumber { get; set; }

    public WaveOut()
      : this(SynchronizationContext.Current == null ? WaveCallbackInfo.FunctionCallback() : WaveCallbackInfo.NewWindow())
    {
    }

    public WaveOut(IntPtr windowHandle)
      : this(WaveCallbackInfo.ExistingWindow(windowHandle))
    {
    }

    public WaveOut(WaveCallbackInfo callbackInfo)
    {
      this.syncContext = SynchronizationContext.Current;
      this.DeviceNumber = 0;
      this.DesiredLatency = 300;
      this.NumberOfBuffers = 2;
      this.callback = new WaveInterop.WaveCallback(this.Callback);
      this.waveOutLock = new object();
      this.callbackInfo = callbackInfo;
      callbackInfo.Connect(this.callback);
    }

    public void Init(IWaveProvider waveProvider)
    {
      this.waveStream = waveProvider;
      int byteSize = waveProvider.WaveFormat.ConvertLatencyToByteSize((this.DesiredLatency + this.NumberOfBuffers - 1) / this.NumberOfBuffers);
      MmResult result;
      lock (this.waveOutLock)
        result = this.callbackInfo.WaveOutOpen(out this.hWaveOut, this.DeviceNumber, this.waveStream.WaveFormat, this.callback);
      MmException.Try(result, "waveOutOpen");
      this.buffers = new WaveOutBuffer[this.NumberOfBuffers];
      this.playbackState = PlaybackState.Stopped;
      for (int index = 0; index < this.NumberOfBuffers; ++index)
        this.buffers[index] = new WaveOutBuffer(this.hWaveOut, byteSize, this.waveStream, this.waveOutLock);
    }

    public void Play()
    {
      if (this.playbackState == PlaybackState.Stopped)
      {
        this.playbackState = PlaybackState.Playing;
        this.EnqueueBuffers();
      }
      else
      {
        if (this.playbackState != PlaybackState.Paused)
          return;
        this.EnqueueBuffers();
        this.Resume();
        this.playbackState = PlaybackState.Playing;
      }
    }

    private void EnqueueBuffers()
    {
      for (int index = 0; index < this.NumberOfBuffers; ++index)
      {
        if (!this.buffers[index].InQueue)
        {
          if (this.buffers[index].OnDone())
          {
            Interlocked.Increment(ref this.queuedBuffers);
          }
          else
          {
            this.playbackState = PlaybackState.Stopped;
            break;
          }
        }
      }
    }

    public void Pause()
    {
      if (this.playbackState != PlaybackState.Playing)
        return;
      MmResult result;
      lock (this.waveOutLock)
        result = WaveInterop.waveOutPause(this.hWaveOut);
      if (result != MmResult.NoError)
        throw new MmException(result, "waveOutPause");
      this.playbackState = PlaybackState.Paused;
    }

    public void Resume()
    {
      if (this.playbackState != PlaybackState.Paused)
        return;
      MmResult result;
      lock (this.waveOutLock)
        result = WaveInterop.waveOutRestart(this.hWaveOut);
      if (result != MmResult.NoError)
        throw new MmException(result, "waveOutRestart");
      this.playbackState = PlaybackState.Playing;
    }

    public void Stop()
    {
      if (this.playbackState == PlaybackState.Stopped)
        return;
      this.playbackState = PlaybackState.Stopped;
      MmResult result;
      lock (this.waveOutLock)
        result = WaveInterop.waveOutReset(this.hWaveOut);
      if (result != MmResult.NoError)
        throw new MmException(result, "waveOutReset");
      if (this.callbackInfo.Strategy != WaveCallbackStrategy.FunctionCallback)
        return;
      this.RaisePlaybackStoppedEvent((Exception) null);
    }

    public long GetPosition()
    {
      lock (this.waveOutLock)
      {
        MmTime mmTime = new MmTime();
        mmTime.wType = 4U;
        MmException.Try(WaveInterop.waveOutGetPosition(this.hWaveOut, out mmTime, Marshal.SizeOf((object) mmTime)), "waveOutGetPosition");
        return mmTime.wType == 4U ? (long) mmTime.cb : throw new Exception(string.Format("waveOutGetPosition: wType -> Expected {0}, Received {1}", (object) 4, (object) mmTime.wType));
      }
    }

    public WaveFormat OutputWaveFormat => this.waveStream.WaveFormat;

    public PlaybackState PlaybackState => this.playbackState;

    public float Volume
    {
      get => this.volume;
      set
      {
        WaveOut.SetWaveOutVolume(value, this.hWaveOut, this.waveOutLock);
        this.volume = value;
      }
    }

    internal static void SetWaveOutVolume(float value, IntPtr hWaveOut, object lockObject)
    {
      if ((double) value < 0.0)
        throw new ArgumentOutOfRangeException(nameof (value), "Volume must be between 0.0 and 1.0");
      if ((double) value > 1.0)
        throw new ArgumentOutOfRangeException(nameof (value), "Volume must be between 0.0 and 1.0");
      int dwVolume = (int) ((double) value * (double) ushort.MaxValue) + ((int) ((double) value * (double) ushort.MaxValue) << 16);
      MmResult result;
      lock (lockObject)
        result = WaveInterop.waveOutSetVolume(hWaveOut, dwVolume);
      MmException.Try(result, "waveOutSetVolume");
    }

    public void Dispose()
    {
      GC.SuppressFinalize((object) this);
      this.Dispose(true);
    }

    protected void Dispose(bool disposing)
    {
      this.Stop();
      if (disposing && this.buffers != null)
      {
        for (int index = 0; index < this.buffers.Length; ++index)
        {
          if (this.buffers[index] != null)
            this.buffers[index].Dispose();
        }
        this.buffers = (WaveOutBuffer[]) null;
      }
      lock (this.waveOutLock)
      {
        int num = (int) WaveInterop.waveOutClose(this.hWaveOut);
      }
      if (!disposing)
        return;
      this.callbackInfo.Disconnect();
    }

    ~WaveOut() => this.Dispose(false);

    private void Callback(
      IntPtr hWaveOut,
      WaveInterop.WaveMessage uMsg,
      IntPtr dwInstance,
      WaveHeader wavhdr,
      IntPtr dwReserved)
    {
      if (uMsg != WaveInterop.WaveMessage.WaveOutDone)
        return;
      WaveOutBuffer target = (WaveOutBuffer) ((GCHandle) wavhdr.userData).Target;
      Interlocked.Decrement(ref this.queuedBuffers);
      Exception e = (Exception) null;
      if (this.PlaybackState == PlaybackState.Playing)
      {
        lock (this.waveOutLock)
        {
          try
          {
            if (target.OnDone())
              Interlocked.Increment(ref this.queuedBuffers);
          }
          catch (Exception ex)
          {
            e = ex;
          }
        }
      }
      if (this.queuedBuffers != 0 || this.callbackInfo.Strategy == WaveCallbackStrategy.FunctionCallback && this.playbackState == PlaybackState.Stopped)
        return;
      this.playbackState = PlaybackState.Stopped;
      this.RaisePlaybackStoppedEvent(e);
    }

    private void RaisePlaybackStoppedEvent(Exception e)
    {
      EventHandler<StoppedEventArgs> handler = this.PlaybackStopped;
      if (handler == null)
        return;
      if (this.syncContext == null)
        handler((object) this, new StoppedEventArgs(e));
      else
        this.syncContext.Post((SendOrPostCallback) (state => handler((object) this, new StoppedEventArgs(e))), (object) null);
    }
  }
}
