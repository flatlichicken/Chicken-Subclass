using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items
{
    public class ChickenJockey : ModItem
    {
        public override void SetDefaults() {
            Item.width = 32;
            Item.height = 38;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.LightRed; // Pink/Light Red fits Pre-Plantera progression
            Item.UseSound = SoundID.Item79;
            Item.noMelee = true;

            // Connects this item to your mount class
            Item.mountType = ModContent.MountType<Mounts.ChickenJockeyMount>();
        }
    }
}