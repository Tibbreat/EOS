// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioCaptureClient
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("C8ADBD64-E71E-48a0-A4DE-185C395CD317")]
  internal interface IAudioCaptureClient
  {
    int GetBuffer(
      out IntPtr dataBuffer,
      out int numFramesToRead,
      out AudioClientBufferFlags bufferFlags,
      out long devicePosition,
      out long qpcPosition);

    int ReleaseBuffer(int numFramesRead);

    int GetNextPacketSize(out int numFramesInNextPacket);
  }
}
