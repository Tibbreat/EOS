// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.SequencerSpecificEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.IO;
using System.Text;

#nullable disable
namespace NAudio.Midi
{
  public class SequencerSpecificEvent : MetaEvent
  {
    private byte[] data;

    public SequencerSpecificEvent(BinaryReader br, int length) => this.data = br.ReadBytes(length);

    public SequencerSpecificEvent(byte[] data, long absoluteTime)
      : base(MetaEventType.SequencerSpecific, data.Length, absoluteTime)
    {
      this.data = data;
    }

    public byte[] Data
    {
      get => this.data;
      set
      {
        this.data = value;
        this.metaDataLength = this.data.Length;
      }
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(base.ToString());
      stringBuilder.Append(" ");
      foreach (byte num in this.data)
        stringBuilder.AppendFormat("{0:X2} ", (object) num);
      --stringBuilder.Length;
      return stringBuilder.ToString();
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write(this.data);
    }
  }
}
