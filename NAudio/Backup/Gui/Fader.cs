// Decompiled with JetBrains decompiler
// Type: NAudio.Gui.Fader
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace NAudio.Gui
{
  public class Fader : Control
  {
    private int minimum;
    private int maximum;
    private float percent;
    private Orientation orientation;
    private System.ComponentModel.Container components;
    private readonly int SliderHeight = 30;
    private readonly int SliderWidth = 15;
    private Rectangle sliderRectangle = new Rectangle();
    private bool dragging;
    private int dragY;

    public Fader()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void DrawSlider(Graphics g)
    {
      Brush brush = (Brush) new SolidBrush(Color.White);
      Pen pen = new Pen(Color.Black);
      this.sliderRectangle.X = (this.Width - this.SliderWidth) / 2;
      this.sliderRectangle.Width = this.SliderWidth;
      this.sliderRectangle.Y = (int) ((double) (this.Height - this.SliderHeight) * (double) this.percent);
      this.sliderRectangle.Height = this.SliderHeight;
      g.FillRectangle(brush, this.sliderRectangle);
      g.DrawLine(pen, this.sliderRectangle.Left, this.sliderRectangle.Top + this.sliderRectangle.Height / 2, this.sliderRectangle.Right, this.sliderRectangle.Top + this.sliderRectangle.Height / 2);
      brush.Dispose();
      pen.Dispose();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      if (this.Orientation == Orientation.Vertical)
      {
        Brush brush = (Brush) new SolidBrush(Color.Black);
        graphics.FillRectangle(brush, this.Width / 2, this.SliderHeight / 2, 2, this.Height - this.SliderHeight);
        brush.Dispose();
        this.DrawSlider(graphics);
      }
      base.OnPaint(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.sliderRectangle.Contains(e.X, e.Y))
      {
        this.dragging = true;
        this.dragY = e.Y - this.sliderRectangle.Y;
      }
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.dragging)
      {
        int num = e.Y - this.dragY;
        this.percent = num >= 0 ? (num <= this.Height - this.SliderHeight ? (float) num / (float) (this.Height - this.SliderHeight) : 1f) : 0.0f;
        this.Invalidate();
      }
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.dragging = false;
      base.OnMouseUp(e);
    }

    public int Minimum
    {
      get => this.minimum;
      set => this.minimum = value;
    }

    public int Maximum
    {
      get => this.maximum;
      set => this.maximum = value;
    }

    public int Value
    {
      get => (int) ((double) this.percent * (double) (this.maximum - this.minimum)) + this.minimum;
      set => this.percent = (float) (value - this.minimum) / (float) (this.maximum - this.minimum);
    }

    public Orientation Orientation
    {
      get => this.orientation;
      set => this.orientation = value;
    }

    private void InitializeComponent() => this.components = new System.ComponentModel.Container();
  }
}
