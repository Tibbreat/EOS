// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.IMFMediaBuffer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("045FA593-8799-42b8-BC8D-8968C6453507")]
  [ComImport]
  public interface IMFMediaBuffer
  {
    void Lock(out IntPtr ppbBuffer, out int pcbMaxLength, out int pcbCurrentLength);

    void Unlock();

    void GetCurrentLength(out int pcbCurrentLength);

    void SetCurrentLength(int cbCurrentLength);

    void GetMaxLength(out int pcbMaxLength);
  }
}
