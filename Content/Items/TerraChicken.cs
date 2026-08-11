using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles; // the piece of code i found out i needed to type to use a custom projectile


namespace Chickensubclass.Content.Items
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class TerraChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 98;
			Item.DamageType = DamageClass.Melee;
			Item.width = 170;
			Item.height = 167;
			Item.useTime = 12;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(0, 20, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<TerraChickenProjectile>();
			Item.shootSpeed = 11f;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Generates white light inside the swinging hitbox
            Lighting.AddLight(hitbox.Center.ToVector2(), 1.0f, 1.0f, 1.0f);
        }


		public override void AddRecipes()
		{
			Recipe TerraChickenRecipe = CreateRecipe();
			TerraChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.TrueDarkChicken>());
			TerraChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.TrueHolyChicken>());
			TerraChickenRecipe.AddIngredient(ItemID.BrokenHeroSword, 1);
			TerraChickenRecipe.AddTile(TileID.MythrilAnvil);
			TerraChickenRecipe.Register();

		}
	}
}

