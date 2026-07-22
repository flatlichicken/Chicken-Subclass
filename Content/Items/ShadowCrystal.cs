using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items
{
	public class ShadowCrystal : ModItem
	{

		public override void SetDefaults() {
			Item.width = 14;
			Item.height = 24;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = 7; 
			Item.rare = ItemRarityID.Lime;
		}


		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	
	}
}
