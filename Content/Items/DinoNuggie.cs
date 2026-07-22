using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Tiles;

namespace Chickensubclass.Content.Items
{
	public class DinoNuggie : ModItem
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
			Item.rare = ItemRarityID.Yellow;
			
		}
		
		public override void AddRecipes()
		{

			Recipe DinoNuggieRecipe = CreateRecipe(2);
			DinoNuggieRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 2);
			DinoNuggieRecipe.AddIngredient(ItemID.LunarTabletFragment, 1);
			DinoNuggieRecipe.AddTile(ModContent.TileType<NuggetMachineTile>());
			DinoNuggieRecipe.Register();

			
		}


		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	
	}
}
