// if you are reading this. this weapon has been removed. in hindsight i thought the joke was cheap, and the weapon working if you beat the moonlord would be too broken if used with other mods.

// with that being said, the code is still here. i just made it all comments

// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;
// using Terraria.DataStructures;
// using Microsoft.Xna.Framework;
// using System.Collections.Generic;
// 
// 
// namespace Chickensubclass.Content.Items
// { 
// 	// This is a basic item template.
// 	// Please see tModLoader's ExampleMod for every other example:
// 	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
// 	public class GodChicken : ModItem
// 	{
// 		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Chickensubclass.hjson' file.
// 		public override void SetDefaults()
// 		{
// 			Item.damage = 999999;
// 			Item.crit = 100;
// 			Item.DamageType = DamageClass.Melee;
// 			Item.width = 4000;
// 			Item.height = 4000;
// 			Item.useTime = 1;
// 			Item.useAnimation = 1;
// 			Item.useStyle = ItemUseStyleID.Swing;
// 			Item.knockBack = 9999999;
// 			Item.value = Item.buyPrice(silver: 1);
// 			Item.rare = ItemRarityID.Red;
// 			Item.UseSound = SoundID.Item1;
// 			Item.autoReuse = true;
// 			Item.useTurn = true;
// 		}
// 
// 		public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers) {
//    		 if (!NPC.downedMoonlord) {
// 		 
// 		 // This removes the weapon's massive damage
//    		 modifiers.SourceDamage *= 0; 
// 
//    		 // This adds exactly 1 damage back
//    		 modifiers.FlatBonusDamage += 1;
// 
//    		 // Optional: Stops the weapon from ever doing a critical hit
//    		 modifiers.DisableCrit(); 
//         }
// 		}
// 
// 
// 		public override void ModifyTooltips(List<TooltipLine> tooltips) {
//     // Check if the current player is a Hardcore character (difficulty 2)
//     if (Main.LocalPlayer.difficulty == 2 && !NPC.downedMoonlord) {
//         // Create the warning line
//         TooltipLine warningLine = new TooltipLine(Mod, "HardcoreWarning", "WARNING HARDCORE PLAYERS: THIS WEAPON WILL KILL YOU!") {
//             OverrideColor = Color.Red // Makes the text red
//         };
//         
//         // Add the line to the bottom of the tooltip list
//         tooltips.Add(warningLine);
//     }
// 
// 	if (NPC.downedMoonlord) {
//     TooltipLine worthyLine = new TooltipLine(Mod, "WorthyWarning", "You are worthy!") {
//             OverrideColor = Color.Yellow // Makes the text yellow
//     };
// 
// 	tooltips.Add(worthyLine);
//      
//     }
// 
//     }
// 		public override bool? UseItem(Player player) {
// 	if (!NPC.downedMoonlord) {	
//     // This kills the player as soon as they swing the sword
//     player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " wasn't worthy."), 9999, 0);
// 	}
//     return true;
// }
// 		public override void AddRecipes()
// 		{
// 
// 			Recipe GodChickenRecipe = CreateRecipe();
// 			GodChickenRecipe.AddIngredient(ModContent.ItemType<Content.Items.Chicken>());
// 			GodChickenRecipe.AddIngredient(ItemID.DirtBlock, 1);
// 			GodChickenRecipe.AddTile(TileID.WorkBenches);
// 			GodChickenRecipe.Register();
// 			
// 		}
// 	}
// }
// 