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
	public class BigShotChickenProjectileCharge : ModProjectile
	{
		private int ammoConDelay = 5;
		private int ammoCount = 0;
		private int finalDamage = 0;
		private int chargeTime =0;
		private int ammoDamage;
		public override void SetStaticDefaults() {
			//ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
		}

		public override void SetDefaults() {
			Projectile.width = 60; // The width of projectile hitbox
			Projectile.height = 64; // The height of projectile hitbox
			
			Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Melee; // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 300; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f; // How much light emit around the projectile
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

			chargeTime = 300;
			
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];
			Item weapon = player.HeldItem;
			Item ammoItem = player.ChooseAmmo(weapon);

			// movement logic
			Vector2 targetDir = Main.MouseWorld - Projectile.Center;
			targetDir.Normalize();

			if(targetDir.X < 0) Projectile.spriteDirection = -1;
			else Projectile.spriteDirection = 1; 
			
			Projectile.rotation = targetDir.ToRotation();
			Projectile.Center = player.Center + (targetDir * 50f);
			//

			// arm following logic
			if (Projectile.rotation > MathHelper.ToRadians(-225f) && Projectile.rotation < MathHelper.ToRadians(225f)) player.bodyFrame.Y = 56 * 3;
			else if (Projectile.rotation >= MathHelper.ToRadians(225f) && Projectile.rotation <= MathHelper.ToRadians(315f)) player.bodyFrame.Y = 56 * 4;
			else if (Projectile.rotation <= MathHelper.ToRadians(-225f) && Projectile.rotation >= MathHelper.ToRadians(-315f)) player.bodyFrame.Y = 56 * 2;
		
			if (Projectile.rotation > MathHelper.ToRadians(-45f) && Projectile.rotation < MathHelper.ToRadians(45f)) player.bodyFrame.Y = 56 * 3;
			else if (Projectile.rotation >= MathHelper.ToRadians(45f) && Projectile.rotation <= MathHelper.ToRadians(135f)) player.bodyFrame.Y = 56 * 4;
			else if (Projectile.rotation <= MathHelper.ToRadians(-45f) && Projectile.rotation >= MathHelper.ToRadians(-135f)) player.bodyFrame.Y = 56 * 2;
			player.direction = Projectile.spriteDirection;
			//
			
			// ammo consumption logic
			--ammoConDelay;
			if (ammoItem != null && ammoConDelay <= 0 && ammoCount < 6) {
				player.PickAmmo(weapon, out _, out _, out ammoDamage, out _, out _, false);
				finalDamage += ammoDamage;
				++ammoCount;
				ammoConDelay = 30;
			}
			//

			// dust logic (copy pasted logic for zenith chicken spawning projectiles)
			float dustSpawnDistance = 50f;
			Vector2 randomDirection = Main.rand.NextFloat((float)Math.PI * 2f).ToRotationVector2();
			Vector2 dustSpawnPosition = Projectile.Center + (randomDirection * dustSpawnDistance);

			Vector2 newVelocity = Projectile.Center - dustSpawnPosition;
			newVelocity.Normalize();
			newVelocity *= 5;

			if (chargeTime > 0) {
			Dust dust = Dust.NewDustPerfect(dustSpawnPosition, DustID.YellowTorch, Projectile.velocity + newVelocity);
			dust.noGravity = true;
			dust.customData = Projectile.whoAmI;
			}

			foreach (Dust activeDust in Main.dust) {
				if (activeDust.active && activeDust.type == DustID.YellowTorch && activeDust.customData is int projectileId && projectileId == Projectile.whoAmI) {
					activeDust.position += Projectile.position - Projectile.oldPosition;
				}
			}
			//
			
			if (Projectile.owner == Main.myPlayer && !Main.mouseRight) {
				if (Projectile.owner == Main.myPlayer && chargeTime <= 0) {
					Vector2 velocity = Projectile.rotation.ToRotationVector2() * 12f;
					Projectile.NewProjectile(
						Projectile.GetSource_FromThis(), 
						Projectile.Center, 
						velocity, 
						ModContent.ProjectileType<BigShotProjectile>(),
						finalDamage, 
						Projectile.knockBack, 
						Projectile.owner
					);
				}
				Projectile.Kill();
			}

			Projectile.timeLeft = 2;
			if (chargeTime == 0) SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
			chargeTime = chargeTime - 2;
		}


		public override bool PreDraw(ref Color lightColor) {
			// color becomes more yellow over time
			int chargeGlow = (int)((300 - chargeTime) * 0.85f);
			
			//

			// main projectile rendering logic
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
			Color color = new Color(lightColor.R, lightColor.G, Math.Max(0, lightColor.B - chargeGlow), lightColor.A);
			color = Projectile.GetAlpha(color);
			SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
			//

			Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
			return false;
		}

		public override void OnKill(int timeLeft) {
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			if (chargeTime <= 0) SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			else SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}