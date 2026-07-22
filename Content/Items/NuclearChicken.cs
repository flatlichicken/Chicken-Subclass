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
	public class NuclearChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 132;
			Item.DamageType = DamageClass.Melee;
			Item.width = 120;
			Item.height = 156;
			Item.useTime = 60;
			Item.useAnimation = 60;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			 Item.value = Item.buyPrice(gold: 5, silver: 80);
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<NuclearChickenProjectile>();
			Item.shootSpeed = 6.5f;
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Generates white light inside the swinging hitbox
            Lighting.AddLight(hitbox.Center.ToVector2(), 1.0f, 1.0f, 1.0f);
        }


		public override void AddRecipes()
		{
			Recipe NuclearChickenRecipe = CreateRecipe();
			NuclearChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.PlantBasedNugget>(), 12);
			NuclearChickenRecipe.AddIngredient(ItemID.ChlorophyteBar, 5);
			NuclearChickenRecipe.AddTile(TileID.MythrilAnvil);
			NuclearChickenRecipe.Register();

		}
	}
}

