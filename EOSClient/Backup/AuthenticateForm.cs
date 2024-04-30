// Decompiled with JetBrains decompiler
// Type: EOSClient.AuthenticateForm
// Assembly: EOSClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7FF5B269-3EA1-40F1-AEFB-0F2D36888412
// Assembly location: C:\Users\admin\Desktop\EOS_Client_SP24_Version2_New\EOSClient.exe

using EncryptData;
using IRemote;
using NAudio.Wave;
using QuestionLib;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;

#nullable disable
namespace EOSClient
{
  public class AuthenticateForm : Form
  {
    private Button btnLogin;
    private TextBox txtUser;
    private TextBox txtPassword;
    private TextBox txtDomain;
    private Label lblUser;
    private Label lblPass;
    private Button btnCancel;
    private Label lblDomain;
    private System.ComponentModel.Container components = (System.ComponentModel.Container) null;
    private Label lblExamCode;
    private Label lblMessage;
    private Label lblVersion;
    private LinkLabel linkLBLCheckFont;
    private LinkLabel lblLinkCheckSound;
    private Label label1;
    private TextBox txtExamCode;
    private string version = "C3AF3F4B-EA15-4EDA-9750-C0214649FEC8";
    private ServerInfo si = (ServerInfo) null;
    private frmCheckFont fcf = (frmCheckFont) null;

