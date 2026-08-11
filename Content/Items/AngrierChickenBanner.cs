using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items
{
    public class AngrierChickenBanner : ModItem
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
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            
            Item.createTile = ModContent.TileType<Tiles.AngrierChickenBannerTile>();
            Item.placeStyle = 0;
        }
    }
}