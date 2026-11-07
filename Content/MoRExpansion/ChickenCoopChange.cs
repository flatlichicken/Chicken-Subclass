using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Chickensubclass.Content.Items;

namespace Chickensubclass.Content.MoRExpansion
{
    public class ChickenCoopDropEdit : GlobalTile
    {
        public override void RandomUpdate(int i, int j, int type) {
            if (ModLoader.TryGetMod("Redemption", out Mod redemptionMod)) {
                if (redemptionMod.TryFind("ChickenCoopTile", out ModTile targetTile) && type == targetTile.Type) {
                    
                    if (Main.rand.NextBool(8)) {
                        if (Main.rand.NextBool(1000) && Main.hardMode) {
                            int susEgg = ModContent.ItemType<SusEgg>();
                            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 32, susEgg);
                        }
                    }
                }
            }
        }
    }
}
