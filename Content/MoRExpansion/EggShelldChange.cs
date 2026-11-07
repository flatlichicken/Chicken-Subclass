using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Chickensubclass.Content;

namespace Chickensubclass.Content.MoRExpansion
{
    public class RedemptionAccessoryModifier : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (IsEggShield(item))
            {
                if (ChickenWeaponDamageBoost.IfUsingChickenWeapon(player))
                {
                    player.noKnockback = true;
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (IsEggShield(item))
            {
                tooltips.Add(new TooltipLine(Mod, "EggShieldChickenEffect",
                    "If using a chicken weapon, you will also ignore knockback"));
            }
        }

        private static bool IsEggShield(Item item)
        {
            return item.ModItem != null &&
                item.ModItem.Mod.Name == "Redemption" &&
                item.ModItem.Name == "EggShield";
        }
    }
}