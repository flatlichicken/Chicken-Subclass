using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Chickensubclass.Content.Items; // the line of code that got me past one of my hardest challenges, getting past cs0246
using Chickensubclass.Content.Items.Accessories;

namespace Chickensubclass.Content.Items.Accessories
{
	
	public class ExpiredCoupon : ModItem
	{
		

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 10;
			Item.accessory = true;
			Item.value = Item.buyPrice(copper: 0);
			Item.rare = ItemRarityID.Blue;

			// Link the LegSlot for the 1120px texture
			 
		}
		
		public override void SetStaticDefaults() {
        // Shimmer this item into another specific item
        ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Coupon>(); 
    	}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			// Restored your original Main.LocalPlayer check
			bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
			if (isChickenWeapon) {
				player.GetDamage(DamageClass.Melee) += 0.05f;
		        player.GetCritChance(DamageClass.Melee) += 2f;
				player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
				player.statDefense += 3;
			}	
		}

	}
}