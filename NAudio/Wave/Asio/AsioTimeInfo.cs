// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.AsioTimeInfo
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave.Asio
{
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  internal struct AsioTimeInfo
  {
    public double speed;
    public ASIO64Bit systemTime;
    public ASIO64Bit samplePosition;
    public double sampleRate;
    public AsioTimeInfoFlags flags;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
    public string reserved;
  }
}
