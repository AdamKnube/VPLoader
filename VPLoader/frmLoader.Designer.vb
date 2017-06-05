<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoader
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pbxGlass = New System.Windows.Forms.PictureBox()
        CType(Me.pbxGlass, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbxGlass
        '
        Me.pbxGlass.Location = New System.Drawing.Point(0, 0)
        Me.pbxGlass.Margin = New System.Windows.Forms.Padding(0)
        Me.pbxGlass.Name = "pbxGlass"
        Me.pbxGlass.Size = New System.Drawing.Size(1, 1)
        Me.pbxGlass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxGlass.TabIndex = 0
        Me.pbxGlass.TabStop = False
        '
        'frmLoader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(568, 482)
        Me.ControlBox = False
        Me.Controls.Add(Me.pbxGlass)
        Me.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "frmLoader"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.pbxGlass, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pbxGlass As PictureBox
End Class
