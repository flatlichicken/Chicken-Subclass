using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Chickensubclass.Content.Items.Accessories
{   
    public class ChickenWings : ModItem
    {
        private static bool AccWorn;
        public override void SetStaticDefaults() {
            int wingSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Wings);
            ArmorIDs.Wing.Sets.Stats[wingSlot] = new WingStats(100, 6f, 1f);
        }

        public override void SetDefaults() {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 80);
            Item.rare = ItemRarityID.Blue;
        }

        public override void Load() {
            EquipLoader.AddEquipTexture(Mod, Texture + "_Wings", EquipType.Wings, this);
        }

        public override void UpdateVanity(Player player) {
            player.wings = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Wings);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
            bool usingChickenOrb = player.GetModPlayer<ChickenOrbLogic>().UsingChickenOrb;

            int wingSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Wings);
            player.wings = wingSlot;
            player.noFallDmg = true;

            if (isChickenWeapon)
            {
                player.jumpSpeedBoost += 1.5f;  
            }

            if (usingChickenOrb)
            {
                player.wingsLogic = wingSlot; // Enables airborne jump/flight controls
                if (isChickenWeapon) player.wingTimeMax = 120;
                else player.wingTimeMax = 100;

                // Recharges flight time while standing on the ground
                if (player.velocity.Y == 0) {
                    player.wingTime = player.wingTimeMax;
                }
            }
            AccWorn = true;
        }

        public static bool WingActive() {
            if (AccWorn == true) {
                AccWorn = false;
                return true;
            }
            else {
                return false;
            }
        }

        public override void AddRecipes()
        {
            Recipe ChickenWingsRecipe = CreateRecipe();
            ChickenWingsRecipe.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 20);
            ChickenWingsRecipe.AddIngredient(ItemID.DemoniteBar, 10);
            ChickenWingsRecipe.AddTile(TileID.WorkBenches);
            ChickenWingsRecipe.Register();

            Recipe ChickenWingsRecipe2 = CreateRecipe();
            ChickenWingsRecipe2.AddIngredient(ModContent.ItemType<Content.Items.ChickenFeather>(), 20);
            ChickenWingsRecipe2.AddIngredient(ItemID.CrimtaneBar, 10);
            ChickenWingsRecipe2.AddTile(TileID.WorkBenches);
            ChickenWingsRecipe2.Register();
        }
    }
}