using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures; // REQUIRED for the Shoot hook
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles;

namespace Chickensubclass.Content.Items
{
    public class HolyChicken : ModItem
    {
        public static int HolyChickenSwingNum = 0;

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Melee;
            Item.width = 109;
            Item.height = 150;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 4, silver: 80);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = false;
            
            // Define the projectile here, but the Shoot hook below controls when it fires
            Item.shoot = ModContent.ProjectileType<HolyChickenProjectile>();
            Item.shootSpeed = 20f;
        }

		public override bool CanUseItem(Player player) {
          return player.ownedProjectileCounts[ModContent.ProjectileType<HolyChickenProjectile>()] <= 0;
		}

        // We use the Shoot hook to control the 'every 3rd swing' logic
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
    		HolyChickenSwingNum++;

    		if (HolyChickenSwingNum >= 5) {
     	   HolyChickenSwingNum = 0; // Reset
       		 Item.noUseGraphic = true; // Hide the sword so the spear looks right
       	 return true; // Fire the spear
  		  }

   		 Item.noUseGraphic = false; // Show the sword for normal swings
   		 return false; // Don't fire anything
			}

        public override void AddRecipes()
        {
            Recipe HolyChickenRecipe = CreateRecipe();
            // Note: Updated these to use the standard ModContent.ItemType syntax
            HolyChickenRecipe.AddIngredient(ModContent.ItemType<ChickenSoul>(), 10);
            HolyChickenRecipe.AddIngredient(ItemID.SoulofLight, 10);
            HolyChickenRecipe.AddIngredient(ModContent.ItemType<ChickenFeather>(), 10);
            HolyChickenRecipe.AddIngredient(ItemID.HallowedBar, 12);
            HolyChickenRecipe.AddTile(TileID.MythrilAnvil);
            HolyChickenRecipe.Register();
        }
    }
}