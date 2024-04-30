// Decompiled with JetBrains decompiler
// Type: NAudio.SoundFont.StructureBuilder`1
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Collections.Generic;
using System.IO;

#nullable disable
namespace NAudio.SoundFont
{
  internal abstract class StructureBuilder<T>
  {
    protected List<T> data;

    public StructureBuilder() => this.Reset();

    public abstract T Read(BinaryReader br);

    public abstract void Write(BinaryWriter bw, T o);

    public abstract int Length { get; }

    public void Reset() => this.data = new List<T>();

    public T[] Data => this.data.ToArray();
  }
}
