using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Tiles;

namespace Chickensubclass.Content.Items
{
    public class AngryChickenBanner : ModItem
    {
        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(ModContent.TileType<AngryChickenBannerTile>(), tileStyleToPlace: 0);
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
            
            Item.createTile = ModContent.TileType<Tiles.AngryChickenBannerTile>();
            Item.placeStyle = 0;
        }
    }
}


