Public Class APIDocument
    Public Class IssuerAddress
        Public Property branchID As String = ""
        Public Property country As String = ""
        Public Property governate As String = ""
        Public Property regionCity As String = ""
        Public Property street As String = ""
        Public Property buildingNumber As String = ""
        Public Property postalCode As String = ""
        Public Property floor As String = ""
        Public Property room As String = ""
        Public Property landmark As String = ""
        Public Property additionalInformation As String = ""
    End Class

    Public Class ReceiverAddress
        'Public Property branchID As String = ""
        Public Property country As String = ""
        Public Property governate As String = ""
        Public Property regionCity As String = ""
        Public Property street As String = cControls.fReturnValue(" Select Street From Customers Where Code = '" & vCustomer_Code & "' ", "")
        Public Property buildingNumber As String = cControls.fReturnValue(" Select BuildingNum From Customers Where Code = '" & vCustomer_Code & "' ", "")
        Public Property postalCode As String = ""
        Public Property floor As String = ""
        Public Property room As String = ""
        Public Property landmark As String = ""
        Public Property additionalInformation As String = ""
    End Class

    Public Class Issuer
        Public Property address As IssuerAddress
        Public Property type As String = ""
        Public Property id As String = ""
        Public Property name As String = ""
    End Class

    Public Class Receiver
        Public Property address As ReceiverAddress
        Public Property type As String = ""
        Public Property id As String = ""
        Public Property name As String = ""
    End Class

    Public Class Payment
        Public Property bankName As String = ""
        Public Property bankAddress As String = ""
        Public Property bankAccountNo As String = ""
        Public Property bankAccountIBAN As String = ""
        Public Property swiftCode As String = ""
        Public Property terms As String = ""
    End Class

    Public Class Delivery
        Public Property approach As String = ""
        Public Property packaging As String = ""
        Public Property dateValidity As DateTime
        Public Property exportPort As String = ""
        Public Property grossWeight As Double
        Public Property netWeight As Double
        Public Property terms As String = ""
    End Class

    Public Class UnitValue
        Public Property currencySold As String = ""
        Public Property amountEGP As Decimal
    End Class

    Public Class Discount
        Public Property rate As Double
        Public Property amount As Double
    End Class

    Public Class TaxableItem
        Public Property taxType As String
        Public Property amount As Double
        Public Property subType As String
        Public Property rate As Integer
    End Class

    Public Class InvoiceLine
        Public Property description As String = ""
        Public Property itemType As String = ""
        Public Property itemCode As String = ""
        Public Property unitType As String = ""
        Public Property quantity As Integer
        Public Property internalCode As String
        Public Property salesTotal As Decimal
        Public Property total As Double
        Public Property valueDifference As Decimal
        Public Property totalTaxableFees As Decimal
        Public Property netTotal As Decimal
        Public Property itemsDiscount As Decimal
        Public Property unitValue As UnitValue
        Public Property discount As Discount
        Public Property taxableItems As List(Of TaxableItem)
    End Class

    Public Class TaxTotal
        Public Property taxType As String
        Public Property amount As Double
    End Class

    Public Class Signature
        Public Property signatureType As String
        Public Property value As String
    End Class

    Public Class Document
        Public Property issuer As Issuer
        Public Property receiver As Receiver
        Public Property documentType As String
        Public Property documentTypeVersion As String
        Public Property dateTimeIssued As String
        Public Property taxpayerActivityCode As String
        Public Property internalID As String
        Public Property purchaseOrderReference As String = ""
        Public Property purchaseOrderDescription As String = ""
        Public Property salesOrderReference As String = ""
        Public Property salesOrderDescription As String = ""
        Public Property proformaInvoiceNumber As String = ""
        'Public Property payment As Payment
        'Public Property delivery As Delivery
        Public Property invoiceLines As List(Of InvoiceLine)
        Public Property totalDiscountAmount As Double
        Public Property totalSalesAmount As Double
        Public Property netAmount As Double
        Public Property taxTotals As List(Of TaxTotal)
        Public Property totalAmount As Double
        Public Property extraDiscountAmount As Double
        Public Property totalItemsDiscountAmount As Double
        'Public Property signatures As List(Of Signature)
    End Class

    Public Class Root
        Public Property documents As List(Of Document)
    End Class

    Public Class AcceptedDocument
        Public Property uuid As String
        Public Property longId As String
        Public Property internalId As String
        Public Property hashKey As String
    End Class

    Public Class AcceptedDocumentRoot
        Public Property submissionId As String
        Public Property acceptedDocuments As List(Of AcceptedDocument)
        Public Property rejectedDocuments As List(Of Object)
    End Class
End Class
