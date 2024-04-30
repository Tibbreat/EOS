// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Compression.WaveFilter
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Compression
{
  [StructLayout(LayoutKind.Sequential)]
  public class WaveFilter
  {
    public int StructureSize = Marshal.SizeOf(typeof (WaveFilter));
    public int FilterTag;
    public int Filter;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public int[] Reserved;
  }
}
