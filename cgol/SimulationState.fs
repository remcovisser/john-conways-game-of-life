module SimulationState

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Entities

let boolToInt bool = 
    match bool with
    | true -> 1
    | false -> 0

let updateBlock (block: Block) (state: SimulationState) =
    let arraySize = (Array2D.length1 state.blocks)-1
    let blockAlive positionX positionY  = 
        match ((block.position.X |> int)+positionX < 0) || ((block.position.X |> int)+positionX > arraySize) || ((block.position.Y |> int)+positionY < 0) || ((block.position.Y |> int)+positionY > arraySize) with 
        | true -> 0
        | false -> boolToInt (Array2D.get state.blocks ((block.position.Y |> int)+positionY) ((block.position.X |> int)+positionX)).alive
    let topLeftAlive = blockAlive -1 -1
    let topMiddleAlive = blockAlive -1 0
    let topRightAlive = blockAlive -1 1
    let leftAlive = blockAlive 0 -1
    let rightAlive = blockAlive 0 1
    let bottomLeftAlive = blockAlive 1 -1
    let bottomMiddleAlive = blockAlive 1 0
    let bottoomRightAlive = blockAlive 1 1

    let aliveNeighbors = topLeftAlive + topMiddleAlive + topRightAlive + leftAlive + rightAlive + bottomLeftAlive + bottomMiddleAlive + bottoomRightAlive
    match aliveNeighbors, block.alive with
    | (3 | 2), true -> true
    | (4 | 1 | 0), true -> false
    | 3, false -> true
    | _ -> false


let update dt state =
    let updatedBlocks = state.blocks |> Array2D.map (fun block -> { block with aliveNextTurn = (updateBlock block state) })
    { state with
        blocks = updatedBlocks |> Array2D.map (fun block -> { block with alive = block.aliveNextTurn })
    }


let draw (spritebatch: SpriteBatch) (state: SimulationState) =
    let getColor alive = 
        match alive with
        | true -> Color.Green
        | false -> Color.Red

    state.blocks |> Array2D.iter (fun block -> spritebatch.Draw(state.texture, new Rectangle(block.position.X * 16.f |> int, block.position.Y * 16.f |> int, 16, 16), (getColor block.alive)))
    ()