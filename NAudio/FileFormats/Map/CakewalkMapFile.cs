// Decompiled with JetBrains decompiler
// Type: NAudio.FileFormats.Map.CakewalkMapFile
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace NAudio.FileFormats.Map
{
  public class CakewalkMapFile
  {
    private int mapEntryCount;
    private readonly List<CakewalkDrumMapping> drumMappings;
    private MapBlockHeader fileHeader1;
    private MapBlockHeader fileHeader2;
    private MapBlockHeader mapNameHeader;
    private MapBlockHeader outputs1Header;
    private MapBlockHeader outputs2Header;
    private MapBlockHeader outputs3Header;
    private int outputs1Count;
    private int outputs2Count;
    private int outputs3Count;
    private string mapName;

    public CakewalkMapFile(string filename)
    {
      using (BinaryReader reader = new BinaryReader((Stream) File.OpenRead(filename), Encoding.Unicode))
      {
        this.drumMappings = new List<CakewalkDrumMapping>();
        this.ReadMapHeader(reader);
        for (int index = 0; index < this.mapEntryCount; ++index)
          this.drumMappings.Add(this.ReadMapEntry(reader));
        this.ReadMapName(reader);
        this.ReadOutputsSection1(reader);
        if (reader.BaseStream.Position == reader.BaseStream.Length)
          return;
        this.ReadOutputsSection2(reader);
        if (reader.BaseStream.Position == reader.BaseStream.Length)
          return;
        this.ReadOutputsSection3(reader);
      }
    }

    public List<CakewalkDrumMapping> DrumMappings => this.drumMappings;

    private void ReadMapHeader(BinaryReader reader)
    {
      this.fileHeader1 = MapBlockHeader.Read(reader);
      this.fileHeader2 = MapBlockHeader.Read(reader);
      this.mapEntryCount = reader.ReadInt32();
    }

    private CakewalkDrumMapping ReadMapEntry(BinaryReader reader)
    {
      CakewalkDrumMapping cakewalkDrumMapping = new CakewalkDrumMapping();
      reader.ReadInt32();
      cakewalkDrumMapping.InNote = reader.ReadInt32();
      reader.ReadInt32();
      reader.ReadInt32();
      reader.ReadInt32();
      reader.ReadInt32();
      reader.ReadInt32();
      reader.ReadInt32();
      cakewalkDrumMapping.VelocityScale = reader.ReadSingle();
      cakewalkDrumMapping.Channel = reader.ReadInt32();
      cakewalkDrumMapping.OutNote = reader.ReadInt32();
      cakewalkDrumMapping.OutPort = reader.ReadInt32();
      cakewalkDrumMapping.VelocityAdjust = reader.ReadInt32();
      char[] chArray = reader.ReadChars(32);
      int length = 0;
      while (length < chArray.Length && chArray[length] != char.MinValue)
        ++length;
      cakewalkDrumMapping.NoteName = new string(chArray, 0, length);
      return cakewalkDrumMapping;
    }

    private void ReadMapName(BinaryReader reader)
    {
      this.mapNameHeader = MapBlockHeader.Read(reader);
      char[] chArray = reader.ReadChars(34);
      int length = 0;
      while (length < chArray.Length && chArray[length] != char.MinValue)
        ++length;
      this.mapName = new string(chArray, 0, length);
      reader.ReadBytes(98);
    }

    private void ReadOutputsSection1(BinaryReader reader)
    {
      this.outputs1Header = MapBlockHeader.Read(reader);
      this.outputs1Count = reader.ReadInt32();
      for (int index = 0; index < this.outputs1Count; ++index)
        reader.ReadBytes(20);
    }

    private void ReadOutputsSection2(BinaryReader reader)
    {
      this.outputs2Header = MapBlockHeader.Read(reader);
      this.outputs2Count = reader.ReadInt32();
      for (int index = 0; index < this.outputs2Count; ++index)
        reader.ReadBytes(24);
    }

    private void ReadOutputsSection3(BinaryReader reader)
    {
      this.outputs3Header = MapBlockHeader.Read(reader);
      if (this.outputs3Header.Length <= 0)
        return;
      this.outputs3Count = reader.ReadInt32();
      for (int index = 0; index < this.outputs3Count; ++index)
        reader.ReadBytes(36);
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("FileHeader1: {0}\r\n", (object) this.fileHeader1);
      stringBuilder.AppendFormat("FileHeader2: {0}\r\n", (object) this.fileHeader2);
      stringBuilder.AppendFormat("MapEntryCount: {0}\r\n", (object) this.mapEntryCount);
      foreach (CakewalkDrumMapping drumMapping in this.drumMappings)
        stringBuilder.AppendFormat("   Map: {0}\r\n", (object) drumMapping);
      stringBuilder.AppendFormat("MapNameHeader: {0}\r\n", (object) this.mapNameHeader);
      stringBuilder.AppendFormat("MapName: {0}\r\n", (object) this.mapName);
      stringBuilder.AppendFormat("Outputs1Header: {0} Count: {1}\r\n", (object) this.outputs1Header, (object) this.outputs1Count);
      stringBuilder.AppendFormat("Outputs2Header: {0} Count: {1}\r\n", (object) this.outputs2Header, (object) this.outputs2Count);
      stringBuilder.AppendFormat("Outputs3Header: {0} Count: {1}\r\n", (object) this.outputs3Header, (object) this.outputs3Count);
      return stringBuilder.ToString();
    }
  }
}
