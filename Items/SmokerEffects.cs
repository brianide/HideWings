using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace WinglessFlight.Items
{
    interface SmokerEffect
    {
        void DoEffects(Player player);
    }

    class SpectreSmokerEffect : SmokerEffect
    {
        private const int DustType = 16;
        private const float DustScale = 1.5f;
        private const int DustAlpha = 20;

        private int soundDelay = 0;

        public void DoEffects(Player player)
        {
            // Determine if the player is in the flight state
            int maxFlightTime = player.wingTimeMax + (player.rocketBoots == 0 ? 0 : player.rocketTimeMax * 6);
            if (player.controlJump && player.wings > 0 && player.wingTime > 0 && player.wingTime < maxFlightTime && player.velocity.Y != 0)
            {
                // Loop sound
                if (soundDelay <= 0)
                {
                    Main.PlaySound(SoundID.Item24, player.position);
                    soundDelay = 20;
                }
                soundDelay--;

                // Make dust for each foot
                for (int nozzle = 0; nozzle < 2; nozzle++)
                {
                    float xOffset = nozzle == 0 ? -4f : player.width - 4f;
                    float xForce = nozzle == 0 ? -2f : 2f;
                    float yOffset = player.gravDir == -1f ? 4 : player.height;

                    // Create dust and set its velocity
                    int dustIndex = Dust.NewDust(new Vector2(player.position.X + xOffset, player.position.Y + yOffset - 10f), 8, 8, DustType, 0f, 0f, DustAlpha, default(Color), DustScale);
                    Dust dust = Main.dust[dustIndex];
                    dust.velocity.X = xForce - player.velocity.X * 0.3f;
                    dust.velocity.Y = 2f * player.gravDir - player.velocity.Y * 0.3f;
                    dust.velocity *= 0.1f;

                    // Inherit shader from wing dye slot
                    dust.shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                }
            }
            else
            {
                soundDelay = 0;
            }
        }
    }
}
