using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles;

namespace Chickensubclass.Content.Items
{
	public class ZenithChicken : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 225;
			Item.DamageType = DamageClass.Melee;
			Item.width = 313;
			Item.height = 255;
			Item.useTime = 5;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 25);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<ZenithChickenProjectile>();
			Item.shootSpeed = 12f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 mousePosition = Main.MouseWorld;
			float spawnDistance = 500f;
			Vector2 randomDirection = Main.rand.NextFloat((float)Math.PI * 2f).ToRotationVector2();
			Vector2 projectileSpawnPosition = mousePosition + (randomDirection * spawnDistance);

			Vector2 newVelocity = mousePosition - projectileSpawnPosition;
			newVelocity.Normalize();
			newVelocity *= Item.shootSpeed;

			Projectile.NewProjectile(source, projectileSpawnPosition, newVelocity, type, damage, knockback, player.whoAmI);

			return false;
		}

		public override void AddRecipes()
		{
			Recipe ZenithChickenChickenRecipe = CreateRecipe();
			ZenithChickenChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.TerraChicken>());
			ZenithChickenChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.BinChicken>());
			ZenithChickenChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.LowPolyChicken>());
			ZenithChickenChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.RoaringChicken>());
			ZenithChickenChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.BeeChicken>());
			ZenithChickenChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.MagicChicken>());
			ZenithChickenChickenRecipe.AddTile(TileID.MythrilAnvil);
			ZenithChickenChickenRecipe.Register();

		}
	}
}
