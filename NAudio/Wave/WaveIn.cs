// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveIn
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Mixer;
using System;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace NAudio.Wave
{
  public class WaveIn : IWaveIn, IDisposable
  {
    private IntPtr waveInHandle;
    private volatile bool recording;
    private WaveInBuffer[] buffers;
    private readonly WaveInterop.WaveCallback callback;
    private WaveCallbackInfo callbackInfo;
    private readonly SynchronizationContext syncContext;
    private int lastReturnedBufferIndex;

    public event EventHandler<WaveInEventArgs> DataAvailable;

    public event EventHandler<StoppedEventArgs> RecordingStopped;

    public WaveIn()
      : this(WaveCallbackInfo.NewWindow())
    {
    }

    public WaveIn(IntPtr windowHandle)
      : this(WaveCallbackInfo.ExistingWindow(windowHandle))
    {
    }

    public WaveIn(WaveCallbackInfo callbackInfo)
    {
      this.syncContext = SynchronizationContext.Current;
      if ((callbackInfo.Strategy == WaveCallbackStrategy.NewWindow || callbackInfo.Strategy == WaveCallbackStrategy.ExistingWindow) && this.syncContext == null)
        throw new InvalidOperationException("Use WaveInEvent to record on a background thread");
      this.DeviceNumber = 0;
      this.WaveFormat = new WaveFormat(8000, 16, 1);
      this.BufferMilliseconds = 100;
      this.NumberOfBuffers = 3;
      this.callback = new WaveInterop.WaveCallback(this.Callback);
      this.callbackInfo = callbackInfo;
      callbackInfo.Connect(this.callback);
    }

    public static int DeviceCount => WaveInterop.waveInGetNumDevs();

    public static WaveInCapabilities GetCapabilities(int devNumber)
    {
      WaveInCapabilities waveInCaps = new WaveInCapabilities();
      int waveInCapsSize = Marshal.SizeOf((object) waveInCaps);
      MmException.Try(WaveInterop.waveInGetDevCaps((IntPtr) devNumber, out waveInCaps, waveInCapsSize), "waveInGetDevCaps");
      return waveInCaps;
    }

    public int BufferMilliseconds { get; set; }

    public int NumberOfBuffers { get; set; }

    public int DeviceNumber { get; set; }

    private void CreateBuffers()
    {
      int bufferSize = this.BufferMilliseconds * this.WaveFormat.AverageBytesPerSecond / 1000;
      if (bufferSize % this.WaveFormat.BlockAlign != 0)
        bufferSize -= bufferSize % this.WaveFormat.BlockAlign;
      this.buffers = new WaveInBuffer[this.NumberOfBuffers];
      for (int index = 0; index < this.buffers.Length; ++index)
        this.buffers[index] = new WaveInBuffer(this.waveInHandle, bufferSize);
    }

    private void Callback(
      IntPtr waveInHandle,
      WaveInterop.WaveMessage message,
      IntPtr userData,
      WaveHeader waveHeader,
      IntPtr reserved)
    {
      if (message != WaveInterop.WaveMessage.WaveInData || !this.recording)
        return;
      WaveInBuffer target = (WaveInBuffer) ((GCHandle) waveHeader.userData).Target;
      if (target == null)
        return;
      this.lastReturnedBufferIndex = Array.IndexOf<WaveInBuffer>(this.buffers, target);
      this.RaiseDataAvailable(target);
      try
      {
        target.Reuse();
      }
      catch (Exception ex)
      {
        this.recording = false;
        this.RaiseRecordingStopped(ex);
      }
    }

    private void RaiseDataAvailable(WaveInBuffer buffer)
    {
      EventHandler<WaveInEventArgs> dataAvailable = this.DataAvailable;
      if (dataAvailable == null)
        return;
      dataAvailable((object) this, new WaveInEventArgs(buffer.Data, buffer.BytesRecorded));
    }

    private void RaiseRecordingStopped(Exception e)
    {
      EventHandler<StoppedEventArgs> handler = this.RecordingStopped;
      if (handler == null)
        return;
      if (this.syncContext == null)
        handler((object) this, new StoppedEventArgs(e));
      else
        this.syncContext.Post((SendOrPostCallback) (state => handler((object) this, new StoppedEventArgs(e))), (object) null);
    }

    private void OpenWaveInDevice()
    {
      this.CloseWaveInDevice();
      MmException.Try(this.callbackInfo.WaveInOpen(out this.waveInHandle, this.DeviceNumber, this.WaveFormat, this.callback), "waveInOpen");
      this.CreateBuffers();
    }

    public void StartRecording()
    {
      if (this.recording)
        throw new InvalidOperationException("Already recording");
      this.OpenWaveInDevice();
      this.EnqueueBuffers();
      MmException.Try(WaveInterop.waveInStart(this.waveInHandle), "waveInStart");
      this.recording = true;
    }

    private void EnqueueBuffers()
    {
      foreach (WaveInBuffer buffer in this.buffers)
      {
        if (!buffer.InQueue)
          buffer.Reuse();
      }
    }

    public void StopRecording()
    {
      if (!this.recording)
        return;
      this.recording = false;
      MmException.Try(WaveInterop.waveInStop(this.waveInHandle), "waveInStop");
      for (int index = 0; index < this.buffers.Length; ++index)
      {
        WaveInBuffer buffer = this.buffers[(index + this.lastReturnedBufferIndex + 1) % this.buffers.Length];
        if (buffer.Done)
          this.RaiseDataAvailable(buffer);
      }
      this.RaiseRecordingStopped((Exception) null);
    }

    public WaveFormat WaveFormat { get; set; }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.recording)
        this.StopRecording();
      this.CloseWaveInDevice();
      if (this.callbackInfo == null)
        return;
      this.callbackInfo.Disconnect();
      this.callbackInfo = (WaveCallbackInfo) null;
    }

    private void CloseWaveInDevice()
    {
      if (this.waveInHandle == IntPtr.Zero)
        return;
      int num1 = (int) WaveInterop.waveInReset(this.waveInHandle);
      if (this.buffers != null)
      {
        for (int index = 0; index < this.buffers.Length; ++index)
          this.buffers[index].Dispose();
        this.buffers = (WaveInBuffer[]) null;
      }
      int num2 = (int) WaveInterop.waveInClose(this.waveInHandle);
      this.waveInHandle = IntPtr.Zero;
    }

    public MixerLine GetMixerLine()
    {
      return !(this.waveInHandle != IntPtr.Zero) ? new MixerLine((IntPtr) this.DeviceNumber, 0, MixerFlags.WaveIn) : new MixerLine(this.waveInHandle, 0, MixerFlags.WaveInHandle);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }
  }
}
