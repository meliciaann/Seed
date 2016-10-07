Imports QBFC13Lib
Imports System.Data.Linq
Imports System
Imports System.Net
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Windows.Forms
Imports SeedGeneral



Public Class QBLibrary
    Public CompanyFile As String = "Miller Seed Company"
    Public QBCustomers As Table(Of QBCustomers)
    Public QBCustomerUserSelection As String = Nothing
    Public MyCustomerRet As ICustomerRet = Nothing

    Public Sub DoCompanyQuery()
        Dim sessionBegun As Boolean
        sessionBegun = False
        Dim connectionOpen As Boolean
        connectionOpen = False
        Dim sessionManager As QBSessionManager
        sessionManager = Nothing

        Try
            'Create the session Manager object
            sessionManager = New QBSessionManager

            'Create the message set request object to hold our request
            Dim requestMsgSet As IMsgSetRequest
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0)
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue

            BuildCompanyQueryRq(requestMsgSet, CompanyFile)

            'Connect to QuickBooks and begin a session
            sessionManager.OpenConnection("", "Sample Code from OSR")
            connectionOpen = True
            sessionManager.BeginSession("", ENOpenMode.omDontCare)
            sessionBegun = True

            'Send the request and get the response from QuickBooks
            Dim responseMsgSet As IMsgSetResponse
            responseMsgSet = sessionManager.DoRequests(requestMsgSet)

            'End the session and close the connection to QuickBooks
            sessionManager.EndSession()
            sessionBegun = False
            sessionManager.CloseConnection()
            connectionOpen = False

            WalkCompanyQueryRs(responseMsgSet)
        Catch e As Exception

            If (sessionBegun) Then
                sessionManager.EndSession()
            End If
            If (connectionOpen) Then
                sessionManager.CloseConnection()
            End If
        End Try
    End Sub
    Public Sub BuildCompanyQueryRq(requestMsgSet As IMsgSetRequest, ByVal CompanyFile As String)
        Dim CompanyQueryRq As ICompanyQuery
        CompanyQueryRq = requestMsgSet.AppendCompanyQueryRq()
        'Set field value for IncludeRetElementList
        'May create more than one of these if needed
        CompanyQueryRq.IncludeRetElementList.Add(CompanyFile)
        'Set field value for OwnerIDList
        'May create more than one of these if needed
        'CompanyQueryRq.OwnerIDList.Add(System.Guid.NewGuid().ToString())
    End Sub
    Public Sub WalkCompanyQueryRs(responseMsgSet As IMsgSetResponse)
        If (responseMsgSet Is Nothing) Then
            Exit Sub
        End If

        Dim responseList As IResponseList
        responseList = responseMsgSet.ResponseList
        If (responseList Is Nothing) Then
            Exit Sub
        End If

        'if we sent only one request, there is only one response, we'll walk the list for this sample
        For j = 0 To responseList.Count - 1
            Dim response As IResponse
            response = responseList.GetAt(j)
            'check the status code of the response, 0=ok, >0 is warning
            If (response.StatusCode >= 0) Then
                'the request-specific response Is in the details, make sure we have some
                If (Not response.Detail Is Nothing) Then
                    'make sure the response Is the type we're expecting
                    Dim responseType As ENResponseType
                    responseType = CType(response.Type.GetValue(), ENResponseType)
                    If (responseType = ENResponseType.rtCompanyQueryRs) Then
                        '//upcast to more specific type here, this is safe because we checked with response.Type check above
                        Dim CompanyRet As ICompanyRet
                        CompanyRet = CType(response.Detail, ICompanyRet)
                        WalkCompanyRet(CompanyRet)
                    End If
                End If
            End If
        Next j
    End Sub
    Public Sub WalkCompanyRet(CompanyRet As ICompanyRet)
        If (CompanyRet Is Nothing) Then
            Exit Sub
        End If
        Dim CompanyAddress As String
        If Not (CompanyRet.Address Is Nothing AndAlso CompanyRet.Address.Addr1 Is Nothing) Then
            CompanyAddress = CompanyRet.Address.Addr1.GetValue()
        End If


    End Sub

    Public Function QBCreateInvoice(ByVal OD As OrderDetails(), ByVal OID As OrderItemDetails()) As String
        Dim OrderDetail = OD.Single
        Dim RefNumber As String = Nothing
        MyCustomerRet = DoCustomerQuery(OrderDetail.CustomerName, "NameFilter")
        If (MyCustomerRet Is Nothing) Then
            Dim CreateCustomerResult As Integer = MessageBox.Show("Customer " + OrderDetail.CustomerName + " doesn't exist in QuickBooks, Create?", "Create Customer?", MessageBoxButtons.YesNo)
            If CreateCustomerResult = DialogResult.Yes Then
                MyCustomerRet = DoCustomerAdd(OD)
            ElseIf CreateCustomerResult = DialogResult.No Then
                Return Nothing
                Exit Function
            End If
        End If
        RefNumber = DoInvoiceAdd(MyCustomerRet, OID, OrderDetail)

        Return RefNumber
    End Function
    Public Function DoPriceListQuery(ByVal QueryValue As String, ByVal QueryType As String) As String
        Dim TempPriceList As String = Nothing
        Dim _CustomerRet As ICustomerRet = DoCustomerQuery(QueryValue, "NameFilter")
        If Not _CustomerRet Is Nothing AndAlso Not _CustomerRet.PriceLevelRef Is Nothing Then
            TempPriceList = _CustomerRet.PriceLevelRef.FullName.GetValue()
        End If

        Return TempPriceList
    End Function
    Public Function DoCustomerQuery(ByVal QueryValue As String, ByVal QueryType As String) As ICustomerRet
        Dim tempCustomerRet As ICustomerRet = Nothing
        If (tempCustomerRet Is Nothing) Then
            Dim sessionBegun As Boolean
            sessionBegun = False
            Dim connectionOpen As Boolean
            connectionOpen = False
            Dim sessionManager As QBSessionManager
            sessionManager = Nothing

            Try
                'Create the session Manager object
                sessionManager = New QBSessionManager

                'Create the message set request object to hold our request
                Dim requestMsgSet As IMsgSetRequest
                requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0)
                requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue

                Dim CustomerQueryRq As ICustomerQuery
                CustomerQueryRq = requestMsgSet.AppendCustomerQueryRq()

                If (QueryType = "ListIDList") Then
                    'Set field value for ListIDList
                    'May create more than one of these if needed
                    CustomerQueryRq.ORCustomerListQuery.ListIDList.Add(QueryValue)
                End If

                If (QueryType = "FullNameList") Then
                    'Set field value for FullNameList
                    'May create more than one of these if needed
                    CustomerQueryRq.ORCustomerListQuery.FullNameList.Add(QueryValue)
                    ' Set field value for MaxReturned
                    CustomerQueryRq.ORCustomerListQuery.CustomerListFilter.MaxReturned.SetValue(6)
                    'Set field value for ActiveStatus
                    CustomerQueryRq.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly)
                End If




                If (QueryType = "NameFilter") Then
                    'Set field value for MatchCriterion
                    CustomerQueryRq.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains)
                    'Set field value for Name
                    CustomerQueryRq.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.Name.SetValue(QueryValue)
                End If

                'Connect to QuickBooks and begin a session
                sessionManager.OpenConnection("", "Sample Code from OSR")
                connectionOpen = True
                sessionManager.BeginSession("", ENOpenMode.omDontCare)
                sessionBegun = True

                'Send the request and get the response from QuickBooks
                Dim responseMsgSet As IMsgSetResponse
                responseMsgSet = sessionManager.DoRequests(requestMsgSet)

                'End the session and close the connection to QuickBooks
                sessionManager.EndSession()
                sessionBegun = False
                sessionManager.CloseConnection()
                connectionOpen = False

                If (responseMsgSet Is Nothing) Then
                    Return tempCustomerRet
                    Exit Function
                End If
                Dim CustomerRetList As ICustomerRetList = Nothing
                Dim responseList As IResponseList
                responseList = responseMsgSet.ResponseList
                If (responseList Is Nothing) Then
                    Return tempCustomerRet
                    Exit Function
                End If

                'if we sent only one request, there is only one response, we'll walk the list for this sample
                For j = 0 To responseList.Count - 1
                    Dim response As IResponse
                    response = responseList.GetAt(j)
                    'check the status code of the response, 0=ok, >0 is warning
                    If (response.StatusCode >= 0) Then
                        'the request-specific response Is in the details, make sure we have some
                        If (Not response.Detail Is Nothing) Then
                            'make sure the response Is the type we're expecting
                            Dim responseType As ENResponseType
                            responseType = CType(response.Type.GetValue(), ENResponseType)
                            If (responseType = ENResponseType.rtCustomerQueryRs) Then
                                '//upcast to more specific type here, this is safe because we checked with response.Type check above
                                CustomerRetList = CType(response.Detail, ICustomerRetList)
                                If (CustomerRetList.Count > 1) Then
                                    Dim MultiCustForm As New MultiCustomersForm(CustomerRetList)

                                    MultiCustForm.ShowDialog()
                                    tempCustomerRet = MultiCustForm.MyCustomerRet
                                Else
                                    tempCustomerRet = CustomerRetList.GetAt(0)
                                End If
                            End If
                        End If
                    End If
                Next j

            Catch e As Exception

                If (sessionBegun) Then
                    sessionManager.EndSession()
                End If
                If (connectionOpen) Then
                    sessionManager.CloseConnection()
                End If
                Return tempCustomerRet
            End Try
        End If
        Return tempCustomerRet
    End Function
    Public Function DoCustomerAdd(ByVal OD As OrderDetails()) As ICustomerRet
        Dim sessionBegun As Boolean
        sessionBegun = False
        Dim connectionOpen As Boolean
        connectionOpen = False
        Dim sessionManager As QBSessionManager
        sessionManager = Nothing
        Dim OrderDetail = OD.Single
        Dim TempCustomerRet As ICustomerRet = Nothing
        Try
            'Create the session Manager object
            sessionManager = New QBSessionManager

            'Create the message set request object to hold our request
            Dim requestMsgSet As IMsgSetRequest
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0)
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue

            Dim CustomerAddRq As ICustomerAdd
            CustomerAddRq = requestMsgSet.AppendCustomerAddRq()
            CustomerAddRq.Name.SetValue(OrderDetail.CustomerName)
            CustomerAddRq.CompanyName.SetValue(OrderDetail.CustomerName)
            CustomerAddRq.BillAddress.Addr1.SetValue(OrderDetail.CustomerAddressLine1)
            CustomerAddRq.BillAddress.City.SetValue(OrderDetail.CustomerCity)
            CustomerAddRq.BillAddress.State.SetValue(OrderDetail.CustomerState)
            CustomerAddRq.BillAddress.PostalCode.SetValue(OrderDetail.CustomerZip)

            'Connect to QuickBooks and begin a session
            sessionManager.OpenConnection("", "Sample Code from OSR")
            connectionOpen = True
            sessionManager.BeginSession("", ENOpenMode.omDontCare)
            sessionBegun = True

            'Send the request and get the response from QuickBooks
            Dim responseMsgSet As IMsgSetResponse
            responseMsgSet = sessionManager.DoRequests(requestMsgSet)

            'End the session and close the connection to QuickBooks
            sessionManager.EndSession()
            sessionBegun = False
            sessionManager.CloseConnection()
            connectionOpen = False

            If (responseMsgSet Is Nothing) Then

                Exit Function
            End If

            Dim responseList As IResponseList
            responseList = responseMsgSet.ResponseList
            If (responseList Is Nothing) Then
                Exit Function
            End If

            'if we sent only one request, there is only one response, we'll walk the list for this sample
            For j = 0 To responseList.Count - 1
                Dim response As IResponse
                response = responseList.GetAt(j)
                'check the status code of the response, 0=ok, >0 is warning
                If (response.StatusCode >= 0) Then
                    'the request-specific response Is in the details, make sure we have some
                    If (Not response.Detail Is Nothing) Then
                        'make sure the response Is the type we're expecting
                        Dim responseType As ENResponseType
                        responseType = CType(response.Type.GetValue(), ENResponseType)
                        If (responseType = ENResponseType.rtCustomerAddRs) Then
                            '//upcast to more specific type here, this is safe because we checked with response.Type check above
                            TempCustomerRet = CType(response.Detail, ICustomerRet)

                        End If
                    End If
                End If
            Next j
            Return TempCustomerRet
        Catch e As Exception
            MessageBox.Show(e.Message, "Error")
            If (sessionBegun) Then
                sessionManager.EndSession()
            End If
            If (connectionOpen) Then
                sessionManager.CloseConnection()
            End If
            Return TempCustomerRet
        End Try
    End Function
    Public Function DoItemQuery(ByVal QueryValue As String, ByVal QueryType As String) As IORItemRet
        Dim sessionBegun As Boolean
        sessionBegun = False
        Dim connectionOpen As Boolean
        connectionOpen = False
        Dim sessionManager As QBSessionManager
        sessionManager = Nothing
        Dim ItemRet As IORItemRet = Nothing
        Try
            'Create the session Manager object
            sessionManager = New QBSessionManager

            'Create the message set request object to hold our request
            Dim requestMsgSet As IMsgSetRequest
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0)
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue

            Dim ItemQueryRq As IItemQuery
            ItemQueryRq = requestMsgSet.AppendItemQueryRq()

            If (QueryType = "ListIDList") Then
                'Set field value for ListIDList
                'May create more than one of these if needed
                ItemQueryRq.ORListQuery.ListIDList.Add(QueryValue)
            End If

            If (QueryType = "FullNameList") Then
                'Set field value for FullNameList
                'May create more than one of these if needed
                ItemQueryRq.ORListQuery.FullNameList.Add(QueryValue)
            End If

            'Set field value for MaxReturned



            If (QueryType = "NameFilter") Then
                'Set field value for MatchCriterion
                ItemQueryRq.ORListQuery.ListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains)
                'Set field value for Name
                ItemQueryRq.ORListQuery.ListFilter.ORNameFilter.NameFilter.Name.SetValue(QueryValue)
            End If

            'Connect to QuickBooks and begin a session
            sessionManager.OpenConnection("", "Sample Code from OSR")
            connectionOpen = True
            sessionManager.BeginSession("", ENOpenMode.omDontCare)
            sessionBegun = True

            'Send the request and get the response from QuickBooks
            Dim responseMsgSet As IMsgSetResponse
            responseMsgSet = sessionManager.DoRequests(requestMsgSet)

            'End the session and close the connection to QuickBooks
            sessionManager.EndSession()
            sessionBegun = False
            sessionManager.CloseConnection()
            connectionOpen = False

            If (responseMsgSet Is Nothing) Then
                Return Nothing
                Exit Function
            End If
            Dim ORItemRetList As IORItemRetList = Nothing
            Dim responseList As IResponseList
            responseList = responseMsgSet.ResponseList
            If (responseList Is Nothing) Then
                Return Nothing
                Exit Function
            End If

            'if we sent only one request, there is only one response, we'll walk the list for this sample
            For j = 0 To responseList.Count - 1
                Dim response As IResponse
                response = responseList.GetAt(j)
                'check the status code of the response, 0=ok, >0 is warning
                If (response.StatusCode >= 0) Then
                    'the request-specific response Is in the details, make sure we have some
                    If (Not response.Detail Is Nothing) Then
                        'make sure the response Is the type we're expecting
                        Dim responseType As ENResponseType
                        responseType = CType(response.Type.GetValue(), ENResponseType)
                        If (responseType = ENResponseType.rtItemQueryRs) Then
                            '//upcast to more specific type here, this is safe because we checked with response.Type check above
                            ORItemRetList = CType(response.Detail, IORItemRetList)
                            ItemRet = ORItemRetList.GetAt(0)

                        End If
                    End If
                End If
            Next j
            Return ItemRet
        Catch e As Exception

            If (sessionBegun) Then
                sessionManager.EndSession()
            End If
            If (connectionOpen) Then
                sessionManager.CloseConnection()
            End If
            Return Nothing
        End Try

    End Function

    Public Function DoInvoiceAdd(ByVal CustomerRet As ICustomerRet, ByVal OID As OrderItemDetails(), ByVal OD As OrderDetails) As String
        Dim sessionBegun As Boolean
        sessionBegun = False
        Dim connectionOpen As Boolean
        connectionOpen = False
        Dim sessionManager As QBSessionManager
        sessionManager = Nothing
        Dim RefNumber As String = Nothing
        Try
            'Create the session Manager object
            sessionManager = New QBSessionManager

            'Create the message set request object to hold our request
            Dim requestMsgSet As IMsgSetRequest
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0)
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue

            Dim InvoiceAddRq As IInvoiceAdd
            InvoiceAddRq = requestMsgSet.AppendInvoiceAddRq()
            'Set field value for ListID
            If Not (CustomerRet Is Nothing) Then
                If Not (CustomerRet.ListID Is Nothing) Then
                    InvoiceAddRq.CustomerRef.ListID.SetValue(CustomerRet.ListID.GetValue)
                End If
                If Not (CustomerRet.FullName Is Nothing) Then
                    InvoiceAddRq.CustomerRef.FullName.SetValue(CustomerRet.FullName.GetValue)
                End If
                If Not (CustomerRet.BillAddress Is Nothing) Then
                    If Not CustomerRet.BillAddress.Addr1 Is Nothing Then
                        InvoiceAddRq.BillAddress.Addr1.SetValue(CustomerRet.BillAddress.Addr1.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.Addr2 Is Nothing Then
                        InvoiceAddRq.BillAddress.Addr2.SetValue(CustomerRet.BillAddress.Addr2.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.City Is Nothing Then
                        InvoiceAddRq.BillAddress.City.SetValue(CustomerRet.BillAddress.City.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.State Is Nothing Then
                        InvoiceAddRq.BillAddress.State.SetValue(CustomerRet.BillAddress.State.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.PostalCode Is Nothing Then
                        InvoiceAddRq.BillAddress.PostalCode.SetValue(CustomerRet.BillAddress.PostalCode.GetValue)
                    End If
                End If
            End If
                Dim Item As IORItemRet = Nothing
            Dim desc As String
            Dim OrderItem = OID.Take(1).FirstOrDefault
            'Adds Line Items
            If (OID.Count > 1) Then
                Item = DoItemQuery("Seed Mix Per Worksheet", "NameFilter")
                desc = "Seed Mix Per Worksheet"
            Else
                Item = DoItemQuery(OrderItem.Lot, "NameFilter")
                desc = OrderItem.Item
            End If

            If Not (Item Is Nothing) Then
                Dim InvoiceLineAdd As IInvoiceLineAdd = InvoiceAddRq.ORInvoiceLineAddList.Append.InvoiceLineAdd

                Select Case Item.ortype
                    Case ENORItemRet.orirItemInventoryAssemblyRet
                        InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemInventoryAssemblyRet.ListID.GetValue)
                        InvoiceLineAdd.Quantity.SetValue(OD.Acres)
                        InvoiceLineAdd.Amount.SetValue(OD.PricePerAcre)
                    Case ENORItemRet.orirItemNonInventoryRet
                        InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemNonInventoryRet.ListID.GetValue)
                        InvoiceLineAdd.Quantity.SetValue(OD.Acres)
                        InvoiceLineAdd.Amount.SetValue((OD.PricePerAcre) * OD.Acres)
                    Case ENORItemRet.orirItemServiceRet
                        InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemServiceRet.ListID.GetValue)
                        InvoiceLineAdd.Quantity.SetValue(OD.Acres)
                        InvoiceLineAdd.Amount.SetValue(Decimal.Round(OD.PricePerAcre))
                    Case ENORItemRet.orirItemDiscountRet
                        InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemDiscountRet.ListID.GetValue)
                        InvoiceLineAdd.Amount.SetValue(Decimal.Round(OD.PricePerAcre))
                    Case ENORItemRet.orirItemInventoryRet
                        InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemInventoryRet.ListID.GetValue)
                        InvoiceLineAdd.Quantity.SetValue(OD.Acres)
                        InvoiceLineAdd.Amount.SetValue(Decimal.Round(OD.PricePerAcre))
                    Case ENORItemRet.orirItemOtherChargeRet
                        InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemOtherChargeRet.ListID.GetValue)
                        InvoiceLineAdd.Quantity.SetValue(OD.Acres)
                        InvoiceLineAdd.Amount.SetValue(Decimal.Round(OD.PricePerAcre))
                    Case ENORItemRet.orirItemSalesTaxRet
                        InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemSalesTaxRet.ListID.GetValue)
                        InvoiceLineAdd.Amount.SetValue(Decimal.Round(OD.PricePerAcre))
                    Case ENORItemRet.orirItemSalesTaxGroupRet
                        InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemSalesTaxGroupRet.ListID.GetValue)
                        InvoiceLineAdd.Amount.SetValue(Decimal.Round(OD.PricePerAcre))
                End Select
                InvoiceLineAdd.Desc.SetValue(desc)
            Else
                MsgBox("Item: " + OrderItem.Lot + " doesn't exist. Please add to QuickBooks.  Invoice NOT Created")
            End If


            'Connect to QuickBooks and begin a session
            sessionManager.OpenConnection("", "Sample Code from OSR")
            connectionOpen = True
            sessionManager.BeginSession("", ENOpenMode.omDontCare)
            sessionBegun = True

            'Send the request and get the response from QuickBooks
            Dim responseMsgSet As IMsgSetResponse
            responseMsgSet = sessionManager.DoRequests(requestMsgSet)

            'End the session and close the connection to QuickBooks
            sessionManager.EndSession()
            sessionBegun = False
            sessionManager.CloseConnection()
            connectionOpen = False

            If (responseMsgSet Is Nothing) Then
                Exit Function
            End If

            Dim responseList As IResponseList
            responseList = responseMsgSet.ResponseList
            If (responseList Is Nothing) Then
                Exit Function
            End If

            'if we sent only one request, there is only one response, we'll walk the list for this sample
            For j = 0 To responseList.Count - 1
                Dim response As IResponse
                response = responseList.GetAt(j)
                'check the status code of the response, 0=ok, >0 is warning
                If (response.StatusCode >= 0) Then
                    'the request-specific response Is in the details, make sure we have some
                    If (Not response.Detail Is Nothing) Then
                        'make sure the response Is the type we're expecting
                        Dim responseType As ENResponseType
                        responseType = CType(response.Type.GetValue(), ENResponseType)
                        If (responseType = ENResponseType.rtInvoiceAddRs) Then
                            '//upcast to more specific type here, this is safe because we checked with response.Type check above
                            Dim InvoiceRet As IInvoiceRet
                            InvoiceRet = CType(response.Detail, IInvoiceRet)
                            If (Not InvoiceRet.RefNumber Is Nothing) Then
                                RefNumber = InvoiceRet.RefNumber.GetValue()
                            End If
                        End If
                    End If
                End If
            Next j
            Return RefNumber
        Catch e As Exception
            MessageBox.Show(e.Message, "Error")
            If (sessionBegun) Then
                sessionManager.EndSession()
            End If
            If (connectionOpen) Then
                sessionManager.CloseConnection()
            End If
        End Try

    End Function
    Public Sub BuildInvoiceAddRq(requestMsgSet As IMsgSetRequest)


    End Sub

