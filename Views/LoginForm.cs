using BookLendingSystem.BLL;
using BookLendingSystem.Model;
using Sunny.UI;
using System;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class LoginForm : UIForm
    {
        private AdminBLL adminBLL = new AdminBLL();

        public LoginForm()
        {
            InitializeComponent();
            EnableDoubleBuffering(this);
        }

        private void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string account = txtAccount.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                UIMessageBox.ShowWarning("请输入账号和密码");
                return;
            }

            try
            {
                LoginResult result = adminBLL.Login(account, password);
                if (result.Success)
                {
                    MainForm mainForm = new MainForm(result.Admin);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    if (result.IsLocked)
                    {
                        UIMessageBox.ShowError(result.Message);
                    }
                    else
                    {
                        UIMessageBox.ShowWarning(result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"登录失败：{ex.Message}");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.RegisterSuccess += (s, args) =>
            {
                txtAccount.Text = string.Empty;
                txtPassword.Text = string.Empty;
                this.Show();
            };
            registerForm.FormClosed += (s, args) =>
            {
                this.Show();
            };
            registerForm.ShowDialog();
        }

        private void InitializeComponent()
        {
            txtAccount = new UITextBox();
            txtPassword = new UITextBox();
            btnLogin = new UIButton();
            btnExit = new UIButton();
            btnRegister = new UIButton();
            label1 = new UILabel();
            label2 = new UILabel();
            label3 = new UILabel();
            SuspendLayout();
            // 
            // txtAccount
            // 
            txtAccount.BackColor = Color.Transparent;
            txtAccount.FillColor2 = Color.Transparent;
            txtAccount.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtAccount.Location = new Point(185, 139);
            txtAccount.Margin = new Padding(4, 5, 4, 5);
            txtAccount.MinimumSize = new Size(1, 16);
            txtAccount.Name = "txtAccount";
            txtAccount.Padding = new Padding(5);
            txtAccount.ShowText = false;
            txtAccount.Size = new Size(200, 32);
            txtAccount.TabIndex = 7;
            txtAccount.TextAlignment = ContentAlignment.MiddleLeft;
            txtAccount.Watermark = "";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.Transparent;
            txtPassword.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtPassword.Location = new Point(185, 193);
            txtPassword.Margin = new Padding(4, 5, 4, 5);
            txtPassword.MinimumSize = new Size(1, 16);
            txtPassword.Name = "txtPassword";
            txtPassword.Padding = new Padding(5);
            txtPassword.PasswordChar = '*';
            txtPassword.ShowText = false;
            txtPassword.Size = new Size(200, 32);
            txtPassword.TabIndex = 6;
            txtPassword.TextAlignment = ContentAlignment.MiddleLeft;
            txtPassword.Watermark = "";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Transparent;
            btnLogin.FillColor = Color.Transparent;
            btnLogin.FillColor2 = Color.White;
            btnLogin.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnLogin.Location = new Point(78, 269);
            btnLogin.MinimumSize = new Size(1, 1);
            btnLogin.Name = "btnLogin";
            btnLogin.Radius = 25;
            btnLogin.RectColor = Color.White;
            btnLogin.Size = new Size(100, 40);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "登录";
            btnLogin.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnLogin.Click += btnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Transparent;
            btnExit.FillColor = Color.Transparent;
            btnExit.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnExit.Location = new Point(361, 269);
            btnExit.MinimumSize = new Size(1, 1);
            btnExit.Name = "btnExit";
            btnExit.Radius = 25;
            btnExit.RectColor = Color.White;
            btnExit.Size = new Size(100, 40);
            btnExit.TabIndex = 3;
            btnExit.Text = "退出";
            btnExit.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnExit.Click += btnExit_Click;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.Transparent;
            btnRegister.FillColor = Color.Transparent;
            btnRegister.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnRegister.Location = new Point(216, 269);
            btnRegister.MinimumSize = new Size(1, 1);
            btnRegister.Name = "btnRegister";
            btnRegister.Radius = 25;
            btnRegister.RectColor = Color.White;
            btnRegister.Size = new Size(100, 40);
            btnRegister.TabIndex = 5;
            btnRegister.Text = "注册";
            btnRegister.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRegister.Click += btnRegister_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.White;
            label1.Location = new Point(103, 139);
            label1.Name = "label1";
            label1.Size = new Size(75, 32);
            label1.TabIndex = 2;
            label1.Text = "账号：";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("楷体", 12F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(103, 193);
            label2.Name = "label2";
            label2.Size = new Size(75, 32);
            label2.TabIndex = 1;
            label2.Text = "密码：";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("隶书", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label3.ForeColor = Color.White;
            label3.Location = new Point(159, 57);
            label3.Name = "label3";
            label3.Size = new Size(257, 59);
            label3.TabIndex = 0;
            label3.Text = "图书管理系统";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            BackgroundImage = Properties.Resources.backgd1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(558, 385);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(btnRegister);
            Controls.Add(txtPassword);
            Controls.Add(txtAccount);
            Name = "LoginForm";
            RectColor = Color.Transparent;
            Text = "图书管理系统-登录";
            TitleColor = Color.Transparent;
            TitleFont = new Font("楷体", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(19, 19, 480, 260);
            ResumeLayout(false);
        }

        private UITextBox txtAccount;
        private UITextBox txtPassword;
        private UIButton btnLogin;
        private UIButton btnExit;
        private UIButton btnRegister;
        private UILabel label1;
        private UILabel label2;
        private UILabel label3;
    }
}