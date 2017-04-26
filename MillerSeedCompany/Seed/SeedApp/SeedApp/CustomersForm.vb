Imports System.Data.Linq
Imports System.Windows.Forms
Imports SeedGeneral
Imports SeedApp.ItemsForm
Imports QBLib.QBLibrary
Imports Microsoft.Reporting.WinForms

Public Class CustomersForm
    Public CustomerID As Integer
    Public d1 As GetDataClass
    'Private CustomersDB As New CustomersDataContext
    Public Sub New(ByRef d As GetDataClass)
        ' This call is required by the designer.
        InitializeComponent()
        d1 = d
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim AddQBCustomer As Boolean = AddCustomer(d1)
        If (AddQBCustomer) Then
            Me.DialogResult = DialogResult.Yes
        Else
            Me.DialogResult = DialogResult.No
        End If

        Me.Close()
    End Sub

    Public Function AddCustomer(ByRef d As GetDataClass) As Boolean
        Dim cust As New Customer
        Dim CreateinQB As Boolean = False
        cust.CustomerName = CustomerNameTB.Text
        cust.CustomerAddressLine1 = CustomerAddressLine1TB.Text
        cust.CustomerAddressLine2 = CustomerAddressLine2TB.Text
        cust.CustomerCity = CustomerCityTB.Text
        cust.CustomerState = CustomerStateTB.Text
        cust.CustomerZip = CustomerZipTB.Text
        cust.CustomerPhone = CustomerPhoneTB.Text
        cust.Email = CustomerEmailTB.Text

        'd.CustomersDB = New CustomersDataContext
        d.CustomersDB.Customers.InsertOnSubmit(cust)
        d.CustomersDB.SubmitChanges()

        d.CurrentCustomer = cust
        If (AddtoQBCB.Checked) Then
            CreateinQB = True
        End If
        Return CreateinQB
    End Function

End Class