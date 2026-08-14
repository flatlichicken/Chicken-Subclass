using Chickensubclass.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

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
			Item.value = Item.sellPrice(silver: 40);
			Item.rare = ItemRarityID.Green;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.noUseGraphic = true; 
			Item.buffType = ModContent.BuffType<ChickenAnger>();
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
			player.ClearBuff(ModContent.BuffType<ChickenPower>());
			player.ClearBuff(ModContent.BuffType<ChickenRage>());
			player.ClearBuff(ModContent.BuffType<ChickenPoison>());
			player.ClearBuff(ModContent.BuffType<ChickenVenom>());
		}
		
		public override void AddRecipes()
		{

			Recipe SpicyChickenNuggetRecipe = CreateRecipe();
			SpicyChickenNuggetRecipe.AddIngredient(ItemID.ChickenNugget, 1);
			SpicyChickenNuggetRecipe.AddIngredient(ItemID.Hellstone, 3);
			SpicyChickenNuggetRecipe.AddTile(TileID.CookingPots);
			SpicyChickenNuggetRecipe.Register();

			
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
