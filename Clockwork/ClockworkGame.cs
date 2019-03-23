using Clockwork.Libraries;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Clockwork
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class ClockworkGame : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public ClockworkGame(int virtualWidth = 624, int virtualHeight = 360, int windowWidth = 1280, int windowHeight = 720, State initialState = null, bool startFullscreen = false)
		{
			_graphics = new GraphicsDeviceManager(this);
			Resolution.Init(ref _graphics);
			Content.RootDirectory = "Content";
			
			Resolution.SetVirtualResolution(virtualWidth, virtualHeight);
			Resolution.SetResolution(windowWidth, windowHeight, startFullscreen);

			CG.State = initialState;
			initialState?.Initialize();

			IsMouseVisible = true;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			Canvas.SpriteBatch = _spriteBatch;
			Libs.Load(Content);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			VMouse.UpdateState();
			
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			
			Time.Delta = (float) gameTime.ElapsedGameTime.TotalSeconds;
			CG.State.PreUpdate();
			CG.State.BeginUpdate();
			CG.State.Update();
			CG.State.EndUpdate();
			
			foreach (GameObject go in CG.State.RemoveQueue)
			{
				go.OnRemove();
				CG.State.Objects.Remove(go);
			}
			CG.State.RemoveQueue.Clear();
			
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			Resolution.BeginDraw();
			_spriteBatch.Begin(transformMatrix: Resolution.GetTransformationMatrix(), samplerState: SamplerState.PointClamp);
			CG.State.Draw();
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
