using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Projectiles
{
	public class HolyChickenProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; 
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
		}

		protected virtual float HoldoutRangeMin => 24f;
		protected virtual float HoldoutRangeMax => 412f;

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Spear); 
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner];
			int duration = player.itemAnimationMax;
			player.heldProj = Projectile.whoAmI;

			if (Projectile.timeLeft > duration) Projectile.timeLeft = duration;

			Projectile.velocity = Vector2.Normalize(Projectile.velocity);

			// This keeps the projectile synced to the player's direction
			Projectile.spriteDirection = player.direction;

			float halfDuration = duration * 0.5f;
			float progress = Projectile.timeLeft < halfDuration 
				? Projectile.timeLeft / halfDuration 
				: (duration - Projectile.timeLeft) / halfDuration;

			Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

			// SWAPPED OFFSETS: This fixes the "flipped on the wrong side" issue
			float rotationOffset = Projectile.spriteDirection == 1 ? 45f : 135f;
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(rotationOffset);

			return false; 
		}

		public override bool PreDraw(ref Color lightColor) {
			Texture2D texture = TextureAssets.Projectile[Type].Value;
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);

			// INVERTED FLIP: If it was wrong before, this flips the logic
			SpriteEffects effects = Projectile.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = (Projectile.oldPos[k] + Projectile.Size * 0.5f) - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
				
				float opacity = (float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length;
				Color color = Projectile.GetAlpha(lightColor) * opacity;

				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0);
			}

			return true;
		}
	}
}
