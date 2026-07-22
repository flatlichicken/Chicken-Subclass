using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Tiles;

namespace Chickensubclass.Content.Items
{
    public class NuggetMachine : ModItem
    {
        public override void SetDefaults() {
            // This MUST point to the exact namespace of your tile
            Item.DefaultToPlaceableTile(ModContent.TileType<NuggetMachineTile>());
            Item.width = 32;
            Item.height = 30;
            Item.value = 50000;
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes() {
        Recipe NuggetMachine = CreateRecipe();
                NuggetMachine.AddIngredient(ItemID.SoulofMight, 5);
                NuggetMachine.AddIngredient(ItemID.SoulofSight, 5);
                NuggetMachine.AddIngredient(ItemID.SoulofFright, 5);
                NuggetMachine.AddIngredient(ModContent.ItemType<Content.Items.ChickenSoul>(), 5);
                NuggetMachine.AddIngredient(ItemID.HallowedBar, 10);
                NuggetMachine.AddTile(TileID.MythrilAnvil);
                NuggetMachine.Register();
        }
    }
}