// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.AsioAudioAvailableEventArgs
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Wave.Asio;
using System;

#nullable disable
namespace NAudio.Wave
{
  public class AsioAudioAvailableEventArgs : EventArgs
  {
    public AsioAudioAvailableEventArgs(
      IntPtr[] inputBuffers,
      IntPtr[] outputBuffers,
      int samplesPerBuffer,
      AsioSampleType asioSampleType)
    {
      this.InputBuffers = inputBuffers;
      this.OutputBuffers = outputBuffers;
      this.SamplesPerBuffer = samplesPerBuffer;
      this.AsioSampleType = asioSampleType;
    }

    public IntPtr[] InputBuffers { get; private set; }

    public IntPtr[] OutputBuffers { get; private set; }

    public bool WrittenToOutputBuffers { get; set; }

    public int SamplesPerBuffer { get; private set; }

    public unsafe int GetAsInterleavedSamples(float[] samples)
    {
      int length = this.InputBuffers.Length;
      if (samples.Length < this.SamplesPerBuffer * length)
        throw new ArgumentException("Buffer not big enough");
      int num1 = 0;
      if (this.AsioSampleType == AsioSampleType.Int32LSB)
      {
        for (int index1 = 0; index1 < this.SamplesPerBuffer; ++index1)
        {
          for (int index2 = 0; index2 < length; ++index2)
            samples[num1++] = (float) *(int*) ((IntPtr) (void*) this.InputBuffers[index2] + (IntPtr) index1 * 4) / (float) int.MaxValue;
        }
      }
      else if (this.AsioSampleType == AsioSampleType.Int16LSB)
      {
        for (int index3 = 0; index3 < this.SamplesPerBuffer; ++index3)
        {
          for (int index4 = 0; index4 < length; ++index4)
            samples[num1++] = (float) *(short*) ((IntPtr) (void*) this.InputBuffers[index4] + (IntPtr) index3 * 2) / (float) short.MaxValue;
        }
      }
      else if (this.AsioSampleType == AsioSampleType.Int24LSB)
      {
        for (int index5 = 0; index5 < this.SamplesPerBuffer; ++index5)
        {
          for (int index6 = 0; index6 < length; ++index6)
          {
            byte* numPtr = (byte*) ((IntPtr) (void*) this.InputBuffers[index6] + (IntPtr) index5 * 3);
            int num2 = (int) *numPtr | (int) numPtr[1] << 8 | (int) (sbyte) numPtr[2] << 16;
            samples[num1++] = (float) num2 / 8388608f;
          }
        }
      }
      else
      {
        if (this.AsioSampleType != AsioSampleType.Float32LSB)
          throw new NotImplementedException(string.Format("ASIO Sample Type {0} not supported", (object) this.AsioSampleType));
        for (int index7 = 0; index7 < this.SamplesPerBuffer; ++index7)
        {
          for (int index8 = 0; index8 < length; ++index8)
            samples[num1++] = *(float*) ((IntPtr) (void*) this.InputBuffers[index8] + (IntPtr) index7 * 4);
        }
      }
      return this.SamplesPerBuffer * length;
    }

    public AsioSampleType AsioSampleType { get; private set; }

    [Obsolete("Better performance if you use the overload that takes an array, and reuse the same one")]
    public float[] GetAsInterleavedSamples()
    {
      float[] samples = new float[this.SamplesPerBuffer * this.InputBuffers.Length];
      this.GetAsInterleavedSamples(samples);
      return samples;
    }
  }
}
