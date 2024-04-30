// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.WasapiCapture
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave;
using System;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class WasapiCapture : IWaveIn, IDisposable
  {
    private const long REFTIMES_PER_SEC = 10000000;
    private const long REFTIMES_PER_MILLISEC = 10000;
    private volatile bool requestStop;
    private byte[] recordBuffer;
    private Thread captureThread;
    private AudioClient audioClient;
    private int bytesPerFrame;
    private WaveFormat waveFormat;
    private bool initialized;
    private readonly SynchronizationContext syncContext;
    private readonly bool isUsingEventSync;
    private EventWaitHandle frameEventWaitHandle;

    public event EventHandler<WaveInEventArgs> DataAvailable;

    public event EventHandler<StoppedEventArgs> RecordingStopped;

    public WasapiCapture()
      : this(WasapiCapture.GetDefaultCaptureDevice())
    {
    }

    public WasapiCapture(MMDevice captureDevice)
      : this(captureDevice, false)
    {
    }

    public WasapiCapture(MMDevice captureDevice, bool useEventSync)
    {
      this.syncContext = SynchronizationContext.Current;
      this.audioClient = captureDevice.AudioClient;
      this.ShareMode = AudioClientShareMode.Shared;
      this.isUsingEventSync = useEventSync;
      this.waveFormat = this.audioClient.MixFormat;
    }

    public AudioClientShareMode ShareMode { get; set; }

    public virtual WaveFormat WaveFormat
    {
      get
      {
        if (this.waveFormat is WaveFormatExtensible waveFormat)
        {
          try
          {
            return waveFormat.ToStandardWaveFormat();
          }
          catch (InvalidOperationException ex)
          {
          }
        }
        return this.waveFormat;
      }
      set => this.waveFormat = value;
    }

    public static MMDevice GetDefaultCaptureDevice()
    {
      return new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
    }

    private void InitializeCaptureDevice()
    {
      if (this.initialized)
        return;
      long num = 1000000;
      if (!this.audioClient.IsFormatSupported(this.ShareMode, this.waveFormat))
        throw new ArgumentException("Unsupported Wave Format");
      AudioClientStreamFlags clientStreamFlags = this.GetAudioClientStreamFlags();
      if (this.isUsingEventSync)
      {
        if (this.ShareMode == AudioClientShareMode.Shared)
          this.audioClient.Initialize(this.ShareMode, AudioClientStreamFlags.EventCallback, num, 0L, this.waveFormat, Guid.Empty);
        else
          this.audioClient.Initialize(this.ShareMode, AudioClientStreamFlags.EventCallback, num, num, this.waveFormat, Guid.Empty);
        this.frameEventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        this.audioClient.SetEventHandle(this.frameEventWaitHandle.SafeWaitHandle.DangerousGetHandle());
      }
      else
        this.audioClient.Initialize(this.ShareMode, clientStreamFlags, num, 0L, this.waveFormat, Guid.Empty);
      int bufferSize = this.audioClient.BufferSize;
      this.bytesPerFrame = this.waveFormat.Channels * this.waveFormat.BitsPerSample / 8;
      this.recordBuffer = new byte[bufferSize * this.bytesPerFrame];
      this.initialized = true;
    }

    protected virtual AudioClientStreamFlags GetAudioClientStreamFlags()
    {
      return AudioClientStreamFlags.None;
    }

    public void StartRecording()
    {
      if (this.captureThread != null)
        throw new InvalidOperationException("Previous recording still in progress");
      this.InitializeCaptureDevice();
      this.captureThread = new Thread((ThreadStart) (() => this.CaptureThread(this.audioClient)));
      this.requestStop = false;
      this.captureThread.Start();
    }

    public void StopRecording() => this.requestStop = true;

    private void CaptureThread(AudioClient client)
    {
      Exception e = (Exception) null;
      try
      {
        this.DoRecording(client);
      }
      catch (Exception ex)
      {
        e = ex;
      }
      finally
      {
        client.Stop();
      }
      this.captureThread = (Thread) null;
      this.RaiseRecordingStopped(e);
    }

    private void DoRecording(AudioClient client)
    {
      long num = (long) (10000000.0 * (double) client.BufferSize / (double) this.waveFormat.SampleRate);
      int millisecondsTimeout1 = (int) (num / 10000L / 2L);
      int millisecondsTimeout2 = (int) (3L * num / 10000L);
      AudioCaptureClient audioCaptureClient = client.AudioCaptureClient;
      client.Start();
      if (!this.isUsingEventSync)
        ;
      while (!this.requestStop)
      {
        bool flag = true;
        if (this.isUsingEventSync)
          flag = this.frameEventWaitHandle.WaitOne(millisecondsTimeout2, false);
        else
          Thread.Sleep(millisecondsTimeout1);
        if (!this.requestStop && flag)
          this.ReadNextPacket(audioCaptureClient);
      }
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

    private void ReadNextPacket(AudioCaptureClient capture)
    {
      int nextPacketSize = capture.GetNextPacketSize();
      int num = 0;
      for (; nextPacketSize != 0; nextPacketSize = capture.GetNextPacketSize())
      {
        int numFramesToRead;
        AudioClientBufferFlags bufferFlags;
        IntPtr buffer = capture.GetBuffer(out numFramesToRead, out bufferFlags);
        int length = numFramesToRead * this.bytesPerFrame;
        if (Math.Max(0, this.recordBuffer.Length - num) < length && num > 0)
        {
          if (this.DataAvailable != null)
            this.DataAvailable((object) this, new WaveInEventArgs(this.recordBuffer, num));
          num = 0;
        }
        if ((bufferFlags & AudioClientBufferFlags.Silent) != AudioClientBufferFlags.Silent)
          Marshal.Copy(buffer, this.recordBuffer, num, length);
        else
          Array.Clear((Array) this.recordBuffer, num, length);
        num += length;
        capture.ReleaseBuffer(numFramesToRead);
      }
      if (this.DataAvailable == null)
        return;
      this.DataAvailable((object) this, new WaveInEventArgs(this.recordBuffer, num));
    }

    public void Dispose()
    {
      this.StopRecording();
      if (this.captureThread != null)
      {
        this.captureThread.Join();
        this.captureThread = (Thread) null;
      }
      if (this.audioClient == null)
        return;
      this.audioClient.Dispose();
      this.audioClient = (AudioClient) null;
    }
  }
}
