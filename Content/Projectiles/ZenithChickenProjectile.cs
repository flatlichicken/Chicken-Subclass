using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Projectiles
{
	public class ZenithChickenProjectile : ModProjectile
	{
		private static Texture2D[] swordImages;
		private int chickenType = -1;

		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; 
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
		}

		public override void SetDefaults() {
			Projectile.width = 36; 
			Projectile.height = 36; 
			// Projectile.aiStyle = 168;
			Projectile.aiStyle = 1; 
			Projectile.friendly = true; 
			Projectile.hostile = false; 
			Projectile.DamageType = DamageClass.Melee; 
			Projectile.penetrate = -1; 
			Projectile.timeLeft = 120; 
			Projectile.alpha = 0; 
			Projectile.light = 0.5f; 
			Projectile.ignoreWater = true; 
			Projectile.tileCollide = false; 
			Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5; 

			AIType = ProjectileID.FirstFractal;
		}

		public override void AI() {
			
			Projectile.ai[0] = 0f;
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.IchorTorch);
			dust.type = DustID.IchorTorch;
			dust.color = new Color(255, 255, 0);
			dust.noGravity = true;
			dust.scale = 1.3f;
			dust.velocity *= 0.2f;
			
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
            Player player = Main.player[Projectile.owner];

            if (swordImages == null) {
                swordImages = new Texture2D[] {
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/BeeChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/NuggetChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/DarkChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/HolyChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/TrueDarkChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/TrueHolyChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/TerraChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/Chicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/MagicChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/FireChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/RedJunglefowl", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/NinjaChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/EvilChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/RoaringChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/LowPolyChickenSprite", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
                    ModContent.Request<Texture2D>("Chickensubclass/Content/Items/BinChicken", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value
                };
            }

            if (chickenType == -1) {
                chickenType = Main.rand.Next(swordImages.Length);
            }

            Texture2D currentSword = swordImages[chickenType];
            Vector2 origin = new Vector2(currentSword.Width / 2f, currentSword.Height / 2f);

            for (int i = 0; i < Projectile.oldPos.Length; i++) {
                if (Projectile.oldPos[i] == Vector2.Zero) continue;
                Vector2 drawPos = Projectile.oldPos[i] + (Projectile.Size / 2f) - Main.screenPosition;
                Color trailColor = lightColor * ((float)(Projectile.oldPos.Length - i) / Projectile.oldPos.Length) * 0.5f;
                Main.spriteBatch.Draw(currentSword, drawPos, null, trailColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
            }

            Main.spriteBatch.Draw(
                currentSword,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.rotation + 90f,
                origin,
                Projectile.scale,
                SpriteEffects.None,
                0f
            );

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            Vector2 originalPosition = player.position;
            player.position = Projectile.Center - new Vector2(player.width / 2f, player.height / 2f);

            Player tempPlayer = new Player {
                position = player.position, width = player.width, height = player.height, direction = player.direction,
                Male = player.Male, skinVariant = player.skinVariant, hair = player.hair, hairDye = player.hairDye,
                hairColor = player.hairColor, skinColor = player.skinColor, eyeColor = player.eyeColor,
                shirtColor = player.shirtColor, underShirtColor = player.underShirtColor, pantsColor = player.pantsColor, shoeColor = player.shoeColor,
                head = player.head, body = player.body, legs = player.legs,
                wings = player.wings, back = player.back, front = player.front, shoe = player.shoe,
                waist = player.waist, shield = player.shield, neck = player.neck, handon = player.handon, handoff = player.handoff
            };

            for (int index = 0; index < player.armor.Length; index++) {
                tempPlayer.armor[index] = player.armor[index].Clone();
            }
            for (int index = 0; index < player.dye.Length; index++) {
                tempPlayer.dye[index] = player.dye[index].Clone();
            }
            for (int index = 0; index < player.hideVisibleAccessory.Length; index++) {
                tempPlayer.hideVisibleAccessory[index] = player.hideVisibleAccessory[index];
            }

            tempPlayer.PlayerFrame();
			tempPlayer.bodyFrame.Y = tempPlayer.bodyFrame.Height * 5;
            tempPlayer.legFrame.Y = tempPlayer.legFrame.Height * 5;
            tempPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.ToRadians(90f));
            Main.PlayerRenderer.DrawPlayer(Main.Camera, tempPlayer, tempPlayer.position, 0, tempPlayer.fullRotationOrigin, 0f);
            player.position = originalPosition;

            return false; // do not return true
        }

		public override void OnKill(int timeLeft) {
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
		}
	}
}