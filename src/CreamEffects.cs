using UnityEngine;

namespace UnknownCreams
{
    internal static class CreamEffects
    {
        private const float ReferenceDoseMl = 10f;
        private const float ReferenceSkinHeal = 3f;
        private const float ReferenceMuscleHeal = 3f;

        internal static void Apply(CreamDefinition def, float ml, Limb limb)
        {
            if (def == null || limb == null)
            {
                return;
            }

            float doses = ml / ReferenceDoseMl;
            if (doses <= 0f)
            {
                return;
            }

            ApplySkin(def, doses, limb);
            ApplyMuscle(def, doses, limb);
            ApplyDisinfect(def, doses, limb);
            ApplyBoneHeal(def, doses, limb);
            ApplyPain(def, doses, limb);

            if (def.Dye != null)
            {
                SkinDye.Apply(limb, def.Dye, doses);
            }

            Sound.Play("cream", limb.body.transform.position);
        }

        internal static void Drink(CreamDefinition def, float ml, Body body)
        {
            if (def == null || body == null || def.Drink != DrinkEffect.Aloe)
            {
                return;
            }

            body.Drink(ml * 0.001f * 35f);
            body.happiness -= ml * 0.05f;
            body.talker.EatBad();
        }

        private static void ApplySkin(CreamDefinition def, float doses, Limb limb)
        {
            if (def.SkinRate <= 0f)
            {
                return;
            }

            limb.skinHealAmount += ReferenceSkinHeal * def.SkinRate * doses;
        }

        private static void ApplyMuscle(CreamDefinition def, float doses, Limb limb)
        {
            if (def.MuscleRate <= 0f)
            {
                return;
            }

            float healed = ReferenceMuscleHeal * def.MuscleRate * doses;
            limb.muscleHealth = Mathf.Clamp(limb.muscleHealth + healed, 0f, 100f);
        }

        private static void ApplyDisinfect(CreamDefinition def, float doses, Limb limb)
        {
            if (def.DisinfectSeconds <= 0f)
            {
                return;
            }

            limb.SetDisinfect(def.DisinfectSeconds * Mathf.Clamp01(doses));
        }

        private static void ApplyBoneHeal(CreamDefinition def, float doses, Limb limb)
        {
            if (def.BoneHealSeconds <= 0f)
            {
                return;
            }

            limb.boneHealTimer = Mathf.Max(0f, limb.boneHealTimer - def.BoneHealSeconds * doses);
        }

        private static void ApplyPain(CreamDefinition def, float doses, Limb limb)
        {
            if (def.Pain == PainEffect.LocalAnesthetic)
            {
                ApplyLocalAnesthetic(limb, doses);
                return;
            }

            if (def.PainIncreaseMax <= 0f)
            {
                return;
            }

            float increase = Random.Range(def.PainIncreaseMin, def.PainIncreaseMax) * doses;
            limb.pain = Mathf.Clamp(limb.pain + increase, 0f, 100f);
            limb.body.happiness -= doses;
            limb.body.talker.EatBad();
        }

        private static void ApplyLocalAnesthetic(Limb limb, float doses)
        {
            string key = "unknowncream_lidocaine_" + limb.name;
            float duration = 60f * doses;

            CoUtils.instance.DoTimedOp(key, delegate
            {
                limb.pain = 0f;
                limb.forceMult = CoUtils.instance.DurationOf(key) > 2f ? 0f : 1f;
            }, duration);
        }
    }
}
