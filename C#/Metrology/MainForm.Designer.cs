namespace Metrology
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.comboBoxDistribution = new System.Windows.Forms.ComboBox();
            this.numericUpDownA = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownB = new System.Windows.Forms.NumericUpDown();
            this.labelA = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownNumOfSplits = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownN = new System.Windows.Forms.NumericUpDown();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dotsDataGrid = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxCriterian = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageNormal = new System.Windows.Forms.TabPage();
            this.numericUpDownDispersion = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownExpectedValue = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPageExponential = new System.Windows.Forms.TabPage();
            this.numericUpDownIntens = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPageUniform = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownUniformA = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownUniformB = new System.Windows.Forms.NumericUpDown();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.groupBoxInterval = new System.Windows.Forms.GroupBox();
            this.buttonManual = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxManual = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumOfSplits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotsDataGrid)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageNormal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDispersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExpectedValue)).BeginInit();
            this.tabPageExponential.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntens)).BeginInit();
            this.tabPageUniform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUniformA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUniformB)).BeginInit();
            this.groupBoxInterval.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxDistribution
            // 
            this.comboBoxDistribution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDistribution.FormattingEnabled = true;
            this.comboBoxDistribution.Location = new System.Drawing.Point(100, 196);
            this.comboBoxDistribution.Name = "comboBoxDistribution";
            this.comboBoxDistribution.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDistribution.TabIndex = 0;
            this.comboBoxDistribution.SelectedValueChanged += new System.EventHandler(this.comboBoxDistribution_SelectedValueChanged);
            // 
            // numericUpDownA
            // 
            this.numericUpDownA.DecimalPlaces = 2;
            this.numericUpDownA.Location = new System.Drawing.Point(75, 21);
            this.numericUpDownA.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownA.Name = "numericUpDownA";
            this.numericUpDownA.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownA.TabIndex = 1;
            // 
            // numericUpDownB
            // 
            this.numericUpDownB.DecimalPlaces = 2;
            this.numericUpDownB.Location = new System.Drawing.Point(75, 47);
            this.numericUpDownB.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownB.Name = "numericUpDownB";
            this.numericUpDownB.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownB.TabIndex = 2;
            this.numericUpDownB.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // labelA
            // 
            this.labelA.AutoSize = true;
            this.labelA.Location = new System.Drawing.Point(6, 23);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(13, 13);
            this.labelA.TabIndex = 3;
            this.labelA.Text = "a";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "b";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Количество точек разбиения";
            // 
            // numericUpDownNumOfSplits
            // 
            this.numericUpDownNumOfSplits.Location = new System.Drawing.Point(428, 197);
            this.numericUpDownNumOfSplits.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownNumOfSplits.Name = "numericUpDownNumOfSplits";
            this.numericUpDownNumOfSplits.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownNumOfSplits.TabIndex = 5;
            this.numericUpDownNumOfSplits.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Количество точек";
            // 
            // numericUpDownN
            // 
            this.numericUpDownN.Location = new System.Drawing.Point(428, 223);
            this.numericUpDownN.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownN.Name = "numericUpDownN";
            this.numericUpDownN.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownN.TabIndex = 7;
            this.numericUpDownN.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // chart1
            // 
            chartArea9.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea9);
            legend9.Name = "Legend1";
            this.chart1.Legends.Add(legend9);
            this.chart1.Location = new System.Drawing.Point(11, 11);
            this.chart1.Name = "chart1";
            series9.ChartArea = "ChartArea1";
            series9.Legend = "Legend1";
            series9.Name = "Series1";
            this.chart1.Series.Add(series9);
            this.chart1.Size = new System.Drawing.Size(253, 172);
            this.chart1.TabIndex = 9;
            this.chart1.Text = "chart1";
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(271, 282);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(94, 23);
            this.buttonGenerate.TabIndex = 10;
            this.buttonGenerate.Text = "Сгенерировать";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(371, 282);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Исключить промахи";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Распределение";
            // 
            // dotsDataGrid
            // 
            this.dotsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dotsDataGrid.Location = new System.Drawing.Point(271, 12);
            this.dotsDataGrid.Name = "dotsDataGrid";
            this.dotsDataGrid.Size = new System.Drawing.Size(240, 171);
            this.dotsDataGrid.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Критерий оценки";
            // 
            // comboBoxCriterian
            // 
            this.comboBoxCriterian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCriterian.FormattingEnabled = true;
            this.comboBoxCriterian.Location = new System.Drawing.Point(363, 249);
            this.comboBoxCriterian.Name = "comboBoxCriterian";
            this.comboBoxCriterian.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCriterian.TabIndex = 15;
            this.comboBoxCriterian.SelectedValueChanged += new System.EventHandler(this.comboBoxCriterian_SelectedValueChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(271, 316);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(220, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Обработать результаты измерений";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageNormal);
            this.tabControl1.Controls.Add(this.tabPageExponential);
            this.tabControl1.Controls.Add(this.tabPageUniform);
            this.tabControl1.Location = new System.Drawing.Point(35, 225);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(186, 87);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 17;
            // 
            // tabPageNormal
            // 
            this.tabPageNormal.Controls.Add(this.numericUpDownDispersion);
            this.tabPageNormal.Controls.Add(this.numericUpDownExpectedValue);
            this.tabPageNormal.Controls.Add(this.label9);
            this.tabPageNormal.Controls.Add(this.label6);
            this.tabPageNormal.Location = new System.Drawing.Point(4, 22);
            this.tabPageNormal.Name = "tabPageNormal";
            this.tabPageNormal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNormal.Size = new System.Drawing.Size(178, 61);
            this.tabPageNormal.TabIndex = 0;
            this.tabPageNormal.Text = "Нормальное";
            this.tabPageNormal.UseVisualStyleBackColor = true;
            // 
            // numericUpDownDispersion
            // 
            this.numericUpDownDispersion.DecimalPlaces = 2;
            this.numericUpDownDispersion.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownDispersion.Location = new System.Drawing.Point(104, 32);
            this.numericUpDownDispersion.Name = "numericUpDownDispersion";
            this.numericUpDownDispersion.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownDispersion.TabIndex = 19;
            this.numericUpDownDispersion.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDownExpectedValue
            // 
            this.numericUpDownExpectedValue.DecimalPlaces = 2;
            this.numericUpDownExpectedValue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownExpectedValue.Location = new System.Drawing.Point(104, 6);
            this.numericUpDownExpectedValue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownExpectedValue.Name = "numericUpDownExpectedValue";
            this.numericUpDownExpectedValue.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownExpectedValue.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Дисперсия";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Мат. ожидание";
            // 
            // tabPageExponential
            // 
            this.tabPageExponential.Controls.Add(this.numericUpDownIntens);
            this.tabPageExponential.Controls.Add(this.label7);
            this.tabPageExponential.Location = new System.Drawing.Point(4, 22);
            this.tabPageExponential.Name = "tabPageExponential";
            this.tabPageExponential.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExponential.Size = new System.Drawing.Size(178, 61);
            this.tabPageExponential.TabIndex = 1;
            this.tabPageExponential.Text = "Экспоненциальное";
            this.tabPageExponential.UseVisualStyleBackColor = true;
            // 
            // numericUpDownIntens
            // 
            this.numericUpDownIntens.DecimalPlaces = 2;
            this.numericUpDownIntens.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownIntens.Location = new System.Drawing.Point(104, 6);
            this.numericUpDownIntens.Name = "numericUpDownIntens";
            this.numericUpDownIntens.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownIntens.TabIndex = 20;
            this.numericUpDownIntens.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Интенсивность";
            // 
            // tabPageUniform
            // 
            this.tabPageUniform.Controls.Add(this.label8);
            this.tabPageUniform.Controls.Add(this.numericUpDownUniformA);
            this.tabPageUniform.Controls.Add(this.label10);
            this.tabPageUniform.Controls.Add(this.numericUpDownUniformB);
            this.tabPageUniform.Location = new System.Drawing.Point(4, 22);
            this.tabPageUniform.Name = "tabPageUniform";
            this.tabPageUniform.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUniform.Size = new System.Drawing.Size(178, 61);
            this.tabPageUniform.TabIndex = 2;
            this.tabPageUniform.Text = "Равномерное";
            this.tabPageUniform.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "b";
            // 
            // numericUpDownUniformA
            // 
            this.numericUpDownUniformA.Location = new System.Drawing.Point(104, 6);
            this.numericUpDownUniformA.Name = "numericUpDownUniformA";
            this.numericUpDownUniformA.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownUniformA.TabIndex = 18;
            this.numericUpDownUniformA.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "a";
            // 
            // numericUpDownUniformB
            // 
            this.numericUpDownUniformB.Location = new System.Drawing.Point(104, 32);
            this.numericUpDownUniformB.Name = "numericUpDownUniformB";
            this.numericUpDownUniformB.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownUniformB.TabIndex = 19;
            this.numericUpDownUniformB.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(271, 345);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.Size = new System.Drawing.Size(220, 20);
            this.textBoxResult.TabIndex = 18;
            this.textBoxResult.Text = "x = ";
            this.textBoxResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBoxInterval
            // 
            this.groupBoxInterval.Controls.Add(this.numericUpDownA);
            this.groupBoxInterval.Controls.Add(this.numericUpDownB);
            this.groupBoxInterval.Controls.Add(this.labelA);
            this.groupBoxInterval.Controls.Add(this.label1);
            this.groupBoxInterval.Location = new System.Drawing.Point(39, 318);
            this.groupBoxInterval.Name = "groupBoxInterval";
            this.groupBoxInterval.Size = new System.Drawing.Size(178, 75);
            this.groupBoxInterval.TabIndex = 19;
            this.groupBoxInterval.TabStop = false;
            this.groupBoxInterval.Text = "Границы интервала";
            // 
            // buttonManual
            // 
            this.buttonManual.Location = new System.Drawing.Point(9, 46);
            this.buttonManual.Name = "buttonManual";
            this.buttonManual.Size = new System.Drawing.Size(94, 23);
            this.buttonManual.TabIndex = 20;
            this.buttonManual.Text = "Ввести";
            this.buttonManual.UseVisualStyleBackColor = true;
            this.buttonManual.Click += new System.EventHandler(this.buttonManual_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxManual);
            this.groupBox1.Controls.Add(this.buttonManual);
            this.groupBox1.Location = new System.Drawing.Point(39, 400);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 76);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ручной ввод";
            // 
            // textBoxManual
            // 
            this.textBoxManual.Location = new System.Drawing.Point(9, 20);
            this.textBoxManual.Name = "textBoxManual";
            this.textBoxManual.Size = new System.Drawing.Size(167, 20);
            this.textBoxManual.TabIndex = 21;
            this.textBoxManual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 486);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxInterval);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBoxCriterian);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dotsDataGrid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownNumOfSplits);
            this.Controls.Add(this.comboBoxDistribution);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Главное окно";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumOfSplits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotsDataGrid)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageNormal.ResumeLayout(false);
            this.tabPageNormal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDispersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExpectedValue)).EndInit();
            this.tabPageExponential.ResumeLayout(false);
            this.tabPageExponential.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntens)).EndInit();
            this.tabPageUniform.ResumeLayout(false);
            this.tabPageUniform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUniformA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUniformB)).EndInit();
            this.groupBoxInterval.ResumeLayout(false);
            this.groupBoxInterval.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxDistribution;
        private System.Windows.Forms.NumericUpDown numericUpDownA;
        private System.Windows.Forms.NumericUpDown numericUpDownB;
        private System.Windows.Forms.Label labelA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownNumOfSplits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownN;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dotsDataGrid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxCriterian;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageNormal;
        private System.Windows.Forms.TabPage tabPageExponential;
        private System.Windows.Forms.TabPage tabPageUniform;
        private System.Windows.Forms.NumericUpDown numericUpDownDispersion;
        private System.Windows.Forms.NumericUpDown numericUpDownExpectedValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownIntens;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownUniformA;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownUniformB;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.GroupBox groupBoxInterval;
        private System.Windows.Forms.Button buttonManual;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxManual;
    }
}

