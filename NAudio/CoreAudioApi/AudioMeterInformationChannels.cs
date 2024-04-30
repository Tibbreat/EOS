// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioMeterInformationChannels
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioMeterInformationChannels
  {
    private readonly IAudioMeterInformation audioMeterInformation;

    public int Count
    {
      get
      {
        int pnChannelCount;
        Marshal.ThrowExceptionForHR(this.audioMeterInformation.GetMeteringChannelCount(out pnChannelCount));
        return pnChannelCount;
      }
    }

    public float this[int index]
    {
      get
      {
        float[] numArray = new float[this.Count];
        GCHandle gcHandle = GCHandle.Alloc((object) numArray, GCHandleType.Pinned);
        Marshal.ThrowExceptionForHR(this.audioMeterInformation.GetChannelsPeakValues(numArray.Length, gcHandle.AddrOfPinnedObject()));
        gcHandle.Free();
        return numArray[index];
      }
    }

    internal AudioMeterInformationChannels(IAudioMeterInformation parent)
    {
      this.audioMeterInformation = parent;
    }
  }
}
