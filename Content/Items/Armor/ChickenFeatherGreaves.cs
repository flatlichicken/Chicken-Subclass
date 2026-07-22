using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace Chickensubclass.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class ChickenFeatherGreaves : ModItem
	{
		public override void SetStaticDefaults() {
			

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Blue; // The rarity of the item
			Item.defense = 3; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			player.moveSpeed += 0.2f; // Increase the movement speed of the player
			player.GetCritChance(DamageClass.Melee) += 2f;
		}
		
		public override void AddRecipes()
		{
			Recipe ChickenFeatherGreavesRecipe = CreateRecipe();
			ChickenFeatherGreavesRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 25);
			ChickenFeatherGreavesRecipe.AddRecipeGroup("GoldBar", 20);
			ChickenFeatherGreavesRecipe.AddTile(TileID.Anvils);
			ChickenFeatherGreavesRecipe.Register();
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		
	}
}