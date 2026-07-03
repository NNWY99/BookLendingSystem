using BookLendingSystem.Model;
using Sunny.UI;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BookLendingSystem.Views
{
    public partial class BookDetailDialog : UIForm
    {
        public BookDetailDialog(Books book)
        {
            InitializeComponent();
            LoadBookDetail(book);
        }

        private void LoadBookDetail(Books book)
        {
            lblBookName.Text = book.BookName;
            lblAuthor.Text = "作者：" + book.Author;
            lblCategory.Text = "类别：" + book.Category;
            lblPublisher.Text = "出版社：" + book.PublishingHouse;
            lblDate.Text = "出版日期：" + book.PublicationDate.ToString("yyyy-MM-dd");
            lblBarcode.Text = "条码号：" + book.BarCode.ToString();
            lblStock.Text = "库存：" + book.LoansNumber.ToString();
            lblTotal.Text = "总数：" + book.TotalNumber.ToString();
            txtDescription.Text = book.Description;

            if (!string.IsNullOrEmpty(book.ImagePath) && File.Exists(book.ImagePath))
            {
                picCover.Image = Image.FromFile(book.ImagePath);
            }
            else
            {
                picCover.Image = null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitializeComponent()
        {
            lblBookName = new UILabel();
            picCover = new PictureBox();
            lblAuthor = new UILabel();
            lblCategory = new UILabel();
            lblPublisher = new UILabel();
            lblDate = new UILabel();
            lblBarcode = new UILabel();
            lblStock = new UILabel();
            lblTotal = new UILabel();
            txtDescription = new RichTextBox();
            btnExit = new UIButton();
            ((System.ComponentModel.ISupportInitialize)picCover).BeginInit();
            SuspendLayout();
            // 
            // lblBookName
            // 
            lblBookName.BackColor = Color.Transparent;
            lblBookName.Font = new Font("隶书", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblBookName.ForeColor = Color.White;
            lblBookName.Location = new Point(0, 0);
            lblBookName.Name = "lblBookName";
            lblBookName.Size = new Size(750, 60);
            lblBookName.TabIndex = 0;
            lblBookName.Text = "书名";
            lblBookName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picCover
            // 
            picCover.BackColor = Color.Transparent;
            picCover.BorderStyle = BorderStyle.FixedSingle;
            picCover.Location = new Point(20, 80);
            picCover.Name = "picCover";
            picCover.Size = new Size(220, 220);
            picCover.SizeMode = PictureBoxSizeMode.StretchImage;
            picCover.TabIndex = 1;
            picCover.TabStop = false;
            // 
            // lblAuthor
            // 
            lblAuthor.BackColor = Color.Transparent;
            lblAuthor.Font = new Font("楷体", 15F, FontStyle.Bold);
            lblAuthor.ForeColor = Color.White;
            lblAuthor.Location = new Point(260, 80);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(470, 30);
            lblAuthor.TabIndex = 2;
            lblAuthor.Text = "作者：";
            // 
            // lblCategory
            // 
            lblCategory.BackColor = Color.Transparent;
            lblCategory.Font = new Font("楷体", 15F, FontStyle.Bold);
            lblCategory.ForeColor = Color.White;
            lblCategory.Location = new Point(260, 120);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(470, 30);
            lblCategory.TabIndex = 3;
            lblCategory.Text = "类别：";
            // 
            // lblPublisher
            // 
            lblPublisher.BackColor = Color.Transparent;
            lblPublisher.Font = new Font("楷体", 15F, FontStyle.Bold);
            lblPublisher.ForeColor = Color.White;
            lblPublisher.Location = new Point(260, 160);
            lblPublisher.Name = "lblPublisher";
            lblPublisher.Size = new Size(470, 30);
            lblPublisher.TabIndex = 4;
            lblPublisher.Text = "出版社：";
            // 
            // lblDate
            // 
            lblDate.BackColor = Color.Transparent;
            lblDate.Font = new Font("楷体", 15F, FontStyle.Bold);
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(260, 200);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(470, 30);
            lblDate.TabIndex = 5;
            lblDate.Text = "出版日期：";
            // 
            // lblBarcode
            // 
            lblBarcode.BackColor = Color.Transparent;
            lblBarcode.Font = new Font("楷体", 15F, FontStyle.Bold);
            lblBarcode.ForeColor = Color.White;
            lblBarcode.Location = new Point(260, 240);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(470, 30);
            lblBarcode.TabIndex = 6;
            lblBarcode.Text = "条码号：";
            // 
            // lblStock
            // 
            lblStock.BackColor = Color.Transparent;
            lblStock.Font = new Font("楷体", 15F, FontStyle.Bold);
            lblStock.ForeColor = Color.White;
            lblStock.Location = new Point(260, 280);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(220, 30);
            lblStock.TabIndex = 7;
            lblStock.Text = "库存：";
            // 
            // lblTotal
            // 
            lblTotal.BackColor = Color.Transparent;
            lblTotal.Font = new Font("楷体", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblTotal.ForeColor = Color.White;
            lblTotal.Location = new Point(510, 280);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(220, 30);
            lblTotal.TabIndex = 8;
            lblTotal.Text = "总数：";
            // 
            // txtDescription
            // 
            txtDescription.BackColor = Color.White;
            txtDescription.Font = new Font("微软雅黑", 12F);
            txtDescription.Location = new Point(20, 313);
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtDescription.Size = new Size(710, 147);
            txtDescription.TabIndex = 9;
            txtDescription.Text = "";
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Transparent;
            btnExit.FillColor = Color.Transparent;
            btnExit.Font = new Font("楷体", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnExit.Location = new Point(305, 475);
            btnExit.MinimumSize = new Size(1, 1);
            btnExit.Name = "btnExit";
            btnExit.Radius = 25;
            btnExit.RectColor = Color.White;
            btnExit.Size = new Size(140, 45);
            btnExit.TabIndex = 10;
            btnExit.Text = "退出";
            btnExit.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnExit.Click += btnExit_Click;
            // 
            // BookDetailDialog
            // 
            AllowShowTitle = false;
            BackgroundImage = Properties.Resources.backgd1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(750, 530);
            Controls.Add(lblBookName);
            Controls.Add(picCover);
            Controls.Add(lblAuthor);
            Controls.Add(lblCategory);
            Controls.Add(lblPublisher);
            Controls.Add(lblDate);
            Controls.Add(lblBarcode);
            Controls.Add(lblStock);
            Controls.Add(lblTotal);
            Controls.Add(txtDescription);
            Controls.Add(btnExit);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BookDetailDialog";
            Padding = new Padding(0);
            RectColor = Color.Transparent;
            ShowTitle = false;
            StartPosition = FormStartPosition.CenterParent;
            ZoomScaleRect = new Rectangle(19, 19, 750, 530);
            ((System.ComponentModel.ISupportInitialize)picCover).EndInit();
            ResumeLayout(false);
        }

        private UILabel lblBookName;
        private PictureBox picCover;
        private UILabel lblAuthor;
        private UILabel lblCategory;
        private UILabel lblPublisher;
        private UILabel lblDate;
        private UILabel lblBarcode;
        private UILabel lblStock;
        private UILabel lblTotal;
        private RichTextBox txtDescription;
        private UIButton btnExit;
    }
}