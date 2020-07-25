<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.pasta = New System.Windows.Forms.TextBox()
        Me.banco = New System.Windows.Forms.TextBox()
        Me.andamento = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.servidor = New System.Windows.Forms.TextBox()
        Me.usuario = New System.Windows.Forms.TextBox()
        Me.senha = New System.Windows.Forms.TextBox()
        Me.Integrado = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1158, 38)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Executar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'pasta
        '
        Me.pasta.Location = New System.Drawing.Point(16, 38)
        Me.pasta.Name = "pasta"
        Me.pasta.Size = New System.Drawing.Size(1136, 20)
        Me.pasta.TabIndex = 1
        Me.pasta.Text = "g:\sqls\"
        '
        'banco
        '
        Me.banco.Location = New System.Drawing.Point(583, 12)
        Me.banco.Name = "banco"
        Me.banco.Size = New System.Drawing.Size(183, 20)
        Me.banco.TabIndex = 2
        Me.banco.Text = "eye"
        '
        'andamento
        '
        Me.andamento.AutoSize = True
        Me.andamento.Location = New System.Drawing.Point(13, 105)
        Me.andamento.Name = "andamento"
        Me.andamento.Size = New System.Drawing.Size(39, 13)
        Me.andamento.TabIndex = 3
        Me.andamento.Text = "Label1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(721, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(418, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Gastei 1h de pesquisa + 3 horas de codificação + 2h de teste e ajustes. Totalizan" & _
    "do 6h"
        Me.Label1.Visible = False
        '
        'servidor
        '
        Me.servidor.Location = New System.Drawing.Point(16, 12)
        Me.servidor.Name = "servidor"
        Me.servidor.Size = New System.Drawing.Size(183, 20)
        Me.servidor.TabIndex = 5
        Me.servidor.Text = "localhost"
        '
        'usuario
        '
        Me.usuario.Location = New System.Drawing.Point(205, 12)
        Me.usuario.Name = "usuario"
        Me.usuario.Size = New System.Drawing.Size(183, 20)
        Me.usuario.TabIndex = 6
        Me.usuario.Text = "sa"
        '
        'senha
        '
        Me.senha.Location = New System.Drawing.Point(394, 12)
        Me.senha.Name = "senha"
        Me.senha.Size = New System.Drawing.Size(183, 20)
        Me.senha.TabIndex = 7
        '
        'Integrado
        '
        Me.Integrado.AutoSize = True
        Me.Integrado.Checked = True
        Me.Integrado.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Integrado.Location = New System.Drawing.Point(772, 14)
        Me.Integrado.Name = "Integrado"
        Me.Integrado.Size = New System.Drawing.Size(115, 17)
        Me.Integrado.TabIndex = 8
        Me.Integrado.Text = "Integrated Security"
        Me.Integrado.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1245, 152)
        Me.Controls.Add(Me.Integrado)
        Me.Controls.Add(Me.senha)
        Me.Controls.Add(Me.usuario)
        Me.Controls.Add(Me.servidor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.andamento)
        Me.Controls.Add(Me.banco)
        Me.Controls.Add(Me.pasta)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents pasta As System.Windows.Forms.TextBox
    Friend WithEvents banco As System.Windows.Forms.TextBox
    Friend WithEvents andamento As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents servidor As System.Windows.Forms.TextBox
    Friend WithEvents usuario As System.Windows.Forms.TextBox
    Friend WithEvents senha As System.Windows.Forms.TextBox
    Friend WithEvents Integrado As System.Windows.Forms.CheckBox

End Class
