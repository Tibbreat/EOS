// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.SoundFont
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;

#nullable disable
namespace NAudio.SoundFont
{
  public class SoundFont
  {
    private InfoChunk info;
    private PresetsChunk presetsChunk;
    private SampleDataChunk sampleData;

    public SoundFont(string fileName)
      : this((Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read))
    {
    }

    public SoundFont(Stream sfFile)
    {
      using (sfFile)
      {
        RiffChunk topLevelChunk = RiffChunk.GetTopLevelChunk(new BinaryReader(sfFile));
        string str = topLevelChunk.ChunkID == "RIFF" ? topLevelChunk.ReadChunkID() : throw new InvalidDataException("Not a RIFF file");
        if (str != "sfbk")
          throw new InvalidDataException(string.Format("Not a SoundFont ({0})", (object) str));
        RiffChunk nextSubChunk = topLevelChunk.GetNextSubChunk();
        this.info = nextSubChunk.ChunkID == "LIST" ? new InfoChunk(nextSubChunk) : throw new InvalidDataException(string.Format("Not info list found ({0})", (object) nextSubChunk.ChunkID));
        this.sampleData = new SampleDataChunk(topLevelChunk.GetNextSubChunk());
        this.presetsChunk = new PresetsChunk(topLevelChunk.GetNextSubChunk());
      }
    }

    public InfoChunk FileInfo => this.info;

    public Preset[] Presets => this.presetsChunk.Presets;

    public Instrument[] Instruments => this.presetsChunk.Instruments;

    public SampleHeader[] SampleHeaders => this.presetsChunk.SampleHeaders;

    public byte[] SampleData => this.sampleData.SampleData;

    public override string ToString()
    {
      return string.Format("Info Chunk:\r\n{0}\r\nPresets Chunk:\r\n{1}", (object) this.info, (object) this.presetsChunk);
    }
  }
}
