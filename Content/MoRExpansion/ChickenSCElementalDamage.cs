using System;
using System.Reflection;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles;

namespace Chickensubclass.Content.MoRExpansion
{
    public class ChickenSCGlobalProjectile : GlobalProjectile
    {
        public override void SetStaticDefaults()
        {
            if (ModLoader.TryGetMod("Redemption", out Mod redemptionMod)) {
                RegisterElement(redemptionMod, "ProjArcane", ModContent.ProjectileType<MagicChickenProjectile>());
                RegisterElement(redemptionMod, "ProjArcane", ModContent.ProjectileType<PrismaticChickenProjectile>());
                RegisterElement(redemptionMod, "ProjArcane", ModContent.ProjectileType<SpookyChickenProjectile>());
                RegisterElement(redemptionMod, "ProjArcane", ModContent.ProjectileType<ChaosChickenProjectile>());

                RegisterElement(redemptionMod, "ProjFire", ModContent.ProjectileType<ChickenFireFeatherProjectile>());
                RegisterElement(redemptionMod, "ProjFire", ModContent.ProjectileType<SolarChickenProjectile>());

                RegisterElement(redemptionMod, "ProjHoly", ModContent.ProjectileType<HolyChickenProjectile>());
                RegisterElement(redemptionMod, "ProjHoly", ModContent.ProjectileType<TrueHolyChickenProjectile>());
                RegisterElement(redemptionMod, "ProjHoly", ModContent.ProjectileType<PrismaticChickenProjectile>());

                RegisterElement(redemptionMod, "ProjShadow", ModContent.ProjectileType<EvilChickenProjectile>());
                RegisterElement(redemptionMod, "ProjShadow", ModContent.ProjectileType<DarkChickenProjectile>());
                RegisterElement(redemptionMod, "ProjShadow", ModContent.ProjectileType<TrueDarkChickenProjectile>());

                RegisterElement(redemptionMod, "ProjNature", ModContent.ProjectileType<RedJunglefowlProjectile>());
                //ElementID.ProjPsychic[ModContent.ProjectileType<ReaperChickenProjectile>()] = true;

                RegisterElement(redemptionMod, "ProjCelestial", ModContent.ProjectileType<SolarChickenProjectile>());
                RegisterElement(redemptionMod, "ProjCelestial", ModContent.ProjectileType<PrismaticChickenProjectile>());
                RegisterElement(redemptionMod, "ProjCelestial", ModContent.ProjectileType<ZenithChickenProjectile>());

                RegisterElement(redemptionMod, "ProjExplosive", ModContent.ProjectileType<SolarChickenProjectile>());
                RegisterElement(redemptionMod, "ProjExplosive", ModContent.ProjectileType<ExplosiveChickenProjectile>());
                RegisterElement(redemptionMod, "ProjExplosive", ModContent.ProjectileType<NuclearChickenProjectile>());
            }
        }

        private static void RegisterElement(Mod redemptionMod, string fieldName, int projectileType)
        {
            Type elementType = redemptionMod.Code.GetType("Redemption.Globals.ElementID");
            FieldInfo elementField = elementType?.GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
            if (elementField?.GetValue(null) is bool[] projectiles && projectileType < projectiles.Length)
                projectiles[projectileType] = true;
        }
    }
}