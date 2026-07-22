using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Tiles;
using Chickensubclass.Content.Buffs;

namespace Chickensubclass.Content.Items
{
	public class ChickenPie : ModItem
	{
		public override void SetStaticDefaults() {

			Item.ResearchUnlockCount = 30; // Configure the amount of this item that's needed to research it in Journey mode.
			
		}

		public override void SetDefaults() {
			Item.width = 34;
			Item.height = 22;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = 7; 
			Item.rare = ItemRarityID.Blue;
			Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.EatFood;
			Item.buffType = ModContent.BuffType<ChickenPower>();
            Item.buffTime = 36000;
			Item.consumable = true;
		}

		public override void OnConsumeItem(Player player)
        {
            player.ClearBuff(ModContent.BuffType<ChickenInstinct>());
        }


		public override void AddRecipes()
		{
			Recipe ChickenPieRecipe = CreateRecipe();
			ChickenPieRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 5);
			ChickenPieRecipe.AddTile(ModContent.TileType<PieMachineTile>());
			ChickenPieRecipe.Register();

			
		}
	
	}
}
