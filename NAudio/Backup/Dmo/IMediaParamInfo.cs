// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.IMediaParamInfo
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
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [SuppressUnmanagedCodeSecurity]
  [Guid("6d6cbb60-a223-44aa-842f-a2f06750be6d")]
  [ComImport]
  internal interface IMediaParamInfo
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetParamCount(out int paramCount);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetParamInfo(int paramIndex, ref MediaParamInfo paramInfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetParamText(int paramIndex, out IntPtr paramText);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetNumTimeFormats(out int numTimeFormats);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSupportedTimeFormat(int formatIndex, out Guid guidTimeFormat);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentTimeFormat(out Guid guidTimeFormat, out int mediaTimeData);
  }
}
