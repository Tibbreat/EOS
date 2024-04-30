// Decompiled with JetBrains decompiler
// Type: NAudio.CoreAudioApi.AudioMeterInformation
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.CoreAudioApi
{
  public class AudioMeterInformation
  {
    private readonly IAudioMeterInformation audioMeterInformation;
    private readonly EEndpointHardwareSupport hardwareSupport;
    private readonly AudioMeterInformationChannels channels;

    internal AudioMeterInformation(IAudioMeterInformation realInterface)
    {
      this.audioMeterInformation = realInterface;
      int pdwHardwareSupportMask;
      Marshal.ThrowExceptionForHR(this.audioMeterInformation.QueryHardwareSupport(out pdwHardwareSupportMask));
      this.hardwareSupport = (EEndpointHardwareSupport) pdwHardwareSupportMask;
      this.channels = new AudioMeterInformationChannels(this.audioMeterInformation);
    }

    public AudioMeterInformationChannels PeakValues => this.channels;

    public EEndpointHardwareSupport HardwareSupport => this.hardwareSupport;

    public float MasterPeakValue
    {
      get
      {
        float pfPeak;
        Marshal.ThrowExceptionForHR(this.audioMeterInformation.GetPeakValue(out pfPeak));
        return pfPeak;
      }
    }
  }
}
