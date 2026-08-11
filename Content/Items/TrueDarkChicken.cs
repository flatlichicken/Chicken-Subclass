using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures; // REQUIRED for the Shoot hook
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles;
using System.Runtime.CompilerServices;

namespace Chickensubclass.Content.Items
{
    public class TrueDarkChicken : ModItem
    {
        public static int TrueDarkChickenSwingNum = 0;

        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.DamageType = DamageClass.Melee;
            Item.width = 120;
            Item.height = 141;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = false;
            
            // Define the projectile here, but the Shoot hook below controls when it fires
            Item.shoot = ModContent.ProjectileType<TrueDarkChickenProjectile>();
            Item.shootSpeed = 20f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Generates white light inside the swinging hitbox
            Lighting.AddLight(hitbox.Center.ToVector2(), 1.0f, 1.0f, 1.0f);
        }


        public override void AddRecipes()
        {
            Recipe TrueDarkChickenRecipe = CreateRecipe();
            // Note: Updated these to use the standard ModContent.ItemType syntax
            TrueDarkChickenRecipe.AddIngredient(ModContent.ItemType<DarkChicken>(), 1);
            TrueDarkChickenRecipe.AddIngredient(ItemID.SoulofFright, 20);
            TrueDarkChickenRecipe.AddIngredient(ItemID.SoulofMight, 20);
            TrueDarkChickenRecipe.AddIngredient(ItemID.SoulofSight, 20);
            TrueDarkChickenRecipe.AddTile(TileID.MythrilAnvil);
            TrueDarkChickenRecipe.Register();
        }
    }
}