using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Chickensubclass.Content;

namespace Chickensubclass.Content.Items.Accessories
{
	
	public class ChickenScent : ModItem
	{
		

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 24;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.LightPurple;
			 
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
    		bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
    		if (isChickenWeapon && Main.rand.NextBool(1)) {
    	    	player.GetModPlayer<ChickenDebuffInflict>().ChickenScentCheck = true;
				player.GetDamage(DamageClass.Melee) += 0.08f;
				player.GetCritChance(DamageClass.Melee) += 8f;
    		}       
		}

		public override void AddRecipes() {
			Recipe ChickenScentRecipe = CreateRecipe();
			ChickenScentRecipe.AddIngredient(ItemID.ChickenNugget, 10);
			ChickenScentRecipe.AddIngredient(ItemID.PutridScent, 1);
			ChickenScentRecipe.AddTile(TileID.MythrilAnvil);
			ChickenScentRecipe.Register();
		}
	}
}