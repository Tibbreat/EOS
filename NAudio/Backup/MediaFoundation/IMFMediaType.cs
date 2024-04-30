// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.IMFMediaType
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace NAudio.MediaFoundation
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("44AE0FA8-EA31-4109-8D2E-4CAE4997C555")]
  [ComImport]
  public interface IMFMediaType : IMFAttributes
  {
    new void GetItem([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, [In, Out] ref PropVariant pValue);

    new void GetItemType([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, out int pType);

    new void CompareItem([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, IntPtr Value, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

    new void Compare([MarshalAs(UnmanagedType.Interface)] IMFAttributes pTheirs, int MatchType, [MarshalAs(UnmanagedType.Bool)] out bool pbResult);

    new void GetUINT32([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, out int punValue);

    new void GetUINT64([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, out long punValue);

    new void GetDouble([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, out double pfValue);

    new void GetGUID([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, out Guid pguidValue);

    new void GetStringLength([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, out int pcchLength);

    new void GetString([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pwszValue, int cchBufSize, out int pcchLength);

    new void GetAllocatedString([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr)] out string ppwszValue, out int pcchLength);

    new void GetBlobSize([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, out int pcbBlobSize);

    new void GetBlob([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, [MarshalAs(UnmanagedType.LPArray), Out] byte[] pBuf, int cbBufSize, out int pcbBlobSize);

    new void GetAllocatedBlob([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, out IntPtr ip, out int pcbSize);

    new void GetUnknown([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct), In] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

    new void SetItem([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, IntPtr Value);

    new void DeleteItem([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey);

    new void DeleteAllItems();

    new void SetUINT32([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, int unValue);

    new void SetUINT64([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, long unValue);

    new void SetDouble([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, double fValue);

    new void SetGUID([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, [MarshalAs(UnmanagedType.LPStruct), In] Guid guidValue);

    new void SetString([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, [MarshalAs(UnmanagedType.LPWStr), In] string wszValue);

    new void SetBlob([MarshalAs(UnmanagedType.LPStruct), In] Guid guidKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), In] byte[] pBuf, int cbBufSize);

    new void SetUnknown([MarshalAs(UnmanagedType.LPStruct)] Guid guidKey, [MarshalAs(UnmanagedType.IUnknown), In] object pUnknown);

    new void LockStore();

    new void UnlockStore();

    new void GetCount(out int pcItems);

    new void GetItemByIndex(int unIndex, out Guid pGuidKey, [In, Out] ref PropVariant pValue);

    new void CopyAllItems([MarshalAs(UnmanagedType.Interface), In] IMFAttributes pDest);

    void GetMajorType(out Guid pguidMajorType);

    void IsCompressedFormat([MarshalAs(UnmanagedType.Bool)] out bool pfCompressed);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsEqual([MarshalAs(UnmanagedType.Interface), In] IMFMediaType pIMediaType, ref int pdwFlags);

    void GetRepresentation([MarshalAs(UnmanagedType.Struct), In] Guid guidRepresentation, ref IntPtr ppvRepresentation);

    void FreeRepresentation([MarshalAs(UnmanagedType.Struct), In] Guid guidRepresentation, [In] IntPtr pvRepresentation);
  }
}
