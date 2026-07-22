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
	public class SpicyChickenGreaves : ModItem
	{
		public override void SetStaticDefaults() {
			

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Orange; // The rarity of the item
			Item.defense = 5; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
			if (isChickenWeapon)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.08f;
        }
         else
		{
            player.GetAttackSpeed(DamageClass.Melee) += 0.03f;
        }
		}
		
		public override void AddRecipes()
		{
			Recipe SpicyChickenGreavesRecipe = CreateRecipe();
			SpicyChickenGreavesRecipe.AddIngredient(ModContent.ItemType<Content.Items.SpicyChickenNugget>(), 10);
			SpicyChickenGreavesRecipe.AddIngredient(ItemID.HellstoneBar, 5);
			SpicyChickenGreavesRecipe.AddTile(TileID.Anvils);
			SpicyChickenGreavesRecipe.Register();
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		
	}
}