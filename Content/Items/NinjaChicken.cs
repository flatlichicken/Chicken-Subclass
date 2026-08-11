using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class NinjaChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 27;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(0, 2, 0, 0);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}
		
		public override void HoldItem(Player player) {
            // Check if the player is currently using (swinging) the weapon
            if (player.itemAnimation > 0) {
                // Increase max run speed by 50%
                player.maxRunSpeed *= 1.5f; 
                // Increase acceleration to reach top speed faster
                player.runAcceleration *= 1.5f; 
            }

		}	

		public override void AddRecipes()
		{

			Recipe NinjaChickenRecipe = CreateRecipe();
			NinjaChickenRecipe.AddIngredient(ModContent.ItemType<ChickenFeather>(), 10);
			NinjaChickenRecipe.AddIngredient(ModContent.ItemType<RawChicken>(), 10);
			NinjaChickenRecipe.AddIngredient(ItemID.Muramasa, 1);
			NinjaChickenRecipe.AddTile(TileID.Anvils);
			NinjaChickenRecipe.Register();

			
		}
	}
}
