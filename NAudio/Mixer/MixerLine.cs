// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.MixerLine
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Mixer
{
  public class MixerLine
  {
    private MixerInterop.MIXERLINE mixerLine;
    private IntPtr mixerHandle;
    private MixerFlags mixerHandleType;

    public MixerLine(IntPtr mixerHandle, int destinationIndex, MixerFlags mixerHandleType)
    {
      this.mixerHandle = mixerHandle;
      this.mixerHandleType = mixerHandleType;
      this.mixerLine = new MixerInterop.MIXERLINE();
      this.mixerLine.cbStruct = Marshal.SizeOf((object) this.mixerLine);
      this.mixerLine.dwDestination = destinationIndex;
      MmException.Try(MixerInterop.mixerGetLineInfo(mixerHandle, ref this.mixerLine, mixerHandleType), "mixerGetLineInfo");
    }

    public MixerLine(
      IntPtr mixerHandle,
      int destinationIndex,
      int sourceIndex,
      MixerFlags mixerHandleType)
    {
      this.mixerHandle = mixerHandle;
      this.mixerHandleType = mixerHandleType;
      this.mixerLine = new MixerInterop.MIXERLINE();
      this.mixerLine.cbStruct = Marshal.SizeOf((object) this.mixerLine);
      this.mixerLine.dwDestination = destinationIndex;
      this.mixerLine.dwSource = sourceIndex;
      MmException.Try(MixerInterop.mixerGetLineInfo(mixerHandle, ref this.mixerLine, mixerHandleType | MixerFlags.ListText), "mixerGetLineInfo");
    }

    public static int GetMixerIdForWaveIn(int waveInDevice)
    {
      int mixerID = -1;
      MmException.Try(MixerInterop.mixerGetID((IntPtr) waveInDevice, out mixerID, MixerFlags.WaveIn), "mixerGetID");
      return mixerID;
    }

    public string Name => this.mixerLine.szName;

    public string ShortName => this.mixerLine.szShortName;

    public int LineId => this.mixerLine.dwLineID;

    public MixerLineComponentType ComponentType => this.mixerLine.dwComponentType;

    public string TypeDescription
    {
      get
      {
        switch (this.mixerLine.dwComponentType)
        {
          case MixerLineComponentType.DestinationUndefined:
            return "Undefined Destination";
          case MixerLineComponentType.DestinationDigital:
            return "Digital Destination";
          case MixerLineComponentType.DestinationLine:
            return "Line Level Destination";
          case MixerLineComponentType.DestinationMonitor:
            return "Monitor Destination";
          case MixerLineComponentType.DestinationSpeakers:
            return "Speakers Destination";
          case MixerLineComponentType.DestinationHeadphones:
            return "Headphones Destination";
          case MixerLineComponentType.DestinationTelephone:
            return "Telephone Destination";
          case MixerLineComponentType.DestinationWaveIn:
            return "Wave Input Destination";
          case MixerLineComponentType.DestinationVoiceIn:
            return "Voice Recognition Destination";
          case MixerLineComponentType.SourceUndefined:
            return "Undefined Source";
          case MixerLineComponentType.SourceDigital:
            return "Digital Source";
          case MixerLineComponentType.SourceLine:
            return "Line Level Source";
          case MixerLineComponentType.SourceMicrophone:
            return "Microphone Source";
          case MixerLineComponentType.SourceSynthesizer:
            return "Synthesizer Source";
          case MixerLineComponentType.SourceCompactDisc:
            return "Compact Disk Source";
          case MixerLineComponentType.SourceTelephone:
            return "Telephone Source";
          case MixerLineComponentType.SourcePcSpeaker:
            return "PC Speaker Source";
          case MixerLineComponentType.SourceWaveOut:
            return "Wave Out Source";
          case MixerLineComponentType.SourceAuxiliary:
            return "Auxiliary Source";
          case MixerLineComponentType.SourceAnalog:
            return "Analog Source";
          default:
            return "Invalid Component Type";
        }
      }
    }

    public int Channels => this.mixerLine.cChannels;

    public int SourceCount => this.mixerLine.cConnections;

    public int ControlsCount => this.mixerLine.cControls;

    public bool IsActive
    {
      get
      {
        return (this.mixerLine.fdwLine & MixerInterop.MIXERLINE_LINEF.MIXERLINE_LINEF_ACTIVE) != (MixerInterop.MIXERLINE_LINEF) 0;
      }
    }

    public bool IsDisconnected
    {
      get
      {
        return (this.mixerLine.fdwLine & MixerInterop.MIXERLINE_LINEF.MIXERLINE_LINEF_DISCONNECTED) != (MixerInterop.MIXERLINE_LINEF) 0;
      }
    }

    public bool IsSource
    {
      get
      {
        return (this.mixerLine.fdwLine & MixerInterop.MIXERLINE_LINEF.MIXERLINE_LINEF_SOURCE) != (MixerInterop.MIXERLINE_LINEF) 0;
      }
    }

    public MixerLine GetSource(int sourceIndex)
    {
      if (sourceIndex < 0 || sourceIndex >= this.SourceCount)
        throw new ArgumentOutOfRangeException(nameof (sourceIndex));
      return new MixerLine(this.mixerHandle, this.mixerLine.dwDestination, sourceIndex, this.mixerHandleType);
    }

    public IEnumerable<MixerControl> Controls
    {
      get
      {
        return (IEnumerable<MixerControl>) MixerControl.GetMixerControls(this.mixerHandle, this, this.mixerHandleType);
      }
    }

    public IEnumerable<MixerLine> Sources
    {
      get
      {
        for (int source = 0; source < this.SourceCount; ++source)
          yield return this.GetSource(source);
      }
    }

    public string TargetName => this.mixerLine.szPname;

    public override string ToString()
    {
      return string.Format("{0} {1} ({2} controls, ID={3})", (object) this.Name, (object) this.TypeDescription, (object) this.ControlsCount, (object) this.mixerLine.dwLineID);
    }
  }
}
