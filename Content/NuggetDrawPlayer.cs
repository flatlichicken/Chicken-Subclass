using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Chickensubclass.Content
{
	public class NuggetDrawPlayer : ModPlayer
	{
	}

	public class SpicyNuggetHeldLayer : PlayerDrawLayer
	{
		public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.HandOnAcc);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player player = drawInfo.drawPlayer;
			return player.itemAnimation > 0 && !player.HeldItem.IsAir && player.HeldItem.ModItem != null && player.HeldItem.ModItem.Mod == Mod;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.shadow != 0f) return;

			Player player = drawInfo.drawPlayer;
			ModItem currentItem = player.HeldItem.ModItem;

			string heldTexturePath = currentItem.Texture + "_Held";
			if (!ModContent.HasAsset(heldTexturePath)) return;

			Texture2D texture = ModContent.Request<Texture2D>(heldTexturePath).Value;

			Vector2 position = drawInfo.ItemLocation - Main.screenPosition + new Vector2(5f * player.direction, -14f);
			SpriteEffects effects = player.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Color color = Lighting.GetColor((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));

			drawInfo.DrawDataCache.Add(new DrawData(
				texture,
				position,
				null,
				color,
				player.itemRotation,
				new Vector2(texture.Width / 2f, texture.Height / 2f),
				player.HeldItem.scale,
				effects,
				0
			));
		}
	}
}
