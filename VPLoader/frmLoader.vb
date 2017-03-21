Public Class frmLoader

    Dim GamesList As New ArrayList
    Dim CurrentGame As Integer = 0

    Private Sub LoadGames()
        For x As Integer = 1 To 10 Step 1
            Dim cur As String = "Game #" & Str(x)
            GamesList.Add(cur)
        Next
    End Sub

    Private Function GetNext(ByVal TheCurrent As Integer)
        Dim NextGame As Integer = TheCurrent + 1
        If (NextGame >= GamesList.Count) Then NextGame = 0
        Return NextGame
    End Function

    Private Function GetLast(ByVal TheCurrent As Integer)
        Dim LastGame As Integer = TheCurrent - 1
        If (LastGame < 0) Then LastGame = GamesList.Count - 1
        Return LastGame
    End Function

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim ShowCurrent As Graphics = Me.CreateGraphics
        ShowCurrent.TranslateTransform((Me.Size.Width - 300), ((Me.Height / 2) + ((Me.Font.Size * GamesList(CurrentGame).ToString.Length) / 2)), Drawing2D.MatrixOrder.Prepend)
        ShowCurrent.RotateTransform(-90, Drawing2D.MatrixOrder.Prepend)
        ShowCurrent.DrawString(GamesList(CurrentGame).ToString, New Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style), Brushes.White, 0, 0)
        Dim LastGame As Integer = GetLast(CurrentGame)
        Dim ShowLast As Graphics = Me.CreateGraphics
        ShowLast.TranslateTransform((Me.Size.Width - 100), Me.Height - (Me.Font.Size * GamesList(LastGame).ToString.Length), Drawing2D.MatrixOrder.Prepend)
        ShowLast.RotateTransform(-90, Drawing2D.MatrixOrder.Prepend)
        ShowLast.DrawString(GamesList(LastGame).ToString, New Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style), Brushes.White, 0, 0)
        Dim NextGame As Integer = GetNext(CurrentGame)
        Dim ShowNext As Graphics = Me.CreateGraphics
        ShowNext.TranslateTransform((Me.Size.Width - 100), Me.Font.Size * GamesList(NextGame).ToString.Length, Drawing2D.MatrixOrder.Prepend)
        ShowNext.RotateTransform(-90, Drawing2D.MatrixOrder.Prepend)
        ShowNext.DrawString(GamesList(NextGame).ToString, New Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style), Brushes.White, 0, 0)
    End Sub

    Private Sub RotateGames(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim LastCurrent As Integer
        If e.KeyCode = Keys.Left Then
            LastCurrent = GetLast(CurrentGame)
        ElseIf e.KeyCode = Keys.Right Then
            LastCurrent = GetNext(CurrentGame)
        End If
        If (LastCurrent <> CurrentGame) Then
            CurrentGame = LastCurrent
            Me.Invalidate()
        End If
    End Sub

    Private Sub PreLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGames()
    End Sub

End Class
