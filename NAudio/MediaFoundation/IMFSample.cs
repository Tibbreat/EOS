// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.IMFSample
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace NAudio.MediaFoundation
{
  [Guid("c40a00f2-b93a-4d80-ae8c-5a1c634f58e4")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IMFSample : IMFAttributes
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

    void GetSampleFlags(out int pdwSampleFlags);

    void SetSampleFlags(int dwSampleFlags);

    void GetSampleTime(out long phnsSampletime);

    void SetSampleTime(long hnsSampleTime);

    void GetSampleDuration(out long phnsSampleDuration);

    void SetSampleDuration(long hnsSampleDuration);

    void GetBufferCount(out int pdwBufferCount);

    void GetBufferByIndex(int dwIndex, out IMFMediaBuffer ppBuffer);

    void ConvertToContiguousBuffer(out IMFMediaBuffer ppBuffer);

    void AddBuffer(IMFMediaBuffer pBuffer);

    void RemoveBufferByIndex(int dwIndex);

    void RemoveAllBuffers();

    void GetTotalLength(out int pcbTotalLength);

    void CopyToBuffer(IMFMediaBuffer pBuffer);
  }
}
