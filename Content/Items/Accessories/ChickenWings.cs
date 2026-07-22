using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Chickensubclass.Content.Items.Accessories
{   
	public class ChickenWings : ModItem
	{
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.value = Item.buyPrice(silver: 50);
			Item.rare = ItemRarityID.Blue;
		}

		public override void Load() {
			EquipLoader.AddEquipTexture(Mod, Texture + "_Wings", EquipType.Wings, this);
		}

		public override void UpdateVanity(Player player) {
			player.wings = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Wings);
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.wings = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Wings);
			player.noFallDmg = false;

			bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
			if (isChickenWeapon)
			{
				player.jumpSpeedBoost += 1.5f;	
			}
		}

		public override void AddRecipes()
		{
			Recipe ChickenWingsRecipe = CreateRecipe();
			ChickenWingsRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 20);
			ChickenWingsRecipe.AddTile(TileID.WorkBenches);
			ChickenWingsRecipe.Register();
		}
	}
}