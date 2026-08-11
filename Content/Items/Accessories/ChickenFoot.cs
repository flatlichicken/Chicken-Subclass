using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items.Accessories
{
	[AutoloadEquip(EquipType.Shoes)]
	public class ChickenFoot : ModItem
	{
		private static bool AccWorn;

		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 18;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 35);
			Item.rare = ItemRarityID.Blue;

			// Link the LegSlot for the 1120px texture
			 
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			// Restored your original Main.LocalPlayer check
			bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
			bool usingChickenOrb = player.GetModPlayer<ChickenOrbLogic>().UsingChickenOrb;
			if (isChickenWeapon) {
				player.moveSpeed += 0.15f;
			}
			AccWorn = true;

			if (usingChickenOrb) {
                player.accRunSpeed = 6f;
            }
		}

		public static bool FootActive() {
            if (AccWorn == true) {
                AccWorn = false;
                return true;
            }
            else {
                return false;
            }
        }

		public override void AddRecipes() {
			Recipe RawChickenRecipe = CreateRecipe();
			RawChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 20);
			RawChickenRecipe.AddTile(TileID.WorkBenches);
			RawChickenRecipe.Register();
		}
	}
}