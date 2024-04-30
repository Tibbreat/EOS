// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveInEvent
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
  public class WaveInEvent : IWaveIn, IDisposable
  {
    private readonly AutoResetEvent callbackEvent;
    private readonly SynchronizationContext syncContext;
    private IntPtr waveInHandle;
    private volatile bool recording;
    private WaveInBuffer[] buffers;

    public event EventHandler<WaveInEventArgs> DataAvailable;

    public event EventHandler<StoppedEventArgs> RecordingStopped;

    public WaveInEvent()
    {
      this.callbackEvent = new AutoResetEvent(false);
      this.syncContext = SynchronizationContext.Current;
      this.DeviceNumber = 0;
      this.WaveFormat = new WaveFormat(8000, 16, 1);
      this.BufferMilliseconds = 100;
      this.NumberOfBuffers = 3;
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

    private void OpenWaveInDevice()
    {
      this.CloseWaveInDevice();
      MmException.Try(WaveInterop.waveInOpenWindow(out this.waveInHandle, (IntPtr) this.DeviceNumber, this.WaveFormat, this.callbackEvent.SafeWaitHandle.DangerousGetHandle(), IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackEvent), "waveInOpen");
      this.CreateBuffers();
    }

    public void StartRecording()
    {
      if (this.recording)
        throw new InvalidOperationException("Already recording");
      this.OpenWaveInDevice();
      MmException.Try(WaveInterop.waveInStart(this.waveInHandle), "waveInStart");
      this.recording = true;
      ThreadPool.QueueUserWorkItem((WaitCallback) (state => this.RecordThread()), (object) null);
    }

    private void RecordThread()
    {
      Exception e = (Exception) null;
      try
      {
        this.DoRecording();
      }
      catch (Exception ex)
      {
        e = ex;
      }
      finally
      {
        this.recording = false;
        this.RaiseRecordingStoppedEvent(e);
      }
    }

    private void DoRecording()
    {
      foreach (WaveInBuffer buffer in this.buffers)
      {
        if (!buffer.InQueue)
          buffer.Reuse();
      }
      while (this.recording)
      {
        if (this.callbackEvent.WaitOne() && this.recording)
        {
          foreach (WaveInBuffer buffer in this.buffers)
          {
            if (buffer.Done)
            {
              if (this.DataAvailable != null)
                this.DataAvailable((object) this, new WaveInEventArgs(buffer.Data, buffer.BytesRecorded));
              buffer.Reuse();
            }
          }
        }
      }
    }

    private void RaiseRecordingStoppedEvent(Exception e)
    {
      EventHandler<StoppedEventArgs> handler = this.RecordingStopped;
      if (handler == null)
        return;
      if (this.syncContext == null)
        handler((object) this, new StoppedEventArgs(e));
      else
        this.syncContext.Post((SendOrPostCallback) (state => handler((object) this, new StoppedEventArgs(e))), (object) null);
    }

    public void StopRecording()
    {
      this.recording = false;
      this.callbackEvent.Set();
      MmException.Try(WaveInterop.waveInStop(this.waveInHandle), "waveInStop");
    }

    public WaveFormat WaveFormat { get; set; }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.recording)
        this.StopRecording();
      this.CloseWaveInDevice();
    }

    private void CloseWaveInDevice()
    {
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
