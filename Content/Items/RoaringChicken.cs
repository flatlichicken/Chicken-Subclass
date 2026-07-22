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
	public class RoaringChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 115;
			Item.DamageType = DamageClass.Melee;
			Item.width = 51;
			Item.height = 48;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<RoaringChickenProjectile>();
			Item.shootSpeed = 0f;


		}
		
		

		public override void AddRecipes()
		{
			Recipe RoaringChickenRecipe = CreateRecipe();
			RoaringChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ShadowCrystal>(), 1);
			RoaringChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenSoul>(), 20);
			RoaringChickenRecipe.AddIngredient(ItemID.SoulofNight, 20);
			RoaringChickenRecipe.AddTile(TileID.MythrilAnvil);
			RoaringChickenRecipe.Register();

		}
		
	}
}

