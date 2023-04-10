namespace Marketing_Assistant
{
     public partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProj = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.paneldetails = new System.Windows.Forms.Panel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.customeMessagePanel = new System.Windows.Forms.Panel();
            this.lblCustomMessage = new System.Windows.Forms.Label();
            this.lblCustomMessageValue = new System.Windows.Forms.Label();
            this.agencyDetailsPanel = new System.Windows.Forms.Panel();
            this.lbphone = new System.Windows.Forms.Label();
            this.lbemail = new System.Windows.Forms.Label();
            this.lbcontact = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbwebsite = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbproject = new System.Windows.Forms.Label();
            this.lbstatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerUpdateCheck11hrs = new System.Windows.Forms.Timer(this.components);
            this.timerSearchKeywords = new System.Windows.Forms.Timer(this.components);
            this.paneldetails.SuspendLayout();
            this.customeMessagePanel.SuspendLayout();
            this.agencyDetailsPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtEmail.Location = new System.Drawing.Point(117, 11);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2, 2, 8, 2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(133, 20);
            this.txtEmail.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(19, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Project Code";
            // 
            // txtProj
            // 
            this.txtProj.Location = new System.Drawing.Point(117, 35);
            this.txtProj.Margin = new System.Windows.Forms.Padding(2, 2, 8, 2);
            this.txtProj.Name = "txtProj";
            this.txtProj.Size = new System.Drawing.Size(133, 20);
            this.txtProj.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(124, 58);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 8, 8);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(1);
            this.button1.Size = new System.Drawing.Size(124, 31);
            this.button1.TabIndex = 4;
            this.button1.Text = "CONNECT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // paneldetails
            // 
            this.paneldetails.AutoSize = true;
            this.paneldetails.BackColor = System.Drawing.Color.WhiteSmoke;
            this.paneldetails.Controls.Add(this.linkLabel3);
            this.paneldetails.Controls.Add(this.lblVersion);
            this.paneldetails.Controls.Add(this.label5);
            this.paneldetails.Controls.Add(this.customeMessagePanel);
            this.paneldetails.Controls.Add(this.agencyDetailsPanel);
            this.paneldetails.Controls.Add(this.lbstatus);
            this.paneldetails.Controls.Add(this.label7);
            this.paneldetails.Controls.Add(this.linkLabel2);
            this.paneldetails.Controls.Add(this.panel3);
            this.paneldetails.Controls.Add(this.panel2);
            this.paneldetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paneldetails.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paneldetails.Location = new System.Drawing.Point(8, 8);
            this.paneldetails.Margin = new System.Windows.Forms.Padding(2);
            this.paneldetails.Name = "paneldetails";
            this.paneldetails.Size = new System.Drawing.Size(499, 483);
            this.paneldetails.TabIndex = 5;
            this.paneldetails.Paint += new System.Windows.Forms.PaintEventHandler(this.paneldetails_Paint);
            // 
            // linkLabel3
            // 
            this.linkLabel3.ActiveLinkColor = System.Drawing.Color.Black;
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel3.DisabledLinkColor = System.Drawing.Color.Black;
            this.linkLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.ForeColor = System.Drawing.SystemColors.Info;
            this.linkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLabel3.LinkColor = System.Drawing.Color.DodgerBlue;
            this.linkLabel3.Location = new System.Drawing.Point(24, 454);
            this.linkLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(139, 16);
            this.linkLabel3.TabIndex = 45;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Check for update Test";
            this.linkLabel3.Visible = false;
            this.linkLabel3.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(424, 454);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(40, 16);
            this.lblVersion.TabIndex = 44;
            this.lblVersion.Text = "1.0.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(356, 454);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 16);
            this.label5.TabIndex = 43;
            this.label5.Text = "Version :";
            // 
            // customeMessagePanel
            // 
            this.customeMessagePanel.AutoSize = true;
            this.customeMessagePanel.BackColor = System.Drawing.Color.White;
            this.customeMessagePanel.Controls.Add(this.lblCustomMessage);
            this.customeMessagePanel.Controls.Add(this.lblCustomMessageValue);
            this.customeMessagePanel.Location = new System.Drawing.Point(0, 261);
            this.customeMessagePanel.Margin = new System.Windows.Forms.Padding(2);
            this.customeMessagePanel.Name = "customeMessagePanel";
            this.customeMessagePanel.Size = new System.Drawing.Size(495, 178);
            this.customeMessagePanel.TabIndex = 42;
            // 
            // lblCustomMessage
            // 
            this.lblCustomMessage.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblCustomMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCustomMessage.ForeColor = System.Drawing.Color.White;
            this.lblCustomMessage.Location = new System.Drawing.Point(2, 2);
            this.lblCustomMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomMessage.Name = "lblCustomMessage";
            this.lblCustomMessage.Size = new System.Drawing.Size(491, 31);
            this.lblCustomMessage.TabIndex = 40;
            this.lblCustomMessage.Text = "Details";
            this.lblCustomMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCustomMessageValue
            // 
            this.lblCustomMessageValue.BackColor = System.Drawing.Color.White;
            this.lblCustomMessageValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomMessageValue.Location = new System.Drawing.Point(13, 53);
            this.lblCustomMessageValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomMessageValue.Name = "lblCustomMessageValue";
            this.lblCustomMessageValue.Size = new System.Drawing.Size(466, 111);
            this.lblCustomMessageValue.TabIndex = 41;
            this.lblCustomMessageValue.Text = "This is custom message";
            // 
            // agencyDetailsPanel
            // 
            this.agencyDetailsPanel.AutoSize = true;
            this.agencyDetailsPanel.BackColor = System.Drawing.Color.White;
            this.agencyDetailsPanel.Controls.Add(this.lbphone);
            this.agencyDetailsPanel.Controls.Add(this.lbemail);
            this.agencyDetailsPanel.Controls.Add(this.lbcontact);
            this.agencyDetailsPanel.Controls.Add(this.label11);
            this.agencyDetailsPanel.Controls.Add(this.label3);
            this.agencyDetailsPanel.Controls.Add(this.lbwebsite);
            this.agencyDetailsPanel.Controls.Add(this.label10);
            this.agencyDetailsPanel.Controls.Add(this.label8);
            this.agencyDetailsPanel.Controls.Add(this.label6);
            this.agencyDetailsPanel.Controls.Add(this.label4);
            this.agencyDetailsPanel.Controls.Add(this.lbproject);
            this.agencyDetailsPanel.Location = new System.Drawing.Point(0, 261);
            this.agencyDetailsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.agencyDetailsPanel.Name = "agencyDetailsPanel";
            this.agencyDetailsPanel.Size = new System.Drawing.Size(496, 181);
            this.agencyDetailsPanel.TabIndex = 38;
            // 
            // lbphone
            // 
            this.lbphone.Location = new System.Drawing.Point(236, 96);
            this.lbphone.Margin = new System.Windows.Forms.Padding(0);
            this.lbphone.Name = "lbphone";
            this.lbphone.Size = new System.Drawing.Size(244, 19);
            this.lbphone.TabIndex = 32;
            this.lbphone.Text = "(888) 888-8888";
            this.lbphone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbphone.Click += new System.EventHandler(this.lbphone_Click);
            // 
            // lbemail
            // 
            this.lbemail.Location = new System.Drawing.Point(236, 122);
            this.lbemail.Margin = new System.Windows.Forms.Padding(0);
            this.lbemail.Name = "lbemail";
            this.lbemail.Size = new System.Drawing.Size(244, 19);
            this.lbemail.TabIndex = 34;
            this.lbemail.Text = "shivam12@gmail.com";
            this.lbemail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbcontact
            // 
            this.lbcontact.Location = new System.Drawing.Point(236, 69);
            this.lbcontact.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbcontact.Name = "lbcontact";
            this.lbcontact.Size = new System.Drawing.Size(244, 19);
            this.lbcontact.TabIndex = 30;
            this.lbcontact.Text = "Shivam";
            this.lbcontact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbcontact.Click += new System.EventHandler(this.lbcontact_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.DodgerBlue;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(1, -2);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(493, 31);
            this.label11.TabIndex = 39;
            this.label11.Text = "Agency Details";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 19);
            this.label3.TabIndex = 27;
            this.label3.Text = "Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbwebsite
            // 
            this.lbwebsite.Location = new System.Drawing.Point(236, 149);
            this.lbwebsite.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbwebsite.Name = "lbwebsite";
            this.lbwebsite.Size = new System.Drawing.Size(244, 19);
            this.lbwebsite.TabIndex = 36;
            this.lbwebsite.Text = "www.yoursite.com";
            this.lbwebsite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(18, 122);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 19);
            this.label10.TabIndex = 35;
            this.label10.Text = "Email";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(18, 96);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 19);
            this.label8.TabIndex = 33;
            this.label8.Text = "Phone Number";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(18, 149);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 19);
            this.label6.TabIndex = 31;
            this.label6.Text = "Website";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 69);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 19);
            this.label4.TabIndex = 29;
            this.label4.Text = "Contact Name";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbproject
            // 
            this.lbproject.Location = new System.Drawing.Point(236, 42);
            this.lbproject.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbproject.Name = "lbproject";
            this.lbproject.Size = new System.Drawing.Size(244, 19);
            this.lbproject.TabIndex = 28;
            this.lbproject.Text = "test";
            this.lbproject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbstatus
            // 
            this.lbstatus.AutoSize = true;
            this.lbstatus.ForeColor = System.Drawing.Color.DarkRed;
            this.lbstatus.Location = new System.Drawing.Point(17, 89);
            this.lbstatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbstatus.Name = "lbstatus";
            this.lbstatus.Size = new System.Drawing.Size(45, 16);
            this.lbstatus.TabIndex = 38;
            this.lbstatus.Text = "Status";
            this.lbstatus.Visible = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.DodgerBlue;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 53);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(496, 31);
            this.label7.TabIndex = 40;
            this.label7.Text = "Current Google Ranking";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel2
            // 
            this.linkLabel2.ActiveLinkColor = System.Drawing.Color.Black;
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel2.DisabledLinkColor = System.Drawing.Color.Black;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.ForeColor = System.Drawing.SystemColors.Info;
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLabel2.LinkColor = System.Drawing.Color.DodgerBlue;
            this.linkLabel2.Location = new System.Drawing.Point(191, 34);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(109, 16);
            this.linkLabel2.TabIndex = 27;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Check for update";
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Location = new System.Drawing.Point(2, 81);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(495, 177);
            this.panel3.TabIndex = 40;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.GhostWhite;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(495, 177);
            this.dataGridView1.TabIndex = 28;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(496, 31);
            this.panel2.TabIndex = 6;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(9, 4);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 24);
            this.label12.TabIndex = 24;
            this.label12.Text = "Welcome";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Black;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.DisabledLinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.ForeColor = System.Drawing.SystemColors.Info;
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(388, 8);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(75, 16);
            this.linkLabel1.TabIndex = 27;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Disconnect";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Device Name   ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Click To Open Marketing Assistant";
            this.notifyIcon.BalloonTipTitle = "Marketing Assistant";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Click To Open Marketing Assistant";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // timerUpdateCheck11hrs
            // 
            this.timerUpdateCheck11hrs.Enabled = true;
            this.timerUpdateCheck11hrs.Interval = 39600000;
            this.timerUpdateCheck11hrs.Tick += new System.EventHandler(this.timerUpdateCheck11hrs_Tick);
            // 
            // timerSearchKeywords
            // 
            this.timerSearchKeywords.Enabled = true;
            this.timerSearchKeywords.Interval = 180000;
            this.timerSearchKeywords.Tick += new System.EventHandler(this.timerSearchKeywords_Tick);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(512, 499);
            this.Controls.Add(this.paneldetails);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtProj);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Marketing Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.Load += new System.EventHandler(this.Login_Load);
            this.Resize += new System.EventHandler(this.Login_Resize);
            this.paneldetails.ResumeLayout(false);
            this.paneldetails.PerformLayout();
            this.customeMessagePanel.ResumeLayout(false);
            this.agencyDetailsPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProj;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel paneldetails;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer fetchQueueTimer;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel agencyDetailsPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbwebsite;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbemail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbphone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbcontact;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbproject;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbstatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer timerUpdateCheck11hrs;
        private System.Windows.Forms.Timer timerSearchKeywords;
        private System.Windows.Forms.Label lblCustomMessageValue;
        private System.Windows.Forms.Label lblCustomMessage;
        private System.Windows.Forms.Panel customeMessagePanel;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel3;
    }
}

