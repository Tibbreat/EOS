// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmDriverDetails
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Compression
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  internal struct AcmDriverDetails
  {
    private const int ShortNameChars = 32;
    private const int LongNameChars = 128;
    private const int CopyrightChars = 80;
    private const int LicensingChars = 128;
    private const int FeaturesChars = 512;
    public int structureSize;
    public uint fccType;
    public uint fccComp;
    public ushort manufacturerId;
    public ushort productId;
    public uint acmVersion;
    public uint driverVersion;
    public AcmDriverDetailsSupportFlags supportFlags;
    public int formatTagsCount;
    public int filterTagsCount;
    public IntPtr hicon;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string shortName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string longName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
    public string copyright;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string licensing;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
    public string features;
  }
}
