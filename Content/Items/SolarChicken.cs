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
	public class SolarChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 112;
			Item.DamageType = DamageClass.Melee;
			Item.width = 140;
			Item.height = 128;
			Item.useTime = 10;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<SolarChickenProjectile>();
			Item.shootSpeed = 14f;


		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Generates white light inside the swinging hitbox
            Lighting.AddLight(hitbox.Center.ToVector2(), 1.0f, 1.0f, 1.0f);
        }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
{
    // Define the number of projectiles to shoot, e.g., 5 shots
    float numberProjectiles = 1;

    // Define the spread angle in degrees
    float spread = 10;

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

		public override void AddRecipes()
		{
			Recipe SolarChickenRecipe = CreateRecipe();
			SolarChickenRecipe.AddIngredient(ItemID.FragmentSolar, 18);
			SolarChickenRecipe.AddTile(TileID.LunarCraftingStation);
			SolarChickenRecipe.Register();

		}
		
	}
}

