namespace FSharpControl

module Task3  =
    open System

     (* Task 3 *)
    /// <summary>  
    ///  ������������������ ����� c ����������� ������ �������
    /// </summary> 
    type Element<'a> = 
        | Element of 'a * int * Element<'a>
        | Empty
        with
            override this.ToString() =
                match this with
                | Element(x, p, n) -> x.ToString() + " <- " + n.ToString()
                | Empty -> "(null)"
    
    /// <summary>  
    ///  FIFO - ������� 
    /// </summary> 
    type PriorityQueue<'a>() = 
        let mutable start = Empty
        let mutable size = 0
        /// <summary>  
        ///  ������ �������
        /// </summary> 
        member this.Size
            with get() = size
        /// <summary>  
        ///  ����� �� �������
        /// </summary> 
        member this.IsEmpty() = 
            start = Empty
        override this.ToString() = 
            start.ToString()

        /// <summary>  
        ///  �������� ������� � �������. 
        ///  � ������� ����������� .Enqueue(10, 1).Enqueue(20, 2)
        /// <param name="item">����������� �������</param>
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
        ///  �������� ������� � ������� � ������� ��� �� ��
        /// </summary> 
        member this.Dequeue() = 
            let getStart() = 
                match start with
                | Empty -> raise (InvalidOperationException("������� �����"))
                | Element(x, p, n) -> (x, n)

            let (data, newStart) = getStart()
            start <- newStart
            size <- size - 1
            data

        /// <summary>  
        ///  �������� ������� � ������� ��� ��������
        /// </summary>
        member this.First() = 
            match start with
            | Empty -> raise (InvalidOperationException("������� �����"))
            | Element(x, p, n) -> x