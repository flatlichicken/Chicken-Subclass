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
                Vector2 baseDrawPos = new Vector2(tileX * 16, tileY * 16) - Main.screenPosition + zero;

                Texture2D texture = TextureAssets.Tile[Type].Value;
                Color lightingColor = Lighting.GetColor(tileX + 1, tileY + 1);

                float time = Main.GlobalTimeWrappedHourly;
                float floatOffset = (float)Math.Sin(time * MathHelper.TwoPi / 5f) * 4f;

                Rectangle statueFrame = new Rectangle(0, 0, FrameWidth, 54);
                Vector2 statueDrawPos = baseDrawPos + new Vector2(0f, floatOffset);

                spriteBatch.Draw(texture, statueDrawPos, statueFrame, lightingColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                float pulseFactor = (float)Math.Sin(time * MathHelper.TwoPi / 2f) * 0.5f + 0.8f;
                Color glowColor = new Color(255, 255, 255, 0) * pulseFactor * 0.35f;

                spriteBatch.Draw(texture, statueDrawPos, statueFrame, glowColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                if (!Main.gamePaused && Main.instance.IsActive && Main.rand.NextBool(8)) {
                    int spawnY = Main.rand.Next(FrameHeight);
                    float yOffset = spawnY < 54 ? floatOffset : 0f;
                    Vector2 dustWorldPos = new Vector2(tileX * 16, tileY * 16) + new Vector2(Main.rand.Next(FrameWidth), spawnY + yOffset);

                    Color dustColor = Main.rand.NextBool(4) ? new Color(255, 253, 220) : Color.White;

                    Dust dust = Dust.NewDustDirect(
                        dustWorldPos, 
                        0, 0, 
                        DustID.SilverFlame, 
                        0f, 0f, 
                        100, 
                        dustColor, 
                        Main.rand.NextFloat(0.9f, 1.2f)
                    );

                    dust.velocity = new Vector2(Main.rand.NextFloat(-0.1f, 0.1f), -0.2f);
                    dust.fadeIn = 1.2f;
                    dust.noGravity = true;
                }
            }

            return false;
        }
    }
}