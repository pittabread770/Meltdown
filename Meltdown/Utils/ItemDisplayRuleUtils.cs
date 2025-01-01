using R2API;
using RoR2;
using UnityEngine;

namespace Meltdown.Utils
{
    public static class ItemDisplayRuleUtils
    {
        public static ItemDisplayRuleDict getReactorVentsDisplay()
        {
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();
            var prefab = Meltdown.Assets.LoadAsset<GameObject>("ReactorVentsDisplay.prefab");

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

        public static ItemDisplayRuleDict getPlutoniumRoundsDisplay()
        {
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();
            var prefab = Meltdown.Assets.LoadAsset<GameObject>("PlutoniumRounds.prefab");

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

        public static ItemDisplayRuleDict getLeakyReactorCoolantDisplay()
        {
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();
            var prefab = Meltdown.Assets.LoadAsset<GameObject>("LeakyReactorCoolant.prefab");

            dict.Add("mdlCommandoDualies", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.2379F, 0.1521F, -0.12725F),
                localAngles = new Vector3(0.41721F, 92.15808F, 7.68711F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlHuntress", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighR",
                localPos = new Vector3(-0.11491F, 0.05884F, 0.04536F),
                localAngles = new Vector3(9.44587F, 247.6054F, 198.7871F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlBandit2", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighR",
                localPos = new Vector3(-0.04644F, 0.2649F, 0.08857F),
                localAngles = new Vector3(354.4018F, 300.3515F, 167.155F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlToolbot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-2.4908F, 1.73171F, 2.01823F),
                localAngles = new Vector3(1.18739F, 289.7088F, 0.66556F),
                localScale = new Vector3(0.17F, 0.17F, 0.17F)
            });

            dict.Add("mdlEngi", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(-0.28239F, -0.00004F, 0F),
                localAngles = new Vector3(357.5472F, 59.89194F, 177.4843F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlMage", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "ThighL",
                localPos = new Vector3(0.10158F, 0.14747F, 0.06473F),
                localAngles = new Vector3(14.98014F, 62.10303F, 175.2345F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlMerc", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.13754F, -0.01277F, -0.22211F),
                localAngles = new Vector3(339.3058F, 43.84799F, 337.533F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlTreebot", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "FootBackL",
                localPos = new Vector3(0.02007F, 0.32254F, -0.14207F),
                localAngles = new Vector3(2.72422F, 340.3991F, 183.0304F),
                localScale = new Vector3(0.03F, 0.03F, 0.03F)
            });

            dict.Add("mdlLoader", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "MechBase",
                localPos = new Vector3(-0.18336F, -0.32105F, -0.11368F),
                localAngles = new Vector3(4F, 34F, 350.535F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlCroco", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(-1.95816F, 1.86818F, 0.41544F),
                localAngles = new Vector3(359.5434F, 293.5136F, 189.8523F),
                localScale = new Vector3(0.2F, 0.2F, 0.2F)
            });

            dict.Add("mdlCaptain", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(0.12277F, -0.0393F, -0.1985F),
                localAngles = new Vector3(0.27959F, 321.5233F, 178.5688F),
                localScale = new Vector3(0.01408F, 0.01408F, 0.01408F)
            });

            dict.Add("mdlRailGunner", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(-0.14353F, -0.24136F, -0.16557F),
                localAngles = new Vector3(10.59585F, 14.22324F, 329.0103F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlVoidSurvivor", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(-0.03752F, 0.17329F, -0.20521F),
                localAngles = new Vector3(0.75566F, 11.0456F, 178.8087F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlSeeker", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(0.21488F, -0.03359F, -0.18707F),
                localAngles = new Vector3(7.56189F, 331.6999F, 12.53235F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlFalseSon", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Pelvis",
                localPos = new Vector3(-0.238F, -0.02635F, -0.26024F),
                localAngles = new Vector3(5.27841F, 46.10352F, 354.4648F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlChef", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Chest",
                localPos = new Vector3(-0.00232F, -0.24796F, -0.18982F),
                localAngles = new Vector3(70.30338F, 10.26087F, 90.63699F),
                localScale = new Vector3(0.02F, 0.02F, 0.02F)
            });

            dict.Add("mdlScav", new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                followerPrefab = prefab,
                childName = "Backpack",
                localPos = new Vector3(-9.55393F, 3.01947F, 0.20227F),
                localAngles = new Vector3(14.59521F, 93.13736F, 356.6356F),
                localScale = new Vector3(0.4F, 0.4F, 0.4F)
            });

            return dict;
        }

        public static ItemDisplayRuleDict getUraniumFuelRodDisplayRules()
        {
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();
            var prefab = Meltdown.Assets.LoadAsset<GameObject>("UraniumFuelRodsDisplay.prefab");

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

        public static ItemDisplayRuleDict getVolatileThoriumBatteryDisplayRules()
        {
            ItemDisplayRuleDict dict = new ItemDisplayRuleDict();
            var prefab = Meltdown.Assets.LoadAsset<GameObject>("VolatileThoriumBattery.prefab");

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
    }
}
