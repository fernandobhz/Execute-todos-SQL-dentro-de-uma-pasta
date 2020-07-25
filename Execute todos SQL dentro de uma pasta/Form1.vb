
Public Class Form1

    Private TextoBotao As String

    Sub New()
        Me.InitializeComponent()

        Me.TextoBotao = Me.Button1.Text
        Me.andamento.Text = ""
    End Sub

    Sub Progresso(s As String)
        Me.andamento.Text = s
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim Exec As ExecSQL

        If Not IsNothing(Me.Tag) Then
            Exec = Me.Tag            

            UINormal
        Else
            Dim builder As New System.Data.SqlClient.SqlConnectionStringBuilder
            builder.DataSource = Me.servidor.Text
            builder.InitialCatalog = Me.banco.Text

            If Me.Integrado.Checked Then
                builder.IntegratedSecurity = True
            Else
                builder.UserID = Me.usuario.Text
                builder.Password = Me.senha.Text
            End If


            Exec = New ExecSQL(Me.pasta.Text, builder.ConnectionString, AddressOf Me.Progresso)
        End If

        Try
            If Exec.Parado Then

                If MsgBox("Já executou o arquivo que deu problema?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Exec.Pular()
                Else
                    Exec.Continuar()
                End If
            Else
                Exec.Executar()
            End If
        Catch ex As Exception
            Dim Msg As String = "Execução do arquivo: " & Me.andamento.Text & " falhou" & vbCrLf & vbCrLf & ex.Message

            If Not IsNothing(ex.InnerException) Then Msg = Msg & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf

            Msg = Msg & vbCrLf & vbCrLf & "Deseja abrir o arquivo para consertar o erro?"

            If MsgBox(Msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.Tag = Exec
                UIParou()

                AbrirArquivoProgramaPadrao(Me.andamento.Text)
                Exit Sub
            End If

        End Try

        UINormal()
        Me.andamento.Text = ""

        MsgBox("Concluído")
    End Sub

    Sub UINormal()
        Me.banco.Enabled = True
        Me.pasta.Enabled = True
        Me.Button1.Text = Me.TextoBotao

        Me.Tag = Nothing
    End Sub

    Sub UIParou()
        Me.banco.Enabled = False
        Me.pasta.Enabled = False

        Me.Button1.Text = "Continuar"
    End Sub

    Private Sub AbrirArquivoProgramaPadrao(ByVal Arquivo As String)
        Dim p As New System.Diagnostics.Process
        Dim s As New System.Diagnostics.ProcessStartInfo(Arquivo)
        s.UseShellExecute = True
        s.WindowStyle = ProcessWindowStyle.Normal
        p.StartInfo = s
        p.Start()
    End Sub

    Private Sub Integrado_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Integrado.CheckedChanged
        IntegradoChange()
    End Sub

    Sub IntegradoChange()
        If Me.Integrado.Checked Then
            Me.usuario.Enabled = False
            Me.senha.Enabled = False
        Else
            Me.usuario.Enabled = True
            Me.senha.Enabled = True
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        IntegradoChange()
    End Sub
End Class


'use banco_teste
'go

'begin try
'begin tran

'insert into teste(cd_teste, nm_teste) values (1, 'abc1')
'insert into teste(cd_teste, nm_teste) values (2, 'abc2')

'go

'insert into teste(cd_teste, nm_teste) values (2/0, 'abc2')


'insert into teste(cd_teste, nm_teste) values (3, 'abc3')
'insert into teste(cd_teste, nm_teste) values (4, 'abc4')

'commit tran
'end try

'begin catch
'	declare @xyzasdfg varchar(1000)
'	set @xyzasdfg = convert(varchar, ERROR_NUMBER()) + ': ' +  ERROR_MESSAGE()
'	raiserror(@xyzasdfg, 15, 1)
'	rollback tran
'end catch

'go
