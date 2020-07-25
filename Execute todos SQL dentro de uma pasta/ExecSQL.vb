Imports System.Data.SqlClient

Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports System.Text

Public Class ExecSQL
    '<?xml version="1.0"?>
    '<configuration>

    '  <startup useLegacyV2RuntimeActivationPolicy="true">
    '      <supportedRuntime version="v4.0"/>
    '    </startup>
    '</configuration>

    Private Parou As String = String.Empty

    Private SQLConn As SqlConnection
    Private Server As Server

    Private Path As String
    Private Progresso As ExecSQLProgressoDelegate

    Delegate Sub ExecSQLProgressoDelegate(Progresso As String)

    Public Sub New(ByVal Path As String, ByVal StringConexao As String, Progresso As ExecSQLProgressoDelegate)
        Me.SQLConn = New SqlConnection(StringConexao)
        Me.SQLConn.Open()

        Me.Server = New Server(New ServerConnection(SQLConn))

        Me.Path = Path
        Me.Progresso = Progresso
    End Sub

    Public Sub Continuar()
        If String.IsNullOrEmpty(Me.Parou) Then
            Throw New Exception("Não existe processamento em andamento")
        Else
            Me.ExecutarArquivo(Me.Parou)

            Dim ComecarApos = Me.Parou
            Me.Parou = String.Empty

            Me.Executar(ComecarApos)
        End If
    End Sub

    Public Sub Pular()
        If String.IsNullOrEmpty(Me.Parou) Then
            Throw New Exception("Não existe processamento em andamento")
        Else
            Dim ComecarApos = Me.Parou
            Me.Parou = String.Empty

            Me.Executar(ComecarApos)
        End If
    End Sub

    ReadOnly Property Parado As Boolean
        Get
            Return Not String.IsNullOrEmpty(Me.Parou)
        End Get
    End Property

    Private Function OrdemExecucao(ByVal FileName As String) As Integer
        Dim ArquivoSomente As String = System.IO.Path.GetFileNameWithoutExtension(FileName)

        Dim PosicaoPonto As Integer = ArquivoSomente.IndexOf(".")

        Dim Resultado As String
        If PosicaoPonto > 0 Then
            Resultado = ArquivoSomente.Substring(0, PosicaoPonto)
        Else
            Throw New Exception("Não existe ponto depois da ordem do arquivo a ser executado" & vbCrLf & vbCrLf & FileName)
        End If

        Return Resultado
    End Function

    Public Sub Executar(Optional ByVal ComecarApos As String = "")

        If Not String.IsNullOrEmpty(Me.Parou) Then
            Throw New Exception("Chame o método Continuar ou Pular")
        End If

        Dim SQLFiles = From FileName In System.IO.Directory.GetFiles(Me.Path) Where FileName.EndsWith(".sql") Order By OrdemExecucao(FileName)

        For Each SQL As String In SQLFiles

            'Se SQL é alfabeticamente maior que Parou
            If SQL.CompareTo(ComecarApos) > 0 Then

                If ComecarApos = SQL Then Continue For
                If Me.Parou = SQL Then Continue For

                Me.Progresso(SQL)
                Application.DoEvents()

                Me.ExecutarArquivo(SQL)
                Application.DoEvents()

                System.Threading.Thread.Sleep(1000)
            End If
        Next

        Me.Progresso("Concluído")
        Me.Parou = String.Empty
    End Sub

    Private Sub ExecutarArquivo(ByVal FilePath As String)

        Try
            Dim FullSQL As String = System.IO.File.ReadAllText(FilePath, System.Text.Encoding.Default)
            Server.ConnectionContext.ExecuteNonQuery(FullSQL)

        Catch ex As Exception
            Me.Parou = FilePath
            Throw ex
        End Try
    End Sub

    'Só pode ser usado se não houver DDL
    Private Sub ExecutarArquivoBeginTry(ByVal FilePath As String)

        Try

            Dim FullSQL As String = System.IO.File.ReadAllText(FilePath)
            FullSQL = FullSQL.Replace(vbCrLf & "go" & vbCrLf, vbCrLf & "|" & vbCrLf)
            FullSQL = FullSQL.Replace(vbCrLf & "Go" & vbCrLf, vbCrLf & "|" & vbCrLf)
            FullSQL = FullSQL.Replace(vbCrLf & "gO" & vbCrLf, vbCrLf & "|" & vbCrLf)
            FullSQL = FullSQL.Replace(vbCrLf & "GO" & vbCrLf, vbCrLf & "|" & vbCrLf)


            Dim x As New StringBuilder
            x.AppendLine("begin tran")


            Dim batches() As String = FullSQL.Split("|")

            For Each b As String In batches

                If Not String.IsNullOrEmpty(b.Trim) Then
                    x.AppendLine("begin try")
                    x.AppendLine("print '" & Now.ToString & "'")

                    x.AppendLine(b.Replace(vbCrLf, vbCrLf & "   "))

                    x.AppendLine("end try")
                    x.AppendLine("begin catch")
                    x.AppendLine("  declare @mensagem_erro_asdfg varchar(1000)")
                    x.AppendLine("  set @mensagem_erro_asdfg = convert(varchar, ERROR_NUMBER()) + ': ' +  ERROR_MESSAGE()")
                    x.AppendLine("  raiserror(@mensagem_erro_asdfg, 15, 1)")
                    x.AppendLine("  rollback tran")
                    x.AppendLine("end catch")
                    x.AppendLine("go")
                End If
            Next

            x.AppendLine("IF @@TRANCOUNT > 0 commit tran")

            ''DEBUG?
            'System.IO.File.WriteAllText("C:\sql_modificado.sql", x.ToString())
            'AbrirArquivoProgramaPadrao("C:\sql_modificado.sql")
            'If MsgBox("Continuar?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            '    Exit Sub
            'End If

            Server.ConnectionContext.ExecuteNonQuery(x.ToString)
        Catch ex As Exception
            Me.Parou = FilePath
            Throw ex
        End Try
    End Sub


    Private Sub AbrirArquivoProgramaPadrao(ByVal Arquivo As String)
        Dim p As New System.Diagnostics.Process
        Dim s As New System.Diagnostics.ProcessStartInfo(Arquivo)
        s.UseShellExecute = True
        s.WindowStyle = ProcessWindowStyle.Normal
        p.StartInfo = s
        p.Start()
    End Sub

End Class
