// Decompiled with JetBrains decompiler
// Type: NAudio.MediaFoundation.MF_SINK_WRITER_STATISTICS
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Runtime.InteropServices;

#nullable disable
namespace NAudio.MediaFoundation
{
  [StructLayout(LayoutKind.Sequential)]
  public class MF_SINK_WRITER_STATISTICS
  {
    public int cb;
    public long llLastTimestampReceived;
    public long llLastTimestampEncoded;
    public long llLastTimestampProcessed;
    public long llLastStreamTickReceived;
    public long llLastSinkSampleRequest;
    public long qwNumSamplesReceived;
    public long qwNumSamplesEncoded;
    public long qwNumSamplesProcessed;
    public long qwNumStreamTicksReceived;
    public int dwByteCountQueued;
    public long qwByteCountProcessed;
    public int dwNumOutstandingSinkSampleRequests;
    public int dwAverageSampleRateReceived;
    public int dwAverageSampleRateEncoded;
    public int dwAverageSampleRateProcessed;
  }
}
