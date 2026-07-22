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
	public class SolarChickenProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; 
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
		}

		public override void SetDefaults() {
			Projectile.width = 50; 
			Projectile.height = 50; 
			Projectile.aiStyle = -1;
			Projectile.friendly = true; 
			Projectile.hostile = false; 
			Projectile.DamageType = DamageClass.Melee; 
			Projectile.penetrate = 1; 
			Projectile.timeLeft = 300; 
			Projectile.alpha = 0; 
			Projectile.light = 0.5f;
			Projectile.ignoreWater = true; 
			Projectile.tileCollide = true; 
			Projectile.extraUpdates = 1; 
		}

		public override void AI() {
			if (Main.rand.NextBool(3)) {
				int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 0.3f;
			}

			if (Projectile.localAI[0] == 0f) {
				Projectile.localAI[0] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position); 
			}

			NPC closestNPC = null;
			float maxHomingDistance = 800f;

			for (int i = 0; i < Main.maxNPCs; i++) {
				NPC npc = Main.npc[i];
				if (npc.CanBeChasedBy(Projectile, false)) {
					float distanceToNPC = Vector2.Distance(npc.Center, Projectile.Center);
					if (distanceToNPC < maxHomingDistance) {
						maxHomingDistance = distanceToNPC;
						closestNPC = npc;
					}
				}
			}
            
			if (closestNPC != null) {
				Vector2 targetVector = closestNPC.Center - Projectile.Center;
				if (targetVector != Vector2.Zero) {
					targetVector.Normalize();
					targetVector *= 8f;
                    
					Projectile.velocity = (Projectile.velocity * 40f + targetVector) / 41f; 
				}
			}

			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6) {
				Projectile.frameCounter = 0;
				Projectile.frame++; 
	
				if (Projectile.frame >= Main.projFrames[Projectile.type]) {
					Projectile.frame = 0; 
				}
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

		public override bool PreDraw(ref Color lightColor) {
			Texture2D texture = TextureAssets.Projectile[Type].Value;

			int frameHeight = texture.Height / Main.projFrames[Projectile.type];
			int startY = frameHeight * Projectile.frame;

			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, frameHeight * 0.5f);

			for (int k = Projectile.oldPos.Length - 1; k > 0; k--) {
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + (Projectile.Size / 2f) + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.5f;
				Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			Vector2 mainDrawPos = (Projectile.Center - Main.screenPosition) + new Vector2(0f, Projectile.gfxOffY);
			Main.EntitySpriteDraw(texture, mainDrawPos, sourceRectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

			return false;
		}

		public override void OnKill(int timeLeft) {
			if (Projectile.owner == Main.myPlayer) {
				int exp = Projectile.NewProjectile(
					Projectile.GetSource_FromThis(), 
					Projectile.Center, 
					Vector2.Zero, 
					ProjectileID.SolarWhipSwordExplosion,
					Projectile.damage, 
					Projectile.knockBack, 
					Projectile.owner
				);

				if (exp != Main.maxProjectiles) {
					Projectile explosion = Main.projectile[exp];
					explosion.ai[0] = 1f;
					explosion.ai[1] = 1f;
					explosion.localAI[0] = 1f;
					explosion.localAI[1] = 1f;
					explosion.frame = 1;
					explosion.timeLeft = 30;
				}
			}

			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
		}
	}
}