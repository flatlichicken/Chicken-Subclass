using Chickensubclass.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace Chickensubclass.Content.Mounts
{
	public class ChickenJockeyMount : ModMount
	{
		public bool UsingChickenWing;
		public bool UsingChickenFoot;
		private int MaxFlightTime;

		public override void SetStaticDefaults() {
			MountData.flightTimeMax = 140;
			MountData.usesHover = false;
			MountData.spawnDust = DustID.Cloud;
			MountData.buff = ModContent.BuffType<Buffs.ChickenJockeyBuff>();
			MountData.textureWidth = 68;  // Replace with full width of your PNG
        	MountData.textureHeight = 562; // Replace with full height of your PNG

			MountData.runSpeed = 6f;
			MountData.dashSpeed = 6f;
			MountData.acceleration = 0.2f;
			MountData.jumpHeight = 10;
			MountData.jumpSpeed = 7.15f;
			MountData.swimSpeed = 4f;

			MountData.playerYOffsets = new int[] { 24, 26, 24, 26, 24, 24, 24,}; 
			MountData.xOffset = 8;
			MountData.yOffset = -12;

			MountData.totalFrames = 7;
			
			MountData.standingFrameCount = 1;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 0;

			MountData.runningFrameCount = 4;
			MountData.runningFrameDelay = 15;
			MountData.runningFrameStart = 0;

			MountData.flyingFrameCount = 3;
			MountData.flyingFrameDelay = 2;
			MountData.flyingFrameStart = 4;

			MountData.inAirFrameCount = 3;
			MountData.inAirFrameDelay = 12;
			MountData.inAirFrameStart = 4;

			MountData.swimFrameCount = 3;
			MountData.swimFrameDelay = 15;
			MountData.swimFrameStart = 4;

			MountData.idleFrameCount = 1;
			MountData.idleFrameDelay = 12;
			MountData.idleFrameStart = 0;

			MountData.idleFrameLoop = true;
		}

		public override void UpdateEffects(Player player)
        {
            // Take(10) ensures we only check equipped armor/accessories, ignoring vanity slots
            bool isWingActive = player.armor.Take(10).Any(item =>
            {
                
                var method = item?.ModItem?.GetType().GetMethod("WingActive", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                if (method == null) return false;

                object[] args = method.GetParameters().Length == 1 ? new object[] { player } : null;
                
                return method.Invoke(null, args) is true;
            });

			bool isFootActive = player.armor.Take(10).Any(item =>
            {
                
                var method = item?.ModItem?.GetType().GetMethod("FootActive", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                if (method == null) return false;

                object[] args = method.GetParameters().Length == 1 ? new object[] { player } : null;
                
                return method.Invoke(null, args) is true;
            });
			bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);

			if (isFootActive && isChickenWeapon)
			{
				MountData.runSpeed = 8f;
			}
			else if (isFootActive)
			{
				MountData.runSpeed = 7f;
			}
			else if (isChickenWeapon)
			{
				MountData.runSpeed = 7f;
			}
			else
			{
				MountData.runSpeed = 6f;
			}

            if (isWingActive)
            {
                MaxFlightTime = 75;
            }
            else 
            {
                MaxFlightTime = 70;
            }
            
            
            if (isChickenWeapon)
            {
                MaxFlightTime = MaxFlightTime * 2;
            }
            
            
            if (player.velocity.Y == 0 || player.mount._flyTime > MaxFlightTime)
            {
                player.mount._flyTime = MaxFlightTime;
            }

			if (player.wet)
    		{
    		    // Pushes the player upward until they reach the surface
    		    if (player.velocity.Y > -4f)
    		    {
    		        player.velocity.Y -= 0.5f;
    		    }
    		}
			
			
        }
	}
}
