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
        Me.components = New System.ComponentModel.Container()
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource3 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemsForm))
        Me.OrderItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SeedDataSet = New SeedApp.SeedDataSet()
        Me.SeedOrderBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SeedOrderDetailBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ItemsDataGridView = New System.Windows.Forms.DataGridView()
        Me.OrdersPage = New System.Windows.Forms.TabControl()
        Me.OrderTabPage = New System.Windows.Forms.TabPage()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.OrderStatusCB = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TotalPricePerAcreTB = New System.Windows.Forms.TextBox()
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
        Me.EditItemsCB = New System.Windows.Forms.CheckBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ItemsSearchTB = New System.Windows.Forms.TextBox()
        Me.CustomerTabPage = New System.Windows.Forms.TabPage()
        Me.CustomerSearchBtn = New System.Windows.Forms.Button()
        Me.CustomerSearchTB = New System.Windows.Forms.TextBox()
        Me.EditCustomersCB = New System.Windows.Forms.CheckBox()
        Me.CustomerDataGridView = New System.Windows.Forms.DataGridView()
        Me.OrdersTabPage = New System.Windows.Forms.TabPage()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.OrdersSearchTB = New System.Windows.Forms.TextBox()
        Me.OrdersGridView = New System.Windows.Forms.DataGridView()
        Me.ReportsTabPage = New System.Windows.Forms.TabPage()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.ReportsCB = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ReportViewer2 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.OrderItemsTableAdapter = New SeedApp.SeedDataSetTableAdapters.OrderItemsTableAdapter()
        Me.SeedOrderTableAdapter = New SeedApp.SeedDataSetTableAdapters.SeedOrderTableAdapter()
        Me.SeedOrderDetailTableAdapter = New SeedApp.SeedDataSetTableAdapters.SeedOrderDetailTableAdapter()
        CType(Me.OrderItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SeedDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SeedOrderBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SeedOrderDetailBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OrdersPage.SuspendLayout()
        Me.OrderTabPage.SuspendLayout()
        CType(Me.OrderItemsGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ItemTabPage.SuspendLayout()
        Me.CustomerTabPage.SuspendLayout()
        CType(Me.CustomerDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OrdersTabPage.SuspendLayout()
        CType(Me.OrdersGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ReportsTabPage.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OrderItemsBindingSource
        '
        Me.OrderItemsBindingSource.DataMember = "OrderItems"
        Me.OrderItemsBindingSource.DataSource = Me.SeedDataSet
        '
        'SeedDataSet
        '
        Me.SeedDataSet.DataSetName = "SeedDataSet"
        Me.SeedDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SeedOrderBindingSource
        '
        Me.SeedOrderBindingSource.DataMember = "SeedOrder"
        Me.SeedOrderBindingSource.DataSource = Me.SeedDataSet
        '
        'SeedOrderDetailBindingSource
        '
        Me.SeedOrderDetailBindingSource.DataMember = "SeedOrderDetail"
        Me.SeedOrderDetailBindingSource.DataSource = Me.SeedDataSet
        '
        'ItemsDataGridView
        '
        Me.ItemsDataGridView.AllowUserToDeleteRows = False
        Me.ItemsDataGridView.AllowUserToOrderColumns = True
        Me.ItemsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.ItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ItemsDataGridView.Location = New System.Drawing.Point(3, 44)
        Me.ItemsDataGridView.Name = "ItemsDataGridView"
        Me.ItemsDataGridView.Size = New System.Drawing.Size(1581, 718)
        Me.ItemsDataGridView.TabIndex = 0
        '
        'OrdersPage
        '
        Me.OrdersPage.Controls.Add(Me.OrderTabPage)
        Me.OrdersPage.Controls.Add(Me.ItemTabPage)
        Me.OrdersPage.Controls.Add(Me.CustomerTabPage)
        Me.OrdersPage.Controls.Add(Me.OrdersTabPage)
        Me.OrdersPage.Controls.Add(Me.ReportsTabPage)
        Me.OrdersPage.Location = New System.Drawing.Point(12, 12)
        Me.OrdersPage.Name = "OrdersPage"
        Me.OrdersPage.SelectedIndex = 0
        Me.OrdersPage.Size = New System.Drawing.Size(1664, 823)
        Me.OrdersPage.TabIndex = 1
        '
        'OrderTabPage
        '
        Me.OrderTabPage.AutoScroll = True
        Me.OrderTabPage.Controls.Add(Me.Button6)
        Me.OrderTabPage.Controls.Add(Me.OrderStatusCB)
        Me.OrderTabPage.Controls.Add(Me.Label6)
        Me.OrderTabPage.Controls.Add(Me.TotalPricePerAcreTB)
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
        Me.OrderTabPage.Size = New System.Drawing.Size(1656, 797)
        Me.OrderTabPage.TabIndex = 0
        Me.OrderTabPage.Text = "Order"
        Me.OrderTabPage.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(770, 12)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(172, 23)
        Me.Button6.TabIndex = 18
        Me.Button6.Text = "Create Invoice"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'OrderStatusCB
        '
        Me.OrderStatusCB.FormattingEnabled = True
        Me.OrderStatusCB.Location = New System.Drawing.Point(374, 81)
        Me.OrderStatusCB.Name = "OrderStatusCB"
        Me.OrderStatusCB.Size = New System.Drawing.Size(200, 21)
        Me.OrderStatusCB.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(54, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Total Price Per Acre"
        Me.Label6.UseWaitCursor = True
        '
        'TotalPricePerAcreTB
        '
        Me.TotalPricePerAcreTB.Location = New System.Drawing.Point(162, 136)
        Me.TotalPricePerAcreTB.Name = "TotalPricePerAcreTB"
        Me.TotalPricePerAcreTB.Size = New System.Drawing.Size(156, 20)
        Me.TotalPricePerAcreTB.TabIndex = 15
        Me.TotalPricePerAcreTB.Text = "0.00"
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
        Me.ProjectTB.Location = New System.Drawing.Point(162, 85)
        Me.ProjectTB.Name = "ProjectTB"
        Me.ProjectTB.Size = New System.Drawing.Size(156, 20)
        Me.ProjectTB.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(54, 158)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Total Price"
        Me.Label4.UseWaitCursor = True
        '
        'OrderTotalTB
        '
        Me.OrderTotalTB.Location = New System.Drawing.Point(162, 162)
        Me.OrderTotalTB.Name = "OrderTotalTB"
        Me.OrderTotalTB.Size = New System.Drawing.Size(156, 20)
        Me.OrderTotalTB.TabIndex = 10
        Me.OrderTotalTB.Text = "0.00"
        '
        'OrderIDTB
        '
        Me.OrderIDTB.Location = New System.Drawing.Point(374, 54)
        Me.OrderIDTB.Name = "OrderIDTB"
        Me.OrderIDTB.Size = New System.Drawing.Size(200, 20)
        Me.OrderIDTB.TabIndex = 9
        '
        'NewBtn
        '
        Me.NewBtn.Location = New System.Drawing.Point(152, 771)
        Me.NewBtn.Name = "NewBtn"
        Me.NewBtn.Size = New System.Drawing.Size(117, 23)
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
        Me.AcresTB.Location = New System.Drawing.Point(162, 111)
        Me.AcresTB.Name = "AcresTB"
        Me.AcresTB.Size = New System.Drawing.Size(156, 20)
        Me.AcresTB.TabIndex = 6
        Me.AcresTB.Text = "0.00"
        '
        'OrderItemsGridView
        '
        Me.OrderItemsGridView.AllowUserToAddRows = False
        Me.OrderItemsGridView.AllowUserToOrderColumns = True
        Me.OrderItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.OrderItemsGridView.Location = New System.Drawing.Point(12, 201)
        Me.OrderItemsGridView.Name = "OrderItemsGridView"
        Me.OrderItemsGridView.Size = New System.Drawing.Size(1581, 561)
        Me.OrderItemsGridView.TabIndex = 5
        '
        'SaveOrderBtn
        '
        Me.SaveOrderBtn.Location = New System.Drawing.Point(14, 771)
        Me.SaveOrderBtn.Name = "SaveOrderBtn"
        Me.SaveOrderBtn.Size = New System.Drawing.Size(117, 23)
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
        Me.InvoiceTB.Location = New System.Drawing.Point(162, 27)
        Me.InvoiceTB.Name = "InvoiceTB"
        Me.InvoiceTB.Size = New System.Drawing.Size(156, 20)
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
        Me.CustomerCB.Location = New System.Drawing.Point(162, 58)
        Me.CustomerCB.Name = "CustomerCB"
        Me.CustomerCB.Size = New System.Drawing.Size(156, 21)
        Me.CustomerCB.TabIndex = 0
        '
        'ItemTabPage
        '
        Me.ItemTabPage.Controls.Add(Me.EditItemsCB)
        Me.ItemTabPage.Controls.Add(Me.Button2)
        Me.ItemTabPage.Controls.Add(Me.Button3)
        Me.ItemTabPage.Controls.Add(Me.ItemsSearchTB)
        Me.ItemTabPage.Controls.Add(Me.ItemsDataGridView)
        Me.ItemTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ItemTabPage.Name = "ItemTabPage"
        Me.ItemTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ItemTabPage.Size = New System.Drawing.Size(1656, 797)
        Me.ItemTabPage.TabIndex = 1
        Me.ItemTabPage.Text = "Items"
        Me.ItemTabPage.UseVisualStyleBackColor = True
        '
        'EditItemsCB
        '
        Me.EditItemsCB.Appearance = System.Windows.Forms.Appearance.Button
        Me.EditItemsCB.AutoSize = True
        Me.EditItemsCB.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.EditItemsCB.Location = New System.Drawing.Point(1147, 8)
        Me.EditItemsCB.Name = "EditItemsCB"
        Me.EditItemsCB.Size = New System.Drawing.Size(35, 23)
        Me.EditItemsCB.TabIndex = 6
        Me.EditItemsCB.Text = "Edit"
        Me.EditItemsCB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.EditItemsCB.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(6, 768)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(131, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Add Items To Order"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(267, 18)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(117, 23)
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
        'CustomerTabPage
        '
        Me.CustomerTabPage.Controls.Add(Me.CustomerSearchBtn)
        Me.CustomerTabPage.Controls.Add(Me.CustomerSearchTB)
        Me.CustomerTabPage.Controls.Add(Me.EditCustomersCB)
        Me.CustomerTabPage.Controls.Add(Me.CustomerDataGridView)
        Me.CustomerTabPage.Location = New System.Drawing.Point(4, 22)
        Me.CustomerTabPage.Name = "CustomerTabPage"
        Me.CustomerTabPage.Size = New System.Drawing.Size(1656, 797)
        Me.CustomerTabPage.TabIndex = 2
        Me.CustomerTabPage.Text = "Customers"
        Me.CustomerTabPage.UseVisualStyleBackColor = True
        '
        'CustomerSearchBtn
        '
        Me.CustomerSearchBtn.Location = New System.Drawing.Point(267, 18)
        Me.CustomerSearchBtn.Name = "CustomerSearchBtn"
        Me.CustomerSearchBtn.Size = New System.Drawing.Size(117, 23)
        Me.CustomerSearchBtn.TabIndex = 9
        Me.CustomerSearchBtn.Text = "Search"
        Me.CustomerSearchBtn.UseVisualStyleBackColor = True
        '
        'CustomerSearchTB
        '
        Me.CustomerSearchTB.Location = New System.Drawing.Point(6, 18)
        Me.CustomerSearchTB.Name = "CustomerSearchTB"
        Me.CustomerSearchTB.Size = New System.Drawing.Size(255, 20)
        Me.CustomerSearchTB.TabIndex = 8
        '
        'EditCustomersCB
        '
        Me.EditCustomersCB.Appearance = System.Windows.Forms.Appearance.Button
        Me.EditCustomersCB.AutoSize = True
        Me.EditCustomersCB.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.EditCustomersCB.Location = New System.Drawing.Point(1147, 8)
        Me.EditCustomersCB.Name = "EditCustomersCB"
        Me.EditCustomersCB.Size = New System.Drawing.Size(35, 23)
        Me.EditCustomersCB.TabIndex = 7
        Me.EditCustomersCB.Text = "Edit"
        Me.EditCustomersCB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.EditCustomersCB.UseVisualStyleBackColor = True
        '
        'CustomerDataGridView
        '
        Me.CustomerDataGridView.AllowUserToDeleteRows = False
        Me.CustomerDataGridView.AllowUserToOrderColumns = True
        Me.CustomerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CustomerDataGridView.Location = New System.Drawing.Point(3, 44)
        Me.CustomerDataGridView.Name = "CustomerDataGridView"
        Me.CustomerDataGridView.Size = New System.Drawing.Size(1581, 750)
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
        Me.OrdersTabPage.Size = New System.Drawing.Size(1656, 797)
        Me.OrdersTabPage.TabIndex = 3
        Me.OrdersTabPage.Text = "Orders"
        Me.OrdersTabPage.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(6, 768)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(131, 23)
        Me.Button4.TabIndex = 12
        Me.Button4.Text = "View Order"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(267, 18)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(117, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'OrdersSearchTB
        '
        Me.OrdersSearchTB.Location = New System.Drawing.Point(6, 18)
        Me.OrdersSearchTB.Name = "OrdersSearchTB"
        Me.OrdersSearchTB.Size = New System.Drawing.Size(255, 20)
        Me.OrdersSearchTB.TabIndex = 10
        '
        'OrdersGridView
        '
        Me.OrdersGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.OrdersGridView.Location = New System.Drawing.Point(3, 44)
        Me.OrdersGridView.MultiSelect = False
        Me.OrdersGridView.Name = "OrdersGridView"
        Me.OrdersGridView.ReadOnly = True
        Me.OrdersGridView.Size = New System.Drawing.Size(1581, 718)
        Me.OrdersGridView.TabIndex = 0
        '
        'ReportsTabPage
        '
        Me.ReportsTabPage.Controls.Add(Me.Button5)
        Me.ReportsTabPage.Controls.Add(Me.ReportsCB)
        Me.ReportsTabPage.Controls.Add(Me.Panel1)
        Me.ReportsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ReportsTabPage.Name = "ReportsTabPage"
        Me.ReportsTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ReportsTabPage.Size = New System.Drawing.Size(1656, 797)
        Me.ReportsTabPage.TabIndex = 4
        Me.ReportsTabPage.Text = "Reports"
        Me.ReportsTabPage.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(267, 18)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(117, 23)
        Me.Button5.TabIndex = 2
        Me.Button5.Text = "View Report"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'ReportsCB
        '
        Me.ReportsCB.FormattingEnabled = True
        Me.ReportsCB.Location = New System.Drawing.Point(6, 18)
        Me.ReportsCB.Name = "ReportsCB"
        Me.ReportsCB.Size = New System.Drawing.Size(255, 21)
        Me.ReportsCB.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ReportViewer2)
        Me.Panel1.Location = New System.Drawing.Point(0, 60)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1581, 639)
        Me.Panel1.TabIndex = 0
        '
        'ReportViewer2
        '
        ReportDataSource1.Name = "OrderItems"
        ReportDataSource1.Value = Me.OrderItemsBindingSource
        ReportDataSource2.Name = "SeedOrder"
        ReportDataSource2.Value = Me.SeedOrderBindingSource
        ReportDataSource3.Name = "SeedOrderDetail"
        ReportDataSource3.Value = Me.SeedOrderDetailBindingSource
        Me.ReportViewer2.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer2.LocalReport.DataSources.Add(ReportDataSource2)
        Me.ReportViewer2.LocalReport.DataSources.Add(ReportDataSource3)
        Me.ReportViewer2.LocalReport.ReportEmbeddedResource = "SeedApp.Bag.rdlc"
        Me.ReportViewer2.Location = New System.Drawing.Point(0, 20)
        Me.ReportViewer2.Name = "ReportViewer2"
        Me.ReportViewer2.Size = New System.Drawing.Size(1581, 619)
        Me.ReportViewer2.TabIndex = 0
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(396, 246)
        Me.ReportViewer1.TabIndex = 0
        '
        'OrderItemsTableAdapter
        '
        Me.OrderItemsTableAdapter.ClearBeforeFill = True
        '
        'SeedOrderTableAdapter
        '
        Me.SeedOrderTableAdapter.ClearBeforeFill = True
        '
        'SeedOrderDetailTableAdapter
        '
        Me.SeedOrderDetailTableAdapter.ClearBeforeFill = True
        '
        'ItemsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(1688, 874)
        Me.Controls.Add(Me.OrdersPage)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ItemsForm"
        Me.Text = "Miller Seed Company"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.OrderItemsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SeedDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SeedOrderBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SeedOrderDetailBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.ReportsTabPage.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
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
    Friend WithEvents EditItemsCB As CheckBox
    Friend WithEvents EditCustomersCB As CheckBox
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
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ReportViewer2 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents OrderItemsBindingSource As BindingSource
    Friend WithEvents SeedDataSet As SeedDataSet
    Friend WithEvents SeedOrderBindingSource As BindingSource
    Friend WithEvents SeedOrderDetailBindingSource As BindingSource
    Friend WithEvents OrderItemsTableAdapter As SeedDataSetTableAdapters.OrderItemsTableAdapter
    Friend WithEvents SeedOrderTableAdapter As SeedDataSetTableAdapters.SeedOrderTableAdapter
    Friend WithEvents SeedOrderDetailTableAdapter As SeedDataSetTableAdapters.SeedOrderDetailTableAdapter
    Friend WithEvents Button5 As Button
    Friend WithEvents ReportsCB As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TotalPricePerAcreTB As TextBox
    Friend WithEvents OrderStatusCB As ComboBox
    Friend WithEvents Button6 As Button
End Class