    public AuthenticateForm() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      AppSettingsReader appSettingsReader = new AppSettingsReader();
      this.btnLogin = new Button();
      this.txtUser = new TextBox();
      this.txtPassword = new TextBox();
      this.txtDomain = new TextBox();
      this.lblUser = new Label();
      this.lblPass = new Label();
      this.btnCancel = new Button();
      this.lblDomain = new Label();
      this.lblExamCode = new Label();
      this.txtExamCode = new TextBox();
      this.lblMessage = new Label();
      this.lblVersion = new Label();
      this.linkLBLCheckFont = new LinkLabel();
      this.lblLinkCheckSound = new LinkLabel();
      this.label1 = new Label();
      this.SuspendLayout();
      this.btnLogin.DialogResult = DialogResult.OK;
      this.btnLogin.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btnLogin.Location = new Point(95, 192);
      this.btnLogin.Name = "btnLogin";
      this.btnLogin.Size = new Size(86, 23);
      this.btnLogin.TabIndex = 3;
      this.btnLogin.Text = "Login";
      this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
      this.txtUser.Location = new Point(96, 80);
      this.txtUser.Name = "txtUser";
      this.txtUser.Size = new Size(272, 20);
      this.txtUser.TabIndex = 1;
      this.txtPassword.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txtPassword.Location = new Point(96, 120);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new Size(272, 22);
      this.txtPassword.TabIndex = 2;
      this.txtDomain.Enabled = false;
      this.txtDomain.Location = new Point(96, 160);
      this.txtDomain.Name = "txtDomain";
      this.txtDomain.Size = new Size(272, 20);
      this.txtDomain.TabIndex = 9;
      this.txtDomain.Text = (string) appSettingsReader.GetValue("domain", typeof (string));
      this.lblUser.AutoSize = true;
      this.lblUser.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblUser.Location = new Point(15, 80);
      this.lblUser.Name = "lblUser";
      this.lblUser.Size = new Size(80, 16);
      this.lblUser.TabIndex = 6;
      this.lblUser.Text = "User Name:";
      this.lblPass.AutoSize = true;
      this.lblPass.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblPass.Location = new Point(24, 120);
      this.lblPass.Name = "lblPass";
      this.lblPass.Size = new Size(71, 16);
      this.lblPass.TabIndex = 7;
      this.lblPass.Text = "Password:";
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btnCancel.Location = new Point(230, 192);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(86, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Exit";
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.lblDomain.AutoSize = true;
      this.lblDomain.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblDomain.Location = new Point(37, 160);
      this.lblDomain.Name = "lblDomain";
      this.lblDomain.Size = new Size(58, 16);
      this.lblDomain.TabIndex = 5;
      this.lblDomain.Text = "Domain:";
      this.lblExamCode.AutoSize = true;
      this.lblExamCode.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblExamCode.Location = new Point(14, 38);
      this.lblExamCode.Name = "lblExamCode";
      this.lblExamCode.Size = new Size(81, 16);
      this.lblExamCode.TabIndex = 10;
      this.lblExamCode.Text = "Exam Code:";
      this.txtExamCode.Location = new Point(96, 38);
      this.txtExamCode.Name = "txtExamCode";
      this.txtExamCode.Size = new Size(272, 20);
      this.txtExamCode.TabIndex = 0;
      this.lblMessage.AutoSize = true;
      this.lblMessage.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblMessage.ForeColor = Color.Blue;
      this.lblMessage.Location = new Point(56, 252);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new Size(294, 17);
      this.lblMessage.TabIndex = 11;
      this.lblMessage.Text = "Register the exam may take time, please wait!";
      this.lblVersion.AutoSize = true;
      this.lblVersion.ForeColor = Color.Blue;
      this.lblVersion.Location = new Point(12, 197);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new Size(64, 13);
      this.lblVersion.TabIndex = 12;
      this.lblVersion.Text = "23.03.20.24";
      this.linkLBLCheckFont.AutoSize = true;
      this.linkLBLCheckFont.Location = new Point(252, 228);
      this.linkLBLCheckFont.Name = "linkLBLCheckFont";
      this.linkLBLCheckFont.Size = new Size(59, 13);
      this.linkLBLCheckFont.TabIndex = 13;
      this.linkLBLCheckFont.TabStop = true;
      this.linkLBLCheckFont.Text = "Check font";
      this.linkLBLCheckFont.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLBLCheckFont_LinkClicked);
      this.lblLinkCheckSound.AutoSize = true;
      this.lblLinkCheckSound.Location = new Point(93, 228);
      this.lblLinkCheckSound.Name = "lblLinkCheckSound";
      this.lblLinkCheckSound.Size = new Size(110, 13);
      this.lblLinkCheckSound.TabIndex = 14;
      this.lblLinkCheckSound.TabStop = true;
      this.lblLinkCheckSound.Text = "Check sound (7 secs)";
      this.lblLinkCheckSound.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lblLinkCheckSound_LinkClicked);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.ForeColor = Color.Red;
      this.label1.Location = new Point(3, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(404, 17);
      this.label1.TabIndex = 15;
      this.label1.Text = "You cannot take the exam if EOS runs under a virtual machine.";
      this.AcceptButton = (IButtonControl) this.btnLogin;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(409, 278);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.lblLinkCheckSound);
      this.Controls.Add((Control) this.linkLBLCheckFont);
      this.Controls.Add((Control) this.lblVersion);
      this.Controls.Add((Control) this.lblMessage);
      this.Controls.Add((Control) this.lblExamCode);
      this.Controls.Add((Control) this.txtExamCode);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.lblPass);
      this.Controls.Add((Control) this.lblUser);
      this.Controls.Add((Control) this.txtDomain);
      this.Controls.Add((Control) this.txtPassword);
      this.Controls.Add((Control) this.txtUser);
      this.Controls.Add((Control) this.lblDomain);
      this.Controls.Add((Control) this.btnLogin);
      this.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (AuthenticateForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "EOS Login Form";
      this.Load += new EventHandler(this.AuthenticateForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
      if (this.txtExamCode.Text.Trim().Equals(""))
      {
        int num1 = (int) MessageBox.Show("Please provide an Exam code", "Login");
      }
      else if (this.txtUser.Text.Trim().Equals(""))
      {
        int num2 = (int) MessageBox.Show("Please provide an username", "Login");
      }
      else if (this.txtPassword.Text.Trim().Equals(""))
      {
        int num3 = (int) MessageBox.Show("Please provide a password", "Login");
      }
      else if (this.txtDomain.Text.Trim().Equals(""))
      {
        int num4 = (int) MessageBox.Show("Please provide a domain address", "Login");
      }
      else
      {
        try
        {
          if (!this.si.Public_IP.Trim().Equals(""))
            this.si.IP = this.si.Public_IP;
          IRemoteServer remoteServer = (IRemoteServer) Activator.GetObject(typeof (IRemoteServer), "tcp://" + this.si.IP + ":" + (object) this.si.Port + "/Server");
          RegisterData rd = new RegisterData();
          rd.Login = this.txtUser.Text;
          rd.Password = this.txtPassword.Text;
          rd.ExamCode = this.txtExamCode.Text;
          rd.Machine = Environment.MachineName.ToUpper();
          EOSData ed = remoteServer.ConductExam(rd);
          if (ed.Status == RegisterStatus.EXAM_CODE_NOT_EXISTS)
          {
            int num5 = (int) MessageBox.Show("Exam code is not available!", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          else if (ed.Status == RegisterStatus.FINISHED)
          {
            int num6 = (int) MessageBox.Show("The exam is finished!", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
          else if (ed.Status == RegisterStatus.REGISTERED)
          {
            int num7 = (int) MessageBox.Show("This user [" + this.txtUser.Text + "] is already registered. Need re-assign to continue the exam.", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
          else if (ed.Status == RegisterStatus.REGISTER_ERROR)
          {
            int num8 = (int) MessageBox.Show("Register ERROR, try again", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          else if (ed.Status == RegisterStatus.NOT_ALLOW_MACHINE)
          {
            int num9 = (int) MessageBox.Show("Your machine is not allowed to take the exam!", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
          else if (ed.Status == RegisterStatus.NOT_ALLOW_STUDENT)
          {
            int num10 = (int) MessageBox.Show("The account is NOT allowed to take the exam!", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
          else if (ed.Status == RegisterStatus.LOGIN_FAILED)
          {
            int num11 = (int) MessageBox.Show("Sorry, unable to verify your information. Check [User Name] and [Password]!", "Login failed");
          }
          if (ed.Status != RegisterStatus.NEW && ed.Status != RegisterStatus.RE_ASSIGN)
            return;
          this.Hide();
          ed.GUI = GZipHelper.DeCompress(ed.GUI, ed.OriginSize);
          Form instance = (Form) Activator.CreateInstance(Assembly.Load(ed.GUI).GetType("ExamClient.frmEnglishExamClient"));
          IExamclient examclient = (IExamclient) instance;
          ed.GUI = (byte[]) null;
          ed.ServerInfomation = this.si;
          ed.RegData = rd;
          examclient.SetExamData(ed);
          instance.Show();
        }
        catch (Exception ex)
        {
          int num12 = (int) MessageBox.Show(ex.ToString());
          int num13 = (int) MessageBox.Show("Start Exam Error:\nCannot connect to the EOS server!\n", "Connecting...", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void btnCancel_Click(object sender, EventArgs e) => Application.Exit();

    private void AuthenticateForm_Load(object sender, EventArgs e)
    {
      string fname = "EOS_Server_Info.dat";
      if (File.Exists(Application.StartupPath + "\\" + fname))
      {
        try
        {
          string key = "04021976";
          this.si = (ServerInfo) EncryptSupport.ByteArrayToObject(EncryptSupport.DecryptQuestions_FromFile(fname, key));
          if (this.version.Equals(this.si.Version))
            return;
          int num = (int) MessageBox.Show("Wrong EOS client version! Please copy the right EOS client version.", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          Application.Exit();
        }
        catch
        {
          int num = (int) MessageBox.Show("Wrong [" + fname + "] file format! Please copy the right EOS client version.", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          Application.Exit();
        }
      }
      else
      {
        int num = (int) MessageBox.Show("File [" + fname + "] not found! Please copy the right EOS client version.", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        Application.Exit();
      }
    }

    private bool IsAdministrator()
    {
      return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }

    private void linkLBLCheckFont_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (this.fcf == null || this.fcf.IsDisposed)
        this.fcf = new frmCheckFont();
      this.fcf.Show();
    }

    private void lblLinkCheckSound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (!File.Exists("ghosts.mp3"))
      {
        int num = (int) MessageBox.Show("Audio file ghosts.mp3 cannot be found!", "Check sound");
      }
      else
        this.PlayFromUrl("ghosts.mp3");
    }

    public void PlayFromUrl(string url)
    {
      FileStream fileStream = File.OpenRead(url);
      byte[] buffer = new byte[fileStream.Length];
      fileStream.Read(buffer, 0, (int) fileStream.Length);
      fileStream.Close();
      Mp3FileReader mp3FileReader = new Mp3FileReader((Stream) new MemoryStream(buffer));
      WaveOut waveOut = new WaveOut();
      waveOut.Init((IWaveProvider) mp3FileReader);
      waveOut.Volume = 1f;
      waveOut.Play();
    }
  }
}
