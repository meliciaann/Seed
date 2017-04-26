Imports System.Data.Linq
Imports System.ComponentModel
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Reflection
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports QBLib.QBLibrary
Imports SeedGeneral
Imports System.Text
Imports System.Drawing.Printing
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing.Imaging


Public Class ItemsForm
    Implements IDisposable
    Private fromIndex As Integer
    Private dragIndex As Integer
    Private dragRect As Rectangle
    Public d As New GetDataClass
    Public VisibleReportsBindingSource As New BindingSource
    Public MixBagTagsBindingSource As New BindingSource
    Public GridviewMenu As New ContextMenu
    Public IsNewOrder As Boolean = False
    Public IsLoading As Boolean
    Public isNewItem As Boolean
    Public IsFillItems As Boolean
    Public IsCustomerEdit As Boolean
    Public IsCustomerRowAdd As Boolean
    'Public CurrentReport As New SeedReport
    Private ItemsSpreadsheetReportDataSource As New ReportDataSource
    Private OrderItemsReportDataSource As New ReportDataSource
    Private SeedOrderReportDataSource As New ReportDataSource
    Private SeedOrderDetailReportDataSource As New ReportDataSource
    Private SeedReportDataSource As New ReportDataSource
    Private BagItemSingleSource As New ReportDataSource
    Private GetMixItemInfoReportDataSource As New ReportDataSource
    Private GetMixItemOrderInfoReportDataSource As New ReportDataSource
    Private GetMixBagTagsReportDataSource As New ReportDataSource
    Private GetBagTagReportDataSource As New ReportDataSource
    Public QB As New QBLib.QBLibrary
    Public Event ReportExport As ExportEventHandler
    Public ShowOrderItemInventoryItem As New MenuItem
    Public ShowInventoryMenuItem As New MenuItem
    Public UpdateItemInfoMenuItem As New MenuItem
    Public ShowInventoryOrderMenuItem As New MenuItem
    Public AddInventoryMenuItem As New MenuItem
    Public AddInventoryFromInventoryMenuItem As New MenuItem
    Public DeleteOrderItem As New MenuItem
    Public DeleteItem As New MenuItem
    'Public ShowItem As New MenuItem
    Public DeleteOrder As New MenuItem
    Public DeleteCustomer As New MenuItem
    Public DeleteInvoiceLineItemSKU As New MenuItem
    Public DeleteInventoryItem As New MenuItem
    Public GetBagTagSingle As New MenuItem
    Public DefaultReportDirectory As String = My.Settings.DefaultReportDirectory
    Public CurrentInventoryObject As Object
    Public ItemOrderR As Integer
    Private AllCustomers As List(Of Customer)
    Private AllItems As List(Of Item)
    Private AllInvoiceLineItems As List(Of InvoiceLineItem)
    Private AllReports As List(Of SeedReport)
    Private AllOrders As List(Of OrderDetails)
    Private CurrentOrderItemDetails As List(Of OrderItemDetails)
    Private AllNetWeight As List(Of NetWeight)



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'SeedDataSet.ItemsSpreadsheet' table. You can move, or remove it, as needed.
        Me.ItemsSpreadsheetTableAdapter.Fill(Me.SeedDataSet.ItemsSpreadsheet)
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

        InventoryLbl.Text = ""
        CustomerDataGridView.ReadOnly = True
        CustomerDataGridView.AllowUserToAddRows = False
        ItemsDataGridView.ReadOnly = True
        ItemsDataGridView.AllowUserToAddRows = False
        OrderDatePicker.Value = DateTime.Now

        Me.ReportViewer2.RefreshReport()
        AddHandler ReportViewer2.ReportExport, AddressOf ReportViewer2_ReportExport

        Me.ContextMenu = GridviewMenu
        ShowInventoryMenuItem.Text = "Show Inventory"
        UpdateItemInfoMenuItem.Text = "Update Item Detail"
        ShowInventoryOrderMenuItem.Text = "Show Inventory"
        GridviewMenu.MenuItems.Add(ShowInventoryMenuItem)
        GridviewMenu.MenuItems.Add(ShowInventoryOrderMenuItem)
        GridviewMenu.MenuItems.Add(UpdateItemInfoMenuItem)
        ShowOrderItemInventoryItem.Text = "Show Inventory"
        GridviewMenu.MenuItems.Add(ShowOrderItemInventoryItem)
        AddInventoryMenuItem.Text = "Update Inventory"
        AddInventoryFromInventoryMenuItem.Text = "Add Inventory"
        GridviewMenu.MenuItems.Add(AddInventoryMenuItem)
        GridviewMenu.MenuItems.Add(AddInventoryFromInventoryMenuItem)
        DeleteOrderItem.Text = "Delete Item"
        GridviewMenu.MenuItems.Add(DeleteOrderItem)
        DeleteItem.Text = "Delete Item"
        GridviewMenu.MenuItems.Add(DeleteItem)
        'ShowItem.Text = "Show Item"
        'GridviewMenu.MenuItems.Add(ShowItem)
        DeleteOrder.Text = "Delete Order"
        GridviewMenu.MenuItems.Add(DeleteOrder)
        DeleteCustomer.Text = "Delete Customer"
        GridviewMenu.MenuItems.Add(DeleteInvoiceLineItemSKU)
        GridviewMenu.MenuItems.Add(DeleteInventoryItem)
        DeleteInvoiceLineItemSKU.Text = "Delete SKU"
        DeleteInventoryItem.Text = "Delete Entry"
        GridviewMenu.MenuItems.Add(DeleteCustomer)
        DeleteItem.Visible = False
        'ShowItem.Visible = False
        GetBagTagSingle.Visible = False
        GetBagTagSingle.Text = "Bag Tag-Single"
        GridviewMenu.MenuItems.Add(GetBagTagSingle)


        AddHandler UpdateItemInfoMenuItem.Click, AddressOf Me.Menu_Click_UpdateItemInfo
        AddHandler ShowOrderItemInventoryItem.Click, AddressOf Me.Menu_Click_OrderShowItems
        AddHandler ShowInventoryMenuItem.Click, AddressOf Me.Menu_Click_ShowItems
        AddHandler ShowInventoryOrderMenuItem.Click, AddressOf Me.Menu_Click_ShowOrderInventoryItems
        AddHandler AddInventoryFromInventoryMenuItem.Click, AddressOf Me.Menu_Click_AddInventoryItems
        AddHandler AddInventoryMenuItem.Click, AddressOf Me.Menu_Click_AddItems
        AddHandler DeleteOrderItem.Click, AddressOf Me.Menu_Click_DeleteOrderItem
        'AddHandler ShowItem.Click, AddressOf Me.Menu_Click_ShowItem
        AddHandler DeleteItem.Click, AddressOf Me.Menu_Click_DeleteItem
        AddHandler GetBagTagSingle.Click, AddressOf Me.Menu_Click_GetBagTagSingle
        AddHandler DeleteOrder.Click, AddressOf Me.Menu_Click_DeleteOrder
        AddHandler DeleteCustomer.Click, AddressOf Me.Menu_Click_DeleteCustomer
        AddHandler DeleteInventoryItem.Click, AddressOf Me.Menu_Click_DeleteInventoryItem
        AddHandler DeleteInvoiceLineItemSKU.Click, AddressOf Me.Menu_Click_DeleteInvoiceLineItemSKU
        OrderItemsGridFormatting()

        InvoiceLineItemCB.SelectedIndex = 0
        ' OrderUnitsCB.SelectedIndex = 0
        CustomerCB.Focus()
        IsLoading = False
        TestDateLbl.Visible = False
        TestDateTP.Visible = False
        NoxiousWeedsComboBox.Visible = False
        NoxiousWeedsLbl.Visible = False
        BagTagAcreDGV.Visible = False
        MixBagTagDGV.Visible = False
        'UserReportInputDGV.Visible = False
    End Sub
    Private Sub RefreshData()
        FillInvoiceLineItems()
        FillNetWt()
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
        If (Not d.CurrentOrder Is Nothing AndAlso d.CurrentOrder.OrderStatusId Is Nothing) OrElse d.CurrentOrder Is Nothing Then
            OrderStatusCB.SelectedIndex = 1
        End If
        OrderUnitsCB.DataSource = d.OrderInfoDB.OrderUnits
        OrderUnitsCB.DisplayMember = "UnitTypeName"
        OrderUnitsCB.ValueMember = "UnitTypeID"

        Dim DefaultOrderUnits = From OrderUnit In d.OrderInfoDB.OrderUnits Where OrderUnit.IsDefault = True
        d.CurrentOrderUnit = DefaultOrderUnits.Single
        OrderUnitsCB.SelectedItem = d.CurrentOrderUnit
        OrderUnitsCB.SelectedValue = d.CurrentOrderUnit.UnitTypeID

        FillCustomerData()
        FillItemsData()



    End Sub
    Private Sub FillInvoiceLineItems()
        If (AllInvoiceLineItems Is Nothing) Then

            If (InvoiceLineItemCB.DataSource Is Nothing OrElse InvoiceLineItemCB.Items.Count <> d.InvoiceLineItemsDB.InvoiceLineItems.Count) Then
                d.InvoiceLineItemsDB = New InvoiceLineItemsDataContext
                d.InvoiceLineItems = d.InvoiceLineItemsDB.GetTable(Of InvoiceLineItem)
                Dim InvLineItems = From ivl In d.InvoiceLineItemsDB.InvoiceLineItems Order By ivl.SKU
                AllInvoiceLineItems = InvLineItems.ToList
                InvoiceLineItemDGV.DataSource = AllInvoiceLineItems.ToList
                InvoiceLineItemCB.DataSource = AllInvoiceLineItems.ToList
                InvoiceLineItemCB.DisplayMember = "SKU"
                InvoiceLineItemCB.ValueMember = "SKU"
                If (d.CurrentOrder IsNot Nothing AndAlso Not d.CurrentOrder.LineItemSKU Is Nothing) Then
                    InvoiceLineItemCB.SelectedItem = d.CurrentOrder.LineItemSKU
                End If
            End If
        End If


    End Sub
    Private Sub FillNetWt()
        If AllNetWeight Is Nothing Then
            d.NetWeightDB = New NetWeightDataContext
            d.Netweights = d.NetWeightDB.GetTable(Of NetWeight)
            AllNetWeight = d.Netweights.ToList
            NetWtDGV.DataSource = AllNetWeight.ToList
        End If

    End Sub
    Private Sub FillBagTagAcres()
        If Not (d.CurrentOrder Is Nothing) Then
            d.BagTagAcres = Nothing
            Dim tmpBagTagAcres = From t In d.OrderInfoDB.BagOrderItems Where t.OrderID = d.CurrentOrder.OrderID
                                 Group t By t.Acres, t.OrderID, t.Bags Into MyGroup = Group
                                 Select Acres, OrderID, Bags

            If d.BagTagAcres Is Nothing Then
                d.BagTagAcres = New BindingList(Of BagTagAcre)
            End If
            If tmpBagTagAcres.Count = 0 Then
                Dim a3 As New BagTagAcre(d.CurrentOrder.OrderID, 0.00, d.CurrentOrder.Acres, 1)
                d.BagTagAcres.Add(a3)
            Else
                For Each b In tmpBagTagAcres
                    Dim a3 As New BagTagAcre(b.OrderID, b.Acres, d.CurrentOrder.Acres, b.Bags)
                    d.BagTagAcres.Add(a3)
                Next
            End If

            IsLoading = True
            ''Me.BagTagBindingSource.DataSource = d.BagTagAcres
            'Me.UserReportInputDGV.AutoGenerateColumns = True
            'Me.UserReportInputDGV.DataSource = d.BagTagAcres
            'Me.UserReportInputDGV.Refresh()

            IsLoading = False

        End If
    End Sub

    Private Sub FillMixBagTags()
        If Not (d.CurrentOrder Is Nothing) Then
            d.MixBagTags = New List(Of MixBagTag)
            For Each NetWeight In d.Netweights
                Dim a1 As New MixBagTag(NetWeight.NetWtID, NetWeight.NetWtDescription, NetWeight.IsDefault, d.CurrentOrder.MixName, d.CurrentOrder.Project, "", 0.00, 0.00, 0.00, 0.00, "", 0, 0, NetWeight.DefaultPages)
                d.MixBagTags.Add(a1)
            Next
        End If
    End Sub
    Private Sub FillCustomerData()
        If AllCustomers Is Nothing Then
            IsFillItems = True
            Dim aCustomers = From Customer In d.Customers Order By Customer.CustomerName
            AllCustomers = aCustomers.ToList
            CustomerDataGridView.AutoGenerateColumns = True
            CustomerDataGridView.DataSource = aCustomers

            CustomerCB.DataSource = AllCustomers.ToList
            CustomerCB.DisplayMember = "CustomerName"
            CustomerCB.ValueMember = "CustomerID"
            CustomerCB.SelectedIndex = -1
            IsFillItems = False
        End If
    End Sub
    Private Sub FillItemsData()
        If (AllItems Is Nothing) Then
            IsLoading = True
            ItemsDataGridView.AutoGenerateColumns = True
            Dim MyItems = From Item In d.Items

            AllItems = MyItems.ToList

        End If
        If TypeFilterCB.SelectedIndex = -1 Then
            Dim aItems = From Item In AllItems
                         Where Item.Discontinued = False Or Item.Discontinued = ShowDiscontinuedItemsCB.Checked Order By Item.Item
            ItemsDataGridView.DataSource = aItems.ToList

        Else
            Dim aItems = From Item In AllItems Where Item.Type = TypeFilterCB.Text And (Item.Discontinued = False Or Item.Discontinued = ShowDiscontinuedItemsCB.Checked) Order By Item.Item
            ItemsDataGridView.DataSource = aItems.ToList
        End If

        If (TypeFilterCB.DataSource Is Nothing) Then
            Dim TypeFilter=From item In AllItems
                           Select item.Type
                           Distinct

            'From item In AllItems
            'Select New ItemTypes With {
            '                .ItemType = item.Type}
            'Distinct


            TypeFilterCB.DataSource = TypeFilter.ToList

            ' TypeFilterCB.DisplayMember = "ItemType"
            ' TypeFilterCB.ValueMember = "ItemType"
            TypeFilterCB.SelectedIndex = -1
        End If

        IsLoading = False
    End Sub

    Public Sub FillInventoryData(ByVal ItemType As Object)
        IsFillItems = True
        CurrentInventoryObject = ItemType
        InventoryDGV.AutoGenerateColumns = True
        Dim InventoryHistory As Object
        Select Case ItemType.GetType.Name
            Case "Item"
                Dim Item As Item = CType(ItemType, Item)
                Select Case Item.ItemGroupID
                    Case = 0
                        InventoryHistory = From Inventory In d.InventoryDB.Inventories Join InventoryItemDetail In d.InventoryDB.InventoryItemDetails On Inventory.ItemID Equals InventoryItemDetail.ItemID
                                           Where Inventory.ItemID = Item.ItemID And Inventory.Quantity IsNot Nothing
                                           Select New InventoryDetails With {
                                               .inventorydate = Inventory.InventoryDate,
                                            .inventoryid = Inventory.InventoryID,
                                               .ItemId = Inventory.ItemID,
                                               .Lot = InventoryItemDetail.Lot,
                                               .Quantity = Inventory.Quantity,
                                               .Item = InventoryItemDetail.Item,
                                               .Memo = Inventory.Memo,
                                               .InvoiceID = Inventory.InvoiceID,
                                               .OrderId = Inventory.OrderId,
                                               .AvailableInventory = InventoryItemDetail.AvailableInventory
                                               }
                    Case Else
                        InventoryHistory = From Inventory In d.InventoryDB.Inventories Join InventoryItemDetail In d.InventoryDB.InventoryItemDetails On Inventory.ItemID Equals InventoryItemDetail.ItemID
                                           Where Inventory.ItemGroupID = Item.ItemGroupID And Inventory.Quantity IsNot Nothing
                                           Select New InventoryDetails With {
                                               .inventorydate = Inventory.InventoryDate,
                                            .inventoryid = Inventory.InventoryID,
                                               .ItemId = Inventory.ItemID,
                                               .Lot = InventoryItemDetail.Lot,
                                               .Quantity = Inventory.Quantity,
                                               .Item = InventoryItemDetail.Item,
                                               .Memo = Inventory.Memo,
                                               .InvoiceID = Inventory.InvoiceID,
                                               .OrderId = Inventory.OrderId,
                                               .AvailableInventory = InventoryItemDetail.AvailableInventory
                                               }
                End Select

                Dim Balance As Decimal? = 0.00
                For Each inventoryItem As InventoryDetails In InventoryHistory
                    Balance = Balance + inventoryItem.Quantity.Value
                Next
                CurrentItemAvailableTB.Text = Balance
                CurrentItemAvailableTB.Visible = True
                Label7.Visible = True
                AddInventoryBtn.Visible = True
                InventoryLbl.Text = d.CurrentItem.Item

            Case "Order"
                Dim Order As Order = CType(ItemType, Order)
                InventoryHistory = From Inventory In d.InventoryDB.Inventories Join InventoryItemDetail In d.InventoryDB.InventoryItemDetails On Inventory.ItemID Equals InventoryItemDetail.ItemID
                                   Where Inventory.OrderId = Order.OrderID
                                   Select New InventoryDetails With {
                                       .inventorydate = Inventory.InventoryDate,
                                    .inventoryid = Inventory.InventoryID,
                                       .ItemId = Inventory.ItemID,
                                       .Lot = InventoryItemDetail.Lot,
                                       .Quantity = Inventory.Quantity,
                                       .Item = InventoryItemDetail.Item,
                                       .Memo = Inventory.Memo,
                                       .InvoiceID = Inventory.InvoiceID,
                                       .AvailableInventory = InventoryItemDetail.AvailableInventory,
                                       .OrderId = Inventory.OrderId, .ItemGroupID = Inventory.ItemGroupID}
                CurrentItemAvailableTB.Visible = False
                Label7.Visible = False
                AddInventoryBtn.Visible = False
                InventoryLbl.Text = d.CurrentOrder.OrderID.ToString() + vbCrLf + d.CurrentOrder.Project + vbCrLf + d.CurrentOrder.MixName
            Case "OrderDetails"
                Dim OrderDetail As OrderDetails = CType(ItemType, OrderDetails)
                InventoryHistory = From Inventory In d.InventoryDB.Inventories Join InventoryItemDetail In d.InventoryDB.InventoryItemDetails On Inventory.ItemID Equals InventoryItemDetail.ItemID
                                   Where Inventory.OrderId = OrderDetail.OrderID
                                   Select New InventoryDetails With {
                                       .inventorydate = Inventory.InventoryDate,
                                    .inventoryid = Inventory.InventoryID,
                                       .ItemId = Inventory.ItemID,
                                       .Lot = InventoryItemDetail.Lot,
                                       .Quantity = Inventory.Quantity,
                                       .Item = InventoryItemDetail.Item,
                                       .Memo = Inventory.Memo,
                                       .InvoiceID = Inventory.InvoiceID,
                                       .AvailableInventory = InventoryItemDetail.AvailableInventory,
                                       .OrderId = Inventory.OrderId,
                                       .ItemGroupID = Inventory.ItemGroupID}
                CurrentItemAvailableTB.Visible = False
                Label7.Visible = False
                AddInventoryBtn.Visible = False
                InventoryLbl.Text = OrderDetail.OrderID.ToString() + vbCrLf + OrderDetail.Project + vbCrLf + OrderDetail.MixName

            Case Else
                InventoryHistory = Nothing
        End Select
        InventoryDGV.DataSource = InventoryHistory

    End Sub

    Private Sub FillReports()

        'Me.ReportViewer2.Clear()
        If Not IsFillItems Then
            If (AllReports Is Nothing) Then
                Dim aReports = From vr In d.SeedReports Order By vr.SortOrder

                AllReports = aReports.ToList

            End If


            Dim visibleReports As Object
            If Not (d.CurrentOrder Is Nothing) Then
                d.UserReports = New List(Of AvailableReport)
                visibleReports = From vr In AllReports Where vr.IsVisible = True Order By vr.SortOrder

            Else
                d.UserReports = New List(Of AvailableReport)
                visibleReports = From vr In AllReports Where (vr.IsVisible = True And vr.NeedsOrder = 0) OrElse vr.ReportFileName = "BagTagSingle.rdlc" Order By vr.SortOrder
            End If

            If (d.UserReports.Count = 0) Then

                For Each SeedReport In visibleReports
                    Dim a1 As New AvailableReport(SeedReport.ReportFileName, SeedReport.ReportID, SeedReport.FriendlyName, SeedReport.SortOrder, 0, SeedReport.IsVisible, SeedReport.AllowMultiple)
                    d.UserReports.Add(a1)
                Next
            End If
            VisibleReportsBindingSource.DataSource = d.UserReports

            ReportsDGV.AutoGenerateColumns = True
            ReportsDGV.DataSource = VisibleReportsBindingSource
        End If
    End Sub
    Private Sub FillOrdersData()
        IsLoading = True
        OrdersGridView.AutoGenerateColumns = True
        If (d.OrderInfoDB Is Nothing) Then
            d.OrderInfoDB = New OrderItemsDataContext
        End If
        If (AllOrders Is Nothing) Then
            Dim OrdersQuery = From Orders In d.OrderInfoDB.Orders
                              Join Customers In d.OrderInfoDB.OrderCustomerDetails On Orders.CustomerID Equals Customers.CustomerId
                              Join InvoiceLineItem In d.OrderInfoDB.InvoiceLineItemDetails On Orders.LineItemSKU Equals InvoiceLineItem.SKU
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
                              .LineItemSKU = Orders.LineItemSKU,
                              .IsMix = InvoiceLineItem.IsMix,
                              .LineItemDesc = InvoiceLineItem.Description
                              }

            AllOrders = OrdersQuery.ToList
        End If
        OrdersGridView.AutoGenerateColumns = True
        OrdersGridView.DataSource = AllOrders.ToList
        IsLoading = False
    End Sub


    Private Sub FillOrderItems()

        If Not d.CurrentOrder Is Nothing Then

            IsFillItems = True
            Dim OrderTotal As Decimal? = 0.00
            Dim OrderPricePerAcre As Decimal? = 0.00
            Dim DistributorOrderPricePerAcre As Decimal? = 0.00
            Dim RetailOrderPricePerAcre As Decimal? = 0.00
            Dim WholesaleOrderPricePerAcre As Decimal? = 0.00

            If Not (d.CurrentOrder Is Nothing) Then
                If (CurrentOrderItemDetails Is Nothing) Then
                    Dim aCurrentOrderItems = From orderitems In d.OrderInfoDB.OrderItems
                                             Join OrderItemDetails In d.OrderInfoDB.OrderItemDetails On orderitems.ItemID Equals OrderItemDetails.ItemID
                                             Where orderitems.OrderID.Equals(d.CurrentOrder.OrderID)
                                             Order By orderitems.ItemOrder
                                             Select New OrderItemDetails With {
                                                .Lot = OrderItemDetails.Lot,
                                                .PLS = orderitems.PLS_Percent,
                                                .Item = OrderItemDetails.Item,
                                                .PLSLBSPerAcre = orderitems.PLS_LBS_PerAcre,
                                                .PricePerPLSLB = orderitems.PricePerPLSLB,
                                                .PricePerAcre = orderitems.PricePerAcre,
                                                .DistributorPricePerAcre = orderitems.DistributorPricePerAcre,
                                                .RetailPricePerAcre = orderitems.RetailPricePerAcre,
                                                .WholesalePricePerAcre = orderitems.WholesalePricePerAcre,
                                                .TotalPrice = orderitems.PricePerAcre * d.CurrentOrder.Acres,
                                                .Distributor = OrderItemDetails.Distributor,
                                                .Wholesale = OrderItemDetails.Wholesale,
                                                .Retail = OrderItemDetails.Retail,
                                                .OrderItemID = orderitems.OrderItemID,
                                                .BulkLbs = orderitems.BulkLbs,
                                                .PLSLBS = orderitems.PLSLbs,
                                                .ItemID = orderitems.ItemID,
                                                .TestDate = OrderItemDetails.Test_Date,
                                                .InventoryID = orderitems.InventoryID,
                                                .AvailableInventory = OrderItemDetails.AvailableInventory,
                                                .ItemOrder = orderitems.ItemOrder}


                    CurrentOrderItemDetails = aCurrentOrderItems.ToList
                End If
                Dim pItemOrderR = Aggregate maxItemOrder In CurrentOrderItemDetails
                                      Into Max(maxItemOrder.ItemOrder)
                If pItemOrderR Is Nothing Then
                    ItemOrderR = 0
                Else
                    ItemOrderR = pItemOrderR.Value
                End If


                OrderItemsGridView.DataSource = CurrentOrderItemDetails.ToList

                For Each OrderItem In CurrentOrderItemDetails
                    OrderPricePerAcre = OrderPricePerAcre + OrderItem.PricePerAcre
                    DistributorOrderPricePerAcre = DistributorOrderPricePerAcre + OrderItem.DistributorPricePerAcre
                    RetailOrderPricePerAcre = RetailOrderPricePerAcre + OrderItem.RetailPricePerAcre
                    WholesaleOrderPricePerAcre = WholesaleOrderPricePerAcre + OrderItem.WholesalePricePerAcre
                Next

                OrderTotal = OrderPricePerAcre * d.CurrentOrder.Acres


                If (OrderTotal Is Nothing) Then
                    OrderTotal = 0.00
                    OrderPricePerAcre = 0.00
                End If
                If (OverrideTotalPriceTB.Checked) Then
                    OrderTotal = OrderTotalTB.Text
                    OrderPricePerAcre = TotalPricePerAcreTB.Text

                End If
                d.CurrentOrder.OrderTotal = OrderTotal
                d.CurrentOrder.TotalPricePerAcre = OrderPricePerAcre
                d.OrderInfoDB.SubmitChanges()

            End If
            OrderTotalTB.Text = FormatCurrency(OrderTotal, 2, TriState.True, TriState.False, TriState.True)
            TotalPricePerAcreTB.Text = FormatCurrency(OrderPricePerAcre, 2, TriState.True, TriState.False, TriState.True)
            RetailPricePerAcreTB.Text = FormatCurrency(RetailOrderPricePerAcre, 2, TriState.True, TriState.False, TriState.True)
            WholesalePricePerAcreTB.Text = FormatCurrency(WholesaleOrderPricePerAcre, 2, TriState.True, TriState.False, TriState.True)
            DistributorPricePerAcreTB.Text = FormatCurrency(DistributorOrderPricePerAcre, 2, TriState.True, TriState.False, TriState.True)
            IsFillItems = False
        End If
    End Sub

    Private Sub UpdateItems()
        Dim SaveItem As MsgBoxResult
        SaveItem = MessageBox.Show("Would you like to save your changes?", "Save Changes?", MessageBoxButtons.YesNo)
        If (SaveItem.Yes) Then

            Dim temp = d.ItemsDB.GetChangeSet
            For Each i As Item In temp.Updates
                Dim InventoryItems = From inv In d.InventoryDB.Inventories Where inv.ItemID = i.ItemID

                For Each invTemp As Inventory In InventoryItems
                    invTemp.ItemGroupID = i.ItemGroupID
                Next

            Next
            d.ItemsDB.SubmitChanges()
            d.InventoryDB.SubmitChanges()
            AllItems = Nothing
            FillItemsData()

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
            'ItemsSearchTB.Text = Nothing
            ItemsSearchTB.Focus()
            FillItemsData()
            ItemsGridFormatting()
            IsLoading = False
            ShowInventoryMenuItem.Visible = True
            UpdateItemInfoMenuItem.Visible = False
            ShowInventoryOrderMenuItem.Visible = False
            AddInventoryMenuItem.Visible = True
            DeleteInvoiceLineItemSKU.Visible = False
            ShowOrderItemInventoryItem.Visible = False
            DeleteCustomer.Visible = False
            'ShowItem.Visible = False
            DeleteOrder.Visible = False
            GetBagTagSingle.Visible = True
            DeleteOrderItem.Visible = False
            DeleteInventoryItem.Visible = False
            AddInventoryFromInventoryMenuItem.Visible = False
        End If

        If (OrdersPage.SelectedTab Is CustomerTabPage) Then
            IsLoading = True
            CustomerGridViewFormatting()
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            UpdateItemInfoMenuItem.Visible = False
            ShowOrderItemInventoryItem.Visible = False
            ShowInventoryOrderMenuItem.Visible = False
            DeleteOrderItem.Visible = False
            'ShowItem.Visible = False
            DeleteOrder.Visible = False
            DeleteCustomer.Visible = False
            GetBagTagSingle.Visible = False
            DeleteInvoiceLineItemSKU.Visible = False
            DeleteInventoryItem.Visible = False
            AddInventoryFromInventoryMenuItem.Visible = False
            CustomerSearchTB.Focus()
            IsLoading = False
        End If
        If (OrdersPage.SelectedTab Is OrdersTabPage) Then
            IsLoading = True
            IsFillItems = True
            FillOrdersData()
            IsLoading = False
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            UpdateItemInfoMenuItem.Visible = False
            ShowOrderItemInventoryItem.Visible = False
            ShowInventoryOrderMenuItem.Visible = True
            DeleteOrderItem.Visible = False
            'ShowItem.Visible = False
            DeleteOrder.Visible = True
            DeleteCustomer.Visible = False
            GetBagTagSingle.Visible = False
            DeleteInvoiceLineItemSKU.Visible = False
            DeleteInventoryItem.Visible = False
            AddInventoryFromInventoryMenuItem.Visible = False
            OrdersGridFormatting()
            OrdersSearchTB.Focus()
            IsFillItems = False
        End If

        If (OrdersPage.SelectedTab Is OrderTabPage) Then
            FillOrderItems()
            IsFillItems = True
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            ShowOrderItemInventoryItem.Visible = True
            ShowInventoryOrderMenuItem.Visible = False
            UpdateItemInfoMenuItem.Visible = True
            DeleteOrderItem.Visible = True
            'ShowItem.Visible = True
            DeleteOrder.Visible = False
            DeleteCustomer.Visible = False
            GetBagTagSingle.Visible = False
            DeleteInvoiceLineItemSKU.Visible = False
            DeleteInventoryItem.Visible = False
            AddInventoryFromInventoryMenuItem.Visible = False
            OrderItemsGridFormatting()
            FillNetWt()
            FillInvoiceLineItems()
            CustomerCB.Focus()
            IsFillItems = False
        End If
        If (OrdersPage.SelectedTab Is ReportsTabPage) Then
            If Not IsFillItems Then
                IsLoading = True
                FillReports()
                ReportsGridFormatting()
                ShowInventoryMenuItem.Visible = False
                AddInventoryMenuItem.Visible = False
                ShowOrderItemInventoryItem.Visible = False
                ShowInventoryOrderMenuItem.Visible = False
                UpdateItemInfoMenuItem.Visible = False
                DeleteOrder.Visible = False
                DeleteOrderItem.Visible = False
                'ShowItem.Visible = False
                DeleteCustomer.Visible = False
                DeleteInvoiceLineItemSKU.Visible = False
                DeleteInventoryItem.Visible = False
                GetBagTagSingle.Visible = False
                AddInventoryFromInventoryMenuItem.Visible = False
                IsLoading = False
            End If
            If (d.CurrentOrder Is Nothing AndAlso d.UserReports.Count = 0) Then
                Button5.Enabled = False
            Else
                Button5.Enabled = True
            End If
        End If
        If (OrdersPage.SelectedTab Is AdminTP) Then
            IsLoading = True
            ItemsSearchTB.Text = Nothing
            ItemsSearchTB.Focus()
            FillItemsData()
            ItemsGridFormatting()
            IsLoading = False
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            ShowOrderItemInventoryItem.Visible = False
            ShowInventoryOrderMenuItem.Visible = False
            UpdateItemInfoMenuItem.Visible = False
            DeleteCustomer.Visible = False
            'ShowItem.Visible = False
            DeleteOrder.Visible = False
            GetBagTagSingle.Visible = False
            DeleteOrderItem.Visible = False
            DeleteInventoryItem.Visible = False
            DeleteInvoiceLineItemSKU.Visible = True
            AddInventoryFromInventoryMenuItem.Visible = False
        End If
        If (OrdersPage.SelectedTab Is InventoryTP) Then
            IsLoading = True
            IsFillItems = True
            ItemsSearchTB.Text = Nothing
            ItemsSearchTB.Focus()
            FillItemsData()
            ItemsGridFormatting()
            IsLoading = False
            ShowInventoryMenuItem.Visible = False
            AddInventoryMenuItem.Visible = False
            AddInventoryFromInventoryMenuItem.Visible = True
            UpdateItemInfoMenuItem.Visible = False
            'ShowItem.Visible = False
            ShowOrderItemInventoryItem.Visible = False
            DeleteCustomer.Visible = False
            DeleteOrder.Visible = False
            GetBagTagSingle.Visible = False
            DeleteOrderItem.Visible = False
            DeleteInvoiceLineItemSKU.Visible = False
            DeleteInventoryItem.Visible = True
            IsFillItems = False
        End If

    End Sub

    Private Sub UpdateAcres(sender As Object, e As System.EventArgs) Handles UnitsTB.Leave
        If Not (d.CurrentOrder Is Nothing) Then
            d.CurrentOrder.Acres = UnitsTB.Text
        End If

    End Sub
    Private Sub InventoryItemUpdate(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles InventoryDGV.CellLeave
        If (IsFillItems = False) Then
            Dim SelectedRow As DataGridViewRow = InventoryDGV.CurrentRow
            InventoryDGV.CommitEdit(DataGridViewDataErrorContexts.Commit)
            Dim CurrentInventoryItem As InventoryDetails = SelectedRow.DataBoundItem

            Dim UpdateInvItems = From inventory In d.InventoryDB.Inventories Where inventory.InventoryID = CurrentInventoryItem.inventoryid

            Dim s As String = sender.columns(e.ColumnIndex).DataPropertyName
            Dim UpdateInventoryItem As Boolean = False
            For Each invItem In UpdateInvItems
                Select Case s
                    Case "Memo"
                        UpdateInventoryItem = True
                        invItem.Memo = CurrentInventoryItem.Memo
                    Case "Quantity"
                        UpdateInventoryItem = True
                        invItem.Quantity = CurrentInventoryItem.Quantity
                    Case "InventoryDate"
                        UpdateInventoryItem = True
                        invItem.InventoryDate = CurrentInventoryItem.inventorydate
                    Case "OrderId"
                        UpdateInventoryItem = True
                        invItem.OrderId = CurrentInventoryItem.OrderId
                    Case "InvoiceID"
                        UpdateInventoryItem = True
                        invItem.InvoiceID = CurrentInventoryItem.InvoiceID
                End Select
            Next
            If (UpdateInventoryItem) Then
                d.InventoryDB.SubmitChanges()
            End If


        End If

    End Sub
    Private Sub OrderItemsGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles OrderItemsGridView.CellEndEdit

        If (IsFillItems = False) AndAlso OrderItemsGridView.IsCurrentRowDirty Then
            Dim SelectedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
            OrderItemsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
            For Each col As DataGridViewColumn In OrderItemsGridView.Columns
                If (col.Index = e.ColumnIndex) Then
                    Continue For
                End If
                Select Case col.Index
                    Case 17, 13, 16, 0, 1, 2, 10, 11, 12, 18, 19, 20, 3, 4



                    Case Else
                        Dim cell As DataGridViewCell = SelectedRow.Cells(col.Index)
                        cell.Value = Nothing
                End Select

            Next

            Dim OrderItemDetails As OrderItemDetails = SelectedRow.DataBoundItem

            Dim UpdateDOrderItems = From OrderItem In d.OrderInfoDB.OrderItems Where OrderItem.OrderItemID = OrderItemDetails.OrderItemID
            Dim s As String = sender.columns(e.ColumnIndex).DataPropertyName

            For Each UpdateDOrderItem In UpdateDOrderItems
                OrderItemDetailUpdate(UpdateDOrderItem, s, OrderItemDetails)
            Next

        End If

    End Sub
    Private Sub OrderItemDetailUpdate(OrderItem As OrderItem, ByVal UpdatedProperty As String, ByVal UpdatedOrderItem As OrderItemDetails)
        Dim UpdateOrderItems As Boolean = False
        Me.Cursor = Cursors.WaitCursor
        Select Case UpdatedProperty
            Case "PLSLBSPerAcre"
                UpdateOrderItems = True
                OrderItem.PLS_LBS_PerAcre = UpdatedOrderItem.PLSLBSPerAcre
            Case "PricePerPLSLB"
                UpdateOrderItems = True
                OrderItem.PricePerPLSLB = UpdatedOrderItem.PricePerPLSLB
            Case "PLS"
                UpdateOrderItems = True
                OrderItem.PLS_Percent = UpdatedOrderItem.PLS
            Case "ItemOrder"
                UpdateOrderItems = True
                OrderItem.ItemOrder = UpdatedOrderItem.ItemOrder
            Case "Acres"
                UpdateOrderItems = True
                d.CurrentOrder.Acres = UnitsTB.Text
            Case Else
                UpdateOrderItems = False
        End Select
        d.OrderInfoDB.SubmitChanges()

        If (UpdateOrderItems) Then

            UpdateOrderItemInformation(OrderItem, UpdatedOrderItem)
            'UpdatedOrderItem.PricePerAcre = UpdatedOrderItem.PricePerAcre
            'UpdatedOrderItem.TotalPrice = UpdatedOrderItem.TotalPrice
            'UpdatedOrderItem.BulkLbs = UpdatedOrderItem.BulkLbs
            'UpdatedOrderItem.PLSLBS = UpdatedOrderItem.PLSLBS
            d.OrderInfoDB.SubmitChanges()
            OrderItemsGridView.Refresh()
            OrderItemsGridFormatting()
        End If
        Me.Cursor = Cursors.Default
    End Sub
    Public Sub UpdateOrderItemInformation(ByVal MyCurrentOrderItem As OrderItem, ByVal OrderItemDetail As OrderItemDetails)
        
        MyCurrentOrderItem.PricePerAcre = MyCurrentOrderItem.PricePerPLSLB * MyCurrentOrderItem.PLS_LBS_PerAcre
        MyCurrentOrderItem.TotalPrice = MyCurrentOrderItem.PricePerAcre * d.CurrentOrder.Acres
        MyCurrentOrderItem.BulkLbs = (MyCurrentOrderItem.PLS_LBS_PerAcre * d.CurrentOrder.Acres) / MyCurrentOrderItem.PLS_Percent
        MyCurrentOrderItem.PLSLbs = MyCurrentOrderItem.PLS_LBS_PerAcre * d.CurrentOrder.Acres
        MyCurrentOrderItem.DistributorPricePerAcre = OrderItemDetail.Distributor * MyCurrentOrderItem.PLS_LBS_PerAcre
        MyCurrentOrderItem.RetailPricePerAcre = OrderItemDetail.Retail * MyCurrentOrderItem.PLS_LBS_PerAcre
        MyCurrentOrderItem.WholesalePricePerAcre = OrderItemDetail.Wholesale * MyCurrentOrderItem.PLS_LBS_PerAcre

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
        'IsFillItems = True
        If (d.CurrentOrder Is Nothing AndAlso IsNewOrder) Then
            d.CurrentOrder = New Order
            d.OrderInfoDB.Orders.InsertOnSubmit(d.CurrentOrder)
            AllOrders = Nothing
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
            d.CurrentOrder.LineItemSKU = InvoiceLineItemCB.SelectedValue
            d.CurrentOrder.OrderNote = OrderNoteTB.Text
            d.CurrentOrder.OrderNoteColor = OrderNoteColorTB.Text
            d.CurrentOrder.OverrideTotalPrice = OverrideTotalPriceTB.Checked
            If (d.CurrentOrder.OverrideTotalPrice) Then
                d.CurrentOrder.OrderTotal = OrderTotalTB.Text
            End If

            'd.CurrentOrder.IsMix = IsMixCB.Checked
            If d.CustomerPriceList Is Nothing AndAlso d.CurrentOrder.PriceList Is Nothing Then
                Dim CurrentCustomerList = From customer In AllCustomers Where customer.CustomerId = d.CurrentOrder.CustomerID
                For Each Customer In CurrentCustomerList
                    d.CurrentCustomer = Customer
                    If (d.CurrentCustomer.QBId Is Nothing) Then
                        d.CurrentCustomer.QBId = QB.DoQBCustomerListID(d.CurrentCustomer.CustomerName, "NameList", IsCustomerEdit)
                        d.CustomersDB.SubmitChanges()
                    End If
                    If (d.CustomerPriceList Is Nothing OrElse d.CustomerPriceList = "") Then
                        d.CustomerPriceList = QB.DoPriceListQuery(d.CurrentCustomer.QBId, "ListIDList", IsCustomerEdit)
                        PriceListCB.Text = d.CustomerPriceList
                    End If
                Next
            End If
            d.OrderInfoDB.SubmitChanges()
            OrderIDTB.Text = d.CurrentOrder.OrderID
            FillOrderItems()
            CurrentInventoryObject = Nothing
        End If
        'IsFillItems = False

    End Sub
    Private Sub ViewCurrentOrder()
        If Not (d.CurrentOrder Is Nothing) Then
            InvoiceTB.Text = d.CurrentOrder.InvoiceID
            CustomerCB.SelectedValue = d.CurrentOrder.CustomerID
            OrderUnitsCB.SelectedValue = d.CurrentOrder.UnitTypeID
            InvoiceLineItemCB.SelectedValue = d.CurrentOrder.LineItemSKU
            UnitsTB.Text = d.CurrentOrder.Acres
            ProjectTB.Text = d.CurrentOrder.Project
            MixNameTB.Text = d.CurrentOrder.MixName
            ControlNbrTB.Text = d.CurrentOrder.ControlNumber
            PriceListCB.Text = d.CurrentOrder.PriceList
            OrderIDTB.Text = d.CurrentOrder.OrderID
            OrderDatePicker.Value = d.CurrentOrder.OrderDate.Value
            OrderNoteColorTB.Text = d.CurrentOrder.OrderNoteColor
            OrderNoteTB.Text = d.CurrentOrder.OrderNote
            OverrideTotalPriceTB.Checked = d.CurrentOrder.OverrideTotalPrice

            TestDateLbl.Visible = False
            TestDateTP.Visible = False
            NoxiousWeedsComboBox.Visible = False
            NoxiousWeedsLbl.Visible = False

            MixBagTagDGV.Visible = False
            BagTagAcreDGV.Visible = False
            'If Not (d.CurrentOrder.IsMix Is Nothing) Then
            '    IsMixCB.Checked = d.CurrentOrder.IsMix.Value
            'End If

            TotalPricePerAcreTB.Text = FormatCurrency(d.CurrentOrder.TotalPricePerAcre, 2, TriState.True, TriState.False, TriState.True)
            OrderTotalTB.Text = FormatCurrency(d.CurrentOrder.OrderTotal, 2, TriState.True, TriState.False, TriState.True)
            FillOrderItems()
            'FillBagTagAcres()
            ReportViewer2.Reset()
        End If
    End Sub

    Private Sub PerformItemsSearch()
        'ic.ItemsDB = New ItemsEditDataContext

        Dim ItemsQuery = From Items In AllItems Where ((Items.Item IsNot Nothing AndAlso Items.Item.ToLower().Contains(ItemsSearchTB.Text.ToLower())) Or (Items.Lot IsNot Nothing AndAlso Items.Lot.ToLower().Contains(ItemsSearchTB.Text.ToLower()))) And (Items.Discontinued = False Or Items.Discontinued = ShowDiscontinuedItemsCB.Checked)
                         Order By Items.Item

        'From Items In AllItems Where (Items.Item.Contains(ItemsSearchTB.Text) Or Items.Lot.Contains(ItemsSearchTB.Text) Or Items.BotanicalName.Contains(ItemsSearchTB.Text)) And (Items.Discontinued = False Or Items.Discontinued = ShowDiscontinuedItemsCB.Checked)
        'Order By Items.Item

        ItemsDataGridView.DataSource = ItemsQuery.ToList
        IsLoading = True
        TypeFilterCB.SelectedIndex = -1
        IsLoading = False


    End Sub
    Private Sub PerformItemsSearch(ByVal ItemID As Integer)
        'ic.ItemsDB = New ItemsEditDataContext
        Dim ItemsQuery = From Items In AllItems Where Items.ItemID = ItemID
                         Order By Items.Item

        ItemsDataGridView.DataSource = ItemsQuery.ToList
        IsLoading = True
        TypeFilterCB.SelectedIndex = -1
        IsLoading = False
        OrdersPage.SelectedTab = ItemTabPage
    End Sub
    Private Sub PerformItemsFilter()
        'ic.ItemsDB = New ItemsEditDataContext

        Dim ItemsQuery = From Items In AllItems Where Items.Type = TypeFilterCB.Text
                         Order By Items.Item
        ItemsDataGridView.DataSource = ItemsQuery.ToList

    End Sub

    Private Sub PerformCustomerSearch()
        Dim CustomersQuery = From Customers In AllCustomers Where Customers.CustomerName.ToLower().Contains(CustomerSearchTB.Text.ToLower())
        CustomerDataGridView.DataSource = CustomersQuery.ToList
    End Sub

    Private Sub PerformOrdersSearch()
        Dim OrdersQuery = From Orders In d.OrderInfoDB.Orders
                          Join Customers In d.OrderInfoDB.OrderCustomerDetails On Orders.CustomerID Equals Customers.CustomerId
                          Join InvoiceLineItem In d.OrderInfoDB.InvoiceLineItemDetails On Orders.LineItemSKU Equals InvoiceLineItem.SKU
                          Where Orders.InvoiceID.Contains(OrdersSearchTB.Text) Or Orders.OrderID.ToString().Contains(OrdersSearchTB.Text) Or Orders.Project.ToString().Contains(OrdersSearchTB.Text) Or Orders.MixName.ToString().Contains(OrdersSearchTB.Text) Or Orders.ControlNumber.ToString().Contains(OrdersSearchTB.Text) Or Customers.CustomerName.ToString.Contains(OrdersSearchTB.Text) Or Customers.CustomerPhone.ToString.Contains(OrdersSearchTB.Text) Or Customers.Email.ToString.Contains(OrdersSearchTB.Text)
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
                              .LineItemSKU = Orders.LineItemSKU,
                              .IsMix = InvoiceLineItem.IsMix,
                              .LineItemDesc = InvoiceLineItem.Description
                              }


        OrdersGridView.DataSource = OrdersQuery
        OrdersGridFormatting()
    End Sub
    Private Sub UserDeletingRow(ByVal sender As Object,
    ByVal e As DataGridViewRowCancelEventArgs) _
    Handles OrderItemsGridView.UserDeletingRow

        Dim DeletedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
        Dim i As OrderItemDetails = DeletedRow.DataBoundItem
        Dim i2 = From OrderItem In d.OrderInfoDB.OrderItems Where OrderItem.OrderItemID = i.OrderItemID
        Dim r As Integer = i2.Count
        For Each OrderItem In i2
            If (MessageBox.Show("Delete Order Item ", "Delete Item?", MessageBoxButtons.YesNo)) = DialogResult.Yes Then
                d.OrderInfoDB.OrderItems.DeleteOnSubmit(OrderItem)
            End If

        Next

    End Sub
    Private Sub AddItemsToOrder()
        If (d.CurrentOrder Is Nothing) Then
            IsNewOrder = True
        End If

        CreateUpdateOrder()

        For Each row As DataGridViewRow In ItemsDataGridView.SelectedRows
            ItemOrderR = ItemOrderR + 2
            Dim currentItem As New Item
            currentItem = row.DataBoundItem
            Dim SelectedItem As New OrderItem
            SelectedItem.ItemID = currentItem.ItemID
            SelectedItem.OrderID = d.CurrentOrder.OrderID
            SelectedItem.PLS_Percent = currentItem.PLS_
            SelectedItem.ItemOrder = ItemOrderR
            SelectedItem.ItemGroupID = currentItem.ItemGroupID
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
        CurrentOrderItemDetails = Nothing
        FillOrderItems()
    End Sub
    Public Sub UpdatePricesForItems()
        Me.Cursor = Cursors.WaitCursor
        For Each Row As DataGridViewRow In OrderItemsGridView.Rows
            Dim currentItem As OrderItemDetails = Row.DataBoundItem
            Dim MyOrderItems = From orderItems In d.OrderInfoDB.OrderItems
                               Where orderItems.OrderItemID = currentItem.OrderItemID

            Dim MyOrderItem As OrderItem = MyOrderItems.Single
            Select Case d.CustomerPriceList
                Case "Distributor", "2) Distributor"
                    currentItem.PricePerPLSLB = currentItem.Distributor
                Case "Retail"
                    currentItem.PricePerPLSLB = currentItem.Retail
                Case "Wholesale", "1) Wholesale"
                    currentItem.PricePerPLSLB = currentItem.Wholesale
            End Select
            MyOrderItem.PricePerPLSLB = currentItem.PricePerPLSLB
            d.OrderInfoDB.SubmitChanges()

            UpdateOrderItemInformation(MyOrderItems.Single, currentItem)

        Next
        CurrentOrderItemDetails = Nothing
        FillOrderItems()
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub NewOrder()
        d.CurrentOrder = New Order
        InvoiceTB.Text = Nothing
        CustomerCB.SelectedItem = Nothing
        OrderStatusCB.SelectedIndex = 1
        UnitsTB.Text = 0.00
        OrderUnitsCB.SelectedIndex = 0
        CurrentOrderItemDetails = Nothing
        OrderTotalTB.Text = FormatCurrency(0.00, 2, TriState.True, TriState.False, TriState.True)
        TotalPricePerAcreTB.Text = FormatCurrency(0.00, 2, TriState.True, TriState.False, TriState.True)
        ProjectTB.Text = Nothing
        ControlNbrTB.Text = Nothing
        MixNameTB.Text = Nothing
        OrderNoteTB.Text = Nothing
        OrderNoteColorTB.Text = "Black"
        InvoiceLineItemCB.SelectedIndex = 0
        'CreateUpdateOrder()
        OrderItemsGridView.DataSource = Nothing
        OrderDatePicker.Value = DateTime.Now
        DistributorPricePerAcreTB.Text = Nothing
        WholesalePricePerAcreTB.Text = Nothing
        RetailPricePerAcreTB.Text = Nothing
        IsNewOrder = True
        d.CustomerPriceList = Nothing
        PriceListCB.Text = ""
        QB = New QBLib.QBLibrary
        ReportViewer2.Reset()
        d.UserReports = New List(Of AvailableReport)
        d.CurrentOrder = Nothing
        d.CurrentOrderItems = Nothing
        OrderIDTB.Text = Nothing
        d.BagTagAcres = Nothing
        CurrentInventoryObject = Nothing
        ItemOrderR = 0

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
        If EditItemsCB.Checked = False Then
            IsLoading = True
            AddItemsToOrder()
            'FillItemsData()
            'ItemsGridFormatting()
            ItemsSearchTB.Text = Nothing
            ItemsSearchTB.Focus()
            IsLoading = False
            OrderItemsGridView.ClearSelection()
        End If

    End Sub
    Private Sub DoubleClickOrdersGrid(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles OrdersGridView.CellMouseDoubleClick

        For Each row As DataGridViewRow In OrdersGridView.SelectedRows
            Select Case row.DataBoundItem.ToString
                Case "SeedGeneral.OrderDetails"
                    Dim SelectedOrder As OrderDetails = row.DataBoundItem
                    Dim i2 = From Orders In d.OrderInfoDB.Orders Where Orders.OrderID = SelectedOrder.OrderID
                    d.CurrentOrder = i2.Single
                    d.CustomerPriceList = d.CurrentOrder.PriceList
                    CurrentOrderItemDetails = Nothing
                Case "SeedGeneral.Order"
                    Dim SelectedOrder As Order = row.DataBoundItem
                    Dim i2 = From Orders In d.OrderInfoDB.Orders Where Orders.OrderID = SelectedOrder.OrderID
                    d.CurrentOrder = i2.Single
                    d.CustomerPriceList = d.CurrentOrder.PriceList
                    CurrentOrderItemDetails = Nothing
            End Select
        Next
        ViewCurrentOrder()
        OrdersPage.SelectedTab = OrderTabPage
    End Sub
    Private Sub DoubleClickCustomersGrid(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles CustomerDataGridView.CellMouseDoubleClick
        'If Not (d.CurrentOrder Is Nothing) Then
        '    Exit Sub
        'End If

        For Each row As DataGridViewRow In CustomerDataGridView.SelectedRows
            d.CurrentCustomer = row.DataBoundItem
        Next
        'CustomerCB.SelectedItem = d.CurrentCustomer
        CustomerCB.SelectedValue = d.CurrentCustomer.CustomerId
        If d.CurrentOrder Is Nothing Then
            d.CurrentOrder = New Order
        End If
        d.CurrentOrder.CustomerID = d.CurrentCustomer.CustomerId
        OrdersPage.SelectedTab = OrderTabPage

    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles EditItemsCB.CheckedChanged
        AllowItemEdits()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles EditCustomersCB.CheckedChanged
        AllowCustomerEdits()
        FillCustomerData()
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
        IsFillItems = True
        If EditCustomersCB.Checked = True Then
            IsCustomerEdit = True
            CustomerDataGridView.BeginEdit(True)
            CustomerDataGridView.ReadOnly = False
            DeleteCustomer.Visible = True
            EditCustomersCB.Text = "Save"
        End If
        If EditCustomersCB.Checked = False Then
            CustomerDataGridView.ReadOnly = True
            IsCustomerEdit = False
            DeleteCustomer.Visible = False
            IsFillItems = False
            UpdateCustomers()
            EditCustomersCB.Text = "Edit"
        End If
        IsFillItems = False
    End Sub
    Public Sub AllowItemEdits()
        If EditItemsCB.Checked = True Then
            ItemsDataGridView.BeginEdit(True)
            ItemsDataGridView.AllowUserToAddRows = True
            ItemsDataGridView.ReadOnly = False
            DeleteItem.Visible = True
            EditItemsCB.Text = "Save"
        End If


        If EditItemsCB.Checked = False Then
            ItemsDataGridView.AllowUserToAddRows = False
            ItemsDataGridView.ReadOnly = True
            DeleteItem.Visible = False
            UpdateItems()
            EditItemsCB.Text = "Edit"
        End If

    End Sub
    Public Sub RefreshDataSources()

        Me.OrderItemsTableAdapter.Fill(Me.SeedDataSet.OrderItems)
        Me.SeedReportsTableAdapter.Fill(Me.SeedDataSet.SeedReports)
        Me.ItemsSpreadsheetTableAdapter.Fill(Me.SeedDataSet.ItemsSpreadsheet)
        'Me.ReportingItemsTableAdapter.Fill(Me.reporting)
        If (d.CurrentOrder IsNot Nothing) Then
            Me.SeedOrderTableAdapter.Fill(Me.SeedDataSet.SeedOrder, d.CurrentOrder.OrderID)
            Me.SeedOrderDetailTableAdapter.Fill(Me.SeedDataSet.SeedOrderDetail, d.CurrentOrder.OrderID)
            Me.GetMixItemInfoTableAdapter.Fill(Me.SeedDataSet.GetMixItemInfo, d.CurrentOrder.OrderID)
        End If


    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        RefreshDataSources()
        Dim ReportFileName As String
        Dim HasSubReports As Integer
        Dim selectreports = From sr In d.UserReports Where sr.UserChecked = True
        If (selectreports.Count > 0) Then
            If (selectreports.Count = 1) Then
                ReportFileName = selectreports.Single.ReportFileName
                HasSubReports = selectreports.Single.HasSubReports

            Else
                Dim i3 = From SeedReports In d.SeedReports Where SeedReports.HasSubReports = True
                ReportFileName = i3.Single.ReportFileName
                HasSubReports = i3.Single.HasSubReports
            End If

            GetReport(ReportFileName, HasSubReports)
            ReportViewer2.RefreshReport()
        End If
    End Sub
    Public Sub GetReport(ByVal ReportFileName As String, ByVal HasSubReports As Integer)
        If d.UserReports Is Nothing OrElse d.UserReports.Count = 0 Then
            d.UserReports = New List(Of AvailableReport)
            FillReports()
            ReportsGridFormatting()
        End If

        Dim RunReports = From rr In d.UserReports Where rr.UserChecked = True

        If (RunReports.Count = 0) OrElse HasSubReports = 2 Then
            RunReports = From rr In d.UserReports Where rr.ReportFileName = ReportFileName
        End If

        Dim p1 As ReportParameter = Nothing
        Me.ReportViewer2.Reset()
        Me.ReportViewer2.LocalReport.ReportPath = ReportFileName
        Me.ReportViewer2.LocalReport.ReportEmbeddedResource = My.Application.Info.AssemblyName + "." + ReportFileName
        For Each rpt In RunReports
            Select Case rpt.ReportFileName
                Case "MixWorksheet.rdlc", "Submittal.rdlc", "Tags.rdlc", "USDeptAg.rdlc", "WarehouseMixWorksheet.rdlc"
                    SeedOrderReportDataSource.Name = "SeedOrder"
                    SeedOrderReportDataSource.Value = Me.SeedOrderBindingSource
                    Me.ReportViewer2.LocalReport.DataSources.Add(SeedOrderReportDataSource)
                    SeedOrderDetailReportDataSource.Name = "SeedOrderDetail"
                    SeedOrderDetailReportDataSource.Value = Me.SeedOrderDetailBindingSource
                    Me.ReportViewer2.LocalReport.DataSources.Add(SeedOrderDetailReportDataSource)
                Case "BagTagSingle.rdlc"
                    BagItemSingleSource.Name = "GetItemInfo"
                    BagItemSingleSource.Value = Me.GetItemInfoBindingSource
                    Me.ReportViewer2.LocalReport.DataSources.Add(BagItemSingleSource)
                Case "BagTagMix.rdlc"
                    GetMixBagTagsReportDataSource.Name = "GetMixBagTags"
                    GetMixBagTagsReportDataSource.Value = Me.MixBagTagsBindingSource
                    d.BagTagMixLabelTable = New List(Of LabelTable)
                    d.NetWeightPagesTable = New List(Of NetWeightPages)
                    Me.ReportViewer2.LocalReport.DataSources.Add(GetMixBagTagsReportDataSource)
                    Dim tmpTags = From m In d.MixBagTags Where m.UserChecked = True

                    For Each t In tmpTags
                        For p = 1 To t.MyPage
                            Dim a2 As New NetWeightPages(t.NetWeightId, p)
                            d.NetWeightPagesTable.Add(a2)
                        Next
                    Next

                    Dim col As Integer = 1
                    Dim row As Integer = 1


                    For c = 0 To col - 1
                        For r = 0 To row - 1
                            Dim a1 As New LabelTable(c, r)
                            d.BagTagMixLabelTable.Add(a1)
                        Next
                    Next


                    Dim OrderItems = From OID In d.OrderInfoDB.OrderItemDetails
                                     Join OI In d.OrderInfoDB.OrderItems On OID.ItemID Equals OI.ItemID
                                     Join o In d.OrderInfoDB.Orders On OI.OrderID Equals o.OrderID
                                     Where OI.OrderID = d.CurrentOrder.OrderID

                    Dim tmpBagTag = From u In d.MixBagTags Join oi In OrderItems On u.Kind Equals oi.o.Project
                                    Join nwp In d.NetWeightPagesTable On u.NetWeightId Equals nwp.NetWeightID
                                    From i In d.BagTagMixLabelTable
                                    Where u.UserChecked = True
                                    Order By oi.OI.ItemOrder
                                    Select New MixBagTag(u.NetWeightId, u.NetWeightDescription, u.UserChecked, u.Lot, u.Kind, oi.OID.Item, oi.OID.Purity, oi.OID.Germ, oi.OID.Inert, oi.OID.Weeds, oi.OID.Orgin, i.MyRow, i.MyColumn, nwp.MyPage)

                    MixBagTagsBindingSource.DataSource = tmpBagTag

                    p1 = New ReportParameter("NoxiousWeeds", NoxiousWeedsComboBox.Text)
                    Dim p2 = New ReportParameter("TestDate", TestDateTP.Value)
                    Dim reportParameters2() As ReportParameter = {p1, p2}
                    ReportViewer2.LocalReport.SetParameters(reportParameters2)
                Case "Items.rdlc"
                    ItemsSpreadsheetReportDataSource.Name = "ItemsSpreadsheet"
                    ItemsSpreadsheetReportDataSource.Value = Me.ItemsSpreadsheetBindingSource
                    Me.ReportViewer2.LocalReport.DataSources.Add(ItemsSpreadsheetReportDataSource)
                Case "Bag.rdlc"
                    Dim tmpBagOrderItems = From c In d.OrderInfoDB.BagOrderItems Where c.OrderID = d.CurrentOrder.OrderID
                    Dim tmpBagTagAcres = From t In d.OrderInfoDB.BagOrderItems
                                         Where t.OrderID = d.CurrentOrder.OrderID
                    If tmpBagTagAcres.Count > 0 Then
                        d.OrderInfoDB.BagOrderItems.DeleteAllOnSubmit(tmpBagTagAcres)
                        d.OrderInfoDB.SubmitChanges()
                    End If

                    Dim tmpOrderItems = From oi In d.OrderInfoDB.OrderItems Where oi.OrderID = d.CurrentOrder.OrderID
                    Dim tmpOrderItemsList As List(Of OrderItem) = tmpOrderItems.ToList
                    Dim tmpOrderItemDetails = From oid In d.OrderInfoDB.OrderItemDetails Join oi In d.OrderInfoDB.OrderItems On oid.ItemID Equals oi.ItemID
                                              Where oi.OrderID = d.CurrentOrder.OrderID
                                              Select oid.Item, oid.Lot, oid.[PLS_], oid.variety, oid.Purity, oid.Crop, oid.Inert, oid.Weeds, oid.Germ, oid.Dormant, oid.Total, oid.Test_Date, oid.Orgin, oid.Distributor,
                                              oid.Wholesale, oid.Retail, oid.ReorderQTY, oid.Reorder, oid.Discontinued, oid.ScientificName, oid.ItemID, oid.Type, oid.BotanicalName, oid.NoxiousWeeds, oi.OrderItemID
                    Dim tmpOrderItemDetailsList As New List(Of OrderItemDetail)
                    For Each i1 In tmpOrderItemDetails
                        Dim a1 As New OrderItemDetail
                        a1.Item = i1.Item
                        a1.Lot = i1.Lot
                        a1.PLS_ = i1.PLS_
                        a1.variety = i1.variety
                        a1.Purity = i1.Purity
                        a1.Crop = i1.Crop
                        a1.Inert = i1.Inert
                        a1.Weeds = i1.Weeds
                        a1.Germ = i1.Germ
                        a1.Dormant = i1.Dormant
                        a1.Total = i1.Total
                        a1.Test_Date = i1.Test_Date
                        a1.Orgin = i1.Orgin
                        a1.Distributor = i1.Distributor
                        a1.Wholesale = i1.Wholesale
                        a1.Retail = i1.Retail
                        a1.ReorderQTY = i1.ReorderQTY
                        a1.Reorder = i1.Reorder
                        a1.Discontinued = i1.Discontinued
                        a1.ScientificName = i1.ScientificName
                        a1.ItemID = i1.ItemID
                        a1.Type = i1.Type
                        a1.BotanicalName = i1.BotanicalName
                        a1.NoxiousWeeds = i1.NoxiousWeeds
                        tmpOrderItemDetailsList.Add(a1)
                    Next

                    Dim BagTagOrderItems = From OI In tmpOrderItemsList
                                           Join OrderItemDetails In tmpOrderItemDetailsList On OI.ItemID Equals OrderItemDetails.ItemID
                                           Join b In d.BagTagAcres On OI.OrderID Equals b.OrderID
                                           Where OI.OrderID.Equals(d.CurrentOrder.OrderID) And b.Acres > 0
                                           Select New BagOrderItem With {.CustomerName = d.CurrentCustomer.CustomerName,
                                        .InvoiceID = d.CurrentOrder.InvoiceID,
                                           .Acres = b.Acres,
                                           .ItemID = OI.ItemID,
                                           .OrderID = b.OrderID,
                                           .Project = d.CurrentOrder.Project,
                                           .PLS_LBS_PerAcre = OI.PLS_LBS_PerAcre,
                                           .PricePerPLSLB = OI.PricePerPLSLB,
                                            .PLS_Lbs = OI.PLS_LBS_PerAcre * b.Acres,
                                           .PLS_Percent = OI.PLS_Percent,
                                           .ControlNumber = d.CurrentOrder.ControlNumber,
                                           .UnitTypeName = d.CurrentOrderUnit.UnitTypeName,
                                           .MixName = d.CurrentOrder.MixName,
                                           .Inert = OrderItemDetails.Inert, .Purity = OrderItemDetails.Purity,
                                           .BULK_LBS = (OI.PLS_LBS_PerAcre * b.Acres) / OI.PLS_Percent,
                                           .Lot = OrderItemDetails.Lot,
                                           .Item = OrderItemDetails.Item,
                                           .Variety = OrderItemDetails.variety,
                                           .Origin = OrderItemDetails.Orgin,
                                           .Germ = OrderItemDetails.Germ,
                                           .Dormant = OrderItemDetails.Dormant,
                                           .Total = OrderItemDetails.Total,
                                           .Weeds = OrderItemDetails.Weeds,
                                           .Crop = OrderItemDetails.Crop,
                                           .OrderDate = d.CurrentOrder.OrderDate,
                                           .Bags = b.Bags,
                                           .OrderItemID = OI.OrderItemID}

                    d.OrderInfoDB.BagOrderItems.InsertAllOnSubmit(BagTagOrderItems)
                    Dim tmp = d.OrderInfoDB.GetChangeSet
                    d.OrderInfoDB.SubmitChanges()



                    Dim bagTable As New List(Of BagTagAcre)

                    Dim tmpBagTagAcresBags = From t In d.OrderInfoDB.BagOrderItems Where t.OrderID = d.CurrentOrder.OrderID
                                             Group t By t.Acres, t.OrderID, t.Bags Into MyGroup = Group
                                             Select Acres, OrderID, Bags

                    For Each b In tmpBagTagAcresBags
                        Dim bags As Integer = b.Bags
                        For i As Integer = 1 To bags
                            Dim a1 As New BagTagAcre(b.OrderID, b.Acres, 0, i)
                            bagTable.Add(a1)
                        Next
                    Next

                    Dim BagTagOrderItemsBags = From j In BagTagOrderItems Join k In bagTable On j.OrderID Equals k.OrderID And j.Acres Equals k.Acres
                                               Join oi In d.OrderInfoDB.OrderItems On j.OrderItemID Equals oi.OrderItemID
                                               Order By oi.ItemOrder
                                               Select New BagOrderItem With {.CustomerName = j.CustomerName,
                                                .InvoiceID = j.InvoiceID,
                                                   .Acres = j.Acres,
                                                   .ItemID = j.ItemID,
                                                   .OrderID = j.OrderID,
                                                   .Project = d.CurrentOrder.Project,
                                                   .PLS_LBS_PerAcre = j.PLS_LBS_PerAcre,
                                                   .PricePerPLSLB = j.PricePerPLSLB,
                                                    .PLS_Lbs = j.PLS_Lbs,
                                                   .PLS_Percent = j.PLS_Percent,
                                                   .ControlNumber = j.ControlNumber,
                                                   .UnitTypeName = j.UnitTypeName,
                                                   .MixName = j.MixName,
                                                   .Inert = j.Inert, .Purity = j.Purity,
                                                   .BULK_LBS = j.BULK_LBS,
                                                   .Lot = j.Lot,
                                                   .Item = j.Item,
                                                   .Variety = j.Variety,
                                                   .Origin = j.Origin,
                                                   .Germ = j.Germ,
                                                   .Dormant = j.Dormant,
                                                   .Total = j.Total,
                                                   .Weeds = j.Weeds,
                                                   .Crop = j.Crop,
                                                   .OrderDate = j.OrderDate,
                                                   .Bags = k.Bags,
                                                       .BagOrderItemID = j.BagOrderItemID}

                    GetBagTagReportDataSource.Name = "BagOrderItems"
                    GetBagTagReportDataSource.Value = BagTagBindingSource
                    BagTagBindingSource.DataSource = BagTagOrderItemsBags
                    Me.ReportViewer2.LocalReport.DataSources.Add(GetBagTagReportDataSource)
                    p1 = New ReportParameter("NoxiousWeeds", NoxiousWeedsComboBox.Text)
                    Dim reportParameters() As ReportParameter = {p1}
                    ReportViewer2.LocalReport.SetParameters(reportParameters)

            End Select

        Next




        'SeedReportDataSource.Name = "SeedReports"
        'SeedReportDataSource.Value = Me.VisibleReportsBindingSource



        'Me.ReportViewer2.LocalReport.DataSources.Add(OrderItemsReportDataSource)


        '
        'Me.ReportViewer2.LocalReport.DataSources.Add(GetMixItemInfoReportDataSource)
        'Me.ReportViewer2.LocalReport.DataSources.Add(GetMixItemOrderInfoReportDataSource)



        'If (ReportFileName = "Bag.rdlc") Or (ReportFileName = "AllReports.rdlc") Then

        'MyCurrentOrderItem.PricePerAcre = MyCurrentOrderItem.PricePerPLSLB * MyCurrentOrderItem.PLS_LBS_PerAcre
        'MyCurrentOrderItem.TotalPrice = MyCurrentOrderItem.PricePerAcre * d.CurrentOrder.Acres
        'MyCurrentOrderItem.BulkLbs = (MyCurrentOrderItem.PLS_LBS_PerAcre * d.CurrentOrder.Acres) / MyCurrentOrderItem.PLS_Percent
        'MyCurrentOrderItem.PLSLbs = MyCurrentOrderItem.PLS_LBS_PerAcre * d.CurrentOrder.Acres

        'GetMixItemInfoReportDataSource.Name = "GetMixItemInfo"
        'GetMixItemInfoReportDataSource.Value = Me.GetMixItemInfoBindingSource
        'GetMixItemOrderInfoReportDataSource.Name = "GetMixItemOrderInfo"
        'GetMixItemOrderInfoReportDataSource.Value = Me.GetMixItemOrderInfoBindingSource

        Me.ReportViewer2.Name = "ReportViewer2"
        If (HasSubReports = 1) Then
            Me.ReportViewer2.ProcessingMode = ProcessingMode.Local
            SeedReportDataSource.Name = "SeedReports"
            SeedReportDataSource.Value = Me.VisibleReportsBindingSource

            Me.ReportViewer2.LocalReport.DataSources.Add(SeedReportDataSource)
            AddHandler Me.ReportViewer2.LocalReport.SubreportProcessing, AddressOf AllReportsSubreportProcessingEventHandler
            Me.ReportViewer2.RefreshReport()
        End If
    End Sub


    Public Sub AllReportsSubreportProcessingEventHandler(ByVal sender As Object,
     ByVal e As SubreportProcessingEventArgs)
        e.DataSources.Add(OrderItemsReportDataSource)
        e.DataSources.Add(SeedOrderReportDataSource)
        e.DataSources.Add(SeedOrderDetailReportDataSource)

    End Sub
    Private Sub OrderStatusCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles OrderStatusCB.SelectedIndexChanged
        If Not d.CurrentOrder Is Nothing Then
            If Not (OrderStatusCB.SelectedItem Is Nothing OrElse OrderStatusCB.SelectedItem.OrderStatusID = 0) Then
                d.CurrentOrder.OrderStatusId = OrderStatusCB.SelectedValue
                d.OrderInfoDB.SubmitChanges()
            End If
        End If
    End Sub
    Public Sub CreateQbItem(ByVal QBType As String)
        Dim OrdersQuery = From Orders In d.OrderInfoDB.Orders
                          Join Customers In d.OrderInfoDB.OrderCustomerDetails On Orders.CustomerID Equals Customers.CustomerId
                          Join InvoiceLineItem In d.OrderInfoDB.InvoiceLineItemDetails On Orders.LineItemSKU Equals InvoiceLineItem.SKU
                          Where Orders.OrderID = d.CurrentOrder.OrderID
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
                              .LineItemSKU = Orders.LineItemSKU,
                              .IsMix = InvoiceLineItem.IsMix,
                              .LineItemDesc = InvoiceLineItem.Description
                              }

        Dim OrdersItemsQuery = From OrderItem In d.OrderInfoDB.OrderItems Join OrderItemDetails In d.OrderInfoDB.OrderItemDetails On OrderItem.ItemID Equals OrderItemDetails.ItemID Where OrderItem.OrderID = d.CurrentOrder.OrderID
                               Order By OrderItem.ItemOrder
                               Select New OrderItemDetails With {
                                .PricePerAcre = OrderItem.PricePerAcre,
                                   .TotalPrice = OrderItem.TotalPrice,
                                   .Lot = OrderItemDetails.Lot,
                                   .Item = OrderItemDetails.Item,
                                   .ItemOrder = OrderItem.ItemOrder
                              }
        Dim OrderItems = OrdersItemsQuery.ToArray()
        Dim Order = OrdersQuery.ToArray()
        Select Case QBType
            Case "Invoice"
                d.CurrentOrder.InvoiceID = QB.QBCreateTransaction(Order, OrderItems, "Invoice")
            Case "Quote"
                d.CurrentOrder.InvoiceID = QB.QBCreateTransaction(Order, OrderItems, "Quote")
            Case "Sales Receipt"
                d.CurrentOrder.InvoiceID = QB.QBCreateTransaction(Order, OrderItems, "SalesReceipt")
        End Select
        If (QBType = "Invoice") Then

        End If

        d.OrderInfoDB.SubmitChanges()
        InvoiceTB.Text = d.CurrentOrder.InvoiceID
    End Sub
    Public Sub UpdateInventory()
        CreateUpdateOrder()
        Dim CurrentInventoryItems = From orderitems In d.OrderInfoDB.OrderItems
                                    Where orderitems.OrderID.Equals(d.CurrentOrder.OrderID) And orderitems.BulkLbs > 0.00

        'Join OrderItemDetails In d.OrderInfoDB.OrderItemDetails On orderitems.ItemID Equals OrderItemDetails.ItemID 
        'Select Case New OrderItemDetails With {
        '                            .Lot = OrderItemDetails.Lot,
        '                            .PLS = OrderItemDetails.PLS_,
        '                            .Item = OrderItemDetails.Item,
        '                            .PLSLBSPerAcre = orderitems.PLS_LBS_PerAcre,
        '                            .PricePerPLSLB = orderitems.PricePerPLSLB,
        '                            .PricePerAcre = orderitems.PricePerAcre,
        '                            .TotalPrice = orderitems.PricePerAcre * d.CurrentOrder.Acres,
        '                            .Distributor = OrderItemDetails.Distributor,
        '                            .Wholesale = OrderItemDetails.Wholesale,
        '                            .Retail = OrderItemDetails.Retail,
        '                            .OrderItemID = orderitems.OrderItemID,
        '                            .BulkLbs = orderitems.BulkLbs,
        '                            .PLSLBS = orderitems.PLSLbs,
        '                            .ItemID = orderitems.ItemID,
        '                            .InventoryID = orderitems.inventoryId}
        'OrderItemsGridView.DataSource = CurrentOrderItems

        'Dim InventoryItems = From inventories In d.InventoryDB.Inventories Where inventories.InvoiceID = d.CurrentOrder.InvoiceID

        'If InventoryItems.Count = 0 Or d.CurrentOrder.InvoiceID Is Nothing Then
        For Each orderitem In CurrentInventoryItems
            Dim tmpInventory As Inventory
            If (orderitem.InventoryID = 0) Then
                tmpInventory = New Inventory
            Else
                Dim tmpInvs = From inv In d.InventoryDB.Inventories Where inv.InventoryID = orderitem.InventoryID
                tmpInventory = tmpInvs.Single
            End If
            tmpInventory.InventoryDate = d.CurrentOrder.OrderDate
            tmpInventory.Quantity = orderitem.BulkLbs * -1.0
            tmpInventory.InvoiceID = d.CurrentOrder.InvoiceID
            tmpInventory.Memo = "Order " + d.CurrentOrder.OrderID.ToString
            tmpInventory.ItemID = orderitem.ItemID
            tmpInventory.OrderId = d.CurrentOrder.OrderID
            tmpInventory.ItemGroupID = orderitem.ItemGroupID
            If (orderitem.InventoryID = 0) Then
                d.InventoryDB.Inventories.InsertOnSubmit(tmpInventory)
            End If
            d.InventoryDB.SubmitChanges()
            orderitem.InventoryID = tmpInventory.InventoryID
            d.OrderInfoDB.SubmitChanges()
        Next
        CurrentOrderItemDetails = Nothing
        FillOrderItems()

        ' End If
    End Sub
    Public Sub AddInventory(ByVal MyItemID As Integer, ByVal Quantity As Decimal?, ByVal InventoryDate As DateTime, ByVal Memo As String, ByVal OrderID As Integer)
        Dim Inventory As New Inventory
        Inventory.ItemID = MyItemID
        Inventory.Quantity = Quantity
        Inventory.InventoryDate = InventoryDate
        Inventory.Memo = Memo
        Inventory.OrderId = OrderID

        d.InventoryDB.Inventories.InsertOnSubmit(Inventory)
        d.InventoryDB.SubmitChanges()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        CreateUpdateOrder()

        If Not (d.CurrentOrder Is Nothing) Then
            Select Case QBItemTypeCB.Text
                Case "Invoice"
                    Dim dr As DialogResult = DialogResult.Yes
                    If Not (d.CurrentOrder.InvoiceID Is Nothing OrElse d.CurrentOrder.InvoiceID = "") Then
                        dr = MessageBox.Show("Invoice ID " + d.CurrentOrder.InvoiceID + " Exists.  Create Invoice?", "Create Invoice?", MessageBoxButtons.YesNo)
                    End If
                    If dr = DialogResult.Yes Then
                            CreateQbItem("Invoice")
                        End If

                Case "Sales Receipt"
                    CreateQbItem("Sales Receipt")
                    UpdateInventory()
                Case Else
                    CreateQbItem(QBItemTypeCB.Text)

            End Select
        End If


    End Sub

    Private Sub CustomerCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CustomerCB.SelectedIndexChanged
        If Not IsLoading AndAlso Not CustomerCB.SelectedItem Is Nothing AndAlso Not IsCustomerEdit Then
            PriceListCB.Text = Nothing
            d.CustomerPriceList = Nothing
            d.CurrentCustomer = CustomerCB.SelectedItem
            If (d.CurrentCustomer.QBId Is Nothing) Then
                d.CurrentCustomer.QBId = QB.DoQBCustomerListID(d.CurrentCustomer.CustomerName, "NameFilter", IsCustomerEdit)
                d.CustomersDB.SubmitChanges()
            End If
            d.CustomerPriceList = QB.DoPriceListQuery(d.CurrentCustomer.QBId, "ListIDList", IsCustomerEdit)
            PriceListCB.Text = d.CustomerPriceList
            If (d.CurrentOrder Is Nothing) Then
                IsNewOrder = True
            End If

        End If
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
        d.CurrentItem = Item
        FillInventoryData(d.CurrentItem)
        InventoryGridViewFormatting()
        OrdersPage.SelectedTab = InventoryTP
        IsLoading = False


    End Sub
    Private Sub Menu_Click_ShowOrderInventoryItems(ByVal sender As Object, ByVal e As System.EventArgs)
        IsLoading = True

        Dim SelectedRow As DataGridViewRow = OrdersGridView.CurrentRow
        Dim Orderdetails As OrderDetails = SelectedRow.DataBoundItem

        FillInventoryData(Orderdetails)
        InventoryGridViewFormatting()
        OrdersPage.SelectedTab = InventoryTP
        IsLoading = False


    End Sub

    Private Sub Menu_Click_AddItems(ByVal sender As Object, ByVal e As System.EventArgs)
        IsLoading = True
        Dim SelectedRow As DataGridViewRow = ItemsDataGridView.CurrentRow
        Dim Item As Item = SelectedRow.DataBoundItem
        d.CurrentItem = Item
        AddItemInventory(d.CurrentItem)

    End Sub
    Private Sub Menu_Click_AddInventoryItems(ByVal sender As Object, ByVal e As System.EventArgs)
        IsLoading = True
        Dim selectedRow As DataGridViewRow = InventoryDGV.CurrentRow
        Dim Inv As InventoryDetails = selectedRow.DataBoundItem
        ' From inventories In d.InventoryDB.Inventories Where inventories.InvoiceID = d.CurrentOrder.InvoiceID
        Dim item = From items In AllItems Where items.ItemID = Inv.ItemId
        d.CurrentItem = item.Single
        AddItemInventory(d.CurrentItem)
    End Sub
    Private Sub Menu_Click_GetBagTagSingle(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SelectedRow As DataGridViewRow = ItemsDataGridView.CurrentRow
        Dim i As Item = SelectedRow.DataBoundItem
        d.CurrentItem = i
        Me.GetItemInfoTableAdapter.Fill(Me.SeedDataSet.GetItemInfo, i.Lot)

        GetReport("BagTagSingle.rdlc", 0)

        ReportViewer2.RefreshReport()
        IsFillItems = True
        Me.OrdersPage.SelectedTab = ReportsTabPage
        IsFillItems = False
        ReportViewer2.Focus()
    End Sub

    Private Sub Menu_Click_DeleteItem(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each DeletedRow As DataGridViewRow In ItemsDataGridView.SelectedRows

            Dim i As Item = DeletedRow.DataBoundItem
            Dim i2 = From Items In AllItems Where Items.ItemID = i.ItemID
            For Each Item In i2
                If (MessageBox.Show("Delete Item " + Item.Item, "Delete Item?", MessageBoxButtons.YesNo)) = DialogResult.Yes Then
                    d.ItemsDB.Items.DeleteOnSubmit(Item)
                End If
            Next
        Next

        d.ItemsDB.SubmitChanges()
        AllItems = Nothing
        FillItemsData()
        FillItemsData()
        ItemsGridFormatting()
    End Sub
    Private Sub Menu_Click_DeleteOrder(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeletedRow As DataGridViewRow = OrdersGridView.CurrentRow
        Dim i As OrderDetails = DeletedRow.DataBoundItem
        Dim i2 = From Orders In d.OrderInfoDB.Orders Where Orders.OrderID = i.OrderID
        Dim i3 = From OrderItems In d.OrderInfoDB.OrderItems Where OrderItems.OrderID = i.OrderID
        For Each Order In i2
            If (MessageBox.Show("Delete Order " + Order.OrderID.ToString(), "Delete Order?", MessageBoxButtons.YesNo)) = DialogResult.Yes Then
                d.OrderInfoDB.Orders.DeleteOnSubmit(Order)
                For Each OrderItem In i3
                    d.OrderInfoDB.OrderItems.DeleteOnSubmit(OrderItem)
                Next
            End If
        Next
        d.OrderInfoDB.SubmitChanges()
        FillOrdersData()
        OrdersGridFormatting()
    End Sub
    Private Sub Menu_Click_DeleteInvoiceLineItemSKU(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeletedRow As DataGridViewRow = InvoiceLineItemDGV.CurrentRow
        Dim i As InvoiceLineItem = DeletedRow.DataBoundItem
        Dim i2 = From InvLinItemSku In d.InvoiceLineItemsDB.InvoiceLineItems Where InvLinItemSku.SKU = i.SKU

        For Each invoiceLineItem In i2
            If (MessageBox.Show("Delete Item " + invoiceLineItem.Description, "Delete Item?", MessageBoxButtons.YesNo)) = DialogResult.Yes Then
                d.InvoiceLineItemsDB.InvoiceLineItems.DeleteOnSubmit(invoiceLineItem)
            End If
        Next
        d.InvoiceLineItemsDB.SubmitChanges()
        FillInvoiceLineItems()


    End Sub
    Private Sub Menu_Click_DeleteCustomer(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeletedRow As DataGridViewRow = CustomerDataGridView.CurrentRow
        Dim i As Customer = DeletedRow.DataBoundItem
        Dim i2 = From Customers In d.CustomersDB.Customers Where Customers.CustomerId = i.CustomerId

        For Each Customer In i2
            If (MessageBox.Show("Delete Customer " + Customer.CustomerName, "Delete Customer?", MessageBoxButtons.YesNo)) = DialogResult.Yes Then
                d.CustomersDB.Customers.DeleteOnSubmit(Customer)
            End If
        Next
        d.CustomersDB.SubmitChanges()
        FillCustomerData()
        CustomerGridViewFormatting()
    End Sub

    Private Sub Menu_Click_DeleteInventoryItem(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeletedRow As DataGridViewRow = InventoryDGV.CurrentRow

        Dim i As InventoryDetails = DeletedRow.DataBoundItem
        'Dim i2 = From Inv In d.OrderInfoDB.OrderItems Where OrderItem.OrderItemID = i.OrderItemID
        Dim i2 = From Inv In d.InventoryDB.Inventories Where Inv.InventoryID = i.inventoryid
        Dim r As Integer = i2.Count
        For Each Inventory In i2
            If (MessageBox.Show("Delete Inventory Item " + Inventory.Memo, "Delete Inventory Item?", MessageBoxButtons.YesNo)) = DialogResult.Yes Then
                d.InventoryDB.Inventories.DeleteOnSubmit(Inventory)
            End If
        Next
        d.InventoryDB.SubmitChanges()

        FillInventoryData(d.CurrentItem)
    End Sub
    Private Sub Menu_Click_ShowItem(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SelectedRow As DataGridViewRow = OrderItemsGridView.CurrentRow
        Dim i As OrderItemDetails = SelectedRow.DataBoundItem
        Dim items = From i2 In AllItems Where i2.ItemID = i.ItemID
        ItemsSearchTB.Text = items.Single.Item
        PerformItemsSearch(i.ItemID)

    End Sub
    Private Sub Menu_Click_DeleteOrderItem(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each DeletedRow As DataGridViewRow In OrderItemsGridView.SelectedRows

            Dim i As OrderItemDetails = DeletedRow.DataBoundItem
            Dim i2 = From OrderItem In d.OrderInfoDB.OrderItems Where OrderItem.OrderItemID = i.OrderItemID
            Dim r As Integer = i2.Count
            For Each OrderItem In i2
                If (MessageBox.Show("Delete Item from Order?", "Delete Order Item", MessageBoxButtons.YesNo)) = DialogResult.Yes Then
                    d.OrderInfoDB.OrderItems.DeleteOnSubmit(OrderItem)

                    If OrderItem.InventoryID <> 0 Then
                        Dim inv = From invs In d.InventoryDB.Inventories Where invs.InventoryID = OrderItem.InventoryID Take 1

                        d.InventoryDB.Inventories.DeleteOnSubmit(inv.Single)
                        d.InventoryDB.SubmitChanges()
                    End If
                End If
            Next
        Next

        d.OrderInfoDB.SubmitChanges()
        CurrentOrderItemDetails = Nothing
        FillOrderItems()
    End Sub
    'Private Sub RightClickOrderItemsGridview(sender As Object, e As MouseEventArgs) Handles OrderItemsGridView.MouseClick
    '    If (e.Button = MouseButtons.Right) Then

    '        AddHandler ShowInventoryMenuItem.Click, AddressOf Me.Menu_Click_OrderShowItems

    '    End If
    'End Sub

    Private Sub Menu_Click_UpdateItemInfo(ByVal sender As Object, ByVal e As System.EventArgs)
        If (MessageBox.Show("Update Item Info?", "Update Item Info", MessageBoxButtons.YesNo)) = DialogResult.Yes Then

            For Each SelectedRow As DataGridViewRow In OrderItemsGridView.SelectedRows

                Dim i As OrderItemDetails = SelectedRow.DataBoundItem
                Dim i2 = From OrderItem In d.OrderInfoDB.OrderItems Where OrderItem.OrderItemID = i.OrderItemID
                For Each OrderItem In i2

                    RefreshOrderItemDetail(OrderItem, i)
                    For Each col As DataGridViewColumn In OrderItemsGridView.Columns
                        OrderItemDetailUpdate(OrderItem, col.Name, i)
                    Next

                Next
            Next
            d.OrderInfoDB.SubmitChanges()
            FillOrderItems()
        End If
    End Sub
    Private Sub Menu_Click_OrderShowItems(ByVal sender As Object, ByVal e As System.EventArgs)
        IsLoading = True
        IsFillItems = True
        FillInventoryData(d.CurrentOrder)
        OrdersPage.SelectedTab = InventoryTP
        InventoryGridViewFormatting()
        IsLoading = False
        IsFillItems = False
    End Sub


    Public Sub InventoryGridViewFormatting()
        IsFillItems = True
        For Each column As DataGridViewColumn In InventoryDGV.Columns
            Select Case column.HeaderCell.Value
                Case "inventoryid"
                    column.Visible = False
                Case "ItemId"
                    column.Visible = False
                Case "inventorydate"
                    column.HeaderText = "Inventory Date"
                Case "Item", "Lot", "AvailableInventory"
                    column.ReadOnly = True


                Case "Item"
                Case Else
                    column.Visible = True
            End Select

        Next
        IsFillItems = False
    End Sub
    Public Sub ItemsGridFormatting()
        IsLoading = True
        For Each column As DataGridViewColumn In ItemsDataGridView.Columns
            Select Case column.HeaderCell.OwningColumn.DataPropertyName
                Case "Item"
                    column.HeaderText = "Common Name"
                    column.Width = 183
                    'column.Width = 183
                Case "Lot"
                    column.Width = 90
                'ItemsDataGridView.Sort(column, System.ComponentModel.ListSortDirection.Ascending)
                Case "PLS_", "Purity", "Crop", "Inert", "Weeds", "Germ", "Dormant", "Total"
                    column.DefaultCellStyle.Format = "p2"
                    column.Width = 72
                    If (column.HeaderCell.OwningColumn.DataPropertyName = "PLS_") Then
                        column.HeaderText = "PLS %"
                    End If
                Case "Distributor", "Retail", "Wholesale"
                    column.DefaultCellStyle.Format = "c2"
                    column.Width = 82
                Case "Test_Date"
                    column.DefaultCellStyle.Format = "MM/yy"
                    column.Width = 82
                    column.HeaderText = "Test Date"
                Case "ItemID"
                    column.Visible = False
                Case "BotanicalName"
                    column.HeaderText = "Botanical Name"
                    column.Visible = False
                    'column.Width = 125
                Case "ScientificName"
                    column.HeaderText = "Scientific Name"
                    'column.Width = 125
                Case "variety"
                    column.HeaderText = "Variety"
                    column.Width = 64
                Case "IsDeleted"
                    column.Visible = False
                Case "ReorderQTY"
                    column.DefaultCellStyle.Format = "n0"
                    column.Width = 92
            End Select
        Next
        For Each Row As DataGridViewRow In ItemsDataGridView.Rows
            Dim InventoryCell As DataGridViewCell = Row.Cells("AvailableInventory")
            Dim TestingCell As DataGridViewCell = Row.Cells("Test_Date")
            If (InventoryCell.Value <= 0) Then
                InventoryCell.Style.ForeColor = Color.Red
                Row.DefaultCellStyle.SelectionBackColor = Color.Red
            End If
            If (DateDiff(DateInterval.Month, CDate(TestingCell.Value), Today) > 12) Then
                TestingCell.Style.ForeColor = Color.Red
                Row.DefaultCellStyle.SelectionBackColor = Color.Red
            End If
        Next

        IsLoading = False

    End Sub
    Public Sub BagTagAcreDGVFormatting()
        For Each column As DataGridViewColumn In BagTagAcreDGV.Columns
            Select Case column.HeaderCell.OwningColumn.DataPropertyName
                Case "NetWeightDescription"
                    column.HeaderText = "Net Weight"
                    column.Visible = True
                Case "UserChecked"
                    column.HeaderText = "Select"
                    column.Visible = True
                Case "MyPage"
                    column.HeaderText = "# of Pages"
                    column.Visible = False
                Case "Acres"
                    column.Visible = True
                Case "TotalAcres"
                    column.Visible = True
                Case "Bags"
                    column.Visible = True
                Case Else
                    column.Visible = False
            End Select
        Next
    End Sub
    Public Sub MixBagTagDGVFormatting()
        For Each column As DataGridViewColumn In MixBagTagDGV.Columns
            Select Case column.HeaderCell.OwningColumn.DataPropertyName
                Case "NetWeightDescription"
                    column.HeaderText = "Net Weight"
                    column.Visible = True
                Case "UserChecked"
                    column.HeaderText = "Select"
                    column.Visible = True
                Case "MyPage"
                    column.HeaderText = "# of Pages"
                    column.Visible = False
                Case "Acres"
                    column.Visible = True
                Case "TotalAcres"
                    column.Visible = True
                Case "Bags"
                    column.Visible = True
                Case Else
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
                    Case "MixName"
                    column.HeaderText = "Mix"
                Case "LineItemDesc", "IsMix"
                    column.Visible = False





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
                Case "AllowMultiple"
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
                    column.DefaultCellStyle.Format = "0.0000"
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
                Case "TestDate"
                    column.DefaultCellStyle.Format = "MM/yy"
                Case "WholesalePricePerAcre", "RetailPricePerAcre", "DistributorPricePerAcre"
                    column.Visible = False
                Case Else

            End Select
        Next

        For Each Row As DataGridViewRow In OrderItemsGridView.Rows
            Dim InventoryCell As DataGridViewCell = Row.Cells("AvailableInventory")
            Dim TestingCell As DataGridViewCell = Row.Cells("TestDate")
            If (InventoryCell.Value <= 0) Then
                InventoryCell.Style.ForeColor = Color.Red
                Row.DefaultCellStyle.SelectionBackColor = Color.Red
            End If
            If (DateDiff(DateInterval.Month, CDate(TestingCell.Value), Today) > 12) Then
                TestingCell.Style.ForeColor = Color.Red
                Row.DefaultCellStyle.SelectionBackColor = Color.Red
            End If
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
        CopyOrder.CustomerID = d.CurrentOrder.CustomerID
        CopyOrder.Project = d.CurrentOrder.Project
        CopyOrder.ControlNumber = d.CurrentOrder.ControlNumber
        CopyOrder.MixName = d.CurrentOrder.MixName
        CopyOrder.PriceList = d.CurrentOrder.PriceList
        CopyOrder.Acres = d.CurrentOrder.Acres
        CopyOrder.OrderNote = d.CurrentOrder.OrderNote
        CopyOrder.LineItemSKU = d.CurrentOrder.LineItemSKU
        CopyOrder.UnitTypeID = d.CurrentOrder.UnitTypeID
        CopyOrder.OverrideTotalPrice = d.CurrentOrder.OverrideTotalPrice
        CopyOrder.OrderStatusId = 1
        d.OrderInfoDB.Orders.InsertOnSubmit(CopyOrder)

        d.OrderInfoDB.SubmitChanges()

        For Each OrderItem In CurrentOrderItems
            Dim CopyOrderItem As New OrderItem
            CopyOrderItem.PLS_LBS_PerAcre = OrderItem.PLS_LBS_PerAcre
            CopyOrderItem.OrderID = CopyOrder.OrderID
            CopyOrderItem.PricePerAcre = Nothing
            CopyOrderItem.DistributorPricePerAcre = Nothing
            CopyOrderItem.RetailPricePerAcre = Nothing
            CopyOrderItem.WholesalePricePerAcre = Nothing
            CopyOrderItem.PricePerPLSLB = Nothing
            CopyOrderItem.PLSLbs = Nothing
            CopyOrderItem.BulkLbs = Nothing
            CopyOrderItem.ItemID = OrderItem.ItemID
            CopyOrderItem.PLS_Percent = OrderItem.PLS_Percent
            CopyOrderItem.ItemOrder = OrderItem.ItemOrder
            d.OrderInfoDB.OrderItems.InsertOnSubmit(CopyOrderItem)
        Next

        d.OrderInfoDB.SubmitChanges()
        d.CurrentOrder = CopyOrder
        ViewCurrentOrder()
        ReportViewer2.Reset()
        d.UserReports = New List(Of AvailableReport)
    End Sub

    Private Sub PriceListCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PriceListCB.SelectedIndexChanged
        If Not (d.CurrentOrder Is Nothing) AndAlso d.CurrentCustomer IsNot Nothing Then
            Dim OrderItemsNoPrice = From oi In d.OrderInfoDB.OrderItems Where oi.OrderID = d.CurrentOrder.OrderID And oi.PricePerAcre Is Nothing
            If d.CustomerPriceList <> PriceListCB.Text Or OrderItemsNoPrice.Count > 0 Then
                d.CustomerPriceList = PriceListCB.Text
                If Not (d.CurrentOrder Is Nothing) Then
                    d.CurrentOrder.PriceList = PriceListCB.Text
                    d.OrderInfoDB.SubmitChanges()
                End If
                If (OrderItemsGridView.RowCount > 0) AndAlso Not (PriceListCB.Text = "") Then
                    Dim UpatePrices As DialogResult = MessageBox.Show("Update Prices with " + PriceListCB.Text + " Prices", "Upate Prices?", MessageBoxButtons.YesNo)
                    If UpatePrices = DialogResult.Yes Then
                        UpdatePricesForItems()
                        MessageBox.Show("Update Complete")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PerformOrdersSearch()
    End Sub
    Private Sub AddCustomer()
        If Not (d.CurrentOrder Is Nothing) Then
            Dim CreateNewOrder As DialogResult = MessageBox.Show("Do you want to start New Order?", "New Order?", MessageBoxButtons.YesNo)
            If CreateNewOrder = DialogResult.Yes Then
                NewOrder()
            End If
        Else
            IsNewOrder = True
        End If
        IsCustomerEdit = True
        d.CurrentCustomer = Nothing
        Dim CustomerForm As New CustomersForm(d)
        Dim CreateCustomerInQB As DialogResult = CustomerForm.ShowDialog()
        If (CreateCustomerInQB = DialogResult.Yes) Then
            d.CurrentCustomer.QBId = QB.DoCustomerAdd(d.CurrentCustomer)
            d.CustomersDB.SubmitChanges()
        End If

        FillCustomerData()
        CustomerCB.SelectedValue = d.CurrentCustomer.CustomerId
        OrdersPage.SelectedTab = OrderTabPage
        IsCustomerEdit = False

    End Sub
    Private Sub AddItemInventory(ByVal Item As Item)
        Dim AddInventoryForm As New AddInventory(Item)
        AddInventoryForm.ShowDialog()
        FillInventoryData(d.CurrentItem)
    End Sub

    Private Sub SaveOrderBtn_Click_1(sender As Object, e As EventArgs) Handles SaveOrderBtn.Click
        IsFillItems = False
        CurrentOrderItemDetails = Nothing

        CreateUpdateOrder()
        For Each row As DataGridViewRow In OrderItemsGridView.Rows
            Dim MyOrderItemDetail As OrderItemDetails = row.DataBoundItem
            Dim MyOrderItems = From orderItems In d.OrderInfoDB.OrderItems
                               Where orderItems.OrderItemID = MyOrderItemDetail.OrderItemID
            UpdateOrderItemInformation(MyOrderItems.Single, MyOrderItemDetail)
        Next
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
    Private Sub ReportViewer2_ReportExport(sender As Object, e As ReportExportEventArgs)
        e.Cancel = True
        Dim ReportDirectory As String = Nothing
        Dim filePath As New StringBuilder
        Select Case Me.ReportViewer2.LocalReport.ReportPath
            Case "BagTagSingle.rdlc"
                ReportDirectory = DefaultReportDirectory
                filePath.Append(d.CurrentItem.Lot)
                filePath.Append("_Bag_Tag")
            Case Else
                ReportDirectory = DefaultReportDirectory + d.CurrentCustomer.CustomerName
                filePath.Append(d.CurrentOrder.InvoiceID)
                filePath.Append(" ")
                filePath.Append(d.CurrentOrder.MixName)
                filePath.Append(" ")
                filePath.Append(d.CurrentOrder.Project)
        End Select


        If Not Directory.Exists(ReportDirectory) Then
            Directory.CreateDirectory(ReportDirectory)
        End If

        Dim extension As String = GetRenderingExtension(e.Extension)
        filePath.Append(extension)
        Dim saveFileDialog As New SaveFileDialog()
        With saveFileDialog
            .Title = "Save As"
            .CheckPathExists = True
            .InitialDirectory = ReportDirectory
            .FileName = filePath.ToString()
            .Filter = e.Extension.LocalizedName + " (*" + extension + ")|*" + extension + "|All files(*.*)|*.*"
            .FilterIndex = 0
        End With

        If (saveFileDialog.ShowDialog(Me) = DialogResult.OK) Then
            Me.ReportViewer2.ExportDialog(e.Extension, e.DeviceInfo, saveFileDialog.FileName)
            'WriteReportsToDisk(saveFileDialog.FileName, e.Extension.LocalizedName)
            OpenFileWithPrompt(saveFileDialog.FileName)
        End If

    End Sub


    Public Sub OpenFileWithPrompt(ByVal file As String)
        If MessageBox.Show(file, "Open File?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Process.Start(file)
        End If

    End Sub

    Private Function GetRenderingExtension(ByRef extension As RenderingExtension) As String
        Select Case extension.LocalizedName
            Case "PDF"
                Return ".pdf"
            Case "CSV"
                Return ".csv"
            Case "Excel"
                Return ".xls"
            Case "MHTML"
                Return ".mhtml"
            Case "IMAGE"
                Return ".tif"
            Case "XML"
                Return ".xml"
            Case "Word"
                Return ".doc"
            Case "HTML4.0"
                Return ".html"
            Case "NULL"
                Throw New NotImplementedException("Extension not implemented.")
        End Select
        Return ""
    End Function

    Private Sub Button7_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub SaveInvoiceLineItemsBtn_CheckedChanged(sender As Object, e As EventArgs) Handles SaveInvoiceLineItemsBtn.CheckedChanged
        d.InvoiceLineItemsDB.SubmitChanges()
        d.NetWeightDB.SubmitChanges()
        FillInvoiceLineItems()
    End Sub

    Private Sub ReportsDGV_CellContentClick(sender As Object, e As EventArgs) Handles ReportsDGV.CurrentCellDirtyStateChanged, ReportsDGV.CellContentClick
        If Not IsLoading AndAlso (ReportsDGV.IsCurrentCellDirty) AndAlso DirectCast(sender, System.Windows.Forms.DataGridView).CurrentCell.OwningColumn.DataPropertyName = "UserChecked" Then
            ReportsDGV.CommitEdit(DataGridViewDataErrorContexts.Commit)
            Dim ar As AvailableReport = sender.currentRow.databounditem
            'ar.UserChecked = ReportsDGV.SelectedCells(0).Value
            ReportSpecificUpdates(ar)
        End If

    End Sub



    Private Sub ReportSpecificUpdates(ByVal ar As AvailableReport)
        TestDateLbl.Visible = False
        TestDateTP.Visible = False
        NoxiousWeedsComboBox.Visible = False
        NoxiousWeedsLbl.Visible = False

        MixBagTagDGV.Visible = False
        BagTagAcreDGV.Visible = False

        Dim NeedsTestDate As Boolean = False
        Dim NeedsNoxiousWeeds As Boolean = False
        Dim NeedsBagTagAcresInput As Boolean = False
        Dim NeedsMixBagTagInput As Boolean = False

        Dim RunReports = From rr In d.UserReports Where rr.UserChecked = True


        For Each rpt In RunReports
            Select Case rpt.ReportFileName
                Case "MixWorksheet.rdlc", "Submittal.rdlc", "Tags.rdlc", "USDeptAg.rdlc", "WarehouseMixWorksheet.rdlc"

                Case "BagTagMix.rdlc"
                    IsLoading = True
                    NeedsTestDate = True
                    NeedsNoxiousWeeds = True
                    NeedsMixBagTagInput = True
                    Dim atestDate = From OI In d.OrderInfoDB.OrderItems Join OID In d.OrderInfoDB.OrderItemDetails On OI.ItemID Equals OID.ItemID
                                    Where OI.OrderID = d.CurrentOrder.OrderID
                                    Order By OID.Test_Date Descending
                                    Take 1

                    TestDateTP.Value = atestDate.Single.OID.Test_Date

                    Dim NoxiousWeeds = From OI In d.OrderInfoDB.OrderItems Join OID In d.OrderInfoDB.OrderItemDetails On OI.ItemID Equals OID.ItemID
                                       Where OI.OrderID = d.CurrentOrder.OrderID
                                       Group By OID = OID.NoxiousWeeds
                                   Into NW = Group
                    NoxiousWeedsComboBox.DataSource = NoxiousWeeds
                    NoxiousWeedsComboBox.ValueMember = "OID"
                    NoxiousWeedsComboBox.DisplayMember = "OID"
                    FillMixBagTags()
                    MixBagTagDGV.AutoGenerateColumns = True
                    MixBagTagDGV.DataSource = d.MixBagTags
                    d.UserReportsGridReport = ar.ReportFileName
                    MixBagTagDGVFormatting()
                Case "Bag.rdlc"
                    IsLoading = True
                    NeedsTestDate = True
                    NeedsNoxiousWeeds = True
                    NeedsBagTagAcresInput = True
                    NeedsMixBagTagInput = False
                    Dim NoxiousWeeds = From OI In d.OrderInfoDB.OrderItems Join OID In d.OrderInfoDB.OrderItemDetails On OI.ItemID Equals OID.ItemID
                                       Where OI.OrderID = d.CurrentOrder.OrderID
                                       Group By OID = OID.NoxiousWeeds
                                       Into NW = Group
                    NoxiousWeedsComboBox.DataSource = NoxiousWeeds
                    NoxiousWeedsComboBox.ValueMember = "OID"
                    NoxiousWeedsComboBox.DisplayMember = "OID"

                    BagTagAcreDGV.AllowUserToAddRows = False
                    FillBagTagAcres()
                    BagTagAcreDGV.AutoGenerateColumns = True
                    Me.BagTagBindingSource.DataSource = d.BagTagAcres
                    BagTagAcreDGV.DataSource = d.BagTagAcres
                    BagTagAcreDGVFormatting()
                    d.UserReportsGridReport = ar.ReportFileName
                    IsLoading = False
            End Select
        Next

        NoxiousWeedsComboBox.Visible = NeedsNoxiousWeeds
        NoxiousWeedsLbl.Visible = NeedsNoxiousWeeds
        BagTagAcreDGV.Visible = NeedsBagTagAcresInput
        MixBagTagDGV.Visible = NeedsMixBagTagInput
        TestDateLbl.Visible = NeedsTestDate
        TestDateTP.Visible = NeedsTestDate

    End Sub
    Private Sub SyncWithQuickBooks()
        Dim SyncItems = From i In AllItems Where i.Discontinued = False

        For Each item In SyncItems
            If (item.QBListID <> "NotQBItem") Then
                Dim ListID As String = QB.DoItemAddUpdate(item)
                If (ListID = "Cancel") Then
                    Exit For
                End If
                If (item.QBListID <> ListID) Then
                    item.QBListID = ListID
                End If
            End If
        Next
        d.ItemsDB.SubmitChanges()

    End Sub


    Private Sub ShowDiscontinuedItemsCB_CheckedChanged(sender As Object, e As EventArgs) Handles ShowDiscontinuedItemsCB.CheckedChanged
        PerformItemsSearch()
    End Sub

    Private Sub ItemsDataGridView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles ItemsDataGridView.CellEndEdit
        If Not IsLoading Then
            Dim col As DataGridViewColumn = ItemsDataGridView.Columns(e.ColumnIndex)

            If (col.DefaultCellStyle.Format.Contains("p")) Then
                Dim Cell As DataGridViewCell = ItemsDataGridView.CurrentCell
                Dim t As Decimal = Cell.Value / 100.0
                Cell.Value = t
            End If
            If (col.Name = "Test_Date") Then
                Dim Cell As DataGridViewCell = ItemsDataGridView.CurrentCell
                Dim d As DateTime = Cell.Value
                Dim year As String = d.Day
                Dim month As String = d.Month
                Cell.Value = CType(month + "/01/" + year, Date)
            End If
            If (col.Name = "Purity") OrElse (col.Name = "Total") Then
                Dim PLSCell As DataGridViewCell = ItemsDataGridView("PLS_", e.RowIndex)
                Dim PurityCell As DataGridViewCell = ItemsDataGridView("Purity", e.RowIndex)
                Dim TotalCell As DataGridViewCell = ItemsDataGridView("Total", e.RowIndex)
                PLSCell.Value = PurityCell.Value * TotalCell.Value
            End If
        End If

    End Sub

    Private Sub ItemsDataGridView_DefaultValuesNeeded(sender As Object, e As DataGridViewRowEventArgs) Handles ItemsDataGridView.DefaultValuesNeeded
        With e.Row
            .Cells("Discontinued").Value = False
            .Cells("Reorder").Value = False
            .Cells("ReorderQTY").Value = CType(0.00, Decimal)
            .Cells("NoxiousWeeds").Value = "None Found"
            .Cells("PLS_").Value = CType(0.00, Decimal)
            .Cells("Purity").Value = CType(0.00, Decimal)
            .Cells("Crop").Value = CType(0.00, Decimal)
            .Cells("Inert").Value = CType(0.00, Decimal)
            .Cells("Weeds").Value = CType(0.00, Decimal)
            .Cells("Germ").Value = CType(0.00, Decimal)
            .Cells("Dormant").Value = CType(0.00, Decimal)
            .Cells("Total").Value = CType(0.00, Decimal)
            .Cells("Distributor").Value = CType(0.00, Decimal)
            .Cells("Wholesale").Value = CType(0.00, Decimal)
            .Cells("Retail").Value = CType(0.00, Decimal)
            .Cells("Test_Date").Value = CType("1/1/1900", Date)


        End With
    End Sub

    Private Sub UpdateInventoryBtn_Click(sender As Object, e As EventArgs) Handles UpdateInventoryBtn.Click
        UpdateInventory()
    End Sub

    Private Sub AddInventoryBtn_Click(sender As Object, e As EventArgs) Handles AddInventoryBtn.Click
        AddItemInventory(d.CurrentItem)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs)

    End Sub

    Public Sub RefreshOrderItemDetail(ByVal OrderItem As OrderItem, ByVal OrderItemDetail As OrderItemDetails)
        Dim items = From i In AllItems Where i.ItemID = OrderItem.ItemID
        Dim item As Item = items.Single
        OrderItemDetail.PLS = item.PLS_
        OrderItem.PLS_Percent = item.PLS_
        d.OrderInfoDB.SubmitChanges()

    End Sub

    Private Sub AddCustomerBtn_Click(sender As Object, e As EventArgs) Handles AddCustomerBtn.Click
        AddCustomer()
    End Sub

    Private Sub UnitsTB_TextChanged(sender As Object, e As EventArgs) Handles UnitsTB.Leave
        For Each row As DataGridViewRow In OrderItemsGridView.Rows
            Dim orderItemDetail As OrderItemDetails = row.DataBoundItem
            Dim OrderItems = From oi In d.OrderInfoDB.OrderItems Where oi.OrderItemID = orderItemDetail.OrderItemID
            Dim OrderItem As OrderItem = OrderItems.Single

            OrderItemDetailUpdate(OrderItem, "Acres", orderItemDetail)

        Next
        FillOrderItems()
    End Sub

    Private Sub ItemsDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles ItemsDataGridView.CellContentClick

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        NewOrder()
        OrdersPage.SelectedTab = OrderTabPage

    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        CopyOrder()
        OrdersPage.SelectedTab = OrderTabPage
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Select Case OrdersPage.SelectedTab.ToString
            Case "TabPage: {Order}"
                If Not (d.CurrentOrder Is Nothing AndAlso d.CurrentCustomer Is Nothing) Then
                    If (MessageBox.Show("Delete Order for " + d.CurrentCustomer.CustomerName, "Delete Order?", MessageBoxButtons.YesNo)) = DialogResult.Yes Then
                        d.OrderInfoDB.Orders.DeleteOnSubmit(d.CurrentOrder)
                        d.OrderInfoDB.SubmitChanges()
                        NewOrder()
                        OrdersPage.SelectedTab = OrderTabPage
                    End If
                End If
            Case "TabPage: {Orders}"
                If Not OrdersGridView.SelectedRows Is Nothing Then
                    For Each GVR As DataGridViewRow In OrdersGridView.SelectedRows
                        Dim OCD As OrderDetails = GVR.DataBoundItem
                        If (MessageBox.Show("Delete Order for " + OCD.CustomerName, "Delete Order?", MessageBoxButtons.YesNo)) = DialogResult.Yes Then
                            Dim O = From DelOrder In d.OrderInfoDB.Orders Where DelOrder.OrderID = DelOrder.OrderID Take 1
                            d.OrderInfoDB.Orders.DeleteOnSubmit(O.Single)
                            d.OrderInfoDB.SubmitChanges()

                        End If
                    Next

                End If

        End Select


    End Sub

    Private Sub RefreshItemDetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshItemDetailToolStripMenuItem.Click
        Dim refr As DialogResult = MessageBox.Show("Refresh Item Detail?", "Refresh Detail", MessageBoxButtons.YesNo)
        If (refr = DialogResult.Yes) Then
            For Each row As DataGridViewRow In OrderItemsGridView.Rows
                Dim orderItemDetail As OrderItemDetails = row.DataBoundItem
                Dim OrderItems = From oi In d.OrderInfoDB.OrderItems Where oi.OrderItemID = orderItemDetail.OrderItemID
                Dim OrderItem As OrderItem = OrderItems.Single
                RefreshOrderItemDetail(OrderItem, orderItemDetail)
                For Each col As DataGridViewColumn In OrderItemsGridView.Columns
                    OrderItemDetailUpdate(OrderItem, col.Name, orderItemDetail)
                Next
            Next
        End If
        OrdersPage.SelectedTab = OrderTabPage
        FillOrderItems()

    End Sub

    Private Sub UnitsTB_Layout(sender As Object, e As LayoutEventArgs) Handles UnitsTB.Layout

    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Cursor = Cursors.WaitCursor
        'Me.ReportViewer2.ExportDialog(extension, e.DeviceInfo, saveFileDialog.FileName)
        'Dim 

        Dim UserChecked = From uc In d.UserReports Where uc.UserChecked = True
        m_streams = New List(Of Stream)()
        RefreshDataSources()
        For Each r As AvailableReport In UserChecked
            WriteReportsToDisk(r)
        Next
        Print()
        Dispose()
        Me.Cursor = Cursors.Default
    End Sub

    Private m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Private Function CreateStream(name As String,
         fileNameExtension As String,
              encoding As Encoding, mimeType As String,
                  willSeek As Boolean) As Stream
        Dim stream As Stream = New FileStream(name + "." + fileNameExtension, FileMode.Create)
        m_streams.Add(stream)

        Return stream
    End Function
    Private Sub PrintPage(sender As Object, ev As PrintPageEventArgs)
        Dim pageImage As New Metafile(m_streams(m_currentPageIndex))
        ev.Graphics.DrawImage(pageImage, ev.PageBounds)

        m_currentPageIndex += 1
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count)
    End Sub
    Private Function CleanFileName(ByVal UserInput As String, ByVal Type As String) As String
        Select Case Type
            Case "Directory"
                For Each invalidChar In IO.Path.GetInvalidPathChars
                    UserInput = UserInput.Replace(invalidChar, "")
                Next
                UserInput = Replace(UserInput, ".", "")
                UserInput = Replace(UserInput, ",", "")
            Case "FilePath"

                For Each invalidChar In IO.Path.GetInvalidFileNameChars
                    UserInput = UserInput.Replace(invalidChar, "")
                Next
                UserInput = Replace(UserInput, ".", "")
                UserInput = Replace(UserInput, ",", "")
        End Select

        Return UserInput

    End Function

    Private Sub Print()
        Const printerName As String = "Microsoft Print To PDF"
        Dim ReportDirectory As String = Nothing
        Dim filePath As New StringBuilder

        ReportDirectory = DefaultReportDirectory + CleanFileName(d.CurrentCustomer.CustomerName, "Directory")


        If (d.CurrentOrder.InvoiceID.Length > 0) Then
            filePath.Append(d.CurrentOrder.InvoiceID)
        End If
        If (d.CurrentOrder.MixName.Length > 0) Then
            filePath.Append("_")
            filePath.Append(d.CurrentOrder.MixName)
        End If
        If (d.CurrentOrder.Project.Length > 0) Then
            filePath.Append("_")
            filePath.Append(d.CurrentOrder.Project)
        End If

        If Not Directory.Exists(ReportDirectory) Then
            Directory.CreateDirectory(ReportDirectory)
        End If

        Dim extension As String = ".pdf"

        Dim myFile As String = CleanFileName(filePath.ToString, "FilePath")
        myFile = ReportDirectory + "\" + myFile + extension
        If m_streams Is Nothing Or m_streams.Count = 0 Then
            Return
        End If
        Dim printDoc As New PrintDocument

        printDoc.PrinterSettings.PrinterName = printerName
        'AddHandler .PrintPage, AddressOf Me.PrintPageHandler
        If My.Computer.FileSystem.FileExists(myFile) Then My.Computer.FileSystem.DeleteFile(myFile)
        printDoc.DefaultPageSettings.PrinterSettings.PrintToFile = True
        printDoc.DefaultPageSettings.PrinterSettings.PrintFileName = myFile

        'RemoveHandler .PrintPage, AddressOf Me.PrintPageHandler

        If Not printDoc.PrinterSettings.IsValid Then
            Dim msg As String = String.Format("Can't find printer ""{0}"".", printerName)
            Debug.WriteLine(msg)
            Return
        End If
        AddHandler printDoc.PrintPage, AddressOf PrintPage
        printDoc.Print()
        OpenFileWithPrompt(myFile)
    End Sub
    Private Sub WriteReportsToDisk(ByVal Report As AvailableReport)
        Dim deviceInfo As String =
         "<DeviceInfo>" +
         "  <OutputFormat>EMF</OutputFormat>" +
         "  <PageWidth>8.5in</PageWidth>" +
         "  <PageHeight>11in</PageHeight>" +
         "  <MarginTop>0.20in</MarginTop>" +
         "  <MarginLeft>0.50in</MarginLeft>" +
         "  <MarginRight>0.50in</MarginRight>" +
         "  <MarginBottom>0.20in</MarginBottom>" +
         "</DeviceInfo>"
        Dim warnings() As Warning = Nothing

        GetReport(Report.ReportFileName, 2)
        ReportViewer2.RefreshReport()
        ReportViewer2.LocalReport.Render("Image", deviceInfo,
              AddressOf CreateStream, warnings)

        Dim stream As Stream
        For Each stream In m_streams
            stream.Position = 0
        Next

        m_currentPageIndex = 0

    End Sub
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        If Not (m_streams Is Nothing) Then
            Dim stream As Stream
            For Each stream In m_streams
                stream.Close()
            Next
            m_streams = Nothing
        End If
    End Sub

    Private Sub BagTagAcreDGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BagTagAcreDGV.CellEndEdit
        If d.UserReportsGridReport = "Bag.rdlc" Then
            BagTagAcreDGV.CommitEdit(DataGridViewDataErrorContexts.Commit)
            Dim CurrentTotalAcres As Decimal = 0.00
            Dim HasZero As Boolean = False
            For Each dr In d.BagTagAcres
                CurrentTotalAcres = (dr.Acres * dr.Bags) + CurrentTotalAcres
                If dr.Acres = 0 Then
                    HasZero = True
                End If
            Next

            If CurrentTotalAcres < d.CurrentOrder.Acres And Not HasZero Then
                Dim a3 As New BagTagAcre(d.CurrentOrder.OrderID, 0.0, d.CurrentOrder.Acres, 1)
                d.BagTagAcres.Add(a3)
            End If

            'FillBagTagAcres()
        End If

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        SyncWithQuickBooks()
        FillItemsData()
    End Sub

    Private Sub OverrideTotalPriceTB_CheckedChanged(sender As Object, e As EventArgs) Handles OverrideTotalPriceTB.CheckedChanged
        Select Case OverrideTotalPriceTB.Checked
            Case True
                TotalPricePerAcreTB.ReadOnly = False
                OrderTotalTB.ReadOnly = False
            Case False
                TotalPricePerAcreTB.ReadOnly = True
                OrderTotalTB.ReadOnly = True
                FillOrderItems()
        End Select


    End Sub

    Private Sub OrderTotalTB_TextChanged(sender As Object, e As EventArgs) Handles OrderTotalTB.TextChanged
        AllOrders = Nothing
    End Sub
End Class
