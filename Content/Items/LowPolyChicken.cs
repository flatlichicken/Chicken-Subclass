using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Chickensubclass.Content.Projectiles;

namespace Chickensubclass.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class LowPolyChicken : ModItem
	{
		public override void SetStaticDefaults() {
			// Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 20));
		}

	
	
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 120;
			Item.DamageType = DamageClass.Melee;
			Item.width = 150;
			Item.height = 150;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 11);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<LowPolyChickenProjectile>();
			Item.shootSpeed = 12f;
		}

		public override void AddRecipes()
		{

			Recipe LowPolyChickenRecipe = CreateRecipe();
			LowPolyChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenSoul>(), 10);
			LowPolyChickenRecipe.AddIngredient(ItemID.MartianConduitPlating, 50);
			LowPolyChickenRecipe.AddIngredient(ItemID.InfluxWaver, 1);
			LowPolyChickenRecipe.AddTile(TileID.MythrilAnvil);
			LowPolyChickenRecipe.Register();

			
		}
	}
}
