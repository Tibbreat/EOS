// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.SFVersionBuilder
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;

#nullable disable
namespace NAudio.SoundFont
{
  internal class SFVersionBuilder : StructureBuilder<SFVersion>
  {
    public override SFVersion Read(BinaryReader br)
    {
      SFVersion sfVersion = new SFVersion();
      sfVersion.Major = br.ReadInt16();
      sfVersion.Minor = br.ReadInt16();
      this.data.Add(sfVersion);
      return sfVersion;
    }

    public override void Write(BinaryWriter bw, SFVersion v)
    {
      bw.Write(v.Major);
      bw.Write(v.Minor);
    }

    public override int Length => 4;
  }
}
