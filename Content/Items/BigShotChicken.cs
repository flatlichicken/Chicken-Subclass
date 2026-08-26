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
	public class BigShotChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 51;
			Item.height = 48;
			Item.useTime = 16;
			Item.useAnimation = 16 ;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 0, 85, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<ChickenFeatherProjectile>();
			Item.shootSpeed = 4f;
			Item.channel = true;


		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.shoot = ModContent.ProjectileType<BigShotChickenProjectileCharge>();
				Item.shootSpeed = 0f;
			}
			else
			{
				Item.shoot = ModContent.ProjectileType<ChickenFeatherProjectile>();
				Item.shootSpeed = 4f;
			}
			return base.CanUseItem(player);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			return true;
		}

		public override void AddRecipes()
		{
			Recipe FlyingChickenRecipe = CreateRecipe();
			FlyingChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 25);
			FlyingChickenRecipe.AddIngredient(ItemID.Feather, 10);
			FlyingChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 8);
			FlyingChickenRecipe.AddRecipeGroup("GoldBar", 10);
			FlyingChickenRecipe.AddTile(TileID.Anvils);
			FlyingChickenRecipe.Register();

		}
		
	}
}
