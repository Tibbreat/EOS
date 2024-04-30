// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.MultiplexingWaveProvider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System;
using System.Collections.Generic;

#nullable disable
namespace NAudio.Wave
{
  public class MultiplexingWaveProvider : IWaveProvider
  {
    private readonly IList<IWaveProvider> inputs;
    private readonly WaveFormat waveFormat;
    private readonly int outputChannelCount;
    private readonly int inputChannelCount;
    private readonly List<int> mappings;
    private readonly int bytesPerSample;
    private byte[] inputBuffer;

    public MultiplexingWaveProvider(IEnumerable<IWaveProvider> inputs, int numberOfOutputChannels)
    {
      this.inputs = (IList<IWaveProvider>) new List<IWaveProvider>(inputs);
      this.outputChannelCount = numberOfOutputChannels;
      if (this.inputs.Count == 0)
        throw new ArgumentException("You must provide at least one input");
      if (numberOfOutputChannels < 1)
        throw new ArgumentException("You must provide at least one output");
      foreach (IWaveProvider input in (IEnumerable<IWaveProvider>) this.inputs)
      {
        if (this.waveFormat == null)
        {
          if (input.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
          {
            this.waveFormat = new WaveFormat(input.WaveFormat.SampleRate, input.WaveFormat.BitsPerSample, numberOfOutputChannels);
          }
          else
          {
            if (input.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
              throw new ArgumentException("Only PCM and 32 bit float are supported");
            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(input.WaveFormat.SampleRate, numberOfOutputChannels);
          }
        }
        else
        {
          if (input.WaveFormat.BitsPerSample != this.waveFormat.BitsPerSample)
            throw new ArgumentException("All inputs must have the same bit depth");
          if (input.WaveFormat.SampleRate != this.waveFormat.SampleRate)
            throw new ArgumentException("All inputs must have the same sample rate");
        }
        this.inputChannelCount += input.WaveFormat.Channels;
      }
      this.bytesPerSample = this.waveFormat.BitsPerSample / 8;
      this.mappings = new List<int>();
      for (int index = 0; index < this.outputChannelCount; ++index)
        this.mappings.Add(index % this.inputChannelCount);
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      int num1 = this.bytesPerSample * this.outputChannelCount;
      int num2 = count / num1;
      int num3 = 0;
      int val1 = 0;
      foreach (IWaveProvider input in (IEnumerable<IWaveProvider>) this.inputs)
      {
        int num4 = this.bytesPerSample * input.WaveFormat.Channels;
        int num5 = num2 * num4;
        this.inputBuffer = BufferHelpers.Ensure(this.inputBuffer, num5);
        int num6 = input.Read(this.inputBuffer, 0, num5);
        val1 = Math.Max(val1, num6 / num4);
        for (int index1 = 0; index1 < input.WaveFormat.Channels; ++index1)
        {
          int num7 = num3 + index1;
          for (int index2 = 0; index2 < this.outputChannelCount; ++index2)
          {
            if (this.mappings[index2] == num7)
            {
              int sourceIndex = index1 * this.bytesPerSample;
              int num8 = offset + index2 * this.bytesPerSample;
              int num9;
              for (num9 = 0; num9 < num2 && sourceIndex < num6; ++num9)
              {
                Array.Copy((Array) this.inputBuffer, sourceIndex, (Array) buffer, num8, this.bytesPerSample);
                num8 += num1;
                sourceIndex += num4;
              }
              for (; num9 < num2; ++num9)
              {
                Array.Clear((Array) buffer, num8, this.bytesPerSample);
                num8 += num1;
              }
            }
          }
        }
        num3 += input.WaveFormat.Channels;
      }
      return val1 * num1;
    }

    public WaveFormat WaveFormat => this.waveFormat;

    public void ConnectInputToOutput(int inputChannel, int outputChannel)
    {
      if (inputChannel < 0 || inputChannel >= this.InputChannelCount)
        throw new ArgumentException("Invalid input channel");
      if (outputChannel < 0 || outputChannel >= this.OutputChannelCount)
        throw new ArgumentException("Invalid output channel");
      this.mappings[outputChannel] = inputChannel;
    }

    public int InputChannelCount => this.inputChannelCount;

    public int OutputChannelCount => this.outputChannelCount;
  }
}
