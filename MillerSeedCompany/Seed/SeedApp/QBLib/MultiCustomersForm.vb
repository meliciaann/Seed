Imports QBFC13Lib
Imports System.Data.Linq
Imports QBLib.QBLibrary
Imports System.Windows.Forms


Public Class MultiCustomersForm
    Public MyCustomerRet As ICustomerRet
    Public Property MultiCustomerRet As ICustomerRet
        Get
            Return MyCustomerRet
        End Get
        Set(value As ICustomerRet)

        End Set
    End Property

    Public Sub New(ByVal CustomerRetList As ICustomerRetList)
        ' This call is required by the designer.
        InitializeComponent()
        PopulateCustomerList(CustomerRetList)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub PopulateCustomerList(ByVal CustomerRetList As ICustomerRetList)
        Dim QBCustomerList = New List(Of QBCustomers)
        For i = 0 To CustomerRetList.Count - 1
            Dim Cust As New QBCustomers
            Dim customerRet As ICustomerRet = CustomerRetList.GetAt(i)
            If (customerRet IsNot Nothing) Then
                If (customerRet.CompanyName IsNot Nothing) Then
                    Cust.CustomerName = customerRet.CompanyName.GetValue()
                End If
                If (customerRet.BillAddress IsNot Nothing) Then
                    If (customerRet.BillAddress.Addr1 IsNot Nothing) Then
                        Cust.CustomerAddress1 = customerRet.BillAddress.Addr1.GetValue()
                    End If
                    If (customerRet.BillAddress.Addr2 IsNot Nothing) Then
                        Cust.CustomerAddress2 = customerRet.BillAddress.Addr2.GetValue()
                    End If
                    If (customerRet.BillAddress.City IsNot Nothing) Then
                        Cust.CustomerCity = customerRet.BillAddress.City.GetValue()
                    End If
                    If (customerRet.BillAddress.State IsNot Nothing) Then
                        Cust.CustomerState = customerRet.BillAddress.State.GetValue()
                    End If
                    If (customerRet.BillAddress.PostalCode IsNot Nothing) Then
                        Cust.CustomerZip = customerRet.BillAddress.PostalCode.GetValue()
                    End If

                End If
                If (customerRet.ListID IsNot Nothing) Then
                    Cust.QBListID = customerRet.ListID.GetValue()
                End If

                QBCustomerList.Add(Cust)
            End If

        Next

        CustomerListGV.DataSource = QBCustomerList
        CustomerListGVLabelHeaders()

    End Sub
    Public Sub CustomerListGVLabelHeaders()
        For Each col As DataGridViewColumn In CustomerListGV.Columns
            Select Case col.Name
                Case "mCustomerName"
                    col.HeaderText = "Customer Name"

                Case "mCustomerAddress1"
                    col.HeaderText = "Address"

                Case "mCustomerAddress2"
                    col.HeaderText = "Address 2"
                Case "mCustomerCity"
                    col.HeaderText = "City"
                Case "mCustomerState"
                    col.HeaderText = "State"

                Case "mCustomerZip"
                    col.HeaderText = "Zip"

                Case Else
                    col.Visible = False
            End Select
        Next
    End Sub
    Private Sub DoubleClickItemGrid(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles CustomerListGV.CellMouseDoubleClick
        Dim selectedRow As DataGridViewRow = CustomerListGV.CurrentRow
        Dim selectedCust As QBCustomers = selectedRow.DataBoundItem
        Dim qb1 As New QBLib.QBLibrary
        MyCustomerRet = qb1.DoCustomerQuery(selectedCust.mQBListID, "ListIDList")
        Me.Close()

    End Sub
End Class