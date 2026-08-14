using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Tiles;
using Chickensubclass.Content.Buffs;

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
			Item.value = 7500;
			Item.rare = ItemRarityID.Lime;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.noUseGraphic = true; 
			Item.buffType = ModContent.BuffType<ChickenPoison>();
			Item.buffTime = 7200;
			Item.useTime = 17;
			Item.useAnimation = 17;
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
			player.ClearBuff(ModContent.BuffType<ChickenVenom>());
        }
		
		public override void AddRecipes()
		{

			Recipe PlantBasedNuggetRecipe = CreateRecipe();
			PlantBasedNuggetRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 1);
			PlantBasedNuggetRecipe.AddIngredient(ItemID.ChlorophyteOre, 3);
			PlantBasedNuggetRecipe.AddTile(ModContent.TileType<NuggetMachineTile>());
			PlantBasedNuggetRecipe.Register();

			
		}

		public override void HoldItem(Player player)
		{
			if (player.itemAnimation > 0)
			{
				string heldTexturePath = Texture + "_Held";
				if (ModContent.HasAsset(heldTexturePath))
				{
					player.heldProj = -1;
				}
			}
		}

		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noMuzzle)
		{
			if (player.itemAnimation > 0 && Main.rand.NextBool(3))
			{
				int dustIndex = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.FoodPiece, 0f, 0f, 150, default(Color), 0.8f);
				Main.dust[dustIndex].velocity *= 0.5f;
			}
		}


		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	
	}
}
