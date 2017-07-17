module Simulation

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open Entities

type Simulator() as this =
    inherit Game()
    
    let graphics = new GraphicsDeviceManager(this)
    let mutable spritebatch = Unchecked.defaultof<SpriteBatch>
    let mutable state = Unchecked.defaultof<SimulationState>

    override this.Initialize() = 
        graphics.PreferredBackBufferHeight <- 1000
        graphics.PreferredBackBufferWidth <- 1600
        do base.Initialize()
   

    override this.LoadContent() =
        do spritebatch <- new SpriteBatch(this.GraphicsDevice)
        let plainTexture = new Texture2D(this.GraphicsDevice, 1, 1)
        plainTexture.SetData([|Color.White|])
        ()


    override this.Update(gameTime) =
        let dt = gameTime.ElapsedGameTime.TotalSeconds |> float32
        do state <- SimulationState.update dt state 
        ()

  
    override this.Draw(gameTime) =
        do this.GraphicsDevice.Clear Color.Black
        spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
        SimulationState.draw spritebatch state
        spritebatch.End()
        ()