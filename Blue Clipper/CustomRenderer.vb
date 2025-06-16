
Public NotInheritable Class CustomRenderer : Inherits ToolStripProfessionalRenderer

    Protected Overrides Sub OnRenderArrow(e As ToolStripArrowRenderEventArgs)
        Dim arrowBounds = e.ArrowRectangle
        ' Calculate arrow
        Dim midY As Integer = arrowBounds.Top + arrowBounds.Height \ 2
        Dim arrowHeight As Integer = arrowBounds.Height \ 3
        Dim arrowWidth As Integer = arrowBounds.Width \ 3
        ' Draw an arrow with the specified color
        e.Graphics.FillPolygon(New SolidBrush(e.Item.ForeColor), New Point() {
             New Point(arrowBounds.Right - arrowWidth, midY - arrowHeight \ 2),
             New Point(arrowBounds.Right, midY),
             New Point(arrowBounds.Right - arrowWidth, midY + arrowHeight \ 2)
        })
    End Sub

    Public Sub New()
        MyBase.New(New CustomColorTable())
    End Sub

End Class



Public NotInheritable Class CustomColorTable
    Inherits ProfessionalColorTable

    Public Overrides ReadOnly Property ToolStripDropDownBackground As Color
        Get
            Return Color.Black
        End Get
    End Property

    Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
        Get
            Return Color.FromArgb(&HFF7AB2F4)
        End Get
    End Property
    Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color
        Get
            Return Color.Black
        End Get
    End Property

    Public Overrides ReadOnly Property ImageMarginGradientEnd As Color
        Get
            Return Color.Black
        End Get
    End Property

End Class
