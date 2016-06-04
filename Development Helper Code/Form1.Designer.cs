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
            this.buttonAddMSDNIconInfo = new System.Windows.Forms.Button();
            this.buttonLowerCaseTags = new System.Windows.Forms.Button();
            this.buttonDistinctTags = new System.Windows.Forms.Button();
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
            // buttonAddMSDNIconInfo
            // 
            this.buttonAddMSDNIconInfo.Location = new System.Drawing.Point(13, 160);
            this.buttonAddMSDNIconInfo.Name = "buttonAddMSDNIconInfo";
            this.buttonAddMSDNIconInfo.Size = new System.Drawing.Size(515, 43);
            this.buttonAddMSDNIconInfo.TabIndex = 3;
            this.buttonAddMSDNIconInfo.Text = "Add MSDN Icon Info";
            this.buttonAddMSDNIconInfo.UseVisualStyleBackColor = true;
            this.buttonAddMSDNIconInfo.Click += new System.EventHandler(this.buttonAddMSDNIconInfo_Click);
            // 
            // buttonLowerCaseTags
            // 
            this.buttonLowerCaseTags.Location = new System.Drawing.Point(12, 209);
            this.buttonLowerCaseTags.Name = "buttonLowerCaseTags";
            this.buttonLowerCaseTags.Size = new System.Drawing.Size(516, 43);
            this.buttonLowerCaseTags.TabIndex = 4;
            this.buttonLowerCaseTags.Text = "Lowercase Tags";
            this.buttonLowerCaseTags.UseVisualStyleBackColor = true;
            this.buttonLowerCaseTags.Click += new System.EventHandler(this.buttonLowerCaseTags_Click);
            // 
            // buttonDistinctTags
            // 
            this.buttonDistinctTags.Location = new System.Drawing.Point(12, 258);
            this.buttonDistinctTags.Name = "buttonDistinctTags";
            this.buttonDistinctTags.Size = new System.Drawing.Size(516, 43);
            this.buttonDistinctTags.TabIndex = 5;
            this.buttonDistinctTags.Text = "Distinct Tags";
            this.buttonDistinctTags.UseVisualStyleBackColor = true;
            this.buttonDistinctTags.Click += new System.EventHandler(this.buttonDistinctTags_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 457);
            this.Controls.Add(this.buttonDistinctTags);
            this.Controls.Add(this.buttonLowerCaseTags);
            this.Controls.Add(this.buttonAddMSDNIconInfo);
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
        private System.Windows.Forms.Button buttonAddMSDNIconInfo;
        private System.Windows.Forms.Button buttonLowerCaseTags;
        private System.Windows.Forms.Button buttonDistinctTags;
    }
}

