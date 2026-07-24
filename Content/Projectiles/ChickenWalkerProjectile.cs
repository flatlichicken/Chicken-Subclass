using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Projectiles
{
    public class ChickenWalkerProjectile: ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.FallingStar;
        
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.FallingStar);
            AIType = ProjectileID.FallingStar;
            Projectile.DamageType = DamageClass.Melee; 
            Projectile.penetrate = 1;
            Projectile.aiStyle = 5;
            Projectile.light = 0.5f; 
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 randomOffset = new Vector2(Main.rand.NextFloat(-300f, 300f), -600f);
                Vector2 spawnPosition = Projectile.Center + randomOffset;
                
                float projSpeed = 12f;
                Vector2 newVelocity = (Projectile.Center - spawnPosition).SafeNormalize(Vector2.UnitY) * projSpeed;

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(), 
                    spawnPosition, 
                    newVelocity, 
                    ModContent.ProjectileType<ChickenWalkerProjectile2>(),
                    Projectile.damage, 
                    Projectile.knockBack, 
                    Projectile.owner
                );
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0) {
                Projectile.Kill();
            }

            else {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
                    Projectile.velocity.X = -oldVelocity.X;
                }

                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }

            return false;
        }
    }
}