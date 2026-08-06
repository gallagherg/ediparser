Imports System.Data
Imports System.Data.OleDb

' Minimal compatibility surface for the original sample's ADODB-style code.
' It uses System.Data.OleDb underneath and avoids a COM dependency.
Namespace Global.ADODB
    Public Enum CursorTypeEnum
        adOpenDynamic = 2
    End Enum

    Public Enum LockTypeEnum
        adLockOptimistic = 3
    End Enum

    Public NotInheritable Class Connection
        Friend ReadOnly InnerConnection As New OleDbConnection()

        Public Sub Open(connectionString As String)
            InnerConnection.ConnectionString = connectionString
            InnerConnection.Open()
        End Sub

        Public Sub Close()
            InnerConnection.Close()
        End Sub
    End Class

    Public NotInheritable Class Recordset
        Private _adapter As OleDbDataAdapter
        Private _table As DataTable
        Private _currentRow As DataRow
        Private _pendingNewRow As DataRow

        Public ReadOnly Property BOF As Boolean
            Get
                Return _currentRow Is Nothing AndAlso _pendingNewRow Is Nothing
            End Get
        End Property

        Public Sub Open(tableName As String, connection As Connection,
                        cursorType As CursorTypeEnum,
                        lockType As LockTypeEnum)
            _adapter = New OleDbDataAdapter("SELECT * FROM [" & tableName & "]", connection.InnerConnection)
            Dim builder As New OleDbCommandBuilder(_adapter)
            _table = New DataTable(tableName)
            _adapter.Fill(_table)
            If _table.Rows.Count > 0 Then _currentRow = _table.Rows(0)
        End Sub

        Public Sub AddNew()
            EnsureOpen()
            _pendingNewRow = _table.NewRow()
            _currentRow = _pendingNewRow
        End Sub

        Public Sub Update()
            EnsureOpen()
            If _pendingNewRow IsNot Nothing Then
                _table.Rows.Add(_pendingNewRow)
                _pendingNewRow = Nothing
            End If
            _adapter.Update(_table)
        End Sub

        Public Sub Close()
            _table = Nothing
            _adapter = Nothing
            _currentRow = Nothing
            _pendingNewRow = Nothing
        End Sub

        Default Public ReadOnly Property Item(columnName As String) As Field
            Get
                EnsureOpen()
                If _currentRow Is Nothing Then
                    Throw New InvalidOperationException("Call AddNew before assigning fields to an empty recordset.")
                End If
                Return New Field(_currentRow, columnName)
            End Get
        End Property

        Private Sub EnsureOpen()
            If _table Is Nothing OrElse _adapter Is Nothing Then
                Throw New InvalidOperationException("The recordset is not open.")
            End If
        End Sub
    End Class

    Public NotInheritable Class Field
        Private ReadOnly _row As DataRow
        Private ReadOnly _columnName As String

        Friend Sub New(row As DataRow, columnName As String)
            _row = row
            _columnName = columnName
        End Sub

        Public Property Value As Object
            Get
                Return _row(_columnName)
            End Get
            Set(value As Object)
                _row(_columnName) = If(value, DBNull.Value)
            End Set
        End Property
    End Class
End Namespace
