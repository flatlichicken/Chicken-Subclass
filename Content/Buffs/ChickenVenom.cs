using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Buffs
{
    public class ChickenVenom : ModBuff
    {
        public override void SetStaticDefaults() {
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
           
        }

        public override void Update(Player player, ref int buffIndex)
        {
            bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
            int BuffPrio = player.GetModPlayer<ChickenDebuffInflict>().ChickenBuffPrio;
			if (isChickenWeapon && BuffPrio == 5) 
            {
            player.GetDamage(DamageClass.Melee) += 0.10f;
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.damage = (int)(npc.defDamage * 1.1f);
            NetMessage.SendData(23, -1, -1, null, npc.whoAmI);
        }
    }
}