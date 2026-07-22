using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items.Accessories
{
	
	public class PoultryEmblem : ModItem
	{
		

		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 18;
			Item.accessory = true;
			Item.value = Item.buyPrice(gold: 2);
			Item.rare = ItemRarityID.LightRed;

			// Link the LegSlot for the 1120px texture
			 
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			
			bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
			if (isChickenWeapon) {
				player.GetDamage(DamageClass.Melee) += 0.20f;
			}		
		}

		public override void AddRecipes() {
			Recipe PoultryEmblemRecipe = CreateRecipe();
			PoultryEmblemRecipe.AddIngredient(ItemID.WarriorEmblem, 1);
			PoultryEmblemRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenSoul>(), 10);
			PoultryEmblemRecipe.AddTile(TileID.MythrilAnvil);
			PoultryEmblemRecipe.Register();
		}
	}
}