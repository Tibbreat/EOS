// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.ResamplerDmoStream
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Dmo;
using System;

#nullable disable
namespace NAudio.Wave
{
  public class ResamplerDmoStream : WaveStream
  {
    private readonly IWaveProvider inputProvider;
    private readonly WaveStream inputStream;
    private readonly WaveFormat outputFormat;
    private DmoOutputDataBuffer outputBuffer;
    private DmoResampler dmoResampler;
    private MediaBuffer inputMediaBuffer;
    private long position;

    public ResamplerDmoStream(IWaveProvider inputProvider, WaveFormat outputFormat)
    {
      this.inputProvider = inputProvider;
      this.inputStream = inputProvider as WaveStream;
      this.outputFormat = outputFormat;
      this.dmoResampler = new DmoResampler();
      if (!this.dmoResampler.MediaObject.SupportsInputWaveFormat(0, inputProvider.WaveFormat))
        throw new ArgumentException("Unsupported Input Stream format", nameof (inputStream));
      this.dmoResampler.MediaObject.SetInputWaveFormat(0, inputProvider.WaveFormat);
      if (!this.dmoResampler.MediaObject.SupportsOutputWaveFormat(0, outputFormat))
        throw new ArgumentException("Unsupported Output Stream format", "outputStream");
      this.dmoResampler.MediaObject.SetOutputWaveFormat(0, outputFormat);
      if (this.inputStream != null)
        this.position = this.InputToOutputPosition(this.inputStream.Position);
      this.inputMediaBuffer = new MediaBuffer(inputProvider.WaveFormat.AverageBytesPerSecond);
      this.outputBuffer = new DmoOutputDataBuffer(outputFormat.AverageBytesPerSecond);
    }

    public override WaveFormat WaveFormat => this.outputFormat;

    private long InputToOutputPosition(long inputPosition)
    {
      double num = (double) this.outputFormat.AverageBytesPerSecond / (double) this.inputProvider.WaveFormat.AverageBytesPerSecond;
      long outputPosition = (long) ((double) inputPosition * num);
      if (outputPosition % (long) this.outputFormat.BlockAlign != 0L)
        outputPosition -= outputPosition % (long) this.outputFormat.BlockAlign;
      return outputPosition;
    }

    private long OutputToInputPosition(long outputPosition)
    {
      double num = (double) this.outputFormat.AverageBytesPerSecond / (double) this.inputProvider.WaveFormat.AverageBytesPerSecond;
      long inputPosition = (long) ((double) outputPosition / num);
      if (inputPosition % (long) this.inputProvider.WaveFormat.BlockAlign != 0L)
        inputPosition -= inputPosition % (long) this.inputProvider.WaveFormat.BlockAlign;
      return inputPosition;
    }

    public override long Length
    {
      get
      {
        return this.inputStream != null ? this.InputToOutputPosition(this.inputStream.Length) : throw new InvalidOperationException("Cannot report length if the input was an IWaveProvider");
      }
    }

    public override long Position
    {
      get => this.position;
      set
      {
        if (this.inputStream == null)
          throw new InvalidOperationException("Cannot set position if the input was not a WaveStream");
        this.inputStream.Position = this.OutputToInputPosition(value);
        this.position = this.InputToOutputPosition(this.inputStream.Position);
        this.dmoResampler.MediaObject.Discontinuity(0);
      }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      int num = 0;
      while (num < count)
      {
        if (this.dmoResampler.MediaObject.IsAcceptingData(0))
        {
          int inputPosition = (int) this.OutputToInputPosition((long) (count - num));
          byte[] numArray = new byte[inputPosition];
          int bytes = this.inputProvider.Read(numArray, 0, inputPosition);
          if (bytes != 0)
          {
            this.inputMediaBuffer.LoadData(numArray, bytes);
            this.dmoResampler.MediaObject.ProcessInput(0, (IMediaBuffer) this.inputMediaBuffer, DmoInputDataBufferFlags.None, 0L, 0L);
            this.outputBuffer.MediaBuffer.SetLength(0);
            this.outputBuffer.StatusFlags = DmoOutputDataBufferFlags.None;
            this.dmoResampler.MediaObject.ProcessOutput(DmoProcessOutputFlags.None, 1, new DmoOutputDataBuffer[1]
            {
              this.outputBuffer
            });
            if (this.outputBuffer.Length != 0)
            {
              this.outputBuffer.RetrieveData(buffer, offset + num);
              num += this.outputBuffer.Length;
            }
            else
              break;
          }
          else
            break;
        }
      }
      this.position += (long) num;
      return num;
    }

    protected override void Dispose(bool disposing)
    {
      if (this.inputMediaBuffer != null)
      {
        this.inputMediaBuffer.Dispose();
        this.inputMediaBuffer = (MediaBuffer) null;
      }
      this.outputBuffer.Dispose();
      if (this.dmoResampler != null)
        this.dmoResampler = (DmoResampler) null;
      base.Dispose(disposing);
    }
  }
}
