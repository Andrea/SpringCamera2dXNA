using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpringCamera
{
	public class SpringCamera
	{
		private Vector2 _desiredPosition;
		private Vector2 _position;
		private Vector2 _velocity;
		private Vector2 _halfScreenSize;
		private Viewport _viewport;

		public SpringCamera( Viewport viewport)
		{
			Transform = Matrix.Identity;
			_viewport = viewport;
			_halfScreenSize = new Vector2(Viewport.Width / 2, Viewport.Height / 2);

			/* Values you can change to modify the camera reaction */
			Damping = 3.9f;
			SpringStiffness = 30;
			Mass = 0.5f;
		}

		public Matrix Transform { get; private set; }

		public float Mass { get; private set; }

		public float SpringStiffness { get; set; }

		public float Damping { get; set; }

		public Viewport Viewport
		{
			get { return _viewport; }
		}


		public void Update(float elapsedSeconds, float rotation, Vector2 desiredPosition)
		{
			var x = _position - desiredPosition;
			var force = -SpringStiffness * x - Damping * _velocity;

			var acceleration = force / Mass;
			_velocity += acceleration * elapsedSeconds;
			_position += _velocity * elapsedSeconds;

			Transform = Matrix.CreateTranslation(-_position.X, -_position.Y, 0) *
			            Matrix.CreateRotationZ(rotation) *
			            Matrix.CreateTranslation(_halfScreenSize.X, _halfScreenSize.Y, 0);
		}
	}
}