using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Chickensubclass.Content;

namespace Chickensubclass.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Head)]
	public class FrostBeak : ModItem
	{
		public override void SetStaticDefaults() {
			

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;

			// If your head equipment should draw hair while drawn, use one of the following:
			// ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
			// ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
			// ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
			// ArmorIDs.Head.Sets.DrawBackHair[Item.headSlot] = true;
			// ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true; 
		}

		public override void SetDefaults() {
			Item.width = 18; 
			Item.height = 18; 
			Item.value = Item.sellPrice(gold: 8); 
			Item.rare = ItemRarityID.Pink; 
			Item.defense = 5; 
		}

		// IsArmorSet determines what armor pieces are needed for the setbonus to take effect
		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ItemID.FrostBreastplate && legs.type == ItemID.FrostLeggings;
		}

		public override void UpdateEquip(Player player) {
			bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
			if (isChickenWeapon)
        {
            player.GetDamage(DamageClass.Melee) += 0.20f;
        }

		}

		// UpdateArmorSet allows you to give set bonuses to the armor.
		public override void UpdateArmorSet(Player player) {
			player.setBonus = "Melee attacks with a chicken weapon cause frostburn\n12% increased melee damage when holding a chicken weapon"; // This is the setbonus tooltip
			bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(Main.LocalPlayer);
    	 if (isChickenWeapon)
        {
            player.GetDamage(DamageClass.Melee) += 0.12f;
			player.GetModPlayer<ChickenDebuffInflict>().FrostBeakCheck = true;
        }

			
		}


		public override void AddRecipes()
		{
			Recipe FrostBeakRecipe = CreateRecipe();
		    FrostBeakRecipe.AddRecipeGroup("AdamantiteBar", 20);
			FrostBeakRecipe.AddIngredient(ItemID.FrostCore, 1);
			FrostBeakRecipe.AddTile(TileID.MythrilAnvil);
			FrostBeakRecipe.Register();

		}
		
	}
}