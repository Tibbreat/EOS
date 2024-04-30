// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiInCapabilities
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Midi
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  public struct MidiInCapabilities
  {
    private const int MaxProductNameLength = 32;
    private ushort manufacturerId;
    private ushort productId;
    private uint driverVersion;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    private string productName;
    private int support;

    public Manufacturers Manufacturer => (Manufacturers) this.manufacturerId;

    public int ProductId => (int) this.productId;

    public string ProductName => this.productName;
  }
}
