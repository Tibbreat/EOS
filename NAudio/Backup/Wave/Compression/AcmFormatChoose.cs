// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.AcmFormatChoose
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Compression
{
  [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
  internal struct AcmFormatChoose
  {
    public int structureSize;
    public AcmFormatChooseStyleFlags styleFlags;
    public IntPtr ownerWindowHandle;
    public IntPtr selectedWaveFormatPointer;
    public int selectedWaveFormatByteSize;
    [MarshalAs(UnmanagedType.LPTStr)]
    public string title;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
    public string formatTagDescription;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string formatDescription;
    [MarshalAs(UnmanagedType.LPTStr)]
    public string name;
    public int nameByteSize;
    public AcmFormatEnumFlags formatEnumFlags;
    public IntPtr waveFormatEnumPointer;
    public IntPtr instanceHandle;
    [MarshalAs(UnmanagedType.LPTStr)]
    public string templateName;
    public IntPtr customData;
    public AcmInterop.AcmFormatChooseHookProc windowCallbackFunction;
  }
}
