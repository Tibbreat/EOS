// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.IMediaBuffer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

#nullable disable
namespace NAudio.Dmo
{
  [SuppressUnmanagedCodeSecurity]
  [Guid("59eff8b9-938c-4a26-82f2-95cb84cdc837")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IMediaBuffer
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetLength(int length);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMaxLength(out int maxLength);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetBufferAndLength(IntPtr bufferPointerPointer, IntPtr validDataLengthPointer);
  }
}
