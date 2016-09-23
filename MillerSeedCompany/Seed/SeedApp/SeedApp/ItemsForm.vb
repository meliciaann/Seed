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
    Public SeedReportsDB As New SeedReportsDataContext
    Public SeedReports As Table(Of SeedReport) = SeedReportsDB.GetTable(Of SeedReport)
    Public OrderInfoDB As New OrderItemsDataContext
    Public CustomersDB As New CustomersDataContext
    Public OrderStatusDB As New OrderStatusDataContext
    Public OrderItems As Table(Of OrderItem) = OrderInfoDB.GetTable(Of OrderItem)
    Public Customers As Table(Of Customer) = CustomersDB.GetTable(Of Customer)
    Public OrderStatus As Table(Of OrderStatus) = OrderStatusDB.GetTable(Of OrderStatus)
    Public Orders As Table(Of Order) = OrderInfoDB.GetTable(Of Order)
    Public CurrentOrder As New Order
    Public CurrentOrderItem As New OrderItemDetails
    Public CurrentCustomer As New SeedApp.Customer
    Public IsNewOrder As Boolean = False
    Public IsLoading As Boolean
    Public isNewItem As Boolean
    Public IsFillItems As Boolean
    Public CurrentReport As New SeedReport
    Private OrderItemsDataSource As New ReportDataSource
    Private SeedOrderDataSource As New ReportDataSource
    Private SeedOrderDetailSource As New ReportDataSource
    Public QB As New QBLib.QBLibrary

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
    End Sub

    Private Sub FillItemsData()
        IsLoading = True
        ItemsDataGridView.AutoGenerateColumns = True
        ItemsDataGridView.DataSource = Items
        IsLoading = False
    End Sub

    Private Sub FillCustomerData()
        IsLoading = True
        CustomerDataGridView.AutoGenerateColumns = True
        CustomerDataGridView.DataSource = Customers
        IsLoading = False
    End Sub
    Private Sub FillReports()
        ReportsCB.DataSource = SeedReports
        ReportsCB.ValueMember = "ReportID"
        ReportsCB.DisplayMember = "FriendlyName"
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
                              .CustomerZip = Customers.CustomerZip
                              }

        OrdersGridView.DataSource = OrdersQuery
        IsLoading = False
    End Sub
    Private Sub FillCustomerComboBox()
        CustomerCB.DataSource = Customers
        CustomerCB.DisplayMember = "CustomerName"
        CustomerCB.ValueMember = "CustomerID"
    End Sub
    Private Sub FillOrderStatusComboBox()
        OrderStatusCB.DataSource = OrderStatus
        OrderStatusCB.DisplayMember = "OrderStatusName"
        OrderStatusCB.ValueMember = "OrderStatusID"
    End Sub
    Private Sub FillOrderItems()
        'CreateUpdateOrder()
        IsFillItems = True
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
                                    .TotalPrice = orderitems.TotalPrice * CurrentOrder.Acres,
                                    .Distributor = OrderItemDetails.Distributor,
                                    .Wholesale = OrderItemDetails.Wholesale,
                                    .Retail = OrderItemDetails.Retail,
                                    .OrderItemID = orderitems.OrderItemID}
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
        End If

        If (OrdersPage.SelectedTab Is CustomerTabPage) Then
            IsLoading = True
            FillCustomerData()
            IsLoading = False
        End If
        If (OrdersPage.SelectedTab Is OrdersTabPage) Then
            IsLoading = True
            FillOrdersData()
            IsLoading = False
        End If

        If (OrdersPage.SelectedTab Is OrderTabPage) Then
            FillCustomerComboBox()
            FillOrderStatusComboBox()

        End If
        If (OrdersPage.SelectedTab Is ReportsTabPage) Then
            FillReports()
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
                    Case "PricePerPLSLB"
                        UpdateOrderItems = True
                    Case "PricePerAcre"
                        UpdateOrderItems = True
                    Case "PLSLBSPerAcre"
                        UpdateOrderItems = True
                    Case Else
                        UpdateOrderItems = False
                End Select
                CurrentOrderItem.PricePerAcre = CurrentOrderItem.PricePerPLSLB * CurrentOrderItem.PLSLBSPerAcre
                CurrentOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres
                UpdateDOrderItem.PricePerPLSLB = CurrentOrderItem.PricePerPLSLB
                UpdateDOrderItem.PricePerAcre = CurrentOrderItem.PricePerAcre
                UpdateDOrderItem.PLS_LBS_PerAcre = CurrentOrderItem.PLSLBSPerAcre
                UpdateDOrderItem.PricePerAcre = CurrentOrderItem.PricePerAcre
                UpdateDOrderItem.TotalPrice = CurrentOrderItem.TotalPrice
            Next
            If (UpdateOrderItems) Then
                OrderInfoDB.SubmitChanges()
            End If

        End If
    End Sub
    Private Sub OrderItemCellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles OrderItemsGridView.CellContentDoubleClick
        Dim SelectedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
        Dim SelectedCell As DataGridViewCell = OrderItemsGridView.CurrentCell
        CurrentOrderItem = SelectedRow.DataBoundItem
        Dim UpdateDOrderItems = From OrderItem In OrderInfoDB.OrderItems Where OrderItem.OrderItemID = CurrentOrderItem.OrderItemID
        For Each UpdateDOrderItem In UpdateDOrderItems
            Dim s As String = sender.columns(e.ColumnIndex).headercell.value
            Select Case s
                Case "Distributor"
                    UpdateDOrderItem.PricePerAcre = CurrentOrderItem.Distributor
                    UpdateDOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres
                Case "Retail"
                    UpdateDOrderItem.PricePerAcre = CurrentOrderItem.Retail
                    UpdateDOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres
                Case "Wholesale"
                    UpdateDOrderItem.PricePerAcre = CurrentOrderItem.Wholesale
                    UpdateDOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres
                Case "PricePerAcre"
                    UpdateDOrderItem.PricePerAcre = SelectedCell.Value
                    UpdateDOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres
                Case "TotalPrice"
                    UpdateDOrderItem.PricePerAcre = CurrentOrderItem.PricePerAcre
                    UpdateDOrderItem.TotalPrice = CurrentOrderItem.PricePerAcre * CurrentOrder.Acres
                Case Else

            End Select
        Next

        OrderInfoDB.SubmitChanges()
        FillOrderItems()



    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        UpdateCustomers()
        UpdateItems()
    End Sub

    Private Sub SaveOrderBtn_Click(sender As Object, e As EventArgs) Handles SaveOrderBtn.Click
        CreateUpdateOrder()
    End Sub

    Private Sub CreateUpdateOrder()
        OrderInfoDB.SubmitChanges()
        CurrentOrder.InvoiceID = InvoiceTB.Text
        CurrentOrder.CustomerID = CustomerCB.SelectedValue

        CurrentOrder.Acres = AcresTB.Text
        CurrentOrder.Project = ProjectTB.Text
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
        OrderDatePicker.Value = CurrentOrder.OrderDate.Value
        FillOrderItems()
        ReportViewer2.Reset()
    End Sub

    Private Sub PerformItemsSearch()
        Dim ItemsQuery = From items In ItemsDB.Items Where items.Item.Contains(ItemsSearchTB.Text) Or items.Item1.Contains(ItemsSearchTB.Text) Or items.Lot.Contains(ItemsSearchTB.Text) Or items.Item2.Contains(ItemsSearchTB.Text)

        ItemsDataGridView.DataSource = ItemsQuery

    End Sub

    Private Sub PerformCustomerSearch()
        Dim CustomersQuery = From Customers In CustomersDB.Customers Where Customers.CustomerName.Contains(CustomerSearchTB.Text)
        CustomerDataGridView.DataSource = CustomersQuery

    End Sub
    Private Sub PerformOrdersSearch()
        Dim OrdersQuery = From Orders In OrderInfoDB.Orders Where Orders.InvoiceID.Contains(OrdersSearchTB.Text) Or Orders.OrderID.ToString().Contains(OrdersSearchTB.Text)
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
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PerformItemsSearch()
    End Sub
    Private Sub SearchEnter(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles ItemsSearchTB.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            PerformItemsSearch()
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


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles EditItemsCB.CheckedChanged
        AllowItemEdits()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles EditCustomersCB.CheckedChanged
        AllowCustomerEdits()

    End Sub

    Private Sub NewBtn_Click(sender As Object, e As EventArgs) Handles NewBtn.Click
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
        'Me.ReportViewer2.LocalReport.DataSources.Clear()
        RefreshDataSources()
        'Me.ReportViewer2.Reset()
        If Not (ReportsCB.SelectedValue Is Nothing) Then
            Dim i3 = From SeedReports In SeedReportsDB.SeedReports Where SeedReports.ReportID = ReportsCB.SelectedValue.ToString()

            For Each SeedReport In i3
                CurrentReport = SeedReport
            Next

            GetReport()


            'Dim ReportDataSources As ReportDataSourceCollection = Me.ReportViewer2.LocalReport.DataSources()
            'For Each Ds As ReportDataSource In ReportDataSources
            '    ReportViewer2.LocalReport.DataSources.Add(Ds)
            'Next
            ReportViewer2.RefreshReport()
        End If
    End Sub
    Public Sub GetReport()

        Me.ReportViewer2.Dock = System.Windows.Forms.DockStyle.Fill
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
            Me.Controls.Add(ReportViewer2)
            Me.ReportViewer2.RefreshReport()
        End If
    End Sub

    Public Sub AllReportsSubreportProcessingEventHandler(ByVal sender As Object,
     ByVal e As SubreportProcessingEventArgs)

        e.DataSources.Add(OrderItemsDataSource)
        e.DataSources.Add(SeedOrderDataSource)
        e.DataSources.Add(SeedOrderDetailSource)
    End Sub

    Private Sub ReportsTabPage_Click(sender As Object, e As EventArgs) Handles ReportsTabPage.Click

    End Sub

    Private Sub OrderStatusCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles OrderStatusCB.SelectedIndexChanged
        If Not (OrderStatusCB.SelectedItem Is Nothing OrElse OrderStatusCB.SelectedItem.OrderStatusID = 0) Then
            CurrentOrder.OrderStatusId = OrderStatusCB.SelectedValue
            OrderInfoDB.SubmitChanges()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        QB.DoInvoiceAdd()


    End Sub
End Class
