namespace midinous_tools
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
            num_orig_x = new NumericUpDown();
            num_scale = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            Btn_load_json = new Button();
            button2 = new Button();
            Btn_save_json = new Button();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            groupBox1 = new GroupBox();
            lbl_scale = new Label();
            lbl_origin = new Label();
            num_orig_y = new NumericUpDown();
            label10 = new Label();
            cmb_pointType = new ComboBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label11 = new Label();
            ((System.ComponentModel.ISupportInitialize)num_orig_x).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_scale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_orig_y).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // num_orig_x
            // 
            num_orig_x.Increment = new decimal(new int[] { 144, 0, 0, 0 });
            num_orig_x.Location = new Point(55, 23);
            num_orig_x.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            num_orig_x.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            num_orig_x.Name = "num_orig_x";
            num_orig_x.Size = new Size(54, 23);
            num_orig_x.TabIndex = 0;
            num_orig_x.ValueChanged += num_orig_x_ValueChanged;
            // 
            // num_scale
            // 
            num_scale.Increment = new decimal(new int[] { 144, 0, 0, 0 });
            num_scale.Location = new Point(55, 50);
            num_scale.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            num_scale.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_scale.Name = "num_scale";
            num_scale.Size = new Size(120, 23);
            num_scale.TabIndex = 1;
            num_scale.Value = new decimal(new int[] { 144, 0, 0, 0 });
            num_scale.ValueChanged += num_scale_ValueChanged;
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(60, 15);
            numericUpDown3.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(120, 23);
            numericUpDown3.TabIndex = 2;
            numericUpDown3.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 25);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 4;
            label1.Text = "origin";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 52);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 5;
            label2.Text = "scale";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 17);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 6;
            label3.Text = "sides";
            // 
            // Btn_load_json
            // 
            Btn_load_json.Location = new Point(12, 27);
            Btn_load_json.Name = "Btn_load_json";
            Btn_load_json.Size = new Size(75, 23);
            Btn_load_json.TabIndex = 7;
            Btn_load_json.Text = "Load json";
            Btn_load_json.UseVisualStyleBackColor = true;
            Btn_load_json.Click += Btn_load_json_Click;
            // 
            // button2
            // 
            button2.Location = new Point(236, 138);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 8;
            button2.Text = "Generate";
            button2.UseVisualStyleBackColor = true;
            // 
            // Btn_save_json
            // 
            Btn_save_json.Location = new Point(93, 27);
            Btn_save_json.Name = "Btn_save_json";
            Btn_save_json.Size = new Size(75, 23);
            Btn_save_json.TabIndex = 9;
            Btn_save_json.Text = "Save json";
            Btn_save_json.UseVisualStyleBackColor = true;
            Btn_save_json.Click += Btn_save_json_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 238);
            label4.Name = "label4";
            label4.Size = new Size(32, 15);
            label4.TabIndex = 10;
            label4.Text = "todo";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 265);
            label5.Name = "label5";
            label5.Size = new Size(85, 15);
            label5.TabIndex = 11;
            label5.Text = "control groups";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 297);
            label6.Name = "label6";
            label6.Size = new Size(98, 15);
            label6.TabIndex = 12;
            label6.Text = "set default values";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 325);
            label7.Name = "label7";
            label7.Size = new Size(185, 15);
            label7.TabIndex = 13;
            label7.Text = "create form for each type of point";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 355);
            label8.Name = "label8";
            label8.Size = new Size(52, 15);
            label8.TabIndex = 14;
            label8.Text = "sort json";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 382);
            label9.Name = "label9";
            label9.Size = new Size(100, 15);
            label9.TabIndex = 15;
            label9.Text = "fix json (reset IDs)";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lbl_scale);
            groupBox1.Controls.Add(lbl_origin);
            groupBox1.Controls.Add(num_orig_y);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(cmb_pointType);
            groupBox1.Controls.Add(tabControl1);
            groupBox1.Controls.Add(num_scale);
            groupBox1.Controls.Add(num_orig_x);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(233, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(555, 216);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Generation";
            // 
            // lbl_scale
            // 
            lbl_scale.AutoSize = true;
            lbl_scale.Location = new Point(181, 52);
            lbl_scale.Name = "lbl_scale";
            lbl_scale.Size = new Size(21, 15);
            lbl_scale.TabIndex = 18;
            lbl_scale.Text = "(1)";
            // 
            // lbl_origin
            // 
            lbl_origin.AutoSize = true;
            lbl_origin.Location = new Point(181, 25);
            lbl_origin.Name = "lbl_origin";
            lbl_origin.Size = new Size(30, 15);
            lbl_origin.TabIndex = 17;
            lbl_origin.Text = "(0,0)";
            // 
            // num_orig_y
            // 
            num_orig_y.Increment = new decimal(new int[] { 144, 0, 0, 0 });
            num_orig_y.Location = new Point(121, 23);
            num_orig_y.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            num_orig_y.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            num_orig_y.Name = "num_orig_y";
            num_orig_y.Size = new Size(54, 23);
            num_orig_y.TabIndex = 12;
            num_orig_y.ValueChanged += num_orig_y_ValueChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(19, 79);
            label10.Name = "label10";
            label10.Size = new Size(30, 15);
            label10.TabIndex = 11;
            label10.Text = "type";
            // 
            // cmb_pointType
            // 
            cmb_pointType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_pointType.FormattingEnabled = true;
            cmb_pointType.Items.AddRange(new object[] { "note", "cc", "logic" });
            cmb_pointType.Location = new Point(55, 79);
            cmb_pointType.Name = "cmb_pointType";
            cmb_pointType.Size = new Size(120, 23);
            cmb_pointType.TabIndex = 10;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(224, 15);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(325, 195);
            tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(numericUpDown3);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(317, 167);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Regular Polygon";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 406);
            label11.Name = "label11";
            label11.Size = new Size(542, 15);
            label11.TabIndex = 17;
            label11.Text = "link control group to control group (settings for one to all, all to all and one to one,  and bidirectional)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label11);
            Controls.Add(groupBox1);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(Btn_save_json);
            Controls.Add(Btn_load_json);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)num_orig_x).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_scale).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_orig_y).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown num_orig_x;
        private NumericUpDown num_scale;
        private NumericUpDown numericUpDown3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button Btn_load_json;
        private Button button2;
        private Button Btn_save_json;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private GroupBox groupBox1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Label label10;
        private ComboBox cmb_pointType;
        private NumericUpDown num_orig_y;
        private Label lbl_origin;
        private Label lbl_scale;
        private Label label11;
    }
}
