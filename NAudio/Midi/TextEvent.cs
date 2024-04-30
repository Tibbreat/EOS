// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.TextEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using NAudio.Utils;
using System.IO;

#nullable disable
namespace NAudio.Midi
{
  public class TextEvent : MetaEvent
  {
    private string text;

    public TextEvent(BinaryReader br, int length)
    {
      this.text = ByteEncoding.Instance.GetString(br.ReadBytes(length));
    }

    public TextEvent(string text, MetaEventType metaEventType, long absoluteTime)
      : base(metaEventType, text.Length, absoluteTime)
    {
      this.text = text;
    }

    public string Text
    {
      get => this.text;
      set
      {
        this.text = value;
        this.metaDataLength = this.text.Length;
      }
    }

    public override string ToString()
    {
      return string.Format("{0} {1}", (object) base.ToString(), (object) this.text);
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      byte[] bytes = ByteEncoding.Instance.GetBytes(this.text);
      writer.Write(bytes);
    }
  }
}
