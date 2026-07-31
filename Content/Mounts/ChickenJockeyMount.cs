using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Chickensubclass.Content.Mounts
{
	public class ChickenJockeyMount : ModMount
	{

		public override void SetStaticDefaults() {
			MountData.flightTimeMax = 160;
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

			MountData.playerYOffsets = new int[] { 24, 24, 24, 24, 24, 24 }; 
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

			MountData.idleFrameCount = 1;
			MountData.idleFrameDelay = 12;
			MountData.idleFrameStart = 0;

			MountData.idleFrameLoop = true;
		}
	}
}
