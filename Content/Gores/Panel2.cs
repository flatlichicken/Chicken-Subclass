using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Gores
{
    // 1. Class name MUST match your feather image file name (e.g. ChickenFeather.png)
    public class Panel2 : ModGore
    {
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