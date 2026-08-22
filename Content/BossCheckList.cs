using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Chickensubclass.Content
{
    public class DownedSystem : ModSystem
    {
        public static bool downedGreatChicken = false;

        public override void OnWorldLoad() => downedGreatChicken = false;
        public override void OnWorldUnload() => downedGreatChicken = false;

        public override void SaveWorldData(TagCompound tag)
        {
            if (downedGreatChicken)
                tag["downedGreatChicken"] = true;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedGreatChicken = tag.ContainsKey("downedGreatChicken");
        }
    }

    public class BossChecklistIntegration : ModSystem
    {
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {
                bossChecklist.Call(
                    "LogBoss",
                    Mod,
                    "GreatChicken",
                    11.02f,
                    () => DownedSystem.downedGreatChicken,
                    ModContent.NPCType<NPCs.GreatChicken>(),
                    new Dictionary<string, object>
                    {
                        ["spawnItems"] = ModContent.ItemType<Items.SusEgg>(),
                        ["spawnInfo"] = Language.GetOrRegister("Mods.Chickensubclass.BossChecklist.GreatChicken.SpawnInfo", () => $"Use a [i:{ModContent.ItemType<Items.SusEgg>()}]"),
                        ["collectibles"] = new List<int>
                        {
                            ModContent.ItemType<Items.GreatChickenTreasureBag>(),
                            ModContent.ItemType<Items.GreatChickenTrophy>(),
                            ModContent.ItemType<Items.GreatChickenRelic>()
                        }
                    }
                );
            }
        }
    }
}