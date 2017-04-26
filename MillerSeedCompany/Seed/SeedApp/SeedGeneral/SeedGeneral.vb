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
Imports QBFC13Lib
Imports System.IO
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports Microsoft.Reporting.WebForms

Public Class OrderItemDetails
    Public Property Lot As String
    Public Property PLS As Decimal?
    Public Property Item As String
    Public Property PLSLBSPerAcre As Decimal?
    Public Property PricePerPLSLB As Decimal?
    Public Property DistributorPricePerAcre As Decimal?
    Public Property WholesalePricePerAcre As Decimal?
    Public Property RetailPricePerAcre As Decimal?
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

    Public Property InventoryID As Integer

    Public Property AvailableInventory As Decimal?

    Public Property ItemOrder As System.Nullable(Of Integer)

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

    Public Property MixName As String

    Public Property ControlNumber As String

    Public Property LineItemSKU As String

    Public Property IsMix As Boolean
    Public Property LineItemDesc As String

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

    Public Property AvailableInventory As Decimal?

    Public Property OrderId As Integer

    Public Property ItemGroupID As Integer

End Class

Public Class MixBagTag
    Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
    Private _NetWeightID As Integer
    Private _NetWeightDescription As String
    Private _UserChecked As Boolean
    Private _Lot As String
    Private _Kind As String
    Private _Item As String
    Private _Purity As Decimal
    Private _Germ As Decimal
    Private _Inert As Decimal
    Private _WeedSeed As Decimal
    Private _Origin As String
    Private _MyRow As Integer
    Private _MyCol As Integer
    Private _MyPage As Integer


    Public Property NetWeightId As Integer
        Get
            Return _NetWeightID
        End Get
        Set(value As Integer)
            _NetWeightID = value
        End Set
    End Property

    Public Property NetWeightDescription As String
        Get
            Return _NetWeightDescription
        End Get
        Set(value As String)
            _NetWeightDescription = value
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

    Public Sub New(ByVal NetweightID As Integer, ByVal NetWeightDescription As String, ByVal UserChecked As Boolean, Lot As String, Kind As String, ByVal Item As String, ByVal Purity As Decimal, ByVal Germ As Decimal, ByVal Inert As Decimal, ByVal WeedSeed As Decimal, ByVal Origin As String, ByVal MyRow As Integer, MyCol As Integer, MyPage As Integer)
        _NetWeightID = NetweightID
        _NetWeightDescription = NetWeightDescription
        _UserChecked = UserChecked
        _Kind = Kind
        _Lot = Lot
        _Item = Item
        _Purity = Purity
        _Germ = Germ
        _Inert = Inert
        _WeedSeed = WeedSeed
        _Origin = Origin
        _MyRow = MyRow
        _MyCol = MyCol
        _MyPage = MyPage
    End Sub

    Public Property Kind As String
        Get
            Return _Kind
        End Get
        Set(value As String)
            _Kind = value
        End Set
    End Property

    Public Property Lot As String
        Get
            Return _Lot
        End Get
        Set(value As String)
            _Lot = value
        End Set
    End Property
    Public Property Item As String
        Get
            Return _Item
        End Get
        Set(value As String)
            _Item = value
        End Set
    End Property
    Public Property Purity As Decimal
        Get
            Return _Purity
        End Get
        Set(value As Decimal)
            _Purity = value
        End Set
    End Property
    Public Property Germ As Decimal
        Get
            Return _Germ
        End Get
        Set(value As Decimal)
            _Germ = value
        End Set
    End Property
    Public Property Inert As Decimal
        Get
            Return _Inert
        End Get
        Set(value As Decimal)
            _Inert = value
        End Set
    End Property
    Public Property WeedSeed As Decimal
        Get
            Return _WeedSeed
        End Get
        Set(value As Decimal)
            _WeedSeed = value
        End Set
    End Property
    Public Property Origin As String
        Get
            Return _Origin
        End Get
        Set(value As String)
            _Origin = value
        End Set
    End Property
    Public Property MyRow As Integer
        Get
            Return _MyRow
        End Get
        Set(value As Integer)
            _MyRow = value
        End Set
    End Property

    Public Property MyCol As Integer
        Get
            Return _MyCol
        End Get
        Set(value As Integer)
            _MyCol = value

        End Set
    End Property
    Public Property MyPage As Integer
        Get
            Return _MyPage
        End Get
        Set(value As Integer)
            _MyPage = value
        End Set
    End Property

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

    Private _AllowMultiple As Boolean


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

    Public Property AllowMultiple As Boolean
        Get
            Return _AllowMultiple
        End Get
        Set(value As Boolean)
            _AllowMultiple = value
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

    Public Sub New(ByVal ReportFileName As String, ByVal ReportID As Long, ByVal FriendlyName As String, ByVal SortOrder As Integer, ByVal UserChecked As Boolean, ByVal IsVisible As Boolean, ByVal AllowMultiple As Boolean)
        _ReportFileName = ReportFileName
        _ReportID = ReportID
        _FriendlyName = FriendlyName
        _SortOrder = SortOrder
        _UserChecked = UserChecked
        _IsVisible = IsVisible
        _AllowMultiple = AllowMultiple
    End Sub
End Class
Public Class ItemTypes
    Public Property ItemType As String
