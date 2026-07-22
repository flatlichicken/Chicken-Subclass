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
	public class RoaringChickenProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; 
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
		}

		public override void SetDefaults() {
			Projectile.width = 62; 
			Projectile.height = 62; 
			Projectile.aiStyle = 179; 
			Projectile.friendly = true; 
			Projectile.hostile = false; 
			Projectile.DamageType = DamageClass.Melee; 
			Projectile.penetrate = 5; 
			Projectile.timeLeft = 300; 
			Projectile.alpha = 0; 
			Projectile.light = 0f; 
			Projectile.ignoreWater = true; 
			Projectile.tileCollide = true; 
			Projectile.extraUpdates = 1; 

			AIType = ProjectileID.Bullet; 
		}

		private Vector2 lockedDirection = Vector2.Zero;

		public override void AI() {
    	if (lockedDirection == Vector2.Zero) {
    	    if (Projectile.velocity != Vector2.Zero) {
    	        lockedDirection = Projectile.velocity;
    	    }
    	    else {
    	        lockedDirection = Main.MouseWorld - Projectile.Center;
    	        lockedDirection.Normalize();
    	        lockedDirection *= 10f; 
    	    }
    	}

		
	
    	NPC target = null;
    	float maxRange = 800f; 
    	float closestDistance = maxRange;

    	for (int i = 0; i < Main.maxNPCs; i++) {
    	    NPC npc = Main.npc[i];
    	    if (npc.active && !npc.friendly && npc.chaseable && npc.lifeMax > 5) {
    	        float distance = Vector2.Distance(Projectile.Center, npc.Center);
    	        if (distance < closestDistance) {
    	            closestDistance = distance;
    	            target = npc;
    	        }
    	    }
    	}

    	if (Projectile.velocity == Vector2.Zero) {
    	    if (target != null) {
    	        Vector2 targetDir = target.Center - Projectile.Center;
    	        targetDir.Normalize();
    	        float lastKnownSpeed = lockedDirection.Length() > 0.1f ? lockedDirection.Length() : 10f;
    	        lockedDirection = targetDir * lastKnownSpeed;
    	    }
    	    Projectile.rotation = lockedDirection.ToRotation() + MathHelper.PiOver2;
    	}
    	else {
    	    float currentSpeed = Projectile.velocity.Length();
    	    Projectile.velocity = lockedDirection.SafeNormalize(Vector2.UnitY) * currentSpeed;

    	    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
    	}
		}

		public override void PostAI() {
            for (int i = 0; i < Main.maxDustToDraw; i++) {
                Dust dust = Main.dust[i];
                if (dust.active && Vector2.Distance(dust.position, Projectile.Center) < 50f) {
                    dust.active = false;
                }
            }
        }

		public override bool OnTileCollide(Vector2 oldVelocity) {
			Projectile.penetrate--;
			if (Projectile.penetrate <= 5) {
				Projectile.Kill();
			}
			else {
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
					Projectile.velocity.X = -oldVelocity.X;
					lockedDirection.X = -lockedDirection.X;
				}

				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
					Projectile.velocity.Y = -oldVelocity.Y;
					lockedDirection.Y = -lockedDirection.Y;
				}
			}

			return false;
		}

		public override bool PreDraw(ref Color lightColor) {
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
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}