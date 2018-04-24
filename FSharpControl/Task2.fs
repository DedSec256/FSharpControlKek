namespace FSharpControl

module Task2  =
    open System

    /// <summary>
    /// Возвращает строковый квадратик size * size
    /// </summary>
    let printKek size =

        if size <= 0 then raise(ArgumentException("WTF?"))

        let rec printKekRec(x, y, buffer) =
            match y with
            | y when y = size - 1 ->
                match x with
                | x when (x >= size * (size - 1) && x < (size * size - 1)) -> printKekRec(x + 1, y, buffer + "*")
                | _ -> buffer + "*"
            | y when y = 0 -> 
                match x with
                | x when (x >= 0 && x < size - 1) -> printKekRec(x + 1, y, buffer + "*")
                | _ -> printKekRec(x + 1, y + 1, buffer + "*\n")
            | y when (y > 0 && y < size - 1) ->
                match x with 
                | x when (x % size = 0) -> printKekRec(x + 1, y, buffer + "*")
                | x when x = (size * (y + 1) - 1) -> printKekRec(x + 1, y + 1, buffer + "*\n")
                | _ -> printKekRec(x + 1, y, buffer + " ")

        printKekRec(0, 0, "")

    /// Данная функция написана после кр, для внутреннего перфекционизма
    /// Просто потому что первая её версия - бяка ужасная
    /// А эта - читабельная, не использующая ничего готового
    /// Для этой функции также написаны тесты
    let printKekFinal size =

        if size <= 0 then raise(ArgumentException("WTF?"))

        let rec printKekRec(x, y, buffer) =
            match x with
            | x when (x = size && y = size) -> buffer + "*"
            | x when (x = 1) -> printKekRec(x + 1, y, buffer + "*")
            | x when (x = size) -> printKekRec(1, y + 1, buffer + "*\n")
            | _ -> printKekRec(x + 1, y, buffer + if (y = 1 || y = size) then "*" else " ")

        printKekRec(1, 1, "")