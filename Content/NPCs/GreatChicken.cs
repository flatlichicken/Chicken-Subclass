using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Gores;
using Chickensubclass.Content.Projectiles;

namespace Chickensubclass.Content.NPCs
{
    [AutoloadBossHead]
    public class GreatChicken : ModNPC
    {
        private int FrameDir = 0;
        private float MoveType = 0;
        private int AttackTimer = 0;
        private int ProjAngle = 0;
        private int FeatherSpreadCooldown = 2;
        private int BossPhase = 0;
        private bool ArmorShed = false;
        public override void SetStaticDefaults() {
                Main.npcFrameCount[NPC.type] = 12;
                
        }
        public override void SetDefaults() {
                NPC.width = 32;
                NPC.height = 32;
                NPC.damage = 15;
                NPC.defense = 12;
                NPC.lifeMax = 2000;
                NPC.HitSound = SoundID.NPCHit1;
                NPC.DeathSound = SoundID.NPCDeath1;
                NPC.value = 100f;
                NPC.noGravity = true;
                NPC.noTileCollide = true;
                NPC.boss = true;
                NPC.knockBackResist = 0f;
        }
        public override void FindFrame(int frameHeight) {
            NPC.spriteDirection = NPC.direction;
            int startFrame = 0;

            if (BossPhase == 2) {
                    startFrame = 8;
            }
            else if (BossPhase == 1) {
                    startFrame = 4;
            }
            
            if (FrameDir == 0) {
                NPC.frameCounter += 0.15f;
                if (NPC.frameCounter >= 3.99f) {
                    NPC.frameCounter = 3.99f;
                    FrameDir = 1;
                }
            }
            else {
                NPC.frameCounter -= 0.15f;
                if (NPC.frameCounter <= 0f) {
                    NPC.frameCounter = 0f;
                    FrameDir = 0;
                }
            }
            int currentSubFrame = (int)NPC.frameCounter;
            NPC.frame.Y = (startFrame + currentSubFrame) * frameHeight;
        }
        public override void AI() {
            float healthRatio = (float)NPC.life / NPC.lifeMax;

            if (healthRatio <= 0.33f) {
                BossPhase = 2;

                if (ArmorShed == false) {
                for (int i = 1; i <= 6; i++) {
                    Vector2 velocity = new Vector2(
                        Main.rand.NextFloat(-2f, 2f), 
                        Main.rand.NextFloat(-3f, -1f)
                    );
                    Gore.NewGore(
                        NPC.GetSource_FromAI(), 
                        NPC.position, 
                        velocity, 
                        Mod.Find<ModGore>($"Panel{i}").Type
                    );
                }
                }

                ArmorShed = true;
            }
            else if (healthRatio <= 0.66f) {
                BossPhase = 1;
            }

            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            AttackTimer += 1;

            if (AttackTimer >= 220 && AttackTimer < 240) {
                MoveType = 4;
            }
            else if (AttackTimer >= 240 && AttackTimer < 300) {
                MoveType = 3;
            }
            else if (AttackTimer >= 520 && AttackTimer < 540) {
                MoveType = 1;
            }
            else if (AttackTimer >= 540 && AttackTimer < 600) {
                MoveType = 2;
            }
            else {
                MoveType = 0;
            }

            if (AttackTimer >= 600) {
                AttackTimer = 0; // reset timer
            }

            if (NPC.HasValidTarget && player.active && !player.dead) {
                    if (MoveType == 0) { // general
                        NPC.direction = player.Center.X < NPC.Center.X ? -1 : 1;
                        Vector2 offset = Main.player[NPC.target].Center - NPC.Center;
                        Vector2 direction = player.Center - NPC.Center;
                        direction.Normalize();

                        float speed;

                        if (offset.LengthSquared() >= 400f * 400f) {
                            speed = (offset.Length() + 100f) / 90f;
                        }
                        else {
                            if (BossPhase == 2) speed = 5.5f;
                            else if (BossPhase == 1) speed = 5.25f;
                            else speed = 5f;
                            
                        }
                        
                        float ySpeedMultiplier = 2f; 
                        NPC.velocity = new Vector2(direction.X * speed, direction.Y * speed * ySpeedMultiplier);
                    }
                    else if (MoveType == 1) { // move up
                        NPC.velocity = new Vector2(0f, -12f);
                    }

                    else if (MoveType == 2) { // feather spread attack
                        NPC.velocity = Vector2.Zero;
                        
                        if (FeatherSpreadCooldown >= 1) {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0f, -5f).RotatedBy(MathHelper.ToRadians(ProjAngle)), ModContent.ProjectileType<GreatChickenFeatherProjectile>(), 10, 1f, Main.myPlayer);
                            if (BossPhase >= 1) Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0f, -5f).RotatedBy(MathHelper.ToRadians(ProjAngle + 186)), ModContent.ProjectileType<GreatChickenFeatherProjectile>(), 10, 1f, Main.myPlayer);
                            FeatherSpreadCooldown = 0;
                        }
                        else FeatherSpreadCooldown += 1;
                        ProjAngle += 6;
                        
                    }
                    else if (MoveType == 3) { // dash attack
                        if (AttackTimer == 240) {
                            Vector2 dashDirection = player.Center - NPC.Center;
                            dashDirection.Normalize();
                            if (BossPhase == 2) NPC.velocity = dashDirection * 25f;
                            else NPC.velocity = dashDirection * 18f;
                        }
                    }

                    else if (MoveType == 4) { // stay still
                        NPC.velocity = Vector2.Zero;
                    }

                }
                else {
                    NPC.velocity.Y += -2f;
                    NPC.EncourageDespawn(10);
                }
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
                                    ModContent.GoreType<ChickenFeatherGore>()
                            );
                    }
            }
        }
    }
}