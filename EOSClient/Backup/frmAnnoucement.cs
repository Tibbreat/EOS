// Decompiled with JetBrains decompiler
// Type: EOSClient.frmAnnoucement
// Assembly: EOSClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7FF5B269-3EA1-40F1-AEFB-0F2D36888412
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\EOSClient.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

#nullable disable
namespace EOSClient
{
  public class frmAnnoucement : Form
  {
    private IContainer components = (IContainer) null;
    private TextBox txtNoiQuy;
    private CheckBox chbRead;
    private Button btnNext;

    public frmAnnoucement() => this.InitializeComponent();

    private void btnNext_Click(object sender, EventArgs e)
    {
      new AuthenticateForm().Show();
      this.Hide();
    }

    private void chbRead_CheckedChanged(object sender, EventArgs e)
    {
      this.btnNext.Enabled = this.chbRead.Checked;
    }

    private bool IsAdministrator()
    {
      return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }

    private void frmAnnoucement_Load(object sender, EventArgs e)
    {
      if (this.IsAdministrator())
        return;
      int num = (int) MessageBox.Show("You must login Windows as System Administrator or Run [EOS Client] as Administrator.", "Run as Administrator!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      Application.Exit();
    }

    private void txtNoiQuy_TextChanged(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmAnnoucement));
      this.txtNoiQuy = new TextBox();
      this.chbRead = new CheckBox();
      this.btnNext = new Button();
      this.SuspendLayout();
      this.txtNoiQuy.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.txtNoiQuy.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192);
      this.txtNoiQuy.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txtNoiQuy.Location = new Point(12, 12);
      this.txtNoiQuy.Multiline = true;
      this.txtNoiQuy.Name = "txtNoiQuy";
      this.txtNoiQuy.ReadOnly = true;
      this.txtNoiQuy.Size = new Size(1006, 389);
      this.txtNoiQuy.TabIndex = 2;
      this.txtNoiQuy.Text = componentResourceManager.GetString("txtNoiQuy.Text");
      this.txtNoiQuy.TextChanged += new EventHandler(this.txtNoiQuy_TextChanged);
      this.chbRead.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.chbRead.AutoSize = true;
      this.chbRead.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.chbRead.ForeColor = Color.Blue;
      this.chbRead.Location = new Point(688, 409);
      this.chbRead.Name = "chbRead";
      this.chbRead.Size = new Size(249, 19);
      this.chbRead.TabIndex = 1;
      this.chbRead.Text = "Tôi đã đọc và hiểu rõ Nội quy kỳ thi";
      this.chbRead.UseVisualStyleBackColor = true;
      this.chbRead.CheckedChanged += new EventHandler(this.chbRead_CheckedChanged);
      this.btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnNext.Enabled = false;
      this.btnNext.Location = new Point(943, 407);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new Size(75, 23);
      this.btnNext.TabIndex = 1;
      this.btnNext.Text = "Next";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new EventHandler(this.btnNext_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1028, 438);
      this.Controls.Add((Control) this.btnNext);
      this.Controls.Add((Control) this.chbRead);
      this.Controls.Add((Control) this.txtNoiQuy);
      this.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.Name = nameof (frmAnnoucement);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Nội quy kỳ thi";
      this.Load += new EventHandler(this.frmAnnoucement_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
