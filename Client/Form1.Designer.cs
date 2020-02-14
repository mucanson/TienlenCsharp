namespace Client
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPass = new System.Windows.Forms.Button();
            this.CreateRoom = new System.Windows.Forms.Button();
            this.SearchRoom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReady = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.EndScreen = new System.Windows.Forms.Button();
            this.btnTryAgain = new System.Windows.Forms.Button();
            this.PBCountDown = new System.Windows.Forms.ProgressBar();
            this.CountDownTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.CircleProgress = new Bunifu.UI.WinForms.BunifuCircleProgress();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPlay.BackgroundImage")));
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlay.CausesValidation = false;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnPlay.Location = new System.Drawing.Point(443, 479);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(170, 42);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Đánh";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_click);
            this.btnPlay.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.btnPlay.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // btnPass
            // 
            this.btnPass.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPass.BackgroundImage")));
            this.btnPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPass.CausesValidation = false;
            this.btnPass.FlatAppearance.BorderSize = 0;
            this.btnPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPass.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPass.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnPass.Location = new System.Drawing.Point(629, 479);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(170, 42);
            this.btnPass.TabIndex = 0;
            this.btnPass.Text = "Bỏ lượt";
            this.btnPass.UseVisualStyleBackColor = true;
            this.btnPass.Click += new System.EventHandler(this.button2_Click);
            this.btnPass.MouseEnter += new System.EventHandler(this.button2_MouseEnter);
            this.btnPass.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
            // 
            // CreateRoom
            // 
            this.CreateRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateRoom.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CreateRoom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CreateRoom.BackgroundImage")));
            this.CreateRoom.CausesValidation = false;
            this.CreateRoom.FlatAppearance.BorderSize = 0;
            this.CreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateRoom.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateRoom.Location = new System.Drawing.Point(443, 193);
            this.CreateRoom.Name = "CreateRoom";
            this.CreateRoom.Size = new System.Drawing.Size(170, 69);
            this.CreateRoom.TabIndex = 0;
            this.CreateRoom.TabStop = false;
            this.CreateRoom.Text = "CreateRoom";
            this.CreateRoom.UseVisualStyleBackColor = false;
            this.CreateRoom.Click += new System.EventHandler(this.button3_Click);
            // 
            // SearchRoom
            // 
            this.SearchRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchRoom.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SearchRoom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SearchRoom.BackgroundImage")));
            this.SearchRoom.CausesValidation = false;
            this.SearchRoom.FlatAppearance.BorderSize = 0;
            this.SearchRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchRoom.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchRoom.Location = new System.Drawing.Point(629, 193);
            this.SearchRoom.Name = "SearchRoom";
            this.SearchRoom.Size = new System.Drawing.Size(170, 69);
            this.SearchRoom.TabIndex = 0;
            this.SearchRoom.TabStop = false;
            this.SearchRoom.Text = "SearchRoom";
            this.SearchRoom.UseVisualStyleBackColor = false;
            this.SearchRoom.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(67)))), ((int)(((byte)(19)))));
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 49);
            this.label1.TabIndex = 0;
            // 
            // btnReady
            // 
            this.btnReady.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnReady.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReady.BackgroundImage")));
            this.btnReady.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReady.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReady.FlatAppearance.BorderSize = 0;
            this.btnReady.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReady.Location = new System.Drawing.Point(551, 299);
            this.btnReady.Name = "btnReady";
            this.btnReady.Size = new System.Drawing.Size(114, 108);
            this.btnReady.TabIndex = 1;
            this.btnReady.UseVisualStyleBackColor = false;
            this.btnReady.Click += new System.EventHandler(this.Ready_Click);
            this.btnReady.MouseEnter += new System.EventHandler(this.Ready_MouseEnter);
            this.btnReady.MouseLeave += new System.EventHandler(this.Ready_MouseLeave);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.CausesValidation = false;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(597, 428);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(48, 44);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(374, 449);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 3;
            // 
            // EndScreen
            // 
            this.EndScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EndScreen.FlatAppearance.BorderSize = 0;
            this.EndScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EndScreen.Location = new System.Drawing.Point(311, 182);
            this.EndScreen.Name = "EndScreen";
            this.EndScreen.Size = new System.Drawing.Size(607, 379);
            this.EndScreen.TabIndex = 5;
            this.EndScreen.UseVisualStyleBackColor = true;
            // 
            // btnTryAgain
            // 
            this.btnTryAgain.BackColor = System.Drawing.SystemColors.Control;
            this.btnTryAgain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTryAgain.BackgroundImage")));
            this.btnTryAgain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTryAgain.FlatAppearance.BorderSize = 0;
            this.btnTryAgain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTryAgain.Location = new System.Drawing.Point(497, 586);
            this.btnTryAgain.Name = "btnTryAgain";
            this.btnTryAgain.Size = new System.Drawing.Size(230, 53);
            this.btnTryAgain.TabIndex = 0;
            this.btnTryAgain.UseVisualStyleBackColor = false;
            this.btnTryAgain.Click += new System.EventHandler(this.btnTryAgain_Click);
            this.btnTryAgain.MouseEnter += new System.EventHandler(this.btnTryAgain_MouseEnter);
            this.btnTryAgain.MouseLeave += new System.EventHandler(this.btnTryAgain_MouseLeave);
            // 
            // PBCountDown
            // 
            this.PBCountDown.Location = new System.Drawing.Point(443, 449);
            this.PBCountDown.Name = "PBCountDown";
            this.PBCountDown.Size = new System.Drawing.Size(356, 23);
            this.PBCountDown.TabIndex = 6;
            // 
            // CountDownTimer
            // 
            this.CountDownTimer.Tick += new System.EventHandler(this.CountDownTimer_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1071, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 41);
            this.button1.TabIndex = 7;
            this.button1.Text = "Trờ về";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1071, 59);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 41);
            this.button2.TabIndex = 8;
            this.button2.Text = "Thoát";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // CircleProgress
            // 
            this.CircleProgress.Animated = true;
            this.CircleProgress.AnimationInterval = 10;
            this.CircleProgress.AnimationSpeed = 10;
            this.CircleProgress.BackColor = System.Drawing.Color.Transparent;
            this.CircleProgress.CircleMargin = 10;
            this.CircleProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Bold);
            this.CircleProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(133)))), ((int)(((byte)(67)))));
            this.CircleProgress.IsPercentage = true;
            this.CircleProgress.LineProgressThickness = 8;
            this.CircleProgress.LineThickness = 5;
            this.CircleProgress.Location = new System.Drawing.Point(523, 239);
            this.CircleProgress.Name = "CircleProgress";
            this.CircleProgress.ProgressBackColor = System.Drawing.Color.WhiteSmoke;
            this.CircleProgress.ProgressColor = System.Drawing.Color.Gold;
            this.CircleProgress.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 1F);
            this.CircleProgress.Size = new System.Drawing.Size(184, 184);
            this.CircleProgress.Step = 100;
            this.CircleProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.CircleProgress.SubScriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(133)))), ((int)(((byte)(67)))));
            this.CircleProgress.SubScriptMargin = new System.Windows.Forms.Padding(0);
            this.CircleProgress.SubScriptText = "";
            this.CircleProgress.SuperScriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.CircleProgress.SuperScriptMargin = new System.Windows.Forms.Padding(5, 50, 0, 0);
            this.CircleProgress.SuperScriptText = "%";
            this.CircleProgress.TabIndex = 16;
            this.CircleProgress.Text = "20";
            this.CircleProgress.TextMargin = new System.Windows.Forms.Padding(0);
            this.CircleProgress.Value = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 363);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(594, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1157, 378);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(594, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 20;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 329);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 17);
            this.label7.TabIndex = 21;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(577, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 17);
            this.label8.TabIndex = 22;
            this.label8.Text = "label8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1160, 362);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 17);
            this.label9.TabIndex = 23;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(594, 570);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "label10";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1204, 800);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CircleProgress);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PBCountDown);
            this.Controls.Add(this.btnTryAgain);
            this.Controls.Add(this.EndScreen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReady);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchRoom);
            this.Controls.Add(this.CreateRoom);
            this.Controls.Add(this.btnPass);
            this.Controls.Add(this.btnPlay);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Button CreateRoom;
        private System.Windows.Forms.Button SearchRoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReady;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button EndScreen;
        private System.Windows.Forms.Button btnTryAgain;
        private System.Windows.Forms.ProgressBar PBCountDown;
        private System.Windows.Forms.Timer CountDownTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Bunifu.UI.WinForms.BunifuCircleProgress CircleProgress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}

