// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.CueWaveFileWriter
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;

#nullable disable
namespace NAudio.Wave
{
  public class CueWaveFileWriter : WaveFileWriter
  {
    private CueList cues;

    public CueWaveFileWriter(string fileName, WaveFormat waveFormat)
      : base(fileName, waveFormat)
    {
    }

    public void AddCue(int position, string label)
    {
      if (this.cues == null)
        this.cues = new CueList();
      this.cues.Add(new Cue(position, label));
    }

    private void WriteCues(BinaryWriter w)
    {
      if (this.cues == null)
        return;
      int length = this.cues.GetRIFFChunks().Length;
      w.Seek(0, SeekOrigin.End);
      if (w.BaseStream.Length % 2L == 1L)
        w.Write((byte) 0);
      w.Write(this.cues.GetRIFFChunks(), 0, length);
      w.Seek(4, SeekOrigin.Begin);
      w.Write((int) (w.BaseStream.Length - 8L));
    }

    protected override void UpdateHeader(BinaryWriter writer)
    {
      base.UpdateHeader(writer);
      this.WriteCues(writer);
    }
  }
}
