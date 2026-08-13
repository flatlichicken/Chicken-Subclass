using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Chickensubclass.Content.Items;
using Terraria.Audio;
using Chickensubclass.Content.Gores;
using Microsoft.Xna.Framework;

namespace Chickensubclass.Content.NPCs
{
    public class Chickemonium : ModNPC
    {
        private bool Enraged = false;
        private bool RoamStarted = false;
		private int soundTimer = 0;

		private float speed = 0;

        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = 2;

            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Duck;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers() {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults() {
            NPC.width = 60;
            NPC.height = 60;
            NPC.damage = 8;
            NPC.defense = 3;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<Items.AngryChickenBanner>();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawChicken>(), 1, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChickenFeather>(), 1, 1, 3));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            return SpawnCondition.OverworldDaySlime.Chance * 0f;
        }

        public override void FindFrame(int frameHeight) {
            if (Enraged) {
                NPC.frame.Y = frameHeight;
            }
            else {
                NPC.frame.Y = 0;
            }
        }

        public override void AI()
        {
            Player targetPlayer = Main.player[NPC.target];

            if (!targetPlayer.active || targetPlayer.dead)
            {
                return;
            }

            float maximumRange = 800f;

            if (Vector2.Distance(NPC.Center, targetPlayer.Center) > maximumRange)
            {
                return;
            }

            if (Collision.CanHit(NPC.Center, 1, 1, targetPlayer.Center, 1, 1))
            {
                Enraged = true;
            }

            Vector2 dashDirection = Vector2.Zero;

            if (!RoamStarted)
            {
                dashDirection = targetPlayer.Center - NPC.Center;
                SoundEngine.PlaySound(new SoundStyle("Chickensubclass/Content/NPCs/ChickemoniumDocile"), NPC.position);
                RoamStarted = true;
            }

            if (Enraged)
            {
                dashDirection = targetPlayer.Center - NPC.Center;
				
				soundTimer--;
                if (soundTimer <= 0)
                {
                    SoundEngine.PlaySound(new SoundStyle("Chickensubclass/Content/NPCs/ChickemoniumAttacking"), NPC.position);
                    soundTimer = 420;
                }
            }

            if (dashDirection != Vector2.Zero)
            {
                dashDirection.Normalize();
                NPC.velocity = dashDirection * 4f;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.Info.AddRange([
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
                new FlavorTextBestiaryInfoElement("C-367 was initially discovered in 1962 near a hydrothermal vent within the Let-Vand zone during the first expedition and excavation of the Hadal Blacksite. Due to its high threat level, early recommendations left C-367 undisturbed and advised personnel to avoid its known roaming areas.")
            ]);
        }

        public override void HitEffect(NPC.HitInfo hit) {
            for (int index = 0; index < 10; index++) {
                int dustType = Main.rand.Next(5, 5);
                var dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, dustType);

                dust.velocity.X += Main.rand.NextFloat(-0.05f, 0.05f);
                dust.velocity.Y += Main.rand.NextFloat(-0.05f, 0.05f);

                dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
            }

            if (NPC.life <= 0) {
                int featherCount = Main.rand.Next(2, 5);

                for (int index = 0; index < featherCount; index++) {
                    Vector2 velocity = new Vector2(
                        Main.rand.NextFloat(-2f, 2f), 
                        Main.rand.NextFloat(-3f, -1f)
                    );

                    Gore.NewGore(
                        NPC.GetSource_Death(), 
                        NPC.position, 
                        velocity, 
                        ModContent.GoreType<ChickenFeatherGore>()
                    );
                }
            }
        }
    }
}