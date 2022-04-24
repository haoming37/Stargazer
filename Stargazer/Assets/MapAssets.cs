using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using System.Linq;

namespace Stargazer.Assets
{
    public static class MapAssets
    {
        static Dictionary<int, ShipStatus> Assets = new Dictionary<int, ShipStatus>();
        static Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
        static List<UnityEngine.Object> UncheckedSprites = new List<UnityEngine.Object>();
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

            UncheckedSprites = UnityEngine.Object.FindObjectsOfTypeIncludingAssets(Sprite.Il2CppType).ToList();
        }

        static public ShipStatus GetAsset(int mapId)
        {
            return Assets[mapId];
        }

        static public Sprite? GetSprite(string name)
        {
            if (Sprites.ContainsKey(name)) return Sprites[name];

            UnityEngine.Object? result = null;
            foreach(var obj in UncheckedSprites)
            {
                try
                {
                    if (obj.name != name) continue;
                    result = obj;
                    break;
                }catch(Exception e) { }
            }
            if (result == null) return null;

            UncheckedSprites.Remove(result);

            Sprite sprite = result.Cast<Sprite>();
            Sprites[sprite.name] = sprite;
            return sprite;
        }
    }
}
