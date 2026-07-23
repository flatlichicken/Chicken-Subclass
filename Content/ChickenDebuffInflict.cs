using Terraria;
using Terraria.ModLoader;
using Chickensubclass.Content.Items;
using Terraria.ID;
using Chickensubclass.Content.Buffs;

namespace Chickensubclass.Content
{
	public class ChickenDebuffInflict : ModPlayer
    {
        public bool NuggetKnucklesCheck;
        public bool ChickenScentCheck;
        public bool FrostBeakCheck;
        public bool SolarFlareBeakCheck;
        public bool DinoHelmCheck;
        bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);


        public override void ResetEffects()
        {
            NuggetKnucklesCheck = false;
            ChickenScentCheck = false;
            FrostBeakCheck = false;
            SolarFlareBeakCheck = false;
            DinoHelmCheck = false;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            if (Player.HasBuff(ModContent.BuffType<ChickenAnger>()) && isChickenWeapon)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }

            if (NuggetKnucklesCheck && Main.rand.NextBool(10))
            {
            target.AddBuff(ModContent.BuffType<Buffs.Chicken>(), 180);
            }
        
            if (ChickenScentCheck && Main.rand.NextBool(1))
            {
            target.AddBuff(ModContent.BuffType<Buffs.ChickenInstinct>(), 180);
            }
        
            if (FrostBeakCheck)
            {
            target.AddBuff(BuffID.Frostburn2, 540);
            }

            if (DinoHelmCheck)
            {
            target.AddBuff(BuffID.CursedInferno, 540);
            }

            if (SolarFlareBeakCheck)
            {
            target.AddBuff(BuffID.Daybreak, 540);
            }
        }
        
        public override void OnHitNPCWithProj(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.HasBuff(ModContent.BuffType<ChickenAnger>()))
            {
                target.AddBuff(BuffID.OnFire, 180);
            }

            if (ChickenScentCheck)
            {
            target.AddBuff(ModContent.BuffType<Buffs.ChickenInstinct>(), 180);
            }
        
            if (FrostBeakCheck)
            {
            target.AddBuff(BuffID.Frostburn2, 300 + Main.rand.Next(540));
            }

            if (DinoHelmCheck)
            {
            target.AddBuff(BuffID.CursedInferno, 300 + Main.rand.Next(540));
            }

            if (SolarFlareBeakCheck)
            {
            target.AddBuff(BuffID.Daybreak, 300 + Main.rand.Next(540));
            }
        }

    }

}