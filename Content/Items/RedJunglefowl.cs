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
	public class RedJunglefowl : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 69; // nice
			Item.height = 50;
			Item.useTime = 60;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = Item.buyPrice(silver: 5);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ProjectileID.VilethornBase;
			Item.shootSpeed = 20f;
			

		}

		




		public override void AddRecipes()
		{
			Recipe RedJunglefowlRecipe = CreateRecipe();
			RedJunglefowlRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 8);
			RedJunglefowlRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 6);
			RedJunglefowlRecipe.AddIngredient(ItemID.JungleSpores, 15);
			RedJunglefowlRecipe.AddIngredient(ItemID.Stinger, 10);
			RedJunglefowlRecipe.AddIngredient(ItemID.Vine, 3);
			RedJunglefowlRecipe.AddTile(TileID.Anvils);
			RedJunglefowlRecipe.Register();

		}
	}
}

