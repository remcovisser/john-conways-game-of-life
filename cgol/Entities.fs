module Entities

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Block = {
    position: Vector2
    alive: bool
    aliveNextTurn: bool
}

type SimulationState = {
    blocks: Block[,]
    texture: Texture2D
}