using BookLendingSystem.BLL;
using Sunny.UI;
using System;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class RegisterForm : UIForm
    {
        private AdminBLL adminBLL = new AdminBLL();

        public event EventHandler? RegisterSuccess;

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string account = txtAccount.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                UIMessageBox.ShowWarning("请输入姓名");
                return;
            }

            if (string.IsNullOrEmpty(account))
            {
                UIMessageBox.ShowWarning("请输入账号");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                UIMessageBox.ShowWarning("请输入密码");
                return;
            }

            if (password.Length < 6)
            {
                UIMessageBox.ShowWarning("密码长度不能少于6位");
                return;
            }

            if (password != confirmPassword)
            {
                UIMessageBox.ShowWarning("两次输入的密码不一致");
                return;
            }

            try
            {
                bool success = adminBLL.Register(name, account, password);
                if (success)
                {
                    UIMessageBox.ShowSuccess("注册成功");
                    RegisterSuccess?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
                else
                {
                    UIMessageBox.ShowError("账号已存在");
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"注册失败：{ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            txtName = new UITextBox();
            txtAccount = new UITextBox();
            txtPassword = new UITextBox();
            txtConfirmPassword = new UITextBox();
            btnRegister = new UIButton();
            btnCancel = new UIButton();
            label1 = new UILabel();
            label2 = new UILabel();
            label3 = new UILabel();
            label4 = new UILabel();
            label5 = new UILabel();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtName.Location = new Point(234, 152);
            txtName.Margin = new Padding(4, 5, 4, 5);
            txtName.MinimumSize = new Size(1, 16);
            txtName.Name = "txtName";
            txtName.Padding = new Padding(5);
            txtName.ShowText = false;
            txtName.Size = new Size(220, 32);
            txtName.TabIndex = 10;
            txtName.TextAlignment = ContentAlignment.MiddleLeft;
            txtName.Watermark = "";
            // 
            // txtAccount
            // 
            txtAccount.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtAccount.Location = new Point(234, 194);
            txtAccount.Margin = new Padding(4, 5, 4, 5);
            txtAccount.MinimumSize = new Size(1, 16);
            txtAccount.Name = "txtAccount";
            txtAccount.Padding = new Padding(5);
            txtAccount.ShowText = false;
            txtAccount.Size = new Size(220, 32);
            txtAccount.TabIndex = 9;
            txtAccount.TextAlignment = ContentAlignment.MiddleLeft;
            txtAccount.Watermark = "";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtPassword.Location = new Point(234, 236);
            txtPassword.Margin = new Padding(4, 5, 4, 5);
            txtPassword.MinimumSize = new Size(1, 16);
            txtPassword.Name = "txtPassword";
            txtPassword.Padding = new Padding(5);
            txtPassword.PasswordChar = '*';
            txtPassword.ShowText = false;
            txtPassword.Size = new Size(220, 32);
            txtPassword.TabIndex = 8;
            txtPassword.TextAlignment = ContentAlignment.MiddleLeft;
            txtPassword.Watermark = "";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtConfirmPassword.Location = new Point(234, 278);
            txtConfirmPassword.Margin = new Padding(4, 5, 4, 5);
            txtConfirmPassword.MinimumSize = new Size(1, 16);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Padding = new Padding(5);
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.ShowText = false;
            txtConfirmPassword.Size = new Size(220, 32);
            txtConfirmPassword.TabIndex = 7;
            txtConfirmPassword.TextAlignment = ContentAlignment.MiddleLeft;
            txtConfirmPassword.Watermark = "";
            // 
            // btnRegister
            // 
            btnRegister.FillColor = Color.Transparent;
            btnRegister.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRegister.Location = new Point(138, 351);
            btnRegister.MinimumSize = new Size(1, 1);
            btnRegister.Name = "btnRegister";
            btnRegister.RectColor = Color.White;
            btnRegister.Size = new Size(100, 40);
            btnRegister.TabIndex = 6;
            btnRegister.Text = "注册";
            btnRegister.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRegister.Click += btnRegister_Click;
            // 
            // btnCancel
            // 
            btnCancel.FillColor = Color.Transparent;
            btnCancel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Location = new Point(354, 351);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = Color.White;
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.White;
            label1.Location = new Point(138, 152);
            label1.Name = "label1";
            label1.Size = new Size(82, 32);
            label1.TabIndex = 4;
            label1.Text = "姓名：";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label2.ForeColor = Color.White;
            label2.Location = new Point(138, 194);
            label2.Name = "label2";
            label2.Size = new Size(82, 32);
            label2.TabIndex = 3;
            label2.Text = "账号：";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label3.ForeColor = Color.White;
            label3.Location = new Point(138, 236);
            label3.Name = "label3";
            label3.Size = new Size(82, 32);
            label3.TabIndex = 2;
            label3.Text = "密码：";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label4.ForeColor = Color.White;
            label4.Location = new Point(109, 278);
            label4.Name = "label4";
            label4.Size = new Size(118, 32);
            label4.TabIndex = 1;
            label4.Text = "确认密码：";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Font = new Font("隶书", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label5.ForeColor = Color.White;
            label5.Location = new Point(194, 62);
            label5.Name = "label5";
            label5.Size = new Size(180, 36);
            label5.TabIndex = 0;
            label5.Text = "用户注册";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            label5.Click += label5_Click;
            // 
            // RegisterForm
            // 
            BackColor = Color.Transparent;
            BackgroundImage = Properties.Resources.backgd1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(600, 470);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnRegister);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(txtAccount);
            Controls.Add(txtName);
            Name = "RegisterForm";
            RectColor = Color.Transparent;
            Text = "图书管理系统 - 注册";
            TitleColor = Color.Transparent;
            TitleFont = new Font("楷体", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(19, 19, 460, 320);
            ResumeLayout(false);
        }

        private UITextBox txtName;
        private UITextBox txtAccount;
        private UITextBox txtPassword;
        private UITextBox txtConfirmPassword;
        private UIButton btnRegister;
        private UIButton btnCancel;
        private UILabel label1;
        private UILabel label2;
        private UILabel label3;
        private UILabel label4;
        private UILabel label5;

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}