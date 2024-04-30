// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.ASIOSampleConvertor
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;

#nullable disable
namespace NAudio.Wave.Asio
{
  internal class ASIOSampleConvertor
  {
    public static ASIOSampleConvertor.SampleConvertor SelectSampleConvertor(
      WaveFormat waveFormat,
      AsioSampleType asioType)
    {
      ASIOSampleConvertor.SampleConvertor sampleConvertor = (ASIOSampleConvertor.SampleConvertor) null;
      bool flag = waveFormat.Channels == 2;
      switch (asioType)
      {
        case AsioSampleType.Int16LSB:
          switch (waveFormat.BitsPerSample)
          {
            case 16:
              sampleConvertor = flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToShort2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToShortGeneric);
              break;
            case 32:
              sampleConvertor = flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToShort2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToShortGeneric);
              break;
          }
          break;
        case AsioSampleType.Int24LSB:
          switch (waveFormat.BitsPerSample)
          {
            case 16:
              throw new ArgumentException("Not a supported conversion");
            case 32:
              sampleConvertor = new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConverterFloatTo24LSBGeneric);
              break;
          }
          break;
        case AsioSampleType.Int32LSB:
          switch (waveFormat.BitsPerSample)
          {
            case 16:
              sampleConvertor = flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToInt2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorShortToIntGeneric);
              break;
            case 32:
              sampleConvertor = flag ? new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToInt2Channels) : new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConvertorFloatToIntGeneric);
              break;
          }
          break;
        case AsioSampleType.Float32LSB:
          switch (waveFormat.BitsPerSample)
          {
            case 16:
              throw new ArgumentException("Not a supported conversion");
            case 32:
              sampleConvertor = new ASIOSampleConvertor.SampleConvertor(ASIOSampleConvertor.ConverterFloatToFloatGeneric);
              break;
          }
          break;
        default:
          throw new ArgumentException(string.Format("ASIO Buffer Type {0} is not yet supported.", (object) Enum.GetName(typeof (AsioSampleType), (object) asioType)));
      }
      return sampleConvertor;
    }

    public static unsafe void ConvertorShortToInt2Channels(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples)
    {
      short* numPtr1 = (short*) (void*) inputInterleavedBuffer;
      short* asioOutputBuffer1 = (short*) (void*) asioOutputBuffers[0];
      short* asioOutputBuffer2 = (short*) (void*) asioOutputBuffers[1];
      short* numPtr2 = asioOutputBuffer1 + 1;
      short* numPtr3 = asioOutputBuffer2 + 1;
      for (int index = 0; index < nbSamples; ++index)
      {
        *numPtr2 = *numPtr1;
        *numPtr3 = numPtr1[1];
        numPtr1 += 2;
        numPtr2 += 2;
        numPtr3 += 2;
      }
    }

    public static unsafe void ConvertorShortToIntGeneric(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples)
    {
      short* numPtr = (short*) (void*) inputInterleavedBuffer;
      short*[] numPtrArray = new short*[nbChannels];
      for (int index = 0; index < nbChannels; ++index)
      {
        numPtrArray[index] = (short*) (void*) asioOutputBuffers[index];
        ++numPtrArray[index];
      }
      for (int index1 = 0; index1 < nbSamples; ++index1)
      {
        for (int index2 = 0; index2 < nbChannels; ++index2)
        {
          *numPtrArray[index2] = *numPtr++;
          numPtrArray[index2] += 2;
        }
      }
    }

    public static unsafe void ConvertorFloatToInt2Channels(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples)
    {
      float* numPtr = (float*) (void*) inputInterleavedBuffer;
      int* asioOutputBuffer1 = (int*) (void*) asioOutputBuffers[0];
      int* asioOutputBuffer2 = (int*) (void*) asioOutputBuffers[1];
      for (int index = 0; index < nbSamples; ++index)
      {
        *asioOutputBuffer1++ = ASIOSampleConvertor.clampToInt((double) *numPtr);
        *asioOutputBuffer2++ = ASIOSampleConvertor.clampToInt((double) numPtr[1]);
        numPtr += 2;
      }
    }

    public static unsafe void ConvertorFloatToIntGeneric(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples)
    {
      float* numPtr = (float*) (void*) inputInterleavedBuffer;
      int*[] numPtrArray = new int*[nbChannels];
      for (int index = 0; index < nbChannels; ++index)
        numPtrArray[index] = (int*) (void*) asioOutputBuffers[index];
      for (int index1 = 0; index1 < nbSamples; ++index1)
      {
        for (int index2 = 0; index2 < nbChannels; ++index2)
          *numPtrArray[index2]++ = ASIOSampleConvertor.clampToInt((double) *numPtr++);
      }
    }

    public static unsafe void ConvertorShortToShort2Channels(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples)
    {
      short* numPtr = (short*) (void*) inputInterleavedBuffer;
      short* asioOutputBuffer1 = (short*) (void*) asioOutputBuffers[0];
      short* asioOutputBuffer2 = (short*) (void*) asioOutputBuffers[1];
      for (int index = 0; index < nbSamples; ++index)
      {
        *asioOutputBuffer1++ = *numPtr;
        *asioOutputBuffer2++ = numPtr[1];
        numPtr += 2;
      }
    }

    public static unsafe void ConvertorShortToShortGeneric(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples)
    {
      short* numPtr = (short*) (void*) inputInterleavedBuffer;
      short*[] numPtrArray = new short*[nbChannels];
      for (int index = 0; index < nbChannels; ++index)
        numPtrArray[index] = (short*) (void*) asioOutputBuffers[index];
      for (int index1 = 0; index1 < nbSamples; ++index1)
      {
        for (int index2 = 0; index2 < nbChannels; ++index2)
          *numPtrArray[index2]++ = *numPtr++;
      }
    }

    public static unsafe void ConvertorFloatToShort2Channels(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples)
    {
      float* numPtr = (float*) (void*) inputInterleavedBuffer;
      short* asioOutputBuffer1 = (short*) (void*) asioOutputBuffers[0];
      short* asioOutputBuffer2 = (short*) (void*) asioOutputBuffers[1];
      for (int index = 0; index < nbSamples; ++index)
      {
        *asioOutputBuffer1++ = ASIOSampleConvertor.clampToShort((double) *numPtr);
        *asioOutputBuffer2++ = ASIOSampleConvertor.clampToShort((double) numPtr[1]);
        numPtr += 2;
      }
    }

    public static unsafe void ConvertorFloatToShortGeneric(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples)
    {
      float* numPtr = (float*) (void*) inputInterleavedBuffer;
      short*[] numPtrArray = new short*[nbChannels];
      for (int index = 0; index < nbChannels; ++index)
        numPtrArray[index] = (short*) (void*) asioOutputBuffers[index];
      for (int index1 = 0; index1 < nbSamples; ++index1)
      {
        for (int index2 = 0; index2 < nbChannels; ++index2)
          *numPtrArray[index2]++ = ASIOSampleConvertor.clampToShort((double) *numPtr++);
      }
    }

    public static unsafe void ConverterFloatTo24LSBGeneric(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples)
    {
      float* numPtr = (float*) (void*) inputInterleavedBuffer;
      byte*[] numPtrArray = new byte*[nbChannels];
      for (int index = 0; index < nbChannels; ++index)
        numPtrArray[index] = (byte*) (void*) asioOutputBuffers[index];
      for (int index1 = 0; index1 < nbSamples; ++index1)
      {
        for (int index2 = 0; index2 < nbChannels; ++index2)
        {
          int num = ASIOSampleConvertor.clampTo24Bit((double) *numPtr++);
          *numPtrArray[index2]++ = (byte) num;
          *numPtrArray[index2]++ = (byte) (num >> 8);
          *numPtrArray[index2]++ = (byte) (num >> 16);
        }
      }
    }

    public static unsafe void ConverterFloatToFloatGeneric(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples)
    {
      float* numPtr = (float*) (void*) inputInterleavedBuffer;
      float*[] numPtrArray = new float*[nbChannels];
      for (int index = 0; index < nbChannels; ++index)
        numPtrArray[index] = (float*) (void*) asioOutputBuffers[index];
      for (int index1 = 0; index1 < nbSamples; ++index1)
      {
        for (int index2 = 0; index2 < nbChannels; ++index2)
          *numPtrArray[index2]++ = *numPtr++;
      }
    }

    private static int clampTo24Bit(double sampleValue)
    {
      sampleValue = sampleValue < -1.0 ? -1.0 : (sampleValue > 1.0 ? 1.0 : sampleValue);
      return (int) (sampleValue * 8388607.0);
    }

    private static int clampToInt(double sampleValue)
    {
      sampleValue = sampleValue < -1.0 ? -1.0 : (sampleValue > 1.0 ? 1.0 : sampleValue);
      return (int) (sampleValue * (double) int.MaxValue);
    }

    private static short clampToShort(double sampleValue)
    {
      sampleValue = sampleValue < -1.0 ? -1.0 : (sampleValue > 1.0 ? 1.0 : sampleValue);
      return (short) (sampleValue * (double) short.MaxValue);
    }

    public delegate void SampleConvertor(
      IntPtr inputInterleavedBuffer,
      IntPtr[] asioOutputBuffers,
      int nbChannels,
      int nbSamples);
  }
}
