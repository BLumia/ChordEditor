namespace chordgen
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TimelinePictureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.logText = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ChordLabel12 = new System.Windows.Forms.Label();
            this.ChordLabel11 = new System.Windows.Forms.Label();
            this.ChordLabel9 = new System.Windows.Forms.Label();
            this.ChordLabel10 = new System.Windows.Forms.Label();
            this.ChordLabel8 = new System.Windows.Forms.Label();
            this.ChordLabel7 = new System.Windows.Forms.Label();
            this.ChordLabel6 = new System.Windows.Forms.Label();
            this.ChordLabel5 = new System.Windows.Forms.Label();
            this.ChordLabel4 = new System.Windows.Forms.Label();
            this.ChordLabel3 = new System.Windows.Forms.Label();
            this.ChordLabel2 = new System.Windows.Forms.Label();
            this.ChordLabel1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TimelinePictureBox)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimelinePictureBox
            // 
            this.TimelinePictureBox.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.TimelinePictureBox.Location = new System.Drawing.Point(13, 13);
            this.TimelinePictureBox.Name = "TimelinePictureBox";
            this.TimelinePictureBox.Size = new System.Drawing.Size(600, 60);
            this.TimelinePictureBox.TabIndex = 0;
            this.TimelinePictureBox.TabStop = false;
            this.TimelinePictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TimelinePictureBox_MouseDown);
            this.TimelinePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TimelinePictureBox_MouseMove);
            this.TimelinePictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TimelinePictureBox_MouseUp);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "任务文件|*.myinfo";
            this.openFileDialog1.InitialDirectory = "C:\\Users\\jjy\\AppData\\Local\\osu!\\Songs";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // logText
            // 
            this.logText.Location = new System.Drawing.Point(13, 254);
            this.logText.Multiline = true;
            this.logText.Name = "logText";
            this.logText.Size = new System.Drawing.Size(600, 100);
            this.logText.TabIndex = 5;
            this.logText.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(397, 96);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(225, 119);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Editor";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Location = new System.Drawing.Point(6, 92);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(133, 14);
            this.label16.TabIndex = 17;
            this.label16.Text = "Ctrl+V: Paste Section";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Location = new System.Drawing.Point(6, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(127, 14);
            this.label15.TabIndex = 16;
            this.label15.Text = "Ctrl+C: Copy Section";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Location = new System.Drawing.Point(108, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 14);
            this.label14.TabIndex = 15;
            this.label14.Text = "W: Sections=4";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Location = new System.Drawing.Point(109, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 14);
            this.label13.TabIndex = 14;
            this.label13.Text = "M: Record Sel";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(6, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 14);
            this.label12.TabIndex = 13;
            this.label12.Text = "/: Clear Sel";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(109, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 14);
            this.label9.TabIndex = 12;
            this.label9.Text = ">: End Sel";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(6, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 14);
            this.label10.TabIndex = 11;
            this.label10.Text = "<: Start Sel";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(6, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 14);
            this.label11.TabIndex = 10;
            this.label11.Text = "Q: Bars=4";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(242, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(68, 69);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rhythm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "Offset=0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "Modify";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "SPB=4";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(242, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 106);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modes";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(6, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 14);
            this.label8.TabIndex = 12;
            this.label8.Text = "O: Origin Chord Off";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "P: Switch Chroma Mode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "E: Edit Mode Off";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ChordLabel12);
            this.groupBox1.Controls.Add(this.ChordLabel11);
            this.groupBox1.Controls.Add(this.ChordLabel9);
            this.groupBox1.Controls.Add(this.ChordLabel10);
            this.groupBox1.Controls.Add(this.ChordLabel8);
            this.groupBox1.Controls.Add(this.ChordLabel7);
            this.groupBox1.Controls.Add(this.ChordLabel6);
            this.groupBox1.Controls.Add(this.ChordLabel5);
            this.groupBox1.Controls.Add(this.ChordLabel4);
            this.groupBox1.Controls.Add(this.ChordLabel3);
            this.groupBox1.Controls.Add(this.ChordLabel2);
            this.groupBox1.Controls.Add(this.ChordLabel1);
            this.groupBox1.Location = new System.Drawing.Point(14, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 126);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chords";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Location = new System.Drawing.Point(6, 105);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(133, 14);
            this.label17.TabIndex = 21;
            this.label17.Text = "`: Show Key Shortcuts";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Location = new System.Drawing.Point(6, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(187, 14);
            this.label7.TabIndex = 20;
            this.label7.Text = "Ctrl: Absolute/Relative Switch";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(6, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 14);
            this.label6.TabIndex = 19;
            this.label6.Text = "Shift: Maj/Min Switch";
            // 
            // ChordLabel12
            // 
            this.ChordLabel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel12.Location = new System.Drawing.Point(185, 35);
            this.ChordLabel12.Name = "ChordLabel12";
            this.ChordLabel12.Size = new System.Drawing.Size(30, 20);
            this.ChordLabel12.TabIndex = 18;
            this.ChordLabel12.Text = "vii";
            this.ChordLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel11
            // 
            this.ChordLabel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel11.Location = new System.Drawing.Point(170, 15);
            this.ChordLabel11.Name = "ChordLabel11";
            this.ChordLabel11.Size = new System.Drawing.Size(31, 20);
            this.ChordLabel11.TabIndex = 17;
            this.ChordLabel11.Text = "VIIb";
            this.ChordLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel9
            // 
            this.ChordLabel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel9.Location = new System.Drawing.Point(140, 15);
            this.ChordLabel9.Name = "ChordLabel9";
            this.ChordLabel9.Size = new System.Drawing.Size(30, 20);
            this.ChordLabel9.TabIndex = 16;
            this.ChordLabel9.Text = "v#";
            this.ChordLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel10
            // 
            this.ChordLabel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel10.Location = new System.Drawing.Point(155, 35);
            this.ChordLabel10.Name = "ChordLabel10";
            this.ChordLabel10.Size = new System.Drawing.Size(30, 20);
            this.ChordLabel10.TabIndex = 15;
            this.ChordLabel10.Text = "vi";
            this.ChordLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel8
            // 
            this.ChordLabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel8.Location = new System.Drawing.Point(125, 35);
            this.ChordLabel8.Name = "ChordLabel8";
            this.ChordLabel8.Size = new System.Drawing.Size(30, 20);
            this.ChordLabel8.TabIndex = 14;
            this.ChordLabel8.Text = "V";
            this.ChordLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel7
            // 
            this.ChordLabel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel7.Location = new System.Drawing.Point(110, 15);
            this.ChordLabel7.Name = "ChordLabel7";
            this.ChordLabel7.Size = new System.Drawing.Size(30, 20);
            this.ChordLabel7.TabIndex = 13;
            this.ChordLabel7.Text = "iv#";
            this.ChordLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel6
            // 
            this.ChordLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel6.Location = new System.Drawing.Point(95, 35);
            this.ChordLabel6.Name = "ChordLabel6";
            this.ChordLabel6.Size = new System.Drawing.Size(30, 20);
            this.ChordLabel6.TabIndex = 12;
            this.ChordLabel6.Text = "IV";
            this.ChordLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel5
            // 
            this.ChordLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel5.Location = new System.Drawing.Point(65, 35);
            this.ChordLabel5.Name = "ChordLabel5";
            this.ChordLabel5.Size = new System.Drawing.Size(30, 20);
            this.ChordLabel5.TabIndex = 11;
            this.ChordLabel5.Text = "iii";
            this.ChordLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel4
            // 
            this.ChordLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel4.Location = new System.Drawing.Point(50, 15);
            this.ChordLabel4.Name = "ChordLabel4";
            this.ChordLabel4.Size = new System.Drawing.Size(31, 20);
            this.ChordLabel4.TabIndex = 10;
            this.ChordLabel4.Text = "IIIb";
            this.ChordLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel3
            // 
            this.ChordLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel3.Location = new System.Drawing.Point(35, 35);
            this.ChordLabel3.Name = "ChordLabel3";
            this.ChordLabel3.Size = new System.Drawing.Size(30, 20);
            this.ChordLabel3.TabIndex = 9;
            this.ChordLabel3.Text = "ii";
            this.ChordLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel2
            // 
            this.ChordLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel2.Location = new System.Drawing.Point(20, 15);
            this.ChordLabel2.Name = "ChordLabel2";
            this.ChordLabel2.Size = new System.Drawing.Size(30, 20);
            this.ChordLabel2.TabIndex = 8;
            this.ChordLabel2.Text = "i#";
            this.ChordLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChordLabel1
            // 
            this.ChordLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel1.Location = new System.Drawing.Point(5, 35);
            this.ChordLabel1.Name = "ChordLabel1";
            this.ChordLabel1.Size = new System.Drawing.Size(30, 20);
            this.ChordLabel1.TabIndex = 7;
            this.ChordLabel1.Text = "I";
            this.ChordLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 372);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.logText);
            this.Controls.Add(this.TimelinePictureBox);
            this.Enabled = false;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.TimelinePictureBox)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox TimelinePictureBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox logText;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label ChordLabel12;
        private System.Windows.Forms.Label ChordLabel11;
        private System.Windows.Forms.Label ChordLabel9;
        private System.Windows.Forms.Label ChordLabel10;
        private System.Windows.Forms.Label ChordLabel8;
        private System.Windows.Forms.Label ChordLabel7;
        private System.Windows.Forms.Label ChordLabel6;
        private System.Windows.Forms.Label ChordLabel5;
        private System.Windows.Forms.Label ChordLabel4;
        private System.Windows.Forms.Label ChordLabel3;
        private System.Windows.Forms.Label ChordLabel2;
        private System.Windows.Forms.Label ChordLabel1;
    }
}

