// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmFormatDetails
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Compression
{
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  internal struct AcmFormatDetails
  {
    public const int FormatDescriptionChars = 128;
    public int structSize;
    public int formatIndex;
    public int formatTag;
    public AcmDriverDetailsSupportFlags supportFlags;
    public IntPtr waveFormatPointer;
    public int waveFormatByteSize;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string formatDescription;
  }
}
