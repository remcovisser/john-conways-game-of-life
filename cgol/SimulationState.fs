module SimulationState

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Entities


let update dt state =
    state


let draw (spritebatch: SpriteBatch) (state: SimulationState) =
    spritebatch.Draw(state.background, Vector2.Zero, Color.White)