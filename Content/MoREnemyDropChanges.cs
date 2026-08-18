using Chickensubclass.Content.Items;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Chickensubclass.Content
{
    public class MoREnemyDropChanges : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.ModNPC != null && npc.ModNPC.Mod.Name == "Redemption")
            {
                string name = npc.ModNPC.Name;

                if (name == "ChickenScratcher" || 
                    name == "ChickenBomber" || 
                    name == "RoosterBooster" || 
                    name == "Haymaker" || 
                    name == "HeadlessChicken" )
                {
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawChicken>(), 1, 0, 2));
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChickenFeather>(), 1, 0, 2));
                }

                if (name == "Cockatrice" || 
                    name == "Basan" ||
                    name == "FowlEmperor")
                {
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawChicken>(), 1, 5, 10));
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChickenFeather>(), 1, 5, 10));
                }
            }
        }
    }
}
