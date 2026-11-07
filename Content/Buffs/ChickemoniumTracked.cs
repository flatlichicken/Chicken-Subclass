using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Items.Accessories;

namespace Chickensubclass.Content.Buffs
{
    public class ChickemoniumTracked : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_204";
        public override void SetStaticDefaults() {
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            NetMessage.SendData(23, -1, -1, null, npc.whoAmI);
        }
    }

    public class ChickemoniumTrackedDrawing : GlobalNPC
    {
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (Main.netMode == NetmodeID.Server)
                return true;

            Player localPlayer = Main.LocalPlayer;
            if (localPlayer == null || !localPlayer.active || localPlayer.dead)
                return true;

            bool hasAccessoryEquipped = false;
            int targetItemType = ModContent.ItemType<ChickemoniumEye>();

            for (int i = 3; i < 10; i++)
            {
                if (localPlayer.armor[i].type == targetItemType)
                {
                    hasAccessoryEquipped = true;
                    break;
                }
            }

            if (hasAccessoryEquipped && npc.HasBuff(ModContent.BuffType<ChickemoniumTracked>()))
            {
                Texture2D texture = TextureAssets.Npc[npc.type].Value;
                Vector2 drawPosition = npc.Center - screenPos;
                SpriteEffects effects = npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Vector2 origin = npc.frame.Size() / 2f;

                spriteBatch.Draw(texture, drawPosition, npc.frame, Color.White, npc.rotation, origin, npc.scale, effects, 0f);

                return false;
            }

            return true;
        }
    }
}
