using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items
{
    public class SoulBottleChicken : ModItem
    {
        public override void SetDefaults() {
            Item.width = 10;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 0, 30);
            
            Item.createTile = ModContent.TileType<Tiles.SoulBottleChickenTile>();
            Item.placeStyle = 0;
        }

        public override void AddRecipes()
		{
			Recipe SoulBottleChickenRecipe = CreateRecipe();
            SoulBottleChickenRecipe.AddIngredient(ItemID.Bottle, 1);
			SoulBottleChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenSoul>(), 1);
			SoulBottleChickenRecipe.Register();

		}
    }
}


