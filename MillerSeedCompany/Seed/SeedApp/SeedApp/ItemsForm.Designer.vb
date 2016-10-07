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
        Me.CopyOrderBtn = New System.Windows.Forms.Button()
        Me.SaveOrderBtn = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.PriceListCB = New System.Windows.Forms.ComboBox()
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AcresTB = New System.Windows.Forms.TextBox()
        Me.OrderItemsGridView = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.InvoiceTB = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CustomerCB = New System.Windows.Forms.ComboBox()
        Me.ItemTabPage = New System.Windows.Forms.TabPage()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TypeFilterCB = New System.Windows.Forms.ComboBox()
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
        Me.ReportsCheckListBox = New System.Windows.Forms.CheckedListBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ReportViewer2 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.InventoryTP = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CurrentItemAvailableTB = New System.Windows.Forms.TextBox()
        Me.InventoryDGV = New System.Windows.Forms.DataGridView()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.NewBtn = New System.Windows.Forms.Button()
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
        Me.InventoryTP.SuspendLayout()
        CType(Me.InventoryDGV, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ItemsDataGridView.Size = New System.Drawing.Size(1183, 718)
        Me.ItemsDataGridView.TabIndex = 0
        '
        'OrdersPage
        '
        Me.OrdersPage.Controls.Add(Me.OrderTabPage)
        Me.OrdersPage.Controls.Add(Me.ItemTabPage)
        Me.OrdersPage.Controls.Add(Me.CustomerTabPage)
        Me.OrdersPage.Controls.Add(Me.OrdersTabPage)
        Me.OrdersPage.Controls.Add(Me.ReportsTabPage)
        Me.OrdersPage.Controls.Add(Me.InventoryTP)
        Me.OrdersPage.Location = New System.Drawing.Point(12, 25)
        Me.OrdersPage.Name = "OrdersPage"
        Me.OrdersPage.SelectedIndex = 0
        Me.OrdersPage.Size = New System.Drawing.Size(1200, 823)
        Me.OrdersPage.TabIndex = 1
        Me.OrdersPage.TabStop = False
        '
        'OrderTabPage
        '
        Me.OrderTabPage.AutoScroll = True
        Me.OrderTabPage.Controls.Add(Me.CopyOrderBtn)
        Me.OrderTabPage.Controls.Add(Me.SaveOrderBtn)
        Me.OrderTabPage.Controls.Add(Me.Label11)
        Me.OrderTabPage.Controls.Add(Me.Label10)
        Me.OrderTabPage.Controls.Add(Me.Label9)
        Me.OrderTabPage.Controls.Add(Me.Label8)
        Me.OrderTabPage.Controls.Add(Me.PriceListCB)
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
        Me.OrderTabPage.Controls.Add(Me.Label3)
        Me.OrderTabPage.Controls.Add(Me.AcresTB)
        Me.OrderTabPage.Controls.Add(Me.OrderItemsGridView)
        Me.OrderTabPage.Controls.Add(Me.Label2)
        Me.OrderTabPage.Controls.Add(Me.InvoiceTB)
        Me.OrderTabPage.Controls.Add(Me.Label1)
        Me.OrderTabPage.Controls.Add(Me.CustomerCB)
        Me.OrderTabPage.Location = New System.Drawing.Point(4, 22)
        Me.OrderTabPage.Name = "OrderTabPage"
        Me.OrderTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.OrderTabPage.Size = New System.Drawing.Size(1192, 797)
        Me.OrderTabPage.TabIndex = 0
        Me.OrderTabPage.Text = "Order"
        Me.OrderTabPage.UseVisualStyleBackColor = True
        '
        'CopyOrderBtn
        '
        Me.CopyOrderBtn.Location = New System.Drawing.Point(691, 70)
        Me.CopyOrderBtn.Name = "CopyOrderBtn"
        Me.CopyOrderBtn.Size = New System.Drawing.Size(172, 23)
        Me.CopyOrderBtn.TabIndex = 25
        Me.CopyOrderBtn.Text = "Copy Order"
        Me.CopyOrderBtn.UseVisualStyleBackColor = True
        '
        'SaveOrderBtn
        '
        Me.SaveOrderBtn.Location = New System.Drawing.Point(691, 7)
        Me.SaveOrderBtn.Name = "SaveOrderBtn"
        Me.SaveOrderBtn.Size = New System.Drawing.Size(172, 23)
        Me.SaveOrderBtn.TabIndex = 7
        Me.SaveOrderBtn.Text = "Save Order"
        Me.SaveOrderBtn.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(268, 38)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 13)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "Order Status"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(261, 70)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Order Number"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(275, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 13)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "Order Date"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 66)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Price List"
        Me.Label8.UseWaitCursor = True
        '
        'PriceListCB
        '
        Me.PriceListCB.FormattingEnabled = True
        Me.PriceListCB.Items.AddRange(New Object() {"Distributor", "Retail", "Wholesale"})
        Me.PriceListCB.Location = New System.Drawing.Point(72, 63)
        Me.PriceListCB.Name = "PriceListCB"
        Me.PriceListCB.Size = New System.Drawing.Size(156, 21)
        Me.PriceListCB.TabIndex = 2
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(691, 39)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(172, 23)
        Me.Button6.TabIndex = 8
        Me.Button6.Text = "Create Invoice"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'OrderStatusCB
        '
        Me.OrderStatusCB.FormattingEnabled = True
        Me.OrderStatusCB.Location = New System.Drawing.Point(340, 38)
        Me.OrderStatusCB.Name = "OrderStatusCB"
        Me.OrderStatusCB.Size = New System.Drawing.Size(200, 21)
        Me.OrderStatusCB.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(232, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Total Price Per Acre"
        Me.Label6.UseWaitCursor = True
        '
        'TotalPricePerAcreTB
        '
        Me.TotalPricePerAcreTB.Location = New System.Drawing.Point(340, 119)
        Me.TotalPricePerAcreTB.Name = "TotalPricePerAcreTB"
        Me.TotalPricePerAcreTB.Size = New System.Drawing.Size(156, 20)
        Me.TotalPricePerAcreTB.TabIndex = 15
        Me.TotalPricePerAcreTB.TabStop = False
        Me.TotalPricePerAcreTB.Text = "0.00"
        '
        'OrderDatePicker
        '
        Me.OrderDatePicker.Location = New System.Drawing.Point(340, 10)
        Me.OrderDatePicker.Name = "OrderDatePicker"
        Me.OrderDatePicker.Size = New System.Drawing.Size(200, 20)
        Me.OrderDatePicker.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Project"
        Me.Label5.UseWaitCursor = True
        '
        'ProjectTB
        '
        Me.ProjectTB.AcceptsReturn = True
        Me.ProjectTB.Location = New System.Drawing.Point(72, 36)
        Me.ProjectTB.Name = "ProjectTB"
        Me.ProjectTB.Size = New System.Drawing.Size(156, 20)
        Me.ProjectTB.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(276, 148)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Total Price"
        Me.Label4.UseWaitCursor = True
        '
        'OrderTotalTB
        '
        Me.OrderTotalTB.Location = New System.Drawing.Point(340, 145)
        Me.OrderTotalTB.Name = "OrderTotalTB"
        Me.OrderTotalTB.Size = New System.Drawing.Size(156, 20)
        Me.OrderTotalTB.TabIndex = 10
        Me.OrderTotalTB.TabStop = False
        Me.OrderTotalTB.Text = "0.00"
        '
        'OrderIDTB
        '
        Me.OrderIDTB.Location = New System.Drawing.Point(340, 67)
        Me.OrderIDTB.Name = "OrderIDTB"
        Me.OrderIDTB.Size = New System.Drawing.Size(200, 20)
        Me.OrderIDTB.TabIndex = 9
        Me.OrderIDTB.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Acres"
        Me.Label3.UseWaitCursor = True
        '
        'AcresTB
        '
        Me.AcresTB.Location = New System.Drawing.Point(72, 90)
        Me.AcresTB.Name = "AcresTB"
        Me.AcresTB.Size = New System.Drawing.Size(156, 20)
        Me.AcresTB.TabIndex = 3
        Me.AcresTB.Text = "0.00"
        '
        'OrderItemsGridView
        '
        Me.OrderItemsGridView.AllowUserToAddRows = False
        Me.OrderItemsGridView.AllowUserToOrderColumns = True
        Me.OrderItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.OrderItemsGridView.Location = New System.Drawing.Point(12, 201)
        Me.OrderItemsGridView.MultiSelect = False
        Me.OrderItemsGridView.Name = "OrderItemsGridView"
        Me.OrderItemsGridView.Size = New System.Drawing.Size(1172, 385)
        Me.OrderItemsGridView.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(282, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Invoice #"
        '
        'InvoiceTB
        '
        Me.InvoiceTB.Location = New System.Drawing.Point(340, 93)
        Me.InvoiceTB.Name = "InvoiceTB"
        Me.InvoiceTB.Size = New System.Drawing.Size(156, 20)
        Me.InvoiceTB.TabIndex = 2
        Me.InvoiceTB.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Customer"
        '
        'CustomerCB
        '
        Me.CustomerCB.FormattingEnabled = True
        Me.CustomerCB.Location = New System.Drawing.Point(72, 9)
        Me.CustomerCB.Name = "CustomerCB"
        Me.CustomerCB.Size = New System.Drawing.Size(156, 21)
        Me.CustomerCB.TabIndex = 0
        '
        'ItemTabPage
        '
        Me.ItemTabPage.Controls.Add(Me.Label12)
        Me.ItemTabPage.Controls.Add(Me.TypeFilterCB)
        Me.ItemTabPage.Controls.Add(Me.EditItemsCB)
        Me.ItemTabPage.Controls.Add(Me.Button2)
        Me.ItemTabPage.Controls.Add(Me.Button3)
        Me.ItemTabPage.Controls.Add(Me.ItemsSearchTB)
        Me.ItemTabPage.Controls.Add(Me.ItemsDataGridView)
        Me.ItemTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ItemTabPage.Name = "ItemTabPage"
        Me.ItemTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ItemTabPage.Size = New System.Drawing.Size(1192, 797)
        Me.ItemTabPage.TabIndex = 1
        Me.ItemTabPage.Text = "Items"
        Me.ItemTabPage.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(493, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 13)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "Filter By Type"
        '
        'TypeFilterCB
        '
        Me.TypeFilterCB.FormattingEnabled = True
        Me.TypeFilterCB.Location = New System.Drawing.Point(570, 17)
        Me.TypeFilterCB.Name = "TypeFilterCB"
        Me.TypeFilterCB.Size = New System.Drawing.Size(188, 21)
        Me.TypeFilterCB.TabIndex = 7
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
        Me.Button3.TabIndex = 1
        Me.Button3.Text = "Search"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ItemsSearchTB
        '
        Me.ItemsSearchTB.Location = New System.Drawing.Point(6, 18)
        Me.ItemsSearchTB.Name = "ItemsSearchTB"
        Me.ItemsSearchTB.Size = New System.Drawing.Size(255, 20)
        Me.ItemsSearchTB.TabIndex = 0
        '
        'CustomerTabPage
        '
        Me.CustomerTabPage.Controls.Add(Me.CustomerSearchBtn)
        Me.CustomerTabPage.Controls.Add(Me.CustomerSearchTB)
        Me.CustomerTabPage.Controls.Add(Me.EditCustomersCB)
        Me.CustomerTabPage.Controls.Add(Me.CustomerDataGridView)
        Me.CustomerTabPage.Location = New System.Drawing.Point(4, 22)
        Me.CustomerTabPage.Name = "CustomerTabPage"
        Me.CustomerTabPage.Size = New System.Drawing.Size(1192, 797)
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
        Me.CustomerDataGridView.Size = New System.Drawing.Size(1179, 750)
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
        Me.OrdersTabPage.Size = New System.Drawing.Size(1192, 797)
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
        Me.OrdersGridView.Size = New System.Drawing.Size(1183, 718)
        Me.OrdersGridView.TabIndex = 0
        '
        'ReportsTabPage
        '
        Me.ReportsTabPage.Controls.Add(Me.ReportsCheckListBox)
        Me.ReportsTabPage.Controls.Add(Me.Button5)
        Me.ReportsTabPage.Controls.Add(Me.Panel1)
        Me.ReportsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ReportsTabPage.Name = "ReportsTabPage"
        Me.ReportsTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ReportsTabPage.Size = New System.Drawing.Size(1192, 797)
        Me.ReportsTabPage.TabIndex = 4
        Me.ReportsTabPage.Text = "Reports"
        Me.ReportsTabPage.UseVisualStyleBackColor = True
        '
        'ReportsCheckListBox
        '
        Me.ReportsCheckListBox.CheckOnClick = True
        Me.ReportsCheckListBox.FormattingEnabled = True
        Me.ReportsCheckListBox.Location = New System.Drawing.Point(6, 6)
        Me.ReportsCheckListBox.Name = "ReportsCheckListBox"
        Me.ReportsCheckListBox.Size = New System.Drawing.Size(156, 64)
        Me.ReportsCheckListBox.TabIndex = 3
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(168, 6)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(117, 23)
        Me.Button5.TabIndex = 2
        Me.Button5.Text = "View Report"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.ReportViewer2)
        Me.Panel1.Location = New System.Drawing.Point(0, 76)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1154, 721)
        Me.Panel1.TabIndex = 0
        '
        'ReportViewer2
        '
        Me.ReportViewer2.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.ReportViewer2.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer2.Name = "ReportViewer2"
        Me.ReportViewer2.Size = New System.Drawing.Size(1152, 719)
        Me.ReportViewer2.TabIndex = 0
        '
        'InventoryTP
        '
        Me.InventoryTP.Controls.Add(Me.Label7)
        Me.InventoryTP.Controls.Add(Me.CurrentItemAvailableTB)
        Me.InventoryTP.Controls.Add(Me.InventoryDGV)
        Me.InventoryTP.Location = New System.Drawing.Point(4, 22)
        Me.InventoryTP.Name = "InventoryTP"
        Me.InventoryTP.Size = New System.Drawing.Size(1192, 797)
        Me.InventoryTP.TabIndex = 5
        Me.InventoryTP.Text = "Inventory"
        Me.InventoryTP.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(23, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Current Available"
        '
        'CurrentItemAvailableTB
        '
        Me.CurrentItemAvailableTB.Location = New System.Drawing.Point(130, 13)
        Me.CurrentItemAvailableTB.Name = "CurrentItemAvailableTB"
        Me.CurrentItemAvailableTB.Size = New System.Drawing.Size(137, 20)
        Me.CurrentItemAvailableTB.TabIndex = 1
        '
        'InventoryDGV
        '
        Me.InventoryDGV.AllowUserToDeleteRows = False
        Me.InventoryDGV.AllowUserToOrderColumns = True
        Me.InventoryDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.InventoryDGV.Location = New System.Drawing.Point(12, 60)
        Me.InventoryDGV.Name = "InventoryDGV"
        Me.InventoryDGV.Size = New System.Drawing.Size(1160, 360)
        Me.InventoryDGV.TabIndex = 0
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(396, 246)
        Me.ReportViewer1.TabIndex = 0
        '
        'NewBtn
        '
        Me.NewBtn.Location = New System.Drawing.Point(1552, 18)
        Me.NewBtn.Name = "NewBtn"
        Me.NewBtn.Size = New System.Drawing.Size(117, 23)
        Me.NewBtn.TabIndex = 9
        Me.NewBtn.TabStop = False
        Me.NewBtn.Text = "New Order"
        Me.NewBtn.UseVisualStyleBackColor = True
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
        Me.ClientSize = New System.Drawing.Size(1224, 874)
        Me.Controls.Add(Me.NewBtn)
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
        Me.InventoryTP.ResumeLayout(False)
        Me.InventoryTP.PerformLayout()
        CType(Me.InventoryDGV, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents OrderItemsGridView As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents ItemsSearchTB As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents AcresTB As TextBox
    Friend WithEvents EditItemsCB As CheckBox
    Friend WithEvents EditCustomersCB As CheckBox
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
    Friend WithEvents Label6 As Label
    Friend WithEvents TotalPricePerAcreTB As TextBox
    Friend WithEvents OrderStatusCB As ComboBox
    Friend WithEvents Button6 As Button
    Friend WithEvents InventoryTP As TabPage
    Friend WithEvents Label7 As Label
    Friend WithEvents CurrentItemAvailableTB As TextBox
    Friend WithEvents InventoryDGV As DataGridView
    Friend WithEvents ReportsCheckListBox As CheckedListBox
    Friend WithEvents PriceListCB As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents SaveOrderBtn As Button
    Friend WithEvents NewBtn As Button
    Friend WithEvents CopyOrderBtn As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents TypeFilterCB As ComboBox
End Class
