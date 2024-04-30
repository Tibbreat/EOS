// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.MediaFoundationTransform
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using NAudio.Wave;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  public abstract class MediaFoundationTransform : IWaveProvider, IDisposable
  {
    protected readonly IWaveProvider sourceProvider;
    protected readonly WaveFormat outputWaveFormat;
    private readonly byte[] sourceBuffer;
    private byte[] outputBuffer;
    private int outputBufferOffset;
    private int outputBufferCount;
    private IMFTransform transform;
    private bool disposed;
    private long inputPosition;
    private long outputPosition;
    private bool initializedForStreaming;

    public MediaFoundationTransform(IWaveProvider sourceProvider, WaveFormat outputFormat)
    {
      this.outputWaveFormat = outputFormat;
      this.sourceProvider = sourceProvider;
      this.sourceBuffer = new byte[sourceProvider.WaveFormat.AverageBytesPerSecond];
      this.outputBuffer = new byte[this.outputWaveFormat.AverageBytesPerSecond + this.outputWaveFormat.BlockAlign];
    }

    private void InitializeTransformForStreaming()
    {
      this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_COMMAND_FLUSH, IntPtr.Zero);
      this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_BEGIN_STREAMING, IntPtr.Zero);
      this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_START_OF_STREAM, IntPtr.Zero);
      this.initializedForStreaming = true;
    }

    protected abstract IMFTransform CreateTransform();

    protected virtual void Dispose(bool disposing)
    {
      if (this.transform == null)
        return;
      Marshal.ReleaseComObject((object) this.transform);
    }

    public void Dispose()
    {
      if (this.disposed)
        return;
      this.disposed = true;
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    ~MediaFoundationTransform() => this.Dispose(false);

    public WaveFormat WaveFormat => this.outputWaveFormat;

    public int Read(byte[] buffer, int offset, int count)
    {
      if (this.transform == null)
      {
        this.transform = this.CreateTransform();
        this.InitializeTransformForStreaming();
      }
      int num = 0;
      if (this.outputBufferCount > 0)
        num += this.ReadFromOutputBuffer(buffer, offset, count - num);
      for (; num < count; num += this.ReadFromOutputBuffer(buffer, offset + num, count - num))
      {
        IMFSample mfSample = this.ReadFromSource();
        if (mfSample == null)
        {
          this.EndStreamAndDrain();
          num += this.ReadFromOutputBuffer(buffer, offset + num, count - num);
          break;
        }
        if (!this.initializedForStreaming)
          this.InitializeTransformForStreaming();
        this.transform.ProcessInput(0, mfSample, 0);
        Marshal.ReleaseComObject((object) mfSample);
        this.ReadFromTransform();
      }
      return num;
    }

    private void EndStreamAndDrain()
    {
      this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_END_OF_STREAM, IntPtr.Zero);
      this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_COMMAND_DRAIN, IntPtr.Zero);
      do
        ;
      while (this.ReadFromTransform() > 0);
      this.outputBufferCount = 0;
      this.outputBufferOffset = 0;
      this.inputPosition = 0L;
      this.outputPosition = 0L;
      this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_END_STREAMING, IntPtr.Zero);
      this.initializedForStreaming = false;
    }

    private int ReadFromTransform()
    {
      MFT_OUTPUT_DATA_BUFFER[] pOutputSamples = new MFT_OUTPUT_DATA_BUFFER[1];
      IMFSample sample = MediaFoundationApi.CreateSample();
      IMFMediaBuffer memoryBuffer = MediaFoundationApi.CreateMemoryBuffer(this.outputBuffer.Length);
      sample.AddBuffer(memoryBuffer);
      sample.SetSampleTime(this.outputPosition);
      pOutputSamples[0].pSample = sample;
      int errorCode = this.transform.ProcessOutput(_MFT_PROCESS_OUTPUT_FLAGS.None, 1, pOutputSamples, out _MFT_PROCESS_OUTPUT_STATUS _);
      switch (errorCode)
      {
        case -1072861838:
          Marshal.ReleaseComObject((object) memoryBuffer);
          Marshal.ReleaseComObject((object) sample);
          return 0;
        case 0:
          IMFMediaBuffer ppBuffer;
          pOutputSamples[0].pSample.ConvertToContiguousBuffer(out ppBuffer);
          IntPtr ppbBuffer;
          int pcbCurrentLength;
          ppBuffer.Lock(out ppbBuffer, out int _, out pcbCurrentLength);
          this.outputBuffer = BufferHelpers.Ensure(this.outputBuffer, pcbCurrentLength);
          Marshal.Copy(ppbBuffer, this.outputBuffer, 0, pcbCurrentLength);
          this.outputBufferOffset = 0;
          this.outputBufferCount = pcbCurrentLength;
          ppBuffer.Unlock();
          this.outputPosition += MediaFoundationTransform.BytesToNsPosition(this.outputBufferCount, this.WaveFormat);
          Marshal.ReleaseComObject((object) memoryBuffer);
          Marshal.ReleaseComObject((object) sample);
          Marshal.ReleaseComObject((object) ppBuffer);
          return pcbCurrentLength;
        default:
          Marshal.ThrowExceptionForHR(errorCode);
          goto case 0;
      }
    }

    private static long BytesToNsPosition(int bytes, WaveFormat waveFormat)
    {
      return 10000000L * (long) bytes / (long) waveFormat.AverageBytesPerSecond;
    }

    private IMFSample ReadFromSource()
    {
      int num = this.sourceProvider.Read(this.sourceBuffer, 0, this.sourceBuffer.Length);
      if (num == 0)
        return (IMFSample) null;
      IMFMediaBuffer memoryBuffer = MediaFoundationApi.CreateMemoryBuffer(num);
      IntPtr ppbBuffer;
      memoryBuffer.Lock(out ppbBuffer, out int _, out int _);
      Marshal.Copy(this.sourceBuffer, 0, ppbBuffer, num);
      memoryBuffer.Unlock();
      memoryBuffer.SetCurrentLength(num);
      IMFSample sample = MediaFoundationApi.CreateSample();
      sample.AddBuffer(memoryBuffer);
      sample.SetSampleTime(this.inputPosition);
      long nsPosition = MediaFoundationTransform.BytesToNsPosition(num, this.sourceProvider.WaveFormat);
      sample.SetSampleDuration(nsPosition);
      this.inputPosition += nsPosition;
      Marshal.ReleaseComObject((object) memoryBuffer);
      return sample;
    }

    private int ReadFromOutputBuffer(byte[] buffer, int offset, int needed)
    {
      int length = Math.Min(needed, this.outputBufferCount);
      Array.Copy((Array) this.outputBuffer, this.outputBufferOffset, (Array) buffer, offset, length);
      this.outputBufferOffset += length;
      this.outputBufferCount -= length;
      if (this.outputBufferCount == 0)
        this.outputBufferOffset = 0;
      return length;
    }

    public void Reposition()
    {
      if (!this.initializedForStreaming)
        return;
      this.EndStreamAndDrain();
      this.InitializeTransformForStreaming();
    }
  }
}
