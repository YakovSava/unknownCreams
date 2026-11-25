using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace UnknownCreams
{
    internal static class SkinDye
    {
        private sealed class DyeState
        {
            internal Color Color;
            internal float Strength;
            internal bool Snow;
        }

        private const float FadePerSecond = 0.006f;
        private const float MaxStrength = 0.85f;

        private static readonly Dictionary<int, DyeState> States = new Dictionary<int, DyeState>();
        private static readonly FieldInfo LimbMaterialField = AccessTools.Field(typeof(Limb), "mat");

        internal static void Apply(Limb limb, SkinDyeSpec spec, float doses)
        {
            if (limb == null || spec == null)
            {
                return;
            }

            int id = limb.GetInstanceID();
            if (!States.TryGetValue(id, out DyeState state))
            {
                state = new DyeState();
                States[id] = state;
            }

            state.Color = spec.Color;
            state.Strength = Mathf.Clamp(Mathf.Max(state.Strength, spec.Strength * doses), 0f, MaxStrength);
            state.Snow = IsWhite(spec.Color) && spec.Strength >= 0.5f;
        }

        internal static void Tick(Limb limb)
        {
            if (limb == null)
            {
                return;
            }

            int id = limb.GetInstanceID();
            if (!States.TryGetValue(id, out DyeState state))
            {
                return;
            }

            SpriteRenderer renderer = limb.GetComponent<SpriteRenderer>();
            state.Strength -= FadePerSecond * Time.deltaTime;

            if (state.Strength <= 0f)
            {
                States.Remove(id);
                if (renderer != null)
                {
                    renderer.color = Color.white;
                }
                return;
            }

            if (renderer != null)
            {
                renderer.color = Color.Lerp(Color.white, state.Color, state.Strength);
            }

            if (state.Snow)
            {
                ForceSnow(limb);
            }
        }

        internal static void Clear()
        {
            States.Clear();
        }

        private static void ForceSnow(Limb limb)
        {
            Material material = LimbMaterialField == null ? null : LimbMaterialField.GetValue(limb) as Material;
            if (material != null)
            {
                material.SetFloat("_SnowAmount", 0.85f);
            }
        }

        private static bool IsWhite(Color color)
        {
            return color.r > 0.85f && color.g > 0.85f && color.b > 0.85f;
        }
    }

    [HarmonyPatch(typeof(Limb), "Update")]
    internal static class LimbDyePatch
    {
        private static void Postfix(Limb __instance)
        {
            SkinDye.Tick(__instance);
        }
    }
}
