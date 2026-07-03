using BookLendingSystem.Model;
using Sunny.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class MainForm : UIForm
    {
        public Admin CurrentAdmin { get; private set; }

        public MainForm(Admin admin)
        {
            CurrentAdmin = admin;
            InitializeComponent();
            lblUserInfo.Text = $"用户：{admin.AdminName}";
            EnableDoubleBuffering(panelMain);
        }

        private void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        private void btnBookManage_Click(object sender, EventArgs e)
        {
            OpenForm<BookManageForm>();
        }

        private void btnBorrowerManage_Click(object sender, EventArgs e)
        {
            OpenForm<BorrowerManageForm>();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            OpenForm<BorrowForm>(CurrentAdmin);
        }

        private void btnBorrowHistory_Click(object sender, EventArgs e)
        {
            OpenForm<BorrowHistoryForm>();
        }

        private void btnOverdue_Click(object sender, EventArgs e)
        {
            OpenForm<OverdueForm>();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelMain.Controls)
            {
                if (ctrl is Form)
                {
                    ((Form)ctrl).Close();
                }
            }
            LoginForm loginForm = null;
            foreach (Form form in Application.OpenForms)
            {
                if (form is LoginForm)
                {
                    loginForm = (LoginForm)form;
                    break;
                }
            }
            if (loginForm != null)
            {
                loginForm.Show();
            }
            else
            {
                loginForm = new LoginForm();
                loginForm.Show();
            }
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenForm<T>(object param = null) where T : Form
        {
            foreach (Control ctrl in panelMain.Controls)
            {
                if (ctrl is T)
                {
                    ctrl.BringToFront();
                    return;
                }
            }

            panelMain.SuspendLayout();

            Form newForm;
            if (param != null)
            {
                newForm = (Form)Activator.CreateInstance(typeof(T), param);
            }
            else
            {
                newForm = (Form)Activator.CreateInstance(typeof(T));
            }

            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.ControlBox = false;
            newForm.MaximizeBox = false;
            newForm.MinimizeBox = false;
            newForm.ShowIcon = false;
            newForm.Text = string.Empty;
            newForm.Dock = DockStyle.Fill;
            newForm.AutoScroll = true;
            newForm.BackColor = Color.Transparent;
            newForm.StartPosition = FormStartPosition.CenterParent;
            newForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            newForm.Size = panelMain.Size;
            newForm.MinimumSize = new Size(0, 0);
            EnableDoubleBuffering(newForm);

            panelMain.Controls.Add(newForm);
            newForm.Show();
            newForm.BringToFront();

            panelMain.ResumeLayout(false);
        }

        private void InitializeComponent()
        {
            panelLeft = new UIPanel();
            bottomFlowPanel = new FlowLayoutPanel();
            btnLogout = new UIButton();
            btnExit = new UIButton();
            navFlowPanel = new FlowLayoutPanel();
            btnBookManage = new UIButton();
            btnBorrowerManage = new UIButton();
            btnBorrow = new UIButton();
            btnBorrowHistory = new UIButton();
            btnOverdue = new UIButton();
            lblUserInfo = new UILabel();
            lblTitle = new UILabel();
            panelMain = new NoFlickerPanel();
            panelLeft.SuspendLayout();
            bottomFlowPanel.SuspendLayout();
            navFlowPanel.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.Transparent;
            panelLeft.Controls.Add(bottomFlowPanel);
            panelLeft.Controls.Add(navFlowPanel);
            panelLeft.Controls.Add(lblUserInfo);
            panelLeft.Controls.Add(lblTitle);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.FillColor = Color.Transparent;
            panelLeft.FillColor2 = Color.FromArgb(30, 35, 50);
            panelLeft.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelLeft.Location = new Point(0, 35);
            panelLeft.Margin = new Padding(4, 5, 4, 5);
            panelLeft.MinimumSize = new Size(1, 1);
            panelLeft.Name = "panelLeft";
            panelLeft.RectColor = Color.Transparent;
            panelLeft.Size = new Size(200, 487);
            panelLeft.TabIndex = 1;
            panelLeft.Text = null;
            panelLeft.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // bottomFlowPanel
            // 
            bottomFlowPanel.BackColor = Color.Transparent;
            bottomFlowPanel.Controls.Add(btnLogout);
            bottomFlowPanel.Controls.Add(btnExit);
            bottomFlowPanel.Dock = DockStyle.Bottom;
            bottomFlowPanel.FlowDirection = FlowDirection.TopDown;
            bottomFlowPanel.Location = new Point(0, 342);
            bottomFlowPanel.Name = "bottomFlowPanel";
            bottomFlowPanel.Padding = new Padding(20, 15, 20, 20);
            bottomFlowPanel.Size = new Size(200, 145);
            bottomFlowPanel.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.FillColor = Color.Transparent;
            btnLogout.FillHoverColor = Color.FromArgb(220, 53, 69);
            btnLogout.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnLogout.Location = new Point(20, 20);
            btnLogout.Margin = new Padding(0, 5, 0, 5);
            btnLogout.MinimumSize = new Size(1, 1);
            btnLogout.Name = "btnLogout";
            btnLogout.RectColor = Color.Transparent;
            btnLogout.Size = new Size(160, 38);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "退出登录";
            btnLogout.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnLogout.Click += btnLogout_Click;
            // 
            // btnExit
            // 
            btnExit.FillColor = Color.Transparent;
            btnExit.FillHoverColor = Color.FromArgb(220, 53, 69);
            btnExit.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnExit.Location = new Point(20, 68);
            btnExit.Margin = new Padding(0, 5, 0, 5);
            btnExit.MinimumSize = new Size(1, 1);
            btnExit.Name = "btnExit";
            btnExit.RectColor = Color.Transparent;
            btnExit.Size = new Size(160, 42);
            btnExit.TabIndex = 1;
            btnExit.Text = "退出系统";
            btnExit.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnExit.Click += btnExit_Click;
            // 
            // navFlowPanel
            // 
            navFlowPanel.BackColor = Color.Transparent;
            navFlowPanel.Controls.Add(btnBookManage);
            navFlowPanel.Controls.Add(btnBorrowerManage);
            navFlowPanel.Controls.Add(btnBorrow);
            navFlowPanel.Controls.Add(btnBorrowHistory);
            navFlowPanel.Controls.Add(btnOverdue);
            navFlowPanel.Dock = DockStyle.Top;
            navFlowPanel.FlowDirection = FlowDirection.TopDown;
            navFlowPanel.Location = new Point(0, 105);
            navFlowPanel.Name = "navFlowPanel";
            navFlowPanel.Padding = new Padding(20, 15, 20, 15);
            navFlowPanel.Size = new Size(200, 249);
            navFlowPanel.TabIndex = 1;
            // 
            // btnBookManage
            // 
            btnBookManage.FillColor = Color.Transparent;
            btnBookManage.FillHoverColor = Color.FromArgb(60, 70, 90);
            btnBookManage.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnBookManage.Location = new Point(20, 20);
            btnBookManage.Margin = new Padding(0, 5, 0, 5);
            btnBookManage.MinimumSize = new Size(1, 1);
            btnBookManage.Name = "btnBookManage";
            btnBookManage.RectColor = Color.Transparent;
            btnBookManage.Size = new Size(160, 31);
            btnBookManage.TabIndex = 0;
            btnBookManage.Text = "图书管理";
            btnBookManage.TipsFont = new Font("楷体", 12F, FontStyle.Bold);
            btnBookManage.Click += btnBookManage_Click;
            // 
            // btnBorrowerManage
            // 
            btnBorrowerManage.FillColor = Color.Transparent;
            btnBorrowerManage.FillHoverColor = Color.FromArgb(60, 70, 90);
            btnBorrowerManage.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnBorrowerManage.Location = new Point(20, 61);
            btnBorrowerManage.Margin = new Padding(0, 5, 0, 5);
            btnBorrowerManage.MinimumSize = new Size(1, 1);
            btnBorrowerManage.Name = "btnBorrowerManage";
            btnBorrowerManage.RectColor = Color.Transparent;
            btnBorrowerManage.Size = new Size(160, 32);
            btnBorrowerManage.TabIndex = 1;
            btnBorrowerManage.Text = "借阅人管理";
            btnBorrowerManage.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnBorrowerManage.Click += btnBorrowerManage_Click;
            // 
            // btnBorrow
            // 
            btnBorrow.FillColor = Color.Transparent;
            btnBorrow.FillHoverColor = Color.FromArgb(60, 70, 90);
            btnBorrow.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnBorrow.Location = new Point(20, 103);
            btnBorrow.Margin = new Padding(0, 5, 0, 5);
            btnBorrow.MinimumSize = new Size(1, 1);
            btnBorrow.Name = "btnBorrow";
            btnBorrow.RectColor = Color.Transparent;
            btnBorrow.Size = new Size(160, 30);
            btnBorrow.TabIndex = 2;
            btnBorrow.Text = "借阅图书";
            btnBorrow.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnBorrow.Click += btnBorrow_Click;
            // 
            // btnBorrowHistory
            // 
            btnBorrowHistory.FillColor = Color.Transparent;
            btnBorrowHistory.FillHoverColor = Color.FromArgb(60, 70, 90);
            btnBorrowHistory.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnBorrowHistory.Location = new Point(20, 143);
            btnBorrowHistory.Margin = new Padding(0, 5, 0, 5);
            btnBorrowHistory.MinimumSize = new Size(1, 1);
            btnBorrowHistory.Name = "btnBorrowHistory";
            btnBorrowHistory.RectColor = Color.Transparent;
            btnBorrowHistory.Size = new Size(160, 28);
            btnBorrowHistory.TabIndex = 3;
            btnBorrowHistory.Text = "借阅历史";
            btnBorrowHistory.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnBorrowHistory.Click += btnBorrowHistory_Click;
            // 
            // btnOverdue
            // 
            btnOverdue.FillColor = Color.Transparent;
            btnOverdue.FillHoverColor = Color.FromArgb(60, 70, 90);
            btnOverdue.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnOverdue.Location = new Point(20, 181);
            btnOverdue.Margin = new Padding(0, 5, 0, 5);
            btnOverdue.MinimumSize = new Size(1, 1);
            btnOverdue.Name = "btnOverdue";
            btnOverdue.RectColor = Color.Transparent;
            btnOverdue.Size = new Size(160, 30);
            btnOverdue.TabIndex = 4;
            btnOverdue.Text = "逾期管理";
            btnOverdue.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOverdue.Click += btnOverdue_Click;
            // 
            // lblUserInfo
            // 
            lblUserInfo.BackColor = Color.Transparent;
            lblUserInfo.Dock = DockStyle.Top;
            lblUserInfo.Font = new Font("微软雅黑", 10F);
            lblUserInfo.ForeColor = Color.FromArgb(180, 190, 200);
            lblUserInfo.Location = new Point(0, 60);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(200, 45);
            lblUserInfo.TabIndex = 2;
            lblUserInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("隶书", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 60);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "图书管理系统";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.Transparent;
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(200, 35);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(815, 487);
            panelMain.TabIndex = 0;
            // 
            // MainForm
            // 
            BackgroundImage = Properties.Resources.backgd1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1015, 522);
            Controls.Add(panelMain);
            Controls.Add(panelLeft);
            Name = "MainForm";
            RectColor = Color.Transparent;
            Text = "图书管理系统";
            TitleColor = Color.Transparent;
            TitleFont = new Font("楷体", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(19, 19, 1050, 650);
            panelLeft.ResumeLayout(false);
            bottomFlowPanel.ResumeLayout(false);
            navFlowPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private UIPanel panelLeft;
        private NoFlickerPanel panelMain;
        private FlowLayoutPanel navFlowPanel;
        private FlowLayoutPanel bottomFlowPanel;
        private UIButton btnBookManage;
        private UIButton btnBorrowerManage;
        private UIButton btnBorrow;
        private UIButton btnBorrowHistory;
        private UIButton btnOverdue;
        private UIButton btnLogout;
        private UIButton btnExit;
        private UILabel lblUserInfo;
        private UILabel lblTitle;
    }
}