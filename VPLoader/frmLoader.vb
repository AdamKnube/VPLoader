Public Class frmLoader
    Private TablePath As String = ""
    Private GamesList As New ArrayList
    Private CurrentGame As Integer = 0
    Private TablesLoaded As Boolean = False

    Private Class GameInfo
        Private Path As String
        Private BackGlass As String = Nothing
        Public Function GetName() As String
            Return System.IO.Path.GetFileName(Path).Replace(".vpt", "")
        End Function
        Public Function GetPath() As String
            Return Path
        End Function
        Public Function GetGlass() As String
            Return BackGlass
        End Function
        Public Sub SetPath(ByVal NewPath As String)
            Path = NewPath
        End Sub
        Public Sub SetGlass(ByVal NewGlass As String)
            BackGlass = NewGlass
        End Sub
    End Class

    Private Sub LoadGames(Optional ByVal ForceNew = False)
        TablesLoaded = False
        TablePath = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\VPLoader", "Tables", Nothing)
        If ((TablePath Is Nothing) Or (ForceNew)) Then
            Dim PathFinder As New FolderBrowserDialog() With {
                .Description = "Select your tables folder...",
                .ShowNewFolderButton = False,
                .RootFolder = Environment.SpecialFolder.MyComputer
            }
            Dim Result As DialogResult = DialogResult.Cancel
            While Result <> DialogResult.OK
                MsgBox("You must select your ""Tables"" folder.", MsgBoxStyle.OkOnly, "VP Tables")
                Result = PathFinder.ShowDialog
            End While
            TablePath = PathFinder.SelectedPath
            My.Computer.Registry.CurrentUser.CreateSubKey("Tables")
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\VPLoader", "Tables", TablePath)
        End If
        DirSearch(TablePath)
        TablesLoaded = True
        Me.Invalidate()
    End Sub

    Private Sub DirSearch(ByVal StartIn As String)
        Dim d, f As String
        Try
            For Each d In System.IO.Directory.GetDirectories(StartIn)
                For Each f In System.IO.Directory.GetFiles(d, "*.vpt")
                    Dim thistable As New GameInfo
                    thistable.SetPath(f)
                    Dim testglass As String = f.Replace(".vpt", ".png")
                    If System.IO.File.Exists(testglass) Then
                        thistable.SetGlass(testglass)
                    End If
                    GamesList.Add(thistable)
                Next
                DirSearch(d)
            Next
        Catch excpt As System.Exception
            Debug.WriteLine(excpt.Message)
        End Try
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

    Private Sub ShowGlass(Optional ByVal BackGlass As String = Nothing)
        Dim thegame As GameInfo = GamesList(CurrentGame)
        If thegame.GetGlass Is Nothing Then
            pbxGlass.Image = Nothing
        Else
            pbxGlass.Image = Bitmap.FromFile(thegame.GetGlass)
            pbxGlass.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
        End If
        pbxGlass.Refresh()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If Not TablesLoaded Then Exit Sub
        Dim c As GameInfo = GamesList(CurrentGame)
        Dim l As GameInfo = GamesList(GetLast(CurrentGame))
        Dim n As GameInfo = GamesList(GetNext(CurrentGame))
        Dim ShowCurrent As Graphics = Me.CreateGraphics
        ShowCurrent.TranslateTransform((Me.Size.Width - 300), ((Me.Height / 2) + ((Me.Font.Size * c.GetName.Length) / 2)), Drawing2D.MatrixOrder.Prepend)
        ShowCurrent.RotateTransform(-90, Drawing2D.MatrixOrder.Prepend)
        ShowCurrent.DrawString(c.GetName, New Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style), Brushes.White, 0, 0)
        Dim ShowLast As Graphics = Me.CreateGraphics
        ShowLast.TranslateTransform((Me.Size.Width - 100), Me.Height - 50, Drawing2D.MatrixOrder.Prepend)
        ShowLast.RotateTransform(-90, Drawing2D.MatrixOrder.Prepend)
        ShowLast.DrawString(l.GetName, New Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style), Brushes.White, 0, 0)
        Dim ShowNext As Graphics = Me.CreateGraphics
        ShowNext.TranslateTransform((Me.Size.Width - 100), Me.Font.Size * n.GetName.Length * 0.8, Drawing2D.MatrixOrder.Prepend)
        ShowNext.RotateTransform(-90, Drawing2D.MatrixOrder.Prepend)
        ShowNext.DrawString(n.GetName, New Font(Me.Font.FontFamily, Me.Font.Size, Me.Font.Style), Brushes.White, 0, 0)
    End Sub

    Private Sub KeyHandler(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim LastCurrent As Integer
        If e.KeyCode = Keys.Left Then
            LastCurrent = GetLast(CurrentGame)
        ElseIf e.KeyCode = Keys.Right Then
            LastCurrent = GetNext(CurrentGame)
        ElseIf e.KeyCode = Keys.Escape Then
            LoadGames(True)
        Else
            Exit Sub
        End If
        If (LastCurrent <> CurrentGame) Then
            CurrentGame = LastCurrent
            ShowGlass()
            Me.Invalidate()
        End If
    End Sub

    Private Sub PreLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        pbxGlass.Height = Me.Height
        pbxGlass.Width = Me.Width - 310
        LoadGames()
        ShowGlass()
    End Sub

End Class
