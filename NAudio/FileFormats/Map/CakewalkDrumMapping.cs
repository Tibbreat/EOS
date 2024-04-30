// Decompiled with JetBrains decompiler
// Type: NAudio.FileFormats.Map.CakewalkDrumMapping
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

#nullable disable
namespace NAudio.FileFormats.Map
{
  public class CakewalkDrumMapping
  {
    public string NoteName { get; set; }

    public int InNote { get; set; }

    public int OutNote { get; set; }

    public int OutPort { get; set; }

    public int Channel { get; set; }

    public int VelocityAdjust { get; set; }

    public float VelocityScale { get; set; }

    public override string ToString()
    {
      return string.Format("{0} In:{1} Out:{2} Ch:{3} Port:{4} Vel+:{5} Vel:{6}%", (object) this.NoteName, (object) this.InNote, (object) this.OutNote, (object) this.Channel, (object) this.OutPort, (object) this.VelocityAdjust, (object) (float) ((double) this.VelocityScale * 100.0));
    }
  }
}
