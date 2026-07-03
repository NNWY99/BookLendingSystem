using BookLendingSystem.BLL;
using BookLendingSystem.Model;
using Sunny.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class BookManageForm : NoFlickerForm
    {
        private BooksBLL booksBLL = new BooksBLL();
        private Books currentBook = null;

        public BookManageForm()
        {
            InitializeComponent();
            EnableDoubleBuffering(tableLayoutPanel);
            EnableDoubleBuffering(dgvBooks);
            LoadCategories();
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

        private void LoadCategories()
        {
            try
            {
                cboCategory.Items.Clear();
                cboCategory.Items.Add("全部类别");
                foreach (string category in booksBLL.GetAllCategories())
                {
                    cboCategory.Items.Add(category);
                }
                cboCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"加载类别失败：{ex.Message}");
            }
        }

        private void LoadBooks(string category = null)
        {
            try
            {
                dgvBooks.AutoGenerateColumns = false;
                if (string.IsNullOrEmpty(category) || category == "全部类别")
                    dgvBooks.DataSource = booksBLL.GetAllBooks();
                else
                    dgvBooks.DataSource = booksBLL.GetBooksByCategory(category);
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"加载失败：{ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BookEditDialog dialog = new BookEditDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if (booksBLL.AddBook(dialog.Book))
                        UIMessageBox.ShowSuccess("添加成功");
                    else
                        UIMessageBox.ShowError("添加失败");
                    LoadCategories();
                    LoadBooks(cboCategory.SelectedItem?.ToString());
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError($"操作失败：{ex.Message}");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentBook == null)
            {
                UIMessageBox.ShowWarning("请先选择要编辑的图书");
                return;
            }

            BookEditDialog dialog = new BookEditDialog(currentBook);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    dialog.Book.Id = currentBook.Id;
                    if (booksBLL.UpdateBook(dialog.Book))
                        UIMessageBox.ShowSuccess("更新成功");
                    else
                        UIMessageBox.ShowError("更新失败");
                    LoadCategories();
                    LoadBooks(cboCategory.SelectedItem?.ToString());
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError($"操作失败：{ex.Message}");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentBook == null)
            {
                UIMessageBox.ShowWarning("请先选择要删除的图书");
                return;
            }

            if (MessageBox.Show(this, $"确定要删除图书「{currentBook.BookName}」吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (booksBLL.DeleteBook(currentBook.Id))
                        UIMessageBox.ShowSuccess("删除成功");
                    else
                        UIMessageBox.ShowError("删除失败");
                    LoadCategories();
                    LoadBooks(cboCategory.SelectedItem?.ToString());
                    currentBook = null;
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError($"删除失败：{ex.Message}");
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    LoadBooks(cboCategory.SelectedItem?.ToString());
                else
                    dgvBooks.DataSource = booksBLL.SearchBooks(keyword);
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"搜索失败：{ex.Message}");
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (currentBook == null)
            {
                UIMessageBox.ShowWarning("请先选择要查看的图书");
                return;
            }

            BookDetailDialog dialog = new BookDetailDialog(currentBook);
            dialog.ShowDialog(this);
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBooks(cboCategory.SelectedItem?.ToString());
        }

        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                currentBook = (Books)dgvBooks.SelectedRows[0].DataBoundItem;
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            tableLayoutPanel = new TableLayoutPanel();
            label1 = new UILabel();
            cboCategory = new UIComboBox();
            txtKeyword = new UITextBox();
            btnSearch = new UIButton();
            dgvBooks = new UIDataGridView();
            colId = new DataGridViewTextBoxColumn();
            colBarCode = new DataGridViewTextBoxColumn();
            colBookName = new DataGridViewTextBoxColumn();
            colCategory = new DataGridViewTextBoxColumn();
            colAuthor = new DataGridViewTextBoxColumn();
            colPublishingHouse = new DataGridViewTextBoxColumn();
            colPublicationDate = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            colLoansNumber = new DataGridViewTextBoxColumn();
            colTotalNumber = new DataGridViewTextBoxColumn();
            flowLayoutPanel = new FlowLayoutPanel();
            btnAdd = new UIButton();
            btnEdit = new UIButton();
            btnDelete = new UIButton();
            btnDetail = new UIButton();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            flowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = Color.Transparent;
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 87F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8F));
            tableLayoutPanel.Controls.Add(label1, 1, 0);
            tableLayoutPanel.Controls.Add(cboCategory, 1, 1);
            tableLayoutPanel.Controls.Add(txtKeyword, 1, 2);
            tableLayoutPanel.Controls.Add(btnSearch, 2, 2);
            tableLayoutPanel.Controls.Add(dgvBooks, 1, 3);
            tableLayoutPanel.Controls.Add(flowLayoutPanel, 1, 4);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tableLayoutPanel.Size = new Size(800, 480);
            tableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.Font = new Font("隶书", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.White;
            label1.Location = new Point(43, 0);
            label1.Name = "label1";
            label1.Size = new Size(184, 60);
            label1.TabIndex = 0;
            label1.Text = "图书管理";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboCategory
            // 
            cboCategory.DataSource = null;
            cboCategory.Dock = DockStyle.Fill;
            cboCategory.FillColor = Color.White;
            cboCategory.Font = new Font("微软雅黑", 11F);
            cboCategory.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboCategory.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboCategory.Location = new Point(44, 65);
            cboCategory.Margin = new Padding(4, 5, 4, 5);
            cboCategory.MinimumSize = new Size(1, 16);
            cboCategory.Name = "cboCategory";
            cboCategory.Padding = new Padding(5, 5, 30, 5);
            cboCategory.Size = new Size(688, 35);
            cboCategory.SymbolSize = 24;
            cboCategory.TabIndex = 5;
            cboCategory.TextAlignment = ContentAlignment.MiddleLeft;
            cboCategory.Watermark = "";
            cboCategory.SelectedIndexChanged += cboCategory_SelectedIndexChanged;
            // 
            // txtKeyword
            // 
            txtKeyword.Dock = DockStyle.Fill;
            txtKeyword.Font = new Font("微软雅黑", 11F);
            txtKeyword.Location = new Point(44, 110);
            txtKeyword.Margin = new Padding(4, 5, 4, 5);
            txtKeyword.MinimumSize = new Size(1, 16);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Padding = new Padding(5);
            txtKeyword.ShowText = false;
            txtKeyword.Size = new Size(688, 35);
            txtKeyword.TabIndex = 1;
            txtKeyword.TextAlignment = ContentAlignment.MiddleLeft;
            txtKeyword.Watermark = "";
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.FillColor = Color.Transparent;
            btnSearch.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnSearch.Location = new Point(739, 108);
            btnSearch.MinimumSize = new Size(1, 1);
            btnSearch.Name = "btnSearch";
            btnSearch.RectColor = Color.White;
            btnSearch.Size = new Size(58, 39);
            btnSearch.Style = UIStyle.Custom;
            btnSearch.TabIndex = 2;
            btnSearch.Text = "搜索";
            btnSearch.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSearch.Click += btnSearch_Click;
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
            dgvBooks.Columns.AddRange(new DataGridViewColumn[] { colId, colBarCode, colBookName, colCategory, colAuthor, colPublishingHouse, colPublicationDate, colDescription, colLoansNumber, colTotalNumber });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dgvBooks.DefaultCellStyle = dataGridViewCellStyle8;
            dgvBooks.Dock = DockStyle.Fill;
            dgvBooks.EnableHeadersVisualStyles = false;
            dgvBooks.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvBooks.GridColor = Color.FromArgb(80, 160, 255);
            dgvBooks.Location = new Point(43, 153);
            dgvBooks.Name = "dgvBooks";
            dgvBooks.RectColor = Color.White;
            dgvBooks.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle9.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dgvBooks.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dgvBooks.RowHeadersVisible = false;
            dgvBooks.RowHeadersWidth = 51;
            dataGridViewCellStyle10.BackColor = Color.White;
            dataGridViewCellStyle10.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvBooks.RowsDefaultCellStyle = dataGridViewCellStyle10;
            dgvBooks.SelectedIndex = -1;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(690, 259);
            dgvBooks.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvBooks.TabIndex = 3;
            dgvBooks.SelectionChanged += dgvBooks_SelectionChanged;
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
            // colPublishingHouse
            // 
            colPublishingHouse.DataPropertyName = "PublishingHouse";
            colPublishingHouse.HeaderText = "出版社";
            colPublishingHouse.MinimumWidth = 6;
            colPublishingHouse.Name = "colPublishingHouse";
            colPublishingHouse.Width = 120;
            // 
            // colPublicationDate
            // 
            colPublicationDate.DataPropertyName = "PublicationDate";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPublicationDate.DefaultCellStyle = dataGridViewCellStyle5;
            colPublicationDate.HeaderText = "出版日期";
            colPublicationDate.MinimumWidth = 6;
            colPublicationDate.Name = "colPublicationDate";
            colPublicationDate.Width = 125;
            // 
            // colDescription
            // 
            colDescription.DataPropertyName = "Description";
            colDescription.HeaderText = "简介";
            colDescription.MinimumWidth = 6;
            colDescription.Name = "colDescription";
            colDescription.Width = 200;
            // 
            // colLoansNumber
            // 
            colLoansNumber.DataPropertyName = "LoansNumber";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colLoansNumber.DefaultCellStyle = dataGridViewCellStyle6;
            colLoansNumber.HeaderText = "库存";
            colLoansNumber.MinimumWidth = 6;
            colLoansNumber.Name = "colLoansNumber";
            colLoansNumber.Width = 60;
            // 
            // colTotalNumber
            // 
            colTotalNumber.DataPropertyName = "TotalNumber";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTotalNumber.DefaultCellStyle = dataGridViewCellStyle7;
            colTotalNumber.HeaderText = "总数";
            colTotalNumber.MinimumWidth = 6;
            colTotalNumber.Name = "colTotalNumber";
            colTotalNumber.Width = 60;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Controls.Add(btnAdd);
            flowLayoutPanel.Controls.Add(btnEdit);
            flowLayoutPanel.Controls.Add(btnDelete);
            flowLayoutPanel.Controls.Add(btnDetail);
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.Location = new Point(43, 418);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Padding = new Padding(10);
            flowLayoutPanel.Size = new Size(690, 59);
            flowLayoutPanel.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.FillColor = Color.FromArgb(110, 190, 40);
            btnAdd.FillColor2 = Color.FromArgb(110, 190, 40);
            btnAdd.FillHoverColor = Color.FromArgb(139, 203, 83);
            btnAdd.FillPressColor = Color.FromArgb(88, 152, 32);
            btnAdd.FillSelectedColor = Color.FromArgb(88, 152, 32);
            btnAdd.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnAdd.LightColor = Color.FromArgb(245, 251, 241);
            btnAdd.Location = new Point(15, 10);
            btnAdd.Margin = new Padding(5, 0, 5, 0);
            btnAdd.MinimumSize = new Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.RectColor = Color.FromArgb(110, 190, 40);
            btnAdd.RectHoverColor = Color.FromArgb(139, 203, 83);
            btnAdd.RectPressColor = Color.FromArgb(88, 152, 32);
            btnAdd.RectSelectedColor = Color.FromArgb(88, 152, 32);
            btnAdd.Size = new Size(90, 38);
            btnAdd.Style = UIStyle.Custom;
            btnAdd.TabIndex = 0;
            btnAdd.Text = "添加";
            btnAdd.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.FillColor = Color.FromArgb(220, 155, 40);
            btnEdit.FillColor2 = Color.FromArgb(220, 155, 40);
            btnEdit.FillHoverColor = Color.FromArgb(227, 175, 83);
            btnEdit.FillPressColor = Color.FromArgb(176, 124, 32);
            btnEdit.FillSelectedColor = Color.FromArgb(176, 124, 32);
            btnEdit.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnEdit.LightColor = Color.FromArgb(253, 249, 241);
            btnEdit.Location = new Point(115, 10);
            btnEdit.Margin = new Padding(5, 0, 5, 0);
            btnEdit.MinimumSize = new Size(1, 1);
            btnEdit.Name = "btnEdit";
            btnEdit.RectColor = Color.FromArgb(220, 155, 40);
            btnEdit.RectHoverColor = Color.FromArgb(227, 175, 83);
            btnEdit.RectPressColor = Color.FromArgb(176, 124, 32);
            btnEdit.RectSelectedColor = Color.FromArgb(176, 124, 32);
            btnEdit.Size = new Size(90, 38);
            btnEdit.Style = UIStyle.Custom;
            btnEdit.TabIndex = 1;
            btnEdit.Text = "编辑";
            btnEdit.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.FillColor = Color.FromArgb(230, 80, 80);
            btnDelete.FillColor2 = Color.FromArgb(230, 80, 80);
            btnDelete.FillHoverColor = Color.FromArgb(235, 115, 115);
            btnDelete.FillPressColor = Color.FromArgb(184, 64, 64);
            btnDelete.FillSelectedColor = Color.FromArgb(184, 64, 64);
            btnDelete.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnDelete.LightColor = Color.FromArgb(253, 243, 243);
            btnDelete.Location = new Point(215, 10);
            btnDelete.Margin = new Padding(5, 0, 5, 0);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectColor = Color.FromArgb(230, 80, 80);
            btnDelete.RectHoverColor = Color.FromArgb(235, 115, 115);
            btnDelete.RectPressColor = Color.FromArgb(184, 64, 64);
            btnDelete.RectSelectedColor = Color.FromArgb(184, 64, 64);
            btnDelete.Size = new Size(90, 38);
            btnDelete.Style = UIStyle.Custom;
            btnDelete.TabIndex = 2;
            btnDelete.Text = "删除";
            btnDelete.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.Click += btnDelete_Click;
            // 
            // btnDetail
            // 
            btnDetail.FillHoverColor = Color.FromArgb(106, 181, 255);
            btnDetail.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnDetail.Location = new Point(315, 10);
            btnDetail.Margin = new Padding(5, 0, 5, 0);
            btnDetail.MinimumSize = new Size(1, 1);
            btnDetail.Name = "btnDetail";
            btnDetail.RectHoverColor = Color.FromArgb(106, 181, 255);
            btnDetail.Size = new Size(90, 38);
            btnDetail.Style = UIStyle.Custom;
            btnDetail.TabIndex = 3;
            btnDetail.Text = "详情";
            btnDetail.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDetail.Click += btnDetail_Click;
            // 
            // BookManageForm
            // 
            AllowShowTitle = false;
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImage = Properties.Resources.backgd1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 480);
            Controls.Add(tableLayoutPanel);
            Name = "BookManageForm";
            Padding = new Padding(0);
            RectColor = Color.Transparent;
            ShowTitle = false;
            Text = "图书管理";
            TitleColor = Color.Transparent;
            TitleFont = new Font("楷体", 12F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(19, 19, 800, 480);
            tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            flowLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private FlowLayoutPanel flowLayoutPanel;
        private UIDataGridView dgvBooks;
        private UITextBox txtKeyword;
        private UIButton btnSearch;
        private UIButton btnAdd;
        private UIButton btnEdit;
        private UIButton btnDelete;
        private UIButton btnDetail;
        private UIComboBox cboCategory;
        private UILabel label1;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colBarCode;
        private DataGridViewTextBoxColumn colBookName;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colAuthor;
        private DataGridViewTextBoxColumn colPublishingHouse;
        private DataGridViewTextBoxColumn colPublicationDate;
        private DataGridViewTextBoxColumn colDescription;
        private DataGridViewTextBoxColumn colLoansNumber;
        private DataGridViewTextBoxColumn colTotalNumber;
    }
}