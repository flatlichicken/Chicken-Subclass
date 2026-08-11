using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles;


namespace Chickensubclass.Content.Items
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class ExplosiveChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 45;
			Item.DamageType = DamageClass.Melee;
			Item.width = 55;
			Item.height = 60;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(0, 2, 10, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<ExplosiveChickenProjectile>();
			Item.shootSpeed = 12f;
		}


		public override void AddRecipes()
		{
			Recipe ExplosiveChickenRecipe = CreateRecipe();
			ExplosiveChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenSoul>(), 15);
			ExplosiveChickenRecipe.AddIngredient(ItemID.Grenade, 100);
			ExplosiveChickenRecipe.AddTile(TileID.MythrilAnvil);
			ExplosiveChickenRecipe.Register();

		}
	}
}

