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
    Public d As New GetDataClass

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
            MsgBox("Could not connect to QuickBooks" + vbCrLf + e.InnerException.ToString)
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

    Public Function QBCreateTransaction(ByVal OD As OrderDetails(), ByVal OID As OrderItemDetails(), ByVal QBTransactionType As String) As String

        Dim OrderDetail = OD.Single
        Dim RefNumber As String = Nothing
        MyCustomerRet = DoCustomerQuery(OrderDetail.CustomerName, "NameFilter", False)
        If (MyCustomerRet Is Nothing) Then
            Dim CreateCustomerResult As Integer = MessageBox.Show("Customer " + d.CurrentCustomer.CustomerName + " doesn't exist in QuickBooks, Create?", "Create Customer?", MessageBoxButtons.YesNo)
            If CreateCustomerResult = DialogResult.Yes Then
                MyCustomerRet = DoCustomerAdd(d.CurrentCustomer)
            ElseIf CreateCustomerResult = DialogResult.No Then
                Return Nothing
                Exit Function
            End If
        End If
        Select Case QBTransactionType
            Case "Invoice"
                RefNumber = DoInvoiceAdd(MyCustomerRet, OID, OrderDetail)
            Case "SalesReceipt"
                RefNumber = DoSalesReceiptAdd(MyCustomerRet, OID, OrderDetail)
            Case "Quote"
                RefNumber = "Q" + DoEstimateAdd(MyCustomerRet, OID, OrderDetail)
        End Select


        Return RefNumber
    End Function


    Public Function DoPriceListQuery(ByVal QueryValue As String, ByVal QueryType As String, ByVal IsCustomerEdit As Boolean) As String
        Dim TempPriceList As String = Nothing
        Dim _CustomerRet As ICustomerRet = DoCustomerQuery(QueryValue, QueryType, IsCustomerEdit)
        If Not _CustomerRet Is Nothing AndAlso Not _CustomerRet.PriceLevelRef Is Nothing Then
            TempPriceList = _CustomerRet.PriceLevelRef.FullName.GetValue()
        End If

        Return TempPriceList
    End Function
    Public Function DoQBCustomerListID(ByVal QueryValue As String, ByVal QueryType As String, ByVal IsCustomerEdit As Boolean) As String
        Dim CustListID As String = Nothing
        If Not IsCustomerEdit Then

            Dim _CustomerRet As ICustomerRet = DoCustomerQuery(QueryValue, "NameFilter", IsCustomerEdit)
            If Not _CustomerRet Is Nothing AndAlso Not _CustomerRet.ListID Is Nothing Then
                CustListID = _CustomerRet.ListID.GetValue()
            End If
        End If
        Return CustListID
    End Function

    Public Function DoCustomerQuery(ByVal QueryValue As String, ByVal QueryType As String, ByVal IsEditingCustomer As Boolean) As ICustomerRet

        Dim tempCustomerRet As ICustomerRet = Nothing
        If Not IsEditingCustomer Then
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
                                        ' d.CurrentCustomer.QBId=tempCustomerRet.ListID.GetValue()
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
        End If
        Return tempCustomerRet
    End Function
    Public Function DoCustomerAdd(ByVal customer As Customer) As String
        Dim sessionBegun As Boolean
        sessionBegun = False
        Dim connectionOpen As Boolean
        connectionOpen = False
        Dim sessionManager As QBSessionManager
        sessionManager = Nothing
        Dim CustomerListId As String = Nothing
        Dim TempCustomerRet As ICustomerRet = Nothing
        Try
            'Create the session Manager object
            sessionManager = New QBSessionManager

            'Create the message set request object to hold our request
            Dim requestMsgSet As IMsgSetRequest
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0)
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue
            TempCustomerRet = DoCustomerQuery(customer.CustomerName, "NameFilter", False)
            If (TempCustomerRet Is Nothing) Then


                Dim CustomerAddRq As ICustomerAdd
                CustomerAddRq = requestMsgSet.AppendCustomerAddRq()
                CustomerAddRq.Name.SetValue(customer.CustomerName)
                CustomerAddRq.CompanyName.SetValue(customer.CustomerName)
                CustomerAddRq.Name.SetValue(customer.CustomerName)
                CustomerAddRq.BillAddress.Addr1.SetValue(customer.CustomerName)
                CustomerAddRq.BillAddress.Addr2.SetValue(customer.CustomerAddressLine1)
                CustomerAddRq.BillAddress.Addr3.SetValue(customer.CustomerAddressLine2)
                CustomerAddRq.BillAddress.City.SetValue(customer.CustomerCity)
                CustomerAddRq.BillAddress.State.SetValue(customer.CustomerState)
                CustomerAddRq.BillAddress.PostalCode.SetValue(customer.CustomerZip)

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
                    Return CustomerListId
                    Exit Function
                End If

                Dim responseList As IResponseList
                responseList = responseMsgSet.ResponseList
                If (responseList Is Nothing) Then
                    Return CustomerListId
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
            End If
            CustomerListId = TempCustomerRet.ListID.GetValue.ToString()
            Return CustomerListId
        Catch e As Exception
            MessageBox.Show(e.Message, "Error")
            If (sessionBegun) Then
                sessionManager.EndSession()
            End If
            If (connectionOpen) Then
                sessionManager.CloseConnection()
            End If
            Return CustomerListId
        End Try

    End Function

    Public Function DoItemAddUpdate(ByRef Item As Item) As String
        Dim ItemRet As IORItemRet
        Dim ListId As String = Nothing
        If (Item.QBListID Is Nothing) Then
            ItemRet = DoItemQuery(Item, "NameFilter")
        Else
            ItemRet = DoItemQuery(Item, "ListIDList")
        End If
        If ItemRet Is Nothing Then
            Dim addItem As MsgBoxResult = MsgBoxResult.Yes
            'MessageBox.Show("Item " + Item.Item + " does not Exist, Create?", "QuickBooks Missing Item", MessageBoxButtons.YesNoCancel)
            Select Case addItem
                Case MsgBoxResult.Yes
                    ItemRet = DoItemAdd(Item)
                Case MsgBoxResult.No
                    ListId = "NotQBItem"
                    Return ListId
                    Exit Function
                Case Else
                    Return "Cancel"
                    Exit Function
            End Select
        ElseIf (Not ItemRet.ItemNonInventoryRet Is Nothing) AndAlso (ItemRet.ItemNonInventoryRet.ManufacturerPartNumber Is Nothing) Then
            ItemRet = DoItemUpdate(Item)
        End If
        If Not ItemRet Is Nothing AndAlso Not ItemRet.ItemNonInventoryRet.ListID Is Nothing Then
            ListId = ItemRet.ItemNonInventoryRet.ListID.GetValue().ToString()
        End If
        Return ListId

    End Function
    Public Function DoItemQuery(ByVal Item As Item, ByVal QueryType As String) As IORItemRet
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
                ItemQueryRq.ORListQuery.ListIDList.Add(Item.Item)
            End If

            If (QueryType = "FullNameList") Then
                'Set field value for FullNameList
                'May create more than one of these if needed
                ItemQueryRq.ORListQuery.FullNameList.Add(Item.Item)
            End If

            'Set field value for MaxReturned



            If (QueryType = "NameFilter") Then
                'Set field value for MatchCriterion
                ItemQueryRq.ORListQuery.ListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcEndsWith)
                'Set field value for Name
                ItemQueryRq.ORListQuery.ListFilter.ORNameFilter.NameFilter.Name.SetValue(Item.Item)
            End If

            'If (QueryType = "PartNumberFilter") Then
            '    'Set field value for MatchCriterion
            '    ItemQueryRq.ORListQuery.
            '    'Set field value for Name
            '    ItemQueryRq.ORListQuery.ListFilter.ORNameFilter.NameFilter.Name.SetValue(QueryValue)
            'End If

            'Or.ItemNonInventoryRet.ManufacturerPartNumber.GetValue()

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
                    If (response.StatusCode = 500) Then
                        ItemRet = DoItemQuery(Item, "NameFilter")
                        Return ItemRet

                    End If

                    If (Not response.Detail Is Nothing) Then
                            'make sure the response Is the type we're expecting
                            Dim responseType As ENResponseType
                            responseType = CType(response.Type.GetValue(), ENResponseType)
                            If (responseType = ENResponseType.rtItemQueryRs) Then
                            '//upcast to more specific type .re, this is safe because we checked with response.Type check above
                            ORItemRetList = CType(response.Detail, IORItemRetList)
                            For k = 0 To ORItemRetList.Count - 1
                                ItemRet = ORItemRetList.GetAt(k)
                                If (ItemRet.ItemNonInventoryRet.Name.GetValue.ToString() = Item.Item) Then
                                    Exit For
                                End If
                            Next
                            End If

                        End If
                    End If
            Next j
            Return ItemRet
        Catch e As Exception
            MsgBox(e.Message.ToString())
            If (sessionBegun) Then
                sessionManager.EndSession()
            End If
            If (connectionOpen) Then
                sessionManager.CloseConnection()
            End If
            Return Nothing
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
                ItemQueryRq.ORListQuery.ListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcEndsWith)
                'Set field value for Name
                ItemQueryRq.ORListQuery.ListFilter.ORNameFilter.NameFilter.Name.SetValue(QueryValue)
            End If

            'If (QueryType = "PartNumberFilter") Then
            '    'Set field value for MatchCriterion
            '    ItemQueryRq.ORListQuery.
            '    'Set field value for Name
            '    ItemQueryRq.ORListQuery.ListFilter.ORNameFilter.NameFilter.Name.SetValue(QueryValue)
            'End If

            'Or.ItemNonInventoryRet.ManufacturerPartNumber.GetValue()

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
    Public Function DoItemUpdate(ByVal Item As Item) As IORItemRet
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

            Dim ItemUpdateRq As IItemNonInventoryMod
            ItemUpdateRq = requestMsgSet.AppendItemNonInventoryModRq

            If (Item.QBListID Is Nothing) Then
                ItemRet = DoItemQuery(Item, "NameFilter")
            Else
                ItemRet = DoItemQuery(Item, "ListIDList")
            End If

            ItemUpdateRq.ManufacturerPartNumber.SetValue(Item.ItemID.ToString())
            ItemUpdateRq.EditSequence.SetValue("MZ")
            ItemUpdateRq.ListID.SetValue(ItemRet.ItemNonInventoryRet.ListID.GetValue())

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


    Public Function DoItemAdd(ByVal Item As Item) As IORItemRet
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

            Dim ItemNonInventoryAddRq As IItemNonInventoryAdd
            ItemNonInventoryAddRq = requestMsgSet.AppendItemNonInventoryAddRq()
            ItemNonInventoryAddRq.Name.SetValue(Item.Item)
            ItemNonInventoryAddRq.ORSalesPurchase.SalesOrPurchase.Desc.SetValue(Item.Item)

            ItemNonInventoryAddRq.ManufacturerPartNumber.SetValue(Item.Lot)

            ItemNonInventoryAddRq.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.SetValue(0.00)
            ItemNonInventoryAddRq.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("Merchandise Sales")

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
            Dim ItemNonInventoryRet As IItemNonInventoryRet = Nothing
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
                        If (responseType = ENResponseType.rtItemNonInventoryAddRs) Then
                            '//upcast to more specific type here, this is safe because we checked with response.Type check above
                            ItemNonInventoryRet = CType(response.Detail, IItemNonInventoryRet)
                            ItemRet = DoItemQuery(ItemNonInventoryRet.ListID.GetValue.ToString, "ListIDList")
                        End If
                    End If
                End If
            Next j
            Return ItemRet
        Catch e As Exception
            MsgBox(e.Message.ToString)
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
        Dim responseMsgSet As IMsgSetResponse = Nothing
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
                If Not (CustomerRet.Email Is Nothing) Then
                    InvoiceAddRq.IsToBeEmailed.SetValue(True)
                End If
            End If

            'Dim OrderItem = OID.Take(1).FirstOrDefault
            'Adds Line Items
            sessionManager.OpenConnection("", "Sample Code from OSR")
            connectionOpen = True
            sessionManager.BeginSession("", ENOpenMode.omDontCare)
            sessionBegun = True

            For Each OrderItem In OID
                Dim Item As IORItemRet = Nothing
                Dim desc As String
                Dim ItemAmount As Decimal = Nothing

                If (OD.IsMix) Then
                    Item = DoItemQuery(OD.LineItemSKU, "NameFilter")
                    desc = OD.LineItemDesc
                    ItemAmount = (Decimal.Round(OD.OrderTotal, 2, MidpointRounding.AwayFromZero))
                    If (Item Is Nothing) Then
                        MsgBox(OD.LineItemSKU, vbOK, "Item Does Not Exist")
                    End If
                Else
                    Dim CommonName As String
                    If OrderItem.ItemID > 0 Then
                        Dim CommonNames = From items In d.Items Where items.ItemID = OrderItem.ItemID
                        CommonName = CommonNames.Single.Item
                    Else
                        CommonName = OrderItem.Item
                    End If

                    Item = DoItemQuery(CommonName, "NameFilter")
                    If (Item Is Nothing) Then
                        MsgBox(OrderItem.Lot + " does not Exist.",, "Invoice Not created!")
                        Exit For
                    End If
                    desc = OrderItem.Item
                    ItemAmount = (Decimal.Round((OrderItem.PricePerAcre * OD.Acres), 2, MidpointRounding.AwayFromZero))
                End If

                If Not (Item Is Nothing) Then
                    Dim InvoiceLineAdd As IInvoiceLineAdd = InvoiceAddRq.ORInvoiceLineAddList.Append.InvoiceLineAdd

                    Select Case Item.ortype
                        Case ENORItemRet.orirItemInventoryAssemblyRet
                            InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemInventoryAssemblyRet.ListID.GetValue)
                            InvoiceLineAdd.Quantity.SetValue(OD.Acres)
                            InvoiceLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemNonInventoryRet
                            InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemNonInventoryRet.ListID.GetValue)
                            InvoiceLineAdd.Quantity.SetValue(OD.Acres)
                            InvoiceLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemServiceRet
                            InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemServiceRet.ListID.GetValue)
                            InvoiceLineAdd.Quantity.SetValue(OD.Acres)
                            InvoiceLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemDiscountRet
                            InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemDiscountRet.ListID.GetValue)
                            InvoiceLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemInventoryRet
                            InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemInventoryRet.ListID.GetValue)
                            InvoiceLineAdd.Quantity.SetValue(OD.Acres)
                            InvoiceLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemOtherChargeRet
                            InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemOtherChargeRet.ListID.GetValue)
                            InvoiceLineAdd.Quantity.SetValue(OD.Acres)
                            InvoiceLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemSalesTaxRet
                            InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemSalesTaxRet.ListID.GetValue)
                            InvoiceLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemSalesTaxGroupRet
                            InvoiceLineAdd.ItemRef.ListID.SetValue(Item.ItemSalesTaxGroupRet.ListID.GetValue)
                            InvoiceLineAdd.Amount.SetValue(ItemAmount)
                    End Select
                    InvoiceLineAdd.Desc.SetValue(desc)


                End If
                If (OD.IsMix) Then
                    Exit For
                End If
            Next
            responseMsgSet = sessionManager.DoRequests(requestMsgSet)
            'End the session and close the connection to QuickBooks
            sessionManager.EndSession()
            sessionBegun = False
            sessionManager.CloseConnection()
            connectionOpen = False

            If (responseMsgSet Is Nothing) Then
                Return RefNumber
                Exit Function
            End If

            Dim responseList As IResponseList
            responseList = responseMsgSet.ResponseList
            If (responseList Is Nothing) Then
                Return RefNumber
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
            Return RefNumber
        End Try

    End Function

    Public Function DoSalesReceiptAdd(ByVal CustomerRet As ICustomerRet, ByVal OID As OrderItemDetails(), ByVal OD As OrderDetails) As String
        Dim sessionBegun As Boolean
        sessionBegun = False
        Dim connectionOpen As Boolean
        connectionOpen = False
        Dim sessionManager As QBSessionManager
        sessionManager = Nothing
        Dim RefNumber As String = Nothing
        Dim responseMsgSet As IMsgSetResponse = Nothing
        Try
            'Create the session Manager object
            sessionManager = New QBSessionManager

            'Create the message set request object to hold our request
            Dim requestMsgSet As IMsgSetRequest
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0)
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue

            Dim SalesReceiptAddRq As ISalesReceiptAdd
            SalesReceiptAddRq = requestMsgSet.AppendSalesReceiptAddRq()
            'Set field value for ListID
            If Not (CustomerRet Is Nothing) Then
                If Not (CustomerRet.ListID Is Nothing) Then
                    SalesReceiptAddRq.CustomerRef.ListID.SetValue(CustomerRet.ListID.GetValue)
                End If
                If Not (CustomerRet.FullName Is Nothing) Then
                    SalesReceiptAddRq.CustomerRef.FullName.SetValue(CustomerRet.FullName.GetValue)
                End If
                If Not (CustomerRet.BillAddress Is Nothing) Then
                    If Not CustomerRet.BillAddress.Addr1 Is Nothing Then
                        SalesReceiptAddRq.BillAddress.Addr1.SetValue(CustomerRet.BillAddress.Addr1.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.Addr2 Is Nothing Then
                        SalesReceiptAddRq.BillAddress.Addr2.SetValue(CustomerRet.BillAddress.Addr2.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.City Is Nothing Then
                        SalesReceiptAddRq.BillAddress.City.SetValue(CustomerRet.BillAddress.City.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.State Is Nothing Then
                        SalesReceiptAddRq.BillAddress.State.SetValue(CustomerRet.BillAddress.State.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.PostalCode Is Nothing Then
                        SalesReceiptAddRq.BillAddress.PostalCode.SetValue(CustomerRet.BillAddress.PostalCode.GetValue)
                    End If
                End If
            End If

            'Dim OrderItem = OID.Take(1).FirstOrDefault
            'Adds Line Items
            sessionManager.OpenConnection("", "Sample Code from OSR")
            connectionOpen = True
            sessionManager.BeginSession("", ENOpenMode.omDontCare)
            sessionBegun = True

            For Each OrderItem In OID
                Dim Item As IORItemRet = Nothing
                Dim desc As String
                Dim ItemAmount As Decimal = Nothing

                If (OD.IsMix) Then
                    Item = DoItemQuery(OD.LineItemSKU, "NameFilter")
                    desc = OD.LineItemDesc
                    ItemAmount = (Decimal.Round(OD.OrderTotal, 2, MidpointRounding.AwayFromZero))
                    If (Item Is Nothing) Then
                        MsgBox(OD.LineItemSKU, vbOK, "Item Does Not Exist")
                    End If
                Else
                    Dim CommonName As String
                    If OrderItem.ItemID > 0 Then
                        Dim CommonNames = From items In d.Items Where items.ItemID = OrderItem.ItemID
                        CommonName = CommonNames.Single.Item
                    Else
                        CommonName = OrderItem.Item
                    End If

                    Item = DoItemQuery(CommonName, "NameFilter")
                    If (Item Is Nothing) Then
                        MsgBox(OrderItem.Lot + " does not Exist.",, "SalesReceipt Not created!")
                        Exit For
                    End If
                    desc = OrderItem.Item
                    ItemAmount = (Decimal.Round((OrderItem.PricePerAcre * OD.Acres), 2, MidpointRounding.AwayFromZero))
                End If

                If Not (Item Is Nothing) Then
                    Dim SalesReceiptLineAdd As ISalesReceiptLineAdd = SalesReceiptAddRq.ORSalesReceiptLineAddList.Append.SalesReceiptLineAdd

                    Select Case Item.ortype
                        Case ENORItemRet.orirItemInventoryAssemblyRet
                            SalesReceiptLineAdd.ItemRef.ListID.SetValue(Item.ItemInventoryAssemblyRet.ListID.GetValue)
                            SalesReceiptLineAdd.Quantity.SetValue(OD.Acres)
                            SalesReceiptLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemNonInventoryRet
                            SalesReceiptLineAdd.ItemRef.ListID.SetValue(Item.ItemNonInventoryRet.ListID.GetValue)
                            SalesReceiptLineAdd.Quantity.SetValue(OD.Acres)
                            SalesReceiptLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemServiceRet
                            SalesReceiptLineAdd.ItemRef.ListID.SetValue(Item.ItemServiceRet.ListID.GetValue)
                            SalesReceiptLineAdd.Quantity.SetValue(OD.Acres)
                            SalesReceiptLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemDiscountRet
                            SalesReceiptLineAdd.ItemRef.ListID.SetValue(Item.ItemDiscountRet.ListID.GetValue)
                            SalesReceiptLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemInventoryRet
                            SalesReceiptLineAdd.ItemRef.ListID.SetValue(Item.ItemInventoryRet.ListID.GetValue)
                            SalesReceiptLineAdd.Quantity.SetValue(OD.Acres)
                            SalesReceiptLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemOtherChargeRet
                            SalesReceiptLineAdd.ItemRef.ListID.SetValue(Item.ItemOtherChargeRet.ListID.GetValue)
                            SalesReceiptLineAdd.Quantity.SetValue(OD.Acres)
                            SalesReceiptLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemSalesTaxRet
                            SalesReceiptLineAdd.ItemRef.ListID.SetValue(Item.ItemSalesTaxRet.ListID.GetValue)
                            SalesReceiptLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemSalesTaxGroupRet
                            SalesReceiptLineAdd.ItemRef.ListID.SetValue(Item.ItemSalesTaxGroupRet.ListID.GetValue)
                            SalesReceiptLineAdd.Amount.SetValue(ItemAmount)
                    End Select
                    SalesReceiptLineAdd.Desc.SetValue(desc)


                End If
                If (OD.IsMix) Then
                    Exit For
                End If
            Next
            responseMsgSet = sessionManager.DoRequests(requestMsgSet)
            'End the session and close the connection to QuickBooks
            sessionManager.EndSession()
            sessionBegun = False
            sessionManager.CloseConnection()
            connectionOpen = False

            If (responseMsgSet Is Nothing) Then
                Return RefNumber
                Exit Function
            End If

            Dim responseList As IResponseList
            responseList = responseMsgSet.ResponseList
            If (responseList Is Nothing) Then
                Return RefNumber
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
                        If (responseType = ENResponseType.rtSalesReceiptAddRs) Then
                            '//upcast to more specific type here, this is safe because we checked with response.Type check above
                            Dim SalesReceiptRet As ISalesReceiptRet
                            SalesReceiptRet = CType(response.Detail, ISalesReceiptRet)
                            If (Not SalesReceiptRet.RefNumber Is Nothing) Then
                                RefNumber = SalesReceiptRet.RefNumber.GetValue()
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
            Return RefNumber
        End Try

    End Function
    Public Function DoEstimateAdd(ByVal CustomerRet As ICustomerRet, ByVal OID As OrderItemDetails(), ByVal OD As OrderDetails) As String
        Dim sessionBegun As Boolean
        sessionBegun = False
        Dim connectionOpen As Boolean
        connectionOpen = False
        Dim sessionManager As QBSessionManager
        sessionManager = Nothing
        Dim RefNumber As String = Nothing
        Dim responseMsgSet As IMsgSetResponse = Nothing
        Try
            'Create the session Manager object
            sessionManager = New QBSessionManager

            'Create the message set request object to hold our request
            Dim requestMsgSet As IMsgSetRequest
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0)
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue

            Dim EstimateAddRq As IEstimateAdd
            EstimateAddRq = requestMsgSet.AppendEstimateAddRq()
            'Set field value for ListID
            If Not (CustomerRet Is Nothing) Then
                If Not (CustomerRet.ListID Is Nothing) Then
                    EstimateAddRq.CustomerRef.ListID.SetValue(CustomerRet.ListID.GetValue)
                End If
                If Not (CustomerRet.FullName Is Nothing) Then
                    EstimateAddRq.CustomerRef.FullName.SetValue(CustomerRet.FullName.GetValue)
                End If
                If Not (CustomerRet.BillAddress Is Nothing) Then
                    If Not CustomerRet.BillAddress.Addr1 Is Nothing Then
                        EstimateAddRq.BillAddress.Addr1.SetValue(CustomerRet.BillAddress.Addr1.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.Addr2 Is Nothing Then
                        EstimateAddRq.BillAddress.Addr2.SetValue(CustomerRet.BillAddress.Addr2.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.City Is Nothing Then
                        EstimateAddRq.BillAddress.City.SetValue(CustomerRet.BillAddress.City.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.State Is Nothing Then
                        EstimateAddRq.BillAddress.State.SetValue(CustomerRet.BillAddress.State.GetValue)
                    End If
                    If Not CustomerRet.BillAddress.PostalCode Is Nothing Then
                        EstimateAddRq.BillAddress.PostalCode.SetValue(CustomerRet.BillAddress.PostalCode.GetValue)
                    End If
                End If
            End If

            'Dim OrderItem = OID.Take(1).FirstOrDefault
            'Adds Line Items
            sessionManager.OpenConnection("", "Sample Code from OSR")
            connectionOpen = True
            sessionManager.BeginSession("", ENOpenMode.omDontCare)
            sessionBegun = True

            For Each OrderItem In OID
                Dim Item As IORItemRet = Nothing
                Dim desc As String
                Dim ItemAmount As Decimal = Nothing

                If (OD.IsMix) Then
                    Item = DoItemQuery(OD.LineItemSKU, "NameFilter")
                    desc = OD.LineItemDesc
                    ItemAmount = (Decimal.Round(OD.OrderTotal, 2, MidpointRounding.AwayFromZero))
                    If (Item Is Nothing) Then
                        MsgBox(OD.LineItemSKU, vbOK, "Item Does Not Exist")
                    End If
                Else
                    Dim CommonName As String
                    If OrderItem.ItemID > 0 Then
                        Dim CommonNames = From items In d.Items Where items.ItemID = OrderItem.ItemID
                        CommonName = CommonNames.Single.Item
                    Else
                        CommonName = OrderItem.Item
                    End If

                    Item = DoItemQuery(CommonName, "NameFilter")
                    If (Item Is Nothing) Then
                        MsgBox(OrderItem.Lot + " does not Exist.",, "Estimate Not created!")
                        Exit For
                    End If
                    desc = OrderItem.Item
                    ItemAmount = (Decimal.Round((OrderItem.PricePerAcre * OD.Acres), 2, MidpointRounding.AwayFromZero))
                End If

                If Not (Item Is Nothing) Then
                    Dim EstimateLineAdd As IEstimateLineAdd = EstimateAddRq.OREstimateLineAddList.Append.EstimateLineAdd

                    Select Case Item.ortype
                        Case ENORItemRet.orirItemInventoryAssemblyRet
                            EstimateLineAdd.ItemRef.ListID.SetValue(Item.ItemInventoryAssemblyRet.ListID.GetValue)
                            EstimateLineAdd.Quantity.SetValue(OD.Acres)
                            EstimateLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemNonInventoryRet
                            EstimateLineAdd.ItemRef.ListID.SetValue(Item.ItemNonInventoryRet.ListID.GetValue)
                            EstimateLineAdd.Quantity.SetValue(OD.Acres)
                            EstimateLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemServiceRet
                            EstimateLineAdd.ItemRef.ListID.SetValue(Item.ItemServiceRet.ListID.GetValue)
                            EstimateLineAdd.Quantity.SetValue(OD.Acres)
                            EstimateLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemDiscountRet
                            EstimateLineAdd.ItemRef.ListID.SetValue(Item.ItemDiscountRet.ListID.GetValue)
                            EstimateLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemInventoryRet
                            EstimateLineAdd.ItemRef.ListID.SetValue(Item.ItemInventoryRet.ListID.GetValue)
                            EstimateLineAdd.Quantity.SetValue(OD.Acres)
                            EstimateLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemOtherChargeRet
                            EstimateLineAdd.ItemRef.ListID.SetValue(Item.ItemOtherChargeRet.ListID.GetValue)
                            EstimateLineAdd.Quantity.SetValue(OD.Acres)
                            EstimateLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemSalesTaxRet
                            EstimateLineAdd.ItemRef.ListID.SetValue(Item.ItemSalesTaxRet.ListID.GetValue)
                            EstimateLineAdd.Amount.SetValue(ItemAmount)
                        Case ENORItemRet.orirItemSalesTaxGroupRet
                            EstimateLineAdd.ItemRef.ListID.SetValue(Item.ItemSalesTaxGroupRet.ListID.GetValue)
                            EstimateLineAdd.Amount.SetValue(ItemAmount)
                    End Select
                    EstimateLineAdd.Desc.SetValue(desc)


                End If
                If (OD.IsMix) Then
                    Exit For
                End If
            Next
            responseMsgSet = sessionManager.DoRequests(requestMsgSet)
            'End the session and close the connection to QuickBooks
            sessionManager.EndSession()
            sessionBegun = False
            sessionManager.CloseConnection()
            connectionOpen = False

            If (responseMsgSet Is Nothing) Then
                Return RefNumber
                Exit Function
            End If

            Dim responseList As IResponseList
            responseList = responseMsgSet.ResponseList
            If (responseList Is Nothing) Then
                Return RefNumber
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
                        If (responseType = ENResponseType.rtEstimateAddRs) Then
                            '//upcast to more specific type here, this is safe because we checked with response.Type check above
                            Dim EstimateRet As IEstimateRet
                            EstimateRet = CType(response.Detail, IEstimateRet)
                            If (Not EstimateRet.RefNumber Is Nothing) Then
                                RefNumber = EstimateRet.RefNumber.GetValue()
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
            Return RefNumber
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
