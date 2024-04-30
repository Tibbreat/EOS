// Decompiled with JetBrains decompiler
// Type: NAudio.FileFormats.Map.MapBlockHeader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;

#nullable disable
namespace NAudio.FileFormats.Map
{
  internal class MapBlockHeader
  {
    private int length;
    private int value2;
    private short value3;
    private short value4;

    public static MapBlockHeader Read(BinaryReader reader)
    {
      return new MapBlockHeader()
      {
        length = reader.ReadInt32(),
        value2 = reader.ReadInt32(),
        value3 = reader.ReadInt16(),
        value4 = reader.ReadInt16()
      };
    }

    public override string ToString()
    {
      return string.Format("{0} {1:X8} {2:X4} {3:X4}", (object) this.length, (object) this.value2, (object) this.value3, (object) this.value4);
    }

    public int Length => this.length;
  }
}
