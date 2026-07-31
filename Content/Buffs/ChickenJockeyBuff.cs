using Terraria;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Buffs
{
	public class ChickenJockeyBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.mount.SetMount(ModContent.MountType<Mounts.ChickenJockeyMount>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
