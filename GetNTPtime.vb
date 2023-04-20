Imports System.Net
Imports System.Net.Sockets

'C# https://stackoverflow.com/a/12150289

Module NTP

    Sub Main()

        Console.WriteLine(GetNetworkTime("time.windows.com"))
        Console.ReadLine()
    End Sub

    Public Function GetNetworkTime(ByVal ntpServer As String) As DateTime

        ' NTP message size - 16 bytes of the digest (RFC 2030)
        Dim ntpData = New Byte(47) {}

        'Setting the Leap Indicator, Version Number and Mode values
        ntpData(0) = &H1B 'LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

        Dim addresses = Dns.GetHostEntry(ntpServer).AddressList

        'The UDP port number assigned to NTP is 123
        Dim ipEndPoint = New IPEndPoint(addresses(0), 123)
        'NTP uses UDP

        Using socket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            socket.Connect(ipEndPoint)

            'Stops code hang if NTP is blocked
            socket.ReceiveTimeout = 3000

            socket.Send(ntpData)
            socket.Receive(ntpData)
            socket.Close()
        End Using

        'Offset to get to the "Transmit Timestamp" field (time at which the reply 
        'departed the server for the client, in 64-bit timestamp format."
        Const serverReplyTime As Byte = 40

        'Get the seconds part
        Dim intPart As ULong = BitConverter.ToUInt32(ntpData, serverReplyTime)

        'Get the seconds fraction
        Dim fractPart As ULong = BitConverter.ToUInt32(ntpData, serverReplyTime + 4)

        'Convert From big-endian to little-endian
        intPart = SwapEndianness(intPart)
        fractPart = SwapEndianness(fractPart)

        Dim milliseconds = intPart * 1000 + fractPart * 1000 / &H100000000L

        '**UTC** time
        Dim networkDateTime = (New DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(milliseconds)

        Return networkDateTime.ToLocalTime()

    End Function

    ' stackoverflow.com/a/3294698/162671
    Private Function SwapEndianness(ByVal x As ULong) As UInteger
        Return ((x And &HFF) << 24) + ((x And &HFF00) << 8) + ((x And &HFF0000) >> 8) + ((x And &HFF000000UI) >> 24)
    End Function

End Module
