using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElectricParticleSimulator
{
    public class Proton : ElectricParticle
    {

        #region Constructor 
        
        public Proton(Texture2D Texture, Color Color, float Magnitute, float CoulombsConstant, Vector2 Location)
            : base(Texture, Color, Magnitute, CoulombsConstant, Location)
        { }

        #endregion

        public void ApplyPhysics(ElectricParticle otherParticle)
        {
            Vector2 direction = Vector2.Normalize(Location-otherParticle.Location);

            if (otherParticle is Proton)
                Velocity -= direction * (float)(CoulombsConstant * ((Magnitute * otherParticle.Magnitute)) / Math.Pow((double)Vector2.Distance(Location, otherParticle.Location), 2));
            else if(otherParticle is Electron)
                Velocity += direction * (float)(CoulombsConstant * ((Magnitute * otherParticle.Magnitute)) / Math.Pow((double)Vector2.Distance(Location, otherParticle.Location), 2));
        }
    }
}
