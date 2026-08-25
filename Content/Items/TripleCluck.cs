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
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class TripleCluck : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
        
		public override void SetDefaults()
		{
			Item.damage = 14;
			Item.DamageType = DamageClass.Melee;
			Item.width = 51;
			Item.height = 48;
			Item.useTime = 60;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 0, 35, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<TripleCluckProjectile>();
			Item.shootSpeed = 4f;


		}

		public override void HoldItem(Player player)
		{
			if (player.ownedProjectileCounts[Item.shoot] > 0)
			{
				player.heldProj = -1;
			}
		}

		public override void AddRecipes()
		{
			Recipe TripleCluckRecipe = CreateRecipe();
			TripleCluckRecipe.AddIngredient(ItemID.Wood, 100);
			TripleCluckRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 15);
			TripleCluckRecipe.AddTile(TileID.WorkBenches);
			TripleCluckRecipe.Register();

		}
		
	}
}

