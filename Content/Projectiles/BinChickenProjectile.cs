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
	public class BinChickenProjectile : ModProjectile
	{
		protected virtual float HoldoutRangeMin => 100f;
		protected virtual float HoldoutRangeMax => 512f;

		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // The recording mode
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Spear); 
			Projectile.penetrate = -1; 
			Projectile.hide = false;
			Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12; 
			
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner]; 
			int duration = player.itemAnimationMax; 

			if (player.heldProj < 0) player.heldProj = Projectile.whoAmI;
			
			if (Projectile.timeLeft > duration) {
				Projectile.timeLeft = duration;
			}

			Projectile.velocity = Vector2.Normalize(Projectile.velocity);

			float halfDuration = duration * 0.5f;
			float progress;

			if (Projectile.timeLeft < halfDuration) {
				progress = Projectile.timeLeft / halfDuration;
			}
			else {
				progress = (duration - Projectile.timeLeft) / halfDuration;
			}

			int count = 0, index = 0;
			for (int i = 0; i < Main.maxProjectiles; i++) {
				if (Main.projectile[i].active && Main.projectile[i].owner == Projectile.owner && Main.projectile[i].type == Type) {
					if (i == Projectile.whoAmI) index = count;
					count++;
				}
			}
			Vector2 dir = Vector2.Normalize(Projectile.velocity);

			Projectile.Center = player.MountedCenter + Vector2.SmoothStep(dir * HoldoutRangeMin, dir * HoldoutRangeMax, progress);
			Projectile.rotation = dir.ToRotation() + MathHelper.ToRadians(Projectile.spriteDirection == -1 ? 45f : 135f);
			Projectile.spriteDirection = player.direction * -1;

			return false; 
		}

		public override bool PreDraw(ref Color lightColor) {
			Texture2D texture = TextureAssets.Projectile[Type].Value;
			Vector2 drawOrigin = texture.Size() / 2f;
			SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

			for (int k = Projectile.oldPos.Length - 1; k > 0; k--) {
				if (Projectile.oldPos[k] == Vector2.Zero) continue;

				Vector2 drawPos = Projectile.oldPos[k] + (Projectile.Size / 2f) - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.5f;
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0);
			}

			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
			return false;
		}

	}
}