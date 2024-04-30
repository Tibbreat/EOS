// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioRenderClient
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("F294ACFC-3146-4483-A7BF-ADDCA7C260E2")]
  internal interface IAudioRenderClient
  {
    int GetBuffer(int numFramesRequested, out IntPtr dataBufferPointer);

    int ReleaseBuffer(int numFramesWritten, AudioClientBufferFlags bufferFlags);
  }
}
