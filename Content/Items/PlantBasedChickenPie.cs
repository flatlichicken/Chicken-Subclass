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
	public class PlantBasedChickenPie : ModItem
	{
		public override void SetStaticDefaults() {

			Item.ResearchUnlockCount = 30; // Configure the amount of this item that's needed to research it in Journey mode.
			
		}

		public override void SetDefaults() {
			Item.width = 34;
			Item.height = 22;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(gold: 1, silver: 80);
			Item.rare = ItemRarityID.Green;
			Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.EatFood;
			Item.buffType = ModContent.BuffType<ChickenVenom>();
            Item.buffTime = 36000;
			Item.consumable = true;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item2; 
		}

		public override void OnConsumeItem(Player player)
        {
            player.ClearBuff(ModContent.BuffType<ChickenInstinct>());
			player.ClearBuff(ModContent.BuffType<ChickenAnger>());
			player.ClearBuff(ModContent.BuffType<ChickenRage>());
			player.ClearBuff(ModContent.BuffType<ChickenPower>());
			player.ClearBuff(ModContent.BuffType<ChickenPoison>());
        }


		public override void AddRecipes()
		{
			Recipe PlantBasedPieRecipe = CreateRecipe();
			PlantBasedPieRecipe.AddIngredient(ModContent.ItemType<Content.Items.PlantBasedNugget>(), 5);
			PlantBasedPieRecipe.AddTile(ModContent.TileType<PieMachineTile>());
			PlantBasedPieRecipe.Register();

			
		}
	
	}
}
