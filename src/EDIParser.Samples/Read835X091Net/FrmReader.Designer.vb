<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReader
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
        Me.lstresults = New System.Windows.Forms.ListBox
        Me.BtnRead = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lstresults
        '
        Me.lstresults.FormattingEnabled = True
        Me.lstresults.Location = New System.Drawing.Point(12, 41)
        Me.lstresults.Name = "lstresults"
        Me.lstresults.Size = New System.Drawing.Size(487, 199)
        Me.lstresults.TabIndex = 0
        '
        'BtnRead
        '
        Me.BtnRead.Location = New System.Drawing.Point(156, 247)
        Me.BtnRead.Name = "BtnRead"
        Me.BtnRead.Size = New System.Drawing.Size(75, 34)
        Me.BtnRead.TabIndex = 1
        Me.BtnRead.Text = "Read"
        Me.BtnRead.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(257, 247)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 34)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Close"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(491, 32)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "This sample program is only a demonstration to show how one can easily use the ED" & _
            "I Parser.Net 835 file."
        '
        'FrmReader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(510, 287)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.BtnRead)
        Me.Controls.Add(Me.lstresults)
        Me.Name = "FrmReader"
        Me.Text = "Reading an EDI X12 835"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstresults As System.Windows.Forms.ListBox
    Friend WithEvents BtnRead As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
