namespace DigiSign
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.signatureBox = new System.Windows.Forms.PictureBox();
            this.sampleNameTextBox = new System.Windows.Forms.TextBox();
            this.startSamplingBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nextSampleBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.sampleCountBox = new System.Windows.Forms.NumericUpDown();
            this.retryBtn = new System.Windows.Forms.Button();
            this.sampleNameLb = new System.Windows.Forms.Label();
            this.endBtn = new System.Windows.Forms.Button();
            this.forBtn = new System.Windows.Forms.Button();
            this.genBtn = new System.Windows.Forms.Button();
            this.countLb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.signatureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sampleCountBox)).BeginInit();
            this.SuspendLayout();
            // 
            // signatureBox
            // 
            this.signatureBox.BackColor = System.Drawing.SystemColors.Desktop;
            this.signatureBox.Location = new System.Drawing.Point(140, 5);
            this.signatureBox.Name = "signatureBox";
            this.signatureBox.Size = new System.Drawing.Size(1096, 685);
            this.signatureBox.TabIndex = 0;
            this.signatureBox.TabStop = false;
            // 
            // sampleNameTextBox
            // 
            this.sampleNameTextBox.Location = new System.Drawing.Point(8, 46);
            this.sampleNameTextBox.Name = "sampleNameTextBox";
            this.sampleNameTextBox.Size = new System.Drawing.Size(126, 31);
            this.sampleNameTextBox.TabIndex = 1;
            // 
            // startSamplingBtn
            // 
            this.startSamplingBtn.Location = new System.Drawing.Point(12, 322);
            this.startSamplingBtn.Name = "startSamplingBtn";
            this.startSamplingBtn.Size = new System.Drawing.Size(112, 50);
            this.startSamplingBtn.TabIndex = 2;
            this.startSamplingBtn.Text = "Start";
            this.startSamplingBtn.UseVisualStyleBackColor = true;
            this.startSamplingBtn.Click += new System.EventHandler(this.startSamplingBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sample name";
            // 
            // nextSampleBtn
            // 
            this.nextSampleBtn.Location = new System.Drawing.Point(12, 378);
            this.nextSampleBtn.Name = "nextSampleBtn";
            this.nextSampleBtn.Size = new System.Drawing.Size(112, 50);
            this.nextSampleBtn.TabIndex = 4;
            this.nextSampleBtn.Text = "Next";
            this.nextSampleBtn.UseVisualStyleBackColor = true;
            this.nextSampleBtn.Click += new System.EventHandler(this.nextSampleBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sample count";
            // 
            // sampleCountBox
            // 
            this.sampleCountBox.Location = new System.Drawing.Point(34, 116);
            this.sampleCountBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sampleCountBox.Name = "sampleCountBox";
            this.sampleCountBox.Size = new System.Drawing.Size(62, 31);
            this.sampleCountBox.TabIndex = 7;
            this.sampleCountBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // retryBtn
            // 
            this.retryBtn.Location = new System.Drawing.Point(12, 508);
            this.retryBtn.Name = "retryBtn";
            this.retryBtn.Size = new System.Drawing.Size(112, 50);
            this.retryBtn.TabIndex = 8;
            this.retryBtn.Text = "Retry";
            this.retryBtn.UseVisualStyleBackColor = true;
            this.retryBtn.Click += new System.EventHandler(this.retryBtn_Click);
            // 
            // sampleNameLb
            // 
            this.sampleNameLb.AutoSize = true;
            this.sampleNameLb.Location = new System.Drawing.Point(22, 101);
            this.sampleNameLb.Name = "sampleNameLb";
            this.sampleNameLb.Size = new System.Drawing.Size(0, 25);
            this.sampleNameLb.TabIndex = 9;
            // 
            // endBtn
            // 
            this.endBtn.Location = new System.Drawing.Point(12, 637);
            this.endBtn.Name = "endBtn";
            this.endBtn.Size = new System.Drawing.Size(112, 53);
            this.endBtn.TabIndex = 10;
            this.endBtn.Text = "End";
            this.endBtn.UseVisualStyleBackColor = true;
            this.endBtn.Click += new System.EventHandler(this.endBtn_Click);
            // 
            // forBtn
            // 
            this.forBtn.Location = new System.Drawing.Point(12, 172);
            this.forBtn.Name = "forBtn";
            this.forBtn.Size = new System.Drawing.Size(112, 34);
            this.forBtn.TabIndex = 11;
            this.forBtn.Text = "Forgery";
            this.forBtn.UseVisualStyleBackColor = true;
            this.forBtn.Click += new System.EventHandler(this.forBtn_Click);
            // 
            // genBtn
            // 
            this.genBtn.Location = new System.Drawing.Point(12, 249);
            this.genBtn.Name = "genBtn";
            this.genBtn.Size = new System.Drawing.Size(112, 34);
            this.genBtn.TabIndex = 12;
            this.genBtn.Text = "Genuine";
            this.genBtn.UseVisualStyleBackColor = true;
            this.genBtn.Click += new System.EventHandler(this.genBtn_Click);
            // 
            // countLb
            // 
            this.countLb.AutoSize = true;
            this.countLb.Location = new System.Drawing.Point(28, 116);
            this.countLb.Name = "countLb";
            this.countLb.Size = new System.Drawing.Size(0, 25);
            this.countLb.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 695);
            this.Controls.Add(this.countLb);
            this.Controls.Add(this.genBtn);
            this.Controls.Add(this.forBtn);
            this.Controls.Add(this.endBtn);
            this.Controls.Add(this.sampleNameLb);
            this.Controls.Add(this.retryBtn);
            this.Controls.Add(this.sampleCountBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nextSampleBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startSamplingBtn);
            this.Controls.Add(this.sampleNameTextBox);
            this.Controls.Add(this.signatureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivated);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.signatureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sampleCountBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox signatureBox;
        private TextBox sampleNameTextBox;
        private Button startSamplingBtn;
        private Label label1;
        private Button nextSampleBtn;
        private Label label2;
        private NumericUpDown sampleCountBox;
        private Button retryBtn;
        private Label sampleNameLb;
        private Button endBtn;
        private Button forBtn;
        private Button genBtn;
        private Label countLb;
    }
}