using BookLendingSystem.BLL;
using BookLendingSystem.Model;
using Sunny.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class BorrowerManageForm : NoFlickerForm
    {
        private BorrowersBLL borrowersBLL = new BorrowersBLL();
        private Borrowers currentBorrower = null;

        public BorrowerManageForm()
        {
            InitializeComponent();
            EnableDoubleBuffering(tableLayoutPanel);
            EnableDoubleBuffering(dgvBorrowers);
            LoadBorrowers();
        }

        private void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        private void LoadBorrowers()
        {
            try
            {
                dgvBorrowers.AutoGenerateColumns = false;
                dgvBorrowers.DataSource = borrowersBLL.GetAllBorrowers();
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"加载失败：{ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BorrowerEditDialog dialog = new BorrowerEditDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if (borrowersBLL.AddBorrower(dialog.Borrower))
                        UIMessageBox.ShowSuccess("添加成功");
                    else
                        UIMessageBox.ShowError("添加失败");
                    LoadBorrowers();
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError($"操作失败：{ex.Message}");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentBorrower == null)
            {
                UIMessageBox.ShowWarning("请先选择要编辑的借阅人");
                return;
            }

            BorrowerEditDialog dialog = new BorrowerEditDialog(currentBorrower);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    dialog.Borrower.Id = currentBorrower.Id;
                    if (borrowersBLL.UpdateBorrower(dialog.Borrower))
                        UIMessageBox.ShowSuccess("更新成功");
                    else
                        UIMessageBox.ShowError("更新失败");
                    LoadBorrowers();
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError($"操作失败：{ex.Message}");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentBorrower == null)
            {
                UIMessageBox.ShowWarning("请先选择要删除的借阅人");
                return;
            }

            if (MessageBox.Show(this, $"确定要删除借阅人「{currentBorrower.BorrowersName}」吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (borrowersBLL.DeleteBorrower(currentBorrower.Id))
                        UIMessageBox.ShowSuccess("删除成功");
                    else
                        UIMessageBox.ShowError("删除失败");
                    LoadBorrowers();
                    currentBorrower = null;
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
                    LoadBorrowers();
                else
                    dgvBorrowers.DataSource = borrowersBLL.SearchBorrowers(keyword);
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"搜索失败：{ex.Message}");
            }
        }

        private void dgvBorrowers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBorrowers.SelectedRows.Count > 0)
            {
                currentBorrower = (Borrowers)dgvBorrowers.SelectedRows[0].DataBoundItem;
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            tableLayoutPanel = new TableLayoutPanel();
            label1 = new UILabel();
            txtKeyword = new UITextBox();
            btnSearch = new UIButton();
            dgvBorrowers = new UIDataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colIDCard = new DataGridViewTextBoxColumn();
            colSex = new DataGridViewTextBoxColumn();
            colTel = new DataGridViewTextBoxColumn();
            colCode = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colOrderNumber = new DataGridViewTextBoxColumn();
            flowLayoutPanel = new FlowLayoutPanel();
            btnAdd = new UIButton();
            btnEdit = new UIButton();
            btnDelete = new UIButton();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBorrowers).BeginInit();
            flowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = Color.Transparent;
            tableLayoutPanel.BackgroundImage = Properties.Resources.backgd1;
            tableLayoutPanel.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 87F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8F));
            tableLayoutPanel.Controls.Add(label1, 1, 0);
            tableLayoutPanel.Controls.Add(txtKeyword, 1, 1);
            tableLayoutPanel.Controls.Add(btnSearch, 2, 1);
            tableLayoutPanel.Controls.Add(dgvBorrowers, 1, 2);
            tableLayoutPanel.Controls.Add(flowLayoutPanel, 1, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tableLayoutPanel.Size = new Size(900, 580);
            tableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.Font = new Font("隶书", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.White;
            label1.Location = new Point(48, 0);
            label1.Name = "label1";
            label1.Size = new Size(225, 60);
            label1.TabIndex = 0;
            label1.Text = "借阅人管理";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtKeyword
            // 
            txtKeyword.Dock = DockStyle.Fill;
            txtKeyword.Font = new Font("微软雅黑", 11F);
            txtKeyword.Location = new Point(49, 65);
            txtKeyword.Margin = new Padding(4, 5, 4, 5);
            txtKeyword.MinimumSize = new Size(1, 16);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Padding = new Padding(5);
            txtKeyword.ShowText = false;
            txtKeyword.Size = new Size(775, 40);
            txtKeyword.TabIndex = 1;
            txtKeyword.TextAlignment = ContentAlignment.MiddleLeft;
            txtKeyword.Watermark = "";
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.FillColor = Color.Transparent;
            btnSearch.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnSearch.Location = new Point(831, 63);
            btnSearch.MinimumSize = new Size(1, 1);
            btnSearch.Name = "btnSearch";
            btnSearch.RectColor = Color.White;
            btnSearch.Size = new Size(66, 44);
            btnSearch.Style = UIStyle.Custom;
            btnSearch.TabIndex = 2;
            btnSearch.Text = "搜索";
            btnSearch.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvBorrowers
            // 
            dgvBorrowers.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            dgvBorrowers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvBorrowers.BackgroundColor = Color.White;
            dgvBorrowers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvBorrowers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvBorrowers.ColumnHeadersHeight = 32;
            dgvBorrowers.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colIDCard, colSex, colTel, colCode, colPrice, colOrderNumber });
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Window;
            dataGridViewCellStyle7.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle7.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            dgvBorrowers.DefaultCellStyle = dataGridViewCellStyle7;
            dgvBorrowers.Dock = DockStyle.Fill;
            dgvBorrowers.EnableHeadersVisualStyles = false;
            dgvBorrowers.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvBorrowers.GridColor = Color.FromArgb(80, 160, 255);
            dgvBorrowers.Location = new Point(48, 113);
            dgvBorrowers.Name = "dgvBorrowers";
            dgvBorrowers.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle8.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle8.SelectionForeColor = Color.White;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            dgvBorrowers.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dgvBorrowers.RowHeadersVisible = false;
            dgvBorrowers.RowHeadersWidth = 51;
            dataGridViewCellStyle9.BackColor = Color.White;
            dataGridViewCellStyle9.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvBorrowers.RowsDefaultCellStyle = dataGridViewCellStyle9;
            dgvBorrowers.SelectedIndex = -1;
            dgvBorrowers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBorrowers.Size = new Size(777, 399);
            dgvBorrowers.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvBorrowers.TabIndex = 3;
            dgvBorrowers.SelectionChanged += dgvBorrowers_SelectionChanged;
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
            // colName
            // 
            colName.DataPropertyName = "BorrowersName";
            colName.HeaderText = "姓名";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.Width = 125;
            // 
            // colIDCard
            // 
            colIDCard.DataPropertyName = "IDCard";
            colIDCard.HeaderText = "身份证";
            colIDCard.MinimumWidth = 6;
            colIDCard.Name = "colIDCard";
            colIDCard.Width = 180;
            // 
            // colSex
            // 
            colSex.DataPropertyName = "Sex";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSex.DefaultCellStyle = dataGridViewCellStyle4;
            colSex.HeaderText = "性别";
            colSex.MinimumWidth = 6;
            colSex.Name = "colSex";
            colSex.Width = 60;
            // 
            // colTel
            // 
            colTel.DataPropertyName = "Tel";
            colTel.HeaderText = "电话";
            colTel.MinimumWidth = 6;
            colTel.Name = "colTel";
            colTel.Width = 120;
            // 
            // colCode
            // 
            colCode.DataPropertyName = "BorrowingCode";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCode.DefaultCellStyle = dataGridViewCellStyle5;
            colCode.HeaderText = "借阅码";
            colCode.MinimumWidth = 6;
            colCode.Name = "colCode";
            colCode.Width = 80;
            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "Price";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPrice.DefaultCellStyle = dataGridViewCellStyle6;
            colPrice.HeaderText = "押金";
            colPrice.MinimumWidth = 6;
            colPrice.Name = "colPrice";
            colPrice.Width = 80;
            // 
            // colOrderNumber
            // 
            colOrderNumber.DataPropertyName = "OrderNumber";
            colOrderNumber.HeaderText = "编号";
            colOrderNumber.MinimumWidth = 6;
            colOrderNumber.Name = "colOrderNumber";
            colOrderNumber.Width = 125;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Controls.Add(btnAdd);
            flowLayoutPanel.Controls.Add(btnEdit);
            flowLayoutPanel.Controls.Add(btnDelete);
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.Location = new Point(48, 518);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Padding = new Padding(10);
            flowLayoutPanel.Size = new Size(777, 59);
            flowLayoutPanel.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.FillColor = Color.FromArgb(110, 190, 40);
            btnAdd.FillColor2 = Color.FromArgb(110, 190, 40);
            btnAdd.FillHoverColor = Color.FromArgb(139, 203, 83);
            btnAdd.FillPressColor = Color.FromArgb(88, 152, 32);
            btnAdd.FillSelectedColor = Color.FromArgb(88, 152, 32);
            btnAdd.Font = new Font("微软雅黑", 11F);
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
            btnEdit.Font = new Font("微软雅黑", 11F);
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
            btnDelete.Font = new Font("微软雅黑", 11F);
            btnDelete.LightColor = Color.FromArgb(253, 243, 243);
            btnDelete.Location = new Point(215, 10);
            btnDelete.Margin = new Padding(5, 0, 5, 0);
            btnDelete.MinimumSize = new Size(1, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.RectColor = Color.FromArgb(230, 80, 80);
            btnDelete.RectHoverColor = Color.FromArgb(235, 115, 115);
            btnDelete.RectPressColor = Color.FromArgb(184, 64, 64);
            btnDelete.RectSelectedColor = Color.FromArgb(184, 64, 64);
            btnDelete.Size = new Size(96, 38);
            btnDelete.Style = UIStyle.Custom;
            btnDelete.TabIndex = 2;
            btnDelete.Text = "删除";
            btnDelete.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnDelete.Click += btnDelete_Click;
            // 
            // BorrowerManageForm
            // 
            AllowShowTitle = false;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(900, 580);
            Controls.Add(tableLayoutPanel);
            Name = "BorrowerManageForm";
            Padding = new Padding(0);
            ShowTitle = false;
            Text = "借阅人管理";
            ZoomScaleRect = new Rectangle(19, 19, 900, 580);
            tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBorrowers).EndInit();
            flowLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private FlowLayoutPanel flowLayoutPanel;
        private UIDataGridView dgvBorrowers;
        private UITextBox txtKeyword;
        private UIButton btnSearch;
        private UIButton btnAdd;
        private UIButton btnEdit;
        private UIButton btnDelete;
        private UILabel label1;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colIDCard;
        private DataGridViewTextBoxColumn colSex;
        private DataGridViewTextBoxColumn colTel;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colOrderNumber;
    }
}
