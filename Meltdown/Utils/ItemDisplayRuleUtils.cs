using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Utils
{
    public static class ItemDisplayRuleUtils
    {
        public static CharacterModel.RendererInfo[] ItemDisplaySetup(GameObject obj)
        {
            MeshRenderer[] meshes = obj.GetComponentsInChildren<MeshRenderer>();
            CharacterModel.RendererInfo[] renderInfos = new CharacterModel.RendererInfo[meshes.Length];

            for (int i = 0; i < meshes.Length; i++)
            {
                renderInfos[i] = new CharacterModel.RendererInfo
                {
                    defaultMaterial = meshes[i].material,
                    renderer = meshes[i],
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                };
            }

            return renderInfos;
        }

        #region Commons
        public static ItemDisplayRuleDict getReactorVentsDisplay(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0F, 0.40287F, -0.14718F),
                localAngles = new Vector3(0F, 270F, 324.7447F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "BowHinge2L",
                localPos = new Vector3(-0.02178F, 0.26937F, 0.06629F),
                localAngles = new Vector3(293.4217F, 0F, 0F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MainWeapon",
                localPos = new Vector3(-0.06282F, 0.24614F, 0.04432F),
                localAngles = new Vector3(270F, 90.00001F, 0F),
                localScale = new Vector3(0.007F, 0.007F, 0.007F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.00003F, 0.69903F, -1.86983F),
                localAngles = new Vector3(0F, 270F, 0F),
                localScale = new Vector3(0.15F, 0.15F, 0.15F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0F, 0.15883F, -0.30878F),
                localAngles = new Vector3(0F, 270F, 1F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "LowerArmR",
                localPos = new Vector3(-0.08718F, 0.18809F, 0.02215F),
                localAngles = new Vector3(0F, 23F, 0F),
                localScale = new Vector3(0.008F, 0.008F, 0.008F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0F, -0.04079F, -0.18073F),
                localAngles = new Vector3(0F, 270F, 30.93145F),
                localScale = new Vector3(0.007F, 0.007F, 0.007F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "PlatformBase",
                localPos = new Vector3(0F, -0.67281F, -0.00001F),
                localAngles = new Vector3(0F, 180F, 90F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.00006F, -0.01843F, -0.38223F),
                localAngles = new Vector3(0.07307F, 270.6938F, 353.9877F),
                localScale = new Vector3(0.025F, 0.025F, 0.025F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighR",
                localPos = new Vector3(-1.15753F, 0.17329F, -0.44305F),
                localAngles = new Vector3(66.30933F, 146.4377F, 168.2153F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MuzzleGun",
                localPos = new Vector3(0.00626F, -0.04132F, 0.02196F),
                localAngles = new Vector3(0F, 90F, 90F),
                localScale = new Vector3(0.008F, 0.008F, 0.008F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(0.06231F, -0.27293F, -0.12392F),
                localAngles = new Vector3(0F, 267.7598F, 0F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.00996F, -0.06498F, -0.17867F),
                localAngles = new Vector3(2.08022F, 265.767F, 26.12463F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Stomach",
                localPos = new Vector3(0F, -0.01632F, -0.12763F),
                localAngles = new Vector3(0F, 270F, 358.4243F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighR",
                localPos = new Vector3(0.0328F, 0.18611F, 0.17325F),
                localAngles = new Vector3(0.72311F, 276.9463F, 174.086F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.17964F, -0.24609F, -0.20686F),
                localAngles = new Vector3(0F, 0F, 90F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Weapon",
                localPos = new Vector3(3.38962F, 9.95801F, 1.55354F),
                localAngles = new Vector3(270F, 159.1111F, 0F),
                localScale = new Vector3(0.25F, 0.25F, 0.25F)
            });

            return dict;
        }
        public static ItemDisplayRuleDict getPlutoniumRoundsDisplay(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Stomach",
                localPos = new Vector3(-0.15941F, 0.08237F, 0.06182F),
                localAngles = new Vector3(7.8795F, 268.1693F, 350.1749F),
                localScale = new Vector3(0.008F, 0.008F, 0.008F)
            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(0.09547F, 0.04931F, 0.10034F),
                localAngles = new Vector3(335.7137F, 275.5423F, 204.2753F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MainWeapon",
                localPos = new Vector3(-0.05669F, 0.24714F, -0.0569F),
                localAngles = new Vector3(16.74946F, 346.4152F, 353.9704F),
                localScale = new Vector3(0.005F, 0.005F, 0.005F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(1.94024F, 1.84717F, 3.29224F),
                localAngles = new Vector3(8.60846F, 339.2685F, 358.1092F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "UpperArmL",
                localPos = new Vector3(0.08257F, 0.21698F, 0.02291F),
                localAngles = new Vector3(338.1512F, 96.91885F, 181.927F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "UpperArmL",
                localPos = new Vector3(0.05681F, 0.03635F, 0.00371F),
                localAngles = new Vector3(350.0081F, 113.2673F, 165.6893F),
                localScale = new Vector3(0.008F, 0.008F, 0.008F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(0.13142F, 0.19811F, 0.05761F),
                localAngles = new Vector3(353.4031F, 89.24884F, 171.3585F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "WeaponPlatformEnd",
                localPos = new Vector3(0.00191F, 0.0607F, 0.20772F),
                localAngles = new Vector3(11.4496F, 340.0925F, 355.1942F),
                localScale = new Vector3(0.015F, 0.015F, 0.015F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.12509F, 0.20693F, 0.18994F),
                localAngles = new Vector3(357.751F, 1.87589F, 3.4417F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "LowerArmR",
                localPos = new Vector3(1.10173F, 3.8498F, 0.62513F),
                localAngles = new Vector3(19.72316F, 44.13465F, 356.5359F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MuzzleGun",
                localPos = new Vector3(0.00064F, 0.02168F, 0.02567F),
                localAngles = new Vector3(71.00872F, 221.4173F, 218.6868F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "SMG",
                localPos = new Vector3(0F, -0.15161F, 0.35862F),
                localAngles = new Vector3(67.31993F, 225.3311F, 224.6011F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.12644F, 0.14831F, -0.12397F),
                localAngles = new Vector3(42.03844F, 215.9946F, 171.1821F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "LowerArmL",
                localPos = new Vector3(-0.01083F, 0.15514F, -0.0603F),
                localAngles = new Vector3(350.3902F, 26.64341F, 175.2377F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "UpperArmL",
                localPos = new Vector3(0.00213F, 0.24798F, -0.13446F),
                localAngles = new Vector3(346.575F, 5.53961F, 169.9939F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Cleaver",
                localPos = new Vector3(-0.08503F, 0.26125F, -0.01928F),
                localAngles = new Vector3(12.87262F, 344.0854F, 357.5269F),
                localScale = new Vector3(0.018F, 0.018F, 0.018F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(-3.90675F, 4.04986F, -4.1389F),
                localAngles = new Vector3(17.88783F, 342.2997F, 354.573F),
                localScale = new Vector3(0.2F, 0.2F, 0.2F)
            });

            return dict;
        }

        public static ItemDisplayRuleDict getTargetLockVisorDisplay(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.09914F, 0.26232F, 0.11024F),
                localAngles = new Vector3(353.3396F, 71.95181F, 339.0948F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.03764F, 0.22506F, 0.06975F),
                localAngles = new Vector3(359.9473F, 89.93391F, 344.03F),
                localScale = new Vector3(0.024F, 0.024F, 0.024F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0347F, 0.03995F, 0.10617F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.62622F, 2.86927F, -1.08764F),
                localAngles = new Vector3(0F, 270F, 300F),
                localScale = new Vector3(0.16F, 0.16F, 0.16F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "HeadCenter",
                localPos = new Vector3(-0.09432F, -0.01204F, 0.10642F),
                localAngles = new Vector3(2.00377F, 76.38385F, 359.7849F),
                localScale = new Vector3(0.025F, 0.025F, 0.025F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.05453F, 0.08304F, 0.06564F),
                localAngles = new Vector3(352.9014F, 76.66881F, 352.6046F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.06717F, 0.15348F, 0.11343F),
                localAngles = new Vector3(355.926F, 74.15999F, 349.1862F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Eye",
                localPos = new Vector3(-0.08335F, 0.85823F, 0.05708F),
                localAngles = new Vector3(0F, 90F, 270F),
                localScale = new Vector3(0.06F, 0.06F, 0.06F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0862F, 0.1022F, 0.10971F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(1.49518F, 1.44877F, 0.14045F),
                localAngles = new Vector3(271.6438F, 359.5333F, 160.88F),
                localScale = new Vector3(0.2F, 0.2F, 0.2F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0344F, 0.07984F, 0.11363F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0419F, 0.05368F, 0.088F),
                localAngles = new Vector3(4.00404F, 68.69869F, 357.5813F),
                localScale = new Vector3(0.015F, 0.015F, 0.015F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.05399F, 0.10653F, 0.14045F),
                localAngles = new Vector3(20.92575F, 83.28931F, 296.3018F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.07134F, 0.12183F, 0.09175F),
                localAngles = new Vector3(355.3813F, 79.93002F, 341.2799F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.04345F, 0.21399F, 0.1371F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.17323F, 0.06413F, 0.10147F),
                localAngles = new Vector3(321.7742F, 180.032F, 268.8741F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-6.21657F, 2.51515F, 1.576F),
                localAngles = new Vector3(338.7009F, 99.2964F, 236.0627F),
                localScale = new Vector3(1F, 1F, 1F)
            });

            return dict;
        }

        public static ItemDisplayRuleDict getThermiteInACanDisplayRules(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Stomach",
                localPos = new Vector3(0.17118F, 0.04114F, 0.05116F),
                localAngles = new Vector3(329.7548F, 326.7891F, 278.6393F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Stomach",
                localPos = new Vector3(-0.13288F, 0.06159F, -0.04239F),
                localAngles = new Vector3(28.36226F, 334.0051F, 67.93385F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Stomach",
                localPos = new Vector3(-0.19772F, 0.03736F, 0.07284F),
                localAngles = new Vector3(39.70339F, 21.92593F, 86.28722F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(1.89671F, 2.64017F, 2.39896F),
                localAngles = new Vector3(10F, 0F, 0F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Base",
                localPos = new Vector3(0.166F, 0.41205F, 0.04225F),
                localAngles = new Vector3(0.00019F, -0.00004F, 326.9903F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Base",
                localPos = new Vector3(0.16195F, 0.16193F, -0.03832F),
                localAngles = new Vector3(340.6832F, -0.0001F, 135.2674F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighR",
                localPos = new Vector3(-0.09045F, -0.0352F, 0.06223F),
                localAngles = new Vector3(314.5728F, 31.03102F, 83.9058F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.7328F, 1.45249F, 0.37783F),
                localAngles = new Vector3(10.0001F, 236.1884F, 0.00002F),
                localScale = new Vector3(0.015F, 0.015F, 0.015F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MechUpperArmL",
                localPos = new Vector3(0.00586F, -0.029F, -0.05483F),
                localAngles = new Vector3(16.02692F, 42.29048F, 204.3944F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MouthMuzzle",
                localPos = new Vector3(0.72011F, 2.48662F, 3.79712F),
                localAngles = new Vector3(5.9375F, 323.3838F, 36.37123F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Stomach",
                localPos = new Vector3(-0.12797F, 0.07607F, 0.16671F),
                localAngles = new Vector3(79.8959F, 248.4059F, 268.7556F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(0.16934F, 0.09168F, -0.00837F),
                localAngles = new Vector3(3.52074F, 196.2048F, 115.6745F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(-0.00178F, 0.0512F, 0.15892F),
                localAngles = new Vector3(323.8983F, 83.52626F, 111.014F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(0.223F, 0.02415F, 0.00068F),
                localAngles = new Vector3(359.927F, 328.6273F, 301.9252F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(0.25243F, 0.197F, -0.02994F),
                localAngles = new Vector3(26.76762F, 4.74372F, 8.52115F),
                localScale = new Vector3(0.013F, 0.013F, 0.013F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.28905F, -0.11805F, -0.23275F),
                localAngles = new Vector3(335F, 10F, 86F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(4.89482F, 3.80221F, -4.08336F),
                localAngles = new Vector3(57.4096F, 74.6315F, 273.2854F),
                localScale = new Vector3(0.2F, 0.2F, 0.2F)
            });

            return dict;
        }

        public static ItemDisplayRuleDict getExhaustPipeDisplay(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.12215F, 0.02963F, -0.25856F),
                localAngles = new Vector3(0.40735F, 198.9882F, 357.641F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)

            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(0.18378F, -0.11043F, 0.15704F),
                localAngles = new Vector3(338.1441F, 27.06536F, 3.51974F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Base",
                localPos = new Vector3(0.22501F, -0.06262F, -0.03318F),
                localAngles = new Vector3(30.16914F, 65.22382F, 78.29147F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Hip",
                localPos = new Vector3(-1.86691F, 1.68062F, 0F),
                localAngles = new Vector3(0.03912F, 272.0335F, 178.8976F),
                localScale = new Vector3(1F, 1F, 1F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.20692F, -0.11508F, -0.23675F),
                localAngles = new Vector3(5.93865F, 138.648F, 9.60196F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.21324F, 0.05602F, -0.32742F),
                localAngles = new Vector3(61.06005F, 214.0115F, 176.5875F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.11957F, -0.15628F, -0.15503F),
                localAngles = new Vector3(38.04384F, 198.1767F, 7.79561F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Base",
                localPos = new Vector3(0F, -0.54186F, 0.54122F),
                localAngles = new Vector3(90F, 180F, 0F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0F, -0.28891F, -0.37843F),
                localAngles = new Vector3(19.82063F, 178.7155F, 359.1089F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MouthMuzzle",
                localPos = new Vector3(-2.27306F, 2.07515F, 2.05068F),
                localAngles = new Vector3(6.13196F, 294.034F, 196.5764F),
                localScale = new Vector3(1F, 1F, 1F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MuzzleGun",
                localPos = new Vector3(0.00338F, -0.07861F, -0.35541F),
                localAngles = new Vector3(0F, 180F, 0F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(0.15902F, -0.50564F, -0.05298F),
                localAngles = new Vector3(52.41477F, 170.3197F, 358.8878F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "LargeExhaust2L",
                localPos = new Vector3(-0.03567F, 0.09877F, -0.04494F),
                localAngles = new Vector3(319.1699F, 179.8685F, 161.4536F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pack",
                localPos = new Vector3(0.22689F, -0.34654F, -0.16306F),
                localAngles = new Vector3(34.00013F, 124.4962F, 295.006F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(-0.2168F, -0.04489F, -0.28574F),
                localAngles = new Vector3(7.43968F, 194.8892F, 1.90041F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.25078F, -0.30646F, -0.28946F),
                localAngles = new Vector3(75.4513F, 134.9794F, 229.7695F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(-7.91568F, -0.05987F, -4.85041F),
                localAngles = new Vector3(2.4032F, 207.4334F, 0.43582F),
                localScale = new Vector3(3F, 3F, 3F)
            });

            return dict;
        }
        #endregion

        #region Rares
        public static ItemDisplayRuleDict getLeakyReactorCoolantDisplay(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.24849F, 0.04667F, -0.1494F),
                localAngles = new Vector3(0.41721F, 92.15807F, 7.68711F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighR",
                localPos = new Vector3(-0.09135F, 0.11012F, -0.04091F),
                localAngles = new Vector3(17.81258F, 249.3997F, 196.864F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighR",
                localPos = new Vector3(-0.07157F, 0.26183F, 0.02836F),
                localAngles = new Vector3(354.4018F, 300.3515F, 167.155F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-2.20399F, 1.14798F, 2.07462F),
                localAngles = new Vector3(0.2671F, 289.7018F, 0.11631F),
                localScale = new Vector3(0.17F, 0.17F, 0.17F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(-0.31778F, 0.08595F, 0.02671F),
                localAngles = new Vector3(357.5472F, 59.89194F, 177.4843F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(0.07488F, 0.14799F, 0.14887F),
                localAngles = new Vector3(14.98014F, 62.10304F, 175.2345F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.1414F, -0.05605F, -0.22881F),
                localAngles = new Vector3(339.3058F, 43.84799F, 337.533F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "FootBackL",
                localPos = new Vector3(-0.13307F, 0.31822F, -0.18084F),
                localAngles = new Vector3(2.72422F, 340.3991F, 183.0304F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MechBase",
                localPos = new Vector3(-0.26839F, -0.18105F, -0.1644F),
                localAngles = new Vector3(3.02276F, 100.0951F, 351.3393F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Hip",
                localPos = new Vector3(-1.95816F, 1.86818F, 0.41544F),
                localAngles = new Vector3(359.5434F, 293.5136F, 189.8523F),
                localScale = new Vector3(0.2F, 0.2F, 0.2F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Stomach",
                localPos = new Vector3(0.14819F, 0.0745F, 0.13536F),
                localAngles = new Vector3(9.50468F, 35.23292F, 343.5568F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(-0.10411F, -0.29876F, -0.18884F),
                localAngles = new Vector3(10.59585F, 14.22324F, 329.0103F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(-0.18225F, 0.18702F, -0.17379F),
                localAngles = new Vector3(346.8251F, 9.82358F, 178.6611F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(0.34131F, -0.05956F, -0.10069F),
                localAngles = new Vector3(10.35429F, 332.588F, 17.92761F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(-0.18137F, -0.0961F, -0.32858F),
                localAngles = new Vector3(5.27841F, 46.10352F, 354.4648F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.09735F, -0.22148F, -0.16374F),
                localAngles = new Vector3(70.3034F, 10.26087F, 90.63699F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(-9.10168F, 6.22546F, -2.13551F),
                localAngles = new Vector3(2.40257F, 100.8797F, 355.9927F),
                localScale = new Vector3(0.4F, 0.4F, 0.4F)
            });

            return dict;
        }
        public static ItemDisplayRuleDict getUraniumFuelRodDisplayRules(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(0.12914F, 0.08444F, 0.02012F),
                localAngles = new Vector3(0F, 353.4648F, 0F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "BowBase",
                localPos = new Vector3(0.00144F, -0.00812F, -0.01455F),
                localAngles = new Vector3(90F, 90F, 0F),
                localScale = new Vector3(0.02F, 0.017F, 0.02F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MainWeapon",
                localPos = new Vector3(-0.07533F, 0.55289F, -0.02753F),
                localAngles = new Vector3(0F, 81.49352F, 0F),
                localScale = new Vector3(0.02F, 0.018F, 0.02F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "UpperArmR",
                localPos = new Vector3(0.00048F, 2.90551F, 0.57676F),
                localAngles = new Vector3(0F, 90F, 353.4091F),
                localScale = new Vector3(0.2F, 0.2F, 0.2F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(0.27859F, 0.28048F, 0.00276F),
                localAngles = new Vector3(0F, 0F, 0F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighR",
                localPos = new Vector3(-0.09242F, 0.31346F, 0.08538F),
                localAngles = new Vector3(0F, 0F, 355.7726F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighR",
                localPos = new Vector3(-0.13684F, 0.14139F, 0.01386F),
                localAngles = new Vector3(0F, 0F, 4.95002F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "WeaponPlatformEnd",
                localPos = new Vector3(0.00006F, -0.48673F, 0.29783F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.04F, 0.07F, 0.04F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.00737F, 0.22085F, -0.31748F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MouthMuzzle",
                localPos = new Vector3(0.14629F, 1.60449F, 1.6085F),
                localAngles = new Vector3(329.4504F, 352.0506F, 87.1423F),
                localScale = new Vector3(0.3F, 0.4F, 0.3F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "CalfR",
                localPos = new Vector3(0.05998F, 0.31535F, 0.01488F),
                localAngles = new Vector3(351.817F, 0F, 3.87134F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "SMG",
                localPos = new Vector3(0F, -0.24108F, 0.35149F),
                localAngles = new Vector3(0F, 270F, 270F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.05826F, 0.08549F, 0.13422F),
                localAngles = new Vector3(357.4831F, 127.1501F, 44.08189F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(-0.04779F, 0.0747F, -0.11786F),
                localAngles = new Vector3(0F, 90F, 341.5865F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "LowerArmR",
                localPos = new Vector3(0.01877F, 0.31538F, 0.12764F),
                localAngles = new Vector3(0.1531F, 96.54499F, 358.6658F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.18668F, -0.27571F, 0.07331F),
                localAngles = new Vector3(0F, 9.25686F, 90F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(2.11933F, 1.76934F, -0.19593F),
                localAngles = new Vector3(0F, 0F, 16.6267F),
                localScale = new Vector3(0.4F, 0.4F, 0.4F)
            });

            return dict;
        }
        public static ItemDisplayRuleDict getVolatileThoriumBatteryDisplayRules(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.0003F, 0.31403F, -0.19567F),
                localAngles = new Vector3(17.77653F, 0F, 348.441F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.10591F, 0.00721F, -0.08287F),
                localAngles = new Vector3(295.1076F, 330.7766F, 250.9964F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.0003F, 0.31403F, -0.19567F),
                localAngles = new Vector3(17.77653F, 0F, 348.441F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.0665F, 0.31457F, -1.83914F),
                localAngles = new Vector3(2.76018F, 6.24079F, 259.2181F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.00089F, 0.32809F, -0.28591F),
                localAngles = new Vector3(17.77653F, 0F, 348.441F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.00315F, 0.03647F, -0.27353F),
                localAngles = new Vector3(5.33804F, 43.3816F, 359.3905F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.0005F, 0.19265F, -0.3163F),
                localAngles = new Vector3(282.4278F, 314.157F, 34.71479F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "PlatformBase",
                localPos = new Vector3(0.71088F, -0.11557F, -0.02951F),
                localAngles = new Vector3(351.2525F, 0.00001F, 348.441F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.00094F, 0.33829F, -0.29372F),
                localAngles = new Vector3(5.11593F, 354.7151F, 79.32722F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MouthMuzzle",
                localPos = new Vector3(-1.04575F, 1.51694F, 4.48379F),
                localAngles = new Vector3(52.53732F, 36.51127F, 78.18307F),
                localScale = new Vector3(0.06F, 0.06F, 0.06F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "HandR",
                localPos = new Vector3(-0.0903F, 0.0735F, 0.01947F),
                localAngles = new Vector3(56.5262F, 92.51429F, 79.59902F),
                localScale = new Vector3(0.01F, 0.01F, 0.01F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(0.00964F, 0.39852F, -0.00464F),
                localAngles = new Vector3(351.6817F, 43.07616F, 347.4608F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.01086F, 0.12196F, -0.23307F),
                localAngles = new Vector3(357.5437F, 4.05081F, 348.9897F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pack",
                localPos = new Vector3(0.12452F, 0.07747F, -0.22681F),
                localAngles = new Vector3(9.30676F, 351.35F, 214.1874F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(-0.32394F, 0.1027F, -0.11775F),
                localAngles = new Vector3(354.2223F, 2.37513F, 330.1555F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.00405F, 0.26996F, -0.25092F),
                localAngles = new Vector3(16.76947F, 353.3085F, 78.55984F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MuzzleEnergyCannon",
                localPos = new Vector3(-0.0707F, -6.03938F, -4.66791F),
                localAngles = new Vector3(77.10149F, 302.8474F, 291.7532F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            return dict;
        }

        public static ItemDisplayRuleDict getCharcoalDisplay(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.00373F, 0.07177F, -0.20732F),
                localAngles = new Vector3(346.4685F, 77.81429F, 281.7885F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)

            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Base",
                localPos = new Vector3(0F, -0.02431F, -0.11212F),
                localAngles = new Vector3(13.75313F, 77.45177F, 356.9711F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Base",
                localPos = new Vector3(0.18716F, 0.14392F, -0.04222F),
                localAngles = new Vector3(9.91285F, 12.37932F, 354.0078F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Base",
                localPos = new Vector3(0F, -0.40807F, -0.30323F),
                localAngles = new Vector3(359.0475F, 76.19363F, 351.6367F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Base",
                localPos = new Vector3(0.2381F, 0.33066F, -0.00009F),
                localAngles = new Vector3(6.31226F, 347.3579F, 358.5873F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "HandR",
                localPos = new Vector3(0.01083F, 0.12999F, 0.0558F),
                localAngles = new Vector3(346.7585F, 281.2592F, 359.1983F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(0.10843F, -0.06882F, 0.02314F),
                localAngles = new Vector3(57.70193F, 316.438F, 233.5271F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "FootBackR",
                localPos = new Vector3(-0.00364F, 1.03116F, -0.09518F),
                localAngles = new Vector3(285.2709F, 288.6932F, 34.97208F),
                localScale = new Vector3(0.06F, 0.06F, 0.06F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MechBase",
                localPos = new Vector3(0.25024F, -0.0622F, -0.08403F),
                localAngles = new Vector3(290.8557F, 320.5878F, 41.15931F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(1.35696F, 0.2963F, -0.1026F),
                localAngles = new Vector3(275.7284F, 210.535F, 129.8458F),
                localScale = new Vector3(0.2F, 0.2F, 0.2F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(0.09024F, 0.02426F, 0.11435F),
                localAngles = new Vector3(290.5736F, 248.093F, 57.74569F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(0.06908F, 0.10935F, 0.10065F),
                localAngles = new Vector3(65.3952F, 263.9827F, 257.3333F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "CannonEnd",
                localPos = new Vector3(0.27434F, -0.26033F, -0.0235F),
                localAngles = new Vector3(277.6904F, 356.5083F, 1.93786F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pack",
                localPos = new Vector3(-0.19383F, -0.20371F, -0.24618F),
                localAngles = new Vector3(38.88949F, 57.94184F, 281.4322F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(0.40678F, -0.00409F, 0.00534F),
                localAngles = new Vector3(0F, 0F, 0F),
                localScale = new Vector3(0.04F, 0.04F, 0.04F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.00249F, 0.08494F, 0.03774F),
                localAngles = new Vector3(14.39209F, 349.2895F, 4.82735F),
                localScale = new Vector3(0.05F, 0.05F, 0.05F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(-6.72266F, 11.81319F, 0.68433F),
                localAngles = new Vector3(309.5154F, 290.72F, 74.17768F),
                localScale = new Vector3(1F, 1F, 1F)
            });

            return dict;
        }
        #endregion

        #region Lunars
        public static ItemDisplayRuleDict getAbandonmentDisplay(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00001F, 0.26469F, -0.16114F),
                localAngles = new Vector3(347.67F, 63.85934F, 289.6846F),
                localScale = new Vector3(0.04F, 0.04F, 0.04F)

            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00095F, 0.20353F, -0.17454F),
                localAngles = new Vector3(347.7398F, 67.4677F, 279.735F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.00297F, 0.22447F, -0.034F),
                localAngles = new Vector3(0.23895F, 10.34276F, 352.0656F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.01416F, 2.90612F, 1.97212F),
                localAngles = new Vector3(85.19018F, 341.6479F, 339.1453F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "HeadCenter",
                localPos = new Vector3(0.00074F, 0.1543F, -0.11261F),
                localAngles = new Vector3(349.6766F, 347.2297F, 2.21201F),
                localScale = new Vector3(0.035F, 0.035F, 0.035F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00107F, 0.19506F, -0.11494F),
                localAngles = new Vector3(0F, 0F, 0F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00111F, 0.26171F, -0.00398F),
                localAngles = new Vector3(0F, 0F, 0F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Base",
                localPos = new Vector3(0F, 0.33389F, 0.65784F),
                localAngles = new Vector3(76.61118F, 186.0831F, 184.415F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00114F, 0.21744F, -0.07608F),
                localAngles = new Vector3(340.7266F, 359.8946F, 0.01715F),
                localScale = new Vector3(0.035F, 0.035F, 0.035F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.01628F, -0.07436F, 1.44452F),
                localAngles = new Vector3(43.50739F, 178.5069F, 179.8989F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00237F, 0.2516F, -0.01967F),
                localAngles = new Vector3(0F, 0F, 0F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00153F, 0.18376F, -0.1509F),
                localAngles = new Vector3(326.2359F, 359.8723F, 0.37437F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0045F, 0.04703F, -0.15494F),
                localAngles = new Vector3(326.9948F, 73.74958F, 287.102F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.00684F, 0.25763F, -0.02819F),
                localAngles = new Vector3(8.95983F, 356.3079F, 352.2377F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00281F, 0.28471F, -0.19224F),
                localAngles = new Vector3(306.3228F, 41.60133F, 311.5596F),
                localScale = new Vector3(0.06F, 0.06F, 0.06F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.86791F, -0.23262F, -0.08596F),
                localAngles = new Vector3(327.7285F, 2.01231F, 103.2689F),
                localScale = new Vector3(0.06F, 0.06F, 0.06F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(0.00003F, 3.89731F, 5.012F),
                localAngles = new Vector3(65.6312F, -0.00006F, -0.00009F),
                localScale = new Vector3(1F, 1F, 1F)
            });

            return dict;
        }
        #endregion

        #region Elite Equipment
        public static ItemDisplayRuleDict getNuclearEliteCrownDisplay(GameObject prefab)
        {
            var itemDisplay = prefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(prefab);
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0008F, 0.23066F, 0.03091F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.08F, 0.08F, 0.08F)
            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00089F, 0.20706F, -0.05809F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.07F, 0.07F, 0.07F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.00334F, 0.08563F, 0.033F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.07F, 0.07F, 0.07F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.00161F, 1.34329F, -0.3888F),
                localAngles = new Vector3(359.9325F, 89.86612F, 62.85089F),
                localScale = new Vector3(0.9F, 0.9F, 0.9F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "HeadCenter",
                localPos = new Vector3(-0.00105F, -0.00559F, 0.07459F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.08F, 0.08F, 0.08F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.00001F, 0.07265F, -0.05852F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.06F, 0.06F, 0.06F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00043F, 0.14566F, 0.03647F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.07F, 0.07F, 0.07F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "FlowerBase",
                localPos = new Vector3(0F, -0.44969F, 0F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.4F, 0.4F, 0.4F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00023F, 0.12061F, 0.03288F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.08F, 0.07F, 0.07F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.00321F, 1.32645F, 0.32662F),
                localAngles = new Vector3(359.9934F, 90.01684F, 104.5834F),
                localScale = new Vector3(0.8F, 0.7F, 0.7F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.00041F, 0.10511F, 0.04115F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.07F, 0.07F, 0.07F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00117F, 0.09612F, -0.0145F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.06F, 0.06F, 0.06F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.02559F, 0.05338F, -0.00558F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.08F, 0.08F, 0.08F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00318F, 0.13509F, 0.03636F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.07F, 0.07F, 0.07F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00266F, 0.20898F, -0.0033F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.13F, 0.13F, 0.13F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.63406F, 0.02827F, -0.00765F),
                localAngles = new Vector3(0F, 0F, 90F),
                localScale = new Vector3(0.13F, 0.13F, 0.13F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.00077F, 3.06681F, -2.76345F),
                localAngles = new Vector3(0F, 90F, 20F),
                localScale = new Vector3(2.5F, 2.5F, 2.5F)
            });

            dict.Add("AcidLarva", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "BodyBase",
                localPos = new Vector3(-0.0008F, 2.99113F, -1.58227F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(1F, 1F, 1F)
            });

            dict.Add("mdlBeetle", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.01203F, 0.07377F, 0.22649F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlBeetleGuard", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0046F, 0.49667F, 0.07938F),
                localAngles = new Vector3(0F, 90F, 90F),
                localScale = new Vector3(0.6F, 0.6F, 0.6F)
            });

            dict.Add("mdlBeetleQueen", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0008F, 1.87299F, 0.56063F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(1F, 1F, 1F)
            });

            dict.Add("mdlBell", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chain",
                localPos = new Vector3(-0.0008F, 0.73346F, 0.03091F),
                localAngles = new Vector3(0F, 328F, 175F),
                localScale = new Vector3(0.5F, 0.5F, 0.5F)
            });

            dict.Add("mdlBison", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0008F, 0.23066F, 0.03091F),
                localAngles = new Vector3(0F, 90F, 90F),
                localScale = new Vector3(0.28F, 0.28F, 0.28F)
            });

            dict.Add("mdlBrother", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0033F, 0.05808F, 0.01246F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlChild", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00096F, 0.31484F, 0.03037F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlClayBoss", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "PotLidTop",
                localPos = new Vector3(-0.0008F, -0.34597F, 1.08433F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.6F, 0.6F, 0.6F)
            });

            dict.Add("mdlClayBruiser", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00057F, 0.14207F, 0.10143F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.15F, 0.15F, 0.15F)
            });

            dict.Add("mdlClayGrenadier", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Torso",
                localPos = new Vector3(-0.00656F, 0.27082F, 0.00331F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlLemurian", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.03552F, 1.65214F, 0.05239F),
                localAngles = new Vector3(0F, 90F, 270F),
                localScale = new Vector3(0.7F, 0.7F, 0.7F)
            });

            dict.Add("mdlLemurianBruiser", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0008F, 0.94039F, -0.73158F),
                localAngles = new Vector3(0F, 90F, 90F),
                localScale = new Vector3(1F, 1F, 1F)
            });

            dict.Add("mdlMagmaWorm", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "LowerJaw",
                localPos = new Vector3(-0.0008F, 0.31495F, -0.35665F),
                localAngles = new Vector3(0F, 90F, 90F),
                localScale = new Vector3(0.5F, 0.5F, 0.5F)
            });

            dict.Add("mdlFlyingVermin", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Body",
                localPos = new Vector3(-0.0008F, 0.22448F, 0.12611F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.4F, 0.4F, 0.4F)
            });

            dict.Add("mdlGolem", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0008F, 0.61663F, 0.0638F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlGrandparent", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.0016F, -2.0141F, -3.71818F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(1.7F, 1.7F, 1.7F)
            });

            dict.Add("mdlGravekeeper", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00594F, 1.29776F, -0.33981F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(1F, 1F, 1F)
            });

            dict.Add("mdlGreaterWisp", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MaskBase",
                localPos = new Vector3(-0.02218F, 0.44836F, 0.36709F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlGup", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MainBody2",
                localPos = new Vector3(-0.0008F, 0.4507F, 0.74217F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.15F, 0.15F, 0.15F)
            });

            dict.Add("mdlHermitCrab", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Base",
                localPos = new Vector3(-0.31478F, 0.25992F, 0.38397F),
                localAngles = new Vector3(0F, 60F, 0F),
                localScale = new Vector3(0.2F, 0.2F, 0.2F)
            });

            dict.Add("mdlImp", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Neck",
                localPos = new Vector3(-0.0008F, -0.09351F, -0.04361F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.1F, 0.1F, 0.1F)
            });

            dict.Add("mdlImpBoss", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Neck",
                localPos = new Vector3(-0.00078F, -0.56499F, -0.49726F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.4F, 0.4F, 0.4F)
            });

            dict.Add("mdlJellyfish", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Hull2",
                localPos = new Vector3(0.00332F, 0.23992F, 0.03435F),
                localAngles = new Vector3(10.03494F, 2.61013F, 344.3175F),
                localScale = new Vector3(0.4F, 0.4F, 0.4F)
            });

            dict.Add("mdlMiniMushroom", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(0.35333F, -0.90836F, 0.03091F),
                localAngles = new Vector3(0F, 0F, 90F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlMinorConstruct", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "CapTop",
                localPos = new Vector3(-0.0008F, -0.10378F, 0.0383F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlNullifier", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Muzzle",
                localPos = new Vector3(-0.01051F, -0.05792F, 1.15683F),
                localAngles = new Vector3(359.8739F, 89.86872F, 28.00765F),
                localScale = new Vector3(0.5F, 0.5F, 0.5F)
            });

            dict.Add("mdlParent", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-43.98314F, 104.9638F, 0.01609F),
                localAngles = new Vector3(0F, 0F, 47F),
                localScale = new Vector3(25F, 25F, 25F)
            });

            dict.Add("mdlRoboBallBoss", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MainEyeMuzzle",
                localPos = new Vector3(0.00887F, 0.47605F, -0.59136F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.25F, 0.25F, 0.25F)
            });

            dict.Add("mdlRoboBallMini", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Muzzle",
                localPos = new Vector3(-0.0008F, 0.30555F, -0.75506F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlScorchling", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.44596F, 0.11552F, -0.00781F),
                localAngles = new Vector3(0F, 0F, 0F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlTitan", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00743F, 4.60858F, 0.27686F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.84F, 0.84F, 0.84F)
            });

            dict.Add("mdlVagrant", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Hull",
                localPos = new Vector3(-0.00035F, 0.7043F, 0.02798F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(0.4F, 0.4F, 0.4F)
            });

            dict.Add("mdlVermin", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.00089F, 0.25632F, -0.03832F),
                localAngles = new Vector3(0F, 90F, 225F),
                localScale = new Vector3(0.4F, 0.4F, 0.4F)
            });

            dict.Add("mdlVoidBarnacle", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.32926F, -0.0143F, 0.09147F),
                localAngles = new Vector3(90F, 0F, 0F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlVoidJailer", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.18832F, -0.00033F, -0.16833F),
                localAngles = new Vector3(272.0641F, 128.0368F, 321.8955F),
                localScale = new Vector3(0.3F, 0.3F, 0.3F)
            });

            dict.Add("mdlVoidMegaCrab", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "BodyBase",
                localPos = new Vector3(0.00831F, 2.73948F, 0.83338F),
                localAngles = new Vector3(0F, 90F, 0F),
                localScale = new Vector3(3F, 3F, 3F)
            });

            dict.Add("mdlVulture", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0543F, 0.15059F, 0.0999F),
                localAngles = new Vector3(0F, 90F, 270F),
                localScale = new Vector3(0.9F, 0.9F, 0.9F)
            });

            dict.Add("mdlWisp1Mouth", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Head",
                localPos = new Vector3(-0.0008F, 0.02437F, 0.39167F),
                localAngles = new Vector3(0F, 90F, 90F),
                localScale = new Vector3(0.25F, 0.25F, 0.25F)
            });

            return dict;
        }
        #endregion
    }
}
