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
	public class BinChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 115;
			Item.DamageType = DamageClass.Melee;
			Item.width = 145;
			Item.height = 110;
			Item.useTime = 10;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.buyPrice(gold: 7);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<BinChickenProjectile>();
			Item.shootSpeed = 10f;
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Stinky, 300);
        }




		public override void AddRecipes()
		{
			Recipe BinChickenRecipe = CreateRecipe();
			BinChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 15);
			BinChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 10);
			BinChickenRecipe.AddIngredient(ItemID.LunarBar, 10);
			BinChickenRecipe.AddTile(TileID.LunarCraftingStation);
			BinChickenRecipe.Register();

		}
	}
}

