// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveHeader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  [StructLayout(LayoutKind.Sequential)]
  internal class WaveHeader
  {
    public IntPtr dataBuffer;
    public int bufferLength;
    public int bytesRecorded;
    public IntPtr userData;
    public WaveHeaderFlags flags;
    public int loops;
    public IntPtr next;
    public IntPtr reserved;
  }
}
