Imports System.Data.Linq
Imports System.ComponentModel
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Windows.Forms
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

Public Class GetDataClass
    Private _ItemsDB As ItemsEditDataContext
    Private _Items As Table(Of Item)
    Private _SeedReportsDB As SeedReportsDataContext
    Private _SeedReports As Table(Of SeedReport)
    Private _UserReports As List(Of AvailableReport)
    Private _VisibleReportsBindingSource As BindingSource
    Private _CustomersDB As CustomersDataContext
    Private _Customers As Table(Of Customer)
    Private _CurrentOrder As Order
    Private _OrderInfoDB As OrderItemsDataContext
    Private _OrderItems As Table(Of OrderItem)
    Private _Orders As Table(Of Order)
    Private _OrderStatusDB As OrderStatusDataContext
    Public _InventoryDB As InventoryDataContext
    Public _Inventory As Table(Of Inventory)
    Public _OrderStatus As Table(Of OrderStatus)
    Public _CurrentOrderItem As OrderItemDetails
    Public _CurrentCustomer As SeedGeneral.Customer
    Public _CustomerPriceList As String
    Public _ItemID As Integer
    Public _CurrentOrderUnit As OrderUnit

    Public Property ItemsDB As ItemsEditDataContext
        Get
            Return _ItemsDB
        End Get
        Set(value As ItemsEditDataContext)
            _ItemsDB = value
        End Set
    End Property

    Public Property Items As Table(Of Item)
        Get
            Return _Items
        End Get
        Set(value As Table(Of Item))
            _Items = value
        End Set
    End Property

    Public Property SeedReportsDB As SeedReportsDataContext
        Get
            Return _SeedReportsDB
        End Get
        Set(value As SeedReportsDataContext)
            _SeedReportsDB = value
        End Set
    End Property

    Public Property SeedReports As Table(Of SeedReport)
        Get
            Return _SeedReports
        End Get
        Set(value As Table(Of SeedReport))
            _SeedReports = value
        End Set
    End Property

    Public Property UserReports As List(Of AvailableReport)
        Get
            Return _UserReports
        End Get
        Set(value As List(Of AvailableReport))
            _UserReports = value
        End Set
    End Property

    Public Property CustomersDB As CustomersDataContext
        Get
            Return _CustomersDB
        End Get
        Set(value As CustomersDataContext)
            _CustomersDB = value
        End Set
    End Property

    Public Property Customers As Table(Of Customer)
        Get
            Return _Customers
        End Get
        Set(value As Table(Of Customer))
            _Customers = value
        End Set
    End Property

    Public Property CurrentOrder As Order
        Get
            Return _CurrentOrder
        End Get
        Set(value As Order)
            _CurrentOrder = value
        End Set
    End Property

    Public Property OrderInfoDB As OrderItemsDataContext
        Get
            Return _OrderInfoDB
        End Get
        Set(value As OrderItemsDataContext)
            _OrderInfoDB = value
        End Set
    End Property

    'Public Property OrderItems As Table(Of OrderItem)
    '    Get
    '        Return _OrderItems
    '    End Get
    '    Set(value As Table(Of OrderItem))
    '        _OrderItems = value
    '    End Set
    'End Property
    'Public Property Orders As Table(Of Order)
    '    Get
    '        Return _Orders
    '    End Get
    '    Set(value As Table(Of Order))
    '        _Orders = value
    '    End Set
    'End Property

    Public Property OrderStatusDB As OrderStatusDataContext
        Get
            Return _OrderStatusDB
        End Get
        Set(value As OrderStatusDataContext)
            _OrderStatusDB = value
        End Set
    End Property
    Public Property InventoryDB As InventoryDataContext
        Get
            Return _InventoryDB
        End Get
        Set(value As InventoryDataContext)
            _InventoryDB = value
        End Set
    End Property

    'Public Property Inventory As Table(Of Inventory)
    '    Get
    '        Return _Inventory
    '    End Get
    '    Set(value As Table(Of Inventory))
    '        _Inventory = value
    '    End Set
    'End Property
    Public Property OrderStatus As Table(Of OrderStatus)
        Get
            Return _OrderStatus
        End Get
        Set(value As Table(Of OrderStatus))
            _OrderStatus = value
        End Set
    End Property

    Public Property CurrentOrderItem As OrderItemDetails
        Get
            Return _CurrentOrderItem
        End Get
        Set(value As OrderItemDetails)
            _CurrentOrderItem = value
        End Set
    End Property

    Public Property CurrentCustomer As SeedGeneral.Customer
        Get
            Return _CurrentCustomer
        End Get
        Set(value As SeedGeneral.Customer)
            _CurrentCustomer = value
        End Set
    End Property

    Public Property CustomerPriceList As String
        Get
            Return _CustomerPriceList
        End Get
        Set(value As String)
            _CustomerPriceList = value
        End Set
    End Property
    Public Property ItemID As Integer
        Get
            Return _ItemID
        End Get
        Set(value As Integer)
            _ItemID = value
        End Set
    End Property
    Public Property CurrentOrderUnit As OrderUnit
        Get
            Return _CurrentOrderUnit
        End Get
        Set(value As OrderUnit)
            _CurrentOrderUnit = value
        End Set
    End Property
End Class

