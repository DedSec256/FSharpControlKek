namespace FSharpControl

module Task3  =
    open System

     (* Task 3 *)
    /// <summary>  
    ///  Последовательность ячеек c приоритетом внутри очереди
    /// </summary> 
    type Element<'a> = 
        | Element of 'a * int * Element<'a>
        | Empty
        with
            override this.ToString() =
                match this with
                | Element(x, p, n) -> x.ToString() + " <- " + n.ToString()
                | Empty -> "(null)"

            member this.GetValue() = 
                match this with 
                | Element(x, p, n) -> Some(x)
                | Empty -> None
    
    /// <summary>  
    ///  FIFO - очередь 
    /// </summary> 
    type PriorityQueue<'a>() = 
        let mutable start = Empty
        let mutable size = 0
        /// <summary>  
        ///  Размер очереди
        /// </summary> 
        member this.Size
            with get() = size
        /// <summary>  
        ///  Пуста ли очередь
        /// </summary> 
        member this.IsEmpty() = 
            start = Empty
        override this.ToString() = 
            start.ToString()

        /// <summary>  
        ///  Добавить элемент в очередь. 
        ///  С текучим синтаксисом .Enqueue(10, 1).Enqueue(20, 2)
        /// <param name="item">Добавляемый элемент</param>
        /// <param name="priority">Приоритет элемента</param>
        /// </summary> 
        member this.Enqueue (item, priority) = 
            let rec recEnqueue data node =
                match node with 
                | Empty -> Element(item, priority, Empty)
                | Element(x, p, n) -> 
                    match p with
                    | p when (p >= priority) -> 
                        match n with
                        | Empty -> Element(x, p, Element(item, priority, Empty))
                        | Element(_, np, _) ->
                            match np with
                            | np when np >= priority -> Element(x, p, recEnqueue item n) 
                            | _ -> Element(x, p, Element(item, priority, n))
                    | _ -> Element(item, priority, node) 
                   
            size <- size + 1
            start <- recEnqueue item start
            this

        /// <summary>  
        ///  Получить первого в очереди и удалить его из неё
        /// </summary> 
        member this.Dequeue() = 
            let getStart() = 
                match start with
                | Empty -> raise (InvalidOperationException("Очередь пуста"))
                | Element(x, p, n) -> (x, n)

            let (data, newStart) = getStart()
            start <- newStart
            size <- size - 1
            data

        /// <summary>  
        ///  Получить первого в очереди без удаления
        /// </summary>
        member this.First() = 
            match start with
            | Empty -> raise (InvalidOperationException("Очередь пуста"))
            | Element(x, p, n) -> x
