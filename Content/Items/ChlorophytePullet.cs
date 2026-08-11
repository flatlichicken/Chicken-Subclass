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
    public class ChlorophytePullet : ModItem
    {
        public static int ChlorophytePulletMissNum = 0;

        public override void SetDefaults()
        {
            Item.damage = 62;
            Item.DamageType = DamageClass.Melee;
            Item.width = 109;
            Item.height = 150;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 4, 80, 0);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = false;
            
            // Define the projectile here, but the Shoot hook below controls when it fires
            Item.shoot = ModContent.ProjectileType<ChlorophytePulletProjectile>();
            Item.shoot = ModContent.ProjectileType<ChickenChlorophyteFeatherProjectile>();
            Item.shootSpeed = 20f;
        }

		public override bool CanUseItem(Player player) {
          return player.ownedProjectileCounts[ModContent.ProjectileType<ChlorophytePulletProjectile>()] <= 0;
		}


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
    		

    	if (ChlorophytePulletMissNum >= 5) {
     	    ChlorophytePulletMissNum = 0; // Reset
        	Item.noUseGraphic = true;

            int secondaryProjectile = ModContent.ProjectileType<ChlorophytePulletProjectile>(); 
   		    Projectile.NewProjectile(source, position, velocity, secondaryProjectile, damage, knockback, player.whoAmI);

       	    return true;
            
  		}
        else
        {
         Item.noUseGraphic = false; // Show the sword for normal swings
         type = ModContent.ProjectileType<ChickenChlorophyteFeatherProjectile>(); 
         return true;
        }

		}

        public override void AddRecipes()
		{
			Recipe ChlorophytePulletRecipe = CreateRecipe();
			ChlorophytePulletRecipe.AddIngredient(ModContent.ItemType<Content.Items.PlantBasedNugget>(), 12);
			ChlorophytePulletRecipe.AddIngredient(ItemID.ChlorophyteBar, 5);
			ChlorophytePulletRecipe.AddTile(TileID.MythrilAnvil);
			ChlorophytePulletRecipe.Register();

		}
    }
}