// Decompiled with JetBrains decompiler
// Type: NAudio.Wave.Cue
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Text.RegularExpressions;

#nullable disable
namespace NAudio.Wave
{
  public class Cue
  {
    public int Position { get; private set; }

    public string Label { get; private set; }

    public Cue(int position, string label)
    {
      this.Position = position;
      if (label == null)
        label = "";
      this.Label = Regex.Replace(label, "[^\\u0000-\\u00FF]", "");
    }
  }
}
