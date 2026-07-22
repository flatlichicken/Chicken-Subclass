using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Tiles;

namespace Chickensubclass.Content.Items
{
    public class PieMachine : ModItem
    {
        public override void SetDefaults() {
            // This MUST point to the exact namespace of your tile
            Item.DefaultToPlaceableTile(ModContent.TileType<PieMachineTile>());
            Item.width = 71;
            Item.height = 40;
            Item.value = 50000;
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes() {
        Recipe PieMachine = CreateRecipe();
                PieMachine.AddIngredient(ItemID.HallowedBar, 10);
                PieMachine.AddTile(TileID.MythrilAnvil);
                PieMachine.Register();
        }
    }
}