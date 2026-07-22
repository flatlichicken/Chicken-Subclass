using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Chickensubclass.Content;

namespace Chickensubclass.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting a X_Body.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class PlantBasedChickenBreastplate: ModItem
	{
		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 5, silver: 20); // How many coins the item is worth
			Item.rare = ItemRarityID.Lime; // The rarity of the item
			Item.defense = 15; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			player.GetDamage(DamageClass.Melee) += 0.08f;
			player.GetCritChance(DamageClass.Melee) += 10f;
		}

		public override void AddRecipes()
		{
			Recipe PlantBasedChickenBreastplateRecipe = CreateRecipe();
			PlantBasedChickenBreastplateRecipe.AddIngredient(ModContent.ItemType<Content.Items.PlantBasedNugget>(), 24);
			PlantBasedChickenBreastplateRecipe.AddIngredient(ItemID.ChlorophyteBar, 10);
			PlantBasedChickenBreastplateRecipe.AddTile(TileID.MythrilAnvil);
			PlantBasedChickenBreastplateRecipe.Register();
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		
	}
}