using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Chickensubclass.Content.Items; // the line of code that got me past one of my hardest challenges, getting past cs0246
using Chickensubclass.Content.Items.Accessories;

namespace Chickensubclass.Content.Items
{
	
	public class Coupon : ModItem
	{
		

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 10;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.value = Item.buyPrice(copper: 0);
			Item.rare = ItemRarityID.Blue;

		}

		public override void SetStaticDefaults() {
        // Shimmer this item into another specific item
        ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<ExpiredCoupon>();
    	}

		public override bool CanRightClick() {
        return true; // Allows the bag to be opened
        }

		public override void RightClick(Player player) {
        
        player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemID.GoldCoin, 5);
		}

		

	}
}