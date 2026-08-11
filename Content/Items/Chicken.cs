using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Chickensubclass.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class Chicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 24;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(0, 0, 15, 0);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void AddRecipes()
		{

			Recipe ChickenRecipe = CreateRecipe();
			ChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 10);
			ChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 10);
			ChickenRecipe.AddTile(TileID.Anvils);
			ChickenRecipe.Register();

			
		}
	}
}
