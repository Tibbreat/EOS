// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.SampleHeader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.SoundFont
{
  public class SampleHeader
  {
    public string SampleName;
    public uint Start;
    public uint End;
    public uint StartLoop;
    public uint EndLoop;
    public uint SampleRate;
    public byte OriginalPitch;
    public sbyte PitchCorrection;
    public ushort SampleLink;
    public SFSampleLink SFSampleLink;

    public override string ToString() => this.SampleName;
  }
}
