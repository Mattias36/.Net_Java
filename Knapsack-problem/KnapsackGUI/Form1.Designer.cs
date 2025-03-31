namespace KnapsackGUI
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
            txtItemCount = new TextBox();
            txtSeed = new TextBox();
            txtCapacity = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnSolve = new Button();
            txtResult = new TextBox();
            txtInstanceResult = new TextBox();
            SuspendLayout();
            // 
            // txtItemCount
            // 
            txtItemCount.Location = new Point(68, 122);
            txtItemCount.Name = "txtItemCount";
            txtItemCount.Size = new Size(125, 27);
            txtItemCount.TabIndex = 0;
            txtItemCount.TextChanged += textBox1_TextChanged;
            // 
            // txtSeed
            // 
            txtSeed.Location = new Point(68, 175);
            txtSeed.Name = "txtSeed";
            txtSeed.Size = new Size(125, 27);
            txtSeed.TabIndex = 1;
            // 
            // txtCapacity
            // 
            txtCapacity.Location = new Point(68, 239);
            txtCapacity.Name = "txtCapacity";
            txtCapacity.Size = new Size(125, 27);
            txtCapacity.TabIndex = 2;
            txtCapacity.TextChanged += textBox2_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(68, 94);
            label1.Name = "label1";
            label1.Size = new Size(123, 20);
            label1.TabIndex = 3;
            label1.Text = "ilosc przemiotow";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(64, 152);
            label2.Name = "label2";
            label2.Size = new Size(134, 20);
            label2.TabIndex = 4;
            label2.Text = "nasienie losowania";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(68, 216);
            label3.Name = "label3";
            label3.Size = new Size(82, 20);
            label3.TabIndex = 5;
            label3.Text = "pojemnosc";
            // 
            // btnSolve
            // 
            btnSolve.BackColor = SystemColors.ButtonHighlight;
            btnSolve.ForeColor = SystemColors.ActiveCaption;
            btnSolve.Location = new Point(97, 296);
            btnSolve.Name = "btnSolve";
            btnSolve.Size = new Size(94, 29);
            btnSolve.TabIndex = 6;
            btnSolve.Text = "rozwiaz";
            btnSolve.UseVisualStyleBackColor = false;
            btnSolve.Click += btnSolve_Click;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(68, 366);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.ScrollBars = ScrollBars.Vertical;
            txtResult.Size = new Size(291, 314);
            txtResult.TabIndex = 7;
            // 
            // txtInstanceResult
            // 
            txtInstanceResult.Location = new Point(494, 94);
            txtInstanceResult.Multiline = true;
            txtInstanceResult.Name = "txtInstanceResult";
            txtInstanceResult.ReadOnly = true;
            txtInstanceResult.ScrollBars = ScrollBars.Vertical;
            txtInstanceResult.Size = new Size(386, 586);
            txtInstanceResult.TabIndex = 8;
            txtInstanceResult.TextChanged += txtResult_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 706);
            Controls.Add(txtInstanceResult);
            Controls.Add(txtResult);
            Controls.Add(btnSolve);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtCapacity);
            Controls.Add(txtSeed);
            Controls.Add(txtItemCount);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtItemCount;
        private TextBox txtSeed;
        private TextBox txtCapacity;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnSolve;
        private TextBox txtResult;
        private TextBox txtInstanceResult;
    }
}
