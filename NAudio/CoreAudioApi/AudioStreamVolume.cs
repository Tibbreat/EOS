// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioStreamVolume
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioStreamVolume : IDisposable
  {
    private IAudioStreamVolume audioStreamVolumeInterface;

    internal AudioStreamVolume(IAudioStreamVolume audioStreamVolumeInterface)
    {
      this.audioStreamVolumeInterface = audioStreamVolumeInterface;
    }

    private void CheckChannelIndex(int channelIndex, string parameter)
    {
      int channelCount = this.ChannelCount;
      if (channelIndex >= channelCount)
        throw new ArgumentOutOfRangeException(parameter, "You must supply a valid channel index < current count of channels: " + channelCount.ToString());
    }

    public float[] GetAllVolumes()
    {
      uint dwCount;
      Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetChannelCount(out dwCount));
      float[] pfVolumes = new float[(IntPtr) dwCount];
      Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetAllVolumes(dwCount, pfVolumes));
      return pfVolumes;
    }

    public int ChannelCount
    {
      get
      {
        uint dwCount;
        Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetChannelCount(out dwCount));
        return (int) dwCount;
      }
    }

    public float GetChannelVolume(int channelIndex)
    {
      this.CheckChannelIndex(channelIndex, nameof (channelIndex));
      float fLevel;
      Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetChannelVolume((uint) channelIndex, out fLevel));
      return fLevel;
    }

    public void SetAllVolumes(float[] levels)
    {
      int channelCount = this.ChannelCount;
      if (levels == null)
        throw new ArgumentNullException(nameof (levels));
      if (levels.Length != channelCount)
        throw new ArgumentOutOfRangeException(nameof (levels), string.Format((IFormatProvider) CultureInfo.InvariantCulture, "SetAllVolumes MUST be supplied with a volume level for ALL channels. The AudioStream has {0} channels and you supplied {1} channels.", (object) channelCount, (object) levels.Length));
      for (int index = 0; index < levels.Length; ++index)
      {
        float level = levels[index];
        if ((double) level < 0.0)
          throw new ArgumentOutOfRangeException(nameof (levels), "All volumes must be between 0.0 and 1.0. Invalid volume at index: " + index.ToString());
        if ((double) level > 1.0)
          throw new ArgumentOutOfRangeException(nameof (levels), "All volumes must be between 0.0 and 1.0. Invalid volume at index: " + index.ToString());
      }
      Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.SetAllVoumes((uint) channelCount, levels));
    }

    public void SetChannelVolume(int index, float level)
    {
      this.CheckChannelIndex(index, nameof (index));
      if ((double) level < 0.0)
        throw new ArgumentOutOfRangeException(nameof (level), "Volume must be between 0.0 and 1.0");
      if ((double) level > 1.0)
        throw new ArgumentOutOfRangeException(nameof (level), "Volume must be between 0.0 and 1.0");
      Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.SetChannelVolume((uint) index, level));
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.audioStreamVolumeInterface == null)
        return;
      Marshal.ReleaseComObject((object) this.audioStreamVolumeInterface);
      this.audioStreamVolumeInterface = (IAudioStreamVolume) null;
    }
  }
}
