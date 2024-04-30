// Decompiled with JetBrains decompiler
// Type: NAudio.Dmo.MediaObjectSizeInfo
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.Dmo
{
  public class MediaObjectSizeInfo
  {
    public int Size { get; private set; }

    public int MaxLookahead { get; private set; }

    public int Alignment { get; private set; }

    public MediaObjectSizeInfo(int size, int maxLookahead, int alignment)
    {
      this.Size = size;
      this.MaxLookahead = maxLookahead;
      this.Alignment = alignment;
    }

    public override string ToString()
    {
      return string.Format("Size: {0}, Alignment {1}, MaxLookahead {2}", (object) this.Size, (object) this.Alignment, (object) this.MaxLookahead);
    }
  }
}
