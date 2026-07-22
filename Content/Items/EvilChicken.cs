using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles; // the piece of code i found out i needed to type to use a custom projectile


namespace Chickensubclass.Content.Items
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class EvilChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 32;
			Item.DamageType = DamageClass.Melee;
			Item.width = 85;
			Item.height = 60;
			Item.useTime = 42;
			Item.useAnimation = 42;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<EvilChickenProjectile>();
			Item.shootSpeed = 5.5f;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Generates white light inside the swinging hitbox
            Lighting.AddLight(hitbox.Center.ToVector2(), 1.0f, 1.0f, 1.0f);
        }


		public override void AddRecipes()
		{
			Recipe EvilChickenRecipe = CreateRecipe();
			EvilChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 15);
			EvilChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 10);
			EvilChickenRecipe.AddIngredient(ItemID.DemoniteBar, 10);
			EvilChickenRecipe.AddIngredient(ItemID.ShadowScale, 5);
			EvilChickenRecipe.AddTile(TileID.Anvils);
			EvilChickenRecipe.Register();

		}
	}
}

