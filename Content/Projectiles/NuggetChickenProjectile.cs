using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Projectiles
{
    public class NuggetChickenProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 13;
            AIType = ProjectileID.ChainGuillotine;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            
            if (player.active && !player.dead)
            {
                player.channel = true;
            }
            
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Vector2 direction = Projectile.Center - player.Center;
            Projectile.rotation = direction.ToRotation() - MathHelper.PiOver2; 
        }

        public override bool PreDrawExtras()
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            
            Texture2D tex = ModContent.Request<Texture2D>("Chickensubclass/Content/Projectiles/NuggetChickenProjectileChain").Value;
            Vector2 pCenter = Main.player[Projectile.owner].MountedCenter;
            
            Vector2 toPlayer = pCenter - Projectile.Center;
            float chainRotation = toPlayer.ToRotation() - MathHelper.PiOver2;

            // this is the chain script not the head
            for (Vector2 pos = Projectile.Center; Vector2.Distance(pos, pCenter) > 16f; pos += Vector2.Normalize(pCenter - pos) * 12f)
            {
                Main.EntitySpriteDraw(tex, pos - Main.screenPosition, null, Lighting.GetColor((int)(pos.X / 16f), (int)(pos.Y / 16f)), chainRotation, tex.Size() * 0.5f, 1f, 0, 0);
            }
            // end of chain part
            
            Vector2 mainDrawPos = Projectile.position - Main.screenPosition + new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f) + new Vector2(0f, Projectile.gfxOffY);
            Main.EntitySpriteDraw(texture, mainDrawPos, null, Lighting.GetColor((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f)), Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}