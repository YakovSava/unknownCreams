using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace UnknownCreams
{
    internal static class CreamContainers
    {
        internal static void Ensure(WaterContainerItem container)
        {
            if (container == null)
            {
                return;
            }

            Item item = container.GetComponent<Item>();
            if (item == null || !CreamCatalog.ById.ContainsKey(item.id))
            {
                return;
            }

            if (!(item.Stats is LiquidItemInfo info) || info.defaultContents == null || info.defaultContents.Count == 0)
            {
                return;
            }

            bool empty = container.stack == null || container.stack.Count == 0;
            float total = empty ? 0f : container.CurrentTotal;
            bool overfilled = total > info.capacity + 0.5f;

            if (!empty && !overfilled)
            {
                return;
            }

            if (empty && item.condition <= 0.001f)
            {
                return;
            }

            float condition = Mathf.Clamp01(item.condition);
            container.stack = new List<LiquidStack>();
            foreach (LiquidStack stack in info.defaultContents)
            {
                if (stack != null)
                {
                    container.stack.Add(new LiquidStack(stack.liquidId, stack.amount * condition));
                }
            }

            item.condition = condition;
        }
    }

    [HarmonyPatch(typeof(WaterContainerItem), "Start")]
    internal static class CreamContainerStartPatch
    {
        private static void Prefix(WaterContainerItem __instance)
        {
            CreamContainers.Ensure(__instance);
        }
    }
}
