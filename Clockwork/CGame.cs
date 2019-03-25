using System;
using Clockwork.Input;
using Clockwork.Libraries;
using Clockwork.Process;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Clockwork
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class CGame : Game
	{
		internal static CGame Game;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		internal Scene QueuedScene = null;

		private Scene _currentScene = null;

		public static Scene CurrentScene => Game._currentScene;

		public CGame(Scene initialScene, int virtualWidth = 624, int virtualHeight = 360, int windowWidth = 1280,
			int windowHeight = 720, bool startFullscreen = false)
		{
			Game = this;
			_graphics = new GraphicsDeviceManager(this);
			Resolution.Init(ref _graphics);
			Content.RootDirectory = "Content";

			Resolution.SetVirtualResolution(virtualWidth, virtualHeight);
			Resolution.SetResolution(windowWidth, windowHeight, startFullscreen);

			IsMouseVisible = true;

			QueuedScene = initialScene;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			InitializeQueuedScene();
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
			VKeyboard.UpdateState();
			VMouse.UpdateState();

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

			Time.Delta = (float) gameTime.ElapsedGameTime.TotalSeconds;
			_currentScene.PreUpdate();
			_currentScene.BeginUpdate();
			_currentScene.Update();
			_currentScene.EndUpdate();
			_currentScene.PostUpdate();

			foreach (BaseObject go in _currentScene.RemoveQueue)
			{
				go.OnRemove();
				_currentScene.Objects.Remove(go);
			}

			_currentScene.RemoveQueue.Clear();

			if (QueuedScene != null)
			{
				InitializeQueuedScene();
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			Resolution.BeginDraw();
			_spriteBatch.Begin(transformMatrix: Resolution.GetTransformationMatrix(),
				samplerState: SamplerState.PointClamp);
			_currentScene.Draw();
			_spriteBatch.End();

			base.Draw(gameTime);
		}

		private void InitializeQueuedScene()
		{
			if (_currentScene != null)
			{
				foreach (BaseObject go in _currentScene.Objects)
				{
					go.OnSceneEnd();
				}
			}

			_currentScene = QueuedScene;
			QueuedScene = null;
			_currentScene.Initialize();
		}
	}
}