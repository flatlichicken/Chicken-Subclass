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
	public class LowPolyChickenProjectile2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; 
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
			Main.projFrames[Projectile.type] = 10;
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
			Projectile.light = 0f; 
			Projectile.ignoreWater = true; 
			Projectile.tileCollide = true; 
			Projectile.extraUpdates = 1; 
			

			AIType = ProjectileID.Bullet; 
		}

		public override void AI() 
{
	if (++Projectile.frameCounter >= 5) {
        Projectile.frameCounter = 0;
        if (++Projectile.frame >= Main.projFrames[Projectile.type]) {
            Projectile.frame = 0;
        }
    }
	
	Projectile.velocity.Y += 0.3f; // Adjust for heavier/lighter feel

    // Simulate Walking (Horizontal Movement)
    if (Projectile.velocity.X == 0) // Example: start moving if stopped
    {
        Projectile.velocity.X = 3f * Projectile.direction;
    }

    // Optional: Jumping over holes (detects if tiles in front are empty)
    // If(Collision.SolidCollision(position_ahead, width, height) == false) { jump }

    // Make the projectile face the right direction
    Projectile.spriteDirection = Projectile.direction;


}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			if (Projectile.penetrate <= 0) {
				if (Projectile.owner == Main.myPlayer) {
				
				}
				
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
    // 1. Get the texture and frame info
    Texture2D texture = TextureAssets.Projectile[Type].Value;
    int frameHeight = texture.Height / Main.projFrames[Projectile.type];
    Rectangle sourceRectangle = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
    
    // 2. Set the origin to the center of a single frame
    Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, frameHeight * 0.5f);
    
    // 3. Determine flip effect
    SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

    // 4. MANUALLY DRAW THE SPRITE
    // We use Projectile.Center so it stays perfectly locked to the hitbox square
    Main.EntitySpriteDraw(
        texture, 
        Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), 
        sourceRectangle, 
        lightColor, 
        Projectile.rotation, 
        drawOrigin, 
        Projectile.scale, 
        effects, 
        0
    );

    // 5. Return false so Terraria doesn't draw the "broken" default version over yours
    return false;
}
		public override void OnKill(int timeLeft) {
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}