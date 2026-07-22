
using Chickensubclass.Content.Items;
using Chickensubclass.Content.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Chickensubclass.Content
{
	// This class contains thoughtful examples of item recipe creation.
	// Recipes are explained in detail on the https://github.com/tModLoader/tModLoader/wiki/Basic-Recipes and https://github.com/tModLoader/tModLoader/wiki/Intermediate-Recipes wiki pages. Please visit the wiki to learn more about recipes if anything is unclear.
	public class VanillaRecipes : ModSystem
	{
		// A place to store the recipe group so we can easily use it later
		public static RecipeGroup VanillaRecipeGroup;

		public override void Unload() {
			VanillaRecipeGroup = null;
		}

		public override void AddRecipeGroups() {
			// Create a recipe group and store it
			// Language.GetTextValue("LegacyMisc.37") is the word "Any" in English, and the corresponding word in other languages
			VanillaRecipeGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ModContent.ItemType<Items.RawChicken>())}",
				ModContent.ItemType<Items.RawChicken>());

			// To avoid name collisions, when a modded items is the iconic or 1st item in a recipe group, name the recipe group: ModName:ItemName
			RecipeGroup.RegisterGroup("Chickensubclass:RawChicken", VanillaRecipeGroup);

			RecipeGroup GoldBarRecipeGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.GoldBar)}",
			ItemID.GoldBar, ItemID.PlatinumBar);
			RecipeGroup.RegisterGroup(nameof(ItemID.GoldBar), GoldBarRecipeGroup);

			RecipeGroup AdamantiteBarRecipeGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.AdamantiteBar)}",
			ItemID.AdamantiteBar, ItemID.TitaniumBar);
			RecipeGroup.RegisterGroup(nameof(ItemID.AdamantiteBar), AdamantiteBarRecipeGroup);

		}
		//public override void PostAddRecipes() {
		//	for (int i = 0; i < Recipe.numRecipes; i++) {
		//		Recipe recipe = Main.recipe[i];

				// All recipes that require wood will now need 100% more
		//		if (recipe.TryGetIngredient(ItemID.Wood, out Item ingredient)) {
		//			ingredient.stack *= 2;
		//		}
		//	}
		//}
	}
}
