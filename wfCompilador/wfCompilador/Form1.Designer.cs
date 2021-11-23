
namespace wfCompilador
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGuardar = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.gramatica = new System.Windows.Forms.TabPage();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAnalizar = new System.Windows.Forms.Button();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.resultado = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gvPila = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lblValidar = new System.Windows.Forms.Label();
            this.btnAnalizarEntrada = new System.Windows.Forms.Button();
            this.textEntrada = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gvTablaParser = new System.Windows.Forms.DataGridView();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.fileSystemWatcher2 = new System.IO.FileSystemWatcher();
            this.tabControl1.SuspendLayout();
            this.gramatica.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.resultado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPila)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTablaParser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(595, 16);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.gramatica);
            this.tabControl1.Controls.Add(this.resultado);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(886, 717);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 3;
            // 
            // gramatica
            // 
            this.gramatica.Controls.Add(this.txtTexto);
            this.gramatica.Controls.Add(this.groupBox1);
            this.gramatica.Location = new System.Drawing.Point(4, 22);
            this.gramatica.Name = "gramatica";
            this.gramatica.Padding = new System.Windows.Forms.Padding(3);
            this.gramatica.Size = new System.Drawing.Size(878, 691);
            this.gramatica.TabIndex = 0;
            this.gramatica.Text = "Compilador";
            this.gramatica.UseVisualStyleBackColor = true;
            // 
            // txtTexto
            // 
            this.txtTexto.BackColor = System.Drawing.SystemColors.WindowText;
            this.txtTexto.Font = new System.Drawing.Font("Lucida Console", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTexto.ForeColor = System.Drawing.SystemColors.Window;
            this.txtTexto.Location = new System.Drawing.Point(6, 67);
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTexto.Size = new System.Drawing.Size(866, 374);
            this.txtTexto.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAnalizar);
            this.groupBox1.Controls.Add(this.txtDocumento);
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.btnAbrir);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(866, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Documento";
            // 
            // btnAnalizar
            // 
            this.btnAnalizar.Location = new System.Drawing.Point(774, 16);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(75, 23);
            this.btnAnalizar.TabIndex = 3;
            this.btnAnalizar.Text = "Analizar";
            this.btnAnalizar.UseVisualStyleBackColor = true;
            this.btnAnalizar.Click += new System.EventHandler(this.btnAnalizar_Click);
            // 
            // txtDocumento
            // 
            this.txtDocumento.Enabled = false;
            this.txtDocumento.Location = new System.Drawing.Point(7, 19);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(479, 20);
            this.txtDocumento.TabIndex = 1;
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(504, 16);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(75, 23);
            this.btnAbrir.TabIndex = 0;
            this.btnAbrir.Text = "Abrir";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // resultado
            // 
            this.resultado.AutoScroll = true;
            this.resultado.Controls.Add(this.label4);
            this.resultado.Controls.Add(this.label3);
            this.resultado.Controls.Add(this.gvPila);
            this.resultado.Controls.Add(this.label2);
            this.resultado.Controls.Add(this.lblValidar);
            this.resultado.Controls.Add(this.btnAnalizarEntrada);
            this.resultado.Controls.Add(this.textEntrada);
            this.resultado.Controls.Add(this.label1);
            this.resultado.Controls.Add(this.gvTablaParser);
            this.resultado.Controls.Add(this.btnRegresar);
            this.resultado.Controls.Add(this.txtResultado);
            this.resultado.Location = new System.Drawing.Point(4, 22);
            this.resultado.Name = "resultado";
            this.resultado.Size = new System.Drawing.Size(878, 691);
            this.resultado.TabIndex = 1;
            this.resultado.Text = "Resultado";
            this.resultado.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 495);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pila compilador";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Reglas Compilador";
            // 
            // gvPila
            // 
            this.gvPila.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPila.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPila.Location = new System.Drawing.Point(-8, 514);
            this.gvPila.Name = "gvPila";
            this.gvPila.Size = new System.Drawing.Size(886, 159);
            this.gvPila.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(283, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Favor ingresar cada valor separado por un espacio";
            // 
            // lblValidar
            // 
            this.lblValidar.AutoSize = true;
            this.lblValidar.Location = new System.Drawing.Point(424, 495);
            this.lblValidar.Name = "lblValidar";
            this.lblValidar.Size = new System.Drawing.Size(0, 13);
            this.lblValidar.TabIndex = 6;
            // 
            // btnAnalizarEntrada
            // 
            this.btnAnalizarEntrada.Location = new System.Drawing.Point(787, 53);
            this.btnAnalizarEntrada.Name = "btnAnalizarEntrada";
            this.btnAnalizarEntrada.Size = new System.Drawing.Size(74, 26);
            this.btnAnalizarEntrada.TabIndex = 5;
            this.btnAnalizarEntrada.Text = "Analizar";
            this.btnAnalizarEntrada.UseVisualStyleBackColor = true;
            this.btnAnalizarEntrada.Click += new System.EventHandler(this.btnAnalizarEntrada_Click);
            // 
            // textEntrada
            // 
            this.textEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEntrada.Location = new System.Drawing.Point(113, 56);
            this.textEntrada.Name = "textEntrada";
            this.textEntrada.Size = new System.Drawing.Size(668, 23);
            this.textEntrada.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cadena a validar:";
            // 
            // gvTablaParser
            // 
            this.gvTablaParser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvTablaParser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTablaParser.Location = new System.Drawing.Point(3, 317);
            this.gvTablaParser.Name = "gvTablaParser";
            this.gvTablaParser.Size = new System.Drawing.Size(872, 157);
            this.gvTablaParser.TabIndex = 2;
            // 
            // btnRegresar
            // 
            this.btnRegresar.Location = new System.Drawing.Point(787, 13);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(75, 23);
            this.btnRegresar.TabIndex = 1;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // txtResultado
            // 
            this.txtResultado.BackColor = System.Drawing.SystemColors.Desktop;
            this.txtResultado.Font = new System.Drawing.Font("Lucida Console", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultado.ForeColor = System.Drawing.SystemColors.Window;
            this.txtResultado.Location = new System.Drawing.Point(3, 81);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado.Size = new System.Drawing.Size(872, 200);
            this.txtResultado.TabIndex = 0;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // fileSystemWatcher2
            // 
            this.fileSystemWatcher2.EnableRaisingEvents = true;
            this.fileSystemWatcher2.SynchronizingObject = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(910, 741);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Compilador";
            this.tabControl1.ResumeLayout(false);
            this.gramatica.ResumeLayout(false);
            this.gramatica.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.resultado.ResumeLayout(false);
            this.resultado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPila)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTablaParser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage gramatica;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Button btnAbrir;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.IO.FileSystemWatcher fileSystemWatcher2;
        private System.Windows.Forms.Button btnAnalizar;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.TabPage resultado;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.DataGridView gvTablaParser;
        private System.Windows.Forms.Button btnAnalizarEntrada;
        private System.Windows.Forms.TextBox textEntrada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblValidar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gvPila;
    }
}

