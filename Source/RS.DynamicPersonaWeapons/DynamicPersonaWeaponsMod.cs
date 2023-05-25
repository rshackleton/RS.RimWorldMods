using HarmonyLib;
using RimWorld;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Verse;

namespace RS.DynamicPersonaWeapons
{
    public class DynamicPersonaWeaponsMod : Mod
    {
        public static DynamicPersonaWeaponsSettings settings;

        private static Rect _scrollRect = new Rect(0f, 0f, 500f, 9001f);
        private static Vector2 _scrollPosition;

        public DynamicPersonaWeaponsMod(ModContentPack pack) : base(pack)
        {
            settings = GetSettings<DynamicPersonaWeaponsSettings>();

            try
            {
                var harmony = new Harmony(Content.Name);
                harmony.PatchAll();

                Log.Message($"[RS] DynamicPersonaWeapons: Applied Harmony patches.");
            }
            catch (Exception e)
            {
                Log.Error($"[RS] DynamicPersonaWeapons: Failed to apply Harmony patches. Error: {e.Message}");
            }
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            var defs = DefSelectors.GetAllNonPersonaWeapons().OrderBy(def => def.label);

            var ls = new Listing_Standard();

            Widgets.BeginScrollView(inRect, ref _scrollPosition, _scrollRect);
            {
                ls.Begin(_scrollRect);
                {
                    foreach (var def in defs)
                    {
                        var checkOn = settings.allowlist.Contains(def.defName);
                        ls.CheckboxLabeled(def.label, ref checkOn);

                        if (checkOn)
                        {
                            settings.allowlist.Add(def.defName);
                        }
                        else
                        {
                            settings.allowlist.Remove(def.defName);
                        }
                    }
                }
                ls.End();
            }
            Widgets.EndScrollView();

            _scrollRect.height = ls.CurHeight;
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
        }

        public override string SettingsCategory()
        {
            return Content.Name;
        }
    }
}
