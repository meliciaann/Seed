Public Class OrderItemDetails
    Public Property Lot As String
    Public Property PLS As String
    Public Property Item As String
    Public Property PLSLBSPerAcre As Decimal?
    Public Property PricePerPLSLB As Decimal?
    Public Property PricePerAcre As Decimal?
    Public Property TotalPrice As Decimal?
    Public Property Distributor As Decimal
    Public Property Wholesale As Decimal
    Public Property Retail As Decimal
    Public Property OrderItemID As Integer
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

    Public CustomerZip As String

End Class
