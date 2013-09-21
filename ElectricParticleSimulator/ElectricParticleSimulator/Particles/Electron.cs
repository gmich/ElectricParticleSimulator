using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElectricParticleSimulator
{
    public class Electron : ElectricParticle
    {

        #region Constructor 
        
        public Electron(Texture2D Texture, Color Color, float Magnitute, float CoulombsConstant, Vector2 Location): base(Texture,Color,Magnitute,CoulombsConstant,Location)
        { }

        #endregion

        public override void ApplyPhysics(ElectricParticle otherParticle)
        {
            if (Location != otherParticle.Location)
            {
                Vector2 direction = (Location - otherParticle.Location);

                if (otherParticle is Proton)
                    Velocity -= direction * ((float)(CoulombsConstant * ((Magnitute * otherParticle.Magnitute)) / Math.Pow((double)Vector2.Distance(Location, otherParticle.Location), 2)));
                else if (otherParticle is Electron)
                    Velocity += direction * ((float)(CoulombsConstant * ((Magnitute * otherParticle.Magnitute)) / Math.Pow((double)Vector2.Distance(Location, otherParticle.Location), 2)));
            }
        }
    }
}
