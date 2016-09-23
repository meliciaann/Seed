<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MultiCustomersForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CustomerListGV = New System.Windows.Forms.DataGridView()
        CType(Me.CustomerListGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CustomerListGV
        '
        Me.CustomerListGV.AllowUserToAddRows = False
        Me.CustomerListGV.AllowUserToDeleteRows = False
        Me.CustomerListGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CustomerListGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CustomerListGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.CustomerListGV.Location = New System.Drawing.Point(0, 0)
        Me.CustomerListGV.MultiSelect = False
        Me.CustomerListGV.Name = "CustomerListGV"
        Me.CustomerListGV.ReadOnly = True
        Me.CustomerListGV.Size = New System.Drawing.Size(647, 230)
        Me.CustomerListGV.TabIndex = 0
        '
        'MultiCustomersForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(647, 230)
        Me.Controls.Add(Me.CustomerListGV)
        Me.Name = "MultiCustomersForm"
        Me.Text = "Choose Customer"
        CType(Me.CustomerListGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CustomerListGV As System.Windows.Forms.DataGridView
End Class
