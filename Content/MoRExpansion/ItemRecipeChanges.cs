using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Items;

namespace Chickensubclass.Content.MoRExpansion
{
    public class ItemSCRecipeChanges : ModSystem
    {
        public override void PostAddRecipes() {
            if (ModLoader.TryGetMod("Chickensubclass", out Mod chickenMod) && ModLoader.TryGetMod("Redemption", out Mod redemptionMod)) {
                if (chickenMod.TryFind("ReaperChicken", out ModItem chickenItem) && redemptionMod.TryFind("XenomiteShard", out ModItem redemptionItem)) {
                    
                    Recipe targetRecipe = Main.recipe.FirstOrDefault(r => r.createItem.type == chickenItem.Type);
                    
                    if (targetRecipe != null) {
                        targetRecipe.DisableRecipe();
                    }

                    Recipe newReaperChickenRecipe = Recipe.Create(chickenItem.Type);
                    newReaperChickenRecipe.AddIngredient(ItemID.DeathSickle, 1);
                    newReaperChickenRecipe.AddIngredient(chickenMod.Find<ModItem>("RawChicken").Type, 10);
                    newReaperChickenRecipe.AddIngredient(redemptionItem.Type, 20);
                    newReaperChickenRecipe.AddTile(TileID.MythrilAnvil);
                    newReaperChickenRecipe.Register();
                }
            }
        }
    }
}
