using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Chickensubclass.Content.Gores
{
    // 1. Class name MUST match your feather image file name (e.g. ChickenFeather.png)
    public class Panel6 : ModGore
    {
        public override void SetStaticDefaults() {
            // Marks this gore as non-bloody/family-friendly so it won't be hidden by Child Safety / Gore settings
            ChildSafety.SafeGore[Type] = true;
        }
        public override void OnSpawn(Gore gore, IEntitySource source) {
            // 2. Gives the feather a randomized rotation speed
            gore.rotation = Main.rand.NextFloat(0f, 6.28f);
            
            // 3. Tosses the feather slightly up and outward when spawned
            gore.velocity.Y -= Main.rand.NextFloat(1f, 3f);
            gore.velocity.X += Main.rand.NextFloat(-2f, 2f);
        }

        public override bool Update(Gore gore) {
            return true; 
        }
    }
}