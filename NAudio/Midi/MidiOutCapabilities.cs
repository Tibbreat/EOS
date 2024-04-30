// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.MidiOutCapabilities
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Midi
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  public struct MidiOutCapabilities
  {
    private const int MaxProductNameLength = 32;
    private short manufacturerId;
    private short productId;
    private int driverVersion;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    private string productName;
    private short wTechnology;
    private short wVoices;
    private short wNotes;
    private ushort wChannelMask;
    private MidiOutCapabilities.MidiOutCapabilityFlags dwSupport;

    public Manufacturers Manufacturer => (Manufacturers) this.manufacturerId;

    public short ProductId => this.productId;

    public string ProductName => this.productName;

    public int Voices => (int) this.wVoices;

    public int Notes => (int) this.wNotes;

    public bool SupportsAllChannels => this.wChannelMask == ushort.MaxValue;

    public bool SupportsChannel(int channel) => ((int) this.wChannelMask & 1 << channel - 1) > 0;

    public bool SupportsPatchCaching
    {
      get
      {
        return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.PatchCaching) != (MidiOutCapabilities.MidiOutCapabilityFlags) 0;
      }
    }

    public bool SupportsSeparateLeftAndRightVolume
    {
      get
      {
        return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.LeftRightVolume) != (MidiOutCapabilities.MidiOutCapabilityFlags) 0;
      }
    }

    public bool SupportsMidiStreamOut
    {
      get
      {
        return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.Stream) != (MidiOutCapabilities.MidiOutCapabilityFlags) 0;
      }
    }

    public bool SupportsVolumeControl
    {
      get
      {
        return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.Volume) != (MidiOutCapabilities.MidiOutCapabilityFlags) 0;
      }
    }

    public MidiOutTechnology Technology => (MidiOutTechnology) this.wTechnology;

    [Flags]
    private enum MidiOutCapabilityFlags
    {
      Volume = 1,
      LeftRightVolume = 2,
      PatchCaching = 4,
      Stream = 8,
    }
  }
}
