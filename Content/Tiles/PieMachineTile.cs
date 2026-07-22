using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Chickensubclass.Content.Tiles
{
    public class PieMachineTile : ModTile
    {
        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileTable[Type] = true;
            Main.tileSolidTop[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Width = 11;
            TileObjectData.newTile.Height = 6;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16 }; 
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2; 
            TileObjectData.newTile.Origin = new Point16(5, 5);

            TileObjectData.addTile(Type);

            AddMapEntry(new Color(191, 142, 111), Language.GetOrRegister("Mods.Chickensubclass.Tiles.PieMachineTile.MapEntry", () => "Pie Machine"));
            
            
        }
    }
}