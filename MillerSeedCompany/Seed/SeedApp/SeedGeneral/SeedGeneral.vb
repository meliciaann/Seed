Imports System.Data.Linq
Imports System.ComponentModel
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection
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

    Public Property TestDate As DateTime
End Class

Public Class OrderDetails
    Public Property OrderID As Integer
    Public Property InvoiceID As String
    Public Property Project As String
    Public Property OrderDate As DateTime = "1/1/1900"
    Public Property OrderTotal As Decimal?
    Public Property CustomerName As String
    Public Property CustomerAddressLine1 As String

    Public Property CustomerAddressLine2 As String
    Public Property CustomerCity As String
    Public Property CustomerState As String
    Public Property CustomerZip As String

    Public Property CustomerEmail As String
    Public Property CustomerPhone As String

    Public Property Acres As Decimal?

    Public Property PricePerAcre As Decimal?
    Public Property PriceList As String

    Public MixName As String

    Public ControlNumber As String

    Public IsMix As Boolean

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
Public Class AvailableReport

    Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)

    Private _ReportFileName As String

    Private _FriendlyName As String

    Private _ReportID As Long

    Private _IsVisible As System.Nullable(Of Boolean)

    Private _SortOrder As System.Nullable(Of Integer)

    Private _UserChecked As System.Nullable(Of Boolean)

    Private _HasSubReports As Boolean

    Public Property ReportFileName As String
        Get
            Return _ReportFileName
        End Get
        Set(ByVal value As String)
            _ReportFileName = value
        End Set
    End Property

    Public Property ReportID As Long
        Get
            Return _ReportID
        End Get
        Set(ByVal value As Long)
            _ReportID = value
        End Set
    End Property

    Public Property FriendlyName As String
        Get
            Return _FriendlyName
        End Get
        Set(ByVal value As String)
            _FriendlyName = value
        End Set
    End Property

    Public Property IsVisible As Boolean
        Get
            Return _IsVisible
        End Get
        Set(value As Boolean)
            _IsVisible = value
        End Set
    End Property
    Public Property SortOrder As Integer
        Get
            Return _SortOrder
        End Get
        Set(value As Integer)
            _SortOrder = value
        End Set
    End Property

    Public Property UserChecked As Boolean
        Get
            Return _UserChecked
        End Get
        Set(value As Boolean)
            _UserChecked = value
        End Set
    End Property

    Public Property HasSubReports As Boolean
        Get
            Return _HasSubReports
        End Get
        Set(value As Boolean)
            _HasSubReports = value
        End Set
    End Property

    Public Sub New(ByVal ReportFileName As String, ByVal ReportID As Long, ByVal FriendlyName As String, ByVal SortOrder As Integer, ByVal UserChecked As Boolean, ByVal IsVisible As Boolean)
        _ReportFileName = ReportFileName
        _ReportID = ReportID
        _FriendlyName = FriendlyName
        _SortOrder = SortOrder
        _UserChecked = UserChecked
        _IsVisible = IsVisible
    End Sub
End Class
Public Class ItemTypes
    Public Property ItemType As String
End Class

'Public Class GetData
'    Private _ItemsDB As ItemsEditDataContext

'    Public Property ItemsDB As ItemsEditDataContext
'        Get
'            Return _ItemsDB
'        End Get
'        Set(value As ItemsEditDataContext)
'            _ItemsDB = value
'        End Set
'    End Property

'    'Public ItemsDB As New SeedGeneral.ItemsEditDataContext

'End Class


