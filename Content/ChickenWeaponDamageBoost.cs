using Terraria;
using Terraria.ModLoader;
using Chickensubclass.Content.Items;

namespace Chickensubclass.Content
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting a X_Body.png file to be placed next to the item's main texture.
	
	public class ChickenWeaponDamageBoost
{
    public static bool IfUsingChickenWeapon(Player player) 
    {
        // list of weapons that get boosted
        int heldItemType = player.inventory[player.selectedItem].type;

        return heldItemType == ModContent.ItemType<FlyingChicken>() ||
               heldItemType == ModContent.ItemType<NuclearChicken>() ||
               heldItemType == ModContent.ItemType<SpookyChicken>() ||
               heldItemType == ModContent.ItemType<SolarChicken>() ||
               heldItemType == ModContent.ItemType<RoaringChicken>() ||
               heldItemType == ModContent.ItemType<WaterChicken>() ||
               heldItemType == ModContent.ItemType<PrismaticChicken>() ||
               heldItemType == ModContent.ItemType<FireChicken>() ||
               heldItemType == ModContent.ItemType<CommieChicken>() ||
               heldItemType == ModContent.ItemType<GreedyChicken>() ||
               heldItemType == ModContent.ItemType<RedJunglefowl>() ||
               heldItemType == ModContent.ItemType<MagicChicken>() ||
               heldItemType == ModContent.ItemType<EvilChicken>() ||
               heldItemType == ModContent.ItemType<ExplosiveChicken>() ||
               heldItemType == ModContent.ItemType<ChaosChicken>() ||
              // heldItemType == ModContent.ItemType<GodChicken>() || (removed)
               heldItemType == ModContent.ItemType<LizardChicken>() ||
               heldItemType == ModContent.ItemType<HolyChicken>() ||
               heldItemType == ModContent.ItemType<TerraChicken>() ||
               heldItemType == ModContent.ItemType<TrueHolyChicken>() ||
               heldItemType == ModContent.ItemType<DarkChicken>() ||
               heldItemType == ModContent.ItemType<TrueDarkChicken>() ||
               heldItemType == ModContent.ItemType<NinjaChicken>() ||
               heldItemType == ModContent.ItemType<ReaperChicken>() ||
               heldItemType == ModContent.ItemType<LowPolyChicken>() ||
               heldItemType == ModContent.ItemType<ChlorophytePullet>() ||
               heldItemType == ModContent.ItemType<BeeChicken>() ||
               heldItemType == ModContent.ItemType<BinChicken>() ||
               heldItemType == ModContent.ItemType<ZenithChicken>() ||
               heldItemType == ModContent.ItemType<Chicken>();
    }
}

}        