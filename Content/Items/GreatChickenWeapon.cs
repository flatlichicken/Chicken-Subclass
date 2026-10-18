using System; //what sources the code uses, these sources allow for calling of terraria functions, existing system functions and microsoft vector functions (probably more)
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Projectiles; // the piece of code i found out i needed to type to use a custom projectile
using Terraria.GameContent;


namespace Chickensubclass.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class GreatChickenWeapon : ModItem
	{
		public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 4, true));
		}
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 65;
			Item.DamageType = DamageClass.Melee;
			Item.width = 87;
			Item.height = 63;
			Item.useTime = 40;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(0, 1, 35, 0);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.shoot = ModContent.ProjectileType<FeatherAimIndecatorFriendly>();
			Item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    // Define the number of projectiles to shoot, e.g., 5 shots
		    float numberProjectiles = 1;

		    // Define the spread angle in degrees
		    float spread = 360;

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
	}
}
