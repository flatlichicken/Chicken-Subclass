using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Chickensubclass.Content.NPCs;

namespace Chickensubclass.Content.Items
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
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Orange;
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
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 10);
				recipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 10);
				recipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenSoul>(), 10);
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.Register();
			}
    }
}
