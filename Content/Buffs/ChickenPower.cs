using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Buffs
{
    public class ChickenPower : ModBuff
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
			if (isChickenWeapon) 
            {
            player.GetDamage(DamageClass.Melee) += 0.10f;
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.damage = (int)(npc.defDamage * 1.15f);
            NetMessage.SendData(23, -1, -1, null, npc.whoAmI);
        }
    }
}