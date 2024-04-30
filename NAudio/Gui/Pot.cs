// Decompiled with JetBrains decompiler
// Type: NAudio.Gui.Pot
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace NAudio.Gui
{
  public class Pot : UserControl
  {
    private double minimum;
    private double maximum = 1.0;
    private double value = 0.5;
    private int beginDragY;
    private double beginDragValue;
    private bool dragging;
    private IContainer components;

    public event EventHandler ValueChanged;

    public Pot()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
      this.InitializeComponent();
    }

    public double Minimum
    {
      get => this.minimum;
      set
      {
        this.minimum = value < this.maximum ? value : throw new ArgumentOutOfRangeException("Minimum must be less than maximum");
        if (this.Value >= this.minimum)
          return;
        this.Value = this.minimum;
      }
    }

    public double Maximum
    {
      get => this.maximum;
      set
      {
        this.maximum = value > this.minimum ? value : throw new ArgumentOutOfRangeException("Maximum must be greater than minimum");
        if (this.Value <= this.maximum)
          return;
        this.Value = this.maximum;
      }
    }

    public double Value
    {
      get => this.value;
      set => this.SetValue(value, false);
    }

    private void SetValue(double newValue, bool raiseEvents)
    {
      if (this.value == newValue)
        return;
      this.value = newValue;
      if (raiseEvents && this.ValueChanged != null)
        this.ValueChanged((object) this, EventArgs.Empty);
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      int num1 = Math.Min(this.Width - 4, this.Height - 4);
      Pen pen = new Pen(this.ForeColor, 3f);
      pen.LineJoin = LineJoin.Round;
      GraphicsState gstate = e.Graphics.Save();
      e.Graphics.TranslateTransform((float) (this.Width / 2), (float) (this.Height / 2));
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      e.Graphics.DrawArc(pen, new Rectangle(num1 / -2, num1 / -2, num1, num1), 135f, 270f);
      double num2 = 135.0 + (this.value - this.minimum) / (this.maximum - this.minimum) * 270.0;
      double x2 = (double) num1 / 2.0 * Math.Cos(Math.PI * num2 / 180.0);
      double y2 = (double) num1 / 2.0 * Math.Sin(Math.PI * num2 / 180.0);
      e.Graphics.DrawLine(pen, 0.0f, 0.0f, (float) x2, (float) y2);
      e.Graphics.Restore(gstate);
      base.OnPaint(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.dragging = true;
      this.beginDragY = e.Y;
      this.beginDragValue = this.value;
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.dragging = false;
      base.OnMouseUp(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.dragging)
      {
        double newValue = this.beginDragValue + (this.maximum - this.minimum) * ((double) (this.beginDragY - e.Y) / 150.0);
        if (newValue < this.minimum)
          newValue = this.minimum;
        if (newValue > this.maximum)
          newValue = this.maximum;
        this.SetValue(newValue, true);
      }
      base.OnMouseMove(e);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = nameof (Pot);
      this.Size = new Size(32, 32);
      this.ResumeLayout(false);
    }
  }
}
