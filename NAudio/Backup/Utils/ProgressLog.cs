// Decompiled with JetBrains decompiler
// Type: NAudio.Utils.ProgressLog
// Assembly: NAudio, Version=1.7.3.0, Culture=neutral, PublicKeyToken=null
// MVID: CED8B364-9D50-4A06-A0C3-526F647769F0
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\NAudio.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace NAudio.Utils
{
  public class ProgressLog : UserControl
  {
    private IContainer components;
    private RichTextBox richTextBoxLog;

    public ProgressLog() => this.InitializeComponent();

    public new string Text => this.richTextBoxLog.Text;

    public void LogMessage(Color color, string message)
    {
      if (this.richTextBoxLog.InvokeRequired)
      {
        this.Invoke((Delegate) new ProgressLog.LogMessageDelegate(this.LogMessage), (object) color, (object) message);
      }
      else
      {
        this.richTextBoxLog.SelectionStart = this.richTextBoxLog.TextLength;
        this.richTextBoxLog.SelectionColor = color;
        this.richTextBoxLog.AppendText(message);
        this.richTextBoxLog.AppendText(Environment.NewLine);
      }
    }

    public void ClearLog()
    {
      if (this.richTextBoxLog.InvokeRequired)
        this.Invoke((Delegate) new ProgressLog.ClearLogDelegate(this.ClearLog), new object[0]);
      else
        this.richTextBoxLog.Clear();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.richTextBoxLog = new RichTextBox();
      this.SuspendLayout();
      this.richTextBoxLog.BorderStyle = BorderStyle.None;
      this.richTextBoxLog.Dock = DockStyle.Fill;
      this.richTextBoxLog.Location = new Point(1, 1);
      this.richTextBoxLog.Name = "richTextBoxLog";
      this.richTextBoxLog.ReadOnly = true;
      this.richTextBoxLog.Size = new Size(311, 129);
      this.richTextBoxLog.TabIndex = 0;
      this.richTextBoxLog.Text = "";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.ControlDarkDark;
      this.Controls.Add((Control) this.richTextBoxLog);
      this.Name = nameof (ProgressLog);
      this.Padding = new Padding(1);
      this.Size = new Size(313, 131);
      this.ResumeLayout(false);
    }

    private delegate void LogMessageDelegate(Color color, string message);

    private delegate void ClearLogDelegate();
  }
}
