// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.IMFReadWriteClassFactory
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Guid("E7FE2E12-661C-40DA-92F9-4F002AB67627")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IMFReadWriteClassFactory
  {
    void CreateInstanceFromURL(
      [MarshalAs(UnmanagedType.LPStruct), In] Guid clsid,
      [MarshalAs(UnmanagedType.LPWStr), In] string pwszURL,
      [MarshalAs(UnmanagedType.Interface), In] IMFAttributes pAttributes,
      [MarshalAs(UnmanagedType.LPStruct), In] Guid riid,
      [MarshalAs(UnmanagedType.Interface)] out object ppvObject);

    void CreateInstanceFromObject(
      [MarshalAs(UnmanagedType.LPStruct), In] Guid clsid,
      [MarshalAs(UnmanagedType.IUnknown), In] object punkObject,
      [MarshalAs(UnmanagedType.Interface), In] IMFAttributes pAttributes,
      [MarshalAs(UnmanagedType.LPStruct), In] Guid riid,
      [MarshalAs(UnmanagedType.Interface)] out object ppvObject);
  }
}
