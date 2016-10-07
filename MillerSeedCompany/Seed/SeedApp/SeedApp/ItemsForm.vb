Imports System.Data.Linq
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Reflection
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports QBLib.QBLibrary
Imports SeedGeneral


Public Class ItemsForm
    Public ItemsDB As New ItemsEditDataContext
    Public Items As Table(Of Item) = ItemsDB.GetTable(Of Item)
    Public SeedReportsDB As New SeedGeneral.SeedReportsDataContext
    Public SeedReports As Table(Of SeedReport) = SeedReportsDB.GetTable(Of SeedReport)
    Public CustomersDB As New CustomersDataContext
    Public OrderInfoDB As New OrderItemsDataContext
    Public OrderStatusDB As New OrderStatusDataContext
    Public InventoryDB As New InventoryDataContext
    Public OrderItems As Table(Of OrderItem) = OrderInfoDB.GetTable(Of OrderItem)
    Public Customers As Table(Of Customer) = CustomersDB.GetTable(Of Customer)
    Public GridviewMenu As New ContextMenu
    Public Inventory As Table(Of Inventory) = InventoryDB.GetTable(Of Inventory)
    Public OrderStatus As Table(Of OrderStatus) = OrderStatusDB.GetTable(Of OrderStatus)
    Public Orders As Table(Of Order) = OrderInfoDB.GetTable(Of Order)
    Public CurrentOrder As New Order
    Public CurrentOrderItem As New OrderItemDetails
    Public CurrentCustomer As New SeedGeneral.Customer
    Public IsNewOrder As Boolean = False
    Public IsLoading As Boolean
    Public isNewItem As Boolean
    Public IsFillItems As Boolean
    Public CurrentReport As New SeedReport
    Private OrderItemsDataSource As New ReportDataSource
    Private SeedOrderDataSource As New ReportDataSource
    Private SeedOrderDetailSource As New ReportDataSource
    Public QB As New QBLib.QBLibrary
    Public CustomerPriceList As String
    Public ShowInventoryMenuItem As New MenuItem
    Public AddInventoryMenuItem As New MenuItem
    Public ItemID As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'SeedDataSet.OrderItems' table. You can move, or remove it, as needed.
        Me.OrderItemsTableAdapter.Fill(Me.SeedDataSet.OrderItems)
        'TODO: This line of code loads data into the 'SeedDataSet.SeedOrder' table. You can move, or remove it, as needed.
        Me.SeedOrderTableAdapter.Fill(Me.SeedDataSet.SeedOrder, CurrentOrder.OrderID)
        'TODO: This line of code loads data into the 'SeedDataSet.SeedOrderDetail' table. You can move, or remove it, as needed.
        Me.SeedOrderDetailTableAdapter.Fill(Me.SeedDataSet.SeedOrderDetail, CurrentOrder.OrderID)
        IsNewOrder = False
        FillCustomerComboBox()
        FillOrderStatusComboBox()
        CustomerDataGridView.ReadOnly = True
        CustomerDataGridView.AllowUserToAddRows = False
        ItemsDataGridView.ReadOnly = True
        ItemsDataGridView.AllowUserToAddRows = False
        OrderDatePicker.Value = DateTime.Now

        ReportViewer1.RefreshReport()

        Me.ReportViewer2.RefreshReport()

        Me.ContextMenu = GridviewMenu
        ShowInventoryMenuItem.Text = "Show Inventory"
        GridviewMenu.MenuItems.Add(ShowInventoryMenuItem)
        AddInventoryMenuItem.Text = "Add Inventory"
        GridviewMenu.MenuItems.Add(AddInventoryMenuItem)

        OrderItemsGridFormatting()
        CustomerCB.Focus()
    End Sub

    Private Sub FillItemsData()
        IsLoading = True
        ItemsDataGridView.AutoGenerateColumns = True
        ItemsDataGridView.DataSource = Items

        Dim TypeFilter = From item In ItemsDB.Items
                         Select New ItemTypes With {
                            .ItemType = item.Type}
                         Distinct


        TypeFilterCB.DataSource = TypeFilter
        Dim i As Integer = TypeFilter.Count
        TypeFilterCB.DisplayMember = "ItemType"
        TypeFilterCB.ValueMember = "ItemType"
        TypeFilterCB.SelectedIndex = -1

        IsLoading = False
    End Sub
    Private Sub FilterItemsData()

    End Sub
    Private Sub FillCustomerData()
        IsLoading = True
        CustomerDataGridView.AutoGenerateColumns = True
        CustomerDataGridView.DataSource = Customers
        IsLoading = False
    End Sub
    Private Sub FillInventoryData(ByVal ItemID As Integer)

        InventoryDGV.AutoGenerateColumns = True
        Dim InventoryHistory = From Inventory In InventoryDB.Inventories Join InventoryItemDetail In InventoryDB.InventoryItemDetails On Inventory.ItemID Equals InventoryItemDetail.ItemID
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
        'ReportsCB.DataSource = SeedReports
        'ReportsCB.ValueMember = "ReportID"
        'ReportsCB.DisplayMember = "FriendlyName"
        Dim ListReports = From seedReports In SeedReportsDB.SeedReports Where seedReports.IsVisible = True
        ReportsCheckListBox.DataSource = ListReports
        ReportsCheckListBox.ValueMember = "ReportID"
        ReportsCheckListBox.DisplayMember = "FriendlyName"
    End Sub
    Private Sub FillOrdersData()
        IsLoading = True
        OrdersGridView.AutoGenerateColumns = True
        Dim OrdersQuery = From Orders In OrderInfoDB.Orders Join Customers In OrderInfoDB.OrderCustomerDetails On Orders.CustomerID Equals Customers.CustomerId
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
                              .PriceList = Orders.PriceList
                              }

        OrdersGridView.DataSource = OrdersQuery
        IsLoading = False
    End Sub
    Private Sub FillCustomerComboBox()
        CustomerCB.DataSource = Customers
        CustomerCB.DisplayMember = "CustomerName"
        CustomerCB.ValueMember = "CustomerID"
        CustomerCB.SelectedIndex = -1

    End Sub

    Private Sub FillOrderStatusComboBox()
        OrderStatusCB.DataSource = OrderStatus
        OrderStatusCB.DisplayMember = "OrderStatusName"
        OrderStatusCB.ValueMember = "OrderStatusID"
    End Sub
    Private Sub FillOrderItems()
        'CreateUpdateOrder()
        'IsFillItems = False
        Dim OrderTotal As Decimal? = 0.00
        Dim OrderPricePerAcre As Decimal? = 0.00

        Dim CurrentOrderItems = From orderitems In OrderInfoDB.OrderItems Join OrderItemDetails In OrderInfoDB.OrderItemDetails On orderitems.ItemID Equals OrderItemDetails.ItemID Where orderitems.OrderID.Equals(CurrentOrder.OrderID)
                                Select New OrderItemDetails With {
                                    .Lot = OrderItemDetails.Lot,
                                    .PLS = OrderItemDetails.PLS_,
                                    .Item = OrderItemDetails.Item,
                                    .PLSLBSPerAcre = orderitems.PLS_LBS_PerAcre,
                                    .PricePerPLSLB = orderitems.PricePerPLSLB,
                                    .PricePerAcre = orderitems.PricePerAcre,
                                    .TotalPrice = orderitems.PricePerPLSLB * CurrentOrder.Acres,
                                    .Distributor = OrderItemDetails.Distributor,
                                    .Wholesale = OrderItemDetails.Wholesale,
                                    .Retail = OrderItemDetails.Retail,
                                    .OrderItemID = orderitems.OrderItemID,
                                    .BulkLbs = orderitems.BulkLbs,
                                    .PLSLBS = orderitems.PLSLbs,
                                    .ItemID = orderitems.ItemID}
        OrderItemsGridView.DataSource = CurrentOrderItems





        For Each OrderItem In CurrentOrderItems
            OrderPricePerAcre = OrderPricePerAcre + OrderItem.PricePerAcre
        Next
        OrderTotal = OrderPricePerAcre * CurrentOrder.Acres
        If (OrderTotal Is Nothing) Then
            OrderTotal = 0.00
            OrderPricePerAcre = 0.00
        End If
        CurrentOrder.OrderTotal = OrderTotal
        CurrentOrder.TotalPricePerAcre = OrderPricePerAcre
        OrderTotalTB.Text = CurrentOrder.OrderTotal
        TotalPricePerAcreTB.Text = CurrentOrder.TotalPricePerAcre
        IsFillItems = False
    End Sub

    Private Sub UpdateItems()
        Dim SaveItem As MsgBoxResult
        SaveItem = MessageBox.Show("Would you like to save your changes?", "Save Changes?", MessageBoxButtons.YesNo)
        If (SaveItem.Yes) Then
            ItemsDB.SubmitChanges()
        End If
    End Sub

    Private Sub UpdateCustomers()
        Dim SaveItem As MsgBoxResult
        SaveItem = MessageBox.Show("Would you like to save your changes?", "Save Changes?", MessageBoxButtons.YesNo)
        If (SaveItem.Yes) Then
            CustomersDB.SubmitChanges()
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
            IsLoading = False
            ShowInventoryMenuItem.Visible = True
            AddInventoryMenuItem.Visible = True
        End If

        If (OrdersPage.SelectedTab Is CustomerTabPage) Then
            IsLoading = True
            FillCustomerData()
            IsLoading = False
            CustomerGridViewFormatting()

            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            CustomerSearchTB.Focus()
        End If
        If (OrdersPage.SelectedTab Is OrdersTabPage) Then
            IsLoading = True
            FillOrdersData()
            IsLoading = False
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            OrdersSearchTB.Focus()
        End If

        If (OrdersPage.SelectedTab Is OrderTabPage) Then
            'FillCustomerComboBox()
            FillOrderStatusComboBox()
            OrderItemsGridFormatting()
            ShowInventoryMenuItem.Visible = True
            AddInventoryMenuItem.Visible = False
            CustomerCB.Focus()
        End If
        If (OrdersPage.SelectedTab Is ReportsTabPage) Then
            FillReports()
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
        End If


    End Sub
    Private Sub RefreshData()
        Customers = CustomersDB.Customers

        CustomerDataGridView.DataSource = Customers

        Items = ItemsDB.Items
        ItemsDataGridView.DataSource = Items

    End Sub
    Private Sub UpdateAcres(sender As Object, e As System.EventArgs) Handles AcresTB.Leave
        CurrentOrder.Acres = AcresTB.Text

    End Sub
    Private Sub PricePerAcreUpdate(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles OrderItemsGridView.CellLeave
        If (IsFillItems = False) Then
            Dim SelectedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
            OrderItemsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
            CurrentOrderItem = SelectedRow.DataBoundItem
            Dim UpdateDOrderItems = From OrderItem In OrderInfoDB.OrderItems Where OrderItem.OrderItemID = CurrentOrderItem.OrderItemID
            Dim s As String = sender.columns(e.ColumnIndex).headercell.value
            Dim UpdateOrderItems As Boolean = False
            For Each UpdateDOrderItem In UpdateDOrderItems
                Select Case s
                    Case "PLS LBS Per Acre"
                        UpdateOrderItems = True
                        UpdateDOrderItem.PLS_LBS_PerAcre = CurrentOrderItem.PLSLBSPerAcre
                    Case "Price Per PLS LBS"
                        UpdateOrderItems = True
                        UpdateDOrderItem.PricePerPLSLB = CurrentOrderItem.PricePerPLSLB
                    Case Else
                        UpdateOrderItems = False
                End Select
                UpdateDOrderItem.PLS_Percent = CurrentOrderItem.PLS
                UpdateOrderItemInformation(UpdateDOrderItem)
                CurrentOrderItem.PricePerAcre = UpdateDOrderItem.PricePerAcre
                CurrentOrderItem.TotalPrice = UpdateDOrderItem.TotalPrice
                CurrentOrderItem.BulkLbs = UpdateDOrderItem.BulkLbs
                CurrentOrderItem.PLSLBS = UpdateDOrderItem.PLSLbs
                OrderItemsGridView.Refresh()
            Next
        End If
    End Sub
    Public Sub UpdateOrderItemInformation(ByVal MyCurrentOrderItem As OrderItem)
        MyCurrentOrderItem.PricePerAcre = MyCurrentOrderItem.PricePerPLSLB * MyCurrentOrderItem.PLS_LBS_PerAcre
        MyCurrentOrderItem.TotalPrice = MyCurrentOrderItem.PricePerAcre * CurrentOrder.Acres
        MyCurrentOrderItem.BulkLbs = (MyCurrentOrderItem.PLS_LBS_PerAcre * CurrentOrder.Acres) / MyCurrentOrderItem.PLS_Percent
        MyCurrentOrderItem.PLSLbs = MyCurrentOrderItem.PLS_LBS_PerAcre * CurrentOrder.Acres
        OrderInfoDB.SubmitChanges()
    End Sub


    'Private Sub OrderItemCellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles OrderItemsGridView.CellContentDoubleClick
    '    Dim SelectedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
    '    Dim SelectedCell As DataGridViewCell = OrderItemsGridView.CurrentCell
    '    CurrentOrderItem = SelectedRow.DataBoundItem
    '    Dim UpdateDOrderItems = From OrderItem In OrderInfoDB.OrderItems Where OrderItem.OrderItemID = CurrentOrderItem.OrderItemID
    '    For Each UpdateDOrderItem In UpdateDOrderItems
    '        Dim s As String = sender.columns(e.ColumnIndex).headercell.value
    '        Select Case s
    '            Case "Distributor"
    '                UpdateDOrderItem.PricePerPLSLB = CurrentOrderItem.Distributor
    '                UpdateDOrderItem.PricePerAcre = CurrentOrderItem.PricePerPLSLB * CurrentOrderItem.PLSLBSPerAcre
    '                UpdateDOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres
    '            Case "Retail"
    '                UpdateDOrderItem.PricePerPLSLB = CurrentOrderItem.Retail
    '                UpdateDOrderItem.PricePerAcre = CurrentOrderItem.PricePerPLSLB * CurrentOrderItem.PLSLBSPerAcre
    '                UpdateDOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres
    '            Case "Wholesale"
    '                UpdateDOrderItem.PricePerPLSLB = CurrentOrderItem.Wholesale
    '                UpdateDOrderItem.PricePerAcre = CurrentOrderItem.PricePerPLSLB * CurrentOrderItem.PLSLBSPerAcre
    '                UpdateDOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres
    '            Case "PricePerPLSLB"
    '                UpdateDOrderItem.PricePerPLSLB = SelectedCell.Value
    '                UpdateDOrderItem.PricePerAcre = CurrentOrderItem.PricePerPLSLB * CurrentOrderItem.PLSLBSPerAcre
    '                UpdateDOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres
    '            Case "Total Price"
    '                UpdateDOrderItem.PricePerPLSLB = CurrentOrderItem.PricePerPLSLB
    '                UpdateDOrderItem.PricePerAcre = CurrentOrderItem.PricePerPLSLB * CurrentOrderItem.PLSLBSPerAcre
    '                UpdateDOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres

    '            Case Else

    '        End Select
    '    Next

    '    OrderInfoDB.SubmitChanges()
    '    FillOrderItems()



    'End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        UpdateCustomers()
        UpdateItems()
    End Sub

    Private Sub SaveOrderBtn_Click(sender As Object, e As EventArgs)
        CreateUpdateOrder()
    End Sub

    Private Sub CreateUpdateOrder()
        OrderInfoDB.SubmitChanges()
        CurrentOrder.InvoiceID = InvoiceTB.Text
        CurrentOrder.CustomerID = CustomerCB.SelectedValue

        CurrentOrder.Acres = AcresTB.Text
        CurrentOrder.Project = ProjectTB.Text
        CurrentOrder.PriceList = PriceListCB.Text
        CurrentOrder.OrderStatusId = OrderStatusCB.SelectedValue
        CurrentOrder = CurrentOrder
        CurrentOrder.OrderDate = OrderDatePicker.Value
        If CurrentOrder.OrderID = 0 And IsNewOrder Then
            OrderInfoDB.Orders.InsertOnSubmit(CurrentOrder)
        Else
            Dim CurrentCustomerList = From customer In CustomersDB.Customers Where customer.CustomerId = CurrentOrder.CustomerID
            If (CurrentCustomerList.Count = 1) Then
                For Each Customer In CurrentCustomerList
                    CurrentCustomer = Customer
                    If (CustomerPriceList Is Nothing OrElse CustomerPriceList = "") Then
                        CustomerPriceList = QB.DoPriceListQuery(CurrentCustomer.CustomerName, "NameList")
                        PriceListCB.Text = CustomerPriceList
                    End If
                Next
            End If
        End If
        OrderInfoDB.SubmitChanges()
        OrderIDTB.Text = CurrentOrder.OrderID
        FillOrderItems()
    End Sub
    Private Sub ViewCurrentOrder()
        InvoiceTB.Text = CurrentOrder.InvoiceID
        CustomerCB.SelectedValue = CurrentOrder.CustomerID
        AcresTB.Text = CurrentOrder.Acres
        ProjectTB.Text = CurrentOrder.Project
        PriceListCB.Text = CurrentOrder.PriceList
        OrderDatePicker.Value = CurrentOrder.OrderDate.Value
        FillOrderItems()
        ReportViewer2.Reset()
    End Sub

    Private Sub PerformItemsSearch()
        Dim ItemsQuery = From items In ItemsDB.Items Where items.Item.Contains(ItemsSearchTB.Text) Or items.Item1.Contains(ItemsSearchTB.Text) Or items.Lot.Contains(ItemsSearchTB.Text) Or items.Item2.Contains(ItemsSearchTB.Text)

        ItemsDataGridView.DataSource = ItemsQuery

    End Sub
    Private Sub PerformItemsFilter()
        Dim ItemsQuery = From items In ItemsDB.Items Where items.Type = TypeFilterCB.Text

        ItemsDataGridView.DataSource = ItemsQuery

    End Sub

    Private Sub PerformCustomerSearch()
        Dim CustomersQuery = From Customers In CustomersDB.Customers Where Customers.CustomerName.Contains(CustomerSearchTB.Text)
        CustomerDataGridView.DataSource = CustomersQuery
    End Sub

    Private Sub PerformOrdersSearch()
        Dim OrdersQuery = From Orders In OrderInfoDB.Orders Where Orders.InvoiceID.Contains(OrdersSearchTB.Text) Or Orders.OrderID.ToString().Contains(OrdersSearchTB.Text) Or Orders.Project.ToString().Contains(OrdersSearchTB.Text)
        OrdersGridView.DataSource = OrdersQuery
    End Sub
    Private Sub UserDeletingRow(ByVal sender As Object,
    ByVal e As DataGridViewRowCancelEventArgs) _
    Handles OrderItemsGridView.UserDeletingRow

        Dim DeletedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
        Dim i As OrderItemDetails = DeletedRow.DataBoundItem
        Dim i2 = From OrderItem In OrderInfoDB.OrderItems Where OrderItem.OrderItemID = i.OrderItemID
        Dim r As Integer = i2.Count
        For Each OrderItem In i2
            OrderInfoDB.OrderItems.DeleteOnSubmit(OrderItem)
        Next

    End Sub
    Private Sub AddItemsToOrder()
        IsNewOrder = True
        CreateUpdateOrder()

        For Each row As DataGridViewRow In ItemsDataGridView.SelectedRows
            Dim currentItem As New Item
            currentItem = row.DataBoundItem
            Dim SelectedItem As New OrderItem
            SelectedItem.ItemID = currentItem.ItemID
            SelectedItem.OrderID = CurrentOrder.OrderID
            OrderInfoDB.OrderItems.InsertOnSubmit(SelectedItem)
            If (Not CustomerPriceList Is Nothing) Then
                Select Case CustomerPriceList
                    Case "Distributor"
                        SelectedItem.PricePerPLSLB = currentItem.Distributor
                    Case "Retail"
                        SelectedItem.PricePerPLSLB = currentItem.Retail
                    Case "Wholesale"
                        SelectedItem.PricePerPLSLB = currentItem.Wholesale
                End Select
            End If
            'OrderInfoDB.SubmitChanges()
        Next
        OrderInfoDB.SubmitChanges()
        ItemsDataGridView.ClearSelection()
        FillOrderItems()
    End Sub
    Private Sub NewOrder()
        CurrentOrder = New Order
        InvoiceTB.Text = Nothing
        CustomerCB.SelectedItem = Nothing
        OrderStatusCB.SelectedItem = Nothing
        AcresTB.Text = 0.00
        OrderTotalTB.Text = 0.00
        ProjectTB.Text = Nothing
        CreateUpdateOrder()
        OrderItemsGridView.DataSource = Nothing
        OrderDatePicker.Value = DateTime.Now
        IsNewOrder = False
        CustomerPriceList = Nothing
        QB = New QBLib.QBLibrary
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
        ItemsSearchTB.Text = Nothing
        ItemsSearchTB.Focus()
        IsLoading = False
    End Sub
    Private Sub DoubleClickOrdersGrid(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles OrdersGridView.CellMouseDoubleClick
        For Each row As DataGridViewRow In OrdersGridView.SelectedRows
            Dim SelectedOrder As OrderDetails = row.DataBoundItem
            Dim i2 = From Orders In OrderInfoDB.Orders Where Orders.OrderID = SelectedOrder.OrderID
            For Each order In i2
                CurrentOrder = order
            Next

        Next
        ViewCurrentOrder()
        OrdersPage.SelectedTab = OrderTabPage
    End Sub
    Private Sub DoubleClickCustomersGrid(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles CustomerDataGridView.CellMouseDoubleClick

        If (CurrentOrder.OrderID > 0) Then
            NewOrder()
        End If
        For Each row As DataGridViewRow In CustomerDataGridView.SelectedRows
            CurrentCustomer = row.DataBoundItem
        Next
        CustomerCB.SelectedItem = CurrentCustomer
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
            Dim i2 = From Orders In OrderInfoDB.Orders Where Orders.OrderID = SelectedOrder.OrderID
            For Each order In i2
                CurrentOrder = order
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
            EditItemsCB.Text = "Save"
        End If


        If EditItemsCB.Checked = False Then
            ItemsDataGridView.AllowUserToAddRows = False
            ItemsDataGridView.ReadOnly = True
            UpdateItems()
            EditItemsCB.Text = "Edit"
        End If

    End Sub
    Public Sub RefreshDataSources()

        'TODO: This line of code loads data into the 'SeedDataSet.OrderItems' table. You can move, or remove it, as needed.
        Me.OrderItemsTableAdapter.Fill(Me.SeedDataSet.OrderItems)
        'TODO: This line of code loads data into the 'SeedDataSet.SeedOrder' table. You can move, or remove it, as needed.
        Me.SeedOrderTableAdapter.Fill(Me.SeedDataSet.SeedOrder, CurrentOrder.OrderID)
        'TODO: This line of code loads data into the 'SeedDataSet.SeedOrderDetail' table. You can move, or remove it, as needed.
        Me.SeedOrderDetailTableAdapter.Fill(Me.SeedDataSet.SeedOrderDetail, CurrentOrder.OrderID)
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        RefreshDataSources()
        If (ReportsCheckListBox.CheckedIndices.Count > 0) Then

            If (ReportsCheckListBox.CheckedIndices.Count = 1) Then
                Dim i As SeedReport = ReportsCheckListBox.CheckedItems(0)
                Dim i3 = From SeedReports In SeedReportsDB.SeedReports Where SeedReports.ReportID = i.ReportID
                CurrentReport = i3.Single
            Else
                Dim i3 = From SeedReports In SeedReportsDB.SeedReports Where SeedReports.HasSubReports = True
                CurrentReport = i3.Single
            End If
            GetReport()

            ReportViewer2.RefreshReport()
        End If
        'If Not (ReportsCB.SelectedValue Is Nothing) Then
        '    Dim i3 = From SeedReports In SeedReportsDB.SeedReports Where SeedReports.ReportID = ReportsCB.SelectedValue.ToString()

        '    For Each SeedReport In i3
        '        CurrentReport = SeedReport
        '    Next

        '    GetReport()

        '    ReportViewer2.RefreshReport()
        'End If
    End Sub
    Public Sub GetReport()

        'Me.ReportViewer2.Dock = System.Windows.Forms.DockStyle.Fill
        OrderItemsDataSource.Name = "OrderItems"
        OrderItemsDataSource.Value = Me.OrderItemsBindingSource
        SeedOrderDataSource.Name = "SeedOrder"
        SeedOrderDataSource.Value = Me.SeedOrderBindingSource
        SeedOrderDetailSource.Name = "SeedOrderDetail"
        SeedOrderDetailSource.Value = Me.SeedOrderDetailBindingSource

        Me.ReportViewer2.Reset()
        Me.ReportViewer2.LocalReport.ReportPath = CurrentReport.ReportFileName
        Me.ReportViewer2.LocalReport.DataSources.Add(OrderItemsDataSource)
        Me.ReportViewer2.LocalReport.DataSources.Add(SeedOrderDataSource)
        Me.ReportViewer2.LocalReport.DataSources.Add(SeedOrderDetailSource)
        Me.ReportViewer2.LocalReport.ReportEmbeddedResource = My.Application.Info.AssemblyName + "." + CurrentReport.ReportFileName
        Me.ReportViewer2.Name = "ReportViewer2"
        If (CurrentReport.HasSubReports) Then
            Me.ReportViewer2.ProcessingMode = ProcessingMode.Local
            AddHandler Me.ReportViewer2.LocalReport.SubreportProcessing, AddressOf AllReportsSubreportProcessingEventHandler
            'Me.Controls.Add(ReportViewer2)

            Dim paramList As New List(Of ReportParameter)
            Dim param As New ReportParameter
            For Each itemChecked As SeedReport In ReportsCheckListBox.CheckedItems
                param.Values.Add(itemChecked.ReportID)
            Next
            param.Name = "SubReportsVisible"
            ReportViewer2.LocalReport.SetParameters(param)
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
        If Not (OrderStatusCB.SelectedItem Is Nothing OrElse OrderStatusCB.SelectedItem.OrderStatusID = 0) Then
            CurrentOrder.OrderStatusId = OrderStatusCB.SelectedValue
            OrderInfoDB.SubmitChanges()
        End If
    End Sub
    Public Sub CreateInvoice()
        Dim OrdersQuery = From Orders In OrderInfoDB.Orders Join Customers In OrderInfoDB.OrderCustomerDetails On Orders.CustomerID Equals Customers.CustomerId Where Orders.OrderID = CurrentOrder.OrderID
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
                              .PricePerAcre = Orders.TotalPricePerAcre
                              }

        Dim OrdersItemsQuery = From OrderItem In OrderInfoDB.OrderItems Join OrderItemDetails In OrderInfoDB.OrderItemDetails On OrderItem.ItemID Equals OrderItemDetails.ItemID Where OrderItem.OrderID = CurrentOrder.OrderID
                               Select New OrderItemDetails With {
                                .PricePerAcre = OrderItem.PricePerAcre,
                                   .Lot = OrderItemDetails.Lot
                              }
        Dim OrderItems = OrdersItemsQuery.ToArray()
        Dim Order = OrdersQuery.ToArray()
        Dim s As String = TypeName(OrderItems)
        Dim t As String = TypeName(Order)


        CurrentOrder.InvoiceID = QB.QBCreateInvoice(Order, OrderItems)
        OrderInfoDB.SubmitChanges()
        InvoiceTB.Text = CurrentOrder.InvoiceID
    End Sub
    Public Sub UpdateInventory()

        Dim CurrentOrderItems = From orderitems In OrderInfoDB.OrderItems Join OrderItemDetails In OrderInfoDB.OrderItemDetails On orderitems.ItemID Equals OrderItemDetails.ItemID Where orderitems.OrderID.Equals(CurrentOrder.OrderID)
                                Select New OrderItemDetails With {
                                    .Lot = OrderItemDetails.Lot,
                                    .PLS = OrderItemDetails.PLS_,
                                    .Item = OrderItemDetails.Item,
                                    .PLSLBSPerAcre = orderitems.PLS_LBS_PerAcre,
                                    .PricePerPLSLB = orderitems.PricePerPLSLB,
                                    .PricePerAcre = orderitems.PricePerAcre,
                                    .TotalPrice = orderitems.TotalPrice * CurrentOrder.Acres,
                                    .Distributor = OrderItemDetails.Distributor,
                                    .Wholesale = OrderItemDetails.Wholesale,
                                    .Retail = OrderItemDetails.Retail,
                                    .OrderItemID = orderitems.OrderItemID,
                                    .BulkLbs = orderitems.BulkLbs,
                                    .PLSLBS = orderitems.PLSLbs,
                                    .ItemID = orderitems.ItemID}
        OrderItemsGridView.DataSource = CurrentOrderItems

        Dim InventoryItems = From inventories In InventoryDB.Inventories Where inventories.InvoiceID = CurrentOrder.InvoiceID

        If InventoryItems.Count = 0 Then
            For Each orderitem In CurrentOrderItems
                Dim tmpInventory As New Inventory
                tmpInventory.InventoryDate = CurrentOrder.OrderDate
                tmpInventory.Quantity = orderitem.BulkLbs * -1.0
                tmpInventory.InvoiceID = CurrentOrder.InvoiceID
                tmpInventory.Memo = "Order " + CurrentOrder.OrderID.ToString
                tmpInventory.ItemID = orderitem.ItemID
                InventoryDB.Inventories.InsertOnSubmit(tmpInventory)
            Next
            InventoryDB.SubmitChanges()
        End If
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If CurrentOrder.InvoiceID Is Nothing OrElse CurrentOrder.InvoiceID = "" Then
            CreateInvoice()

        End If
        UpdateInventory()
    End Sub

    Private Sub CustomerCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CustomerCB.SelectedIndexChanged
        PriceListCB.Text = Nothing
        CustomerPriceList = Nothing
        CurrentCustomer = CustomerCB.SelectedItem
    End Sub

    Private Sub RightClickItemsGridview(sender As Object, e As MouseEventArgs) Handles ItemsDataGridView.MouseClick
        If (e.Button = MouseButtons.Right) Then
            AddHandler ShowInventoryMenuItem.Click, AddressOf Me.Menu_Click_ShowItems
            AddHandler AddInventoryMenuItem.Click, AddressOf Me.Menu_Click_AddItems
        End If
    End Sub


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
    Private Sub RightClickOrderItemsGridview(sender As Object, e As MouseEventArgs) Handles OrderItemsGridView.MouseClick
        If (e.Button = MouseButtons.Right) Then

            AddHandler ShowInventoryMenuItem.Click, AddressOf Me.Menu_Click_OrderShowItems

        End If
    End Sub
    Private Sub Menu_Click_OrderShowItems(ByVal sender As Object, ByVal e As System.EventArgs)
        IsLoading = True
        Dim SelectedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
        Dim Item As OrderItemDetails = SelectedRow.DataBoundItem
        FillInventoryData(Item.ItemID)
        ItemID = Item.ItemID
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
    Public Sub OrderItemsGridFormatting()
        For Each column As DataGridViewColumn In OrderItemsGridView.Columns
            Select Case column.HeaderCell.Value
                Case "PLS"
                    column.HeaderText = "PLS %"
                Case "PLSLBSPerAcre"
                    column.HeaderText = "PLS LBS Per Acre"
                Case "PricePerPLSLB"
                    column.HeaderText = "Price Per PLS LBS"
                Case "PricePerAcre"
                    column.HeaderText = "Price Per Acre"
                Case "TotalPrice"
                    column.HeaderText = "Total Price"
                Case "OrderItemID"
                    column.Visible = False
                Case "BulkLbs"
                    column.HeaderText = "Bulk LBS"
                Case "PLSLBS"
                    column.HeaderText = "PLS LBS"
                Case "ItemID"
                    column.Visible = False
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
                    column.HeaderText="Name"
                    Case "CustomerAddressLine1"
                    column.HeaderText="Address Line 1"
                    Case "CustomerAddressLine2"
                    column.HeaderText = "Address Line 2"
                Case "CustomerCity"
                    column.HeaderText="City"
                Case "CustomerState"
                    column.HeaderText="State"
                Case "CustomerZip"
                    column.HeaderText = "Zip"
                Case "QBId"
                    column.Visible = False
                Case Else

            End Select

        Next
    End Sub
    Public Sub CopyOrder()
        Dim CurrentOrderItems = From orderitems In OrderInfoDB.OrderItems Where orderitems.OrderID = CurrentOrder.OrderID
        Dim CopyOrder As New Order
        CopyOrder.Acres = 0.00
        CopyOrder.OrderDate = Now()
        CopyOrder.OrderID = Nothing
        CopyOrder.InvoiceID = Nothing
        CopyOrder.CustomerID = Nothing
        CopyOrder.Project = Nothing
        OrderInfoDB.Orders.InsertOnSubmit(CopyOrder)

        OrderInfoDB.SubmitChanges()

        For Each OrderItem In CurrentOrderItems
            Dim CopyOrderItem As New OrderItem
            CopyOrderItem.OrderID = CopyOrder.OrderID
            CopyOrderItem.PricePerAcre = Nothing
            CopyOrderItem.PricePerPLSLB = Nothing
            CopyOrderItem.PLSLbs = Nothing
            CopyOrderItem.BulkLbs = Nothing
            CopyOrderItem.ItemID = OrderItem.ItemID
            CopyOrderItem.PLS_Percent = OrderItem.PLS_Percent
            OrderInfoDB.OrderItems.InsertOnSubmit(CopyOrderItem)
        Next
        OrderInfoDB.SubmitChanges()
        CurrentOrder = CopyOrder
        ViewCurrentOrder()
    End Sub

    Private Sub PriceListCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PriceListCB.SelectedIndexChanged
        CustomerPriceList = PriceListCB.Text
        CurrentOrder.PriceList = PriceListCB.Text
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
            Dim MyOrderItems = From orderItems In OrderInfoDB.OrderItems Where orderItems.OrderItemID = MyOrderItemDetail.OrderItemID
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

    End Sub
End Class
