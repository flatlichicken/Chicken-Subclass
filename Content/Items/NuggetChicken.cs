using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles; // the piece of code i found out i needed to type to use a custom projectile
using Terraria.GameContent;


namespace Chickensubclass.Content.Items
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class NuggetChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 31;
			Item.DamageType = DamageClass.Melee;
			Item.width = 55;
			Item.height = 60;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<NuggetChickenProjectile>();
			Item.shootSpeed = 12f;
		}


		public override void AddRecipes()
		{
			Recipe NuggetChickenRecipe = CreateRecipe();
			NuggetChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 15);
			NuggetChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 10);
			NuggetChickenRecipe.AddIngredient(ItemID.CrimtaneBar, 10);
			NuggetChickenRecipe.AddIngredient(ItemID.TissueSample, 5);
			NuggetChickenRecipe.AddTile(TileID.Anvils);
			NuggetChickenRecipe.Register();

		}
	}
}

