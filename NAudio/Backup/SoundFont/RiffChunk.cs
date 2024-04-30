// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.RiffChunk
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System;
using System.IO;

#nullable disable
namespace NAudio.SoundFont
{
  internal class RiffChunk
  {
    private string chunkID;
    private uint chunkSize;
    private long dataOffset;
    private BinaryReader riffFile;

    public static RiffChunk GetTopLevelChunk(BinaryReader file)
    {
      RiffChunk topLevelChunk = new RiffChunk(file);
      topLevelChunk.ReadChunk();
      return topLevelChunk;
    }

    private RiffChunk(BinaryReader file)
    {
      this.riffFile = file;
      this.chunkID = "????";
      this.chunkSize = 0U;
      this.dataOffset = 0L;
    }

    public string ReadChunkID()
    {
      byte[] bytes = this.riffFile.ReadBytes(4);
      if (bytes.Length != 4)
        throw new InvalidDataException("Couldn't read Chunk ID");
      return ByteEncoding.Instance.GetString(bytes, 0, bytes.Length);
    }

    private void ReadChunk()
    {
      this.chunkID = this.ReadChunkID();
      this.chunkSize = this.riffFile.ReadUInt32();
      this.dataOffset = this.riffFile.BaseStream.Position;
    }

    public RiffChunk GetNextSubChunk()
    {
      if (this.riffFile.BaseStream.Position + 8L >= this.dataOffset + (long) this.chunkSize)
        return (RiffChunk) null;
      RiffChunk nextSubChunk = new RiffChunk(this.riffFile);
      nextSubChunk.ReadChunk();
      return nextSubChunk;
    }

    public byte[] GetData()
    {
      this.riffFile.BaseStream.Position = this.dataOffset;
      byte[] data = this.riffFile.ReadBytes((int) this.chunkSize);
      if ((long) data.Length != (long) this.chunkSize)
        throw new InvalidDataException(string.Format("Couldn't read chunk's data Chunk: {0}, read {1} bytes", (object) this, (object) data.Length));
      return data;
    }

    public string GetDataAsString()
    {
      byte[] data = this.GetData();
      return data == null ? (string) null : ByteEncoding.Instance.GetString(data, 0, data.Length);
    }

    public T GetDataAsStructure<T>(StructureBuilder<T> s)
    {
      this.riffFile.BaseStream.Position = this.dataOffset;
      if ((long) s.Length != (long) this.chunkSize)
        throw new InvalidDataException(string.Format("Chunk size is: {0} so can't read structure of: {1}", (object) this.chunkSize, (object) s.Length));
      return s.Read(this.riffFile);
    }

    public T[] GetDataAsStructureArray<T>(StructureBuilder<T> s)
    {
      this.riffFile.BaseStream.Position = this.dataOffset;
      if ((long) this.chunkSize % (long) s.Length != 0L)
        throw new InvalidDataException(string.Format("Chunk size is: {0} not a multiple of structure size: {1}", (object) this.chunkSize, (object) s.Length));
      int length = (int) ((long) this.chunkSize / (long) s.Length);
      T[] asStructureArray = new T[length];
      for (int index = 0; index < length; ++index)
        asStructureArray[index] = s.Read(this.riffFile);
      return asStructureArray;
    }

    public string ChunkID
    {
      get => this.chunkID;
      set
      {
        if (value == null)
          throw new ArgumentNullException("ChunkID may not be null");
        this.chunkID = value.Length == 4 ? value : throw new ArgumentException("ChunkID must be four characters");
      }
    }

    public uint ChunkSize => this.chunkSize;

    public long DataOffset => this.dataOffset;

    public override string ToString()
    {
      return string.Format("RiffChunk ID: {0} Size: {1} Data Offset: {2}", (object) this.ChunkID, (object) this.ChunkSize, (object) this.DataOffset);
    }
  }
}
