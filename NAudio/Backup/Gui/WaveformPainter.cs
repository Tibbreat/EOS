// Decompiled with JetBrains decompiler
// Type: NAudio.Gui.WaveformPainter
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace NAudio.Gui
{
  public class WaveformPainter : Control
  {
    private Pen foregroundPen;
    private List<float> samples = new List<float>(1000);
    private int maxSamples;
    private int insertPos;
    private IContainer components;

    public WaveformPainter()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.InitializeComponent();
      this.OnForeColorChanged(EventArgs.Empty);
      this.OnResize(EventArgs.Empty);
    }

    protected override void OnResize(EventArgs e)
    {
      this.maxSamples = this.Width;
      base.OnResize(e);
    }

    protected override void OnForeColorChanged(EventArgs e)
    {
      this.foregroundPen = new Pen(this.ForeColor);
      base.OnForeColorChanged(e);
    }

    public void AddMax(float maxSample)
    {
      if (this.maxSamples == 0)
        return;
      if (this.samples.Count <= this.maxSamples)
        this.samples.Add(maxSample);
      else if (this.insertPos < this.maxSamples)
        this.samples[this.insertPos] = maxSample;
      ++this.insertPos;
      this.insertPos %= this.maxSamples;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
      base.OnPaint(pe);
      for (int index = 0; index < this.Width; ++index)
      {
        float num = (float) this.Height * this.GetSample(index - this.Width + this.insertPos);
        float y1 = (float) (((double) this.Height - (double) num) / 2.0);
        pe.Graphics.DrawLine(this.foregroundPen, (float) index, y1, (float) index, y1 + num);
      }
    }

    private float GetSample(int index)
    {
      if (index < 0)
        index += this.maxSamples;
      return index >= 0 & index < this.samples.Count ? this.samples[index] : 0.0f;
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
