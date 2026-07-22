using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Tiles;

namespace Chickensubclass.Content.Items
{
	public class SolarNugget : ModItem
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
			Item.rare = ItemRarityID.Cyan;
		}
		
		//public override void AddRecipes()
		//{

			//Recipe SolarNuggetRecipe = CreateRecipe();
			//SolarNuggetRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 1);
			//SolarNuggetRecipe.AddIngredient(ItemID.FragmentSolar, 2);
			//SolarNuggetRecipe.AddTile(ModContent.TileType<NuggetMachineTile>());
			//SolarNuggetRecipe.Register();

			
		//}


		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	
	}
}
