using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

namespace Stargazer.Assets
{
    public static class MapAssets
    {
        static Dictionary<int, ShipStatus> Assets = new Dictionary<int, ShipStatus>();

        public static void LoadAssets(AmongUsClient __instance)
        {
            int[] mapIdArray = new int[] { 0, 1, 2,4};

            foreach (var id in mapIdArray)
            {
                AssetReference assetReference = __instance.ShipPrefabs.ToArray()[id];
                
                AsyncOperationHandle<GameObject> asset = assetReference.LoadAsset<GameObject>();
                asset.WaitForCompletion();
                Assets[id] = assetReference.Asset.Cast<GameObject>().GetComponent<ShipStatus>();
            }
        }

        static public ShipStatus GetAsset(int mapId)
        {
            return Assets[mapId];
        }
    }
}
