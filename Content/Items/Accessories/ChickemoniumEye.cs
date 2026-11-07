using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Chickensubclass.Content.Buffs;


namespace Chickensubclass.Content.Items.Accessories
{
	
	public class ChickemoniumEye : ModItem
	{

		public override void SetDefaults() {
			Item.width = 36;
			Item.height = 36;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.LightPurple;
			 
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            float maximumRange = 800f;

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.friendly || npc.dontTakeDamage || !npc.chaseable)
                    continue;

                float currentDistance = Vector2.Distance(player.Center, npc.Center);

                if (currentDistance <= maximumRange)
                {
                    if (Collision.CanHit(player.Center, 1, 1, npc.Center, 1, 1))
                    {
                        npc.AddBuff(ModContent.BuffType<ChickemoniumTracked>(), 900);
                    }
                }
            }
		}
	}

}