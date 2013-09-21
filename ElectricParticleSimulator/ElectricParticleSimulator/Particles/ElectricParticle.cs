#region Declarations

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace ElectricParticleSimulator
{
    public abstract class ElectricParticle
    {

        #region Declarations

        Vector2 velocity;

        #endregion

        #region Properties

        #region Physics Properties

        public float Magnitute
        {
            get;
            private set;
        }

        public Vector2 Location
        {
            get;
            private set;
        }

        public float CoulombsConstant
        {
            get;
            private set;
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = new Vector2(MathHelper.Min(value.X, 100), MathHelper.Min(value.Y, 100));
            }
        }

        #endregion

        #region Drawing Properties

        Rectangle DestinationRectangle
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, Size, Size);
            }
        }

        Color Color
        {
            get;
            set;
        }

        int Size
        {
            get
            {
                return 3;
            }
        }

        Texture2D Texture
        {
            get;
            set;
        }

        #endregion

        public bool Expired
        {
            get
            {
                return (Location.X < 0 || Location.Y < 0 || Location.X > 1000 || Location.Y > 800);
            }
        }
        #endregion

        #region Constructor

        public ElectricParticle(Texture2D Texture,Color Color, float Magnitute, float CoulombsConstant, Vector2 Location)
        {
            this.Magnitute = Magnitute;
            this.Texture = Texture;
            this.Location = Location;
            this.CoulombsConstant = CoulombsConstant;
            this.Color = Color;
            Velocity = Vector2.Zero;
        }

        #endregion

        #region Determine Direction

        public abstract void ApplyPhysics(ElectricParticle otherParticle);

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            this.Location += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, Color);
        }

        #endregion

    }
}
