using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items
{
    public class GreatChickenTrophy : ModItem
    {
        public override void SetDefaults() {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            
            Item.createTile = ModContent.TileType<Tiles.GreatChickenTrophyTile>(); 
            
            Item.width = 30;
            Item.height = 30;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(gold: 2);
        }
    }
}


