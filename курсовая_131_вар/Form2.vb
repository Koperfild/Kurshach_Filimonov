Public Class Form2
    'Ввод данных
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        a = vvod(TextBox1)
        b = vvod(TextBox2)
        Eps = vvod(TextBox3)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Очищаем Listbox от результатов предыдущих вычислений
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()

        MullerMethod(a, b, Eps, koren1)
        'Reshenie(a, b, Eps, koren1)
        vivod(koren1, TextBox6)
        vivod(Func1(koren1), TextBox7)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ListBox8.Items.Clear()
        ListBox9.Items.Clear()
        ListBox10.Items.Clear()
        'MullerMethod(a, b, Eps, koren2)
        SecantMethod(a, b, Eps, koren2)
        'Nuton(t, Eps, koren2)
        vivod(koren2, TextBox8)
        vivod(Func1(koren2), TextBox9)
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        choice(koren1, koren2)
        Form4.Show()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Form3.Show()
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        End
    End Sub
End Class
