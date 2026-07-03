using BookLendingSystem.BLL;
using Sunny.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class BorrowHistoryForm : NoFlickerForm
    {
        private BorrowBLL borrowBLL = new BorrowBLL();
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colBookName;
        private DataGridViewTextBoxColumn colBorrowerName;
        private DataGridViewTextBoxColumn colTel;
        private DataGridViewTextBoxColumn colLoanTime;
        private DataGridViewTextBoxColumn colCutOffTime;
        private DataGridViewTextBoxColumn colReturnTime;
        private DataGridViewTextBoxColumn colStatus;

        public BorrowHistoryForm()
        {
            InitializeComponent();
            EnableDoubleBuffering(tableLayoutPanel);
            EnableDoubleBuffering(dgvHistory);
            LoadHistory();
        }

        private void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        private void LoadHistory()
        {
            try
            {
                dgvHistory.AutoGenerateColumns = false;
                DataTable dt = GetHistoryDataTable();
                dgvHistory.DataSource = dt;
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"加载失败：{ex.Message}");
            }
        }

        private DataTable GetHistoryDataTable()
        {
            DataTable dt = borrowBLL.GetActiveBorrowingDetailsWithInfo();
            dt.Columns.Add("归还时间", typeof(string));
            dt.Columns.Add("状态", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                bool isReturned = row["return_time"] != DBNull.Value;
                row["归还时间"] = isReturned ? Convert.ToDateTime(row["return_time"]).ToString("yyyy-MM-dd") : "未归还";
                row["状态"] = isReturned ? "已归还" : "借阅中";
            }

            return dt;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadHistory();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvHistory.SelectedRows.Count > 0)
            {
                int detailId = (int)dgvHistory.SelectedRows[0].Cells["colId"].Value;
                string status = dgvHistory.SelectedRows[0].Cells["colStatus"].Value?.ToString() ?? string.Empty;

                if (status == "已归还")
                {
                    UIMessageBox.ShowWarning("该书已归还");
                    return;
                }

                if (MessageBox.Show(this, "确认归还此书？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (borrowBLL.ReturnBook(detailId))
                        {
                            UIMessageBox.ShowSuccess("归还成功");
                            LoadHistory();
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
            dgvHistory = new UIDataGridView();
            colId = new DataGridViewTextBoxColumn();
            colBookName = new DataGridViewTextBoxColumn();
            colBorrowerName = new DataGridViewTextBoxColumn();
            colTel = new DataGridViewTextBoxColumn();
            colLoanTime = new DataGridViewTextBoxColumn();
            colCutOffTime = new DataGridViewTextBoxColumn();
            colReturnTime = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            flowLayoutPanel = new FlowLayoutPanel();
            btnRefresh = new UIButton();
            btnReturn = new UIButton();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
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
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel.Controls.Add(label1, 1, 0);
            tableLayoutPanel.Controls.Add(dgvHistory, 1, 1);
            tableLayoutPanel.Controls.Add(flowLayoutPanel, 1, 2);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
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
            label1.Size = new Size(189, 60);
            label1.TabIndex = 0;
            label1.Text = "借阅历史";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            dgvHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvHistory.ColumnHeadersHeight = 32;
            dgvHistory.Columns.AddRange(new DataGridViewColumn[] { colId, colBookName, colBorrowerName, colTel, colLoanTime, colCutOffTime, colReturnTime, colStatus });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvHistory.DefaultCellStyle = dataGridViewCellStyle6;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvHistory.GridColor = Color.FromArgb(80, 160, 255);
            dgvHistory.Location = new Point(43, 63);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowHeadersWidth = 51;
            dgvHistory.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvHistory.SelectedIndex = -1;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(714, 349);
            dgvHistory.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvHistory.TabIndex = 1;
            // 
            // colId
            // 
            colId.DataPropertyName = "id";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(80, 160, 255);
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
            colBorrowerName.Width = 125;
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
            // colReturnTime
            // 
            colReturnTime.DataPropertyName = "归还时间";
            colReturnTime.DefaultCellStyle = dataGridViewCellStyle4;
            colReturnTime.HeaderText = "归还时间";
            colReturnTime.MinimumWidth = 6;
            colReturnTime.Name = "colReturnTime";
            colReturnTime.Width = 120;
            // 
            // colStatus
            // 
            colStatus.DataPropertyName = "状态";
            colStatus.DefaultCellStyle = dataGridViewCellStyle3;
            colStatus.HeaderText = "状态";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.Width = 80;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Controls.Add(btnRefresh);
            flowLayoutPanel.Controls.Add(btnReturn);
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.Location = new Point(43, 418);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Padding = new Padding(10);
            flowLayoutPanel.Size = new Size(714, 59);
            flowLayoutPanel.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
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
            btnReturn.Font = new Font("楷体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
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
            // BorrowHistoryForm
            // 
            AllowShowTitle = false;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 480);
            Controls.Add(tableLayoutPanel);
            Name = "BorrowHistoryForm";
            Padding = new Padding(0);
            ShowTitle = false;
            Text = "借阅历史";
            ZoomScaleRect = new Rectangle(19, 19, 800, 480);
            tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            flowLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutPanel;
        private FlowLayoutPanel flowLayoutPanel;
        private UIDataGridView dgvHistory;
        private UIButton btnRefresh;
        private UIButton btnReturn;
        private UILabel label1;
    }
}
