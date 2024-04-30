// Decompiled with JetBrains decompiler
// Type: NAudio.Midi.SysexEvent
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace NAudio.Midi
{
  public class SysexEvent : MidiEvent
  {
    private byte[] data;

    public static SysexEvent ReadSysexEvent(BinaryReader br)
    {
      SysexEvent sysexEvent = new SysexEvent();
      List<byte> byteList = new List<byte>();
      bool flag = true;
      while (flag)
      {
        byte num = br.ReadByte();
        if (num == (byte) 247)
          flag = false;
        else
          byteList.Add(num);
      }
      sysexEvent.data = byteList.ToArray();
      return sysexEvent;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (byte num in this.data)
        stringBuilder.AppendFormat("{0:X2} ", (object) num);
      return string.Format("{0} Sysex: {1} bytes\r\n{2}", (object) this.AbsoluteTime, (object) this.data.Length, (object) stringBuilder.ToString());
    }

    public override void Export(ref long absoluteTime, BinaryWriter writer)
    {
      base.Export(ref absoluteTime, writer);
      writer.Write(this.data, 0, this.data.Length);
      writer.Write((byte) 247);
    }
  }
}
