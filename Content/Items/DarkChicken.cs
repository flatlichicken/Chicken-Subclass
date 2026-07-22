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
    public class DarkChicken : ModItem
    {
        public static int DarkChickenSwingNum = 0;

        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee;
            Item.width = 120;
            Item.height = 141;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 4);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = false;
            
            // Define the projectile here, but the Shoot hook below controls when it fires
            Item.shoot = ModContent.ProjectileType<DarkChickenProjectile>();
            Item.shootSpeed = 20f;
        }

		public override bool CanUseItem(Player player) {
          return player.ownedProjectileCounts[ModContent.ProjectileType<DarkChickenProjectile>()] <= 0;
		}

        // We use the Shoot hook to control the 'every 3rd swing' logic
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
    		DarkChickenSwingNum++;

             float offsetDistance = 20f; 
    
         // Calculate the spawn position based on player direction
        Vector2 spawnPosition = player.Center + new Vector2(offsetDistance * player.direction, 0f);


    		if (DarkChickenSwingNum >= 3) {
     	   DarkChickenSwingNum = 0; // Reset
       		 Item.noUseGraphic = true; // Hide the sword so the spear looks right
       	 return true; // Fire the spear
  		  }

   		 Item.noUseGraphic = false; // Show the sword for normal swings
   		 return false; // Don't fire anything
			}

        public override void AddRecipes()
        {
            Recipe DarkChickenRecipe = CreateRecipe();
            // Note: Updated these to use the standard ModContent.ItemType syntax
            DarkChickenRecipe.AddIngredient(ModContent.ItemType<EvilChicken>(), 1);
            DarkChickenRecipe.AddIngredient(ModContent.ItemType<RedJunglefowl>(), 1);
            DarkChickenRecipe.AddIngredient(ModContent.ItemType<FireChicken>(), 1);
            DarkChickenRecipe.AddIngredient(ModContent.ItemType<NinjaChicken>(), 1);
            DarkChickenRecipe.AddTile(TileID.Anvils);
            DarkChickenRecipe.Register();

            Recipe DarkChickenRecipe2 = CreateRecipe();
            // Note: Updated these to use the standard ModContent.ItemType syntax
            DarkChickenRecipe2.AddIngredient(ModContent.ItemType<NuggetChicken>(), 1);
            DarkChickenRecipe2.AddIngredient(ModContent.ItemType<RedJunglefowl>(), 1);
            DarkChickenRecipe2.AddIngredient(ModContent.ItemType<FireChicken>(), 1);
            DarkChickenRecipe2.AddIngredient(ModContent.ItemType<NinjaChicken>(), 1);
            DarkChickenRecipe2.AddTile(TileID.Anvils);
            DarkChickenRecipe2.Register();
        }
    }
}