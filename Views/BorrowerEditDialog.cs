using BookLendingSystem.Model;
using Sunny.UI;
using System;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class BorrowerEditDialog : UIForm
    {
        public Borrowers Borrower { get; private set; }

        public BorrowerEditDialog(Borrowers borrower = null)
        {
            InitializeComponent();
            EnableDoubleBuffering(tableLayoutPanel);
            if (borrower != null)
            {
                Tag = borrower;
                LoadBorrowerData();
                Text = "编辑借阅人";
            }
            else
            {
                Text = "添加借阅人";
                txtSex.SelectedIndex = 0;
                txtPrice.Text = "0";
            }
        }

        private void LoadBorrowerData()
        {
            Borrowers borrower = (Borrowers)Tag;
            txtName.Text = borrower.BorrowersName;
            txtIDCard.Text = borrower.IDCard;
            txtSex.SelectedItem = borrower.Sex;
            txtTel.Text = borrower.Tel;
            txtCode.Text = borrower.BorrowingCode?.ToString() ?? string.Empty;
            txtPrice.Text = borrower.Price.ToString();
            txtOrderNumber.Text = borrower.OrderNumber;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            Borrower = new Borrowers
            {
                BorrowersName = txtName.Text,
                IDCard = txtIDCard.Text,
                Sex = txtSex.Text,
                Tel = txtTel.Text,
                BorrowingCode = string.IsNullOrEmpty(txtCode.Text) ? null : int.Parse(txtCode.Text),
                Price = int.Parse(txtPrice.Text),
                OrderNumber = txtOrderNumber.Text,
                Remark = 1
            };

            if (Tag is Borrowers originalBorrower)
                Borrower.Id = originalBorrower.Id;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                UIMessageBox.ShowWarning("请输入姓名");
                return false;
            }
            if (string.IsNullOrEmpty(txtIDCard.Text))
            {
                UIMessageBox.ShowWarning("请输入身份证号");
                return false;
            }
            if (string.IsNullOrEmpty(txtSex.Text))
            {
                UIMessageBox.ShowWarning("请选择性别");
                return false;
            }
            if (string.IsNullOrEmpty(txtTel.Text))
            {
                UIMessageBox.ShowWarning("请输入电话号码");
                return false;
            }
            if (!string.IsNullOrEmpty(txtCode.Text) && !int.TryParse(txtCode.Text, out _))
            {
                UIMessageBox.ShowWarning("请输入有效的借书码");
                return false;
            }
            if (string.IsNullOrEmpty(txtPrice.Text) || !int.TryParse(txtPrice.Text, out _))
            {
                UIMessageBox.ShowWarning("请输入有效的押金");
                return false;
            }
            return true;
        }

        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            label1 = new UILabel();
            txtName = new UITextBox();
            label2 = new UILabel();
            txtIDCard = new UITextBox();
            label3 = new UILabel();
            txtSex = new UIComboBox();
            label4 = new UILabel();
            txtTel = new UITextBox();
            label5 = new UILabel();
            txtCode = new UITextBox();
            label6 = new UILabel();
            txtPrice = new UITextBox();
            label7 = new UILabel();
            txtOrderNumber = new UITextBox();
            btnCancel = new UIButton();
            btnConfirm = new UIButton();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = Color.Transparent;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(label1, 0, 0);
            tableLayoutPanel.Controls.Add(txtName, 1, 0);
            tableLayoutPanel.Controls.Add(label2, 0, 1);
            tableLayoutPanel.Controls.Add(txtIDCard, 1, 1);
            tableLayoutPanel.Controls.Add(label3, 0, 2);
            tableLayoutPanel.Controls.Add(txtSex, 1, 2);
            tableLayoutPanel.Controls.Add(label4, 0, 3);
            tableLayoutPanel.Controls.Add(txtTel, 1, 3);
            tableLayoutPanel.Controls.Add(label5, 0, 4);
            tableLayoutPanel.Controls.Add(txtCode, 1, 4);
            tableLayoutPanel.Controls.Add(label6, 0, 5);
            tableLayoutPanel.Controls.Add(txtPrice, 1, 5);
            tableLayoutPanel.Controls.Add(label7, 0, 6);
            tableLayoutPanel.Controls.Add(txtOrderNumber, 1, 6);
            tableLayoutPanel.Location = new Point(0, 35);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.Size = new Size(804, 285);
            tableLayoutPanel.TabIndex = 2;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("楷体", 13.8F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(94, 40);
            label1.TabIndex = 0;
            label1.Text = "姓名：";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Fill;
            txtName.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtName.Location = new Point(104, 5);
            txtName.Margin = new Padding(4, 5, 4, 5);
            txtName.MinimumSize = new Size(1, 16);
            txtName.Name = "txtName";
            txtName.Padding = new Padding(5);
            txtName.ShowText = false;
            txtName.Size = new Size(696, 30);
            txtName.TabIndex = 1;
            txtName.TextAlignment = ContentAlignment.MiddleLeft;
            txtName.Watermark = "";
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label2.ForeColor = Color.White;
            label2.Location = new Point(3, 40);
            label2.Name = "label2";
            label2.Size = new Size(94, 40);
            label2.TabIndex = 2;
            label2.Text = "身份证号";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtIDCard
            // 
            txtIDCard.Dock = DockStyle.Fill;
            txtIDCard.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtIDCard.Location = new Point(104, 45);
            txtIDCard.Margin = new Padding(4, 5, 4, 5);
            txtIDCard.MinimumSize = new Size(1, 16);
            txtIDCard.Name = "txtIDCard";
            txtIDCard.Padding = new Padding(5);
            txtIDCard.ShowText = false;
            txtIDCard.Size = new Size(696, 30);
            txtIDCard.TabIndex = 3;
            txtIDCard.TextAlignment = ContentAlignment.MiddleLeft;
            txtIDCard.Watermark = "";
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("楷体", 13.8F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(3, 80);
            label3.Name = "label3";
            label3.Size = new Size(94, 40);
            label3.TabIndex = 4;
            label3.Text = "性别：";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtSex
            // 
            txtSex.DataSource = null;
            txtSex.Dock = DockStyle.Fill;
            txtSex.FillColor = Color.White;
            txtSex.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtSex.ItemHoverColor = Color.FromArgb(155, 200, 255);
            txtSex.Items.AddRange(new object[] { "男", "女" });
            txtSex.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            txtSex.Location = new Point(104, 85);
            txtSex.Margin = new Padding(4, 5, 4, 5);
            txtSex.MinimumSize = new Size(63, 0);
            txtSex.Name = "txtSex";
            txtSex.Padding = new Padding(0, 0, 30, 2);
            txtSex.Size = new Size(696, 30);
            txtSex.SymbolSize = 24;
            txtSex.TabIndex = 5;
            txtSex.TextAlignment = ContentAlignment.MiddleLeft;
            txtSex.Watermark = "";
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("楷体", 13.8F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(3, 120);
            label4.Name = "label4";
            label4.Size = new Size(94, 40);
            label4.TabIndex = 6;
            label4.Text = "电话：";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTel
            // 
            txtTel.Dock = DockStyle.Fill;
            txtTel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtTel.Location = new Point(104, 125);
            txtTel.Margin = new Padding(4, 5, 4, 5);
            txtTel.MinimumSize = new Size(1, 16);
            txtTel.Name = "txtTel";
            txtTel.Padding = new Padding(5);
            txtTel.ShowText = false;
            txtTel.Size = new Size(696, 30);
            txtTel.TabIndex = 7;
            txtTel.TextAlignment = ContentAlignment.MiddleLeft;
            txtTel.Watermark = "";
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label5.ForeColor = Color.White;
            label5.Location = new Point(3, 160);
            label5.Name = "label5";
            label5.Size = new Size(94, 40);
            label5.TabIndex = 8;
            label5.Text = "借书码";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtCode
            // 
            txtCode.Dock = DockStyle.Fill;
            txtCode.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtCode.Location = new Point(104, 165);
            txtCode.Margin = new Padding(4, 5, 4, 5);
            txtCode.MinimumSize = new Size(1, 16);
            txtCode.Name = "txtCode";
            txtCode.Padding = new Padding(5);
            txtCode.ShowText = false;
            txtCode.Size = new Size(696, 30);
            txtCode.TabIndex = 9;
            txtCode.TextAlignment = ContentAlignment.MiddleLeft;
            txtCode.Watermark = "";
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("楷体", 13.8F, FontStyle.Bold);
            label6.ForeColor = Color.White;
            label6.Location = new Point(3, 200);
            label6.Name = "label6";
            label6.Size = new Size(94, 40);
            label6.TabIndex = 10;
            label6.Text = "押金：";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPrice
            // 
            txtPrice.Dock = DockStyle.Fill;
            txtPrice.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtPrice.Location = new Point(104, 205);
            txtPrice.Margin = new Padding(4, 5, 4, 5);
            txtPrice.MinimumSize = new Size(1, 16);
            txtPrice.Name = "txtPrice";
            txtPrice.Padding = new Padding(5);
            txtPrice.ShowText = false;
            txtPrice.Size = new Size(696, 30);
            txtPrice.TabIndex = 11;
            txtPrice.TextAlignment = ContentAlignment.MiddleLeft;
            txtPrice.Watermark = "";
            // 
            // label7
            // 
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label7.ForeColor = Color.White;
            label7.Location = new Point(3, 240);
            label7.Name = "label7";
            label7.Size = new Size(94, 45);
            label7.TabIndex = 12;
            label7.Text = "订单号";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtOrderNumber
            // 
            txtOrderNumber.Dock = DockStyle.Fill;
            txtOrderNumber.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtOrderNumber.Location = new Point(104, 245);
            txtOrderNumber.Margin = new Padding(4, 5, 4, 5);
            txtOrderNumber.MinimumSize = new Size(1, 16);
            txtOrderNumber.Name = "txtOrderNumber";
            txtOrderNumber.Padding = new Padding(5);
            txtOrderNumber.ShowText = false;
            txtOrderNumber.Size = new Size(696, 35);
            txtOrderNumber.TabIndex = 13;
            txtOrderNumber.TextAlignment = ContentAlignment.MiddleLeft;
            txtOrderNumber.Watermark = "";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.FillColor = Color.Transparent;
            btnCancel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Location = new Point(468, 373);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Radius = 25;
            btnCancel.RectColor = Color.White;
            btnCancel.Size = new Size(100, 38);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Click += btnCancel_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.Transparent;
            btnConfirm.FillColor = Color.Transparent;
            btnConfirm.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnConfirm.Location = new Point(203, 373);
            btnConfirm.MinimumSize = new Size(1, 1);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Radius = 25;
            btnConfirm.RectColor = Color.White;
            btnConfirm.Size = new Size(100, 38);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "确定";
            btnConfirm.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnConfirm.Click += btnConfirm_Click;
            // 
            // BorrowerEditDialog
            // 
            AllowShowTitle = false;
            BackgroundImage = Properties.Resources.backgd1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(807, 450);
            Controls.Add(tableLayoutPanel);
            Controls.Add(btnConfirm);
            Controls.Add(btnCancel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BorrowerEditDialog";
            Padding = new Padding(0);
            RectColor = Color.Transparent;
            ShowTitle = false;
            StartPosition = FormStartPosition.CenterParent;
            TitleColor = Color.Transparent;
            ZoomScaleRect = new Rectangle(19, 19, 400, 340);
            tableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private UILabel label1;
        private UILabel label2;
        private UILabel label3;
        private UILabel label4;
        private UILabel label5;
        private UILabel label6;
        private UILabel label7;
        private UITextBox txtName;
        private UITextBox txtIDCard;
        private UIComboBox txtSex;
        private UITextBox txtTel;
        private UITextBox txtCode;
        private UITextBox txtPrice;
        private UITextBox txtOrderNumber;
        private UIButton btnConfirm;
        private UIButton btnCancel;
    }
}