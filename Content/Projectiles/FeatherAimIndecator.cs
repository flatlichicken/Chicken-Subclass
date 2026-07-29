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
    public class FeatherAimIndecator : ModProjectile
    {
        private int TimeLeft = 60;
        public float featherSpeed = 10f;

        public override void SetStaticDefaults() {
            
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults() {
            Projectile.width = 62; // The width of projectile hitbox
            Projectile.height = 62; // The height of projectile hitbox
            Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 300; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame

            
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            // If collide with tile, reduce the penetrate.
            // So the projectile can reflect at most 5 times
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0) {
                Projectile.Kill();
            }
            else {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

                // If the projectile hits the left or right side of the tile, reverse the X velocity
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
                    Projectile.velocity.X = -oldVelocity.X;
                }

                // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }

            return false;
        }

        public override void AI() 
        {
            TimeLeft--;

            float featherSpeed = Projectile.ai[0] * 2;

            Player target = Main.player[Player.FindClosest(Projectile.Center, Projectile.width, Projectile.height)];
            if (target != null && target.active && !target.dead) {
                Vector2 targetDirection = target.Center - Projectile.Center;
                Projectile.rotation = targetDirection.ToRotation() - MathHelper.PiOver2;
            }

            if (++Projectile.frameCounter >= 6) {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type]) {
                    Projectile.frame = 0;
                }
            }

            if (TimeLeft <= 0) {
                if (Main.myPlayer == Projectile.owner) {
                    
                    float angle = Projectile.rotation + MathHelper.PiOver2;
                    Vector2 featherVelocity = angle.ToRotationVector2() * featherSpeed;
                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(), 
                        Projectile.Center, 
                        featherVelocity, 
                        ModContent.ProjectileType<GreatChickenFeatherProjectile>(),
                        Projectile.damage, 
                        Projectile.knockBack, 
                        Projectile.owner
                    );
                    
                }
                Projectile.Kill();
            }
        }

        public override bool PreDraw(ref Color lightColor) {
            return true;
        }

        public override void OnKill(int timeLeft) {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        }
    }
}