﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection


<Global.System.Data.Linq.Mapping.DatabaseAttribute(Name:="Seed")>  _
Partial Public Class InventoryDataContext
	Inherits System.Data.Linq.DataContext
	
	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()
	
  #Region "Extensibility Method Definitions"
  Partial Private Sub OnCreated()
  End Sub
  Partial Private Sub InsertInventoryItemDetail(instance As InventoryItemDetail)
    End Sub
  Partial Private Sub UpdateInventoryItemDetail(instance As InventoryItemDetail)
    End Sub
  Partial Private Sub DeleteInventoryItemDetail(instance As InventoryItemDetail)
    End Sub
  Partial Private Sub InsertInventoryOrderDetail(instance As InventoryOrderDetail)
    End Sub
  Partial Private Sub UpdateInventoryOrderDetail(instance As InventoryOrderDetail)
    End Sub
  Partial Private Sub DeleteInventoryOrderDetail(instance As InventoryOrderDetail)
    End Sub
  Partial Private Sub InsertInventory(instance As Inventory)
    End Sub
  Partial Private Sub UpdateInventory(instance As Inventory)
    End Sub
  Partial Private Sub DeleteInventory(instance As Inventory)
    End Sub
#End Region
    Public Sub New()
        MyBase.New(My.MySettings.Default.SeedConnectionString, mappingSource)
        OnCreated()
    End Sub
    Public Sub New(ByVal connection As String)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As System.Data.IDbConnection)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As String, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As System.Data.IDbConnection, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public ReadOnly Property InventoryItemDetails() As System.Data.Linq.Table(Of InventoryItemDetail)
		Get
			Return Me.GetTable(Of InventoryItemDetail)
		End Get
	End Property
	
	Public ReadOnly Property InventoryOrderDetails() As System.Data.Linq.Table(Of InventoryOrderDetail)
		Get
			Return Me.GetTable(Of InventoryOrderDetail)
		End Get
	End Property
	
	Public ReadOnly Property Inventories() As System.Data.Linq.Table(Of Inventory)
		Get
			Return Me.GetTable(Of Inventory)
		End Get
	End Property
	
	<Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.InventoryHistoryByItem")>  _
	Public Function InventoryHistoryByItem(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="ItemID", DbType:="Int")> ByVal itemID As System.Nullable(Of Integer)) As ISingleResult(Of InventoryHistoryByItemResult1)
		Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod,MethodInfo), itemID)
		Return CType(result.ReturnValue,ISingleResult(Of InventoryHistoryByItemResult1))
	End Function
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.Items")>  _
Partial Public Class InventoryItemDetail
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _Item As String
	
	Private _Lot As String
	
	Private _PLS_ As System.Nullable(Of Decimal)
	
	Private _variety As String
	
	Private _Purity As System.Nullable(Of Decimal)
	
	Private _Crop As System.Nullable(Of Decimal)
	
	Private _Weeds As System.Nullable(Of Decimal)
	
	Private _Germ As System.Nullable(Of Decimal)
	
	Private _Dormant As System.Nullable(Of Decimal)
	
	Private _Total As System.Nullable(Of Decimal)
	
	Private _Test_Date As System.Nullable(Of Date)
	
	Private _Orgin As String
	
	Private _Distributor As System.Nullable(Of Decimal)
	
	Private _Wholesale As System.Nullable(Of Decimal)
	
	Private _Retail As System.Nullable(Of Decimal)
	
	Private _Item1 As String
	
	Private _Item2 As String
	
	Private _ReorderQTY As System.Nullable(Of Decimal)
	
	Private _Reorder As System.Nullable(Of Boolean)
	
	Private _Discontinued As System.Nullable(Of Boolean)
	
	Private _ScientificName As String
	
	Private _ItemID As Integer
	
	Private _Type As String
	
	Private _Inventory As EntityRef(Of Inventory)
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnItemChanging(value As String)
    End Sub
    Partial Private Sub OnItemChanged()
    End Sub
    Partial Private Sub OnLotChanging(value As String)
    End Sub
    Partial Private Sub OnLotChanged()
    End Sub
    Partial Private Sub OnPLS_Changing(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnPLS_Changed()
    End Sub
    Partial Private Sub OnvarietyChanging(value As String)
    End Sub
    Partial Private Sub OnvarietyChanged()
    End Sub
    Partial Private Sub OnPurityChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnPurityChanged()
    End Sub
    Partial Private Sub OnCropChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnCropChanged()
    End Sub
    Partial Private Sub OnWeedsChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnWeedsChanged()
    End Sub
    Partial Private Sub OnGermChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnGermChanged()
    End Sub
    Partial Private Sub OnDormantChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnDormantChanged()
    End Sub
    Partial Private Sub OnTotalChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnTotalChanged()
    End Sub
    Partial Private Sub OnTest_DateChanging(value As System.Nullable(Of Date))
    End Sub
    Partial Private Sub OnTest_DateChanged()
    End Sub
    Partial Private Sub OnOrginChanging(value As String)
    End Sub
    Partial Private Sub OnOrginChanged()
    End Sub
    Partial Private Sub OnDistributorChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnDistributorChanged()
    End Sub
    Partial Private Sub OnWholesaleChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnWholesaleChanged()
    End Sub
    Partial Private Sub OnRetailChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnRetailChanged()
    End Sub
    Partial Private Sub OnItem1Changing(value As String)
    End Sub
    Partial Private Sub OnItem1Changed()
    End Sub
    Partial Private Sub OnItem2Changing(value As String)
    End Sub
    Partial Private Sub OnItem2Changed()
    End Sub
    Partial Private Sub OnReorderQTYChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnReorderQTYChanged()
    End Sub
    Partial Private Sub OnReorderChanging(value As System.Nullable(Of Boolean))
    End Sub
    Partial Private Sub OnReorderChanged()
    End Sub
    Partial Private Sub OnDiscontinuedChanging(value As System.Nullable(Of Boolean))
    End Sub
    Partial Private Sub OnDiscontinuedChanged()
    End Sub
    Partial Private Sub OnScientificNameChanging(value As String)
    End Sub
    Partial Private Sub OnScientificNameChanged()
    End Sub
    Partial Private Sub OnItemIDChanging(value As Integer)
    End Sub
    Partial Private Sub OnItemIDChanged()
    End Sub
    Partial Private Sub OnTypeChanging(value As String)
    End Sub
    Partial Private Sub OnTypeChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		Me._Inventory = CType(Nothing, EntityRef(Of Inventory))
		OnCreated
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Item", DbType:="NVarChar(100)")>  _
	Public Property Item() As String
		Get
			Return Me._Item
		End Get
		Set
			If (String.Equals(Me._Item, value) = false) Then
				Me.OnItemChanging(value)
				Me.SendPropertyChanging
				Me._Item = value
				Me.SendPropertyChanged("Item")
				Me.OnItemChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Lot", DbType:="NVarChar(100)")>  _
	Public Property Lot() As String
		Get
			Return Me._Lot
		End Get
		Set
			If (String.Equals(Me._Lot, value) = false) Then
				Me.OnLotChanging(value)
				Me.SendPropertyChanging
				Me._Lot = value
				Me.SendPropertyChanged("Lot")
				Me.OnLotChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[PLS%]", Storage:="_PLS_", DbType:="Decimal(20,10)")>  _
	Public Property PLS_() As System.Nullable(Of Decimal)
		Get
			Return Me._PLS_
		End Get
		Set
			If (Me._PLS_.Equals(value) = false) Then
				Me.OnPLS_Changing(value)
				Me.SendPropertyChanging
				Me._PLS_ = value
				Me.SendPropertyChanged("PLS_")
				Me.OnPLS_Changed
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_variety", DbType:="NVarChar(100)")>  _
	Public Property variety() As String
		Get
			Return Me._variety
		End Get
		Set
			If (String.Equals(Me._variety, value) = false) Then
				Me.OnvarietyChanging(value)
				Me.SendPropertyChanging
				Me._variety = value
				Me.SendPropertyChanged("variety")
				Me.OnvarietyChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Purity", DbType:="Decimal(20,10)")>  _
	Public Property Purity() As System.Nullable(Of Decimal)
		Get
			Return Me._Purity
		End Get
		Set
			If (Me._Purity.Equals(value) = false) Then
				Me.OnPurityChanging(value)
				Me.SendPropertyChanging
				Me._Purity = value
				Me.SendPropertyChanged("Purity")
				Me.OnPurityChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Crop", DbType:="Decimal(20,10)")>  _
	Public Property Crop() As System.Nullable(Of Decimal)
		Get
			Return Me._Crop
		End Get
		Set
			If (Me._Crop.Equals(value) = false) Then
				Me.OnCropChanging(value)
				Me.SendPropertyChanging
				Me._Crop = value
				Me.SendPropertyChanged("Crop")
				Me.OnCropChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Weeds", DbType:="Decimal(20,10)")>  _
	Public Property Weeds() As System.Nullable(Of Decimal)
		Get
			Return Me._Weeds
		End Get
		Set
			If (Me._Weeds.Equals(value) = false) Then
				Me.OnWeedsChanging(value)
				Me.SendPropertyChanging
				Me._Weeds = value
				Me.SendPropertyChanged("Weeds")
				Me.OnWeedsChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Germ", DbType:="Decimal(20,10)")>  _
	Public Property Germ() As System.Nullable(Of Decimal)
		Get
			Return Me._Germ
		End Get
		Set
			If (Me._Germ.Equals(value) = false) Then
				Me.OnGermChanging(value)
				Me.SendPropertyChanging
				Me._Germ = value
				Me.SendPropertyChanged("Germ")
				Me.OnGermChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Dormant", DbType:="Decimal(20,10)")>  _
	Public Property Dormant() As System.Nullable(Of Decimal)
		Get
			Return Me._Dormant
		End Get
		Set
			If (Me._Dormant.Equals(value) = false) Then
				Me.OnDormantChanging(value)
				Me.SendPropertyChanging
				Me._Dormant = value
				Me.SendPropertyChanged("Dormant")
				Me.OnDormantChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Total", DbType:="Decimal(20,10)")>  _
	Public Property Total() As System.Nullable(Of Decimal)
		Get
			Return Me._Total
		End Get
		Set
			If (Me._Total.Equals(value) = false) Then
				Me.OnTotalChanging(value)
				Me.SendPropertyChanging
				Me._Total = value
				Me.SendPropertyChanged("Total")
				Me.OnTotalChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Name:="[Test Date]", Storage:="_Test_Date", DbType:="DateTime")>  _
	Public Property Test_Date() As System.Nullable(Of Date)
		Get
			Return Me._Test_Date
		End Get
		Set
			If (Me._Test_Date.Equals(value) = false) Then
				Me.OnTest_DateChanging(value)
				Me.SendPropertyChanging
				Me._Test_Date = value
				Me.SendPropertyChanged("Test_Date")
				Me.OnTest_DateChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Orgin", DbType:="NVarChar(100)")>  _
	Public Property Orgin() As String
		Get
			Return Me._Orgin
		End Get
		Set
			If (String.Equals(Me._Orgin, value) = false) Then
				Me.OnOrginChanging(value)
				Me.SendPropertyChanging
				Me._Orgin = value
				Me.SendPropertyChanged("Orgin")
				Me.OnOrginChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Distributor", DbType:="Decimal(12,4)")>  _
	Public Property Distributor() As System.Nullable(Of Decimal)
		Get
			Return Me._Distributor
		End Get
		Set
			If (Me._Distributor.Equals(value) = false) Then
				Me.OnDistributorChanging(value)
				Me.SendPropertyChanging
				Me._Distributor = value
				Me.SendPropertyChanged("Distributor")
				Me.OnDistributorChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Wholesale", DbType:="Decimal(12,4)")>  _
	Public Property Wholesale() As System.Nullable(Of Decimal)
		Get
			Return Me._Wholesale
		End Get
		Set
			If (Me._Wholesale.Equals(value) = false) Then
				Me.OnWholesaleChanging(value)
				Me.SendPropertyChanging
				Me._Wholesale = value
				Me.SendPropertyChanged("Wholesale")
				Me.OnWholesaleChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Retail", DbType:="Decimal(12,4)")>  _
	Public Property Retail() As System.Nullable(Of Decimal)
		Get
			Return Me._Retail
		End Get
		Set
			If (Me._Retail.Equals(value) = false) Then
				Me.OnRetailChanging(value)
				Me.SendPropertyChanging
				Me._Retail = value
				Me.SendPropertyChanged("Retail")
				Me.OnRetailChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Item1", DbType:="NVarChar(100)")>  _
	Public Property Item1() As String
		Get
			Return Me._Item1
		End Get
		Set
			If (String.Equals(Me._Item1, value) = false) Then
				Me.OnItem1Changing(value)
				Me.SendPropertyChanging
				Me._Item1 = value
				Me.SendPropertyChanged("Item1")
				Me.OnItem1Changed
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Item2", DbType:="NVarChar(100)")>  _
	Public Property Item2() As String
		Get
			Return Me._Item2
		End Get
		Set
			If (String.Equals(Me._Item2, value) = false) Then
				Me.OnItem2Changing(value)
				Me.SendPropertyChanging
				Me._Item2 = value
				Me.SendPropertyChanged("Item2")
				Me.OnItem2Changed
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ReorderQTY", DbType:="Decimal(12,4)")>  _
	Public Property ReorderQTY() As System.Nullable(Of Decimal)
		Get
			Return Me._ReorderQTY
		End Get
		Set
			If (Me._ReorderQTY.Equals(value) = false) Then
				Me.OnReorderQTYChanging(value)
				Me.SendPropertyChanging
				Me._ReorderQTY = value
				Me.SendPropertyChanged("ReorderQTY")
				Me.OnReorderQTYChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Reorder", DbType:="Bit")>  _
	Public Property Reorder() As System.Nullable(Of Boolean)
		Get
			Return Me._Reorder
		End Get
		Set
			If (Me._Reorder.Equals(value) = false) Then
				Me.OnReorderChanging(value)
				Me.SendPropertyChanging
				Me._Reorder = value
				Me.SendPropertyChanged("Reorder")
				Me.OnReorderChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Discontinued", DbType:="Bit")>  _
	Public Property Discontinued() As System.Nullable(Of Boolean)
		Get
			Return Me._Discontinued
		End Get
		Set
			If (Me._Discontinued.Equals(value) = false) Then
				Me.OnDiscontinuedChanging(value)
				Me.SendPropertyChanging
				Me._Discontinued = value
				Me.SendPropertyChanged("Discontinued")
				Me.OnDiscontinuedChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ScientificName", DbType:="VarChar(255)")>  _
	Public Property ScientificName() As String
		Get
			Return Me._ScientificName
		End Get
		Set
			If (String.Equals(Me._ScientificName, value) = false) Then
				Me.OnScientificNameChanging(value)
				Me.SendPropertyChanging
				Me._ScientificName = value
				Me.SendPropertyChanged("ScientificName")
				Me.OnScientificNameChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ItemID", AutoSync:=AutoSync.OnInsert, DbType:="Int NOT NULL IDENTITY", IsPrimaryKey:=true, IsDbGenerated:=true)>  _
	Public Property ItemID() As Integer
		Get
			Return Me._ItemID
		End Get
		Set
			If ((Me._ItemID = value)  _
						= false) Then
				If Me._Inventory.HasLoadedOrAssignedValue Then
					Throw New System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException()
				End If
				Me.OnItemIDChanging(value)
				Me.SendPropertyChanging
				Me._ItemID = value
				Me.SendPropertyChanged("ItemID")
				Me.OnItemIDChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Type", DbType:="NVarChar(50)")>  _
	Public Property Type() As String
		Get
			Return Me._Type
		End Get
		Set
			If (String.Equals(Me._Type, value) = false) Then
				Me.OnTypeChanging(value)
				Me.SendPropertyChanging
				Me._Type = value
				Me.SendPropertyChanged("Type")
				Me.OnTypeChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Inventory_InventoryItemDetail", Storage:="_Inventory", ThisKey:="ItemID", OtherKey:="ItemID", IsForeignKey:=true)>  _
	Public Property Inventory() As Inventory
		Get
			Return Me._Inventory.Entity
		End Get
		Set
			Dim previousValue As Inventory = Me._Inventory.Entity
			If ((Object.Equals(previousValue, value) = false)  _
						OrElse (Me._Inventory.HasLoadedOrAssignedValue = false)) Then
				Me.SendPropertyChanging
				If ((previousValue Is Nothing)  _
							= false) Then
					Me._Inventory.Entity = Nothing
					previousValue.InventoryItemDetails.Remove(Me)
				End If
				Me._Inventory.Entity = value
				If ((value Is Nothing)  _
							= false) Then
					value.InventoryItemDetails.Add(Me)
					Me._ItemID = value.ItemID
				Else
					Me._ItemID = CType(Nothing, Integer)
				End If
				Me.SendPropertyChanged("Inventory")
			End If
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.Orders")>  _
Partial Public Class InventoryOrderDetail
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _OrderID As Integer
	
	Private _InvoiceID As String
	
	Private _CustomerID As Integer
	
	Private _Project As String
	
	Private _Acres As System.Nullable(Of Decimal)
	
	Private _OrderTotal As System.Nullable(Of Decimal)
	
	Private _OrderDate As System.Nullable(Of Date)
	
	Private _SubmittedBy As String
	
	Private _TotalPricePerAcre As System.Nullable(Of Decimal)
	
	Private _OrderStatusId As System.Nullable(Of Long)
	
	Private _Inventory As EntityRef(Of Inventory)
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnOrderIDChanging(value As Integer)
    End Sub
    Partial Private Sub OnOrderIDChanged()
    End Sub
    Partial Private Sub OnInvoiceIDChanging(value As String)
    End Sub
    Partial Private Sub OnInvoiceIDChanged()
    End Sub
    Partial Private Sub OnCustomerIDChanging(value As Integer)
    End Sub
    Partial Private Sub OnCustomerIDChanged()
    End Sub
    Partial Private Sub OnProjectChanging(value As String)
    End Sub
    Partial Private Sub OnProjectChanged()
    End Sub
    Partial Private Sub OnAcresChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnAcresChanged()
    End Sub
    Partial Private Sub OnOrderTotalChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnOrderTotalChanged()
    End Sub
    Partial Private Sub OnOrderDateChanging(value As System.Nullable(Of Date))
    End Sub
    Partial Private Sub OnOrderDateChanged()
    End Sub
    Partial Private Sub OnSubmittedByChanging(value As String)
    End Sub
    Partial Private Sub OnSubmittedByChanged()
    End Sub
    Partial Private Sub OnTotalPricePerAcreChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnTotalPricePerAcreChanged()
    End Sub
    Partial Private Sub OnOrderStatusIdChanging(value As System.Nullable(Of Long))
    End Sub
    Partial Private Sub OnOrderStatusIdChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		Me._Inventory = CType(Nothing, EntityRef(Of Inventory))
		OnCreated
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_OrderID", AutoSync:=AutoSync.OnInsert, DbType:="Int NOT NULL IDENTITY", IsPrimaryKey:=true, IsDbGenerated:=true)>  _
	Public Property OrderID() As Integer
		Get
			Return Me._OrderID
		End Get
		Set
			If ((Me._OrderID = value)  _
						= false) Then
				Me.OnOrderIDChanging(value)
				Me.SendPropertyChanging
				Me._OrderID = value
				Me.SendPropertyChanged("OrderID")
				Me.OnOrderIDChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_InvoiceID", DbType:="NVarChar(50)")>  _
	Public Property InvoiceID() As String
		Get
			Return Me._InvoiceID
		End Get
		Set
			If (String.Equals(Me._InvoiceID, value) = false) Then
				If Me._Inventory.HasLoadedOrAssignedValue Then
					Throw New System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException()
				End If
				Me.OnInvoiceIDChanging(value)
				Me.SendPropertyChanging
				Me._InvoiceID = value
				Me.SendPropertyChanged("InvoiceID")
				Me.OnInvoiceIDChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CustomerID", DbType:="Int NOT NULL")>  _
	Public Property CustomerID() As Integer
		Get
			Return Me._CustomerID
		End Get
		Set
			If ((Me._CustomerID = value)  _
						= false) Then
				Me.OnCustomerIDChanging(value)
				Me.SendPropertyChanging
				Me._CustomerID = value
				Me.SendPropertyChanged("CustomerID")
				Me.OnCustomerIDChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Project", DbType:="NVarChar(100)")>  _
	Public Property Project() As String
		Get
			Return Me._Project
		End Get
		Set
			If (String.Equals(Me._Project, value) = false) Then
				Me.OnProjectChanging(value)
				Me.SendPropertyChanging
				Me._Project = value
				Me.SendPropertyChanged("Project")
				Me.OnProjectChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Acres", DbType:="Decimal(12,2)")>  _
	Public Property Acres() As System.Nullable(Of Decimal)
		Get
			Return Me._Acres
		End Get
		Set
			If (Me._Acres.Equals(value) = false) Then
				Me.OnAcresChanging(value)
				Me.SendPropertyChanging
				Me._Acres = value
				Me.SendPropertyChanged("Acres")
				Me.OnAcresChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_OrderTotal", DbType:="Decimal(12,4)")>  _
	Public Property OrderTotal() As System.Nullable(Of Decimal)
		Get
			Return Me._OrderTotal
		End Get
		Set
			If (Me._OrderTotal.Equals(value) = false) Then
				Me.OnOrderTotalChanging(value)
				Me.SendPropertyChanging
				Me._OrderTotal = value
				Me.SendPropertyChanged("OrderTotal")
				Me.OnOrderTotalChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_OrderDate", DbType:="DateTime")>  _
	Public Property OrderDate() As System.Nullable(Of Date)
		Get
			Return Me._OrderDate
		End Get
		Set
			If (Me._OrderDate.Equals(value) = false) Then
				Me.OnOrderDateChanging(value)
				Me.SendPropertyChanging
				Me._OrderDate = value
				Me.SendPropertyChanged("OrderDate")
				Me.OnOrderDateChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_SubmittedBy", DbType:="NVarChar(100)")>  _
	Public Property SubmittedBy() As String
		Get
			Return Me._SubmittedBy
		End Get
		Set
			If (String.Equals(Me._SubmittedBy, value) = false) Then
				Me.OnSubmittedByChanging(value)
				Me.SendPropertyChanging
				Me._SubmittedBy = value
				Me.SendPropertyChanged("SubmittedBy")
				Me.OnSubmittedByChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TotalPricePerAcre", DbType:="Decimal(12,4)")>  _
	Public Property TotalPricePerAcre() As System.Nullable(Of Decimal)
		Get
			Return Me._TotalPricePerAcre
		End Get
		Set
			If (Me._TotalPricePerAcre.Equals(value) = false) Then
				Me.OnTotalPricePerAcreChanging(value)
				Me.SendPropertyChanging
				Me._TotalPricePerAcre = value
				Me.SendPropertyChanged("TotalPricePerAcre")
				Me.OnTotalPricePerAcreChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_OrderStatusId", DbType:="BigInt")>  _
	Public Property OrderStatusId() As System.Nullable(Of Long)
		Get
			Return Me._OrderStatusId
		End Get
		Set
			If (Me._OrderStatusId.Equals(value) = false) Then
				Me.OnOrderStatusIdChanging(value)
				Me.SendPropertyChanging
				Me._OrderStatusId = value
				Me.SendPropertyChanged("OrderStatusId")
				Me.OnOrderStatusIdChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Inventory_InventoryOrderDetail", Storage:="_Inventory", ThisKey:="InvoiceID", OtherKey:="InvoiceID", IsForeignKey:=true)>  _
	Public Property Inventory() As Inventory
		Get
			Return Me._Inventory.Entity
		End Get
		Set
			Dim previousValue As Inventory = Me._Inventory.Entity
			If ((Object.Equals(previousValue, value) = false)  _
						OrElse (Me._Inventory.HasLoadedOrAssignedValue = false)) Then
				Me.SendPropertyChanging
				If ((previousValue Is Nothing)  _
							= false) Then
					Me._Inventory.Entity = Nothing
					previousValue.InventoryOrderDetails.Remove(Me)
				End If
				Me._Inventory.Entity = value
				If ((value Is Nothing)  _
							= false) Then
					value.InventoryOrderDetails.Add(Me)
					Me._InvoiceID = value.InvoiceID
				Else
					Me._InvoiceID = CType(Nothing, String)
				End If
				Me.SendPropertyChanged("Inventory")
			End If
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.Inventory")>  _
Partial Public Class Inventory
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _InventoryID As Long
	
	Private _ItemID As Integer
	
	Private _Memo As String
	
	Private _Quantity As System.Nullable(Of Decimal)
	
	Private _InvoiceID As String
	
	Private _InventoryDate As System.Nullable(Of Date)
	
	Private _InventoryItemDetails As EntitySet(Of InventoryItemDetail)
	
	Private _InventoryOrderDetails As EntitySet(Of InventoryOrderDetail)
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnInventoryIDChanging(value As Long)
    End Sub
    Partial Private Sub OnInventoryIDChanged()
    End Sub
    Partial Private Sub OnItemIDChanging(value As Integer)
    End Sub
    Partial Private Sub OnItemIDChanged()
    End Sub
    Partial Private Sub OnMemoChanging(value As String)
    End Sub
    Partial Private Sub OnMemoChanged()
    End Sub
    Partial Private Sub OnQuantityChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnQuantityChanged()
    End Sub
    Partial Private Sub OnInvoiceIDChanging(value As String)
    End Sub
    Partial Private Sub OnInvoiceIDChanged()
    End Sub
    Partial Private Sub OnInventoryDateChanging(value As System.Nullable(Of Date))
    End Sub
    Partial Private Sub OnInventoryDateChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		Me._InventoryItemDetails = New EntitySet(Of InventoryItemDetail)(AddressOf Me.attach_InventoryItemDetails, AddressOf Me.detach_InventoryItemDetails)
		Me._InventoryOrderDetails = New EntitySet(Of InventoryOrderDetail)(AddressOf Me.attach_InventoryOrderDetails, AddressOf Me.detach_InventoryOrderDetails)
		OnCreated
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_InventoryID", AutoSync:=AutoSync.OnInsert, DbType:="BigInt NOT NULL IDENTITY", IsPrimaryKey:=true, IsDbGenerated:=true)>  _
	Public Property InventoryID() As Long
		Get
			Return Me._InventoryID
		End Get
		Set
			If ((Me._InventoryID = value)  _
						= false) Then
				Me.OnInventoryIDChanging(value)
				Me.SendPropertyChanging
				Me._InventoryID = value
				Me.SendPropertyChanged("InventoryID")
				Me.OnInventoryIDChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ItemID", DbType:="Int NOT NULL")>  _
	Public Property ItemID() As Integer
		Get
			Return Me._ItemID
		End Get
		Set
			If ((Me._ItemID = value)  _
						= false) Then
				Me.OnItemIDChanging(value)
				Me.SendPropertyChanging
				Me._ItemID = value
				Me.SendPropertyChanged("ItemID")
				Me.OnItemIDChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Memo", DbType:="NVarChar(MAX)")>  _
	Public Property Memo() As String
		Get
			Return Me._Memo
		End Get
		Set
			If (String.Equals(Me._Memo, value) = false) Then
				Me.OnMemoChanging(value)
				Me.SendPropertyChanging
				Me._Memo = value
				Me.SendPropertyChanged("Memo")
				Me.OnMemoChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Quantity", DbType:="Decimal(12,4)")>  _
	Public Property Quantity() As System.Nullable(Of Decimal)
		Get
			Return Me._Quantity
		End Get
		Set
			If (Me._Quantity.Equals(value) = false) Then
				Me.OnQuantityChanging(value)
				Me.SendPropertyChanging
				Me._Quantity = value
				Me.SendPropertyChanged("Quantity")
				Me.OnQuantityChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_InvoiceID", DbType:="NVarChar(50)")>  _
	Public Property InvoiceID() As String
		Get
			Return Me._InvoiceID
		End Get
		Set
			If (String.Equals(Me._InvoiceID, value) = false) Then
				Me.OnInvoiceIDChanging(value)
				Me.SendPropertyChanging
				Me._InvoiceID = value
				Me.SendPropertyChanged("InvoiceID")
				Me.OnInvoiceIDChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_InventoryDate", DbType:="DateTime")>  _
	Public Property InventoryDate() As System.Nullable(Of Date)
		Get
			Return Me._InventoryDate
		End Get
		Set
			If (Me._InventoryDate.Equals(value) = false) Then
				Me.OnInventoryDateChanging(value)
				Me.SendPropertyChanging
				Me._InventoryDate = value
				Me.SendPropertyChanged("InventoryDate")
				Me.OnInventoryDateChanged
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Inventory_InventoryItemDetail", Storage:="_InventoryItemDetails", ThisKey:="ItemID", OtherKey:="ItemID")>  _
	Public Property InventoryItemDetails() As EntitySet(Of InventoryItemDetail)
		Get
			Return Me._InventoryItemDetails
		End Get
		Set
			Me._InventoryItemDetails.Assign(value)
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Inventory_InventoryOrderDetail", Storage:="_InventoryOrderDetails", ThisKey:="InvoiceID", OtherKey:="InvoiceID")>  _
	Public Property InventoryOrderDetails() As EntitySet(Of InventoryOrderDetail)
		Get
			Return Me._InventoryOrderDetails
		End Get
		Set
			Me._InventoryOrderDetails.Assign(value)
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
	
	Private Sub attach_InventoryItemDetails(ByVal entity As InventoryItemDetail)
		Me.SendPropertyChanging
		entity.Inventory = Me
	End Sub
	
	Private Sub detach_InventoryItemDetails(ByVal entity As InventoryItemDetail)
		Me.SendPropertyChanging
		entity.Inventory = Nothing
	End Sub
	
	Private Sub attach_InventoryOrderDetails(ByVal entity As InventoryOrderDetail)
		Me.SendPropertyChanging
		entity.Inventory = Me
	End Sub
	
	Private Sub detach_InventoryOrderDetails(ByVal entity As InventoryOrderDetail)
		Me.SendPropertyChanging
		entity.Inventory = Nothing
	End Sub
