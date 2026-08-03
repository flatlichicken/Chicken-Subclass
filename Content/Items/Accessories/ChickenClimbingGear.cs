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
    public class ChickenClimbingGear : ModItem
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
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ItemRarityID.Green;
        }

        public override void Load() {
            EquipLoader.AddEquipTexture(Mod, Texture + "_Wings", EquipType.Wings, this);
            EquipLoader.AddEquipTexture(Mod, Texture + "_Shoes", EquipType.Shoes, this);
        }

        public override void UpdateVanity(Player player) {
            player.wings = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Wings);
            player.shoe = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Shoes);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            if (!hideVisual) {
                player.shoe = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Shoes);
            }

            bool isChickenWeapon = ChickenWeaponDamageBoost.IfUsingChickenWeapon(player);
            bool usingChickenOrb = player.GetModPlayer<ChickenOrbLogic>().UsingChickenOrb;

            if (isChickenWeapon)
            {
                player.wings = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Wings);
                player.jumpSpeedBoost += 1.5f;
                player.moveSpeed += 0.15f;
                player.spikedBoots = 2;
            }  

            if (usingChickenOrb)
            {
                int wingSlot = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Wings);
                
                player.wings = wingSlot;
                player.wingsLogic = wingSlot;
                if (isChickenWeapon) player.wingTimeMax = 120;
                else player.wingTimeMax = 100;
                player.noFallDmg = true;

                if (player.velocity.Y == 0) {
                    player.wingTime = player.wingTimeMax;
                }
            }
            if (usingChickenOrb) {
                player.accRunSpeed = 6f;
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
        public static bool FootActive() {
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
            Recipe ChickenClimbingGearRecipe = CreateRecipe();
            ChickenClimbingGearRecipe.AddIngredient(ModContent.ItemType<ChickenFoot>(), 1);
            ChickenClimbingGearRecipe.AddIngredient(ModContent.ItemType<ChickenWings>(), 1);
            ChickenClimbingGearRecipe.AddIngredient(ItemID.TigerClimbingGear, 1);
            ChickenClimbingGearRecipe.AddTile(TileID.TinkerersWorkbench);
            ChickenClimbingGearRecipe.Register();
        }
    }
}