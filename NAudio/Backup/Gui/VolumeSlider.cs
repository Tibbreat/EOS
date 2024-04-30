// Decompiled with JetBrains decompiler
// Type: NAudio.Gui.VolumeSlider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace NAudio.Gui
{
  public class VolumeSlider : UserControl
  {
    private System.ComponentModel.Container components;
    private float volume = 1f;
    private float MinDb = -48f;

    public event EventHandler VolumeChanged;

    public VolumeSlider() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.Name = nameof (VolumeSlider);
      this.Size = new Size(96, 16);
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
      StringFormat format = new StringFormat();
      format.LineAlignment = StringAlignment.Center;
      format.Alignment = StringAlignment.Center;
      pe.Graphics.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);
      float num1 = 20f * (float) Math.Log10((double) this.Volume);
      float num2 = (float) (1.0 - (double) num1 / (double) this.MinDb);
      pe.Graphics.FillRectangle(Brushes.LightGreen, 1, 1, (int) ((double) (this.Width - 2) * (double) num2), this.Height - 2);
      string s = string.Format("{0:F2} dB", (object) num1);
      pe.Graphics.DrawString(s, this.Font, Brushes.Black, (RectangleF) this.ClientRectangle, format);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
        this.SetVolumeFromMouse(e.X);
      base.OnMouseMove(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.SetVolumeFromMouse(e.X);
      base.OnMouseDown(e);
    }

    private void SetVolumeFromMouse(int x)
    {
      float num = (float) (1.0 - (double) x / (double) this.Width) * this.MinDb;
      if (x <= 0)
        this.Volume = 0.0f;
      else
        this.Volume = (float) Math.Pow(10.0, (double) num / 20.0);
    }

    [DefaultValue(1f)]
    public float Volume
    {
      get => this.volume;
      set
      {
        if ((double) value < 0.0)
          value = 0.0f;
        if ((double) value > 1.0)
          value = 1f;
        if ((double) this.volume == (double) value)
          return;
        this.volume = value;
        if (this.VolumeChanged != null)
          this.VolumeChanged((object) this, EventArgs.Empty);
        this.Invalidate();
      }
    }
  }
}
