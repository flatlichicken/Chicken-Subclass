using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting a X_Body.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class ChickenFeatherBreastplate: ModItem
	{
		

		

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Blue; // The rarity of the item
			Item.defense = 4; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			player.GetDamage(DamageClass.Melee) += 0.05f; // Increase dealt damage for all weapon classes by 20%
		}

		public override void AddRecipes()
		{
			Recipe ChickenFeatherBreastplateRecipe = CreateRecipe();
			ChickenFeatherBreastplateRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 30);
			ChickenFeatherBreastplateRecipe.AddRecipeGroup("GoldBar", 25);
			ChickenFeatherBreastplateRecipe.AddTile(TileID.Anvils);
			ChickenFeatherBreastplateRecipe.Register();
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		
	}
}