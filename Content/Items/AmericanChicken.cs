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
    public class AmericanChicken : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Melee;
            Item.width = 92;
            Item.height = 90;
            Item.useTime = 8;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 1, 10, 0);
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.shoot = ModContent.ProjectileType<AmericanChickenProjectile>();
            Item.shootSpeed = 12f;
            Item.UseSound = SoundID.Item11;
            Item.useAmmo = AmmoID.Bullet;
        }

		public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.33f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Define the number of projectiles to shoot, e.g., 5 shots
            float numberProjectiles = 1;

            // Define the spread angle in degrees
            float spread = 5;

            for (int i = 0; i < numberProjectiles; i++)
            {
                // Randomize the velocity of each projectile
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread));

                // Spawn the projectile
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            // Return false to stop the default projectile from being spawned
            return false;
        }

        public override void AddRecipes()
        {
            Recipe AmericanChickenRecipe = CreateRecipe();
            AmericanChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 15);
            AmericanChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 12);
            AmericanChickenRecipe.AddIngredient(ItemID.FlintlockPistol, 1);
            AmericanChickenRecipe.AddTile(TileID.Anvils);
            AmericanChickenRecipe.Register();
        }
    }
}