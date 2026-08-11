using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles; // the piece of code i found out i needed to type to use a custom projectile


namespace Chickensubclass.Content.Items
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class FireChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 17;
			Item.DamageType = DamageClass.Melee;
			Item.width = 51;
			Item.height = 48;
			Item.useTime = 40;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 1, 25, 0);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<ChickenFireFeatherProjectile>();
			Item.shootSpeed = 7f;


		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Generates white light inside the swinging hitbox
            Lighting.AddLight(hitbox.Center.ToVector2(), 1.0f, 1.0f, 1.0f);
        }
		
		

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
{
    // Define the number of projectiles to shoot, e.g., 5 shots
    float numberProjectiles = 5;

    // Define the spread angle in degrees
    float spread = 20;

    for (int i = 0; i < numberProjectiles; i++)
    {
        // Randomize the velocity of each projectile
        Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread));

        // Spawn the projectile
        Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
    }

    // Return false to stop the default projectile from being spawned
    return false;
}

public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
    // Inflicts "On Fire!" for 3 seconds (180 frames)
    target.AddBuff(BuffID.OnFire, 180); 
}


		public override void AddRecipes()
		{
			Recipe FireChickenRecipe = CreateRecipe();
			FireChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.FlyingChicken>(), 1);
			FireChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 10);
			FireChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.SpicyChickenNugget>(), 15);
			FireChickenRecipe.AddIngredient(ItemID.HellstoneBar, 10);
			FireChickenRecipe.AddTile(TileID.Anvils);
			FireChickenRecipe.Register();

		}
		
	}
}

