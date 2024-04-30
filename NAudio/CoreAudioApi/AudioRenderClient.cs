// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioRenderClient
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioRenderClient : IDisposable
  {
    private IAudioRenderClient audioRenderClientInterface;

    internal AudioRenderClient(IAudioRenderClient audioRenderClientInterface)
    {
      this.audioRenderClientInterface = audioRenderClientInterface;
    }

    public IntPtr GetBuffer(int numFramesRequested)
    {
      IntPtr dataBufferPointer;
      Marshal.ThrowExceptionForHR(this.audioRenderClientInterface.GetBuffer(numFramesRequested, out dataBufferPointer));
      return dataBufferPointer;
    }

    public void ReleaseBuffer(int numFramesWritten, AudioClientBufferFlags bufferFlags)
    {
      Marshal.ThrowExceptionForHR(this.audioRenderClientInterface.ReleaseBuffer(numFramesWritten, bufferFlags));
    }

    public void Dispose()
    {
      if (this.audioRenderClientInterface == null)
        return;
      Marshal.ReleaseComObject((object) this.audioRenderClientInterface);
      this.audioRenderClientInterface = (IAudioRenderClient) null;
      GC.SuppressFinalize((object) this);
    }
  }
}
