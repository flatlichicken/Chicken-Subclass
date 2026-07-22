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
	public class ExplosiveChickenProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = false;
		
		}

		public override void SetDefaults() {
			Projectile.width = 14; // The width of projectile hitbox
			Projectile.height = 14; // The height of projectile hitbox
			Projectile.aiStyle = 14; // The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Melee; // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 300; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0f; // How much light emit around the projectile
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = true; // Can the projectile collide with tiles?
			Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

			AIType = ProjectileID.Glowstick; // Act exactly like default Bullet
		}
	public override bool CanHitPlayer(Player target) {
    // If the target is the person who threw the chicken, return false.
    // This stops Projectile.Damage() from hurting you in OnKill.
    if (target.whoAmI == Projectile.owner) {
        return false;
    }
    return base.CanHitPlayer(target);
	}
	public override void OnKill(int timeLeft) {
    // 1. Play the vanilla explosion sound
    SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

    // 2. VANILLA HITBOX EXPANSION LOGIC
    // First, shift position by half width/height to find the current center
    Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
    Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
    
    // Set the explosion diameter (128 is vanilla grenade standard)
    Projectile.width = 128;
    Projectile.height = 128;
    
    // Shift the top-left corner back so the NEW large hitbox is centered on the OLD center
    Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
    Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);

    // 3. FINAL DAMAGE PULSE
    // Forces the game to check for collisions with the new 128x128 box
    Projectile.Damage();

    // 4. VANILLA VISUALS (Smoke and Fire)
    for (int i = 0; i < 30; i++) {
        // Dust ID 31 is the grey smoke used for all vanilla explosives
        int smoke = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
        Main.dust[smoke].velocity *= 1.4f;

        // Dust ID 6 is the bright orange fire (Torch)
        int fire = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
        Main.dust[fire].noGravity = true;
        Main.dust[fire].velocity *= 5f;
    }
	}

		


	}
}