using BookLendingSystem.Model;
using Sunny.UI;
using System;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class BookEditDialog : UIForm
    {
        public Books Book { get; private set; }

        public BookEditDialog(Books book = null)
        {
            InitializeComponent();
            if (book != null)
            {
                Tag = book;
                LoadBookData();
                Text = "编辑图书";
            }
            else
            {
                Text = "添加图书";
                dtpPublicationDate.Value = DateTime.Now;
                txtLoansNumber.Text = "0";
                txtTotalNumber.Text = "0";
            }
        }

        private void LoadBookData()
        {
            Books book = (Books)Tag;
            txtBarCode.Text = book.BarCode.ToString();
            txtBookName.Text = book.BookName;
            txtCategory.Text = book.Category;
            txtAuthor.Text = book.Author;
            txtPublishingHouse.Text = book.PublishingHouse;
            dtpPublicationDate.Value = book.PublicationDate;
            txtLoansNumber.Text = book.LoansNumber.ToString();
            txtTotalNumber.Text = book.TotalNumber.ToString();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            Book = new Books
            {
                BarCode = int.Parse(txtBarCode.Text),
                BookName = txtBookName.Text,
                Category = txtCategory.Text,
                Author = txtAuthor.Text,
                PublishingHouse = txtPublishingHouse.Text,
                PublicationDate = dtpPublicationDate.Value,
                LoansNumber = int.Parse(txtLoansNumber.Text),
                TotalNumber = int.Parse(txtTotalNumber.Text),
                Remark = 1
            };

            if (Tag is Books originalBook)
                Book.Id = originalBook.Id;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtBarCode.Text) || !int.TryParse(txtBarCode.Text, out _))
            {
                UIMessageBox.ShowWarning("请输入有效的条码号");
                return false;
            }
            if (string.IsNullOrEmpty(txtBookName.Text))
            {
                UIMessageBox.ShowWarning("请输入书名");
                return false;
            }
            if (string.IsNullOrEmpty(txtCategory.Text))
            {
                UIMessageBox.ShowWarning("请输入类别");
                return false;
            }
            if (string.IsNullOrEmpty(txtAuthor.Text))
            {
                UIMessageBox.ShowWarning("请输入作者");
                return false;
            }
            if (string.IsNullOrEmpty(txtPublishingHouse.Text))
            {
                UIMessageBox.ShowWarning("请输入出版社");
                return false;
            }
            if (string.IsNullOrEmpty(txtLoansNumber.Text) || !int.TryParse(txtLoansNumber.Text, out _))
            {
                UIMessageBox.ShowWarning("请输入有效的可借阅数量");
                return false;
            }
            if (string.IsNullOrEmpty(txtTotalNumber.Text) || !int.TryParse(txtTotalNumber.Text, out _))
            {
                UIMessageBox.ShowWarning("请输入有效的总数量");
                return false;
            }
            return true;
        }

        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            label1 = new UILabel();
            txtBarCode = new UITextBox();
            label2 = new UILabel();
            txtBookName = new UITextBox();
            label3 = new UILabel();
            txtCategory = new UITextBox();
            label4 = new UILabel();
            txtAuthor = new UITextBox();
            label5 = new UILabel();
            txtPublishingHouse = new UITextBox();
            label6 = new UILabel();
            dtpPublicationDate = new UIDatePicker();
            txtLoansNumber = new UITextBox();
            txtTotalNumber = new UITextBox();
            label8 = new UILabel();
            label7 = new UILabel();
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
            tableLayoutPanel.Controls.Add(txtBarCode, 1, 0);
            tableLayoutPanel.Controls.Add(label2, 0, 1);
            tableLayoutPanel.Controls.Add(txtBookName, 1, 1);
            tableLayoutPanel.Controls.Add(label3, 0, 2);
            tableLayoutPanel.Controls.Add(txtCategory, 1, 2);
            tableLayoutPanel.Controls.Add(label4, 0, 3);
            tableLayoutPanel.Controls.Add(txtAuthor, 1, 3);
            tableLayoutPanel.Controls.Add(label5, 0, 4);
            tableLayoutPanel.Controls.Add(txtPublishingHouse, 1, 4);
            tableLayoutPanel.Controls.Add(label6, 0, 5);
            tableLayoutPanel.Controls.Add(dtpPublicationDate, 1, 5);
            tableLayoutPanel.Controls.Add(txtLoansNumber, 1, 6);
            tableLayoutPanel.Controls.Add(txtTotalNumber, 1, 7);
            tableLayoutPanel.Controls.Add(label8, 0, 7);
            tableLayoutPanel.Controls.Add(label7, 0, 6);
            tableLayoutPanel.Location = new Point(0, 35);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel.Size = new Size(696, 250);
            tableLayoutPanel.TabIndex = 2;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.White;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(94, 30);
            label1.TabIndex = 0;
            label1.Text = "条码号：";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtBarCode
            // 
            txtBarCode.Dock = DockStyle.Fill;
            txtBarCode.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtBarCode.Location = new Point(104, 5);
            txtBarCode.Margin = new Padding(4, 5, 4, 5);
            txtBarCode.MinimumSize = new Size(1, 16);
            txtBarCode.Name = "txtBarCode";
            txtBarCode.Padding = new Padding(5);
            txtBarCode.ShowText = false;
            txtBarCode.Size = new Size(588, 20);
            txtBarCode.TabIndex = 1;
            txtBarCode.TextAlignment = ContentAlignment.MiddleLeft;
            txtBarCode.Watermark = "";
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("楷体", 12F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(3, 30);
            label2.Name = "label2";
            label2.Size = new Size(94, 30);
            label2.TabIndex = 2;
            label2.Text = "书名：";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtBookName
            // 
            txtBookName.Dock = DockStyle.Fill;
            txtBookName.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtBookName.Location = new Point(104, 35);
            txtBookName.Margin = new Padding(4, 5, 4, 5);
            txtBookName.MinimumSize = new Size(1, 16);
            txtBookName.Name = "txtBookName";
            txtBookName.Padding = new Padding(5);
            txtBookName.ShowText = false;
            txtBookName.Size = new Size(588, 20);
            txtBookName.TabIndex = 3;
            txtBookName.TextAlignment = ContentAlignment.MiddleLeft;
            txtBookName.Watermark = "";
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("楷体", 12F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(3, 60);
            label3.Name = "label3";
            label3.Size = new Size(94, 30);
            label3.TabIndex = 4;
            label3.Text = "类别：";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtCategory
            // 
            txtCategory.Dock = DockStyle.Fill;
            txtCategory.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtCategory.Location = new Point(104, 65);
            txtCategory.Margin = new Padding(4, 5, 4, 5);
            txtCategory.MinimumSize = new Size(1, 16);
            txtCategory.Name = "txtCategory";
            txtCategory.Padding = new Padding(5);
            txtCategory.ShowText = false;
            txtCategory.Size = new Size(588, 20);
            txtCategory.TabIndex = 5;
            txtCategory.TextAlignment = ContentAlignment.MiddleLeft;
            txtCategory.Watermark = "";
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("楷体", 12F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(3, 90);
            label4.Name = "label4";
            label4.Size = new Size(94, 30);
            label4.TabIndex = 6;
            label4.Text = "作者：";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtAuthor
            // 
            txtAuthor.Dock = DockStyle.Fill;
            txtAuthor.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtAuthor.Location = new Point(104, 95);
            txtAuthor.Margin = new Padding(4, 5, 4, 5);
            txtAuthor.MinimumSize = new Size(1, 16);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Padding = new Padding(5);
            txtAuthor.ShowText = false;
            txtAuthor.Size = new Size(588, 20);
            txtAuthor.TabIndex = 7;
            txtAuthor.TextAlignment = ContentAlignment.MiddleLeft;
            txtAuthor.Watermark = "";
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("楷体", 12F, FontStyle.Bold);
            label5.ForeColor = Color.White;
            label5.Location = new Point(3, 120);
            label5.Name = "label5";
            label5.Size = new Size(94, 30);
            label5.TabIndex = 8;
            label5.Text = "出版社：";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPublishingHouse
            // 
            txtPublishingHouse.Dock = DockStyle.Fill;
            txtPublishingHouse.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtPublishingHouse.Location = new Point(104, 125);
            txtPublishingHouse.Margin = new Padding(4, 5, 4, 5);
            txtPublishingHouse.MinimumSize = new Size(1, 16);
            txtPublishingHouse.Name = "txtPublishingHouse";
            txtPublishingHouse.Padding = new Padding(5);
            txtPublishingHouse.ShowText = false;
            txtPublishingHouse.Size = new Size(588, 20);
            txtPublishingHouse.TabIndex = 9;
            txtPublishingHouse.TextAlignment = ContentAlignment.MiddleLeft;
            txtPublishingHouse.Watermark = "";
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("楷体", 12F, FontStyle.Bold);
            label6.ForeColor = Color.White;
            label6.Location = new Point(3, 150);
            label6.Name = "label6";
            label6.Size = new Size(94, 30);
            label6.TabIndex = 10;
            label6.Text = "出版日期";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtpPublicationDate
            // 
            dtpPublicationDate.DateCultureInfo = new System.Globalization.CultureInfo("");
            dtpPublicationDate.Dock = DockStyle.Fill;
            dtpPublicationDate.FillColor = Color.White;
            dtpPublicationDate.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dtpPublicationDate.Location = new Point(104, 155);
            dtpPublicationDate.Margin = new Padding(4, 5, 4, 5);
            dtpPublicationDate.MaxLength = 10;
            dtpPublicationDate.MinimumSize = new Size(63, 0);
            dtpPublicationDate.Name = "dtpPublicationDate";
            dtpPublicationDate.Padding = new Padding(0, 0, 30, 2);
            dtpPublicationDate.Size = new Size(588, 20);
            dtpPublicationDate.SymbolDropDown = 61555;
            dtpPublicationDate.SymbolNormal = 61555;
            dtpPublicationDate.SymbolSize = 24;
            dtpPublicationDate.TabIndex = 11;
            dtpPublicationDate.Text = "2026-07-02";
            dtpPublicationDate.TextAlignment = ContentAlignment.MiddleLeft;
            dtpPublicationDate.Value = new DateTime(2026, 7, 2, 15, 35, 9, 122);
            dtpPublicationDate.Watermark = "";
            // 
            // txtLoansNumber
            // 
            txtLoansNumber.Dock = DockStyle.Fill;
            txtLoansNumber.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtLoansNumber.Location = new Point(104, 185);
            txtLoansNumber.Margin = new Padding(4, 5, 4, 5);
            txtLoansNumber.MinimumSize = new Size(1, 16);
            txtLoansNumber.Name = "txtLoansNumber";
            txtLoansNumber.Padding = new Padding(5);
            txtLoansNumber.ShowText = false;
            txtLoansNumber.Size = new Size(588, 20);
            txtLoansNumber.TabIndex = 13;
            txtLoansNumber.TextAlignment = ContentAlignment.MiddleLeft;
            txtLoansNumber.Watermark = "";
            // 
            // txtTotalNumber
            // 
            txtTotalNumber.Dock = DockStyle.Fill;
            txtTotalNumber.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtTotalNumber.Location = new Point(104, 215);
            txtTotalNumber.Margin = new Padding(4, 5, 4, 5);
            txtTotalNumber.MinimumSize = new Size(1, 16);
            txtTotalNumber.Name = "txtTotalNumber";
            txtTotalNumber.Padding = new Padding(5);
            txtTotalNumber.ShowText = false;
            txtTotalNumber.Size = new Size(588, 30);
            txtTotalNumber.TabIndex = 15;
            txtTotalNumber.TextAlignment = ContentAlignment.MiddleLeft;
            txtTotalNumber.Watermark = "";
            // 
            // label8
            // 
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("楷体", 12F, FontStyle.Bold);
            label8.ForeColor = Color.White;
            label8.Location = new Point(3, 210);
            label8.Name = "label8";
            label8.Size = new Size(94, 40);
            label8.TabIndex = 14;
            label8.Text = "总数量：";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("楷体", 12F, FontStyle.Bold);
            label7.ForeColor = Color.White;
            label7.Location = new Point(3, 180);
            label7.Name = "label7";
            label7.Size = new Size(94, 30);
            label7.TabIndex = 12;
            label7.Text = "可借数量";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.FillColor = Color.Transparent;
            btnCancel.FillColor2 = Color.Transparent;
            btnCancel.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnCancel.Location = new Point(419, 369);
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
            btnConfirm.Font = new Font("楷体", 12F, FontStyle.Bold);
            btnConfirm.Location = new Point(197, 369);
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
            // BookEditDialog
            // 
            AllowShowTitle = false;
            BackgroundImage = Properties.Resources.backgd1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(699, 454);
            Controls.Add(tableLayoutPanel);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BookEditDialog";
            Padding = new Padding(0);
            ShowTitle = false;
            StartPosition = FormStartPosition.CenterParent;
            ZoomScaleRect = new Rectangle(19, 19, 400, 380);
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
        private UILabel label8;
        private UITextBox txtBarCode;
        private UITextBox txtBookName;
        private UITextBox txtCategory;
        private UITextBox txtAuthor;
        private UITextBox txtPublishingHouse;
        private UIDatePicker dtpPublicationDate;
        private UITextBox txtLoansNumber;
        private UITextBox txtTotalNumber;
        private UIButton btnConfirm;
        private UIButton btnCancel;
    }
}