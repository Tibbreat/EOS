// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioCaptureClient
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioCaptureClient : IDisposable
  {
    private IAudioCaptureClient audioCaptureClientInterface;

    internal AudioCaptureClient(IAudioCaptureClient audioCaptureClientInterface)
    {
      this.audioCaptureClientInterface = audioCaptureClientInterface;
    }

    public IntPtr GetBuffer(
      out int numFramesToRead,
      out AudioClientBufferFlags bufferFlags,
      out long devicePosition,
      out long qpcPosition)
    {
      IntPtr dataBuffer;
      Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.GetBuffer(out dataBuffer, out numFramesToRead, out bufferFlags, out devicePosition, out qpcPosition));
      return dataBuffer;
    }

    public IntPtr GetBuffer(out int numFramesToRead, out AudioClientBufferFlags bufferFlags)
    {
      IntPtr dataBuffer;
      Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.GetBuffer(out dataBuffer, out numFramesToRead, out bufferFlags, out long _, out long _));
      return dataBuffer;
    }

    public int GetNextPacketSize()
    {
      int numFramesInNextPacket;
      Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.GetNextPacketSize(out numFramesInNextPacket));
      return numFramesInNextPacket;
    }

    public void ReleaseBuffer(int numFramesWritten)
    {
      Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.ReleaseBuffer(numFramesWritten));
    }

    public void Dispose()
    {
      if (this.audioCaptureClientInterface == null)
        return;
      Marshal.ReleaseComObject((object) this.audioCaptureClientInterface);
      this.audioCaptureClientInterface = (IAudioCaptureClient) null;
      GC.SuppressFinalize((object) this);
    }
  }
}
