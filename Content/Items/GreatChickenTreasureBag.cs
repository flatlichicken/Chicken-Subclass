using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Chickensubclass.Content.Items.Accessories;

namespace Chickensubclass.Content.Items
{
    public class GreatChickenTreasureBag : ModItem
    {
        public override void SetStaticDefaults() {
            ItemID.Sets.BossBag[Type] = true;

            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults() {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
        }

        public override bool CanRightClick() {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<Content.NPCs.GreatChicken>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<PieMachine>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChickenOrb>(), 1));
   
        }

        public override Color? GetAlpha(Color lightColor) {
            return Color.White;
        }

        public override void PostUpdate() {
            Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);
            if (Item.timeSinceItemSpawned % 12 == 0) {
                Vector2 vector = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                vector.Normalize();
                int dust = Dust.NewDust(Item.position, Item.width, Item.height, DustID.RainbowRod, vector.X * 2f, vector.Y * 2f, 100, default, 0.8f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
            Texture2D texture = TextureAssets.Item[Item.type].Value;
            Rectangle frame = texture.Frame();
            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 position = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = (float)Main.timeForVisualEffects / 10f;
            for (int i = 0; i < 4; i++) {
                Vector2 drawPos = position + (time + i * MathHelper.PiOver2).ToRotationVector2() * 2f;
                spriteBatch.Draw(texture, drawPos, frame, new Color(255, 255, 255, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}