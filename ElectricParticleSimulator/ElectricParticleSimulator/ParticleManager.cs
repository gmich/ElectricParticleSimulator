using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ElectricParticleSimulator
{
    public class ParticleManager
    {
        #region Declarations

        List<ElectricParticle> electricParticles;
        float timePassed;

        #endregion

        #region Constructor

        public ParticleManager(ContentManager Content)
        {
            electricParticles = new List<ElectricParticle>();
            timePassed = 0.0f;
            ProtonTexture = Content.Load<Texture2D>(@"Proton");
            ElectronTexture = Content.Load<Texture2D>(@"Electron");
        }

        #endregion

        #region Properties

        Texture2D ProtonTexture
        {
            get;
            set;
        }

        Texture2D ElectronTexture
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
            electricParticles.Add(new Electron(ElectronTexture, Color.Red, 10, CoulombsConstant, location));
        }

        void AddProton(Vector2 location)
        {
            electricParticles.Add(new Proton(ProtonTexture, Color.Blue, 6, CoulombsConstant, location));
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {

        }

        #endregion
    }
}
