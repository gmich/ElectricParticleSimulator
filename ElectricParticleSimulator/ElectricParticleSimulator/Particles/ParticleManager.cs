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
        float coulombsConstant;
        float electronMagnitute;
        float protonMagnitute;

        #endregion

        #region Constructor

        public ParticleManager(Texture2D particleTexture, SpriteFont font)
        {
            this.Font = font;
            electricParticles = new List<ElectricParticle>();
            timePassed = 0.0f;
            this.ParticleTexture = particleTexture;
            ElectronMagnitute = ProtonMagnitute = 6;
            CoulombsConstant = 2;
            ApplyForce = false;
            //AddRandomParticles(100);
        }

        #endregion

        #region Properties

        SpriteFont Font
        {
            get;
            set;
        }

        Texture2D ParticleTexture
        {
            get;
            set;
        }

        bool ApplyForce
        {
            get;
            set;
        }

        float CoulombsConstant
        {
            get
            {
                return coulombsConstant;
            }
            set
            {
                coulombsConstant= MathHelper.Max(value, 0);
            }
        }

        float ElectronMagnitute
        {
            get
            {
                return electronMagnitute;
            }
            set
            {
                electronMagnitute = MathHelper.Max(value, 0);
            }
        }

        float ProtonMagnitute
        {
            get
            {
                return protonMagnitute;
            }
            set
            {
                protonMagnitute = MathHelper.Max(value, 0);
            }
        }

        #endregion

        #region Add Particle Methods

        void AddElectron(Vector2 location)
        {
            electricParticles.Add(new Electron(ParticleTexture, Color.Blue, ElectronMagnitute, CoulombsConstant, location));
        }

        void AddProton(Vector2 location)
        {
            electricParticles.Add(new Proton(ParticleTexture, Color.Red, ProtonMagnitute, CoulombsConstant, location));
        }

        #endregion

        #region Add Random Particles

        public void AddRandomParticles(int quantity)
        {
            Random rand = new Random();
            for (int i = 0; i < quantity; i++)
            {
                Vector2 location = new Vector2(rand.Next(1,999),rand.Next(1,799));
                if (rand.Next(0, 2) == 0)
                    AddElectron(location);
                else
                    AddProton(location);
            }
        }

        #endregion

        #region UpdateConstants

        void UpdateConstants()
        {
            if (InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                CoulombsConstant -= 0.05f;
            }
            else if (InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                CoulombsConstant += 0.05f; ;
            }

            if (InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                ElectronMagnitute -= 0.05f;
            }
            else if (InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                ElectronMagnitute += 0.05f;
            }

            if (InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                ProtonMagnitute -= 0.05f;
            }
            else if (InputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.E))
            {
                ProtonMagnitute += 0.05f;
            }

            if (InputManager.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                ApplyForce = !ApplyForce;
            }
            if (InputManager.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.R))
            {
                electricParticles.Clear();
            }
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timePassed > 0.04f)
            {
                if (InputManager.LeftButtonIsClicked())
                    AddElectron(InputManager.MousePosition);
                else if (InputManager.RightButtonIsClicked())
                    AddProton(InputManager.MousePosition);

                timePassed = 0.0f;
            }

            UpdateConstants();

            if (ApplyForce)
            {
                foreach (ElectricParticle particle in electricParticles)
                {
                    foreach (ElectricParticle otherParticle in electricParticles)
                        particle.ApplyPhysics(otherParticle);
                }

                for (int i = 0; i < electricParticles.Count; i++)
                {
                    if (electricParticles[i].Expired)
                        electricParticles.RemoveAt(i);
                    else
                        electricParticles[i].Update(gameTime);
                }
            }
        }

        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "Coulombs Constant <Q,A>    : " + CoulombsConstant, new Vector2(5, 5), Color.Black);
            spriteBatch.DrawString(Font, "Electron's Magnitute <W,S> : " + ElectronMagnitute, new Vector2(5,20), Color.Black);
            spriteBatch.DrawString(Font, "Proton's Magnitute <E,D>   : " + ProtonMagnitute, new Vector2(5, 35), Color.Black);
            spriteBatch.DrawString(Font, "ApplyForce <Space>         : " + ApplyForce, new Vector2(5, 50), Color.Black);
            spriteBatch.DrawString(Font, "Reset <R> ", new Vector2(5, 65), Color.Black);

            foreach (ElectricParticle particle in electricParticles)
                particle.Draw(spriteBatch);
        }
    }
}
