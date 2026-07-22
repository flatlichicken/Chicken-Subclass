using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Tiles;

namespace Chickensubclass.Content.Items
{
	public class PlantBasedNugget : ModItem
	{
		public override void SetStaticDefaults() {

			Item.ResearchUnlockCount = 25; // Configure the amount of this item that's needed to research it in Journey mode.
			
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = 5000;
			Item.rare = ItemRarityID.Lime;
		}
		
		public override void AddRecipes()
		{

			Recipe PlantBasedNuggetRecipe = CreateRecipe();
			PlantBasedNuggetRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 1);
			PlantBasedNuggetRecipe.AddIngredient(ItemID.ChlorophyteOre, 3);
			PlantBasedNuggetRecipe.AddTile(ModContent.TileType<NuggetMachineTile>());
			PlantBasedNuggetRecipe.Register();

			
		}


		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	
	}
}