End Class

Partial Public Class InventoryHistoryByItemResult1
	
	Private _inventorydate As System.Nullable(Of Date)
	
	Private _Quantity As System.Nullable(Of Decimal)
	
	Private _Memo As String
	
	Private _Item As String
	
	Private _Lot As String
	
	Private _CustomerName As String
	
	Private _Project As String
	
	Public Sub New()
		MyBase.New
	End Sub
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_inventorydate", DbType:="DateTime")>  _
	Public Property inventorydate() As System.Nullable(Of Date)
		Get
			Return Me._inventorydate
		End Get
		Set
			If (Me._inventorydate.Equals(value) = false) Then
				Me._inventorydate = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Quantity", DbType:="Decimal(12,4)")>  _
	Public Property Quantity() As System.Nullable(Of Decimal)
		Get
			Return Me._Quantity
		End Get
		Set
			If (Me._Quantity.Equals(value) = false) Then
				Me._Quantity = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Memo", DbType:="NVarChar(MAX)")>  _
	Public Property Memo() As String
		Get
			Return Me._Memo
		End Get
		Set
			If (String.Equals(Me._Memo, value) = false) Then
				Me._Memo = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Item", DbType:="NVarChar(100)")>  _
	Public Property Item() As String
		Get
			Return Me._Item
		End Get
		Set
			If (String.Equals(Me._Item, value) = false) Then
				Me._Item = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Lot", DbType:="NVarChar(100)")>  _
	Public Property Lot() As String
		Get
			Return Me._Lot
		End Get
		Set
			If (String.Equals(Me._Lot, value) = false) Then
				Me._Lot = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CustomerName", DbType:="NVarChar(255)")>  _
	Public Property CustomerName() As String
		Get
			Return Me._CustomerName
		End Get
		Set
			If (String.Equals(Me._CustomerName, value) = false) Then
				Me._CustomerName = value
			End If
		End Set
	End Property
	
	<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Project", DbType:="NVarChar(100)")>  _
	Public Property Project() As String
		Get
			Return Me._Project
		End Get
		Set
			If (String.Equals(Me._Project, value) = false) Then
				Me._Project = value
			End If
		End Set
	End Property
End Class