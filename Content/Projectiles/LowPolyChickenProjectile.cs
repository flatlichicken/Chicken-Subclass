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
	public class LowPolyChickenProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; 
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
			Main.projFrames[Projectile.type] = 10;
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
		}

		public override void SetDefaults() {
			Projectile.width = 50; 
			Projectile.height = 50; 
			Projectile.aiStyle = 1; 
			Projectile.friendly = true; 
			Projectile.hostile = false; 
			Projectile.DamageType = DamageClass.Melee; 
			Projectile.penetrate = 3; 
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

	Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;
	Projectile.rotation = Projectile.velocity.ToRotation();

	if (Projectile.spriteDirection == -1) {
		Projectile.rotation += MathHelper.Pi;
	}

    Projectile.velocity.Y += 0.05f; 

    if (Projectile.velocity.Y > 16f) 
    {
        Projectile.velocity.Y = 16f;
    }

	
}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			Projectile.penetrate--;
			if (Projectile.penetrate <= 3) {
				if (Projectile.owner == Main.myPlayer) {
					Projectile.NewProjectile(
						Projectile.GetSource_FromThis(), 
						Projectile.Center, 
						oldVelocity, 
						ModContent.ProjectileType<LowPolyChickenProjectile2>(),
						Projectile.damage, 
						Projectile.knockBack, 
						Projectile.owner
					);
				}
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