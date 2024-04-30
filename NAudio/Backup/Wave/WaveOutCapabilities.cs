// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.WaveOutCapabilities
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Wave
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  public struct WaveOutCapabilities
  {
    private const int MaxProductNameLength = 32;
    private short manufacturerId;
    private short productId;
    private int driverVersion;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    private string productName;
    private SupportedWaveFormat supportedFormats;
    private short channels;
    private short reserved;
    private WaveOutSupport support;
    private Guid manufacturerGuid;
    private Guid productGuid;
    private Guid nameGuid;

    public int Channels => (int) this.channels;

    public bool SupportsPlaybackRateControl
    {
      get => (this.support & WaveOutSupport.PlaybackRate) == WaveOutSupport.PlaybackRate;
    }

    public string ProductName => this.productName;

    public bool SupportsWaveFormat(SupportedWaveFormat waveFormat)
    {
      return (this.supportedFormats & waveFormat) == waveFormat;
    }

    public Guid NameGuid => this.nameGuid;

    public Guid ProductGuid => this.productGuid;

    public Guid ManufacturerGuid => this.manufacturerGuid;
  }
}
