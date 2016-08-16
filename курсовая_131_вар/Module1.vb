Option Strict On
Option Explicit On
Imports System.Math
Module Module1
    Public a As Double 'Начало отрезка интегрирования
    Public b As Double 'Конец отрезка интегрирования
    Public c As Double 'Начало отрезка поиска точки минимума функции f2
    Public d As Double 'Конец отрезка поиска точки минимума функции f2
    Public t As Double 'Начало отрезка нахождения корня уравнения f1
    Public p As Double 'Конец отрезка нахождения корня уравнения f1
    Public Eps As Double ' Погрешность измерений
    Public koren1 As Double 'Решение f1(x) методом половинного деления 
    Public koren2 As Double 'Решение f2(x) методом Ньютона
    Public xk As Double ' Более точное значение из koren1 и koren2 для дальнейших вычислений
    Public x_min As Double 'абсцисса точки минимума функции f2(x)
    Public S As Double ' Площадь, огранниченная фунциями f1(x) и f2(x)
    ' вывод результатов в Listbox
    Sub vivodlist(ByVal z As Double, ByVal LB As ListBox)
        LB.Items.Add(Format(z, "0.0000"))
    End Sub

    ' Ввод данных в Textbox:
    Function vvod(ByVal t As TextBox) As Double
        Return CSng(Val(t.Text))
    End Function
    'Вывод данных в Textbox
    Sub vivod(ByVal x As Double, ByVal T As TextBox)
        T.Text = Format(x, "0.0000000")
    End Sub
    Sub vivod(ByVal x As Integer, ByRef T As TextBox)
        T.Text = CStr(x)
    End Sub
    'Вывод целочисленных результатов в Listbox:
    Sub vivodlistINT(ByVal z As Integer, ByVal LB As ListBox)
        LB.Items.Add(Format(z, "0"))
    End Sub
    'Функция f1(x), у которой неободимо найти корень
    Function Func1(ByVal x As Double) As Double
        Func1 = x * Sqrt(x + 4) - 2
    End Function
    ' Функция f2(x), у которой необходима найти x_min
    Function Func2(ByVal x As Double) As Double
        Func2 = Exp(x) - 5 * x
    End Function
    'Производная функции f1(x), необходимая для решения уравнения f1(x)=0 методом Ньютона
    Function F1proizv(ByVal x As Double) As Double
        F1proizv = Sqrt(x + 4) + x / (2 * Sqrt(x + 4))
    End Function
    'Функция, необходимая для вычисления площади области, ограниченной функциями f1(x) и f2(x)
    Function Func3(ByVal x As Double) As Double
        Func3 = Func1(x) - Func2(x)
    End Function
    ' Функция необходимая для вычисления F(x): 
    Function F(ByVal x As Double) As Double
        Return (Func1(x) + Func2(x)) / (Xk + x_min)
    End Function

    ' Нахождение решения нелинейного уравнения методом половинного деления
    Sub Reshenie(ByVal a As Double, ByVal b As Double, ByVal EE As Double, ByRef solution As Double)
        Dim n As Integer
        Dim c As Double
        n = 0
        c = (a + b) / 2
        Do
            n = n + 1
            If Func1(c) * Func1(b) < 0 Then
                a = c
            Else
                b = c
            End If
            vivodlist(n, Form2.ListBox1)
            vivodlist(a, Form2.ListBox2)
            vivodlist(Func1(a), Form2.ListBox3)
            vivodlist(b, Form2.ListBox4)
            vivodlist(Func1(b), Form2.ListBox5)
            c = (a + b) / 2
            vivodlist(c, Form2.ListBox6)
            vivodlist(Func1(c), Form2.ListBox7)
        Loop Until (Abs(b - a) <= EE)
        solution = c
    End Sub
    ' Нахождение решения нелинейного уравнения методом Ньютона
    Sub Nuton(ByVal a As Double, ByVal Eps As Double, ByRef solution As Double)
        Dim t, x, f2 As Double ' x - начальное приближение к корню
        Dim n As Integer
        x = b
        n = 0
        Do
            n = n + 1
            t = Func1(x) / F1proizv(x) 'Вычисление поправки к значению корня
            x = x - t
            f2 = Func1(x)
            vivodlist(n, Form2.ListBox8)
            vivodlist(x, Form2.ListBox9)
            vivodlist(Func1(x), Form2.ListBox10)
        Loop Until t < Eps
        solution = x
    End Sub
    Sub choice(ByVal koren1 As Double, ByVal koren2 As Double)
        If Abs(koren1) < Abs(koren2) Then
            xk = koren1
        Else
            xk = koren2
        End If
    End Sub

    ' Подпрограмма для одномерной оптимизации функции Func1 методом Дихотомии
    Sub Dixotomia(ByVal a As Double, ByVal b As Double, ByVal Eps As Double, _
                  ByRef x_min As Double) ' x_min - минимальное значение функции на [a;b]
        Dim d, x1, x2, delta_n As Double
        Randomize()
        Dim n As Integer
        n = 0
        d = Eps / 2 * Rnd() ' d - параметр метода
        Do
            n = n + 1
            x1 = (a + b) / 2 - d
            x2 = (a + b) / 2 + d
            delta_n = b - a
            vivodlistINT(n, Form4.ListBox1)
            vivodlist(a, Form4.ListBox2)
            vivodlist(b, Form4.ListBox3)
            vivodlist(x1, Form4.ListBox4)
            vivodlist(x2, Form4.ListBox5)
            vivodlist(Func2(x1), Form4.ListBox6)
            vivodlist(Func2(x2), Form4.ListBox7)
            vivodlist(delta_n, Form4.ListBox8)
            If Func2(x1) > Func2(x2) Then
                a = x1
            Else
                b = x2
            End If
        Loop Until b - a <= Eps
        x_min = (a + b) / 2
    End Sub
    ' Подпрограмма - функция для вычисления интеграла методом Симпсона
    'Параметр T определяет, интеграл какой функции будет расчитываться. Если Т=1, то расчитывается функция 
    ' Func3(x) (необходимая для вычисления площади области, ограниченной функциями f1(x) и f2(x)), 
    ' в противном услучае - функция, необходимая для вычисления интеграла F(x)
    Sub Simpson(ByVal a As Double, ByVal b As Double, ByVal Eps As Double, ByRef S As Double, ByVal T As Byte)
        Dim h, s1, c, x As Double
        Dim n As Integer
        n = 2
        h = (b - a) / n ' h - шаг интегрирования
        If T = 1 Then
            S = (Func3(a) + Func3((a + b) / 2) + Func3(b)) * h / 3
        Else
            S = (F(a) + F((a + b) / 2) + F(b)) * h / 3

        End If
        Do
            n = 2 * n
            h = (b - a) / n
            s1 = S : c = 4 : x = a
            If T = 1 Then
                S = Func3(a) + Func3(b)
            Else
                S = F(a) + F(b)
            End If

            For i = 1 To n - 1
                x = x + h

                If T = 1 Then
                    S = S + c * Func3(x)
                Else
                    S = S + c * F(x)
                End If
                c = 6 - c
            Next i
            S = S * h / 3
            If T = 1 Then
                vivodlist(n, Form5.ListBox1)
                vivodlist(h, Form5.ListBox2)
                vivodlist(S, Form5.ListBox3)
            Else
                vivodlist(n, Form5.ListBox4)
                vivodlist(h, Form5.ListBox5)
                vivodlist(S, Form5.ListBox6)
            End If
        Loop Until Abs(S - s1) / 15 < Eps
    End Sub
End Module
