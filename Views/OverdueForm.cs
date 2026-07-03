using BookLendingSystem.BLL;
using Sunny.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class OverdueForm : UIForm
    {
        private BorrowBLL borrowBLL = new BorrowBLL();
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colBookName;
        private DataGridViewTextBoxColumn colBorrowerName;
        private DataGridViewTextBoxColumn colTel;
        private DataGridViewTextBoxColumn colLoanTime;
        private DataGridViewTextBoxColumn colCutOffTime;
        private DataGridViewTextBoxColumn colOverdueDays;

        public OverdueForm()
        {
            InitializeComponent();
            LoadOverdue();
        }

        private void LoadOverdue()
        {
            try
            {
                dgvOverdue.AutoGenerateColumns = false;
                DataTable dt = GetOverdueDataTable();
                dgvOverdue.DataSource = dt;
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"加载失败：{ex.Message}");
            }
        }

        private DataTable GetOverdueDataTable()
        {
            DataTable dt = borrowBLL.GetOverdueDetailsWithInfo();
            dt.Columns.Add("逾期天数", typeof(int));

            foreach (DataRow row in dt.Rows)
            {
                DateTime cutOffTime = Convert.ToDateTime(row["cut_off_time"]);
                int days = (DateTime.Now - cutOffTime).Days;
                row["逾期天数"] = days;
            }

            return dt;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOverdue();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvOverdue.SelectedRows.Count > 0)
            {
                int detailId = (int)dgvOverdue.SelectedRows[0].Cells["colId"].Value;

                if (MessageBox.Show(this, "确认归还此书？（逾期图书）", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (borrowBLL.ReturnBook(detailId))
                        {
                            UIMessageBox.ShowSuccess("归还成功");
                            LoadOverdue();
                        }
                        else
                        {
                            UIMessageBox.ShowError("归还失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        UIMessageBox.ShowError($"归还失败：{ex.Message}");
                    }
                }
            }
            else
            {
                UIMessageBox.ShowWarning("请选择要归还的记录");
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            tableLayoutPanel = new TableLayoutPanel();
            label1 = new UILabel();
            dgvOverdue = new UIDataGridView();
            colId = new DataGridViewTextBoxColumn();
            colBookName = new DataGridViewTextBoxColumn();
            colBorrowerName = new DataGridViewTextBoxColumn();
            colTel = new DataGridViewTextBoxColumn();
            colLoanTime = new DataGridViewTextBoxColumn();
            colCutOffTime = new DataGridViewTextBoxColumn();
            colOverdueDays = new DataGridViewTextBoxColumn();
            flowLayoutPanel = new FlowLayoutPanel();
            btnRefresh = new UIButton();
            btnReturn = new UIButton();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOverdue).BeginInit();
            flowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel.Controls.Add(label1, 1, 0);
            tableLayoutPanel.Controls.Add(dgvOverdue, 1, 1);
            tableLayoutPanel.Controls.Add(flowLayoutPanel, 1, 2);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tableLayoutPanel.Size = new Size(900, 580);
            tableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.Font = new Font("隶书", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.FromArgb(220, 53, 69);
            label1.Location = new Point(48, 0);
            label1.Name = "label1";
            label1.Size = new Size(187, 60);
            label1.TabIndex = 0;
            label1.Text = "逾期管理";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvOverdue
            // 
            dgvOverdue.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 243, 243);
            dgvOverdue.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvOverdue.BackgroundColor = Color.White;
            dgvOverdue.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(220, 53, 69);
            dataGridViewCellStyle2.Font = new Font("楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvOverdue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvOverdue.ColumnHeadersHeight = 32;
            dgvOverdue.Columns.AddRange(new DataGridViewColumn[] { colId, colBookName, colBorrowerName, colTel, colLoanTime, colCutOffTime, colOverdueDays });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvOverdue.DefaultCellStyle = dataGridViewCellStyle6;
            dgvOverdue.Dock = DockStyle.Fill;
            dgvOverdue.EnableHeadersVisualStyles = false;
            dgvOverdue.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvOverdue.GridColor = Color.FromArgb(220, 53, 69);
            dgvOverdue.Location = new Point(48, 63);
            dgvOverdue.Name = "dgvOverdue";
            dgvOverdue.RectColor = Color.White;
            dgvOverdue.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvOverdue.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvOverdue.RowHeadersVisible = false;
            dgvOverdue.RowHeadersWidth = 51;
            dgvOverdue.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvOverdue.SelectedIndex = -1;
            dgvOverdue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOverdue.Size = new Size(804, 449);
            dgvOverdue.StripeOddColor = Color.FromArgb(255, 243, 243);
            dgvOverdue.TabIndex = 1;
            // 
            // colId
            // 
            colId.DataPropertyName = "id";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(255, 243, 243);
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 53, 69);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            colId.DefaultCellStyle = dataGridViewCellStyle3;
            colId.HeaderText = "ID";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.Width = 50;
            // 
            // colBookName
            // 
            colBookName.DataPropertyName = "bookName";
            colBookName.HeaderText = "书名";
            colBookName.MinimumWidth = 6;
            colBookName.Name = "colBookName";
            colBookName.Width = 150;
            // 
            // colBorrowerName
            // 
            colBorrowerName.DataPropertyName = "borrowers_name";
            colBorrowerName.HeaderText = "借阅人";
            colBorrowerName.MinimumWidth = 6;
            colBorrowerName.Name = "colBorrowerName";
            // 
            // colTel
            // 
            colTel.DataPropertyName = "tel";
            colTel.HeaderText = "电话";
            colTel.MinimumWidth = 6;
            colTel.Name = "colTel";
            colTel.Width = 120;
            // 
            // colLoanTime
            // 
            colLoanTime.DataPropertyName = "loanTime";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            colLoanTime.DefaultCellStyle = dataGridViewCellStyle4;
            colLoanTime.HeaderText = "借阅时间";
            colLoanTime.MinimumWidth = 6;
            colLoanTime.Name = "colLoanTime";
            colLoanTime.Width = 120;
            // 
            // colCutOffTime
            // 
            colCutOffTime.DataPropertyName = "cut_off_time";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCutOffTime.DefaultCellStyle = dataGridViewCellStyle5;
            colCutOffTime.HeaderText = "截止时间";
            colCutOffTime.MinimumWidth = 6;
            colCutOffTime.Name = "colCutOffTime";
            colCutOffTime.Width = 120;
            // 
            // colOverdueDays
            // 
            colOverdueDays.DataPropertyName = "逾期天数";
            colOverdueDays.DefaultCellStyle = dataGridViewCellStyle3;
            colOverdueDays.HeaderText = "逾期天数";
            colOverdueDays.MinimumWidth = 6;
            colOverdueDays.Name = "colOverdueDays";
            colOverdueDays.Width = 80;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Controls.Add(btnRefresh);
            flowLayoutPanel.Controls.Add(btnReturn);
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.Location = new Point(48, 518);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Padding = new Padding(10);
            flowLayoutPanel.Size = new Size(804, 59);
            flowLayoutPanel.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.Font = new Font("微软雅黑", 11F);
            btnRefresh.Location = new Point(15, 10);
            btnRefresh.Margin = new Padding(5, 0, 5, 0);
            btnRefresh.MinimumSize = new Size(1, 1);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 38);
            btnRefresh.Style = UIStyle.Custom;
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "刷新";
            btnRefresh.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnReturn
            // 
            btnReturn.FillColor = Color.FromArgb(110, 190, 40);
            btnReturn.FillColor2 = Color.FromArgb(110, 190, 40);
            btnReturn.FillHoverColor = Color.FromArgb(139, 203, 83);
            btnReturn.FillPressColor = Color.FromArgb(88, 152, 32);
            btnReturn.FillSelectedColor = Color.FromArgb(88, 152, 32);
            btnReturn.Font = new Font("微软雅黑", 11F);
            btnReturn.LightColor = Color.FromArgb(245, 251, 241);
            btnReturn.Location = new Point(125, 10);
            btnReturn.Margin = new Padding(5, 0, 5, 0);
            btnReturn.MinimumSize = new Size(1, 1);
            btnReturn.Name = "btnReturn";
            btnReturn.RectColor = Color.FromArgb(110, 190, 40);
            btnReturn.RectHoverColor = Color.FromArgb(139, 203, 83);
            btnReturn.RectPressColor = Color.FromArgb(88, 152, 32);
            btnReturn.RectSelectedColor = Color.FromArgb(88, 152, 32);
            btnReturn.Size = new Size(110, 38);
            btnReturn.Style = UIStyle.Custom;
            btnReturn.TabIndex = 1;
            btnReturn.Text = "归还图书";
            btnReturn.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnReturn.Click += btnReturn_Click;
            // 
            // OverdueForm
            // 
            AllowShowTitle = false;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Transparent;
            BackgroundImage = Properties.Resources.backgd1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(900, 580);
            Controls.Add(tableLayoutPanel);
            Name = "OverdueForm";
            Padding = new Padding(0);
            RectColor = Color.Transparent;
            ShowTitle = false;
            Text = "逾期管理";
            ZoomScaleRect = new Rectangle(19, 19, 900, 580);
            tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOverdue).EndInit();
            flowLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private FlowLayoutPanel flowLayoutPanel;
        private UIDataGridView dgvOverdue;
        private UIButton btnRefresh;
        private UIButton btnReturn;
        private UILabel label1;
    }
}
