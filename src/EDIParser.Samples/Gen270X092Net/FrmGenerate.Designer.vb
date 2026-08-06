<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGenerate
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
        Me.BtnGenerate = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtEdiFile = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'BtnGenerate
        '
        Me.BtnGenerate.Location = New System.Drawing.Point(185, 236)
        Me.BtnGenerate.Name = "BtnGenerate"
        Me.BtnGenerate.Size = New System.Drawing.Size(105, 31)
        Me.BtnGenerate.TabIndex = 0
        Me.BtnGenerate.Text = "Generate"
        Me.BtnGenerate.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(176, 24)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "EDI file:"
        '
        'txtEdiFile
        '
        Me.txtEdiFile.Location = New System.Drawing.Point(22, 86)
        Me.txtEdiFile.Multiline = True
        Me.txtEdiFile.Name = "txtEdiFile"
        Me.txtEdiFile.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtEdiFile.Size = New System.Drawing.Size(576, 128)
        Me.txtEdiFile.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(22, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(584, 40)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "This sample program is only a demonstration to show how easily one can use the ED" & _
            "IParser.NET component to generate an EDI X12 270 file."
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(318, 236)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(104, 32)
        Me.cmdClose.TabIndex = 8
        Me.cmdClose.Text = "Close"
        '
        'FrmGenerate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 294)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtEdiFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnGenerate)
        Me.Name = "FrmGenerate"
        Me.Text = "Generating an EDI X12 270 "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnGenerate As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEdiFile As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
End Class
