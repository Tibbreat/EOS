// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.MediaFoundationEncoder
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.MediaFoundation;
using NAudio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  public class MediaFoundationEncoder : IDisposable
  {
    private readonly MediaType outputMediaType;
    private bool disposed;

    public static int[] GetEncodeBitrates(Guid audioSubtype, int sampleRate, int channels)
    {
      return ((IEnumerable<MediaType>) MediaFoundationEncoder.GetOutputMediaTypes(audioSubtype)).Where<MediaType>((Func<MediaType, bool>) (mt => mt.SampleRate == sampleRate && mt.ChannelCount == channels)).Select<MediaType, int>((Func<MediaType, int>) (mt => mt.AverageBytesPerSecond * 8)).Distinct<int>().OrderBy<int, int>((Func<int, int>) (br => br)).ToArray<int>();
    }

    public static MediaType[] GetOutputMediaTypes(Guid audioSubtype)
    {
      IMFCollection ppAvailableTypes;
      try
      {
        MediaFoundationInterop.MFTranscodeGetAudioOutputAvailableTypes(audioSubtype, _MFT_ENUM_FLAG.MFT_ENUM_FLAG_ALL, (IMFAttributes) null, out ppAvailableTypes);
      }
      catch (COMException ex)
      {
        if (ex.GetHResult() == -1072875819)
          return new MediaType[0];
        throw;
      }
      int pcElements;
      ppAvailableTypes.GetElementCount(out pcElements);
      List<MediaType> mediaTypeList = new List<MediaType>(pcElements);
      for (int dwElementIndex = 0; dwElementIndex < pcElements; ++dwElementIndex)
      {
        object ppUnkElement;
        ppAvailableTypes.GetElement(dwElementIndex, out ppUnkElement);
        IMFMediaType mediaType = (IMFMediaType) ppUnkElement;
        mediaTypeList.Add(new MediaType(mediaType));
      }
      Marshal.ReleaseComObject((object) ppAvailableTypes);
      return mediaTypeList.ToArray();
    }

    public static void EncodeToWma(
      IWaveProvider inputProvider,
      string outputFile,
      int desiredBitRate = 192000)
    {
      MediaType outputMediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_WMAudioV8, inputProvider.WaveFormat, desiredBitRate);
      if (outputMediaType == null)
        throw new InvalidOperationException("No suitable WMA encoders available");
      using (MediaFoundationEncoder foundationEncoder = new MediaFoundationEncoder(outputMediaType))
        foundationEncoder.Encode(outputFile, inputProvider);
    }

    public static void EncodeToMp3(
      IWaveProvider inputProvider,
      string outputFile,
      int desiredBitRate = 192000)
    {
      MediaType outputMediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_MP3, inputProvider.WaveFormat, desiredBitRate);
      if (outputMediaType == null)
        throw new InvalidOperationException("No suitable MP3 encoders available");
      using (MediaFoundationEncoder foundationEncoder = new MediaFoundationEncoder(outputMediaType))
        foundationEncoder.Encode(outputFile, inputProvider);
    }

    public static void EncodeToAac(
      IWaveProvider inputProvider,
      string outputFile,
      int desiredBitRate = 192000)
    {
      MediaType outputMediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_AAC, inputProvider.WaveFormat, desiredBitRate);
      if (outputMediaType == null)
        throw new InvalidOperationException("No suitable AAC encoders available");
      using (MediaFoundationEncoder foundationEncoder = new MediaFoundationEncoder(outputMediaType))
        foundationEncoder.Encode(outputFile, inputProvider);
    }

    public static MediaType SelectMediaType(
      Guid audioSubtype,
      WaveFormat inputFormat,
      int desiredBitRate)
    {
      return ((IEnumerable<MediaType>) MediaFoundationEncoder.GetOutputMediaTypes(audioSubtype)).Where<MediaType>((Func<MediaType, bool>) (mt => mt.SampleRate == inputFormat.SampleRate && mt.ChannelCount == inputFormat.Channels)).Select(mt => new
      {
        MediaType = mt,
        Delta = Math.Abs(desiredBitRate - mt.AverageBytesPerSecond * 8)
      }).OrderBy(mt => mt.Delta).Select(mt => mt.MediaType).FirstOrDefault<MediaType>();
    }

    public MediaFoundationEncoder(MediaType outputMediaType)
    {
      this.outputMediaType = outputMediaType != null ? outputMediaType : throw new ArgumentNullException(nameof (outputMediaType));
    }

    public void Encode(string outputFile, IWaveProvider inputProvider)
    {
      if (inputProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm && inputProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
        throw new ArgumentException("Encode input format must be PCM or IEEE float");
      MediaType mediaType = new MediaType(inputProvider.WaveFormat);
      IMFSinkWriter sinkWriter = MediaFoundationEncoder.CreateSinkWriter(outputFile);
      try
      {
        int pdwStreamIndex;
        sinkWriter.AddStream(this.outputMediaType.MediaFoundationObject, out pdwStreamIndex);
        sinkWriter.SetInputMediaType(pdwStreamIndex, mediaType.MediaFoundationObject, (IMFAttributes) null);
        this.PerformEncode(sinkWriter, pdwStreamIndex, inputProvider);
      }
      finally
      {
        Marshal.ReleaseComObject((object) sinkWriter);
        Marshal.ReleaseComObject((object) mediaType.MediaFoundationObject);
      }
    }

    private static IMFSinkWriter CreateSinkWriter(string outputFile)
    {
      IMFAttributes attributes = MediaFoundationApi.CreateAttributes(1);
      attributes.SetUINT32(MediaFoundationAttributes.MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS, 1);
      IMFSinkWriter ppSinkWriter;
      try
      {
        MediaFoundationInterop.MFCreateSinkWriterFromURL(outputFile, (IMFByteStream) null, attributes, out ppSinkWriter);
      }
      catch (COMException ex)
      {
        if (ex.GetHResult() == -1072875819)
          throw new ArgumentException("Was not able to create a sink writer for this file extension");
        throw;
      }
      finally
      {
        Marshal.ReleaseComObject((object) attributes);
      }
      return ppSinkWriter;
    }

    private void PerformEncode(IMFSinkWriter writer, int streamIndex, IWaveProvider inputProvider)
    {
      byte[] managedBuffer = new byte[inputProvider.WaveFormat.AverageBytesPerSecond * 4];
      writer.BeginWriting();
      long position = 0;
      long num;
      do
      {
        num = this.ConvertOneBuffer(writer, streamIndex, inputProvider, position, managedBuffer);
        position += num;
      }
      while (num > 0L);
      writer.DoFinalize();
    }

    private static long BytesToNsPosition(int bytes, WaveFormat waveFormat)
    {
      return 10000000L * (long) bytes / (long) waveFormat.AverageBytesPerSecond;
    }

    private long ConvertOneBuffer(
      IMFSinkWriter writer,
      int streamIndex,
      IWaveProvider inputProvider,
      long position,
      byte[] managedBuffer)
    {
      long hnsSampleDuration = 0;
      IMFMediaBuffer memoryBuffer = MediaFoundationApi.CreateMemoryBuffer(managedBuffer.Length);
      int pcbMaxLength;
      memoryBuffer.GetMaxLength(out pcbMaxLength);
      IMFSample sample = MediaFoundationApi.CreateSample();
      sample.AddBuffer(memoryBuffer);
      IntPtr ppbBuffer;
      memoryBuffer.Lock(out ppbBuffer, out pcbMaxLength, out int _);
      int num = inputProvider.Read(managedBuffer, 0, pcbMaxLength);
      if (num > 0)
      {
        hnsSampleDuration = MediaFoundationEncoder.BytesToNsPosition(num, inputProvider.WaveFormat);
        Marshal.Copy(managedBuffer, 0, ppbBuffer, num);
        memoryBuffer.SetCurrentLength(num);
        memoryBuffer.Unlock();
        sample.SetSampleTime(position);
        sample.SetSampleDuration(hnsSampleDuration);
        writer.WriteSample(streamIndex, sample);
      }
      else
        memoryBuffer.Unlock();
      Marshal.ReleaseComObject((object) sample);
      Marshal.ReleaseComObject((object) memoryBuffer);
      return hnsSampleDuration;
    }

    protected void Dispose(bool disposing)
    {
      Marshal.ReleaseComObject((object) this.outputMediaType.MediaFoundationObject);
    }

    public void Dispose()
    {
      if (!this.disposed)
      {
        this.disposed = true;
        this.Dispose(true);
      }
      GC.SuppressFinalize((object) this);
    }

    ~MediaFoundationEncoder() => this.Dispose(false);
  }
}
