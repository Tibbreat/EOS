// Decompiled with JetBrains decompiler
// Type: NAudio.Mixer.MixerLineComponentType
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Mixer
{
  public enum MixerLineComponentType
  {
    DestinationUndefined = 0,
    DestinationDigital = 1,
    DestinationLine = 2,
    DestinationMonitor = 3,
    DestinationSpeakers = 4,
    DestinationHeadphones = 5,
    DestinationTelephone = 6,
    DestinationWaveIn = 7,
    DestinationVoiceIn = 8,
    SourceUndefined = 4096, // 0x00001000
    SourceDigital = 4097, // 0x00001001
    SourceLine = 4098, // 0x00001002
    SourceMicrophone = 4099, // 0x00001003
    SourceSynthesizer = 4100, // 0x00001004
    SourceCompactDisc = 4101, // 0x00001005
    SourceTelephone = 4102, // 0x00001006
    SourcePcSpeaker = 4103, // 0x00001007
    SourceWaveOut = 4104, // 0x00001008
    SourceAuxiliary = 4105, // 0x00001009
    SourceAnalog = 4106, // 0x0000100A
  }
}
