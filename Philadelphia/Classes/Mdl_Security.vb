Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Module Mdl_Security

    Private TripleDes As New TripleDESCryptoServiceProvider

    'Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()

    '    Dim sha1 As New SHA1CryptoServiceProvider

    '    ' Hash the key. 
    '    Dim keyBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(key)
    '    Dim hash() As Byte = sha1.ComputeHash(keyBytes)

    '    ' Truncate or pad the hash. 
    '    ReDim Preserve hash(length - 1)
    '    Return hash
    'End Function

    'Public Function fEncryptData(ByVal pPlaintext As String) As String

    '    ' Convert the plaintext string to a byte array. 
    '    Dim plaintextBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(pPlaintext)

    '    ' Create the stream. 
    '    Dim ms As New System.IO.MemoryStream
    '    ' Create the encoder to write to the stream. 
    '    Dim encStream As New CryptoStream(ms, _
    '        TripleDes.CreateEncryptor(), _
    '        System.Security.Cryptography.CryptoStreamMode.Write)

    '    ' Use the crypto stream to write the byte array to the stream.
    '    encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
    '    encStream.FlushFinalBlock()

    '    ' Convert the encrypted stream to a printable string. 
    '    Return Convert.ToBase64String(ms.ToArray)
    'End Function

    'Public Function fDecryptData(ByVal encryptedtext As String) As String

    '    ' Convert the encrypted text string to a byte array. 
    '    Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

    '    ' Create the stream. 
    '    Dim ms As New System.IO.MemoryStream
    '    ' Create the decoder to write to the stream. 
    '    Dim decStream As New CryptoStream(ms, _
    '        TripleDes.CreateDecryptor(), _
    '        System.Security.Cryptography.CryptoStreamMode.Write)

    '    ' Use the crypto stream to write the byte array to the stream.
    '    decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
    '    decStream.FlushFinalBlock()

    '    ' Convert the plaintext stream to a string. 
    '    Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    'End Function

    Public Function sEncrypt(ByVal pText As String) As String
        Return Encrypt(pText, "&%#@?,:*")
    End Function
    'Decrypt the text 
    Public Function sDecrypt(ByVal pText As String) As String
        Return Decrypt(pText, "&%#@?,:*")
    End Function
    'The function used to encrypt the text
    Private Function Encrypt(ByVal strText As String, ByVal strEncrKey As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8))
            Dim des As New System.Security.Cryptography.DESCryptoServiceProvider
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
            Dim ms As New MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray())
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    'The function used to decrypt the text
    Private Function Decrypt(ByVal strText As String, ByVal sDecrKey As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Return encoding.GetString(ms.ToArray())
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
End Module
