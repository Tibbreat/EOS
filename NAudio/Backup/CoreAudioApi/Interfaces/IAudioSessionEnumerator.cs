// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.Interfaces.IAudioSessionEnumerator
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi.Interfaces
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8")]
  internal interface IAudioSessionEnumerator
  {
    int GetCount(out int sessionCount);

    int GetSession(int sessionCount, out IAudioSessionControl session);
  }
}
