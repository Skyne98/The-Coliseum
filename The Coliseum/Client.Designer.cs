namespace The_Coliseum
{
    partial class Client
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.turnProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.readyBut = new System.Windows.Forms.Button();
            this.playersBox = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.locationLab = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.playerLab = new System.Windows.Forms.Label();
            this.characterLab = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.actionsView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.sendBut = new System.Windows.Forms.Button();
            this.messageTypeBox = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.turnProgress});
            this.statusStrip.Location = new System.Drawing.Point(0, 705);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1321, 26);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(49, 21);
            this.statusLabel.Text = "Status";
            // 
            // turnProgress
            // 
            this.turnProgress.Name = "turnProgress";
            this.turnProgress.Size = new System.Drawing.Size(267, 20);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1321, 706);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(380, 700);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.readyBut);
            this.tabPage1.Controls.Add(this.playersBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(372, 671);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Game";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // readyBut
            // 
            this.readyBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.readyBut.Location = new System.Drawing.Point(0, 634);
            this.readyBut.Name = "readyBut";
            this.readyBut.Size = new System.Drawing.Size(372, 36);
            this.readyBut.TabIndex = 1;
            this.readyBut.Text = "Not Ready";
            this.readyBut.UseVisualStyleBackColor = true;
            this.readyBut.Click += new System.EventHandler(this.readyBut_Click);
            // 
            // playersBox
            // 
            this.playersBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.playersBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playersBox.FormattingEnabled = true;
            this.playersBox.ItemHeight = 16;
            this.playersBox.Location = new System.Drawing.Point(0, 0);
            this.playersBox.Name = "playersBox";
            this.playersBox.Size = new System.Drawing.Size(372, 624);
            this.playersBox.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.locationLab);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(372, 671);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Character";
            // 
            // locationLab
            // 
            this.locationLab.AutoSize = true;
            this.locationLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.locationLab.Location = new System.Drawing.Point(6, 86);
            this.locationLab.Name = "locationLab";
            this.locationLab.Size = new System.Drawing.Size(73, 20);
            this.locationLab.TabIndex = 3;
            this.locationLab.Text = "Location";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.playerLab);
            this.panel1.Controls.Add(this.characterLab);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 83);
            this.panel1.TabIndex = 2;
            // 
            // playerLab
            // 
            this.playerLab.AutoSize = true;
            this.playerLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerLab.Location = new System.Drawing.Point(3, 14);
            this.playerLab.Name = "playerLab";
            this.playerLab.Size = new System.Drawing.Size(156, 29);
            this.playerLab.TabIndex = 0;
            this.playerLab.Text = "PlayerName";
            // 
            // characterLab
            // 
            this.characterLab.AutoSize = true;
            this.characterLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.characterLab.Location = new System.Drawing.Point(3, 43);
            this.characterLab.Name = "characterLab";
            this.characterLab.Size = new System.Drawing.Size(150, 25);
            this.characterLab.TabIndex = 1;
            this.characterLab.Text = "CharacterName";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.actionsView);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(372, 671);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Actions";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // actionsView
            // 
            this.actionsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.actionsView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.actionsView.Location = new System.Drawing.Point(0, 0);
            this.actionsView.Name = "actionsView";
            this.actionsView.Size = new System.Drawing.Size(372, 678);
            this.actionsView.TabIndex = 0;
            this.actionsView.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.actionsView_NodeMouseHover);
            this.actionsView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.actionsView_NodeMouseDoubleClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.logBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(389, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(929, 700);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.BackColor = System.Drawing.SystemColors.Control;
            this.logBox.Location = new System.Drawing.Point(3, 3);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(923, 659);
            this.logBox.TabIndex = 1;
            this.logBox.Text = "";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 172F));
            this.tableLayoutPanel3.Controls.Add(this.messageBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.sendBut, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.messageTypeBox, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 668);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(923, 29);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.Location = new System.Drawing.Point(3, 3);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(567, 22);
            this.messageBox.TabIndex = 0;
            // 
            // sendBut
            // 
            this.sendBut.Location = new System.Drawing.Point(754, 3);
            this.sendBut.Name = "sendBut";
            this.sendBut.Size = new System.Drawing.Size(163, 23);
            this.sendBut.TabIndex = 2;
            this.sendBut.Text = "Send";
            this.sendBut.UseVisualStyleBackColor = true;
            // 
            // messageTypeBox
            // 
            this.messageTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.messageTypeBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.messageTypeBox.FormattingEnabled = true;
            this.messageTypeBox.Items.AddRange(new object[] {
            "Global",
            "Local"});
            this.messageTypeBox.Location = new System.Drawing.Point(576, 3);
            this.messageTypeBox.Name = "messageTypeBox";
            this.messageTypeBox.Size = new System.Drawing.Size(172, 24);
            this.messageTypeBox.TabIndex = 1;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 731);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Client";
            this.ShowIcon = false;
            this.Text = "The Coliseum";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.ComboBox messageTypeBox;
        private System.Windows.Forms.Button sendBut;
        private System.Windows.Forms.ListBox playersBox;
        public System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel statusLabel;
        public System.Windows.Forms.ToolStripProgressBar turnProgress;
        private System.Windows.Forms.Button readyBut;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label playerLab;
        private System.Windows.Forms.Label characterLab;
        private System.Windows.Forms.Label locationLab;
        public System.Windows.Forms.TreeView actionsView;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

