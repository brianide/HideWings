using Terraria.ID;
using Terraria.ModLoader;

namespace WinglessFlight.Items
{
    class Smoker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Smoke");
            Tooltip.SetDefault("Makes smoke and noise for fancier flying");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 24;
            item.rare = 3;
            item.accessory = true;
            item.vanity = true;
            item.value = 1000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(ItemID.Feather, 3);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
