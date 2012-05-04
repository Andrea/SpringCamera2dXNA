using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpringCamera
{
	public class ShipGame : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Ship _ship;
		private Texture2D _background;
		
		private SpringCamera _springCamera;
		private float _rotation;

		public ShipGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_springCamera = new SpringCamera(new Viewport(0, 0, GraphicsDevice.Viewport.Width - 20, GraphicsDevice.Viewport.Height));
			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_ship = new Ship(Content.Load<Texture2D>("enemyShip"));
			_background = Content.Load<Texture2D>("space2");
		}

		
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();
			
			var keyboardState = Keyboard.GetState();
			var elapsedSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
			_ship.Update(keyboardState, elapsedSeconds);
			_springCamera.Update(elapsedSeconds, _ship.Rotation, _ship.Position);
			base.Update(gameTime);
		}

		
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);

			_graphics.GraphicsDevice.Viewport = _springCamera.Viewport;

			_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, _springCamera.Transform);
			_spriteBatch.Draw(_background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
			_spriteBatch.End();

			_spriteBatch.Begin();
			Vector2 halfScreen = new Vector2(_graphics.GraphicsDevice.Viewport.Width/2, _graphics.GraphicsDevice.Viewport.Height/2);
			_spriteBatch.Draw(_ship.Texture, halfScreen, Color.White);
			_spriteBatch.End();

			base.Draw(gameTime);

		}
	}
}
