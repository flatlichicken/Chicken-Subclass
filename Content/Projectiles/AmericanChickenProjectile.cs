using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Projectiles
{
    public class AmericanChickenProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;
        
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Melee; 
            Projectile.aiStyle = 1;
            Projectile.light = 0.5f; 
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player owner = Main.player[Projectile.owner];

            Item ammo = owner.ChooseAmmo(owner.HeldItem);

            if (ammo != null && ammo.shoot > ProjectileID.None)
            {
                int originalDamageType = Projectile.DamageType;

                Projectile.CloneDefaults(ammo.shoot);

                Projectile.DamageType = originalDamageType;
            }
        }
    }
}