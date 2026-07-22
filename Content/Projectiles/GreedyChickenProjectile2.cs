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
	public class GreedyChickenProjectile2 : ModProjectile
	{
		public override void SetStaticDefaults() {

			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; 
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
		}

		public override void SetDefaults() {
			Projectile.width = 16; 
			Projectile.height = 16; 
			Projectile.aiStyle = -1;
			Projectile.friendly = true; 
			Projectile.hostile = true; 
			Projectile.DamageType = DamageClass.Melee; 
			Projectile.penetrate = -1; 
			Projectile.timeLeft = 300; 
			Projectile.alpha = 0; 
			Projectile.light = 0.5f;
			Projectile.ignoreWater = true; 
			Projectile.tileCollide = false; 
			Projectile.extraUpdates = 1; 
		}

		public override void AI() {

			if (Projectile.localAI[0] == 0f) {
				Projectile.localAI[0] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position); 
			}

			Player closestPlayer = null;
            float maxHomingDistance = 800f;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player player = Main.player[i];
                if (player.active && !player.dead && !player.ghost) {
                    float distanceToPlayer = Vector2.Distance(player.Center, Projectile.Center);
                    if (distanceToPlayer < maxHomingDistance) {
                        maxHomingDistance = distanceToPlayer;
                        closestPlayer = player;
                    }
                }
            }
            if (closestPlayer != null) {
                Vector2 targetVector = closestPlayer.Center - Projectile.Center;
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

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			Projectile.Kill();
			    target.AddBuff(BuffID.Regeneration, 300); 
		}

		

		public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{	
			Projectile.Kill();
			target.AddBuff(BuffID.Regeneration, 300);
			modifiers.Cancel();
    		modifiers.DisableSound();  
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.SourceDamage *= 0.10f;
			modifiers.Knockback *= 0.10f;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				
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
			// Draws an afterimage trail. See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#afterimage-trail for more information.

			Texture2D texture = TextureAssets.Projectile[Type].Value;

			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = Projectile.oldPos.Length - 1; k > 0; k--) {
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return true;
		}

		public override void OnKill(int timeLeft) {
			if (Projectile.owner == Main.myPlayer) {
			}

			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.CoinPickup, Projectile.position);
		}
	}
}