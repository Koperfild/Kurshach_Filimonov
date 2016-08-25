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

    '/////////////////////////////////////////////////////////////////////////
    '//~~~~~~~~~~~~~~~~~~~~DELETE!!!!!!!!!!!!!!!!!!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    '////////////////////////////////////////////////////////////////////////////
    'Производная функции f1(x), необходимая для решения уравнения f1(x)=0 методом Ньютона
    Function F1proizv(ByVal x As Double) As Double
        F1proizv = Sqrt(x + 4) + x / (2 * Sqrt(x + 4))
    End Function
    '/////////////////////////////////////////////////////////////////////////////
    Function F2proizv(ByVal x As Double) As Double
        F2proizv = Exp(x) - 5
    End Function

    'Приближение производной f1(x), необходимая в методе секущих SecantMethod
    Function F1DerivativeApproximation(ByVal x1 As Double, ByVal x2 As Double) As Double
        F1DerivativeApproximation = Func1(x2) * (x2 - x1) / (Func1(x2) - Func1(x1))
    End Function


    'Функция, необходимая для вычисления площади области, ограниченной функциями f1(x) и f2(x)
    Function Func3(ByVal x As Double) As Double
        Func3 = Func1(x) - Func2(x)
    End Function
    ' Функция необходимая для вычисления F(x): 
    Function F(ByVal x As Double) As Double
        Return (Func1(x) + Func2(x)) / (Xk + x_min)
    End Function

    ' Нахождение решения нелинейного уравнения методом секущих
    Sub SecantMethod(ByVal a As Double, ByVal b As Double, ByVal Eps As Double, ByRef solution As Double)
        Dim t, x0, x1, f2 As Double ' x - начальное приближение к корню
        Dim n As Integer
        x0 = a 'первое начальное приближение
        x1 = b 'второе начальное приближение
        n = 0
        Do
            n = n + 1
            t = F1DerivativeApproximation(x0, x1) 'Вычисление поправки к значению корня
            x0 = x1
            x1 = x1 - t
            'f2 = Func1(x)
            vivodlist(n, Form2.ListBox8)
            vivodlist(x1, Form2.ListBox9)
            vivodlist(Func1(x1), Form2.ListBox10)
        Loop Until Abs(x1 - x0) < Eps
        solution = x1
    End Sub

    ' Нахождение решения нелинейного уравнения методом парабол (метод Мюллера)
    Sub MullerMethod(ByVal left As Double, ByVal right As Double, ByVal Eps As Double, ByRef solution As Double)
        Dim x0 = left, x2 = right
        Dim x3 As Double
        Dim x1 = (left + right) / 2.0
        Dim q As Double
        Dim A, B, C As Double 'Коэф-ты в уравнении интерполирующей параболы
        Dim MaxABSDenominator As Double
        Dim count = 0

        While Abs(x2 - x0) > Eps
            count += 1
            q = (x2 - x1) / (x1 - x0)
            A = q * Func1(x2) - q * (1 + q) * Func1(x1) + q * q * Func1(x0)
            B = (2 * q + 1) * Func1(x2) - (1 + q) * (1 + q) * Func1(x1) + q * q * Func1(x0)
            C = (1 + q) * Func1(x2)

            If B * B - 4 * A * C < 0 Then
                Return
            End If
            MaxABSDenominator = Math.Max(Abs(B + Math.Sqrt(B * B - 4 * A * C)), Abs(B - Math.Sqrt(B * B - 4 * A * C)))

            x3 = x2 - (x2 - x1) * (2 * C / MaxABSDenominator)
            x0 = x1
            x1 = x2
            x2 = x3
            vivodlist(count, Form2.ListBox1)
            vivodlist(x1, Form2.ListBox2)
            vivodlist(Func1(x1), Form2.ListBox3)
        End While
        solution = x2
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
            xk = koren2
        Else
            xk = koren1
        End If
    End Sub

    'Метод парабол нахождения локального экстремума (минимума)
    ' http://dssp.petrsu.ru/p/tutorial/meth_calc/files/12.shtml 
    Sub ParabolaExtremum(ByVal a As Double, ByVal b As Double, ByVal Eps As Double,
                  ByRef x_min As Double) ' x_min - минимальное значение функции на [a;b]
        Dim h, x0, x1, tempX, denominator As Double
        Dim numerator, temp1, temp2, temp3 As Double
        Dim counter = 0
        'Проверяем функцию на убывание (f(xk+1) < f(xk))
        If F1proizv(a) >= 0 Then 'Функция увеличивается
            Return
        End If
        h = 0.005
        x0 = a
        Do
            counter += 1
            x1 = x0 - h / 2 * ((Func2(x0 + h) - Func2(x0 - h)) / (Func2(x0 + h) - 2 * Func2(x0) + Func2(x0 - h)))

            'If denominator <= 0 Then Не стал делать проверку
            x1 = RecursiveLesseningStep(x1, x0)
            x0 = x1
            vivodlist(counter, Form4.ListBox1)
            vivodlist(x1, Form4.ListBox2)
            vivodlist(Func2(x1), Form4.ListBox3)
        Loop Until Abs(F2proizv(x1)) < Eps
        x_min = x1
    End Sub

    ''' <summary>
    ''' Finds next root approximation. Used in ParabolaExtremum
    ''' </summary>
    ''' <param name="x1"></param>
    ''' <param name="x0"></param>
    ''' <returns>x(k+1)</returns>
    Function RecursiveLesseningStep(ByVal x1 As Double, ByVal x0 As Double) As Double
        If Func2(x1) <= Func2(x0) Then
            Return x1
        End If
        x1 = x0 + (x1 - x0) / 2
        Return RecursiveLesseningStep(x1, x0)
    End Function

    ' Подпрограмма - функция для вычисления интеграла методом Симпсона
    'Параметр T определяет, интеграл какой функции будет расчитываться. Если Т=1, то расчитывается функция 
    ' Func3(x) (необходимая для вычисления площади области, ограниченной функциями f1(x) и f2(x)), 
    ' в противном услучае - функция, необходимая для вычисления интеграла F(x)

    'Менять a->c, b->d, a и b убрать
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

            End If
        Loop Until Abs(S - s1) / 15 < Eps
    End Sub

    Sub TrapeziumIntegration(ByVal c As Double, ByVal d As Double, ByVal Eps As Double, ByRef S As Double, ByVal T As Byte)
        Dim h, s1, x As Double 's1 - площадь предыдущего интегрирования, S - площадь текущего (в 2 раза больше отрезков разбиения)
        Dim n As Integer
        Dim counter As Integer
        n = 2 'начальное число разбиений отрезка интегрирования
        h = Abs(c - d) / n ' h - шаг интегрирования
        S = h * (Func3(c) + Func3(d)) / 2 'Первоначальное приближение интеграла
        counter = 0
        Do
            counter += 1
            n = n * 2
            h = Abs(c - d) / n
            s1 = S : x = c
            S = 0
            For i = 0 To n
                If i = 0 Or i = n Then
                    S += Func3(c + i * h) / 2
                Else
                    S += Func3(c + i * h)
                End If
            Next
            S *= h 'Домножаем сумму на шаг
            vivodlist(counter, Form5.ListBox1)
            vivodlist(h, Form5.ListBox2)
            vivodlist(S, Form5.ListBox3)
        Loop Until Abs(S - s1) / 3 < Eps
    End Sub
End Module
