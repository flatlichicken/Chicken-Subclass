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
	public class SpicyChickenBreastplate: ModItem
	{
		

		

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Orange; // The rarity of the item
			Item.defense = 6; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
			if (isChickenWeapon)
        {
            player.GetDamage(DamageClass.Melee) += 0.08f;
        }
         else
		{
            player.GetDamage(DamageClass.Melee) += 0.03f;
        }
		}

		public override void AddRecipes()
		{
			Recipe SpicyChickenBreastplateRecipe = CreateRecipe();
			SpicyChickenBreastplateRecipe.AddIngredient(ModContent.ItemType<Content.Items.SpicyChickenNugget>(), 15);
			SpicyChickenBreastplateRecipe.AddIngredient(ItemID.HellstoneBar, 5);
			SpicyChickenBreastplateRecipe.AddTile(TileID.Anvils);
			SpicyChickenBreastplateRecipe.Register();
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		
	}
}