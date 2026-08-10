using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items
{
    public class GiantChickenBanner : ModItem
    {
        public override void SetDefaults() {
            Item.Width = 10;
            Item.Height = 24;
            Item.MaxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            
            Item.createTile = ModContent.TileType<Tiles.GiantChickenBannerTile>();
            Item.placeStyle = 0;
        }
    }
}


