using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Chickensubclass.Content;

namespace Chickensubclass.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class PlantBasedChickenHelmet : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(gold: 6, silver: 20);
			Item.rare = ItemRarityID.Lime;
			Item.defense = 12;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ModContent.ItemType<PlantBasedChickenBreastplate>() && legs.type == ModContent.ItemType<PlantBasedChickenGreaves>();
		}

		public override void UpdateEquip(Player player) {
			if (ChickenWeaponDamageBoost.IfUsingChickenWeapon(player))
			{
				player.GetCritChance(DamageClass.Melee) += 8f;
				player.GetDamage(DamageClass.Melee) += 0.20f;
			}
		}

		public override void UpdateArmorSet(Player player) {
			player.setBonus = "Leaf crystal protection and increased life regeneration while holding a chicken weapon";
			if (ChickenWeaponDamageBoost.IfUsingChickenWeapon(player))
			{
				player.AddBuff(BuffID.LeafCrystal, 2);
				player.lifeRegen += 4;
			}
		}

		public override void AddRecipes()
		{
			Recipe PlantBasedChickenHelmetRecipe = CreateRecipe();
			PlantBasedChickenHelmetRecipe.AddIngredient(ModContent.ItemType<Content.Items.PlantBasedNugget>(), 12);
			PlantBasedChickenHelmetRecipe.AddIngredient(ItemID.ChlorophyteBar, 6);
			PlantBasedChickenHelmetRecipe.AddTile(TileID.MythrilAnvil);
			PlantBasedChickenHelmetRecipe.Register();
		}
	}
}