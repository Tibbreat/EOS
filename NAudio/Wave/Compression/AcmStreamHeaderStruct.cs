// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmStreamHeaderStruct
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Compression
{
  [StructLayout(LayoutKind.Sequential, Size = 128)]
  internal class AcmStreamHeaderStruct
  {
    public int cbStruct;
    public AcmStreamHeaderStatusFlags fdwStatus;
    public IntPtr userData;
    public IntPtr sourceBufferPointer;
    public int sourceBufferLength;
    public int sourceBufferLengthUsed;
    public IntPtr sourceUserData;
    public IntPtr destBufferPointer;
    public int destBufferLength;
    public int destBufferLengthUsed;
    public IntPtr destUserData;
  }
}
