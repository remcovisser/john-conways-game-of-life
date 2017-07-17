open Simulation

[<EntryPoint>]
let main argv =     
    use simulation = new Simulator()
    simulation.Run()

    0 // return an integer exit code    