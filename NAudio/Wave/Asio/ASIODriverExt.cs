// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.ASIODriverExt
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.Asio
{
  internal class ASIODriverExt
  {
    private ASIODriver driver;
    private ASIOCallbacks callbacks;
    private AsioDriverCapability capability;
    private ASIOBufferInfo[] bufferInfos;
    private bool isOutputReadySupported;
    private IntPtr[] currentOutputBuffers;
    private IntPtr[] currentInputBuffers;
    private int numberOfOutputChannels;
    private int numberOfInputChannels;
    private ASIOFillBufferCallback fillBufferCallback;
    private int bufferSize;
    private int outputChannelOffset;
    private int inputChannelOffset;

    public ASIODriverExt(ASIODriver driver)
    {
      this.driver = driver;
      if (!driver.init(IntPtr.Zero))
        throw new InvalidOperationException(driver.getErrorMessage());
      this.callbacks = new ASIOCallbacks();
      this.callbacks.pasioMessage = new ASIOCallbacks.ASIOAsioMessageCallBack(this.AsioMessageCallBack);
      this.callbacks.pbufferSwitch = new ASIOCallbacks.ASIOBufferSwitchCallBack(this.BufferSwitchCallBack);
      this.callbacks.pbufferSwitchTimeInfo = new ASIOCallbacks.ASIOBufferSwitchTimeInfoCallBack(this.BufferSwitchTimeInfoCallBack);
      this.callbacks.psampleRateDidChange = new ASIOCallbacks.ASIOSampleRateDidChangeCallBack(this.SampleRateDidChangeCallBack);
      this.BuildCapabilities();
    }

    public void SetChannelOffset(int outputChannelOffset, int inputChannelOffset)
    {
      if (outputChannelOffset + this.numberOfOutputChannels > this.Capabilities.NbOutputChannels)
        throw new ArgumentException("Invalid channel offset");
      this.outputChannelOffset = outputChannelOffset;
      if (inputChannelOffset + this.numberOfInputChannels > this.Capabilities.NbInputChannels)
        throw new ArgumentException("Invalid channel offset");
      this.inputChannelOffset = inputChannelOffset;
    }

    public ASIODriver Driver => this.driver;

    public void Start() => this.driver.start();

    public void Stop()
    {
      int num = (int) this.driver.stop();
    }

    public void ShowControlPanel() => this.driver.controlPanel();

    public void ReleaseDriver()
    {
      try
      {
        int num = (int) this.driver.disposeBuffers();
      }
      catch (Exception ex)
      {
        Console.Out.WriteLine(ex.ToString());
      }
      this.driver.ReleaseComASIODriver();
    }

    public bool IsSampleRateSupported(double sampleRate) => this.driver.canSampleRate(sampleRate);

    public void SetSampleRate(double sampleRate)
    {
      this.driver.setSampleRate(sampleRate);
      this.BuildCapabilities();
    }

    public ASIOFillBufferCallback FillBufferCallback
    {
      get => this.fillBufferCallback;
      set => this.fillBufferCallback = value;
    }

    public AsioDriverCapability Capabilities => this.capability;

    public unsafe int CreateBuffers(
      int numberOfOutputChannels,
      int numberOfInputChannels,
      bool useMaxBufferSize)
    {
      if (numberOfOutputChannels < 0 || numberOfOutputChannels > this.capability.NbOutputChannels)
        throw new ArgumentException(string.Format("Invalid number of channels {0}, must be in the range [0,{1}]", (object) numberOfOutputChannels, (object) this.capability.NbOutputChannels));
      if (numberOfInputChannels < 0 || numberOfInputChannels > this.capability.NbInputChannels)
        throw new ArgumentException(nameof (numberOfInputChannels), string.Format("Invalid number of input channels {0}, must be in the range [0,{1}]", (object) numberOfInputChannels, (object) this.capability.NbInputChannels));
      this.numberOfOutputChannels = numberOfOutputChannels;
      this.numberOfInputChannels = numberOfInputChannels;
      int numChannels = this.capability.NbInputChannels + this.capability.NbOutputChannels;
      this.bufferInfos = new ASIOBufferInfo[numChannels];
      this.currentOutputBuffers = new IntPtr[numberOfOutputChannels];
      this.currentInputBuffers = new IntPtr[numberOfInputChannels];
      int index = 0;
      int num1 = 0;
      while (num1 < this.capability.NbInputChannels)
      {
        this.bufferInfos[index].isInput = true;
        this.bufferInfos[index].channelNum = num1;
        this.bufferInfos[index].pBuffer0 = IntPtr.Zero;
        this.bufferInfos[index].pBuffer1 = IntPtr.Zero;
        ++num1;
        ++index;
      }
      int num2 = 0;
      while (num2 < this.capability.NbOutputChannels)
      {
        this.bufferInfos[index].isInput = false;
        this.bufferInfos[index].channelNum = num2;
        this.bufferInfos[index].pBuffer0 = IntPtr.Zero;
        this.bufferInfos[index].pBuffer1 = IntPtr.Zero;
        ++num2;
        ++index;
      }
      this.bufferSize = !useMaxBufferSize ? this.capability.BufferPreferredSize : this.capability.BufferMaxSize;
      fixed (ASIOBufferInfo* asioBufferInfoPtr = &this.bufferInfos[0])
        this.driver.createBuffers(new IntPtr((void*) asioBufferInfoPtr), numChannels, this.bufferSize, ref this.callbacks);
      this.isOutputReadySupported = this.driver.outputReady() == ASIOError.ASE_OK;
      return this.bufferSize;
    }

    private void BuildCapabilities()
    {
      this.capability = new AsioDriverCapability();
      this.capability.DriverName = this.driver.getDriverName();
      this.driver.getChannels(out this.capability.NbInputChannels, out this.capability.NbOutputChannels);
      this.capability.InputChannelInfos = new ASIOChannelInfo[this.capability.NbInputChannels];
      this.capability.OutputChannelInfos = new ASIOChannelInfo[this.capability.NbOutputChannels];
      for (int channelNumber = 0; channelNumber < this.capability.NbInputChannels; ++channelNumber)
        this.capability.InputChannelInfos[channelNumber] = this.driver.getChannelInfo(channelNumber, true);
      for (int channelNumber = 0; channelNumber < this.capability.NbOutputChannels; ++channelNumber)
        this.capability.OutputChannelInfos[channelNumber] = this.driver.getChannelInfo(channelNumber, false);
      this.capability.SampleRate = this.driver.getSampleRate();
      ASIOError latencies = this.driver.GetLatencies(out this.capability.InputLatency, out this.capability.OutputLatency);
      switch (latencies)
      {
        case ASIOError.ASE_NotPresent:
        case ASIOError.ASE_OK:
          this.driver.getBufferSize(out this.capability.BufferMinSize, out this.capability.BufferMaxSize, out this.capability.BufferPreferredSize, out this.capability.BufferGranularity);
          break;
        default:
          throw new ASIOException("ASIOgetLatencies")
          {
            Error = latencies
          };
      }
    }

    private void BufferSwitchCallBack(int doubleBufferIndex, bool directProcess)
    {
      for (int index = 0; index < this.numberOfInputChannels; ++index)
        this.currentInputBuffers[index] = this.bufferInfos[index + this.inputChannelOffset].Buffer(doubleBufferIndex);
      for (int index = 0; index < this.numberOfOutputChannels; ++index)
        this.currentOutputBuffers[index] = this.bufferInfos[index + this.outputChannelOffset + this.capability.NbInputChannels].Buffer(doubleBufferIndex);
      if (this.fillBufferCallback != null)
        this.fillBufferCallback(this.currentInputBuffers, this.currentOutputBuffers);
      if (!this.isOutputReadySupported)
        return;
      int num = (int) this.driver.outputReady();
    }

    private void SampleRateDidChangeCallBack(double sRate) => this.capability.SampleRate = sRate;

    private int AsioMessageCallBack(
      ASIOMessageSelector selector,
      int value,
      IntPtr message,
      IntPtr opt)
    {
      switch (selector)
      {
        case ASIOMessageSelector.kAsioSelectorSupported:
          switch ((ASIOMessageSelector) Enum.ToObject(typeof (ASIOMessageSelector), value))
          {
            case ASIOMessageSelector.kAsioEngineVersion:
              return 1;
            case ASIOMessageSelector.kAsioResetRequest:
              return 0;
            case ASIOMessageSelector.kAsioBufferSizeChange:
              return 0;
            case ASIOMessageSelector.kAsioResyncRequest:
              return 0;
            case ASIOMessageSelector.kAsioLatenciesChanged:
              return 0;
            case ASIOMessageSelector.kAsioSupportsTimeInfo:
              return 0;
            case ASIOMessageSelector.kAsioSupportsTimeCode:
              return 0;
          }
          break;
        case ASIOMessageSelector.kAsioEngineVersion:
          return 2;
        case ASIOMessageSelector.kAsioResetRequest:
          return 1;
        case ASIOMessageSelector.kAsioBufferSizeChange:
          return 0;
        case ASIOMessageSelector.kAsioResyncRequest:
          return 0;
        case ASIOMessageSelector.kAsioLatenciesChanged:
          return 0;
        case ASIOMessageSelector.kAsioSupportsTimeInfo:
          return 0;
        case ASIOMessageSelector.kAsioSupportsTimeCode:
          return 0;
      }
      return 0;
    }

    private IntPtr BufferSwitchTimeInfoCallBack(
      IntPtr asioTimeParam,
      int doubleBufferIndex,
      bool directProcess)
    {
      return IntPtr.Zero;
    }
  }
}
