// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.AsioOut
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave.Asio;
using System;
using System.Threading;

#nullable disable
namespace NAudio.Wave
{
  public class AsioOut : IWavePlayer, IDisposable
  {
    private ASIODriverExt driver;
    private IWaveProvider sourceStream;
    private PlaybackState playbackState;
    private int nbSamples;
    private byte[] waveBuffer;
    private ASIOSampleConvertor.SampleConvertor convertor;
    private readonly string driverName;
    private readonly SynchronizationContext syncContext;

    public event EventHandler<StoppedEventArgs> PlaybackStopped;

    public event EventHandler<AsioAudioAvailableEventArgs> AudioAvailable;

    public AsioOut()
      : this(0)
    {
    }

    public AsioOut(string driverName)
    {
      this.syncContext = SynchronizationContext.Current;
      this.InitFromName(driverName);
    }

    public AsioOut(int driverIndex)
    {
      this.syncContext = SynchronizationContext.Current;
      string[] driverNames = AsioOut.GetDriverNames();
      if (driverNames.Length == 0)
        throw new ArgumentException("There is no ASIO Driver installed on your system");
      if (driverIndex < 0 || driverIndex > driverNames.Length)
        throw new ArgumentException(string.Format("Invalid device number. Must be in the range [0,{0}]", (object) driverNames.Length));
      this.driverName = driverNames[driverIndex];
      this.InitFromName(this.driverName);
    }

    ~AsioOut() => this.Dispose();

    public void Dispose()
    {
      if (this.driver == null)
        return;
      if (this.playbackState != PlaybackState.Stopped)
        this.driver.Stop();
      this.driver.ReleaseDriver();
      this.driver = (ASIODriverExt) null;
    }

    public static string[] GetDriverNames() => ASIODriver.GetASIODriverNames();

    public static bool isSupported() => AsioOut.GetDriverNames().Length > 0;

    private void InitFromName(string driverName)
    {
      this.driver = new ASIODriverExt(ASIODriver.GetASIODriverByName(driverName));
      this.ChannelOffset = 0;
    }

    public void ShowControlPanel() => this.driver.ShowControlPanel();

    public void Play()
    {
      if (this.playbackState == PlaybackState.Playing)
        return;
      this.playbackState = PlaybackState.Playing;
      this.driver.Start();
    }

    public void Stop()
    {
      this.playbackState = PlaybackState.Stopped;
      this.driver.Stop();
      this.RaisePlaybackStopped((Exception) null);
    }

    public void Pause()
    {
      this.playbackState = PlaybackState.Paused;
      this.driver.Stop();
    }

    public void Init(IWaveProvider waveProvider) => this.InitRecordAndPlayback(waveProvider, 0, -1);

    public void InitRecordAndPlayback(
      IWaveProvider waveProvider,
      int recordChannels,
      int recordOnlySampleRate)
    {
      if (this.sourceStream != null)
        throw new InvalidOperationException("Already initialised this instance of AsioOut - dispose and create a new one");
      int sampleRate = waveProvider != null ? waveProvider.WaveFormat.SampleRate : recordOnlySampleRate;
      if (waveProvider != null)
      {
        this.sourceStream = waveProvider;
        this.NumberOfOutputChannels = waveProvider.WaveFormat.Channels;
        this.convertor = ASIOSampleConvertor.SelectSampleConvertor(waveProvider.WaveFormat, this.driver.Capabilities.OutputChannelInfos[0].type);
      }
      else
        this.NumberOfOutputChannels = 0;
      if (!this.driver.IsSampleRateSupported((double) sampleRate))
        throw new ArgumentException("SampleRate is not supported");
      if (this.driver.Capabilities.SampleRate != (double) sampleRate)
        this.driver.SetSampleRate((double) sampleRate);
      this.driver.FillBufferCallback = new ASIOFillBufferCallback(this.driver_BufferUpdate);
      this.NumberOfInputChannels = recordChannels;
      this.nbSamples = this.driver.CreateBuffers(this.NumberOfOutputChannels, this.NumberOfInputChannels, false);
      this.driver.SetChannelOffset(this.ChannelOffset, this.InputChannelOffset);
      if (waveProvider == null)
        return;
      this.waveBuffer = new byte[this.nbSamples * this.NumberOfOutputChannels * waveProvider.WaveFormat.BitsPerSample / 8];
    }

    private unsafe void driver_BufferUpdate(IntPtr[] inputChannels, IntPtr[] outputChannels)
    {
      if (this.NumberOfInputChannels > 0)
      {
        EventHandler<AsioAudioAvailableEventArgs> audioAvailable = this.AudioAvailable;
        if (audioAvailable != null)
        {
          AsioAudioAvailableEventArgs e = new AsioAudioAvailableEventArgs(inputChannels, outputChannels, this.nbSamples, this.driver.Capabilities.InputChannelInfos[0].type);
          audioAvailable((object) this, e);
          if (e.WrittenToOutputBuffers)
            return;
        }
      }
      if (this.NumberOfOutputChannels <= 0)
        return;
      int num = this.sourceStream.Read(this.waveBuffer, 0, this.waveBuffer.Length);
      int length = this.waveBuffer.Length;
      fixed (byte* numPtr = &this.waveBuffer[0])
        this.convertor(new IntPtr((void*) numPtr), outputChannels, this.NumberOfOutputChannels, this.nbSamples);
      if (num != 0)
        return;
      this.Stop();
    }

    public int PlaybackLatency
    {
      get
      {
        int outputLatency;
        int latencies = (int) this.driver.Driver.GetLatencies(out int _, out outputLatency);
        return outputLatency;
      }
    }

    public PlaybackState PlaybackState => this.playbackState;

    public string DriverName => this.driverName;

    public int NumberOfOutputChannels { get; private set; }

    public int NumberOfInputChannels { get; private set; }

    public int DriverInputChannelCount => this.driver.Capabilities.NbInputChannels;

    public int DriverOutputChannelCount => this.driver.Capabilities.NbOutputChannels;

    public int ChannelOffset { get; set; }

    public int InputChannelOffset { get; set; }

    [Obsolete("this function will be removed in a future NAudio as ASIO does not support setting the volume on the device")]
    public float Volume
    {
      get => 1f;
      set
      {
        if ((double) value != 1.0)
          throw new InvalidOperationException("AsioOut does not support setting the device volume");
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

    public string AsioInputChannelName(int channel)
    {
      return channel <= this.DriverInputChannelCount ? this.driver.Capabilities.InputChannelInfos[channel].name : "";
    }

    public string AsioOutputChannelName(int channel)
    {
      return channel <= this.DriverOutputChannelCount ? this.driver.Capabilities.OutputChannelInfos[channel].name : "";
    }
  }
}
