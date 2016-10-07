<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddInventory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddInventory))
        Me.InventoryQtyTB = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.InventoryDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.InventoryDateLbl = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.MemoTB = New System.Windows.Forms.TextBox()
        Me.InventoryAddBtn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ItemTB = New System.Windows.Forms.TextBox()
        Me.LotLbl = New System.Windows.Forms.Label()
        Me.LotTB = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CurrentAvailableTB = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'InventoryQtyTB
        '
        Me.InventoryQtyTB.Location = New System.Drawing.Point(104, 126)
        Me.InventoryQtyTB.Name = "InventoryQtyTB"
        Me.InventoryQtyTB.Size = New System.Drawing.Size(100, 20)
        Me.InventoryQtyTB.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(52, 126)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Quantity"
        '
        'InventoryDatePicker
        '
        Me.InventoryDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.InventoryDatePicker.Location = New System.Drawing.Point(104, 100)
        Me.InventoryDatePicker.Name = "InventoryDatePicker"
        Me.InventoryDatePicker.Size = New System.Drawing.Size(100, 20)
        Me.InventoryDatePicker.TabIndex = 1
        '
        'InventoryDateLbl
        '
        Me.InventoryDateLbl.AutoSize = True
        Me.InventoryDateLbl.Location = New System.Drawing.Point(24, 100)
        Me.InventoryDateLbl.Name = "InventoryDateLbl"
        Me.InventoryDateLbl.Size = New System.Drawing.Size(74, 13)
        Me.InventoryDateLbl.TabIndex = 3
        Me.InventoryDateLbl.Text = "InventoryDate"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(52, 152)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Memo"
        '
        'MemoTB
        '
        Me.MemoTB.Location = New System.Drawing.Point(104, 152)
        Me.MemoTB.Name = "MemoTB"
        Me.MemoTB.Size = New System.Drawing.Size(403, 20)
        Me.MemoTB.TabIndex = 3
        '
        'InventoryAddBtn
        '
        Me.InventoryAddBtn.Location = New System.Drawing.Point(141, 185)
        Me.InventoryAddBtn.Name = "InventoryAddBtn"
        Me.InventoryAddBtn.Size = New System.Drawing.Size(54, 23)
        Me.InventoryAddBtn.TabIndex = 5
        Me.InventoryAddBtn.Text = "Add"
        Me.InventoryAddBtn.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(201, 185)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(54, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(61, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Item"
        '
        'ItemTB
        '
        Me.ItemTB.Location = New System.Drawing.Point(104, 22)
        Me.ItemTB.Name = "ItemTB"
        Me.ItemTB.ReadOnly = True
        Me.ItemTB.Size = New System.Drawing.Size(100, 20)
        Me.ItemTB.TabIndex = 8
        Me.ItemTB.TabStop = False
        '
        'LotLbl
        '
        Me.LotLbl.AutoSize = True
        Me.LotLbl.Location = New System.Drawing.Point(66, 51)
        Me.LotLbl.Name = "LotLbl"
        Me.LotLbl.Size = New System.Drawing.Size(22, 13)
        Me.LotLbl.TabIndex = 11
        Me.LotLbl.Text = "Lot"
        '
        'LotTB
        '
        Me.LotTB.Location = New System.Drawing.Point(104, 48)
        Me.LotTB.Name = "LotTB"
        Me.LotTB.ReadOnly = True
        Me.LotTB.Size = New System.Drawing.Size(100, 20)
        Me.LotTB.TabIndex = 10
        Me.LotTB.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Current Available"
        '
        'CurrentAvailableTB
        '
        Me.CurrentAvailableTB.Location = New System.Drawing.Point(104, 74)
        Me.CurrentAvailableTB.Name = "CurrentAvailableTB"
        Me.CurrentAvailableTB.ReadOnly = True
        Me.CurrentAvailableTB.Size = New System.Drawing.Size(100, 20)
        Me.CurrentAvailableTB.TabIndex = 12
        Me.CurrentAvailableTB.TabStop = False
        '
        'AddInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(565, 242)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CurrentAvailableTB)
        Me.Controls.Add(Me.LotLbl)
        Me.Controls.Add(Me.LotTB)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ItemTB)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.InventoryAddBtn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.MemoTB)
        Me.Controls.Add(Me.InventoryDateLbl)
        Me.Controls.Add(Me.InventoryDatePicker)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.InventoryQtyTB)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddInventory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Inventory"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents InventoryQtyTB As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents InventoryDatePicker As DateTimePicker
    Friend WithEvents InventoryDateLbl As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents MemoTB As TextBox
    Protected Friend WithEvents InventoryAddBtn As Button
    Protected Friend WithEvents Button1 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ItemTB As TextBox
    Friend WithEvents LotLbl As Label
    Friend WithEvents LotTB As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents CurrentAvailableTB As TextBox
End Class
