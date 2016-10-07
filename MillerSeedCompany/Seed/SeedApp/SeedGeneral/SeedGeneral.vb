Imports System.Data.Linq

Public Class OrderItemDetails
    Public Property Lot As String
    Public Property PLS As Decimal?
    Public Property Item As String
    Public Property PLSLBSPerAcre As Decimal?
    Public Property PricePerPLSLB As Decimal?
    Public Property PricePerAcre As Decimal?
    Public Property TotalPrice As Decimal?
    Public Property Distributor As Decimal?
    Public Property Wholesale As Decimal?
    Public Property Retail As Decimal?
    Public Property OrderItemID As Integer

    Public Property BulkLbs As Decimal?

    Public Property PLSLBS As Decimal?

    Public Property ItemID As Integer
End Class

Public Class OrderDetails
    Public Property OrderID As Integer
    Public Property InvoiceID As String
    Public Property Project As String
    Public Property OrderDate As DateTime = "1/1/1900"
    Public Property OrderTotal As Decimal?
    Public Property CustomerName As String
    Public Property CustomerAddressLine1 As String
    Public Property CustomerCity As String
    Public Property CustomerState As String
    Public Property CustomerZip As String

    Public Property Acres As Decimal?

    Public Property PricePerAcre As Decimal?
    Public Property PriceList As String

End Class

Public Class InventoryDetails
    Public Property inventoryid As Integer
    Public Property ItemId As Integer
    Public Property inventorydate As DateTime
    Public Property Quantity As Decimal?
    Public Property Memo As String
    Public Property Item As String
    Public Property Lot As String

    Public Property InvoiceID As String

End Class

Public Class ItemTypes
    Public Property ItemType As String
End Class


