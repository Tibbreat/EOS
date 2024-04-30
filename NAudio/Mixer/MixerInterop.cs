﻿// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.MixerInterop
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Mixer
{
  internal class MixerInterop
  {
    public const uint MIXERCONTROL_CONTROLF_UNIFORM = 1;
    public const uint MIXERCONTROL_CONTROLF_MULTIPLE = 2;
    public const uint MIXERCONTROL_CONTROLF_DISABLED = 2147483648;
    public const int MAXPNAMELEN = 32;
    public const int MIXER_SHORT_NAME_CHARS = 16;
    public const int MIXER_LONG_NAME_CHARS = 64;

    [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
    public static extern int mixerGetNumDevs();

    [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
    public static extern MmResult mixerOpen(
      out IntPtr hMixer,
      int uMxId,
      IntPtr dwCallback,
      IntPtr dwInstance,
      MixerFlags dwOpenFlags);

    [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
    public static extern MmResult mixerClose(IntPtr hMixer);

    [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
    public static extern MmResult mixerGetControlDetails(
      IntPtr hMixer,
      ref MixerInterop.MIXERCONTROLDETAILS mixerControlDetails,
      MixerFlags dwDetailsFlags);

    [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
    public static extern MmResult mixerGetDevCaps(
      IntPtr nMixerID,
      ref MixerInterop.MIXERCAPS mixerCaps,
      int mixerCapsSize);

    [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
    public static extern MmResult mixerGetID(
      IntPtr hMixer,
      out int mixerID,
      MixerFlags dwMixerIDFlags);

    [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
    public static extern MmResult mixerGetLineControls(
      IntPtr hMixer,
      ref MixerInterop.MIXERLINECONTROLS mixerLineControls,
      MixerFlags dwControlFlags);

    [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
    public static extern MmResult mixerGetLineInfo(
      IntPtr hMixer,
      ref MixerInterop.MIXERLINE mixerLine,
      MixerFlags dwInfoFlags);

    [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
    public static extern MmResult mixerMessage(
      IntPtr hMixer,
      uint nMessage,
      IntPtr dwParam1,
      IntPtr dwParam2);

    [DllImport("winmm.dll", CharSet = CharSet.Ansi)]
    public static extern MmResult mixerSetControlDetails(
      IntPtr hMixer,
      ref MixerInterop.MIXERCONTROLDETAILS mixerControlDetails,
      MixerFlags dwDetailsFlags);

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
    public struct MIXERCONTROLDETAILS
    {
      public int cbStruct;
      public int dwControlID;
      public int cChannels;
      public IntPtr hwndOwner;
      public int cbDetails;
      public IntPtr paDetails;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MIXERCAPS
    {
      public ushort wMid;
      public ushort wPid;
      public uint vDriverVersion;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string szPname;
      public uint fdwSupport;
      public uint cDestinations;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MIXERLINECONTROLS
    {
      public int cbStruct;
      public int dwLineID;
      public int dwControlID;
      public int cControls;
      public int cbmxctrl;
      public IntPtr pamxctrl;
    }

    [Flags]
    public enum MIXERLINE_LINEF
    {
      MIXERLINE_LINEF_ACTIVE = 1,
      MIXERLINE_LINEF_DISCONNECTED = 32768, // 0x00008000
      MIXERLINE_LINEF_SOURCE = -2147483648, // 0x80000000
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MIXERLINE
    {
      public int cbStruct;
      public int dwDestination;
      public int dwSource;
      public int dwLineID;
      public MixerInterop.MIXERLINE_LINEF fdwLine;
      public IntPtr dwUser;
      public MixerLineComponentType dwComponentType;
      public int cChannels;
      public int cConnections;
      public int cControls;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
      public string szShortName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
      public string szName;
      public uint dwType;
      public uint dwDeviceID;
      public ushort wMid;
      public ushort wPid;
      public uint vDriverVersion;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string szPname;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Bounds
    {
      public int minimum;
      public int maximum;
      public int reserved2;
      public int reserved3;
      public int reserved4;
      public int reserved5;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Metrics
    {
      public int step;
      public int customData;
      public int reserved2;
      public int reserved3;
      public int reserved4;
      public int reserved5;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MIXERCONTROL
    {
      public uint cbStruct;
      public int dwControlID;
      public MixerControlType dwControlType;
      public uint fdwControl;
      public uint cMultipleItems;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
      public string szShortName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
      public string szName;
      public MixerInterop.Bounds Bounds;
      public MixerInterop.Metrics Metrics;
    }

    public struct MIXERCONTROLDETAILS_BOOLEAN
    {
      public int fValue;
    }

    public struct MIXERCONTROLDETAILS_SIGNED
    {
      public int lValue;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MIXERCONTROLDETAILS_LISTTEXT
    {
      public uint dwParam1;
      public uint dwParam2;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
      public string szName;
    }

    public struct MIXERCONTROLDETAILS_UNSIGNED
    {
      public uint dwValue;
    }
  }
}
