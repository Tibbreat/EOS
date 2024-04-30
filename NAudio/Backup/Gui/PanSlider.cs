// Decompiled with JetBrains decompiler
// Type: NAudio.Gui.PanSlider
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace NAudio.Gui
{
  public class PanSlider : UserControl
  {
    private System.ComponentModel.Container components;
    private float pan;

    public event EventHandler PanChanged;

    public PanSlider() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.Name = nameof (PanSlider);
      this.Size = new Size(104, 16);
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
      StringFormat format = new StringFormat();
      format.LineAlignment = StringAlignment.Center;
      format.Alignment = StringAlignment.Center;
      string s;
      if ((double) this.pan == 0.0)
      {
        pe.Graphics.FillRectangle(Brushes.Orange, this.Width / 2 - 1, 1, 3, this.Height - 2);
        s = "C";
      }
      else if ((double) this.pan > 0.0)
      {
        pe.Graphics.FillRectangle(Brushes.Orange, this.Width / 2, 1, (int) ((double) (this.Width / 2) * (double) this.pan), this.Height - 2);
        s = string.Format("{0:F0}%R", (object) (float) ((double) this.pan * 100.0));
      }
      else
      {
        pe.Graphics.FillRectangle(Brushes.Orange, (int) ((double) (this.Width / 2) * ((double) this.pan + 1.0)), 1, (int) ((double) (this.Width / 2) * (0.0 - (double) this.pan)), this.Height - 2);
        s = string.Format("{0:F0}%L", (object) (float) ((double) this.pan * -100.0));
      }
      pe.Graphics.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);
      pe.Graphics.DrawString(s, this.Font, Brushes.Black, (RectangleF) this.ClientRectangle, format);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
        this.SetPanFromMouse(e.X);
      base.OnMouseMove(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.SetPanFromMouse(e.X);
      base.OnMouseDown(e);
    }

    private void SetPanFromMouse(int x)
    {
      this.Pan = (float) ((double) x / (double) this.Width * 2.0 - 1.0);
    }

    public float Pan
    {
      get => this.pan;
      set
      {
        if ((double) value < -1.0)
          value = -1f;
        if ((double) value > 1.0)
          value = 1f;
        if ((double) value == (double) this.pan)
          return;
        this.pan = value;
        if (this.PanChanged != null)
          this.PanChanged((object) this, EventArgs.Empty);
        this.Invalidate();
      }
    }
  }
}
