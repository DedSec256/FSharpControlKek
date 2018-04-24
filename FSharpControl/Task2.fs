﻿namespace FSharpControl

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