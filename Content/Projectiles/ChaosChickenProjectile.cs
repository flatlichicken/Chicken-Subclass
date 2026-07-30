using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Chickensubclass.Content.Projectiles
{
    public class ChaosChickenProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; 
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
        }

        public override void SetDefaults() {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 360;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.aiStyle = 0; // Set to 0 to use custom AI or no movement logic
        }

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
  	    if (target.type == NPCID.TheDestroyer || target.type == NPCID.TheDestroyerBody || target.type == NPCID.TheDestroyerTail) {
        // SourceDamage cuts the raw power of the chicken before the Destroyer's armor 
        modifiers.SourceDamage *= 0.5f; 
 		   }
		}

        public override void AI() {
        // Force the projectile to stay exactly at the player's center or "RotatedRelativePoint"
        Player player = Main.player[Projectile.owner];
        if (player.heldProj == Projectile.whoAmI) {
        Projectile.Center = player.RotatedRelativePoint(player.MountedCenter, true);
        Projectile.rotation = player.itemRotation + (player.direction == -1 ? MathHelper.Pi : 0);
            }
        }

        public override bool PreDraw(ref Color lightColor) {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);

            for (int k = 0; k < Projectile.oldPos.Length; k++) {
                if (Projectile.oldPos[k] == Vector2.Zero) continue;

                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                float opacity = (Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length;
                Color color = Projectile.GetAlpha(lightColor) * opacity;

                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            } // This was the only brace you needed here

            return true; 
        }

        public override void OnKill(int timeLeft) {
			    // Dust or sound effects go here
        }
    }
}