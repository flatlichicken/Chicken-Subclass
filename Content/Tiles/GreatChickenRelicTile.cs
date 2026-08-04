using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Chickensubclass.Content.Tiles
{
    public class GreatChickenRelicTile : ModTile
    {
        public const int FrameWidth = 54;
        public const int FrameHeight = 72;

        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(233, 182, 51), CreateMapEntryName());
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) {
            Tile tile = Main.tile[i, j];

            if (tile.TileFrameY >= 54) {
                return true;
            }

            if (tile.HasTile && tile.TileFrameX == 0 && tile.TileFrameY == 0) {
                Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
                Vector2 tileDrawPos = new Vector2(i * 16, j * 16) - Main.screenPosition + zero;

                Texture2D texture = TextureAssets.Tile[Type].Value;

                float floatOffset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * MathHelper.TwoPi / 5f) * 4f;
                Rectangle statueFrame = new Rectangle(0, 0, FrameWidth, 54);
                Vector2 statueDrawPos = tileDrawPos + new Vector2(0f, floatOffset);

                spriteBatch.Draw(texture, statueDrawPos, statueFrame, Lighting.GetColor(i + 1, j + 1), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            return false;
        }
    }
}