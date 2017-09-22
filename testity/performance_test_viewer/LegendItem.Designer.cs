
//   Copyright Giuseppe Campana (giu.campana@gmail.com) 2016.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)

namespace performance_test_viewer
{
    partial class LegendItem
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblColorBox = new System.Windows.Forms.Label();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.txtSourceCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblColorBox
            // 
            this.lblColorBox.BackColor = System.Drawing.Color.Red;
            this.lblColorBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblColorBox.Location = new System.Drawing.Point(39, 14);
            this.lblColorBox.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorBox.Name = "lblColorBox";
            this.lblColorBox.Size = new System.Drawing.Size(41, 30);
            this.lblColorBox.TabIndex = 0;
            // 
            // lblPercentage
            // 
            this.lblPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercentage.Location = new System.Drawing.Point(9, 56);
            this.lblPercentage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(106, 95);
            this.lblPercentage.TabIndex = 2;
            this.lblPercentage.Text = "label1";
            this.lblPercentage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtSourceCode
            // 
            this.txtSourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceCode.Location = new System.Drawing.Point(121, 5);
            this.txtSourceCode.Multiline = true;
            this.txtSourceCode.Name = "txtSourceCode";
            this.txtSourceCode.Size = new System.Drawing.Size(529, 153);
            this.txtSourceCode.TabIndex = 3;
            // 
            // LegendItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtSourceCode);
            this.Controls.Add(this.lblPercentage);
            this.Controls.Add(this.lblColorBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LegendItem";
            this.Size = new System.Drawing.Size(653, 158);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblColorBox;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.TextBox txtSourceCode;
    }
}
