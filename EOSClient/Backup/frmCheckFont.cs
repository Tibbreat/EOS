// Decompiled with JetBrains decompiler
// Type: EOSClient.frmCheckFont
// Assembly: EOSClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7FF5B269-3EA1-40F1-AEFB-0F2D36888412
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\EOSClient.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

#nullable disable
namespace EOSClient
{
  public class frmCheckFont : Form
  {
    private IContainer components = (IContainer) null;
    private Button btnClose;
    private TextBox txtFontGuide;
    private Label lblTestDisplay;

    public frmCheckFont() => this.InitializeComponent();

    private bool checkFont(string fontName)
    {
      using (Font font = new Font(fontName, 12f, FontStyle.Regular))
        return font.Name.Equals(fontName);
    }

    private void frmCheckFont_Load(object sender, EventArgs e)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      foreach (FontFamily family in new InstalledFontCollection().Families)
      {
        string upper = family.GetName(0).Trim().ToUpper();
        if (upper.StartsWith("KaiTi".ToUpper()))
          flag1 = true;
        if (upper.StartsWith("Ms Mincho".ToUpper()))
          flag2 = true;
        if (upper.StartsWith("HGSeikai".ToUpper()))
          flag3 = true;
        if (upper.StartsWith("NtMotoya".ToUpper()))
          flag4 = true;
      }
      string str1 = "CHECK FONT RESULT:\r\n\r\n";
      string str2 = "KaiTi";
      string str3 = !flag1 ? str1 + "Chinese font ('" + str2 + "') : NOT FOUND.\r\n\r\n" : str1 + "Chinese font ('" + str2 + "') : OK.\r\n\r\n";
      string str4 = "MS Mincho";
      string str5 = !flag2 ? str3 + "Japanese font 1 ('" + str4 + "') :  NOT FOUND.\r\n\r\n" : str3 + "Japanese font 1 ('" + str4 + "') : OK.\r\n\r\n";
      string str6 = "HGSeikaishotaiPRO";
      string str7 = !flag3 ? str5 + "Japanese font 2 ('" + str6 + "') :  NOT FOUND.\r\n\r\n" : str5 + "Japanese font 2 ('" + str6 + "') : OK.\r\n\r\n";
      string str8 = "NtMotoya Kyotai";
      string str9 = !flag4 ? str7 + "Japanese font 3 ('" + str8 + "') :  NOT FOUND.\r\n\r\n" : str7 + "Japanese font 3 ('" + str8 + "') : OK.\r\n\r\n";
      if (!flag2 || !flag1 || !flag3 || !flag4)
        str9 += "\r\n\r\nINSTALLING FONTS ON Windows:\r\n\r\nThere are several ways to install fonts on Windows.\r\nKeep in mind that you must be an Administrator on the target machine to install fonts.\r\n\r\n - Download the font.\r\n - Double-click on a font file to open the font preview and select 'Install'.\r\n\r\nOR\r\n\r\n - Right-click on a font file, and then select 'Install'.";
      this.txtFontGuide.Text = str9;
    }

    private void btnClose_Click(object sender, EventArgs e) => this.Close();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnClose = new Button();
      this.txtFontGuide = new TextBox();
      this.lblTestDisplay = new Label();
      this.SuspendLayout();
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClose.Location = new Point(628, 480);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.txtFontGuide.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.txtFontGuide.BackColor = Color.White;
      this.txtFontGuide.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txtFontGuide.Location = new Point(12, 12);
      this.txtFontGuide.Multiline = true;
      this.txtFontGuide.Name = "txtFontGuide";
      this.txtFontGuide.ReadOnly = true;
      this.txtFontGuide.ScrollBars = ScrollBars.Vertical;
      this.txtFontGuide.Size = new Size(691, 448);
      this.txtFontGuide.TabIndex = 1;
      this.lblTestDisplay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblTestDisplay.AutoSize = true;
      this.lblTestDisplay.Font = new Font("MS Mincho", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblTestDisplay.Location = new Point(9, 480);
      this.lblTestDisplay.Name = "lblTestDisplay";
      this.lblTestDisplay.Size = new Size(173, 39);
      this.lblTestDisplay.TabIndex = 2;
      this.lblTestDisplay.Text = "ベトナム (in Japanese)\r\n越南 (in Chinese)\r\nViệt Nam (in Vietnamese)\r\n";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(715, 532);
      this.Controls.Add((Control) this.lblTestDisplay);
      this.Controls.Add((Control) this.txtFontGuide);
      this.Controls.Add((Control) this.btnClose);
      this.Name = nameof (frmCheckFont);
      this.Text = "Check fonts for Japanese/Chinese exam.";
      this.Load += new EventHandler(this.frmCheckFont_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
