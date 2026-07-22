using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items
{
	public class SpicyChickenNugget : ModItem
	{
		public override void SetStaticDefaults() {

			Item.ResearchUnlockCount = 25; // Configure the amount of this item that's needed to research it in Journey mode.
			
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = 300;
			Item.value = Item.sellPrice(silver: 40);
			Item.rare = ItemRarityID.Green;
		}
		
		public override void AddRecipes()
		{

			Recipe SpicyChickenNuggetRecipe = CreateRecipe();
			SpicyChickenNuggetRecipe.AddIngredient(ItemID.ChickenNugget, 1);
			SpicyChickenNuggetRecipe.AddIngredient(ItemID.Hellstone, 3);
			SpicyChickenNuggetRecipe.AddTile(TileID.CookingPots);
			SpicyChickenNuggetRecipe.Register();

			
		}


		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	
	}
}
