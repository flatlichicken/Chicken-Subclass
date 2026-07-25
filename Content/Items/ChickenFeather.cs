using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items
{
	public class ChickenFeather : ModItem
	{
		public override void SetStaticDefaults() {

			Item.ResearchUnlockCount = 25; // Configure the amount of this item that's needed to research it in Journey mode.
			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<RawChicken>();
		}

		public override void SetDefaults() {
			Item.width = 18;
			Item.height = 18;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = 7; 
			Item.rare = ItemRarityID.White;
		}


		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	
	}
}
