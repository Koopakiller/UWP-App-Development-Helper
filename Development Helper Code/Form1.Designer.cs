namespace Koopakiller.Apps.UwpAppDevelopmentHelper.DevelopmentHelperCode
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.buttonOpenXmlAndWriteImages = new System.Windows.Forms.Button();
            this.buttonCompareImages = new System.Windows.Forms.Button();
            this.buttonCreateXML = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOpenXmlAndWriteImages
            // 
            this.buttonOpenXmlAndWriteImages.Location = new System.Drawing.Point(13, 13);
            this.buttonOpenXmlAndWriteImages.Name = "buttonOpenXmlAndWriteImages";
            this.buttonOpenXmlAndWriteImages.Size = new System.Drawing.Size(515, 43);
            this.buttonOpenXmlAndWriteImages.TabIndex = 0;
            this.buttonOpenXmlAndWriteImages.Text = "Open XML File and Generate Images";
            this.buttonOpenXmlAndWriteImages.UseVisualStyleBackColor = true;
            this.buttonOpenXmlAndWriteImages.Click += new System.EventHandler(this.buttonOpenXmlAndWriteImages_Click);
            // 
            // buttonCompareImages
            // 
            this.buttonCompareImages.Location = new System.Drawing.Point(12, 62);
            this.buttonCompareImages.Name = "buttonCompareImages";
            this.buttonCompareImages.Size = new System.Drawing.Size(516, 43);
            this.buttonCompareImages.TabIndex = 1;
            this.buttonCompareImages.Text = "Compare Images";
            this.buttonCompareImages.UseVisualStyleBackColor = true;
            this.buttonCompareImages.Click += new System.EventHandler(this.buttonCompareImages_Click);
            // 
            // buttonCreateXML
            // 
            this.buttonCreateXML.Location = new System.Drawing.Point(13, 111);
            this.buttonCreateXML.Name = "buttonCreateXML";
            this.buttonCreateXML.Size = new System.Drawing.Size(515, 43);
            this.buttonCreateXML.TabIndex = 2;
            this.buttonCreateXML.Text = "Create XML";
            this.buttonCreateXML.UseVisualStyleBackColor = true;
            this.buttonCreateXML.Click += new System.EventHandler(this.buttonCreateXML_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 457);
            this.Controls.Add(this.buttonCreateXML);
            this.Controls.Add(this.buttonCompareImages);
            this.Controls.Add(this.buttonOpenXmlAndWriteImages);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenXmlAndWriteImages;
        private System.Windows.Forms.Button buttonCompareImages;
        private System.Windows.Forms.Button buttonCreateXML;
    }
}

