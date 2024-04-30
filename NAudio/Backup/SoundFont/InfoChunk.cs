// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.InfoChunk
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;

#nullable disable
namespace NAudio.SoundFont
{
  public class InfoChunk
  {
    private SFVersion verSoundFont;
    private string waveTableSoundEngine;
    private string bankName;
    private string dataROM;
    private string creationDate;
    private string author;
    private string targetProduct;
    private string copyright;
    private string comments;
    private string tools;
    private SFVersion verROM;

    internal InfoChunk(RiffChunk chunk)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      if (chunk.ReadChunkID() != "INFO")
        throw new InvalidDataException("Not an INFO chunk");
      RiffChunk nextSubChunk;
      while ((nextSubChunk = chunk.GetNextSubChunk()) != null)
      {
        switch (nextSubChunk.ChunkID)
        {
          case "ifil":
            flag1 = true;
            this.verSoundFont = nextSubChunk.GetDataAsStructure<SFVersion>((StructureBuilder<SFVersion>) new SFVersionBuilder());
            continue;
          case "isng":
            flag2 = true;
            this.waveTableSoundEngine = nextSubChunk.GetDataAsString();
            continue;
          case "INAM":
            flag3 = true;
            this.bankName = nextSubChunk.GetDataAsString();
            continue;
          case "irom":
            this.dataROM = nextSubChunk.GetDataAsString();
            continue;
          case "iver":
            this.verROM = nextSubChunk.GetDataAsStructure<SFVersion>((StructureBuilder<SFVersion>) new SFVersionBuilder());
            continue;
          case "ICRD":
            this.creationDate = nextSubChunk.GetDataAsString();
            continue;
          case "IENG":
            this.author = nextSubChunk.GetDataAsString();
            continue;
          case "IPRD":
            this.targetProduct = nextSubChunk.GetDataAsString();
            continue;
          case "ICOP":
            this.copyright = nextSubChunk.GetDataAsString();
            continue;
          case "ICMT":
            this.comments = nextSubChunk.GetDataAsString();
            continue;
          case "ISFT":
            this.tools = nextSubChunk.GetDataAsString();
            continue;
          default:
            throw new InvalidDataException(string.Format("Unknown chunk type {0}", (object) nextSubChunk.ChunkID));
        }
      }
      if (!flag1)
        throw new InvalidDataException("Missing SoundFont version information");
      if (!flag2)
        throw new InvalidDataException("Missing wavetable sound engine information");
      if (!flag3)
        throw new InvalidDataException("Missing SoundFont name information");
    }

    public SFVersion SoundFontVersion => this.verSoundFont;

    public string WaveTableSoundEngine
    {
      get => this.waveTableSoundEngine;
      set => this.waveTableSoundEngine = value;
    }

    public string BankName
    {
      get => this.bankName;
      set => this.bankName = value;
    }

    public string DataROM
    {
      get => this.dataROM;
      set => this.dataROM = value;
    }

    public string CreationDate
    {
      get => this.creationDate;
      set => this.creationDate = value;
    }

    public string Author
    {
      get => this.author;
      set => this.author = value;
    }

    public string TargetProduct
    {
      get => this.targetProduct;
      set => this.targetProduct = value;
    }

    public string Copyright
    {
      get => this.copyright;
      set => this.copyright = value;
    }

    public string Comments
    {
      get => this.comments;
      set => this.comments = value;
    }

    public string Tools
    {
      get => this.tools;
      set => this.tools = value;
    }

    public SFVersion ROMVersion
    {
      get => this.verROM;
      set => this.verROM = value;
    }

    public override string ToString()
    {
      return string.Format("Bank Name: {0}\r\nAuthor: {1}\r\nCopyright: {2}\r\nCreation Date: {3}\r\nTools: {4}\r\nComments: {5}\r\nSound Engine: {6}\r\nSoundFont Version: {7}\r\nTarget Product: {8}\r\nData ROM: {9}\r\nROM Version: {10}", (object) this.BankName, (object) this.Author, (object) this.Copyright, (object) this.CreationDate, (object) this.Tools, (object) "TODO-fix comments", (object) this.WaveTableSoundEngine, (object) this.SoundFontVersion, (object) this.TargetProduct, (object) this.DataROM, (object) this.ROMVersion);
    }
  }
}
