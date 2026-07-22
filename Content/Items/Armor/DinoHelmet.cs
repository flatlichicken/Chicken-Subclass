using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Chickensubclass.Content;
using System.Linq;

namespace Chickensubclass.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class DinoHelmet : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(gold: 6, silver: 20);
			Item.rare = ItemRarityID.Yellow;
			Item.defense = 13;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ModContent.ItemType<DinoBreastplate>() && legs.type == ModContent.ItemType<DinoGreaves>();
		}

		public override void UpdateEquip(Player player) {
			if (ChickenWeaponDamageBoost.IfUsingChickenWeapon(player))
			{
				player.GetCritChance(DamageClass.Melee) += 10f;
				player.GetDamage(DamageClass.Melee) += 0.24f;
				
			}
		}


		public override void UpdateArmorSet(Player player) {
			player.setBonus = "Melee attacks with a chicken weapon cause cursed inferno";
			if (ChickenWeaponDamageBoost.IfUsingChickenWeapon(player))
			{
				player.GetModPlayer<ChickenDebuffInflict>().DinoHelmCheck = true;
			}
		}

		public override void AddRecipes()
		{
			Recipe DinoHelmetRecipe = CreateRecipe();
			DinoHelmetRecipe.AddIngredient(ModContent.ItemType<Content.Items.DinoNuggie>(), 12);
			DinoHelmetRecipe.AddIngredient(ItemID.SpectreBar, 6);
			DinoHelmetRecipe.AddTile(TileID.MythrilAnvil);
			DinoHelmetRecipe.Register();
		}
	}
}