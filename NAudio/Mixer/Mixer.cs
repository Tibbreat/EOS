// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.Mixer
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.Mixer
{
  public class Mixer
  {
    private MixerInterop.MIXERCAPS caps;
    private IntPtr mixerHandle;
    private MixerFlags mixerHandleType;

    public static int NumberOfDevices => MixerInterop.mixerGetNumDevs();

    public Mixer(int mixerIndex)
    {
      if (mixerIndex < 0 || mixerIndex >= NAudio.Mixer.Mixer.NumberOfDevices)
        throw new ArgumentOutOfRangeException("mixerID");
      this.caps = new MixerInterop.MIXERCAPS();
      MmException.Try(MixerInterop.mixerGetDevCaps((IntPtr) mixerIndex, ref this.caps, Marshal.SizeOf((object) this.caps)), "mixerGetDevCaps");
      this.mixerHandle = (IntPtr) mixerIndex;
      this.mixerHandleType = MixerFlags.Mixer;
    }

    public int DestinationCount => (int) this.caps.cDestinations;

    public string Name => this.caps.szPname;

    public Manufacturers Manufacturer => (Manufacturers) this.caps.wMid;

    public int ProductID => (int) this.caps.wPid;

    public MixerLine GetDestination(int destinationIndex)
    {
      if (destinationIndex < 0 || destinationIndex >= this.DestinationCount)
        throw new ArgumentOutOfRangeException(nameof (destinationIndex));
      return new MixerLine(this.mixerHandle, destinationIndex, this.mixerHandleType);
    }

    public IEnumerable<MixerLine> Destinations
    {
      get
      {
        for (int destination = 0; destination < this.DestinationCount; ++destination)
          yield return this.GetDestination(destination);
      }
    }

    public static IEnumerable<NAudio.Mixer.Mixer> Mixers
    {
      get
      {
        for (int device = 0; device < NAudio.Mixer.Mixer.NumberOfDevices; ++device)
          yield return new NAudio.Mixer.Mixer(device);
      }
    }
  }
}
