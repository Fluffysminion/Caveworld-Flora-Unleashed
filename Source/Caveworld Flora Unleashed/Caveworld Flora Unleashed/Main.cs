using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Caveworld_Flora_Unleashed
{
    public class Main : Mod
    {
        public static ModContentPack mcp;

        public Main(ModContentPack mcp) : base(mcp)
        {
            Main.mcp = mcp;
            GetSettings<Settings>();
        }
        public override string SettingsCategory()
        {
            return "Caveworld Flora Unleashed";
        }
        public override void DoSettingsWindowContents(Rect inRect) => Settings.DoSettingsWindowContents(inRect);
    }
}
