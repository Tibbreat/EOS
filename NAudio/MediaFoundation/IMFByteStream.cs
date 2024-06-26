﻿// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.IMFByteStream
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("ad4c1b00-4bf7-422f-9175-756693d9130d")]
  [ComImport]
  public interface IMFByteStream
  {
    void GetCapabilities(ref int pdwCapabiities);

    void GetLength(ref long pqwLength);

    void SetLength(long qwLength);

    void GetCurrentPosition(ref long pqwPosition);

    void SetCurrentPosition(long qwPosition);

    void IsEndOfStream([MarshalAs(UnmanagedType.Bool)] ref bool pfEndOfStream);

    void Read(IntPtr pb, int cb, ref int pcbRead);

    void BeginRead(IntPtr pb, int cb, IntPtr pCallback, IntPtr punkState);

    void EndRead(IntPtr pResult, ref int pcbRead);

    void Write(IntPtr pb, int cb, ref int pcbWritten);

    void BeginWrite(IntPtr pb, int cb, IntPtr pCallback, IntPtr punkState);

    void EndWrite(IntPtr pResult, ref int pcbWritten);

    void Seek(int SeekOrigin, long llSeekOffset, int dwSeekFlags, ref long pqwCurrentPosition);

    void Flush();

    void Close();
  }
}
