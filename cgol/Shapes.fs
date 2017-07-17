module Shapes

open Entities

let setLine (block: Block) =
    match block.position.X = 1.0f with
    | true -> true
    | false -> false

let setRow (block: Block) =
    match block.position.Y >= 10.0f && block.position.Y < 20.f && block.position.X = 10.0f with
    | true -> true
    | false -> false

let setGlider (block: Block) = 
    if (block.position.Y = 10.0f && block.position.X = 10.0f) || 
        (block.position.Y = 11.0f && block.position.X = 11.0f) || 
        (block.position.Y = 12.0f && block.position.X = 9.0f) || 
        (block.position.Y = 12.0f && block.position.X = 10.0f) || 
        (block.position.Y = 12.0f && block.position.X = 11.0f) then
        true
    else 
        false