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
    public class TrueHolyChicken : ModItem
    {
        

        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.DamageType = DamageClass.Melee;
            Item.width = 109;
            Item.height = 150;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 4, silver: 80);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = false;
            
            // Define the projectile here, but the Shoot hook below controls when it fires
            Item.shoot = ModContent.ProjectileType<TrueHolyChickenProjectile>();
            Item.shootSpeed = 20f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Generates white light inside the swinging hitbox
            Lighting.AddLight(hitbox.Center.ToVector2(), 1.0f, 1.0f, 1.0f);
        }

        public override void AddRecipes()
        {
            Recipe TrueHolyChickenRecipe = CreateRecipe();
            // Note: Updated these to use the standard ModContent.ItemType syntax
            TrueHolyChickenRecipe.AddIngredient(ModContent.ItemType<HolyChicken>(), 1);
            TrueHolyChickenRecipe.AddIngredient(ModContent.ItemType<PlantBasedNugget>(), 24);
            TrueHolyChickenRecipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            TrueHolyChickenRecipe.AddTile(TileID.MythrilAnvil);
            TrueHolyChickenRecipe.Register();
        }
    }
}