using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace ElectricParticleSimulator
{
    using Input;

    public class ParticleManager
    {
        #region Declarations

        List<ElectricParticle> electricParticles;
        float timePassed;

        #endregion

        #region Constructor

        public ParticleManager(Texture2D particleTexture)
        {
            electricParticles = new List<ElectricParticle>();
            timePassed = 0.0f;
            this.ParticleTexture = particleTexture;
        }

        #endregion

        #region Properties

        Texture2D ParticleTexture
        {
            get;
            set;
        }

        float CoulombsConstant
        {
            get
            {
                return 100;
            }
        }

        #endregion

        #region Add Particle Methods

        void AddElectron(Vector2 location)
        {
            electricParticles.Add(new Electron(ParticleTexture, Color.Red, 10, CoulombsConstant, location));
        }

        void AddProton(Vector2 location)
        {
            electricParticles.Add(new Proton(ParticleTexture, Color.Blue, 6, CoulombsConstant, location));
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timePassed > 0.1f)
            {
                if (InputManager.LeftButtonIsClicked())
                    AddElectron(InputManager.MousePosition);
                else if (InputManager.RightButtonIsClicked())
                    AddProton(InputManager.MousePosition);
                
                timePassed = 0.0f;
            }

            foreach (ElectricParticle particle in electricParticles)
                foreach (ElectricParticle otherParticle in electricParticles)
                    particle.ApplyPhysics(otherParticle);
        }

        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ElectricParticle particle in electricParticles)
                particle.Draw(spriteBatch);
        }
    }
}
