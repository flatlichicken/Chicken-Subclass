using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Chickensubclass.Content.Tiles
{
    public class NuggetMachineTile : ModTile
    {
        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileSolidTop[Type] = true;

            // 1. Start with the 2x2 base
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            
            // 2. Fix the Heights
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 }; 
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2; 

            TileObjectData.addTile(Type);

            // This forces the name "Nugget Machine" without using the .hjson file
            AddMapEntry(new Color(191, 142, 111), Language.GetOrRegister("Mods.Chickensubclass.Tiles.NuggetMachineTile.MapEntry", () => "Nugget Machine"));
            
           
        }

        public override void AnimateTile(ref int frame, ref int frameCounter) {
            if (++frameCounter >= 10) {
                frameCounter = 0;
                if (++frame >= 3) { 
                    frame = 0;
                }
            }
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset) {
            // Frame calculation for the sprite sheet
            frameYOffset = Main.tileFrame[type] * 36;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY) {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Items.NuggetMachine>());
        }
    }
}