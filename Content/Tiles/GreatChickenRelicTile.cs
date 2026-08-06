using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
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

            AddMapEntry(new Color(233, 207, 95), Language.GetText("MapObject.Relic"));
        }

        public override bool PreDraw(int tileX, int tileY, SpriteBatch spriteBatch) {
            Tile tile = Main.tile[tileX, tileY];

            if (tile.TileFrameY >= 54) {
                return true;
            }

            if (tile.HasTile && tile.TileFrameX == 0 && tile.TileFrameY == 0) {
                Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
                Vector2 tileDrawPos = new Vector2(tileX * 16, tileY * 16) - Main.screenPosition + zero;

                Texture2D texture = TextureAssets.Tile[Type].Value;

                float floatOffset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * MathHelper.TwoPi / 5f) * 4f;
                Rectangle statueFrame = new Rectangle(0, 0, FrameWidth, 54);
                Vector2 statueDrawPos = tileDrawPos + new Vector2(0f, floatOffset);

                Color baseColor = Lighting.GetColor(tileX + 1, tileY + 1);
                spriteBatch.Draw(texture, statueDrawPos, statueFrame, baseColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                float pulse = (float)Math.Sin(Main.GlobalTimeWrappedHourly * (MathHelper.TwoPi / 2f)) * 0.3f + 0.7f;
                Color glowColor = new Color(255, 255, 255, 0) * 0.1f * pulse;

                for (int glowIndex = 0; glowIndex < 4; glowIndex++)
                {
                    Vector2 glowOffset = new Vector2(0f, 2f).RotatedBy(glowIndex * MathHelper.PiOver2);
                    spriteBatch.Draw(texture, statueDrawPos + glowOffset, statueFrame, glowColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }

            return false;
        }
    }
}