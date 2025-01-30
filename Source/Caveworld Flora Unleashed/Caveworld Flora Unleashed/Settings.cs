using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Caveworld_Flora_Unleashed
{
    public class Settings : ModSettings
    {
        public static float spawnRate = 10f;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref spawnRate, "spawnRate", spawnRate, true);
        }

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard l = new Listing_Standard(GameFont.Small)
            {
                ColumnWidth = inRect.width
            };

            l.Begin(inRect);

            l.Label("Caveworld_Flora_Unleashed_Spawnrate_Label".Translate() + " : " + spawnRate.ToString("F1"),
                tooltip: "Caveworld_Flora_Unleashed_Spawnrate_Desc".Translate() + " [" + "Default".Translate() + ": " + 10f.ToString("F1") + "]");
            spawnRate = l.Slider(spawnRate, 0.1f, 40f);


            l.End();
        }
    }
}
