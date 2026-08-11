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
	
	public class NuggetKnuckles : ModItem
	{
		

		public override void SetDefaults() {
			Item.width = 30;
			Item.height = 26;
			Item.accessory = true;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.defense = 4;
			
			 
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
    		bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
    		if (isChickenWeapon && Main.rand.NextBool(5)) {
    	    	player.GetModPlayer<ChickenDebuffInflict>().NuggetKnucklesCheck = true;
    		}       
		}

		public override void AddRecipes() {
			Recipe NuggetKnucklesRecipe = CreateRecipe();
			NuggetKnucklesRecipe.AddIngredient(ItemID.ChickenNugget, 10);
			NuggetKnucklesRecipe.AddIngredient(ItemID.FleshKnuckles, 1);
			NuggetKnucklesRecipe.AddTile(TileID.MythrilAnvil);
			NuggetKnucklesRecipe.Register();
		}
	}
}