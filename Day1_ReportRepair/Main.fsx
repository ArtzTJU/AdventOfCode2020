let filePath = "./Day1_ReportRepair/input.txt"

let numbers =
    System.IO.File.ReadAllLines filePath
    |> Seq.map System.Int32.Parse

let combine2D sequence =
    seq {
        for (i1, e1) in sequence |> Seq.indexed do
            for (i2, e2) in sequence |> Seq.indexed do
                if (i1 < i2) then yield (e1, e2)
    }

let combine3D sequence =
    seq {
        for (i1, e1) in sequence |> Seq.indexed do
            for (i2, e2) in sequence |> Seq.indexed do
                for (i3, e3) in sequence |> Seq.indexed do
                    if (i1 < i2 && i1 < i3 && i2 < i3) then
                        yield (e1, e2, e3)
    }

let partOne =
    numbers
    |> combine2D
    |> Seq.find (fun (x, y) -> x + y = 2020)
    |> (fun (x, y) -> x * y)

let partTwo =
    numbers
    |> combine3D
    |> Seq.find (fun (x, y, z) -> x + y + z = 2020)
    |> (fun (x, y, z) -> x * y * z)
