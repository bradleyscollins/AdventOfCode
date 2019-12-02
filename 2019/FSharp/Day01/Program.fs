module Day01

open System.IO

let calcFuelForMass mass =
    let fuel = mass / 3 - 2
    max fuel 0

let calcModuleFuel mass =
    let rec calcTotalFuelForMass acc mass =
        if mass = 0
        then acc
        else calcTotalFuelForMass (acc + mass) (calcFuelForMass mass)

    calcTotalFuelForMass 0 (calcFuelForMass mass)

[<EntryPoint>]
let main argv =
    let masses = File.ReadAllLines "input.txt"
                 |> Seq.map int
    let sumOfFuelRequirements = masses
                                |> Seq.map calcFuelForMass
                                |> Seq.sum
    printfn
        "The sum of the fuel requirements for all of the modules on your spacecraft: %d"
        sumOfFuelRequirements

    let sumOfTotalFuelRequements = masses
                                   |> Seq.map calcModuleFuel
                                   |> Seq.sum
    printfn
        "The sum of the fuel requirements for all of the modules on your spacecraft when also taking into account the mass of the added fuel: %d"
        sumOfTotalFuelRequements

    0 // return an integer exit code
