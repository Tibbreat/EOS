// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.SampleHeaderBuilder
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System.IO;

#nullable disable
namespace NAudio.SoundFont
{
  internal class SampleHeaderBuilder : StructureBuilder<SampleHeader>
  {
    public override SampleHeader Read(BinaryReader br)
    {
      SampleHeader sampleHeader = new SampleHeader();
      byte[] bytes = br.ReadBytes(20);
      sampleHeader.SampleName = ByteEncoding.Instance.GetString(bytes, 0, bytes.Length);
      sampleHeader.Start = br.ReadUInt32();
      sampleHeader.End = br.ReadUInt32();
      sampleHeader.StartLoop = br.ReadUInt32();
      sampleHeader.EndLoop = br.ReadUInt32();
      sampleHeader.SampleRate = br.ReadUInt32();
      sampleHeader.OriginalPitch = br.ReadByte();
      sampleHeader.PitchCorrection = br.ReadSByte();
      sampleHeader.SampleLink = br.ReadUInt16();
      sampleHeader.SFSampleLink = (SFSampleLink) br.ReadUInt16();
      this.data.Add(sampleHeader);
      return sampleHeader;
    }

    public override void Write(BinaryWriter bw, SampleHeader sampleHeader)
    {
    }

    public override int Length => 46;

    internal void RemoveEOS() => this.data.RemoveAt(this.data.Count - 1);

    public SampleHeader[] SampleHeaders => this.data.ToArray();
  }
}
