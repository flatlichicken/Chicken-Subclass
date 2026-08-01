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
		private int MaxFlightTime;

		public override void SetStaticDefaults() {
			MountData.flightTimeMax = 140;
			MountData.usesHover = false;
			MountData.spawnDust = DustID.Tin;
			MountData.buff = ModContent.BuffType<Buffs.ChickenJockeyBuff>();
			MountData.textureWidth = 68;  // Replace with full width of your PNG
        	MountData.textureHeight = 482; // Replace with full height of your PNG

			MountData.runSpeed = 6f;
			MountData.dashSpeed = 6f;
			MountData.acceleration = 0.2f;
			MountData.jumpHeight = 10;
			MountData.jumpSpeed = 7.15f;
			MountData.swimSpeed = 4f;

			MountData.playerYOffsets = new int[] { 24, 26, 24, 24, 24, 24 }; 
			MountData.xOffset = 8;
			MountData.yOffset = -12;

			MountData.totalFrames = 6;
			
			MountData.standingFrameCount = 1;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 0;

			MountData.runningFrameCount = 3;
			MountData.runningFrameDelay = 15;
			MountData.runningFrameStart = 0;

			MountData.flyingFrameCount = 3;
			MountData.flyingFrameDelay = 2;
			MountData.flyingFrameStart = 3;

			MountData.inAirFrameCount = 3;
			MountData.inAirFrameDelay = 12;
			MountData.inAirFrameStart = 3;

			MountData.swimFrameCount = 3;
			MountData.swimFrameDelay = 15;
			MountData.swimFrameStart = 3;

			MountData.idleFrameCount = 1;
			MountData.idleFrameDelay = 12;
			MountData.idleFrameStart = 0;

			MountData.idleFrameLoop = true;
		}

		public override void UpdateEffects(Player player)
        {
            MaxFlightTime = 70;

            if (UsingChickenWing)
            {
                MaxFlightTime = MaxFlightTime * 2;
            }
            
            bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
            if (isChickenWeapon)
            {
                MaxFlightTime = MaxFlightTime * 2;
            }
            
            if (player.mount._flyTime > MaxFlightTime)
            {
                player.mount._flyTime = MaxFlightTime;
            }
        }
	}
}
