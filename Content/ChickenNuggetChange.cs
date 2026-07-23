using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Chickensubclass.Content.Items;
using Chickensubclass.Content.Buffs;
    
namespace Content.ChickenSubClass
{
    public class ChickenNuggetChange : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.ChickenNugget)
            {
                item.buffTime = 7200;
                item.buffType = ModContent.BuffType<ChickenInstinct>();
            }
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            if (item.type == ItemID.ChickenNugget)
            {
                player.ClearBuff(ModContent.BuffType<ChickenPower>());
                player.ClearBuff(ModContent.BuffType<ChickenAnger>());
                player.ClearBuff(ModContent.BuffType<ChickenRage>());
            }
        }
        
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.ChickenNugget) 
            {
                tooltips.RemoveAll(line => line.Mod == "Terraria" && line.Name == "Tooltip1");

                foreach (TooltipLine line in tooltips)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                    {
                        line.Text = "Grants Chicken Instinct";
                    }
                }
            }
        }
    }

    public class ChickenNuggetRecipeSystem : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe ChickenNuggetRecipe = Recipe.Create(ItemID.ChickenNugget);
            ChickenNuggetRecipe.AddIngredient(ModContent.ItemType<RawChicken>(), 1);
            ChickenNuggetRecipe.AddTile(TileID.CookingPots);
            ChickenNuggetRecipe.Register();
        }
    }
}
