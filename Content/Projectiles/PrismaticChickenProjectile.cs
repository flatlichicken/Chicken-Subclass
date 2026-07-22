using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Projectiles
{
    public class PrismaticChickenProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.FairyQueenRangedItemShot;
        private Color glowColor = Color.White;
        private int Whiteness = 0;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = ProjectileID.Sets.TrailCacheLength[ProjectileID.FairyQueenRangedItemShot];
            ProjectileID.Sets.TrailingMode[Projectile.type] = ProjectileID.Sets.TrailingMode[ProjectileID.FairyQueenRangedItemShot];
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; 
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.FairyQueenRangedItemShot);
            AIType = ProjectileID.Bullet;
            Projectile.aiStyle = 1;
            Projectile.light = 0.5f; 
        }

        public override bool PreAI() {
            for (int i = 0; i < Main.maxProjectiles; i++) {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.whoAmI != Projectile.whoAmI && proj.owner == Projectile.owner && proj.type == ProjectileID.FairyQueenRangedItemShot && Vector2.Distance(proj.Center, Projectile.Center) < 5f) {
                    proj.Kill();
                }
            }
            return true;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, glowColor.ToVector3() * 0.5f);
        }

        public override void PostAI() {
            if (Main.rand.NextBool(2)) {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, 266 + 1, Projectile.velocity * 0.5f, 100, glowColor, 1.2f);
                dust.noGravity = true;
                dust.fadeIn = 0.2f;
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            float hueOffset = Main.rand.NextFloat(); 
            glowColor = Main.hslToRgb(hueOffset, 1f, 0.5f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            int borderSize = 4;

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                if (Projectile.oldPos[k] == Vector2.Zero) continue;
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f) + new Vector2(0f, Projectile.gfxOffY);
                Color trailColor = glowColor.MultiplyRGBA(new Color(255, 255, 255, 0)) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.6f;
                Main.EntitySpriteDraw(texture, drawPos, null, trailColor, Projectile.rotation - MathHelper.PiOver2, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            Vector2 mainDrawPos = Projectile.position - Main.screenPosition + new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f) + new Vector2(0f, Projectile.gfxOffY);
            Color edgeGlow = glowColor.MultiplyRGBA(new Color(255, 255, 255, 0)) * 0.8f;
            for (int i = 0; i < 4; i++)
            {
                Vector2 offset = new Vector2(borderSize, 0).RotatedBy(i * MathHelper.PiOver2);
                Main.EntitySpriteDraw(texture, mainDrawPos + offset, null, edgeGlow, Projectile.rotation - MathHelper.PiOver2, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            if (Whiteness < 255) Whiteness += 5;
            Main.EntitySpriteDraw(texture, mainDrawPos, null, new Color(Whiteness, Whiteness, Whiteness, 0), Projectile.rotation - MathHelper.PiOver2, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

            return false; 
        }
    }
}