using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace Chickensubclass.Content.Tiles
{
    public class SoulBottleChickenTile : ModBannerTile
    {
        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileLighted[Type] = true;

            DustType = -1;
            AnimationFrameHeight = 36;

            TileID.Sets.MultiTileSway[Type] = true;
            
            

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new int[] { 36 };
            TileObjectData.newTile.StyleHorizontal = true;
            
            TileObjectData.newTile.AnchorTop = new AnchorData(
                AnchorType.SolidTile | AnchorType.Platform | AnchorType.PlanterBox, 
                TileObjectData.newTile.Width, 
                0
            );
            TileObjectData.newTile.WaterDeath = true;
            TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
            TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(255, 230, 100));
            RegisterItemDrop(ModContent.ItemType<Items.SoulBottleChicken>());
        }

        public override void AnimateTile(ref int frame, ref int frameCounter) {
            frameCounter++;
            if (frameCounter >= 6) {
                frameCounter = 0;
                frame = (frame + 1) % 4;
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
            r = 0.95f;
            g = 0.85f;
            b = 0.25f;
        }
    }
}