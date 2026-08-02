using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Chickensubclass.Content.Items; // the line of code that got me past one of my hardest challenges, getting past cs0246
using Chickensubclass.Content.Items.Accessories;
using Terraria.Audio;
using Chickensubclass.Content.Gores;
using Microsoft.Xna.Framework;

namespace Chickensubclass.Content.NPCs
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class GiantChicken : ModNPC
    {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie];

            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Duck;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers() {
                    Velocity = 1f,
                    Position = new Vector2(0f, 15f),            // Moves the main right-side preview UP
					Scale = 0.9f,
                    PortraitPositionYOverride = -0f,             // Slightly adjusts small left-side icon
                    PortraitPositionXOverride = -0f,
                    PortraitScale = 1f                         // Scales down large boss sprites to fit
                };
                NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            
            ContentSamples.NpcBestiaryRarityStars[Type] = 2;
        }

        public override void SetDefaults() {
            NPC.width = 36;
            NPC.height = 80;
            NPC.damage = 16;
            NPC.defense = 6;
            NPC.lifeMax = 60;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 120f;
            NPC.knockBackResist = 0.1f;
            NPC.aiStyle = 3; // Fighter AI, important to choose the aiStyle that matches the NPCID that we want to mimic
            NPC.rarity = 3;

            AIType = NPCID.AnomuraFungus; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
            AnimationType = NPCID.Zombie; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
            Banner = Item.NPCtoBanner(NPCID.Bird); // Makes this NPC get affected by the normal zombie banner.
            BannerItem = Item.BannerToItem(Banner); // Makes kills of this NPC go towards dropping the banner it's associated with.
             // Associates this NPC with the ExampleSurfaceBiome in Bestiary
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            // Since Party Zombie is essentially just another variation of Zombie, we'd like to mimic the Zombie drops.
            // To do this, we can either (1) copy the drops from the Zombie directly or (2) just recreate the drops in our code.
            // (1) Copying the drops directly means that if Terraria updates and changes the Zombie drops, your ModNPC will also inherit the changes automatically.
            // (2) Recreating the drops can give you more control if desired but requires consulting the wiki, bestiary, or source code and then writing drop code.

            // (1) This example shows copying the drops directly. For consistency and mod compatibility, we suggest using the smallest positive NPCID when dealing with npcs with many variants and shared drop pools.
            var zombieDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.Zombie, false); // false is important here
            foreach (var zombieDropRule in zombieDropRules) {
                // In this foreach loop, we simple add each drop to the PartyZombie drop pool. 
                
            }

            // (2) This example shows recreating the drops. This code is commented out because we are using the previous method instead.
            // npcLoot.Add(ItemDropRule.Common(ItemID.Shackle, 50)); // Drop shackles with a 1 out of 50 chance.
            // npcLoot.Add(ItemDropRule.Common(ItemID.ZombieArm, 250)); // Drop zombie arm with a 1 out of 250 chance.

            // Finally, we can add additional drops. Many Zombie variants have their own unique drops: https://terraria.fandom.com/wiki/Zombie
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawChicken>(), 1, 4, 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChickenFeather>(), 1, 4, 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ExpiredCoupon>(), 2, 1, 1));

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            return SpawnCondition.OverworldDaySlime.Chance * 0.012f; // Spawn with 1/5th the chance of a regular zombie.
        }

        public override void AI() {
            if (Main.rand.NextBool(600)) { 
            SoundEngine.PlaySound(new SoundStyle("Chickensubclass/Content/NPCs/GiantChickenNoise"), NPC.position);
            }

            if (NPC.wet) {
                if (NPC.honeyWet) { // Removes the effects of honey's fall rate making the NPC fall normally in honey
                    NPC.GravityMultiplier /= NPC.GravityWetMultipliers[LiquidID.Honey];
                    NPC.MaxFallSpeedMultiplier /= NPC.MaxFallSpeedWetMultipliers[LiquidID.Honey];
                }
                else if (!NPC.lavaWet && !NPC.shimmerWet) { // Removes water falls speed effects, then adds honey falls speed effects, making the NPC fall at the honey rate in water
                    NPC.GravityMultiplier *= NPC.GravityWetMultipliers[LiquidID.Honey] / NPC.GravityWetMultipliers[LiquidID.Water];
                    NPC.MaxFallSpeedMultiplier *= NPC.MaxFallSpeedWetMultipliers[LiquidID.Honey] / NPC.MaxFallSpeedWetMultipliers[LiquidID.Water];
                }
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange([
                // Sets the spawning conditions of this NPC that is listed in the bestiary.
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                // Sets the description of this NPC that is listed in the bestiary.
                new FlavorTextBestiaryInfoElement("Don't take coupons from these.")

                // By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
                // The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
                // so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
                
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit) {
            for (int i = 0; i < 10; i++) {
                int dustType = Main.rand.Next(5, 5);
                var dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, dustType);

                dust.velocity.X += Main.rand.NextFloat(-0.05f, 0.05f);
                dust.velocity.Y += Main.rand.NextFloat(-0.05f, 0.05f);

                dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
            }

            if (NPC.life <= 0) {
                int featherCount = Main.rand.Next(3, 6);

                for (int i = 0; i < featherCount; i++) {
                    Vector2 velocity = new Vector2(
                        Main.rand.NextFloat(-2f, 2f), 
                        Main.rand.NextFloat(-3f, -1f)
                    );

                    Gore.NewGore(
                        NPC.GetSource_Death(), 
                        NPC.position, 
                        velocity, 
                        ModContent.GoreType<GiantChickenFeatherGore>()
                    );
                }
            }
        }
    }
}