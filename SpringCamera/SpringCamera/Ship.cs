using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpringCamera
{
	public class Ship
	{
		private readonly Texture2D _texture2D;
		private const float TankVelocity = 400;

		public Ship(Texture2D texture2D)
		{
			_texture2D = texture2D;
			Rotation = 0;
		}

		public Texture2D Texture
		{
			get { return _texture2D; }
		}

		public Vector2 Position { get; private set; }

		public float Rotation { get; private set; }

		public void Update(KeyboardState keyboardState, float elapseSeconds)
		{
			var direction = new Vector2((float)(Math.Cos(-Rotation)), (float)(Math.Sin(-Rotation)));

			if (keyboardState.IsKeyDown(Keys.W))
			{
				Position += direction * TankVelocity * elapseSeconds;
			}
			else if (keyboardState.IsKeyDown(Keys.S))
			{
				Position += -direction * TankVelocity * elapseSeconds;
			}

			if (keyboardState.IsKeyDown(Keys.A))
				Rotation = Rotation - 0.04f;
			else if (keyboardState.IsKeyDown(Keys.D))
				Rotation = Rotation + 0.04f;

			if (Rotation < 0)
				Rotation = MathHelper.TwoPi - Rotation;
			else if (Rotation > MathHelper.TwoPi)
				Rotation = Rotation - MathHelper.TwoPi;
		}
	}
}