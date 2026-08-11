using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles; // the piece of code i found out i needed to type to use a custom projectile


namespace Chickensubclass.Content.Items
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com
	public class GreedyChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 56;
			Item.DamageType = DamageClass.Melee;
			Item.width = 55;
			Item.height = 60;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(0, 1, 50, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.channel = true; 
			Item.shoot = ModContent.ProjectileType<GreedyChickenProjectile>();
			Item.shootSpeed = 10f;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Generates white light inside the swinging hitbox
            Lighting.AddLight(hitbox.Center.ToVector2(), 1.0f, 1.0f, 1.0f);
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) =>
			player.itemAnimation == player.itemAnimationMax && Main.mouseLeftRelease || player.ownedProjectileCounts[type] is 0;


		public override void AddRecipes()
		{
			Recipe GreedyChickenRecipe = CreateRecipe();
			GreedyChickenRecipe.AddIngredient(ItemID.CursedFlame, 20);
			GreedyChickenRecipe.AddIngredient(ItemID.SoulofNight, 15);
			GreedyChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenSoul>(), 10);
			GreedyChickenRecipe.AddTile(TileID.MythrilAnvil);
			GreedyChickenRecipe.Register();

		}
	}
}
