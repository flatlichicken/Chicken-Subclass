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
	public class NuclearChickenProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
		}

		public override void SetDefaults() {
			Projectile.width = 76; // The width of projectile hitbox
			Projectile.height = 80; // The height of projectile hitbox
			Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Melee; // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 180; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f; // How much light emit around the projectile
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = true; // Can the projectile collide with tiles?
			Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

			AIType = ProjectileID.Bullet;
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
    	SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
	
    	// We spawn the effects BEFORE resizing the hitbox so they stay centered
    	for (int i = 0; i < 80; i++)
    	{
    	    // SPEED: Increased to 10-25 for a "snap" effect
    	    Vector2 speed = Main.rand.NextVector2Unit() * Main.rand.NextFloat(10f, 25f); 
	
    	    // GREEN SPARKS (The Nuclear Glow)
    	    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, speed.X, speed.Y, 100, default, 4f);
    	    dust.noGravity = true;
    	    dust.fadeIn = 1.2f; // Makes them burst out
			dust.velocity *= 10f; // Extra kick
       
	
    	    // CLOUDS: Added every 2nd loop for a thick smoke effect
    	    if (i % 2 == 0) {
    	        Vector2 cloudSpeed = Main.rand.NextVector2Unit() * Main.rand.NextFloat(5f, 15f);
    	        Dust cloud = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Cloud, cloudSpeed.X, cloudSpeed.Y, 100, default, 2.5f);
    	        cloud.noGravity = true;
    	        cloud.fadeIn = 2f; // Makes the clouds "puff up" fast
    	        cloud.color = Color.Green; // Tints the clouds green
    	    }
    	}

    	if (Projectile.owner == Main.myPlayer)
    	{
    	    // Resize for damage
    	    Projectile.width = 186;
    	    Projectile.height = 186;
    	    Projectile.Center = Projectile.Center; // Cleaner way to re-center
    	    Projectile.damage = 140;
    	    Projectile.Damage();
    	}
        }	
		}
}