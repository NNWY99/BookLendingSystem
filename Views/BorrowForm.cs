using BookLendingSystem.BLL;
using BookLendingSystem.Model;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class BorrowForm : NoFlickerForm
    {
        private Admin currentAdmin;
        private BooksBLL booksBLL = new BooksBLL();
        private BorrowersBLL borrowersBLL = new BorrowersBLL();
        private BorrowBLL borrowBLL = new BorrowBLL();
        private List<int> selectedBookIds = new List<int>();
        private Borrowers selectedBorrower = null;

        public BorrowForm(Admin admin)
        {
            currentAdmin = admin;
            InitializeComponent();
            EnableDoubleBuffering(tableLayoutPanel);
            EnableDoubleBuffering(dgvBooks);
            EnableDoubleBuffering(leftPanel);
            EnableDoubleBuffering(rightPanel);
            EnableDoubleBuffering(leftTable);
            EnableDoubleBuffering(rightTable);
            EnableDoubleBuffering(lstSelectedBooks);
            LoadBooks();
        }

        private void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        private void LoadBooks()
        {
            try
            {
                dgvBooks.AutoGenerateColumns = false;
                dgvBooks.DataSource = booksBLL.GetAllBooks();
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"加载失败：{ex.Message}");
            }
        }

        private void btnSearchBook_Click(object sender, EventArgs e)
        {
            string keyword = txtBookKeyword.Text.Trim();
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    LoadBooks();
                else
                    dgvBooks.DataSource = booksBLL.SearchBooks(keyword);
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"搜索失败：{ex.Message}");
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                Books book = (Books)dgvBooks.SelectedRows[0].DataBoundItem;
                if (book.LoansNumber <= 0)
                {
                    UIMessageBox.ShowWarning("该书已无库存");
                    return;
                }
                if (!selectedBookIds.Contains(book.Id))
                {
                    selectedBookIds.Add(book.Id);
                    lstSelectedBooks.Items.Add($"{book.BookName} - {book.Author}");
                }
            }
        }

        private void btnRemoveBook_Click(object sender, EventArgs e)
        {
            if (lstSelectedBooks.SelectedIndex >= 0)
            {
                selectedBookIds.RemoveAt(lstSelectedBooks.SelectedIndex);
                lstSelectedBooks.Items.RemoveAt(lstSelectedBooks.SelectedIndex);
            }
        }

        private void btnSearchBorrower_Click(object sender, EventArgs e)
        {
            string keyword = txtBorrowerKeyword.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                UIMessageBox.ShowWarning("请输入借阅人姓名或身份证号");
                return;
            }

            try
            {
                Borrowers borrower = borrowersBLL.GetBorrowerByIDCard(keyword);
                if (borrower == null)
                {
                    var list = borrowersBLL.SearchBorrowers(keyword);
                    if (list.Count > 0)
                        borrower = list[0];
                }

                if (borrower != null)
                {
                    selectedBorrower = borrower;
                    lblBorrowerInfo.Text = $"姓名：{borrower.BorrowersName} | 身份证：{borrower.IDCard} | 电话：{borrower.Tel}";
                }
                else
                {
                    UIMessageBox.ShowWarning("未找到该借阅人");
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"搜索失败：{ex.Message}");
            }
        }

        private void btnConfirmBorrow_Click(object sender, EventArgs e)
        {
            if (selectedBorrower == null)
            {
                UIMessageBox.ShowWarning("请先选择借阅人");
                return;
            }
            if (selectedBookIds.Count == 0)
            {
                UIMessageBox.ShowWarning("请至少选择一本图书");
                return;
            }

            int days = 30;
            if (!string.IsNullOrEmpty(txtDays.Text) && !int.TryParse(txtDays.Text, out days))
            {
                UIMessageBox.ShowWarning("请输入有效的借阅天数");
                return;
            }

            if (MessageBox.Show(this, $"确认借阅 {selectedBookIds.Count} 本书给 {selectedBorrower.BorrowersName}，借阅期限 {days} 天？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (borrowBLL.CreateBorrow(currentAdmin.Id, selectedBorrower.Id, selectedBookIds, days))
                    {
                        UIMessageBox.ShowSuccess("借阅成功");
                        ClearSelection();
                        LoadBooks();
                    }
                    else
                    {
                        UIMessageBox.ShowError("借阅失败");
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError($"借阅失败：{ex.Message}");
                }
            }
        }

        private void ClearSelection()
        {
            selectedBookIds.Clear();
            lstSelectedBooks.Items.Clear();
            selectedBorrower = null;
            lblBorrowerInfo.Text = "请选择借阅人";
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            tableLayoutPanel = new NoFlickerTableLayoutPanel();
            label1 = new UILabel();
            leftPanel = new NoFlickerUIPanel();
            leftTable = new NoFlickerTableLayoutPanel();
            txtBookKeyword = new UITextBox();
            btnSearchBook = new UIButton();
            dgvBooks = new UIDataGridView();
            colId = new DataGridViewTextBoxColumn();
            colBarCode = new DataGridViewTextBoxColumn();
            colBookName = new DataGridViewTextBoxColumn();
            colCategory = new DataGridViewTextBoxColumn();
            colAuthor = new DataGridViewTextBoxColumn();
            colLoansNumber = new DataGridViewTextBoxColumn();
            btnAddBook = new UIButton();
            btnRemoveBook = new UIButton();
            rightPanel = new NoFlickerUIPanel();
            rightTable = new NoFlickerTableLayoutPanel();
            label3 = new UILabel();
            lstSelectedBooks = new NoFlickerListBox();
            label2 = new UILabel();
            txtBorrowerKeyword = new UITextBox();
            btnSearchBorrower = new UIButton();
            lblBorrowerInfo = new UILabel();
            label4 = new UILabel();
            txtDays = new UITextBox();
            btnConfirmBorrow = new UIButton();
            tableLayoutPanel.SuspendLayout();
            leftPanel.SuspendLayout();
            leftTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            rightPanel.SuspendLayout();
            rightTable.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = Color.Transparent;
            tableLayoutPanel.BackgroundImage = Properties.Resources.backgd1;
            tableLayoutPanel.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel.ColumnCount = 5;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel.Controls.Add(label1, 1, 0);
            tableLayoutPanel.Controls.Add(leftPanel, 1, 1);
            tableLayoutPanel.Controls.Add(rightPanel, 3, 1);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            tableLayoutPanel.Size = new Size(962, 576);
            tableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            tableLayoutPanel.SetColumnSpan(label1, 4);
            label1.Font = new Font("隶书", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.White;
            label1.Location = new Point(31, 0);
            label1.Name = "label1";
            label1.Size = new Size(189, 50);
            label1.TabIndex = 0;
            label1.Text = "借阅图书";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // leftPanel
            // 
            leftPanel.Controls.Add(leftTable);
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.FillColor = Color.White;
            leftPanel.FillColor2 = Color.White;
            leftPanel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            leftPanel.Location = new Point(32, 55);
            leftPanel.Margin = new Padding(4, 5, 4, 5);
            leftPanel.MinimumSize = new Size(1, 1);
            leftPanel.Name = "leftPanel";
            leftPanel.Radius = 8;
            leftPanel.RectColor = Color.FromArgb(220, 220, 220);
            leftPanel.Size = new Size(444, 516);
            leftPanel.TabIndex = 1;
            leftPanel.Text = null;
            leftPanel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // leftTable
            // 
            leftTable.ColumnCount = 2;
            leftTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            leftTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            leftTable.Controls.Add(txtBookKeyword, 0, 0);
            leftTable.Controls.Add(btnSearchBook, 1, 0);
            leftTable.Controls.Add(dgvBooks, 0, 1);
            leftTable.Controls.Add(btnAddBook, 0, 2);
            leftTable.Controls.Add(btnRemoveBook, 1, 2);
            leftTable.Dock = DockStyle.Fill;
            leftTable.Location = new Point(0, 0);
            leftTable.Name = "leftTable";
            leftTable.Padding = new Padding(10);
            leftTable.RowCount = 3;
            leftTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            leftTable.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            leftTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            leftTable.Size = new Size(444, 516);
            leftTable.TabIndex = 0;
            // 
            // txtBookKeyword
            // 
            txtBookKeyword.Dock = DockStyle.Fill;
            txtBookKeyword.Font = new Font("微软雅黑", 11F);
            txtBookKeyword.Location = new Point(14, 15);
            txtBookKeyword.Margin = new Padding(4, 5, 4, 5);
            txtBookKeyword.MinimumSize = new Size(1, 16);
            txtBookKeyword.Name = "txtBookKeyword";
            txtBookKeyword.Padding = new Padding(5);
            txtBookKeyword.ShowText = false;
            txtBookKeyword.Size = new Size(310, 35);
            txtBookKeyword.TabIndex = 0;
            txtBookKeyword.TextAlignment = ContentAlignment.MiddleLeft;
            txtBookKeyword.Watermark = "";
            // 
            // btnSearchBook
            // 
            btnSearchBook.Dock = DockStyle.Fill;
            btnSearchBook.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnSearchBook.Location = new Point(331, 13);
            btnSearchBook.MinimumSize = new Size(1, 1);
            btnSearchBook.Name = "btnSearchBook";
            btnSearchBook.Size = new Size(100, 39);
            btnSearchBook.Style = UIStyle.Custom;
            btnSearchBook.TabIndex = 1;
            btnSearchBook.Text = "搜索图书";
            btnSearchBook.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSearchBook.Click += btnSearchBook_Click;
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            dgvBooks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvBooks.BackgroundColor = Color.White;
            dgvBooks.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvBooks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvBooks.ColumnHeadersHeight = 32;
            dgvBooks.Columns.AddRange(new DataGridViewColumn[] { colId, colBarCode, colBookName, colCategory, colAuthor, colLoansNumber });
            leftTable.SetColumnSpan(dgvBooks, 2);
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvBooks.DefaultCellStyle = dataGridViewCellStyle6;
            dgvBooks.Dock = DockStyle.Fill;
            dgvBooks.EnableHeadersVisualStyles = false;
            dgvBooks.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvBooks.GridColor = Color.FromArgb(80, 160, 255);
            dgvBooks.Location = new Point(13, 58);
            dgvBooks.Name = "dgvBooks";
            dgvBooks.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle7.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle7.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle7.SelectionForeColor = Color.White;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dgvBooks.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dgvBooks.RowHeadersVisible = false;
            dgvBooks.RowHeadersWidth = 51;
            dataGridViewCellStyle8.BackColor = Color.White;
            dataGridViewCellStyle8.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvBooks.RowsDefaultCellStyle = dataGridViewCellStyle8;
            dgvBooks.SelectedIndex = -1;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(418, 400);
            dgvBooks.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvBooks.TabIndex = 2;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colId.DefaultCellStyle = dataGridViewCellStyle3;
            colId.HeaderText = "ID";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.Width = 50;
            // 
            // colBarCode
            // 
            colBarCode.DataPropertyName = "BarCode";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colBarCode.DefaultCellStyle = dataGridViewCellStyle4;
            colBarCode.HeaderText = "条码";
            colBarCode.MinimumWidth = 6;
            colBarCode.Name = "colBarCode";
            colBarCode.Width = 80;
            // 
            // colBookName
            // 
            colBookName.DataPropertyName = "BookName";
            colBookName.HeaderText = "书名";
            colBookName.MinimumWidth = 6;
            colBookName.Name = "colBookName";
            colBookName.Width = 150;
            // 
            // colCategory
            // 
            colCategory.DataPropertyName = "Category";
            colCategory.HeaderText = "类别";
            colCategory.MinimumWidth = 6;
            colCategory.Name = "colCategory";
            colCategory.Width = 80;
            // 
            // colAuthor
            // 
            colAuthor.DataPropertyName = "Author";
            colAuthor.HeaderText = "作者";
            colAuthor.MinimumWidth = 6;
            colAuthor.Name = "colAuthor";
            colAuthor.Width = 125;
            // 
            // colLoansNumber
            // 
            colLoansNumber.DataPropertyName = "LoansNumber";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colLoansNumber.DefaultCellStyle = dataGridViewCellStyle5;
            colLoansNumber.HeaderText = "库存";
            colLoansNumber.MinimumWidth = 6;
            colLoansNumber.Name = "colLoansNumber";
            colLoansNumber.Width = 60;
            // 
            // btnAddBook
            // 
            btnAddBook.Anchor = AnchorStyles.Left;
            btnAddBook.FillColor = Color.FromArgb(110, 190, 40);
            btnAddBook.FillColor2 = Color.FromArgb(110, 190, 40);
            btnAddBook.FillHoverColor = Color.FromArgb(139, 203, 83);
            btnAddBook.FillPressColor = Color.FromArgb(88, 152, 32);
            btnAddBook.FillSelectedColor = Color.FromArgb(88, 152, 32);
            btnAddBook.Font = new Font("微软雅黑", 11F);
            btnAddBook.LightColor = Color.FromArgb(245, 251, 241);
            btnAddBook.Location = new Point(13, 466);
            btnAddBook.MinimumSize = new Size(1, 1);
            btnAddBook.Name = "btnAddBook";
            btnAddBook.RectColor = Color.FromArgb(110, 190, 40);
            btnAddBook.RectHoverColor = Color.FromArgb(139, 203, 83);
            btnAddBook.RectPressColor = Color.FromArgb(88, 152, 32);
            btnAddBook.RectSelectedColor = Color.FromArgb(88, 152, 32);
            btnAddBook.Size = new Size(100, 35);
            btnAddBook.Style = UIStyle.Custom;
            btnAddBook.TabIndex = 3;
            btnAddBook.Text = "添加图书";
            btnAddBook.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddBook.Click += btnAddBook_Click;
            // 
            // btnRemoveBook
            // 
            btnRemoveBook.Anchor = AnchorStyles.Left;
            btnRemoveBook.FillColor = Color.FromArgb(230, 80, 80);
            btnRemoveBook.FillColor2 = Color.FromArgb(230, 80, 80);
            btnRemoveBook.FillHoverColor = Color.FromArgb(235, 115, 115);
            btnRemoveBook.FillPressColor = Color.FromArgb(184, 64, 64);
            btnRemoveBook.FillSelectedColor = Color.FromArgb(184, 64, 64);
            btnRemoveBook.Font = new Font("微软雅黑", 11F);
            btnRemoveBook.LightColor = Color.FromArgb(253, 243, 243);
            btnRemoveBook.Location = new Point(331, 466);
            btnRemoveBook.MinimumSize = new Size(1, 1);
            btnRemoveBook.Name = "btnRemoveBook";
            btnRemoveBook.RectColor = Color.FromArgb(230, 80, 80);
            btnRemoveBook.RectHoverColor = Color.FromArgb(235, 115, 115);
            btnRemoveBook.RectPressColor = Color.FromArgb(184, 64, 64);
            btnRemoveBook.RectSelectedColor = Color.FromArgb(184, 64, 64);
            btnRemoveBook.Size = new Size(99, 35);
            btnRemoveBook.Style = UIStyle.Custom;
            btnRemoveBook.TabIndex = 4;
            btnRemoveBook.Text = "移除图书";
            btnRemoveBook.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRemoveBook.Click += btnRemoveBook_Click;
            // 
            // rightPanel
            // 
            rightPanel.Controls.Add(rightTable);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.FillColor = Color.White;
            rightPanel.FillColor2 = Color.White;
            rightPanel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            rightPanel.Location = new Point(532, 55);
            rightPanel.Margin = new Padding(4, 5, 4, 5);
            rightPanel.MinimumSize = new Size(1, 1);
            rightPanel.Name = "rightPanel";
            rightPanel.Radius = 8;
            rightPanel.RectColor = Color.FromArgb(220, 220, 220);
            rightPanel.Size = new Size(376, 516);
            rightPanel.TabIndex = 2;
            rightPanel.Text = null;
            rightPanel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // rightTable
            // 
            rightTable.ColumnCount = 2;
            rightTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            rightTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            rightTable.Controls.Add(label3, 0, 0);
            rightTable.Controls.Add(lstSelectedBooks, 0, 1);
            rightTable.Controls.Add(label2, 0, 2);
            rightTable.Controls.Add(txtBorrowerKeyword, 1, 2);
            rightTable.Controls.Add(btnSearchBorrower, 0, 3);
            rightTable.Controls.Add(lblBorrowerInfo, 0, 4);
            rightTable.Controls.Add(label4, 0, 5);
            rightTable.Controls.Add(txtDays, 1, 5);
            rightTable.Controls.Add(btnConfirmBorrow, 0, 6);
            rightTable.Dock = DockStyle.Fill;
            rightTable.Location = new Point(0, 0);
            rightTable.Name = "rightTable";
            rightTable.Padding = new Padding(10);
            rightTable.RowCount = 7;
            rightTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            rightTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            rightTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            rightTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            rightTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            rightTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            rightTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            rightTable.Size = new Size(376, 516);
            rightTable.TabIndex = 0;
            // 
            // label3
            // 
            rightTable.SetColumnSpan(label3, 2);
            label3.Dock = DockStyle.Left;
            label3.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label3.ForeColor = Color.FromArgb(50, 60, 80);
            label3.Location = new Point(13, 10);
            label3.Name = "label3";
            label3.Size = new Size(110, 35);
            label3.TabIndex = 0;
            label3.Text = "已选图书";
            // 
            // lstSelectedBooks
            // 
            lstSelectedBooks.BorderStyle = BorderStyle.FixedSingle;
            rightTable.SetColumnSpan(lstSelectedBooks, 2);
            lstSelectedBooks.Dock = DockStyle.Fill;
            lstSelectedBooks.Font = new Font("微软雅黑", 10F);
            lstSelectedBooks.ItemHeight = 23;
            lstSelectedBooks.Location = new Point(13, 48);
            lstSelectedBooks.Name = "lstSelectedBooks";
            lstSelectedBooks.Size = new Size(350, 225);
            lstSelectedBooks.TabIndex = 1;
            // 
            // label2
            // 
            label2.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label2.ForeColor = Color.FromArgb(48, 48, 48);
            label2.Location = new Point(13, 276);
            label2.Name = "label2";
            label2.Size = new Size(118, 45);
            label2.TabIndex = 2;
            label2.Text = "借阅人：";
            // 
            // txtBorrowerKeyword
            // 
            txtBorrowerKeyword.Dock = DockStyle.Fill;
            txtBorrowerKeyword.Font = new Font("微软雅黑", 11F);
            txtBorrowerKeyword.Location = new Point(138, 281);
            txtBorrowerKeyword.Margin = new Padding(4, 5, 4, 5);
            txtBorrowerKeyword.MinimumSize = new Size(1, 16);
            txtBorrowerKeyword.Name = "txtBorrowerKeyword";
            txtBorrowerKeyword.Padding = new Padding(5);
            txtBorrowerKeyword.ShowText = false;
            txtBorrowerKeyword.Size = new Size(224, 35);
            txtBorrowerKeyword.TabIndex = 3;
            txtBorrowerKeyword.TextAlignment = ContentAlignment.MiddleLeft;
            txtBorrowerKeyword.Watermark = "";
            // 
            // btnSearchBorrower
            // 
            rightTable.SetColumnSpan(btnSearchBorrower, 2);
            btnSearchBorrower.Dock = DockStyle.Left;
            btnSearchBorrower.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnSearchBorrower.Location = new Point(13, 324);
            btnSearchBorrower.MinimumSize = new Size(1, 1);
            btnSearchBorrower.Name = "btnSearchBorrower";
            btnSearchBorrower.Size = new Size(118, 34);
            btnSearchBorrower.Style = UIStyle.Custom;
            btnSearchBorrower.TabIndex = 4;
            btnSearchBorrower.Text = "查找借阅人";
            btnSearchBorrower.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSearchBorrower.Click += btnSearchBorrower_Click;
            // 
            // lblBorrowerInfo
            // 
            rightTable.SetColumnSpan(lblBorrowerInfo, 2);
            lblBorrowerInfo.Dock = DockStyle.Fill;
            lblBorrowerInfo.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblBorrowerInfo.ForeColor = Color.FromArgb(60, 120, 200);
            lblBorrowerInfo.Location = new Point(13, 361);
            lblBorrowerInfo.Name = "lblBorrowerInfo";
            lblBorrowerInfo.Size = new Size(350, 50);
            lblBorrowerInfo.TabIndex = 5;
            lblBorrowerInfo.Text = "请选择借阅人";
            lblBorrowerInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label4.ForeColor = Color.FromArgb(48, 48, 48);
            label4.Location = new Point(13, 411);
            label4.Name = "label4";
            label4.Size = new Size(118, 40);
            label4.TabIndex = 6;
            label4.Text = "借阅天数：";
            // 
            // txtDays
            // 
            txtDays.Anchor = AnchorStyles.Left;
            txtDays.DoubleValue = 30D;
            txtDays.Font = new Font("微软雅黑", 11F);
            txtDays.IntValue = 30;
            txtDays.Location = new Point(138, 416);
            txtDays.Margin = new Padding(4, 5, 4, 5);
            txtDays.MinimumSize = new Size(1, 16);
            txtDays.Name = "txtDays";
            txtDays.Padding = new Padding(5);
            txtDays.ShowText = false;
            txtDays.Size = new Size(80, 35);
            txtDays.TabIndex = 7;
            txtDays.Text = "30";
            txtDays.TextAlignment = ContentAlignment.MiddleLeft;
            txtDays.Watermark = "";
            // 
            // btnConfirmBorrow
            // 
            btnConfirmBorrow.Anchor = AnchorStyles.Right;
            rightTable.SetColumnSpan(btnConfirmBorrow, 2);
            btnConfirmBorrow.FillColor = Color.FromArgb(110, 190, 40);
            btnConfirmBorrow.FillColor2 = Color.FromArgb(110, 190, 40);
            btnConfirmBorrow.FillHoverColor = Color.FromArgb(139, 203, 83);
            btnConfirmBorrow.FillPressColor = Color.FromArgb(88, 152, 32);
            btnConfirmBorrow.FillSelectedColor = Color.FromArgb(88, 152, 32);
            btnConfirmBorrow.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnConfirmBorrow.LightColor = Color.FromArgb(245, 251, 241);
            btnConfirmBorrow.Location = new Point(242, 462);
            btnConfirmBorrow.MinimumSize = new Size(1, 1);
            btnConfirmBorrow.Name = "btnConfirmBorrow";
            btnConfirmBorrow.RectColor = Color.FromArgb(110, 190, 40);
            btnConfirmBorrow.RectHoverColor = Color.FromArgb(139, 203, 83);
            btnConfirmBorrow.RectPressColor = Color.FromArgb(88, 152, 32);
            btnConfirmBorrow.RectSelectedColor = Color.FromArgb(88, 152, 32);
            btnConfirmBorrow.Size = new Size(121, 38);
            btnConfirmBorrow.Style = UIStyle.Custom;
            btnConfirmBorrow.TabIndex = 8;
            btnConfirmBorrow.Text = "确认借阅";
            btnConfirmBorrow.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnConfirmBorrow.Click += btnConfirmBorrow_Click;
            // 
            // BorrowForm
            // 
            AllowShowTitle = false;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(962, 576);
            Controls.Add(tableLayoutPanel);
            Name = "BorrowForm";
            Padding = new Padding(0);
            ShowTitle = false;
            Text = "借阅图书";
            ZoomScaleRect = new Rectangle(19, 19, 950, 580);
            tableLayoutPanel.ResumeLayout(false);
            leftPanel.ResumeLayout(false);
            leftTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            rightPanel.ResumeLayout(false);
            rightTable.ResumeLayout(false);
            ResumeLayout(false);
        }

        private NoFlickerTableLayoutPanel tableLayoutPanel;
        private NoFlickerUIPanel leftPanel;
        private NoFlickerUIPanel rightPanel;
        private NoFlickerTableLayoutPanel leftTable;
        private NoFlickerTableLayoutPanel rightTable;
        private UIDataGridView dgvBooks;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colBarCode;
        private DataGridViewTextBoxColumn colBookName;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colAuthor;
        private DataGridViewTextBoxColumn colLoansNumber;
        private NoFlickerListBox lstSelectedBooks;
        private UITextBox txtBookKeyword;
        private UITextBox txtBorrowerKeyword;
        private UITextBox txtDays;
        private UIButton btnSearchBook;
        private UIButton btnSearchBorrower;
        private UIButton btnAddBook;
        private UIButton btnRemoveBook;
        private UIButton btnConfirmBorrow;
        private UILabel label1;
        private UILabel label2;
        private UILabel label3;
        private UILabel label4;
        private UILabel lblBorrowerInfo;
    }
}
