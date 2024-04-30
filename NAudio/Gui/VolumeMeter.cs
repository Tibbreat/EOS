// Decompiled with JetBrains decompiler
// Type: NAudio.Gui.VolumeMeter
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
  public class VolumeMeter : Control
  {
    private Brush foregroundBrush;
    private float amplitude;
    private IContainer components;

    public VolumeMeter()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.MinDb = -60f;
      this.MaxDb = 18f;
      this.Amplitude = 0.0f;
      this.Orientation = Orientation.Vertical;
      this.InitializeComponent();
      this.OnForeColorChanged(EventArgs.Empty);
    }

    protected override void OnForeColorChanged(EventArgs e)
    {
      this.foregroundBrush = (Brush) new SolidBrush(this.ForeColor);
      base.OnForeColorChanged(e);
    }

    [DefaultValue(-3.0)]
    public float Amplitude
    {
      get => this.amplitude;
      set
      {
        this.amplitude = value;
        this.Invalidate();
      }
    }

    [DefaultValue(-60.0)]
    public float MinDb { get; set; }

    [DefaultValue(18.0)]
    public float MaxDb { get; set; }

    [DefaultValue(Orientation.Vertical)]
    public Orientation Orientation { get; set; }

    protected override void OnPaint(PaintEventArgs pe)
    {
      pe.Graphics.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);
      double num1 = 20.0 * Math.Log10((double) this.Amplitude);
      if (num1 < (double) this.MinDb)
        num1 = (double) this.MinDb;
      if (num1 > (double) this.MaxDb)
        num1 = (double) this.MaxDb;
      double num2 = (num1 - (double) this.MinDb) / ((double) this.MaxDb - (double) this.MinDb);
      int width1 = this.Width - 2;
      int height1 = this.Height - 2;
      if (this.Orientation == Orientation.Horizontal)
      {
        int width2 = (int) ((double) width1 * num2);
        pe.Graphics.FillRectangle(this.foregroundBrush, 1, 1, width2, height1);
      }
      else
      {
        int height2 = (int) ((double) height1 * num2);
        pe.Graphics.FillRectangle(this.foregroundBrush, 1, this.Height - 1 - height2, width1, height2);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
  }
}