End Class
Public Class NetWeightPages
    Public _NetWeightID As Integer
    Public _MyPage As Integer

    Public Property NetWeightID As Integer
        Get
            Return _NetWeightID
        End Get
        Set(value As Integer)
            _NetWeightID = value
        End Set
    End Property
    Public Property MyPage As Integer
        Get
            Return _MyPage
        End Get
        Set(value As Integer)
            _MyPage = value
        End Set
    End Property
    Public Sub New(ByVal NetWeightID As Integer, ByVal MyPage As Integer)
        _NetWeightID = NetWeightID
        _MyPage = MyPage
    End Sub

End Class

Public Class LabelTable
    Public _MyColumn As Integer
    Public _MyRow As Integer



    Public Property MyColumn As Integer
        Get
            Return _MyColumn
        End Get
        Set(value As Integer)
            _MyColumn = value
        End Set
    End Property
    Public Property MyRow As Integer
        Get
            Return _MyRow
        End Get
        Set(value As Integer)
            _MyRow = value
        End Set
    End Property


    Public Sub New(ByVal MyColumn As Integer, ByVal MyRow As Integer)
        _MyColumn = MyColumn
        _MyRow = MyRow


    End Sub
End Class


Public Class GetDataClass
    Private _ItemsDB As ItemsEditDataContext
    Private _Items As Table(Of Item)
    Private _NetWeightDB As NetWeightDataContext
    Private _NetWeights As Table(Of NetWeight)
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
    Public _CurrentOrderItems As Table(Of OrderItemDetails)
    Public _CurrentCustomer As SeedGeneral.Customer
    Public _CustomerPriceList As String
    Public _ItemID As Integer
    Public _CurrentOrderUnit As OrderUnit
    Private _InvoiceLineItemsDB As InvoiceLineItemsDataContext
    Private _InvoiceLineItems As Table(Of InvoiceLineItem)
    Private _CurrentItem As Item
    Private _MixBagTags As List(Of MixBagTag)
    Private _BagTagMixLabelTable As List(Of LabelTable)
    Private _NetWeightPagesTable As List(Of NetWeightPages)
    Private _BagTagAcres As BindingList(Of BagTagAcre)
    Private _UserReportsGridReport As String

    Public Property UserReportsGridReport As String
        Get
            Return _UserReportsGridReport
        End Get
        Set(value As String)
            _UserReportsGridReport = value
        End Set
    End Property

    Public Property BagTagMixLabelTable As List(Of LabelTable)
        Get
            Return _BagTagMixLabelTable
        End Get
        Set(value As List(Of LabelTable))
            _BagTagMixLabelTable = value
        End Set
    End Property
    Public Property NetWeightPagesTable As List(Of NetWeightPages)
        Get
            Return _NetWeightPagesTable
        End Get
        Set(value As List(Of NetWeightPages))
            _NetWeightPagesTable = value
        End Set
    End Property

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
    Public Property NetWeightDB As NetWeightDataContext
        Get
            Return _NetWeightDB
        End Get
        Set(value As NetWeightDataContext)
            _NetWeightDB = value
        End Set
    End Property


    Public Property Netweights As Table(Of NetWeight)
        Get
            Return _NetWeights
        End Get
        Set(value As Table(Of NetWeight))
            _NetWeights = value
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
    Public Property CurrentOrderItems As Table(Of OrderItemDetails)
        Get
            Return _CurrentOrderItems
        End Get
        Set(value As Table(Of OrderItemDetails))
            _CurrentOrderItems = value
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
    Public Property InvoiceLineItemsDB As InvoiceLineItemsDataContext
        Get
            Return _InvoiceLineItemsDB
        End Get
        Set(value As InvoiceLineItemsDataContext)
            _InvoiceLineItemsDB = value
        End Set
    End Property

    Public Property InvoiceLineItems As Table(Of InvoiceLineItem)
        Get
            Return _InvoiceLineItems
        End Get
        Set(value As Table(Of InvoiceLineItem))
            _InvoiceLineItems = value
        End Set
    End Property

    Public Property CurrentItem As Item
        Get
            Return _CurrentItem
        End Get
        Set(value As Item)
            _CurrentItem = value
        End Set
    End Property
    Public Property MixBagTags As List(Of MixBagTag)
        Get
            Return _MixBagTags
        End Get
        Set(value As List(Of MixBagTag))
            _MixBagTags = value
        End Set
    End Property

    Public Property BagTagAcres As BindingList(Of BagTagAcre)
        Get
            Return _BagTagAcres
        End Get
        Set(value As BindingList(Of BagTagAcre))
            _BagTagAcres = value
        End Set
    End Property
End Class

Public Class BagTagAcre
    Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged

    Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
    Private _Orderid As Integer
    Private _Acres As Decimal
    Private _Bags As Integer
    Private _TotalAcres As Decimal


    Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging

    Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Public Property OrderID As Integer
        Get
            Return _Orderid
        End Get
        Set(value As Integer)
            _Orderid = value
        End Set
    End Property

    Public Property Acres As Decimal
        Get
            Return _Acres
        End Get
        Set(value As Decimal)
            _Acres = value
        End Set
    End Property
    Public Property TotalAcres As Decimal
        Get
            Return _TotalAcres
        End Get
        Set(value As Decimal)
            _TotalAcres = value
        End Set
    End Property
    Public Property Bags As Integer
        Get
            Return _Bags
        End Get
        Set(value As Integer)
            _Bags = value
        End Set
    End Property

    Public Sub New(ByVal OrderID As Integer, ByVal Acres As Decimal, ByVal TotalAcres As Decimal, Bags As Integer)
        _Orderid = OrderID
        _Acres = Acres
        _TotalAcres = TotalAcres
        _Bags = Bags
    End Sub
End Class


