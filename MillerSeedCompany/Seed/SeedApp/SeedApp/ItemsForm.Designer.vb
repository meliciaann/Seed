<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ItemsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ItemsDataGridView = New System.Windows.Forms.DataGridView()
        Me.OrdersPage = New System.Windows.Forms.TabControl()
        Me.OrderTabPage = New System.Windows.Forms.TabPage()
        Me.OrderDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ProjectTB = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.OrderTotalTB = New System.Windows.Forms.TextBox()
        Me.OrderIDTB = New System.Windows.Forms.TextBox()
        Me.NewBtn = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AcresTB = New System.Windows.Forms.TextBox()
        Me.OrderItemsGridView = New System.Windows.Forms.DataGridView()
        Me.SaveOrderBtn = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.InvoiceTB = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CustomerCB = New System.Windows.Forms.ComboBox()
        Me.ItemTabPage = New System.Windows.Forms.TabPage()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ItemsSearchTB = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.CustomerTabPage = New System.Windows.Forms.TabPage()
        Me.CustomerSearchBtn = New System.Windows.Forms.Button()
        Me.CustomerSearchTB = New System.Windows.Forms.TextBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CustomerDataGridView = New System.Windows.Forms.DataGridView()
        Me.OrdersTabPage = New System.Windows.Forms.TabPage()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.OrdersSearchTB = New System.Windows.Forms.TextBox()
        Me.OrdersGridView = New System.Windows.Forms.DataGridView()
        Me.ReportsTabPage = New System.Windows.Forms.TabPage()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        CType(Me.ItemsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OrdersPage.SuspendLayout()
        Me.OrderTabPage.SuspendLayout()
        CType(Me.OrderItemsGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ItemTabPage.SuspendLayout()
        Me.CustomerTabPage.SuspendLayout()
        CType(Me.CustomerDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OrdersTabPage.SuspendLayout()
        CType(Me.OrdersGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ItemsDataGridView
        '
        Me.ItemsDataGridView.AllowUserToDeleteRows = False
        Me.ItemsDataGridView.AllowUserToOrderColumns = True
        Me.ItemsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.ItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ItemsDataGridView.Location = New System.Drawing.Point(3, 44)
        Me.ItemsDataGridView.Name = "ItemsDataGridView"
        Me.ItemsDataGridView.Size = New System.Drawing.Size(1217, 467)
        Me.ItemsDataGridView.TabIndex = 0
        '
        'OrdersPage
        '
        Me.OrdersPage.Controls.Add(Me.OrderTabPage)
        Me.OrdersPage.Controls.Add(Me.ItemTabPage)
        Me.OrdersPage.Controls.Add(Me.CustomerTabPage)
        Me.OrdersPage.Controls.Add(Me.OrdersTabPage)
        Me.OrdersPage.Controls.Add(Me.ReportsTabPage)
        Me.OrdersPage.Location = New System.Drawing.Point(12, 48)
        Me.OrdersPage.Name = "OrdersPage"
        Me.OrdersPage.SelectedIndex = 0
        Me.OrdersPage.Size = New System.Drawing.Size(1231, 581)
        Me.OrdersPage.TabIndex = 1
        '
        'OrderTabPage
        '
        Me.OrderTabPage.Controls.Add(Me.OrderDatePicker)
        Me.OrderTabPage.Controls.Add(Me.Label5)
        Me.OrderTabPage.Controls.Add(Me.ProjectTB)
        Me.OrderTabPage.Controls.Add(Me.Label4)
        Me.OrderTabPage.Controls.Add(Me.OrderTotalTB)
        Me.OrderTabPage.Controls.Add(Me.OrderIDTB)
        Me.OrderTabPage.Controls.Add(Me.NewBtn)
        Me.OrderTabPage.Controls.Add(Me.Label3)
        Me.OrderTabPage.Controls.Add(Me.AcresTB)
        Me.OrderTabPage.Controls.Add(Me.OrderItemsGridView)
        Me.OrderTabPage.Controls.Add(Me.SaveOrderBtn)
        Me.OrderTabPage.Controls.Add(Me.Label2)
        Me.OrderTabPage.Controls.Add(Me.InvoiceTB)
        Me.OrderTabPage.Controls.Add(Me.Label1)
        Me.OrderTabPage.Controls.Add(Me.CustomerCB)
        Me.OrderTabPage.Location = New System.Drawing.Point(4, 22)
        Me.OrderTabPage.Name = "OrderTabPage"
        Me.OrderTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.OrderTabPage.Size = New System.Drawing.Size(1223, 555)
        Me.OrderTabPage.TabIndex = 0
        Me.OrderTabPage.Text = "Order"
        Me.OrderTabPage.UseVisualStyleBackColor = True
        '
        'OrderDatePicker
        '
        Me.OrderDatePicker.Location = New System.Drawing.Point(374, 23)
        Me.OrderDatePicker.Name = "OrderDatePicker"
        Me.OrderDatePicker.Size = New System.Drawing.Size(200, 20)
        Me.OrderDatePicker.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(54, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Project"
        Me.Label5.UseWaitCursor = True
        '
        'ProjectTB
        '
        Me.ProjectTB.AcceptsReturn = True
        Me.ProjectTB.Location = New System.Drawing.Point(122, 81)
        Me.ProjectTB.Name = "ProjectTB"
        Me.ProjectTB.Size = New System.Drawing.Size(196, 20)
        Me.ProjectTB.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(54, 133)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Order Total"
        Me.Label4.UseWaitCursor = True
        '
        'OrderTotalTB
        '
        Me.OrderTotalTB.Location = New System.Drawing.Point(122, 133)
        Me.OrderTotalTB.Name = "OrderTotalTB"
        Me.OrderTotalTB.Size = New System.Drawing.Size(196, 20)
        Me.OrderTotalTB.TabIndex = 10
        Me.OrderTotalTB.Text = "0.00"
        '
        'OrderIDTB
        '
        Me.OrderIDTB.Location = New System.Drawing.Point(1123, 0)
        Me.OrderIDTB.Name = "OrderIDTB"
        Me.OrderIDTB.Size = New System.Drawing.Size(100, 20)
        Me.OrderIDTB.TabIndex = 9
        '
        'NewBtn
        '
        Me.NewBtn.Location = New System.Drawing.Point(195, 516)
        Me.NewBtn.Name = "NewBtn"
        Me.NewBtn.Size = New System.Drawing.Size(123, 23)
        Me.NewBtn.TabIndex = 8
        Me.NewBtn.Text = "New Order"
        Me.NewBtn.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(54, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Acres"
        Me.Label3.UseWaitCursor = True
        '
        'AcresTB
        '
        Me.AcresTB.Location = New System.Drawing.Point(122, 107)
        Me.AcresTB.Name = "AcresTB"
        Me.AcresTB.Size = New System.Drawing.Size(196, 20)
        Me.AcresTB.TabIndex = 6
        Me.AcresTB.Text = "0.00"
        '
        'OrderItemsGridView
        '
        Me.OrderItemsGridView.AllowUserToAddRows = False
        Me.OrderItemsGridView.AllowUserToOrderColumns = True
        Me.OrderItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.OrderItemsGridView.Location = New System.Drawing.Point(57, 190)
        Me.OrderItemsGridView.Name = "OrderItemsGridView"
        Me.OrderItemsGridView.Size = New System.Drawing.Size(976, 305)
        Me.OrderItemsGridView.TabIndex = 5
        '
        'SaveOrderBtn
        '
        Me.SaveOrderBtn.Location = New System.Drawing.Point(57, 516)
        Me.SaveOrderBtn.Name = "SaveOrderBtn"
        Me.SaveOrderBtn.Size = New System.Drawing.Size(120, 23)
        Me.SaveOrderBtn.TabIndex = 4
        Me.SaveOrderBtn.Text = "Save Order"
        Me.SaveOrderBtn.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(54, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Invoice #"
        '
        'InvoiceTB
        '
        Me.InvoiceTB.Location = New System.Drawing.Point(122, 23)
        Me.InvoiceTB.Name = "InvoiceTB"
        Me.InvoiceTB.Size = New System.Drawing.Size(196, 20)
        Me.InvoiceTB.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(54, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Customer"
        '
        'CustomerCB
        '
        Me.CustomerCB.FormattingEnabled = True
        Me.CustomerCB.Location = New System.Drawing.Point(122, 54)
        Me.CustomerCB.Name = "CustomerCB"
        Me.CustomerCB.Size = New System.Drawing.Size(196, 21)
        Me.CustomerCB.TabIndex = 0
        '
        'ItemTabPage
        '
        Me.ItemTabPage.Controls.Add(Me.CheckBox1)
        Me.ItemTabPage.Controls.Add(Me.Button3)
        Me.ItemTabPage.Controls.Add(Me.ItemsSearchTB)
        Me.ItemTabPage.Controls.Add(Me.Button2)
        Me.ItemTabPage.Controls.Add(Me.ItemsDataGridView)
        Me.ItemTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ItemTabPage.Name = "ItemTabPage"
        Me.ItemTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ItemTabPage.Size = New System.Drawing.Size(1223, 555)
        Me.ItemTabPage.TabIndex = 1
        Me.ItemTabPage.Text = "Items"
        Me.ItemTabPage.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(1181, 15)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(35, 23)
        Me.CheckBox1.TabIndex = 6
        Me.CheckBox1.Text = "Edit"
        Me.CheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(268, 18)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "Search"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ItemsSearchTB
        '
        Me.ItemsSearchTB.Location = New System.Drawing.Point(6, 18)
        Me.ItemsSearchTB.Name = "ItemsSearchTB"
        Me.ItemsSearchTB.Size = New System.Drawing.Size(255, 20)
        Me.ItemsSearchTB.TabIndex = 4
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(6, 525)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(131, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Add Items To Order"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'CustomerTabPage
        '
        Me.CustomerTabPage.Controls.Add(Me.CustomerSearchBtn)
        Me.CustomerTabPage.Controls.Add(Me.CustomerSearchTB)
        Me.CustomerTabPage.Controls.Add(Me.CheckBox2)
        Me.CustomerTabPage.Controls.Add(Me.CustomerDataGridView)
        Me.CustomerTabPage.Location = New System.Drawing.Point(4, 22)
        Me.CustomerTabPage.Name = "CustomerTabPage"
        Me.CustomerTabPage.Size = New System.Drawing.Size(1223, 555)
        Me.CustomerTabPage.TabIndex = 2
        Me.CustomerTabPage.Text = "Customers"
        Me.CustomerTabPage.UseVisualStyleBackColor = True
        '
        'CustomerSearchBtn
        '
        Me.CustomerSearchBtn.Location = New System.Drawing.Point(265, 11)
        Me.CustomerSearchBtn.Name = "CustomerSearchBtn"
        Me.CustomerSearchBtn.Size = New System.Drawing.Size(75, 23)
        Me.CustomerSearchBtn.TabIndex = 9
        Me.CustomerSearchBtn.Text = "Search"
        Me.CustomerSearchBtn.UseVisualStyleBackColor = True
        '
        'CustomerSearchTB
        '
        Me.CustomerSearchTB.Location = New System.Drawing.Point(3, 11)
        Me.CustomerSearchTB.Name = "CustomerSearchTB"
        Me.CustomerSearchTB.Size = New System.Drawing.Size(255, 20)
        Me.CustomerSearchTB.TabIndex = 8
        '
        'CheckBox2
        '
        Me.CheckBox2.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(1185, 8)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(35, 23)
        Me.CheckBox2.TabIndex = 7
        Me.CheckBox2.Text = "Edit"
        Me.CheckBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CustomerDataGridView
        '
        Me.CustomerDataGridView.AllowUserToDeleteRows = False
        Me.CustomerDataGridView.AllowUserToOrderColumns = True
        Me.CustomerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CustomerDataGridView.Location = New System.Drawing.Point(0, 37)
        Me.CustomerDataGridView.Name = "CustomerDataGridView"
        Me.CustomerDataGridView.Size = New System.Drawing.Size(1223, 518)
        Me.CustomerDataGridView.TabIndex = 0
        '
        'OrdersTabPage
        '
        Me.OrdersTabPage.Controls.Add(Me.Button4)
        Me.OrdersTabPage.Controls.Add(Me.Button1)
        Me.OrdersTabPage.Controls.Add(Me.OrdersSearchTB)
        Me.OrdersTabPage.Controls.Add(Me.OrdersGridView)
        Me.OrdersTabPage.Location = New System.Drawing.Point(4, 22)
        Me.OrdersTabPage.Name = "OrdersTabPage"
        Me.OrdersTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.OrdersTabPage.Size = New System.Drawing.Size(1223, 555)
        Me.OrdersTabPage.TabIndex = 3
        Me.OrdersTabPage.Text = "Orders"
        Me.OrdersTabPage.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(6, 516)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(131, 23)
        Me.Button4.TabIndex = 12
        Me.Button4.Text = "View Order"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(265, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'OrdersSearchTB
        '
        Me.OrdersSearchTB.Location = New System.Drawing.Point(3, 10)
        Me.OrdersSearchTB.Name = "OrdersSearchTB"
        Me.OrdersSearchTB.Size = New System.Drawing.Size(255, 20)
        Me.OrdersSearchTB.TabIndex = 10
        '
        'OrdersGridView
        '
        Me.OrdersGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.OrdersGridView.Location = New System.Drawing.Point(3, 39)
        Me.OrdersGridView.MultiSelect = False
        Me.OrdersGridView.Name = "OrdersGridView"
        Me.OrdersGridView.ReadOnly = True
        Me.OrdersGridView.Size = New System.Drawing.Size(1213, 471)
        Me.OrdersGridView.TabIndex = 0
        '
        'ReportsTabPage
        '
        Me.ReportsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ReportsTabPage.Name = "ReportsTabPage"
        Me.ReportsTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ReportsTabPage.Size = New System.Drawing.Size(1223, 555)
        Me.ReportsTabPage.TabIndex = 4
        Me.ReportsTabPage.Text = "Reports"
        Me.ReportsTabPage.UseVisualStyleBackColor = True
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer"
        Me.ReportViewer1.Size = New System.Drawing.Size(396, 246)
        Me.ReportViewer1.TabIndex = 0
        '
        'ItemsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1244, 630)
        Me.Controls.Add(Me.OrdersPage)
        Me.Name = "ItemsForm"
        Me.Text = "Form1"
        CType(Me.ItemsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.OrdersPage.ResumeLayout(False)
        Me.OrderTabPage.ResumeLayout(False)
        Me.OrderTabPage.PerformLayout()
        CType(Me.OrderItemsGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ItemTabPage.ResumeLayout(False)
        Me.ItemTabPage.PerformLayout()
        Me.CustomerTabPage.ResumeLayout(False)
        Me.CustomerTabPage.PerformLayout()
        CType(Me.CustomerDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.OrdersTabPage.ResumeLayout(False)
        Me.OrdersTabPage.PerformLayout()
        CType(Me.OrdersGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ItemsDataGridView As DataGridView
    Friend WithEvents OrdersPage As TabControl
    Friend WithEvents OrderTabPage As TabPage
    Friend WithEvents ItemTabPage As TabPage
    Friend WithEvents CustomerTabPage As TabPage
    Friend WithEvents CustomerDataGridView As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents CustomerCB As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents InvoiceTB As TextBox
    Friend WithEvents SaveOrderBtn As Button
    Friend WithEvents OrderItemsGridView As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents ItemsSearchTB As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents AcresTB As TextBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents NewBtn As Button
    Friend WithEvents OrderIDTB As TextBox
    Friend WithEvents CustomerSearchBtn As Button
    Friend WithEvents CustomerSearchTB As TextBox
    Friend WithEvents OrdersTabPage As TabPage
    Friend WithEvents Button1 As Button
    Friend WithEvents OrdersSearchTB As TextBox
    Friend WithEvents OrdersGridView As DataGridView
    Friend WithEvents Button4 As Button
    Friend WithEvents OrderTotalTB As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ProjectTB As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents OrderDatePicker As DateTimePicker
    Friend WithEvents ReportsTabPage As TabPage
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
