namespace Class_Scheduling_System
{
    partial class Userform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Userform));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mANAGEMENTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProfessor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStudentSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProfessorSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSubject = new System.Windows.Forms.ToolStripMenuItem();
            this.yToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.recordStudentSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.recordProfessor = new System.Windows.Forms.ToolStripMenuItem();
            this.recordProfessorSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.mAINTENANCEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mtAcadYear = new System.Windows.Forms.ToolStripMenuItem();
            this.mtCourse = new System.Windows.Forms.ToolStripMenuItem();
            this.mtSection = new System.Windows.Forms.ToolStripMenuItem();
            this.mtRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.mtCampus = new System.Windows.Forms.ToolStripMenuItem();
            this.mnLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Location = new System.Drawing.Point(0, 88);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1212, 608);
            this.panel3.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.menuStrip1);
            this.panel2.Location = new System.Drawing.Point(0, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1212, 54);
            this.panel2.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mANAGEMENTToolStripMenuItem,
            this.yToolStripMenuItem,
            this.mAINTENANCEToolStripMenuItem,
            this.mnLogout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1212, 54);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mANAGEMENTToolStripMenuItem
            // 
            this.mANAGEMENTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStudent,
            this.menuProfessor,
            this.menuStudentSchedule,
            this.menuProfessorSchedule,
            this.menuSubject});
            this.mANAGEMENTToolStripMenuItem.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mANAGEMENTToolStripMenuItem.Name = "mANAGEMENTToolStripMenuItem";
            this.mANAGEMENTToolStripMenuItem.Size = new System.Drawing.Size(128, 50);
            this.mANAGEMENTToolStripMenuItem.Text = "MANAGEMENT";
            // 
            // menuStudent
            // 
            this.menuStudent.Name = "menuStudent";
            this.menuStudent.Size = new System.Drawing.Size(326, 22);
            this.menuStudent.Text = "MANAGE STUDENT";
            this.menuStudent.Click += new System.EventHandler(this.menuStudent_Click);
            // 
            // menuProfessor
            // 
            this.menuProfessor.Name = "menuProfessor";
            this.menuProfessor.Size = new System.Drawing.Size(326, 22);
            this.menuProfessor.Text = "MANAGE PROFESSOR";
            this.menuProfessor.Click += new System.EventHandler(this.menuProfessor_Click);
            // 
            // menuStudentSchedule
            // 
            this.menuStudentSchedule.Name = "menuStudentSchedule";
            this.menuStudentSchedule.Size = new System.Drawing.Size(326, 22);
            this.menuStudentSchedule.Text = "MANAGE STUDENT SCHEDULE";
            this.menuStudentSchedule.Click += new System.EventHandler(this.menuStudentSchedule_Click);
            // 
            // menuProfessorSchedule
            // 
            this.menuProfessorSchedule.Name = "menuProfessorSchedule";
            this.menuProfessorSchedule.Size = new System.Drawing.Size(326, 22);
            this.menuProfessorSchedule.Text = "MANAGE PROFESSOR SCHEDULE";
            this.menuProfessorSchedule.Click += new System.EventHandler(this.menuProfessorSchedule_Click);
            // 
            // menuSubject
            // 
            this.menuSubject.Name = "menuSubject";
            this.menuSubject.Size = new System.Drawing.Size(326, 22);
            this.menuSubject.Text = "MANAGE SUBJECT";
            this.menuSubject.Click += new System.EventHandler(this.menuSubject_Click);
            // 
            // yToolStripMenuItem
            // 
            this.yToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordStudent,
            this.recordStudentSchedule,
            this.recordProfessor,
            this.recordProfessorSchedule});
            this.yToolStripMenuItem.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yToolStripMenuItem.Name = "yToolStripMenuItem";
            this.yToolStripMenuItem.Size = new System.Drawing.Size(96, 50);
            this.yToolStripMenuItem.Text = "RECORDS";
            // 
            // recordStudent
            // 
            this.recordStudent.Name = "recordStudent";
            this.recordStudent.Size = new System.Drawing.Size(258, 22);
            this.recordStudent.Text = "STUDENT RECORDS";
            this.recordStudent.Click += new System.EventHandler(this.recordStudent_Click);
            // 
            // recordStudentSchedule
            // 
            this.recordStudentSchedule.Name = "recordStudentSchedule";
            this.recordStudentSchedule.Size = new System.Drawing.Size(258, 22);
            this.recordStudentSchedule.Text = "STUDENT SCHEDULE";
            this.recordStudentSchedule.Click += new System.EventHandler(this.recordStudentSchedule_Click);
            // 
            // recordProfessor
            // 
            this.recordProfessor.Name = "recordProfessor";
            this.recordProfessor.Size = new System.Drawing.Size(258, 22);
            this.recordProfessor.Text = "PROFESSOR RECORDS";
            this.recordProfessor.Click += new System.EventHandler(this.recordProfessor_Click);
            // 
            // recordProfessorSchedule
            // 
            this.recordProfessorSchedule.Name = "recordProfessorSchedule";
            this.recordProfessorSchedule.Size = new System.Drawing.Size(258, 22);
            this.recordProfessorSchedule.Text = "PROFESSOR SCHEDULE";
            this.recordProfessorSchedule.Click += new System.EventHandler(this.recordProfessorSchedule_Click);
            // 
            // mAINTENANCEToolStripMenuItem
            // 
            this.mAINTENANCEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mtAcadYear,
            this.mtCourse,
            this.mtSection,
            this.mtRoom,
            this.mtCampus});
            this.mAINTENANCEToolStripMenuItem.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mAINTENANCEToolStripMenuItem.Name = "mAINTENANCEToolStripMenuItem";
            this.mAINTENANCEToolStripMenuItem.Size = new System.Drawing.Size(129, 50);
            this.mAINTENANCEToolStripMenuItem.Text = "MAINTENANCE";
            // 
            // mtAcadYear
            // 
            this.mtAcadYear.Name = "mtAcadYear";
            this.mtAcadYear.Size = new System.Drawing.Size(198, 22);
            this.mtAcadYear.Text = "ACADEMIC YEAR";
            this.mtAcadYear.Click += new System.EventHandler(this.mtAcadYear_Click);
            // 
            // mtCourse
            // 
            this.mtCourse.Name = "mtCourse";
            this.mtCourse.Size = new System.Drawing.Size(198, 22);
            this.mtCourse.Text = "COURSE";
            this.mtCourse.Click += new System.EventHandler(this.mtCourse_Click);
            // 
            // mtSection
            // 
            this.mtSection.Name = "mtSection";
            this.mtSection.Size = new System.Drawing.Size(198, 22);
            this.mtSection.Text = "SECTION";
            this.mtSection.Click += new System.EventHandler(this.mtSection_Click);
            // 
            // mtRoom
            // 
            this.mtRoom.Name = "mtRoom";
            this.mtRoom.Size = new System.Drawing.Size(198, 22);
            this.mtRoom.Text = "ROOM";
            this.mtRoom.Click += new System.EventHandler(this.mtRoom_Click);
            // 
            // mtCampus
            // 
            this.mtCampus.Name = "mtCampus";
            this.mtCampus.Size = new System.Drawing.Size(198, 22);
            this.mtCampus.Text = "CAMPUS";
            this.mtCampus.Click += new System.EventHandler(this.mtCampus_Click);
            // 
            // mnLogout
            // 
            this.mnLogout.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnLogout.Name = "mnLogout";
            this.mnLogout.Size = new System.Drawing.Size(86, 50);
            this.mnLogout.Text = "LOGOUT";
            this.mnLogout.Click += new System.EventHandler(this.mnLogout_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnMinimize);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1212, 33);
            this.panel1.TabIndex = 5;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, -1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(47, 34);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 57;
            this.pictureBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(53, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "QUEZON CITY UNIVERSITY";
            // 
            // btnMinimize
            // 
            this.btnMinimize.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimize.Image")));
            this.btnMinimize.Location = new System.Drawing.Point(1136, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(36, 33);
            this.btnMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnMinimize.TabIndex = 1;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1178, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(34, 33);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClose.TabIndex = 1;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Userform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1212, 697);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Userform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Userform";
            this.Load += new System.EventHandler(this.Userform_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mANAGEMENTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuStudent;
        private System.Windows.Forms.ToolStripMenuItem menuProfessor;
        private System.Windows.Forms.ToolStripMenuItem menuStudentSchedule;
        private System.Windows.Forms.ToolStripMenuItem menuProfessorSchedule;
        private System.Windows.Forms.ToolStripMenuItem menuSubject;
        private System.Windows.Forms.ToolStripMenuItem yToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordStudent;
        private System.Windows.Forms.ToolStripMenuItem recordStudentSchedule;
        private System.Windows.Forms.ToolStripMenuItem recordProfessor;
        private System.Windows.Forms.ToolStripMenuItem recordProfessorSchedule;
        private System.Windows.Forms.ToolStripMenuItem mAINTENANCEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mtAcadYear;
        private System.Windows.Forms.ToolStripMenuItem mtCourse;
        private System.Windows.Forms.ToolStripMenuItem mtSection;
        private System.Windows.Forms.ToolStripMenuItem mtRoom;
        private System.Windows.Forms.ToolStripMenuItem mtCampus;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox btnMinimize;
        private System.Windows.Forms.PictureBox btnClose;
        public System.Windows.Forms.ToolStripMenuItem mnLogout;
    }
}