End Class


Public Class QBCustomers
    Public Property mCustomerName As String
    Public Property mCustomerAddress1 As String
    Public Property mCustomerAddress2 As String
    Public Property mCustomerCity As String
    Public Property mCustomerState As String
    Public Property mCustomerZip As String
    Public Property mQBListID As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal CustomerName As String, ByVal CustomerAddress1 As String, ByVal CustomerAddress2 As String, ByVal CustomerCity As String, ByVal CustomerState As String, ByVal CustomerZip As String, ByVal QBListID As String)
            mCustomerName = CustomerName
            mCustomerAddress1 = CustomerAddress1
            mCustomerAddress2 = CustomerAddress2
            mCustomerCity = CustomerCity
            mCustomerState = CustomerState
            mCustomerZip = CustomerZip
            mQBListID = QBListID
        End Sub
        Public Property CustomerName As String
            Get
                Return mCustomerName
            End Get
            Set(ByVal value As String)
                mCustomerName = value
            End Set
        End Property

        Public Property CustomerAddress1 As String
            Get
                Return mCustomerAddress1
            End Get
            Set(ByVal value As String)
                mCustomerAddress1 = value
            End Set
        End Property

        Public Property CustomerAddress2 As String
            Get
                Return mCustomerAddress2
            End Get
            Set(ByVal value As String)
                mCustomerAddress2 = value
            End Set
        End Property
        Public Property CustomerCity As String
            Get
                Return mCustomerCity
            End Get
            Set(ByVal value As String)
                mCustomerCity = value
            End Set
        End Property
        Public Property CustomerState As String
            Get
                Return mCustomerState
            End Get
            Set(ByVal value As String)
                mCustomerState = value
            End Set
        End Property
    Public Property CustomerZip As String
        Get
            Return mCustomerZip
        End Get
        Set(ByVal value As String)
            mCustomerZip = value
        End Set
    End Property
    Public Property QBListID As String
        Get
            Return mQBListID
        End Get
        Set(ByVal value As String)
            mQBListID = value
        End Set
    End Property
End Class
