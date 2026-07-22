using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles;
using Terraria.DataStructures;


namespace Chickensubclass.Content.Items
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class CommieChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 56;
			Item.DamageType = DamageClass.Melee;
			Item.width = 111;
			Item.height = 125;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<CommieChickenHammer>();
			Item.shootSpeed = 12f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
 	  	 // 1. Shoot the primary projectile (usually the one passed in 'type')
		 Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

   		 // 2. Shoot a secondary projectile (e.g., a vanilla Projectile ID)
   		 int secondaryProjectile = ModContent.ProjectileType<CommieChickenSickle>(); 
   		 Projectile.NewProjectile(source, position, velocity, secondaryProjectile, damage, knockback, player.whoAmI);

   		 return false; // Prevent default firing
		}


		public override void AddRecipes()
		{
			Recipe CommieChickenRecipe = CreateRecipe();
			CommieChickenRecipe.AddIngredient(ItemID.Ichor, 20);
			CommieChickenRecipe.AddIngredient(ItemID.SoulofNight, 15);
			CommieChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenSoul>(), 10);
			CommieChickenRecipe.AddTile(TileID.MythrilAnvil);
			CommieChickenRecipe.Register();

		}
	}
}

