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
	public class ReaperChicken : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 26;
			Item.DamageType = DamageClass.Melee;
			Item.width = 93;
			Item.height = 160;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.buyPrice(gold: 8);
			Item.rare = ItemRarityID.LightPurple;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<ReaperChickenFeatherProjectile>();
			Item.shootSpeed = 2f;


		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    // Define the number of projectiles to shoot, e.g., 5 shots
    float numberProjectiles = 5;

    // Define the spread angle in degrees
    float spread = 15;

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

		public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
{
	// This forces a brand new line into existence so Terraria has something to draw.
	TooltipLine line = new TooltipLine(Mod, "AmalgamateLine1", "Don't pick on me.");
	tooltips.Add(line);
}

		public override void PostDrawTooltipLine(DrawableTooltipLine line)
		{
			// Once the line above is created, we catch it right here as it draws on screen.
			if (line.Mod == "Chickensubclass" && line.Name == "AmalgamateLine1")
			{
				string[] extraOverlappingLines = new string[] {
					"Bawk bawk bawk bawk bakawk.",
					"I took chickens to kfc and nobody will know.", // unless they look in the code
					"I don't want to be a pie! I don't like gravy.",

					
				};

				for (int i = 0; i < extraOverlappingLines.Length; i++)
				{
					Utils.DrawBorderStringFourWay(
						Main.spriteBatch,
						FontAssets.MouseText.Value,
						extraOverlappingLines[i],
						line.X,
						line.Y,
						Color.White,
						Color.Black,
						Vector2.Zero,
						1f
					);
				}
			}
		}
		public override void AddRecipes()
		{
			Recipe ReaperChickenRecipe = CreateRecipe();
			ReaperChickenRecipe.AddIngredient(ItemID.DeathSickle, 1);
			ReaperChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.RawChicken>(), 10);
			ReaperChickenRecipe.AddIngredient(ItemID.Gel, 50);
			ReaperChickenRecipe.AddTile(TileID.MythrilAnvil);
			ReaperChickenRecipe.Register();

		}
		
	}
}

