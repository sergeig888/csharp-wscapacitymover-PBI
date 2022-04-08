
namespace PBI_WorkspaceCapacityMover
{
    partial class formMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.lblTenantId = new System.Windows.Forms.Label();
            this.tbTenantId = new System.Windows.Forms.TextBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.btnGetMyWorkspaces = new System.Windows.Forms.Button();
            this.dgViewMyWorkspaces = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkspaceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapacityId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbCapacities = new System.Windows.Forms.ComboBox();
            this.lblCapacities = new System.Windows.Forms.Label();
            this.btnAssignToCapacity = new System.Windows.Forms.Button();
            this.tbCapacityInfo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewMyWorkspaces)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTenantId
            // 
            this.lblTenantId.AutoSize = true;
            this.lblTenantId.Location = new System.Drawing.Point(3, 16);
            this.lblTenantId.Name = "lblTenantId";
            this.lblTenantId.Size = new System.Drawing.Size(58, 13);
            this.lblTenantId.TabIndex = 0;
            this.lblTenantId.Text = "Tenant ID:";
            // 
            // tbTenantId
            // 
            this.tbTenantId.Location = new System.Drawing.Point(83, 13);
            this.tbTenantId.Name = "tbTenantId";
            this.tbTenantId.ReadOnly = true;
            this.tbTenantId.Size = new System.Drawing.Size(269, 20);
            this.tbTenantId.TabIndex = 1;
            // 
            // btnLogIn
            // 
            this.btnLogIn.Location = new System.Drawing.Point(371, 11);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(75, 23);
            this.btnLogIn.TabIndex = 2;
            this.btnLogIn.Text = "Login";
            this.btnLogIn.UseVisualStyleBackColor = true;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // btnGetMyWorkspaces
            // 
            this.btnGetMyWorkspaces.Location = new System.Drawing.Point(463, 11);
            this.btnGetMyWorkspaces.Name = "btnGetMyWorkspaces";
            this.btnGetMyWorkspaces.Size = new System.Drawing.Size(127, 23);
            this.btnGetMyWorkspaces.TabIndex = 3;
            this.btnGetMyWorkspaces.Text = "Get MyWorkspaces";
            this.btnGetMyWorkspaces.UseVisualStyleBackColor = true;
            this.btnGetMyWorkspaces.Click += new System.EventHandler(this.btnGetMyWorkspaces_Click);
            // 
            // dgViewMyWorkspaces
            // 
            this.dgViewMyWorkspaces.AllowUserToAddRows = false;
            this.dgViewMyWorkspaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgViewMyWorkspaces.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgViewMyWorkspaces.ColumnHeadersHeight = 21;
            this.dgViewMyWorkspaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgViewMyWorkspaces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.WorkspaceName,
            this.User,
            this.State,
            this.CapacityId});
            this.dgViewMyWorkspaces.Location = new System.Drawing.Point(6, 73);
            this.dgViewMyWorkspaces.Name = "dgViewMyWorkspaces";
            this.dgViewMyWorkspaces.ReadOnly = true;
            this.dgViewMyWorkspaces.Size = new System.Drawing.Size(1023, 511);
            this.dgViewMyWorkspaces.TabIndex = 4;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 220;
            // 
            // WorkspaceName
            // 
            this.WorkspaceName.HeaderText = "Workspace Name";
            this.WorkspaceName.Name = "WorkspaceName";
            this.WorkspaceName.ReadOnly = true;
            this.WorkspaceName.Width = 255;
            // 
            // User
            // 
            this.User.HeaderText = "User";
            this.User.Name = "User";
            this.User.ReadOnly = true;
            this.User.Width = 205;
            // 
            // State
            // 
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            this.State.Width = 80;
            // 
            // CapacityId
            // 
            this.CapacityId.HeaderText = "CapacityId";
            this.CapacityId.Name = "CapacityId";
            this.CapacityId.ReadOnly = true;
            this.CapacityId.Width = 220;
            // 
            // cbCapacities
            // 
            this.cbCapacities.FormattingEnabled = true;
            this.cbCapacities.Items.AddRange(new object[] {
            "00000000-0000-0000-0000-000000000000"});
            this.cbCapacities.Location = new System.Drawing.Point(83, 39);
            this.cbCapacities.Name = "cbCapacities";
            this.cbCapacities.Size = new System.Drawing.Size(269, 21);
            this.cbCapacities.TabIndex = 5;
            this.cbCapacities.SelectedValueChanged += new System.EventHandler(this.cbCapacities_SelectedValueChanged);
            // 
            // lblCapacities
            // 
            this.lblCapacities.AutoSize = true;
            this.lblCapacities.Location = new System.Drawing.Point(3, 42);
            this.lblCapacities.Name = "lblCapacities";
            this.lblCapacities.Size = new System.Drawing.Size(59, 13);
            this.lblCapacities.TabIndex = 6;
            this.lblCapacities.Text = "Capacities:";
            // 
            // btnAssignToCapacity
            // 
            this.btnAssignToCapacity.Location = new System.Drawing.Point(371, 40);
            this.btnAssignToCapacity.Name = "btnAssignToCapacity";
            this.btnAssignToCapacity.Size = new System.Drawing.Size(219, 23);
            this.btnAssignToCapacity.TabIndex = 7;
            this.btnAssignToCapacity.Text = "Assign Selected To Capacity";
            this.btnAssignToCapacity.UseVisualStyleBackColor = true;
            this.btnAssignToCapacity.Click += new System.EventHandler(this.btnAssignToCapacity_Click);
            // 
            // tbCapacityInfo
            // 
            this.tbCapacityInfo.Location = new System.Drawing.Point(608, 12);
            this.tbCapacityInfo.Multiline = true;
            this.tbCapacityInfo.Name = "tbCapacityInfo";
            this.tbCapacityInfo.ReadOnly = true;
            this.tbCapacityInfo.Size = new System.Drawing.Size(421, 51);
            this.tbCapacityInfo.TabIndex = 8;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 590);
            this.Controls.Add(this.tbCapacityInfo);
            this.Controls.Add(this.btnAssignToCapacity);
            this.Controls.Add(this.lblCapacities);
            this.Controls.Add(this.cbCapacities);
            this.Controls.Add(this.dgViewMyWorkspaces);
            this.Controls.Add(this.btnGetMyWorkspaces);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.tbTenantId);
            this.Controls.Add(this.lblTenantId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1050, 1000);
            this.MinimumSize = new System.Drawing.Size(1050, 350);
            this.Name = "formMain";
            this.Text = "Power BI Workspace Capacity Manager Tool (Copyright © 2022 Sergei Gundorov)";
            ((System.ComponentModel.ISupportInitialize)(this.dgViewMyWorkspaces)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTenantId;
        private System.Windows.Forms.TextBox tbTenantId;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Button btnGetMyWorkspaces;
        private System.Windows.Forms.DataGridView dgViewMyWorkspaces;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkspaceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapacityId;
        private System.Windows.Forms.ComboBox cbCapacities;
        private System.Windows.Forms.Label lblCapacities;
        private System.Windows.Forms.Button btnAssignToCapacity;
        private System.Windows.Forms.TextBox tbCapacityInfo;
    }
}

