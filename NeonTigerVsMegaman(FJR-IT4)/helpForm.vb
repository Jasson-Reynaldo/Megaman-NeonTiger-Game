Public Class helpForm
    Dim dragable As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer

    Sub mousemovedown()
        dragable = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Sub mousemoving()
        If dragable Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Sub mousemoveup()
        dragable = False
    End Sub

    Private Sub helpForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub pnlTop_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlTop.MouseMove
        mousemoving()
    End Sub

    Private Sub pnlTop_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlTop.MouseUp
        mousemoveup()
    End Sub

    Private Sub pnlTop_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTop.MouseDown
        mousemovedown()
    End Sub


    Private Sub lblTop_MouseDown(sender As Object, e As MouseEventArgs) Handles lblTop.MouseDown
        mousemovedown()
    End Sub

    Private Sub lblTop_MouseMove(sender As Object, e As MouseEventArgs) Handles lblTop.MouseMove
        mousemoving()
    End Sub

    Private Sub lblTop_MouseUp(sender As Object, e As MouseEventArgs) Handles lblTop.MouseUp
        mousemoveup()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub helpForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class