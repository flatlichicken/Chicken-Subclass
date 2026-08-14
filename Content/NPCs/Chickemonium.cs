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
using ReLogic.Utilities;

namespace Chickensubclass.Content.NPCs
{
    public class Chickemonium : ModNPC
    {
        private bool Enraged = false;
        private bool RoamStarted = false;
        private bool ReachedCloseRange = false;
        private int soundTimer = 0;
        private int soundTimer2 = 0;
        private float speed = 0;
        private SlotId attackSoundSlot;
        private SlotId docileSoundSlot;
        private bool attackStarted = false;
        private int attackTime = 2400;

        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = 2;

            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Duck;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers() {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults() {
            NPC.width = 100;
            NPC.height = 100;
            NPC.damage = 42;
            NPC.defense = 8;
            NPC.lifeMax = 1500;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.rarity = 3;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawChicken>(), 1, 5, 10));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChickenFeather>(), 1, 5, 10));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            if (!NPC.downedBoss3) {
                return 0f;
            }

            if (spawnInfo.Player.ZoneRockLayerHeight) {
                float depthProgress = (spawnInfo.SpawnTileY - (float)Main.rockLayer) / (float)(Main.maxTilesY - Main.rockLayer);
                depthProgress = MathHelper.Clamp(depthProgress, 0.1f, 1f);

                return SpawnCondition.Cavern.Chance * 0.08f * depthProgress;
            }

            return 0f;
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            string[] deathMessages = [
                $"{target.name} was consumed by C-367 after being spotted. As usual, no remains.",
                $"{target.name} was spotted by C-367 and hunted down."   
            ];

            string chosenMessage = deathMessages[Main.rand.Next(deathMessages.Length)];

            target.HurtPlayerDeathReason = Terraria.DataStructures.PlayerDeathReason.ByCustomReason(chosenMessage);
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
            if (!NPC.active)
            {
                StopAllSounds();
                return;
            }

            bool isHovered = NPC.Hitbox.Contains(Main.MouseWorld.ToPoint());
            

            NPC.TargetClosest(true);
            Player targetPlayer = Main.player[NPC.target];

            if (!targetPlayer.active || targetPlayer.dead)
            {
                StopAllSounds();
                return;
            }

            float currentDistance = Vector2.Distance(NPC.Center, targetPlayer.Center);
            float radius60Blocks = 960f;

            if (currentDistance <= radius60Blocks)
            {
                ReachedCloseRange = true;
            }

            if (!Enraged && ReachedCloseRange && currentDistance > (radius60Blocks * 2))
            {
                StopAllSounds();
                NPC.active = false;
                return;
            }

            float maximumRange = 800f;

            if (!Enraged && currentDistance <= maximumRange)
            {
                if (Collision.CanHit(NPC.Center, 1, 1, targetPlayer.Center, 1, 1))
                {
                    Enraged = true;
                }
            }

            if (!Enraged)
            {
                if (!RoamStarted)
                {
                    StopAllSounds();
                    docileSoundSlot = SoundEngine.PlaySound(new SoundStyle("Chickensubclass/Content/NPCs/ChickemoniumDocile"), NPC.position);
                    soundTimer2 = 1560;
                    RoamStarted = true;
                }

                soundTimer2--;
                if (soundTimer2 <= 0)
                {
                    StopAllSounds();
                    docileSoundSlot = SoundEngine.PlaySound(new SoundStyle("Chickensubclass/Content/NPCs/ChickemoniumDocile"), NPC.position);
                    soundTimer2 = 1560;
                }

                if (NPC.velocity == Vector2.Zero)
                {
                    Vector2 initialDir = targetPlayer.Center - NPC.Center;
                    if (initialDir != Vector2.Zero)
                    {
                        initialDir.Normalize();
                    }
                    speed = 2.5f;
                    NPC.velocity = initialDir * speed;
                }
            }
            else
            {
                Vector2 dashDirection = targetPlayer.Center - NPC.Center;
                if (dashDirection != Vector2.Zero)
                {
                    dashDirection.Normalize();
                }
                if (!attackStarted)
                {
                    speed = 7.5f;
                    attackStarted = true;
                }
                
                attackTime--;
                if (attackTime <= 0) speed = -12f;                
                else if (isHovered && speed > 5f) speed -= 0.05f;
                else if (!isHovered && speed < 10f) speed += 0.1f;


                NPC.velocity = dashDirection * speed;
                soundTimer--;
                if (soundTimer <= 0)
                {
                    StopAllSounds();
                    attackSoundSlot = SoundEngine.PlaySound(new SoundStyle("Chickensubclass/Content/NPCs/ChickemoniumAttacking"), NPC.position);
                    soundTimer = 420;
                }
            }
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public override void OnKill()
        {
            StopAllSounds();
        }

        private void StopAllSounds()
        {
            if (SoundEngine.TryGetActiveSound(attackSoundSlot, out ActiveSound activeAttackSound)) 
            {
                activeAttackSound.Stop();
            }
            if (SoundEngine.TryGetActiveSound(docileSoundSlot, out ActiveSound activeDocileSound)) 
            {
                activeDocileSound.Stop();
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.Info.AddRange([
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
                new FlavorTextBestiaryInfoElement("C-367 was initially discovered in 1962 near a sunken chicken coop within the Let-Vand zone during the first expedition and excavation of the Hadal Blacksite. Due to its high threat level, early recommendations left C-367 underground and advised personnel to avoid its known roaming areas.")
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