Imports System.Data.Linq
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Reflection
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports QBLib.QBLibrary
Imports SeedGeneral
Imports System.Text

Public Class ItemsForm
    Public d As New GetDataClass
    Public VisibleReportsBindingSource As New BindingSource
    Public GridviewMenu As New ContextMenu
    Public IsNewOrder As Boolean = False
    Public IsLoading As Boolean
    Public isNewItem As Boolean
    Public IsFillItems As Boolean
    'Public CurrentReport As New SeedReport
    Private OrderItemsDataSource As New ReportDataSource
    Private SeedOrderDataSource As New ReportDataSource
    Private SeedOrderDetailSource As New ReportDataSource
    Private SeedReportsSource As New ReportDataSource
    Public QB As New QBLib.QBLibrary

    Public ShowOrderItemInventoryItem As New MenuItem
    Public ShowInventoryMenuItem As New MenuItem
    Public AddInventoryMenuItem As New MenuItem
    Public DeleteOrderItem As New MenuItem
    Public DeleteItem As New MenuItem


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''TODO: This line of code loads data into the 'SeedDataSet.OrderItems' table. You can move, or remove it, as needed.
        'Me.OrderItemsTableAdapter.Fill(Me.SeedDataSet.OrderItems)
        ''TODO: This line of code loads data into the 'SeedDataSet.SeedOrder' table. You can move, or remove it, as needed.
        'Me.SeedOrderTableAdapter.Fill(Me.SeedDataSet.SeedOrder, d.CurrentOrder.OrderID)
        ''TODO: This line of code loads data into the 'SeedDataSet.SeedOrderDetail' table. You can move, or remove it, as needed.
        'Me.SeedOrderDetailTableAdapter.Fill(Me.SeedDataSet.SeedOrderDetail, d.CurrentOrder.OrderID)
        ''Me.SeedReportsTableAdapter.Fill(Me.SeedDataSet.SeedReports)

        IsNewOrder = False
        IsLoading = True
        RefreshData()


        CustomerDataGridView.ReadOnly = True
        CustomerDataGridView.AllowUserToAddRows = False
        ItemsDataGridView.ReadOnly = True
        ItemsDataGridView.AllowUserToAddRows = False
        OrderDatePicker.Value = DateTime.Now

        Me.ReportViewer2.RefreshReport()

        Me.ContextMenu = GridviewMenu
        ShowInventoryMenuItem.Text = "Show Inventory"
        GridviewMenu.MenuItems.Add(ShowInventoryMenuItem)
        ShowOrderItemInventoryItem.Text = "Show Inventory"
        GridviewMenu.MenuItems.Add(ShowOrderItemInventoryItem)
        AddInventoryMenuItem.Text = "Add Inventory"
        GridviewMenu.MenuItems.Add(AddInventoryMenuItem)
        DeleteOrderItem.Text = "Delete Item"
        GridviewMenu.MenuItems.Add(DeleteOrderItem)
        DeleteItem.Text = "Delete Item"
        GridviewMenu.MenuItems.Add(DeleteItem)
        DeleteItem.Visible = False

        AddHandler ShowOrderItemInventoryItem.Click, AddressOf Me.Menu_Click_OrderShowItems
        AddHandler ShowInventoryMenuItem.Click, AddressOf Me.Menu_Click_ShowItems
        AddHandler AddInventoryMenuItem.Click, AddressOf Me.Menu_Click_AddItems
        AddHandler DeleteOrderItem.Click, AddressOf Me.Menu_Click_DeleteOrderItem
        AddHandler DeleteItem.Click, AddressOf Me.Menu_Click_DeleteItem
        OrderItemsGridFormatting()
        CustomerCB.Focus()
        IsLoading = False
    End Sub
    Private Sub RefreshData()

        d.ItemsDB = New ItemsEditDataContext
        d.Items = d.ItemsDB.GetTable(Of Item)
        d.CustomersDB = New CustomersDataContext
        d.Customers = d.CustomersDB.GetTable(Of Customer)
        d.OrderInfoDB = New OrderItemsDataContext
        d.SeedReportsDB = New SeedReportsDataContext
        d.SeedReports = d.SeedReportsDB.GetTable(Of SeedReport)
        d.OrderStatusDB = New OrderStatusDataContext
        d.OrderStatus = d.OrderStatusDB.GetTable(Of OrderStatus)
        d.InventoryDB = New InventoryDataContext

        OrderStatusCB.DataSource = d.OrderStatus
        OrderStatusCB.DisplayMember = "OrderStatusName"
        OrderStatusCB.ValueMember = "OrderStatusID"

        OrderUnitsCB.DataSource = d.OrderInfoDB.OrderUnits
        OrderUnitsCB.DisplayMember = "UnitTypeName"
        OrderUnitsCB.ValueMember = "UnitTypeID"



        Dim DefaultOrderUnits = From OrderUnit In d.OrderInfoDB.OrderUnits Where OrderUnit.IsDefault = True
        d.CurrentOrderUnit = DefaultOrderUnits.Single
        OrderUnitsCB.SelectedItem = d.CurrentOrderUnit
        OrderUnitsCB.SelectedValue = d.CurrentOrderUnit.UnitTypeID

        Dim aCustomers = From Customer In d.Customers Order By Customer.CustomerName
        Dim aItems = From Item In d.Items Order By Item.Item
        CustomerDataGridView.AutoGenerateColumns = True
        CustomerDataGridView.DataSource = aCustomers

        ItemsDataGridView.DataSource = aItems
        CustomerCB.DataSource = aCustomers
        CustomerCB.DisplayMember = "CustomerName"
        CustomerCB.ValueMember = "CustomerID"
        CustomerCB.SelectedIndex = -1

    End Sub
    Private Sub FillItemsData()
        IsLoading = True
        'd.ItemsDB.Refresh(RefreshMode.OverwriteCurrentValues, d.Items)
        ItemsDataGridView.AutoGenerateColumns = True
        Dim aItems = From Item In d.Items Order By Item.Item
        ItemsDataGridView.DataSource = aItems

        If (TypeFilterCB.DataSource Is Nothing) Then
            Dim TypeFilter = From item In d.Items
                             Select New ItemTypes With {
                                .ItemType = item.Type}
                             Distinct


            TypeFilterCB.DataSource = TypeFilter

            TypeFilterCB.DisplayMember = "ItemType"
            TypeFilterCB.ValueMember = "ItemType"
            TypeFilterCB.SelectedIndex = -1
        End If

        IsLoading = False
    End Sub

    Private Sub FillInventoryData(ByVal ItemID As Integer)

        InventoryDGV.AutoGenerateColumns = True
        Dim InventoryHistory = From Inventory In d.InventoryDB.Inventories Join InventoryItemDetail In d.InventoryDB.InventoryItemDetails On Inventory.ItemID Equals InventoryItemDetail.ItemID
                               Where Inventory.ItemID = ItemID And Inventory.Quantity IsNot Nothing
                               Select New InventoryDetails With {
                                   .inventorydate = Inventory.InventoryDate,
                                .inventoryid = Inventory.InventoryID,
                                   .ItemId = Inventory.ItemID,
                                   .Lot = InventoryItemDetail.Lot,
                                   .Quantity = Inventory.Quantity,
                                   .Item = InventoryItemDetail.Item,
                                   .Memo = Inventory.Memo,
                                   .InvoiceID = Inventory.InvoiceID}
        InventoryDGV.DataSource = InventoryHistory

        Dim Balance As Decimal? = 0.00
        For Each inventoryItem As InventoryDetails In InventoryHistory
            Balance = Balance + inventoryItem.Quantity.Value
        Next
        CurrentItemAvailableTB.Text = Balance

    End Sub

    Private Sub FillReports()

        d.UserReports = New List(Of AvailableReport)
        Dim visibleReports = From vr In d.SeedReports Where vr.IsVisible = True

        If (d.UserReports.Count = 0) Then

            For Each SeedReport In visibleReports
                Dim a1 As New AvailableReport(SeedReport.ReportFileName, SeedReport.ReportID, SeedReport.FriendlyName, SeedReport.SortOrder, 0, SeedReport.IsVisible)
                d.UserReports.Add(a1)
            Next
        End If
        VisibleReportsBindingSource.DataSource = d.UserReports

        ReportsDGV.AutoGenerateColumns = True
        ReportsDGV.DataSource = VisibleReportsBindingSource

    End Sub
    Private Sub FillOrdersData()
        IsLoading = True
        OrdersGridView.AutoGenerateColumns = True
        If (d.OrderInfoDB Is Nothing) Then
            d.OrderInfoDB = New OrderItemsDataContext
        End If

        Dim OrdersQuery = From Orders In d.OrderInfoDB.Orders Join Customers In d.OrderInfoDB.OrderCustomerDetails On Orders.CustomerID Equals Customers.CustomerId
                          Select New OrderDetails With {
                                .OrderID = Orders.OrderID,
                              .InvoiceID = Orders.InvoiceID,
                              .Project = Orders.Project,
                              .MixName = Orders.MixName,
                              .ControlNumber = Orders.ControlNumber,
                              .OrderDate = Orders.OrderDate,
                              .OrderTotal = Orders.OrderTotal,
                              .CustomerName = Customers.CustomerName,
                              .CustomerAddressLine1 = Customers.CustomerAddressLine1,
                              .CustomerCity = Customers.CustomerCity,
                              .CustomerState = Customers.CustomerState,
                              .CustomerZip = Customers.CustomerZip,
                              .CustomerEmail = Customers.Email,
                              .CustomerPhone = Customers.CustomerPhone,
                              .Acres = Orders.Acres,
                              .PricePerAcre = Orders.TotalPricePerAcre,
                              .PriceList = Orders.PriceList,
                              .IsMix = Orders.IsMix
                              }

        OrdersGridView.DataSource = OrdersQuery
        IsLoading = False
    End Sub


    Private Sub FillOrderItems()
        'CreateUpdateOrder()
        'IsFillItems = False
        Dim OrderTotal As Decimal? = 0.00
        Dim OrderPricePerAcre As Decimal? = 0.00
        If Not (d.CurrentOrder Is Nothing) Then
            Dim CurrentOrderItems = From orderitems In d.OrderInfoDB.OrderItems Join OrderItemDetails In d.OrderInfoDB.OrderItemDetails On orderitems.ItemID Equals OrderItemDetails.ItemID Where orderitems.OrderID.Equals(d.CurrentOrder.OrderID)
                                    Select New OrderItemDetails With {
                                        .Lot = OrderItemDetails.Lot,
                                        .PLS = orderitems.PLS_Percent,
                                        .Item = OrderItemDetails.Item,
                                        .PLSLBSPerAcre = orderitems.PLS_LBS_PerAcre,
                                        .PricePerPLSLB = orderitems.PricePerPLSLB,
                                        .PricePerAcre = orderitems.PricePerAcre,
                                        .TotalPrice = orderitems.PricePerAcre * d.CurrentOrder.Acres,
                                        .Distributor = OrderItemDetails.Distributor,
                                        .Wholesale = OrderItemDetails.Wholesale,
                                        .Retail = OrderItemDetails.Retail,
                                        .OrderItemID = orderitems.OrderItemID,
                                        .BulkLbs = orderitems.BulkLbs,
                                        .PLSLBS = orderitems.PLSLbs,
                                        .ItemID = orderitems.ItemID,
                                        .TestDate = OrderItemDetails.Test_Date}

            OrderItemsGridView.DataSource = CurrentOrderItems

            For Each OrderItem In CurrentOrderItems
                OrderPricePerAcre = OrderPricePerAcre + OrderItem.PricePerAcre
            Next

            OrderTotal = OrderPricePerAcre * d.CurrentOrder.Acres
            If (OrderTotal Is Nothing) Then
                OrderTotal = 0.00
                OrderPricePerAcre = 0.00
            End If
            d.CurrentOrder.OrderTotal = OrderTotal
            d.CurrentOrder.TotalPricePerAcre = OrderPricePerAcre
        End If
        OrderTotalTB.Text = FormatCurrency(OrderTotal, 2, TriState.True, TriState.False, TriState.True)
        TotalPricePerAcreTB.Text = FormatCurrency(OrderPricePerAcre, 2, TriState.True, TriState.False, TriState.True)
        IsFillItems = False
    End Sub

    Private Sub UpdateItems()
        Dim SaveItem As MsgBoxResult
        SaveItem = MessageBox.Show("Would you like to save your changes?", "Save Changes?", MessageBoxButtons.YesNo)
        If (SaveItem.Yes) Then
            d.ItemsDB.SubmitChanges()
        End If
    End Sub

    Private Sub UpdateCustomers()
        Dim SaveItem As MsgBoxResult
        SaveItem = MessageBox.Show("Would you like to save your changes?", "Save Changes?", MessageBoxButtons.YesNo)
        If (SaveItem.Yes) Then
            d.CustomersDB.SubmitChanges()
        End If
        If (SaveItem.No) Then
            Exit Sub
        End If
    End Sub
    Private Sub TabChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles OrdersPage.SelectedIndexChanged
        CreateUpdateOrder()

        If (OrdersPage.SelectedTab Is ItemTabPage) Then
            IsLoading = True
            ItemsSearchTB.Text = Nothing
            ItemsSearchTB.Focus()
            FillItemsData()
            ItemsGridFormatting()
            IsLoading = False
            ShowInventoryMenuItem.Visible = True
            AddInventoryMenuItem.Visible = True
            ShowOrderItemInventoryItem.Visible = False
            DeleteOrderItem.Visible = False
        End If

        If (OrdersPage.SelectedTab Is CustomerTabPage) Then
            IsLoading = True

            IsLoading = False
            CustomerGridViewFormatting()
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            ShowOrderItemInventoryItem.Visible = False
            DeleteOrderItem.Visible = False
            CustomerSearchTB.Focus()
        End If
        If (OrdersPage.SelectedTab Is OrdersTabPage) Then
            IsLoading = True
            FillOrdersData()
            IsLoading = False
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            ShowOrderItemInventoryItem.Visible = False
            DeleteOrderItem.Visible = False
            OrdersGridFormatting()
            OrdersSearchTB.Focus()
        End If

        If (OrdersPage.SelectedTab Is OrderTabPage) Then
            'FillCustomerComboBox()
            FillOrderItems()
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            ShowOrderItemInventoryItem.Visible = True
            DeleteOrderItem.Visible = True
            OrderItemsGridFormatting()
            CustomerCB.Focus()
        End If
        If (OrdersPage.SelectedTab Is ReportsTabPage) Then
            FillReports()
            ReportsGridFormatting()
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            ShowOrderItemInventoryItem.Visible = False
            DeleteOrderItem.Visible = False
        End If


    End Sub

    Private Sub UpdateAcres(sender As Object, e As System.EventArgs) Handles UnitsTB.Leave
        If Not (d.CurrentOrder Is Nothing) Then
            d.CurrentOrder.Acres = UnitsTB.Text
        End If

    End Sub
    Private Sub PricePerAcreUpdate(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles OrderItemsGridView.CellLeave
        If (IsFillItems = False) Then
            Dim SelectedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
            OrderItemsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
            d.CurrentOrderItem = SelectedRow.DataBoundItem
            Dim UpdateDOrderItems = From OrderItem In d.OrderInfoDB.OrderItems Where OrderItem.OrderItemID = d.CurrentOrderItem.OrderItemID
            Dim s As String = sender.columns(e.ColumnIndex).DataPropertyName

            Dim UpdateOrderItems As Boolean = False
            For Each UpdateDOrderItem In UpdateDOrderItems
                Select Case s
                    Case "PLSLBSPerAcre"
                        UpdateOrderItems = True
                        UpdateDOrderItem.PLS_LBS_PerAcre = d.CurrentOrderItem.PLSLBSPerAcre
                    Case "PricePerPLSLB"
                        UpdateOrderItems = True
                        UpdateDOrderItem.PricePerPLSLB = d.CurrentOrderItem.PricePerPLSLB
                    Case "PLS"
                        UpdateOrderItems = True
                        UpdateDOrderItem.PLS_Percent = d.CurrentOrderItem.PLS
                    Case Else
                        UpdateOrderItems = False
                End Select
                UpdateDOrderItem.PLS_Percent = d.CurrentOrderItem.PLS
                UpdateOrderItemInformation(UpdateDOrderItem)
                d.CurrentOrderItem.PricePerAcre = UpdateDOrderItem.PricePerAcre
                d.CurrentOrderItem.TotalPrice = UpdateDOrderItem.TotalPrice
                d.CurrentOrderItem.BulkLbs = UpdateDOrderItem.BulkLbs
                d.CurrentOrderItem.PLSLBS = UpdateDOrderItem.PLSLbs

            Next
            OrderItemsGridView.Refresh()
            OrderItemsGridFormatting()
        End If
    End Sub
    Public Sub UpdateOrderItemInformation(ByVal MyCurrentOrderItem As OrderItem)
        MyCurrentOrderItem.PricePerAcre = MyCurrentOrderItem.PricePerPLSLB * MyCurrentOrderItem.PLS_LBS_PerAcre
        MyCurrentOrderItem.TotalPrice = MyCurrentOrderItem.PricePerAcre * d.CurrentOrder.Acres
        MyCurrentOrderItem.BulkLbs = (MyCurrentOrderItem.PLS_LBS_PerAcre * d.CurrentOrder.Acres) / MyCurrentOrderItem.PLS_Percent
        MyCurrentOrderItem.PLSLbs = MyCurrentOrderItem.PLS_LBS_PerAcre * d.CurrentOrder.Acres

        d.OrderInfoDB.SubmitChanges()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        UpdateCustomers()
        UpdateItems()
    End Sub

    Private Sub SaveOrderBtn_Click(sender As Object, e As EventArgs)
        CreateUpdateOrder()
    End Sub

    Private Sub CreateUpdateOrder()
        If (d.CurrentOrder Is Nothing AndAlso IsNewOrder) Then
            d.CurrentOrder = New Order
            d.OrderInfoDB.Orders.InsertOnSubmit(d.CurrentOrder)
        End If

        If Not (d.CurrentOrder Is Nothing) Then
            ' d.OrderInfoDB.SubmitChanges()
            d.CurrentOrder.InvoiceID = InvoiceTB.Text
            d.CurrentOrder.CustomerID = CustomerCB.SelectedValue
            d.CurrentOrder.UnitTypeID = OrderUnitsCB.SelectedValue
            d.CurrentOrder.Acres = UnitsTB.Text
            d.CurrentOrder.Project = ProjectTB.Text
            d.CurrentOrder.MixName = MixNameTB.Text
            d.CurrentOrder.ControlNumber = ControlNbrTB.Text
            d.CurrentOrder.PriceList = PriceListCB.Text
            d.CurrentOrder.OrderStatusId = OrderStatusCB.SelectedValue
            d.CurrentOrder.OrderDate = OrderDatePicker.Value
            d.CurrentOrder.IsMix = IsMixCB.Checked


            If d.CustomerPriceList Is Nothing Then
                Dim CurrentCustomerList = From customer In d.Customers Where customer.CustomerId = d.CurrentOrder.CustomerID
                For Each Customer In CurrentCustomerList
                    d.CurrentCustomer = Customer
                    If (d.CustomerPriceList Is Nothing OrElse d.CustomerPriceList = "") Then
                        d.CustomerPriceList = QB.DoPriceListQuery(d.CurrentCustomer.CustomerName, "NameList")
                        PriceListCB.Text = d.CustomerPriceList
                    End If
                Next
            End If
            d.OrderInfoDB.SubmitChanges()
            OrderIDTB.Text = d.CurrentOrder.OrderID
            FillOrderItems()
        End If


    End Sub
    Private Sub ViewCurrentOrder()
        If Not (d.CurrentOrder Is Nothing) Then
            InvoiceTB.Text = d.CurrentOrder.InvoiceID
            CustomerCB.SelectedValue = d.CurrentOrder.CustomerID
            UnitsTB.Text = d.CurrentOrder.Acres
            ProjectTB.Text = d.CurrentOrder.Project
            MixNameTB.Text = d.CurrentOrder.MixName
            ControlNbrTB.Text = d.CurrentOrder.ControlNumber
            PriceListCB.Text = d.CurrentOrder.PriceList
            OrderIDTB.Text = d.CurrentOrder.OrderID
            OrderDatePicker.Value = d.CurrentOrder.OrderDate.Value
            If Not (d.CurrentOrder.IsMix Is Nothing) Then
                IsMixCB.Checked = d.CurrentOrder.IsMix.Value
            End If

            TotalPricePerAcreTB.Text = FormatCurrency(d.CurrentOrder.TotalPricePerAcre, 2, TriState.True, TriState.False, TriState.True)
            OrderTotalTB.Text = FormatCurrency(d.CurrentOrder.OrderTotal, 2, TriState.True, TriState.False, TriState.True)
            FillOrderItems()
            ReportViewer2.Reset()
        End If
    End Sub

    Private Sub PerformItemsSearch()
        'ic.ItemsDB = New ItemsEditDataContext
        Dim ItemsQuery = From Items In d.Items Where Items.Item.Contains(ItemsSearchTB.Text) Or Items.Lot.Contains(ItemsSearchTB.Text) Or Items.BotanicalName.Contains(ItemsSearchTB.Text)
                         Order By Items.Item

        ItemsDataGridView.DataSource = ItemsQuery
        TypeFilterCB.SelectedIndex = -1

    End Sub
    Private Sub PerformItemsFilter()
        'ic.ItemsDB = New ItemsEditDataContext

        Dim ItemsQuery = From Items In d.Items Where Items.Type = TypeFilterCB.Text
                         Order By Items.Item
        ItemsDataGridView.DataSource = ItemsQuery

    End Sub

    Private Sub PerformCustomerSearch()
        Dim CustomersQuery = From Customers In d.Customers Where Customers.CustomerName.Contains(CustomerSearchTB.Text)
        CustomerDataGridView.DataSource = CustomersQuery
    End Sub

    Private Sub PerformOrdersSearch()
        Dim OrdersQuery = From Orders In d.OrderInfoDB.Orders Where Orders.InvoiceID.Contains(OrdersSearchTB.Text) Or Orders.OrderID.ToString().Contains(OrdersSearchTB.Text) Or Orders.Project.ToString().Contains(OrdersSearchTB.Text) Or Orders.MixName.ToString().Contains(OrdersSearchTB.Text) Or Orders.ControlNumber.ToString().Contains(OrdersSearchTB.Text)
        OrdersGridView.DataSource = OrdersQuery
    End Sub
    Private Sub UserDeletingRow(ByVal sender As Object,
    ByVal e As DataGridViewRowCancelEventArgs) _
    Handles OrderItemsGridView.UserDeletingRow

        Dim DeletedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
        Dim i As OrderItemDetails = DeletedRow.DataBoundItem
        Dim i2 = From OrderItem In d.OrderInfoDB.OrderItems Where OrderItem.OrderItemID = i.OrderItemID
        Dim r As Integer = i2.Count
        For Each OrderItem In i2
            d.OrderInfoDB.OrderItems.DeleteOnSubmit(OrderItem)
        Next

    End Sub
    Private Sub AddItemsToOrder()
        If (d.CurrentOrder Is Nothing) Then
            IsNewOrder = True
        End If

        CreateUpdateOrder()

        For Each row As DataGridViewRow In ItemsDataGridView.SelectedRows
            Dim currentItem As New Item
            currentItem = row.DataBoundItem
            Dim SelectedItem As New OrderItem
            SelectedItem.ItemID = currentItem.ItemID
            SelectedItem.OrderID = d.CurrentOrder.OrderID
            SelectedItem.PLS_Percent = currentItem.PLS_
            d.OrderInfoDB.OrderItems.InsertOnSubmit(SelectedItem)
            If (Not d.CustomerPriceList Is Nothing) Then
                Select Case d.CustomerPriceList
                    Case "Distributor"
                        SelectedItem.PricePerPLSLB = currentItem.Distributor
                    Case "Retail"
                        SelectedItem.PricePerPLSLB = currentItem.Retail
                    Case "Wholesale"
                        SelectedItem.PricePerPLSLB = currentItem.Wholesale
                End Select
            End If
            Dim dd As Long = (DateDiff(DateInterval.Day, CType(currentItem.Test_Date, DateTime), Now()))
            If dd > 365 Then
                MsgBox(currentItem.Item + " hasn't been tested in " + dd.ToString() + " days" + vbCrLf + "Last test date: " + currentItem.Test_Date.ToString())
            End If
        Next
        d.OrderInfoDB.SubmitChanges()
        ItemsDataGridView.ClearSelection()
        'FillOrderItems()
    End Sub
    Private Sub NewOrder()
        d.CurrentOrder = New Order
        InvoiceTB.Text = Nothing
        CustomerCB.SelectedItem = Nothing
        OrderStatusCB.SelectedItem = Nothing
        UnitsTB.Text = 0.00
        OrderTotalTB.Text = FormatCurrency(0.00, 2, TriState.True, TriState.False, TriState.True)
        TotalPricePerAcreTB.Text = FormatCurrency(0.00, 2, TriState.True, TriState.False, TriState.True)
        ProjectTB.Text = Nothing
        ControlNbrTB.Text = Nothing
        MixNameTB.Text = Nothing
        'CreateUpdateOrder()
        OrderItemsGridView.DataSource = Nothing
        OrderDatePicker.Value = DateTime.Now
        IsNewOrder = True
        d.CustomerPriceList = Nothing
        QB = New QBLib.QBLibrary
        ReportViewer2.Reset()
        d.UserReports = New List(Of AvailableReport)
        d.CurrentOrder = Nothing
        OrderIDTB.Text = Nothing
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PerformItemsSearch()

    End Sub
    Private Sub SearchEnter(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles ItemsSearchTB.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            PerformItemsSearch()
        End If

    End Sub
    Private Sub SearchCustomerEnter(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles CustomerSearchTB.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            PerformCustomerSearch()
        End If

    End Sub
    Private Sub SearchOrdersEnter(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles OrdersSearchTB.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            PerformOrdersSearch()
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AddItemsToOrder()
        ''OrdersPage.SelectedTab = OrderTabPage
    End Sub
    Private Sub DoubleClickItemGrid(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ItemsDataGridView.CellMouseDoubleClick
        IsLoading = True
        AddItemsToOrder()
        FillItemsData()
        ItemsGridFormatting()
        ItemsSearchTB.Text = Nothing
        ItemsSearchTB.Focus()
        IsLoading = False
        OrderItemsGridView.ClearSelection()
    End Sub
    Private Sub DoubleClickOrdersGrid(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles OrdersGridView.CellMouseDoubleClick
        For Each row As DataGridViewRow In OrdersGridView.SelectedRows
            Dim SelectedOrder As OrderDetails = row.DataBoundItem
            Dim i2 = From Orders In d.OrderInfoDB.Orders Where Orders.OrderID = SelectedOrder.OrderID
            For Each order In i2
                d.CurrentOrder = order
            Next

        Next
        ViewCurrentOrder()
        OrdersPage.SelectedTab = OrderTabPage
    End Sub
    Private Sub DoubleClickCustomersGrid(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles CustomerDataGridView.CellMouseDoubleClick
        If Not (d.CurrentOrder Is Nothing) Then
            Exit Sub
        End If

        For Each row As DataGridViewRow In CustomerDataGridView.SelectedRows
            d.CurrentCustomer = row.DataBoundItem
        Next
        'CustomerCB.SelectedItem = d.CurrentCustomer
        CustomerCB.SelectedValue = d.CurrentCustomer.CustomerId

        OrdersPage.SelectedTab = OrderTabPage

    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles EditItemsCB.CheckedChanged
        AllowItemEdits()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles EditCustomersCB.CheckedChanged
        AllowCustomerEdits()

    End Sub

    Private Sub NewBtn_Click(sender As Object, e As EventArgs)
        NewOrder()

    End Sub

    Private Sub CustomerSearchBtn_Click(sender As Object, e As EventArgs) Handles CustomerSearchBtn.Click
        PerformCustomerSearch()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For Each row As DataGridViewRow In OrdersGridView.SelectedRows
            Dim SelectedOrder As OrderDetails = row.DataBoundItem
            Dim i2 = From Orders In d.OrderInfoDB.Orders Where Orders.OrderID = SelectedOrder.OrderID
            For Each order In i2
                d.CurrentOrder = order
            Next

        Next
        ViewCurrentOrder()

        OrdersPage.SelectedTab = OrderTabPage

    End Sub
    Public Sub AllowCustomerEdits()
        If EditCustomersCB.Checked = True Then
            CustomerDataGridView.AllowUserToAddRows = True
            CustomerDataGridView.ReadOnly = False
            EditCustomersCB.Text = "Save"
        End If
        If EditCustomersCB.Checked = False Then
            CustomerDataGridView.AllowUserToAddRows = False
            CustomerDataGridView.ReadOnly = True
            UpdateCustomers()
            EditCustomersCB.Text = "Edit"
        End If

    End Sub
    Public Sub AllowItemEdits()
        If EditItemsCB.Checked = True Then
            ItemsDataGridView.AllowUserToAddRows = True
            ItemsDataGridView.ReadOnly = False
            'DeleteItem.Visible = True
            EditItemsCB.Text = "Save"
        End If


        If EditItemsCB.Checked = False Then
            ItemsDataGridView.AllowUserToAddRows = False
            ItemsDataGridView.ReadOnly = True
            'DeleteItem.Visible = False
            UpdateItems()
            EditItemsCB.Text = "Edit"
        End If

    End Sub
    Public Sub RefreshDataSources()

        'TODO: This line of code loads data into the 'SeedDataSet.OrderItems' table. You can move, or remove it, as needed.
        Me.OrderItemsTableAdapter.Fill(Me.SeedDataSet.OrderItems)
        'TODO: This line of code loads data into the 'SeedDataSet.SeedOrder' table. You can move, or remove it, as needed.
        Me.SeedOrderTableAdapter.Fill(Me.SeedDataSet.SeedOrder, d.CurrentOrder.OrderID)
        'TODO: This line of code loads data into the 'SeedDataSet.SeedOrderDetail' table. You can move, or remove it, as needed.
        Me.SeedOrderDetailTableAdapter.Fill(Me.SeedDataSet.SeedOrderDetail, d.CurrentOrder.OrderID)

        Me.SeedReportsTableAdapter.Fill(Me.SeedDataSet.SeedReports)

    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        RefreshDataSources()
        Dim ReportFileName As String
        Dim HasSubReports As Boolean

        If (d.UserReports.Count > 0) Then

            If (d.UserReports.Count = 1) Then
                ReportFileName = d.UserReports.Single.ReportFileName
                HasSubReports = d.UserReports.Single.HasSubReports
            Else
                Dim i3 = From SeedReports In d.SeedReports Where SeedReports.HasSubReports = True
                ReportFileName = i3.Single.ReportFileName
                HasSubReports = i3.Single.HasSubReports
            End If
            GetReport(ReportFileName, HasSubReports)
            ReportViewer2.RefreshReport()
        End If
    End Sub
    Public Sub GetReport(ByRef ReportFileName As String, ByRef HasSubReports As Boolean)

        'Me.ReportViewer2.Dock = System.Windows.Forms.DockStyle.Fill
        OrderItemsDataSource.Name = "OrderItems"
        OrderItemsDataSource.Value = Me.OrderItemsBindingSource
        SeedOrderDataSource.Name = "SeedOrder"
        SeedOrderDataSource.Value = Me.SeedOrderBindingSource
        SeedOrderDetailSource.Name = "SeedOrderDetail"
        SeedOrderDetailSource.Value = Me.SeedOrderDetailBindingSource
        SeedReportsSource.Name = "SeedReports"
        SeedReportsSource.Value = Me.VisibleReportsBindingSource

        Me.ReportViewer2.Reset()
        Me.ReportViewer2.LocalReport.ReportPath = ReportFileName
        Me.ReportViewer2.LocalReport.DataSources.Add(OrderItemsDataSource)
        Me.ReportViewer2.LocalReport.DataSources.Add(SeedOrderDataSource)
        Me.ReportViewer2.LocalReport.DataSources.Add(SeedOrderDetailSource)
        Me.ReportViewer2.LocalReport.DataSources.Add(SeedReportsSource)
        Me.ReportViewer2.LocalReport.ReportEmbeddedResource = My.Application.Info.AssemblyName + "." + ReportFileName
        Me.ReportViewer2.LocalReport.DisplayName = d.CurrentCustomer.CustomerName + "_" + d.CurrentOrder.Project
        Me.ReportViewer2.PrinterSettings.PrintFileName = d.CurrentCustomer.CustomerName + "_" + d.CurrentOrder.Project
        Me.ReportViewer2.Name = "ReportViewer2"
        If (HasSubReports) Then
            Me.ReportViewer2.ProcessingMode = ProcessingMode.Local
            AddHandler Me.ReportViewer2.LocalReport.SubreportProcessing, AddressOf AllReportsSubreportProcessingEventHandler
            Me.ReportViewer2.RefreshReport()
        End If
    End Sub

    Public Sub AllReportsSubreportProcessingEventHandler(ByVal sender As Object,
     ByVal e As SubreportProcessingEventArgs)
        e.DataSources.Add(OrderItemsDataSource)
        e.DataSources.Add(SeedOrderDataSource)
        e.DataSources.Add(SeedOrderDetailSource)
    End Sub
    Private Sub OrderStatusCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles OrderStatusCB.SelectedIndexChanged
        If Not d.CurrentOrder Is Nothing Then
            If Not (OrderStatusCB.SelectedItem Is Nothing OrElse OrderStatusCB.SelectedItem.OrderStatusID = 0) Then
                d.CurrentOrder.OrderStatusId = OrderStatusCB.SelectedValue
                d.OrderInfoDB.SubmitChanges()
            End If
        End If
    End Sub
    Public Sub CreateInvoice()
        Dim OrdersQuery = From Orders In d.OrderInfoDB.Orders Join Customers In d.OrderInfoDB.OrderCustomerDetails On Orders.CustomerID Equals Customers.CustomerId Where Orders.OrderID = d.CurrentOrder.OrderID
                          Select New OrderDetails With {
                                .OrderID = Orders.OrderID,
                              .InvoiceID = Orders.InvoiceID,
                              .Project = Orders.Project,
                              .OrderDate = Orders.OrderDate,
                              .OrderTotal = Orders.OrderTotal,
                              .CustomerName = Customers.CustomerName,
                              .CustomerAddressLine1 = Customers.CustomerAddressLine1,
                              .CustomerCity = Customers.CustomerCity,
                              .CustomerState = Customers.CustomerState,
                              .CustomerZip = Customers.CustomerZip,
                              .Acres = Orders.Acres,
                              .PricePerAcre = Orders.TotalPricePerAcre,
                              .IsMix = Orders.IsMix
                              }

        Dim OrdersItemsQuery = From OrderItem In d.OrderInfoDB.OrderItems Join OrderItemDetails In d.OrderInfoDB.OrderItemDetails On OrderItem.ItemID Equals OrderItemDetails.ItemID Where OrderItem.OrderID = d.CurrentOrder.OrderID
                               Select New OrderItemDetails With {
                                .PricePerAcre = OrderItem.PricePerAcre,
                                   .TotalPrice = OrderItem.TotalPrice,
                                   .Lot = OrderItemDetails.Lot,
                                   .Item = OrderItemDetails.Item
                              }
        Dim OrderItems = OrdersItemsQuery.ToArray()
        Dim Order = OrdersQuery.ToArray()


        d.CurrentOrder.InvoiceID = QB.QBCreateInvoice(Order, OrderItems)
        d.OrderInfoDB.SubmitChanges()
        InvoiceTB.Text = d.CurrentOrder.InvoiceID
    End Sub
    Public Sub UpdateInventory()

        Dim CurrentOrderItems = From orderitems In d.OrderInfoDB.OrderItems Join OrderItemDetails In d.OrderInfoDB.OrderItemDetails On orderitems.ItemID Equals OrderItemDetails.ItemID Where orderitems.OrderID.Equals(d.CurrentOrder.OrderID)
                                Select New OrderItemDetails With {
                                    .Lot = OrderItemDetails.Lot,
                                    .PLS = OrderItemDetails.PLS_,
                                    .Item = OrderItemDetails.Item,
                                    .PLSLBSPerAcre = orderitems.PLS_LBS_PerAcre,
                                    .PricePerPLSLB = orderitems.PricePerPLSLB,
                                    .PricePerAcre = orderitems.PricePerAcre,
                                    .TotalPrice = orderitems.PricePerAcre * d.CurrentOrder.Acres,
                                    .Distributor = OrderItemDetails.Distributor,
                                    .Wholesale = OrderItemDetails.Wholesale,
                                    .Retail = OrderItemDetails.Retail,
                                    .OrderItemID = orderitems.OrderItemID,
                                    .BulkLbs = orderitems.BulkLbs,
                                    .PLSLBS = orderitems.PLSLbs,
                                    .ItemID = orderitems.ItemID}
        OrderItemsGridView.DataSource = CurrentOrderItems

        Dim InventoryItems = From inventories In d.InventoryDB.Inventories Where inventories.InvoiceID = d.CurrentOrder.InvoiceID

        If InventoryItems.Count = 0 Then
            For Each orderitem In CurrentOrderItems
                Dim tmpInventory As New Inventory
                tmpInventory.InventoryDate = d.CurrentOrder.OrderDate
                tmpInventory.Quantity = orderitem.BulkLbs * -1.0
                tmpInventory.InvoiceID = d.CurrentOrder.InvoiceID
                tmpInventory.Memo = "Order " + d.CurrentOrder.OrderID.ToString
                tmpInventory.ItemID = orderitem.ItemID
                d.InventoryDB.Inventories.InsertOnSubmit(tmpInventory)
            Next
            d.InventoryDB.SubmitChanges()
        End If
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        CreateUpdateOrder()

        If d.CurrentOrder.InvoiceID Is Nothing OrElse d.CurrentOrder.InvoiceID = "" Then
            CreateInvoice()

        End If
        UpdateInventory()
    End Sub

    Private Sub CustomerCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CustomerCB.SelectedIndexChanged
        PriceListCB.Text = Nothing
        d.CustomerPriceList = Nothing
        d.CurrentCustomer = CustomerCB.SelectedItem
    End Sub

    'Private Sub RightClickItemsGridview(sender As Object, e As MouseEventArgs) Handles ItemsDataGridView.MouseClick
    '    If (e.Button = MouseButtons.Right) Then
    '        AddHandler ShowInventoryMenuItem.Click, AddressOf Me.Menu_Click_ShowItems
    '        AddHandler AddInventoryMenuItem.Click, AddressOf Me.Menu_Click_AddItems
    '    End If
    'End Sub


    Private Sub Menu_Click_ShowItems(ByVal sender As Object, ByVal e As System.EventArgs)
        IsLoading = True

        Dim SelectedRow As DataGridViewRow = ItemsDataGridView.CurrentRow
        Dim Item As Item = SelectedRow.DataBoundItem
        FillInventoryData(Item.ItemID)
        InventoryGridViewFormatting()
        OrdersPage.SelectedTab = InventoryTP
        IsLoading = False

    End Sub
    Private Sub Menu_Click_AddItems(ByVal sender As Object, ByVal e As System.EventArgs)
        IsLoading = True
        Dim SelectedRow As DataGridViewRow = ItemsDataGridView.CurrentRow
        Dim Item As Item = SelectedRow.DataBoundItem
        AddItemInventory(Item)

    End Sub
    Private Sub Menu_Click_DeleteItem(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeletedRow As DataGridViewRow = ItemsDataGridView.CurrentRow
        Dim i As Item = DeletedRow.DataBoundItem
        Dim i2 = From Items In d.Items Where Items.ItemID = i.ItemID
        d.ItemsDB.SubmitChanges()
        FillItemsData()
        ItemsGridFormatting()
    End Sub
    Private Sub Menu_Click_DeleteOrderItem(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeletedRow As DataGridViewRow = OrderItemsGridView.CurrentRow

        Dim i As OrderItemDetails = DeletedRow.DataBoundItem
        Dim i2 = From OrderItem In d.OrderInfoDB.OrderItems Where OrderItem.OrderItemID = i.OrderItemID
        Dim r As Integer = i2.Count
        For Each OrderItem In i2
            d.OrderInfoDB.OrderItems.DeleteOnSubmit(OrderItem)
        Next
        d.OrderInfoDB.SubmitChanges()
        FillOrderItems()
    End Sub
    'Private Sub RightClickOrderItemsGridview(sender As Object, e As MouseEventArgs) Handles OrderItemsGridView.MouseClick
    '    If (e.Button = MouseButtons.Right) Then

    '        AddHandler ShowInventoryMenuItem.Click, AddressOf Me.Menu_Click_OrderShowItems

    '    End If
    'End Sub
    Private Sub Menu_Click_OrderShowItems(ByVal sender As Object, ByVal e As System.EventArgs)
        IsLoading = True
        Dim SelectedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
        Dim Item As OrderItemDetails = SelectedRow.DataBoundItem
        d.ItemID = Item.ItemID
        FillInventoryData(d.ItemID)
        OrdersPage.SelectedTab = InventoryTP
        InventoryGridViewFormatting()
        IsLoading = False

    End Sub


    Public Sub InventoryGridViewFormatting()
        For Each column As DataGridViewColumn In InventoryDGV.Columns
            Select Case column.HeaderCell.Value
                Case "inventoryid"
                    column.Visible = False
                Case "ItemId"
                    column.Visible = False
                Case "inventorydate"
                    column.HeaderText = "Inventory Date"

                Case "Item"
                Case Else
                    column.Visible = True
            End Select

        Next
    End Sub
    Public Sub ItemsGridFormatting()

        For Each column As DataGridViewColumn In ItemsDataGridView.Columns
            Select Case column.HeaderCell.OwningColumn.DataPropertyName
                Case "Item"
                    column.HeaderText = "Common Name"
                'ItemsDataGridView.Sort(column, System.ComponentModel.ListSortDirection.Ascending)
                Case "PLS_", "Purity", "Crop", "Inert", "Weeds", "Germ", "Dormant", "Total"
                    column.DefaultCellStyle.Format = "p2"
                    If (column.HeaderCell.OwningColumn.DataPropertyName = "PLS_") Then
                        column.HeaderText = "PLS %"
                    End If
                Case "Distributor", "Retail", "Wholesale"
                    column.DefaultCellStyle.Format = "c2"
                Case "Test_Date"
                    column.DefaultCellStyle.Format = "MM/dd"
                Case "ItemID"
                    column.Visible = False
                Case "BotanicalName"
                    column.HeaderText = "Botanical Name"
                Case "ScientificName"
                    column.HeaderText = "Scientific Name"
                Case "variety"
                    column.HeaderText = "Variety"
                Case "IsDeleted"
                    column.Visible = False
            End Select
        Next
    End Sub
    Public Sub OrdersGridFormatting()
        For Each column As DataGridViewColumn In OrdersGridView.Columns
            Select Case column.HeaderCell.OwningColumn.DataPropertyName
                Case "OrderTotal"
                    column.HeaderText = "Order Total"
                    column.DefaultCellStyle.Format = "c2"
                Case "OrderID"
                    column.HeaderText = "Order #"
                Case "InvoiceID"
                    column.HeaderText = "Invoice"
                Case "OrderDate"
                    column.HeaderText = "Order Date"
                    column.DefaultCellStyle.Format = "MM/dd/yyyy"
                Case "CustomerName"
                    column.HeaderText = "Customer Name"
                Case "CustomerAddressLine1"
                    column.HeaderText = "Address Line 1"
                Case "CustomerAddressLine2"
                    column.HeaderText = "Address Line 2"
                Case "CustomerCity"
                    column.HeaderText = "City"
                Case "CustomerState"
                    column.HeaderText = "State"
                Case "CustomerZip"
                    column.HeaderText = "Zip"
                Case "CustomerPhone"
                    column.HeaderText = "Phone"
                Case "PricePerAcre"
                    column.HeaderText = "Price Per Acre"
                    column.DefaultCellStyle.Format = "c2"
                Case "PriceList"
                    column.HeaderText = "Price List"





            End Select
        Next
    End Sub
    Public Sub ReportsGridFormatting()
        For Each column As DataGridViewColumn In ReportsDGV.Columns
            Select Case column.HeaderCell.OwningColumn.DataPropertyName
                Case "IsVisible"
                    column.Visible = False
                Case "ReportFileName"
                    column.Visible = False
                Case "ReportID"
                    column.Visible = False
                Case "FriendlyName"
                    column.HeaderText = "Report"
                Case "SortOrder"
                    column.Visible = False
                Case "UserChecked"
                    column.HeaderText = "Select"
                Case "HasSubReports"
                    column.Visible = False



            End Select
        Next
    End Sub
    Public Sub OrderItemsGridFormatting()
        For Each column As DataGridViewColumn In OrderItemsGridView.Columns
            Select Case column.HeaderCell.OwningColumn.DataPropertyName
                Case "PLS"
                    column.HeaderText = "PLS %"
                    column.DefaultCellStyle.Format = "p2"
                Case "PLSLBSPerAcre"
                    column.HeaderText = "PLS LBS Per " + d.CurrentOrderUnit.UnitTypeName
                    column.DefaultCellStyle.Format = "0.00"
                Case "PricePerPLSLB"
                    column.HeaderText = "Price Per PLS LBS"
                    column.DefaultCellStyle.Format = "c2"
                Case "PricePerAcre"
                    column.HeaderText = "Price Per " + d.CurrentOrderUnit.UnitTypeName
                    column.DefaultCellStyle.Format = "c2"
                Case "TotalPrice"
                    column.HeaderText = "Total Price"
                    column.DefaultCellStyle.Format = "c2"
                Case "OrderItemID"
                    column.Visible = False
                Case "BulkLbs"
                    column.HeaderText = "Bulk LBS"
                    column.DefaultCellStyle.Format = "0.00"
                Case "PLSLBS"
                    column.HeaderText = "PLS LBS"
                    column.DefaultCellStyle.Format = "0.00"
                Case "ItemID"
                    column.Visible = False
                Case "Distributor", "Retail", "Wholesale"
                    column.DefaultCellStyle.Format = "c2"

                Case Else

            End Select
        Next
    End Sub
    Public Sub CustomerGridViewFormatting()
        For Each column As DataGridViewColumn In CustomerDataGridView.Columns
            Select Case column.HeaderCell.Value
                Case "CustomerId"
                    column.Visible = False
                Case "CustomerName"
                    column.HeaderText = "Customer Name"
                    CustomerDataGridView.Sort(column, System.ComponentModel.ListSortDirection.Ascending)
                Case "CustomerAddressLine1"
                    column.HeaderText = "Address Line 1"
                Case "CustomerAddressLine2"
                    column.HeaderText = "Address Line 2"
                Case "CustomerCity"
                    column.HeaderText = "City"
                Case "CustomerState"
                    column.HeaderText = "State"
                Case "CustomerZip"
                    column.HeaderText = "Zip"
                Case "CustomerPhone"
                    column.HeaderText = "Phone"
                Case "QBId"
                    column.Visible = False
                Case Else

            End Select

        Next
    End Sub
    Public Sub CopyOrder()
        Dim CurrentOrderItems = From orderitems In d.OrderInfoDB.OrderItems Where orderitems.OrderID = d.CurrentOrder.OrderID
        Dim CopyOrder As New Order
        CopyOrder.Acres = 0.00
        CopyOrder.OrderDate = Now()
        CopyOrder.OrderID = Nothing
        CopyOrder.InvoiceID = Nothing
        CopyOrder.CustomerID = Nothing
        CopyOrder.Project = Nothing
        d.OrderInfoDB.Orders.InsertOnSubmit(CopyOrder)

        d.OrderInfoDB.SubmitChanges()

        For Each OrderItem In CurrentOrderItems
            Dim CopyOrderItem As New OrderItem
            CopyOrderItem.OrderID = CopyOrder.OrderID
            CopyOrderItem.PricePerAcre = Nothing
            CopyOrderItem.PricePerPLSLB = Nothing
            CopyOrderItem.PLSLbs = Nothing
            CopyOrderItem.BulkLbs = Nothing
            CopyOrderItem.ItemID = OrderItem.ItemID
            CopyOrderItem.PLS_Percent = OrderItem.PLS_Percent
            d.OrderInfoDB.OrderItems.InsertOnSubmit(CopyOrderItem)
        Next
        d.OrderInfoDB.SubmitChanges()
        d.CurrentOrder = CopyOrder
        ViewCurrentOrder()
        ReportViewer2.Reset()
        d.UserReports = New List(Of AvailableReport)
    End Sub

    Private Sub PriceListCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PriceListCB.SelectedIndexChanged
        d.CustomerPriceList = PriceListCB.Text
        If Not (d.CurrentOrder Is Nothing) Then
            d.CurrentOrder.PriceList = PriceListCB.Text
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PerformOrdersSearch()
    End Sub
    Private Sub AddItemInventory(ByVal Item As Item)
        Dim AddInventoryForm As New AddInventory(Item)
        AddInventoryForm.ShowDialog()
    End Sub

    Private Sub SaveOrderBtn_Click_1(sender As Object, e As EventArgs) Handles SaveOrderBtn.Click
        IsFillItems = False
        CreateUpdateOrder()
        For Each row As DataGridViewRow In OrderItemsGridView.Rows
            Dim MyOrderItemDetail As OrderItemDetails = row.DataBoundItem
            Dim MyOrderItems = From orderItems In d.OrderInfoDB.OrderItems Where orderItems.OrderItemID = MyOrderItemDetail.OrderItemID
            UpdateOrderItemInformation(MyOrderItems.Single)
        Next
    End Sub

    Private Sub NewBtn_Click_1(sender As Object, e As EventArgs) Handles NewBtn.Click
        NewOrder()
    End Sub

    Private Sub CopyOrderBtn_Click(sender As Object, e As EventArgs) Handles CopyOrderBtn.Click
        CopyOrder()
    End Sub

    Private Sub TypeFilterCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TypeFilterCB.SelectedIndexChanged
        If Not (IsLoading) Then
            PerformItemsFilter()
        End If
        ItemsSearchTB.Text = Nothing
    End Sub

    Private Sub OrderUnitsCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles OrderUnitsCB.SelectedIndexChanged
        If Not IsLoading Then
            d.CurrentOrderUnit = OrderUnitsCB.SelectedItem
            OrderItemsGridFormatting()
        End If

    End Sub

End Class
