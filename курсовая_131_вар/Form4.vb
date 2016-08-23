Public Class Form4

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form5.Show()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        c = vvod(TextBox3)
        d = vvod(TextBox4)
        Eps = 0.004
        ParabolaExtremum(c, d, Eps, x_min)
        'Dixotomia(c, d, Eps, x_min)
        vivod(x_min, TextBox1)
        vivod(Func2(x_min), TextBox2)

    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        End
    End Sub

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
