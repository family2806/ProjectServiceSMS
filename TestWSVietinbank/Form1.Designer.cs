namespace TestWSVietinbank
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
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.btnEncodeHTML = new System.Windows.Forms.Button();
            this.btnDecodeHTML = new System.Windows.Forms.Button();
            this.btnCallService = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDencrypt = new System.Windows.Forms.Button();
            this.txtRequest = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(13, 208);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(520, 169);
            this.txtResponse.TabIndex = 1;
            // 
            // btnEncodeHTML
            // 
            this.btnEncodeHTML.Location = new System.Drawing.Point(13, 174);
            this.btnEncodeHTML.Name = "btnEncodeHTML";
            this.btnEncodeHTML.Size = new System.Drawing.Size(105, 23);
            this.btnEncodeHTML.TabIndex = 2;
            this.btnEncodeHTML.Text = "Encode HTML";
            this.btnEncodeHTML.UseVisualStyleBackColor = true;
            this.btnEncodeHTML.Click += new System.EventHandler(this.btnEncodeHTML_Click);
            // 
            // btnDecodeHTML
            // 
            this.btnDecodeHTML.Location = new System.Drawing.Point(124, 173);
            this.btnDecodeHTML.Name = "btnDecodeHTML";
            this.btnDecodeHTML.Size = new System.Drawing.Size(134, 23);
            this.btnDecodeHTML.TabIndex = 3;
            this.btnDecodeHTML.Text = "Decode HTML";
            this.btnDecodeHTML.UseVisualStyleBackColor = true;
            this.btnDecodeHTML.Click += new System.EventHandler(this.btnDecodeHTML_Click);
            // 
            // btnCallService
            // 
            this.btnCallService.Location = new System.Drawing.Point(264, 173);
            this.btnCallService.Name = "btnCallService";
            this.btnCallService.Size = new System.Drawing.Size(160, 23);
            this.btnCallService.TabIndex = 4;
            this.btnCallService.Text = "CALL Service";
            this.btnCallService.UseVisualStyleBackColor = true;
            this.btnCallService.Click += new System.EventHandler(this.btnCallService_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(539, 208);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(103, 23);
            this.btnEncrypt.TabIndex = 5;
            this.btnEncrypt.Text = "Encypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDencrypt
            // 
            this.btnDencrypt.Location = new System.Drawing.Point(539, 237);
            this.btnDencrypt.Name = "btnDencrypt";
            this.btnDencrypt.Size = new System.Drawing.Size(103, 23);
            this.btnDencrypt.TabIndex = 6;
            this.btnDencrypt.Text = "Dencypt";
            this.btnDencrypt.UseVisualStyleBackColor = true;
            this.btnDencrypt.Click += new System.EventHandler(this.btnDencrypt_Click);
            // 
            // txtRequest
            // 
            this.txtRequest.Location = new System.Drawing.Point(13, 13);
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.Size = new System.Drawing.Size(629, 154);
            this.txtRequest.TabIndex = 7;
            this.txtRequest.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 389);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.btnDencrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.btnCallService);
            this.Controls.Add(this.btnDecodeHTML);
            this.Controls.Add(this.btnEncodeHTML);
            this.Controls.Add(this.txtResponse);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Button btnEncodeHTML;
        private System.Windows.Forms.Button btnDecodeHTML;
        private System.Windows.Forms.Button btnCallService;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDencrypt;
        private System.Windows.Forms.RichTextBox txtRequest;
    }
}

