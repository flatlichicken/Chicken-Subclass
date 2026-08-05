using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Chickensubclass.Content.NPCs;

namespace Chickensubclass.Items
{
    public class SusEgg : ModItem
    {
        public override void SetStaticDefaults() {
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 12;
        }

        public override void SetDefaults() {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 20;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player) {
            return !NPC.AnyNPCs(ModContent.NPCType<GreatChicken>());
        }

        public override bool? UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer) {
                SoundEngine.PlaySound(SoundID.Roar, player.position);
                int spawnSource = player.whoAmI;
                int bossType = ModContent.NPCType<GreatChicken>();

                if (Main.netMode != NetmodeID.MultiplayerClient) {
                    NPC.SpawnOnPlayer(spawnSource, bossType);
                } else {
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: spawnSource, number2: bossType);
                }
            }
            return true;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
