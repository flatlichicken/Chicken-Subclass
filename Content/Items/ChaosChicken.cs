using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles; 


namespace Chickensubclass.Content.Items
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class ChaosChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 42;
			Item.DamageType = DamageClass.Melee;
			Item.width = 89;
			Item.height = 95;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 4);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<ChaosChickenProjectile>();
			Item.shootSpeed = 10f;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Generates white light inside the swinging hitbox
            Lighting.AddLight(hitbox.Center.ToVector2(), 1.0f, 1.0f, 1.0f);
        }




public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
{
    float numberProjectiles = 1;

    int rangeX = 0; 
    int rangeY = 100; // Updated per request

    // Manually calculate the angle toward the mouse since shootSpeed is 0
    float areaRotation = player.AngleTo(Main.MouseWorld);

    for (int i = 0; i < numberProjectiles; i++)
    {
    // 1. Define the random box offset
    Vector2 offset = new Vector2(Main.rand.Next(0, rangeX), Main.rand.Next(-rangeY, rangeY));

    // 2. Rotate the offset by the aim angle
    Vector2 spawnPos = player.Center + offset.RotatedBy(areaRotation);

    // 3. CALCULATE VELOCITY: 
    // Take a unit vector pointing at the angle, then multiply by speed (e.g., 10f)
    Vector2 moveVelocity = areaRotation.ToRotationVector2() * 10f; 

    // 4. Spawn the projectile with the new velocity
    Projectile.NewProjectile(source, spawnPos, moveVelocity, type, damage, knockback, player.whoAmI, areaRotation);
    }

    return false;
}


		public override void AddRecipes()
		{
			Recipe ChaosChickenRecipe = CreateRecipe();
			ChaosChickenRecipe.AddIngredient(ItemID.CrystalShard, 20);
			ChaosChickenRecipe.AddIngredient(ItemID.SoulofLight, 15);
			ChaosChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenSoul>(), 10);
			ChaosChickenRecipe.AddTile(TileID.MythrilAnvil);
			ChaosChickenRecipe.Register();

		}
	}
}

