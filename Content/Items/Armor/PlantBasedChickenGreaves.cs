using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Chickensubclass.Content;

namespace Chickensubclass.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class PlantBasedChickenGreaves : ModItem
	{
		public override void SetStaticDefaults() {
			

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 4); // How many coins the item is worth
			Item.rare = ItemRarityID.Lime; // The rarity of the item
			Item.defense = 10; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			player.GetCritChance(DamageClass.Melee) += 10f;
			player.moveSpeed += 0.07f;
		}
		
		
		public override void AddRecipes()
		{
			Recipe PlantBasedChickenGreavesRecipe = CreateRecipe();
			PlantBasedChickenGreavesRecipe.AddIngredient(ModContent.ItemType<Content.Items.PlantBasedNugget>(), 18);
			PlantBasedChickenGreavesRecipe.AddIngredient(ItemID.ChlorophyteBar, 8);
			PlantBasedChickenGreavesRecipe.AddTile(TileID.MythrilAnvil);
			PlantBasedChickenGreavesRecipe.Register();
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		
	}
}