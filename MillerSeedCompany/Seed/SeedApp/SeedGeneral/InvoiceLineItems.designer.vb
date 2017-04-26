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
Partial Public Class InvoiceLineItemsDataContext
	Inherits System.Data.Linq.DataContext
	
	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()
	
  #Region "Extensibility Method Definitions"
  Partial Private Sub OnCreated()
  End Sub
    Partial Private Sub InsertInvoiceLineItem(instance As InvoiceLineItem)
    End Sub
    Partial Private Sub UpdateInvoiceLineItem(instance As InvoiceLineItem)
    End Sub
    Partial Private Sub DeleteInvoiceLineItem(instance As InvoiceLineItem)
    End Sub
#End Region
    Public Sub New()
        MyBase.New(Global.SeedGeneral.My.MySettings.Default.SeedConnectionString, mappingSource)
        OnCreated()
    End Sub
    Public Sub New(ByVal connection As String)
        MyBase.New(connection, mappingSource)
        OnCreated()
    End Sub

    Public Sub New(ByVal connection As System.Data.IDbConnection)
        MyBase.New(connection, mappingSource)
        OnCreated()
    End Sub

    Public Sub New(ByVal connection As String, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
        MyBase.New(connection, mappingSource)
        OnCreated()
    End Sub

    Public Sub New(ByVal connection As System.Data.IDbConnection, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
        MyBase.New(connection, mappingSource)
        OnCreated()
    End Sub

    Public ReadOnly Property InvoiceLineItems() As System.Data.Linq.Table(Of InvoiceLineItem)
        Get
            Return Me.GetTable(Of InvoiceLineItem)
        End Get
    End Property
End Class

<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.InvoiceLineItem")>
Partial Public Class InvoiceLineItem
    Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged

    Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)

    Private _SKU As String

    Private _Description As String

    Private _IsMix As System.Nullable(Of Boolean)

#Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnSKUChanging(value As String)
    End Sub
    Partial Private Sub OnSKUChanged()
    End Sub
    Partial Private Sub OnDescriptionChanging(value As String)
    End Sub
    Partial Private Sub OnDescriptionChanged()
    End Sub
    Partial Private Sub OnIsMixChanging(value As System.Nullable(Of Boolean))
    End Sub
    Partial Private Sub OnIsMixChanged()
    End Sub
#End Region

    Public Sub New()
        MyBase.New
        OnCreated()
    End Sub

    <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_SKU", DbType:="NVarChar(100) NOT NULL", CanBeNull:=False, IsPrimaryKey:=True)>
    Public Property SKU() As String
        Get
            Return Me._SKU
        End Get
        Set
            If (String.Equals(Me._SKU, Value) = False) Then
                Me.OnSKUChanging(Value)
                Me.SendPropertyChanging()
                Me._SKU = Value
                Me.SendPropertyChanged("SKU")
                Me.OnSKUChanged()
            End If
        End Set
    End Property

    <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Description", DbType:="NVarChar(100)")>
    Public Property Description() As String
        Get
            Return Me._Description
        End Get
        Set
            If (String.Equals(Me._Description, Value) = False) Then
                Me.OnDescriptionChanging(Value)
                Me.SendPropertyChanging()
                Me._Description = Value
                Me.SendPropertyChanged("Description")
                Me.OnDescriptionChanged()
            End If
        End Set
    End Property

    <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IsMix", DbType:="Bit")>
    Public Property IsMix() As System.Nullable(Of Boolean)
        Get
            Return Me._IsMix
        End Get
        Set
            If (Me._IsMix.Equals(Value) = False) Then
                Me.OnIsMixChanging(Value)
                Me.SendPropertyChanging()
                Me._IsMix = Value
                Me.SendPropertyChanged("IsMix")
                Me.OnIsMixChanged()
            End If
        End Set
    End Property

    Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging

    Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Protected Overridable Sub SendPropertyChanging()
        If ((Me.PropertyChangingEvent Is Nothing) _
                    = False) Then
            RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
        End If
    End Sub

    Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
        If ((Me.PropertyChangedEvent Is Nothing) _
                    = False) Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End If
    End Sub
End Class