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
	
	public class FlockEmblem : ModItem
	{
		

		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 18;
			Item.accessory = true;
			Item.value = Item.buyPrice(gold: 7);
			Item.rare = ItemRarityID.Lime;
			 
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
    		bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
    		if (isChickenWeapon && Main.rand.NextBool(1)) {
    	    	player.GetModPlayer<ChickenDebuffInflict>().ChickenScentCheck = true;
				player.GetDamage(DamageClass.Melee) += 0.20f;
				player.GetCritChance(DamageClass.Melee) += 15f;
    		}       
		}

		public override void AddRecipes() {
			Recipe FlockEmblemRecipe = CreateRecipe();
			FlockEmblemRecipe.AddIngredient(ModContent.ItemType<Content.Items.Accessories.PoultryEmblem>(), 1);
			FlockEmblemRecipe.AddIngredient(ItemID.EyeoftheGolem, 1);
			FlockEmblemRecipe.AddTile(TileID.MythrilAnvil);
			FlockEmblemRecipe.Register();
		}
	}
}