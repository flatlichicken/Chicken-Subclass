using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Projectiles
{
    public class ChickenWalkerProjectile: ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Starfury;
        
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.FallingStar);
            AIType = ProjectileID.FallingStar;
            Projectile.DamageType = DamageClass.Melee; 
            Projectile.aiStyle = 9;
            Projectile.light = 0.5f; 
        }
    }
}