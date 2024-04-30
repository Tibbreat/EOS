// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.IMediaObject
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

#nullable disable
namespace NAudio.Dmo
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [SuppressUnmanagedCodeSecurity]
  [Guid("d8ad0f58-5494-4102-97c5-ec798e59bcf4")]
  [ComImport]
  internal interface IMediaObject
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetStreamCount(out int inputStreams, out int outputStreams);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetInputStreamInfo(int inputStreamIndex, out InputStreamInfoFlags flags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetOutputStreamInfo(int outputStreamIndex, out OutputStreamInfoFlags flags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetInputType(int inputStreamIndex, int typeIndex, out DmoMediaType mediaType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetOutputType(int outputStreamIndex, int typeIndex, out DmoMediaType mediaType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetInputType(int inputStreamIndex, [In] ref DmoMediaType mediaType, DmoSetTypeFlags flags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetOutputType(int outputStreamIndex, [In] ref DmoMediaType mediaType, DmoSetTypeFlags flags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetInputCurrentType(int inputStreamIndex, out DmoMediaType mediaType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetOutputCurrentType(int outputStreamIndex, out DmoMediaType mediaType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetInputSizeInfo(
      int inputStreamIndex,
      out int size,
      out int maxLookahead,
      out int alignment);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetOutputSizeInfo(int outputStreamIndex, out int size, out int alignment);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetInputMaxLatency(int inputStreamIndex, out long referenceTimeMaxLatency);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetInputMaxLatency(int inputStreamIndex, long referenceTimeMaxLatency);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Flush();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Discontinuity(int inputStreamIndex);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AllocateStreamingResources();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FreeStreamingResources();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetInputStatus(int inputStreamIndex, out DmoInputStatusFlags flags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ProcessInput(
      int inputStreamIndex,
      [In] IMediaBuffer mediaBuffer,
      DmoInputDataBufferFlags flags,
      long referenceTimeTimestamp,
      long referenceTimeDuration);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ProcessOutput(
      DmoProcessOutputFlags flags,
      int outputBufferCount,
      [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1), In, Out] DmoOutputDataBuffer[] outputBuffers,
      out int statusReserved);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Lock(bool acquireLock);
  }
}
