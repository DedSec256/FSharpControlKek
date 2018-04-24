namespace Tests

open System
open FSharpControl.Task3
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type Task3TestClass () =

    [<TestMethod>]
    member this.QueueUsingScenario () =
        let queue = PriorityQueue<int>()
        queue.Enqueue(1, 1).Enqueue(10, 10) |> ignore //второй приоритет выше
        Assert.AreEqual("10 <- 1 <- (null)", queue.ToString())

        queue.Enqueue(1, 1) |> ignore
        Assert.AreEqual("10 <- 1 <- 1 <- (null)", queue.ToString())

        queue.Enqueue(9, 9).Enqueue(11, 11) |> ignore
        Assert.AreEqual("11 <- 10 <- 9 <- 1 <- 1 <- (null)", queue.ToString())

        queue.Enqueue(9, 9).Enqueue(11, 11) |> ignore
        Assert.AreEqual("11 <- 11 <- 10 <- 9 <- 9 <- 1 <- 1 <- (null)", queue.ToString())

        Assert.AreEqual(7, queue.Size)
        
        queue.Dequeue()
        queue.Dequeue()
        queue.Dequeue()

        Assert.AreEqual("9 <- 9 <- 1 <- 1 <- (null)", queue.ToString())
        Assert.AreEqual(4, queue.Size)

    [<TestMethod>]
    member this.MustThrow () = 
        let queue = PriorityQueue<int>()
        queue.Enqueue(1, 1).Enqueue(2, 2).Enqueue(3, 3).Enqueue(2, 2) |> ignore

        queue.Dequeue()
        queue.Dequeue()
        queue.Dequeue()
        queue.Dequeue()
        Assert.Throws<InvalidOperationException>(fun () -> queue.Dequeue() |> ignore)

    [<TestMethod>]
    member this.SizeTest () = 
        let queue = PriorityQueue<int>()
        queue.Enqueue(1, 1).Enqueue(2, 2).Enqueue(3, 3).Enqueue(2, 2) |> ignore

        queue.Dequeue()
        queue.Dequeue()
        queue.Dequeue()
        queue.Dequeue()
        Assert.AreEqual(0, queue.Size)