module Simulation

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Entities
open Shapes

type Simulator() as this =
    inherit Game()
    let graphics = new GraphicsDeviceManager(this)
    let mutable spritebatch = Unchecked.defaultof<SpriteBatch>
    let mutable state = Unchecked.defaultof<SimulationState>
    let mutable delay = 0.1f
    let mutable originalDelay = delay
   
    override this.Initialize() = 
        graphics.PreferredBackBufferHeight <- 1600
        graphics.PreferredBackBufferWidth <- 1600
        do base.Initialize()
  
    override this.LoadContent() =
        do spritebatch <- new SpriteBatch(this.GraphicsDevice)
        let plainTexture = new Texture2D(this.GraphicsDevice, 1, 1)
        plainTexture.SetData([|Color.White|])
        // Initialize default state
        let blocks = Array2D.init 100 100 (fun x y -> {position = new Vector2(y |> float32, x |> float32); alive = false; aliveNextTurn = false})
        let blocksWithShape = blocks |> Array2D.map (fun block -> { block with alive = (setGlider block) })
        do state <- {texture = plainTexture; blocks = blocksWithShape}
        ()

    override this.Update(gameTime) =
        let dt = gameTime.ElapsedGameTime.TotalSeconds |> float32
        do delay <- delay-dt
        match (delay < 0.f) with
        | true -> 
            do delay <- originalDelay
            do state <- SimulationState.update dt state 
        | false -> ()
  
    override this.Draw(gameTime) =
        do this.GraphicsDevice.Clear Color.Black
        spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
        SimulationState.draw spritebatch state
        spritebatch.End()
        ()