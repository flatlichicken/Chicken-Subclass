using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Items.Accessories
{
    public class ChickenOrb : ModItem
    {
        private static bool AccWorn;

        public override void SetStaticDefaults() {
            ItemID.Sets.ItemNoGravity[Type] = true; 
        }

        public override void SetDefaults() {
            Item.width = 32;
            Item.height = 38;
            Item.accessory = true;
            Item.expert = true;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Expert;
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            AccWorn = true;
        }

        public static bool OrbActive() {
            if (AccWorn == true) {
                AccWorn = false;
                return true;
            }
            else {
                return false;
            }
        }
    }

    public class ChickenOrbLogic : ModPlayer
    {
        public bool UsingChickenOrb;

        public override void PostUpdateEquips() {
            UsingChickenOrb = ChickenOrb.OrbActive();

            if (UsingChickenOrb) {
                Player.wingTimeMax = 100;
            }
        }
    }
}