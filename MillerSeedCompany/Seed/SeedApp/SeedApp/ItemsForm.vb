Imports System.Data.Linq
Imports Microsoft.Reporting.WinForms

Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class ItemsForm
    Public ItemsDB As New ItemsEditDataContext
    Public Items As Table(Of Item) = ItemsDB.GetTable(Of Item)
    Public OrderInfoDB As New OrderItemsDataContext
    Public CustomersDB As New CustomersDataContext
    Public OrderItems As Table(Of OrderItem) = OrderInfoDB.GetTable(Of OrderItem)
    Public Customers As Table(Of Customer) = CustomersDB.GetTable(Of Customer)
    Public Orders As Table(Of Order) = OrderInfoDB.GetTable(Of Order)
    Public CurrentOrder As New Order
    Public CurrentOrderItem As New OrderItemDetails
    Public IsLoading As Boolean
    Public isNewItem As Boolean
    Public IsFillItems As Boolean


    Public Class OrderItemDetails
        Public Property Lot As String
        Public Property PLS As String
        Public Property Item As String
        Public Property PricePerAcre As Decimal?
        Public Property TotalPrice As Decimal?
        Public Property Distributor As Decimal
        Public Property Wholesale As Decimal
        Public Property Retail As Decimal
        Public Property OrderItemID As Integer
    End Class
    Public Class OrderDetails
        Public Property OrderID As Integer
        Public Property Orders As Integer
        Public Property InvoiceID As String
        Public Property Project As String
        Public Property OrderDate As DateTime = "1/1/1900"
        Public Property OrderTotal As Decimal?
        Public Property CustomerName As String
        Public Property CustomerAddressLine1 As String
        Public Property CustomerCity As String
        Public Property CustomerState As String

        Public CustomerZip As String

    End Class


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillCustomerComboBox()
        CustomerDataGridView.ReadOnly = True
        ItemsDataGridView.ReadOnly = True
        OrderDatePicker.Value = DateTime.Now

        ReportViewer1.RefreshReport()
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
    Private Sub FillOrderItems()
        'CreateUpdateOrder()
        IsFillItems = True
        Dim OrderTotal As Decimal? = 0.00

        Dim CurrentOrderItems = From orderitems In OrderInfoDB.OrderItems Join OrderItemDetails In OrderInfoDB.OrderItemDetails On orderitems.ItemID Equals OrderItemDetails.ItemID Where orderitems.OrderID.Equals(CurrentOrder.OrderID)
                                Select New OrderItemDetails With {
                                    .Lot = OrderItemDetails.Lot,
                                    .PLS = OrderItemDetails.PLS_,
                                    .Item = OrderItemDetails.Item,
                                    .PricePerAcre = orderitems.PricePerAcre,
                                    .TotalPrice = orderitems.TotalPrice * CurrentOrder.Acres,
                                    .Distributor = OrderItemDetails.Distributor,
                                    .Wholesale = OrderItemDetails.Wholesale,
                                    .Retail = OrderItemDetails.Retail,
                                    .OrderItemID = orderitems.OrderItemID}

        OrderItemsGridView.DataSource = CurrentOrderItems
        For Each OrderItem In CurrentOrderItems
            OrderTotal = OrderTotal + OrderItem.TotalPrice
        Next
        If (OrderTotal Is Nothing) Then
            OrderTotal = 0.00
        End If
        CurrentOrder.OrderTotal = OrderTotal
        OrderTotalTB.Text = CurrentOrder.OrderTotal
        IsFillItems = False
    End Sub

    'Private Sub dataGridView1_NewRowNeeded(ByVal sender As Object,
    '    ByVal e As DataGridViewRowEventArgs) _
    '    Handles ItemsDataGridView.NewRowNeeded

    '    isNewItem = True
    'End Sub
    'Private Sub ItemsRowAdded(ByVal sender As Object, ByVal e As DataGridViewRowEventArgs) Handles ItemsDataGridView.UserAddedRow
    '    isNewItem = True
    'End Sub


    'Private Sub ItemsRowChanged(ByVal sender As Object,
    '    ByVal e As DataGridViewCellEventArgs) _
    '    Handles ItemsDataGridView.RowLeave

    '    If isNewItem Then
    '        Dim i As Item
    '        i = sender.CurrentRow.DataBoundItem
    '        If (i IsNot Nothing) Then
    '            i.ItemID = System.Guid.NewGuid()
    '            ItemsDB.Items.InsertOnSubmit(i)
    '            isNewItem = False
    '        End If
    '    End If

    '    If (ItemsDataGridView.IsCurrentRowDirty = True AndAlso IsLoading = False) Then
    '        UpdateItems()

    '    End If

    'End Sub

    'Private Sub CustomersRowChanged(ByVal sender As Object,
    '    ByVal e As DataGridViewCellEventArgs) _
    '    Handles CustomerDataGridView.RowLeave
    '    If (CustomerDataGridView.IsCurrentRowDirty = True AndAlso IsLoading = False) Then
    '        UpdateCustomers()
    '    End If

    'End Sub
    'Private Sub CustomersRowAdded(ByVal sender As Object,
    '    ByVal e As DataGridViewRowEventArgs) Handles CustomerDataGridView.UserAddedRow
    '    If (CustomerDataGridView.IsCurrentRowDirty = True AndAlso IsLoading = False) Then
    '        UpdateCustomers()
    '    End If

    'End Sub
    ''Private Sub ItemsInsertRow(ByVal sender As Object,ByVal e As DataGridViewRowsAddedEventArgs) Handles ItemsDataGridView.rows
    'Private Sub ItemsRowsAdded(ByVal sender As Object,
    'ByVal e As DataGridViewRowsAddedEventArgs) _
    'Handles ItemsDataGridView.RowsAdded
    '    IsLoading = True
    '    If (IsLoading = False) Then
    '        Dim i As Item
    '        i = ItemsDataGridView.CurrentRow.DataBoundItem
    '        ItemsDB.Items.InsertOnSubmit(i)
    '        UpdateItems()
    '    End If
    '    IsLoading = False
    'End Sub

    Private Sub CustomersRowsAdded(ByVal sender As Object,
    ByVal e As DataGridViewRowsAddedEventArgs) _
    Handles CustomerDataGridView.RowsAdded
        If (IsLoading = False) Then
            UpdateCustomers()
        End If
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
            'CustomerDataGridView.EndEdit()
            OrderInfoDB.SubmitChanges()
        End If
    End Sub
    Private Sub TabChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles OrdersPage.SelectedIndexChanged
        CreateUpdateOrder()


        If (OrdersPage.SelectedTab Is ItemTabPage) Then
            IsLoading = True
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
    'Private Sub PricePerAcreUpdate(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles OrderItemsGridView.CellLeave
    '    If (IsFillItems = False) Then
    '        Dim SelectedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
    '        CurrentOrderItem = SelectedRow.DataBoundItem
    '        Dim UpdateDOrderItems = From OrderItem In OrderInfoDB.OrderItems Where OrderItem.OrderItemID = CurrentOrderItem.OrderItemID
    '        For Each UpdateDOrderItem In UpdateDOrderItems
    '            UpdateDOrderItem.TotalPrice = UpdateDOrderItem.PricePerAcre * CurrentOrder.Acres
    '        Next
    '        OrderInfoDB.SubmitChanges()
    '        FillOrderItems()
    '    End If
    'End Sub

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
                    UpdateDOrderItem.TotalPrice = UpdateDOrderItem.PricePerAcre * CurrentOrder.Acres
                Case "Retail"
                    UpdateDOrderItem.PricePerAcre = CurrentOrderItem.Retail
                    UpdateDOrderItem.TotalPrice = UpdateDOrderItem.PricePerAcre * CurrentOrder.Acres
                Case "Wholesale"
                    UpdateDOrderItem.PricePerAcre = CurrentOrderItem.Wholesale
                    UpdateDOrderItem.TotalPrice = UpdateDOrderItem.PricePerAcre * CurrentOrder.Acres
                Case "PricePerAcre"
                    UpdateDOrderItem.PricePerAcre = SelectedCell.Value
                    UpdateDOrderItem.TotalPrice = UpdateDOrderItem.PricePerAcre * CurrentOrder.Acres
                Case "TotalCost"
                    UpdateDOrderItem.PricePerAcre = CurrentOrderItem.PricePerAcre
                    UpdateDOrderItem.TotalPrice = UpdateDOrderItem.PricePerAcre * CurrentOrder.Acres
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
        CurrentOrder = CurrentOrder
        CurrentOrder.OrderDate = OrderDatePicker.Value
        If CurrentOrder.OrderID = 0 Then
            OrderInfoDB.Orders.InsertOnSubmit(CurrentOrder)
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
        Dim OrdersQuery = From Orders In OrderInfoDB.Orders Where Orders.InvoiceID.Contains(OrdersSearchTB.Text)
        OrdersGridView.DataSource = OrdersQuery
    End Sub
    Private Sub UserDeletingRow(ByVal sender As Object,
    ByVal e As DataGridViewRowCancelEventArgs) _
    Handles OrderItemsGridView.UserDeletingRow

        Dim DeletedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
        Dim i As OrderItem = DeletedRow.DataBoundItem
        Dim i2 = From OrderItem In OrderInfoDB.OrderItems Where OrderItem.OrderItemID = i.OrderItemID

        Dim r As Integer = i2.Count
        For Each OrderItem In i2
            OrderInfoDB.OrderItems.DeleteOnSubmit(OrderItem)
        Next

    End Sub
    Private Sub AddItemsToOrder()
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
        AcresTB.Text = 0.00
        OrderTotalTB.Text = 0.00
        ProjectTB.Text = Nothing
        CreateUpdateOrder()
        OrderItemsGridView.DataSource = Nothing
        OrderDatePicker.Value = DateTime.Now

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PerformItemsSearch()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AddItemsToOrder()
        OrdersPage.SelectedTab = OrderTabPage
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        UpdateItems()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        UpdateCustomers()
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
End Class
