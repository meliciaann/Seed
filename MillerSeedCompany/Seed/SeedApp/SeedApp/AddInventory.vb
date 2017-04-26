Imports System.Data.Linq
Imports System.Windows.Forms
Imports SeedGeneral
Imports SeedApp.ItemsForm
Public Class AddInventory
    Public MyItemId As Integer
    Private InventoryDB As New InventoryDataContext
    Public d As New GetDataClass


    Public Sub New(ByVal Item As Item)
        InitializeComponent()
        MyItemId = Item.ItemID
        d.InventoryDB = New InventoryDataContext
        Dim ItemText As String = Nothing
        If (Item.Item IsNot Nothing) Then
            ItemTB.Text = Item.Item
        End If
        If (Item.Lot IsNot Nothing) Then
            LotTB.Text = Item.Lot
        End If
        Dim InventoryHistory = From Inventory In d.InventoryDB.Inventories Join InventoryItemDetail In d.InventoryDB.InventoryItemDetails On Inventory.ItemID Equals InventoryItemDetail.ItemID
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
                                   .AvailableInventory = Item.AvailableInventory,
                                   .OrderId = 0,
                                   .ItemGroupID = Item.ItemGroupID}


        Dim Balance As Decimal? = 0.00
        For Each inventoryItem As InventoryDetails In InventoryHistory
            Balance = Balance + inventoryItem.Quantity.Value
        Next
        CurrentAvailableTB.Text = Balance.Value.ToString()

    End Sub
    Private Sub InventoryAddBtn_Click(sender As Object, e As EventArgs) Handles InventoryAddBtn.Click
        ItemsForm.AddInventory(MyItemId, InventoryQtyTB.Text, InventoryDatePicker.Value, MemoTB.Text, InventoryOrderIdTB.Text)
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub


End Class