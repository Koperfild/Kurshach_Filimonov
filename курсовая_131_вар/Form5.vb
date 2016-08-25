Public Class Form5
    'Ввод данных
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        a = vvod(TextBox3)
        b = vvod(TextBox4)
        Simpson(a, b, Eps, S, t)
        'vivod(x_min, TextBox4)
        'vivod(xk, TextBox3)
        vivod(S, TextBox6)
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        t = 1
        xk = vvod(TextBox3)
        x_min = vvod(TextBox4)
        'Simpson(xk, x_min, Eps, S, t)
        TrapeziumIntegration(xk, x_min, Eps, S, t)
        vivod(S, TextBox5)
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        vivod(xk, TextBox3)
        vivod(x_min, TextBox4)
    End Sub
End Class
