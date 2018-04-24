namespace Tests

open System
open FSharpControl.Task2
open Microsoft.VisualStudio.TestTools.UnitTesting

module Assert = 
    /// <summary>
    /// Проверяет, выдаёт ли функция исключение заданного типа 'a
    /// </summary>
    /// <param name="f"> Функция, которая должна выдать исключение </param>
    let Throws<'a> f =
        let mutable wasThrown = false
        try
            f()
        with
        | ex -> Assert.AreEqual(ex.GetType(), typedefof<'a>, (sprintf "Actual Exception: %A" ex)); wasThrown <- true
        Assert.IsTrue(wasThrown, "No exception thrown")


[<TestClass>]
type Task2TestClass () =

    [<TestMethod>]
    member this.Second0 () =
        let res = printKek 3;

        Assert.AreEqual("***\n* *\n***", res)

    [<TestMethod>]
    member this.Second1 () =
        let res = printKek 4;

        Assert.AreEqual("****\n*  *\n*  *\n****", res)

    [<TestMethod>]
    member this.Second11 () =
        let res = printKek 5;

        Assert.AreEqual("*****\n" +
                        "*   *\n" +
                        "*   *\n" +
                        "*   *\n" +
                        "*****", res)

    [<TestMethod>]
    member this.Second2 () =
        let res = printKek 1;

        Assert.AreEqual("*", res)

    [<TestMethod>]
    member this.Second3 () =
        Assert.Throws<ArgumentException>(fun () -> printKek -1 |> ignore)

    
