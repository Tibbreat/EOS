// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Asio.AsioDriverCapability
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Wave.Asio
{
  internal class AsioDriverCapability
  {
    public string DriverName;
    public int NbInputChannels;
    public int NbOutputChannels;
    public int InputLatency;
    public int OutputLatency;
    public int BufferMinSize;
    public int BufferMaxSize;
    public int BufferPreferredSize;
    public int BufferGranularity;
    public double SampleRate;
    public ASIOChannelInfo[] InputChannelInfos;
    public ASIOChannelInfo[] OutputChannelInfos;
  }
}
