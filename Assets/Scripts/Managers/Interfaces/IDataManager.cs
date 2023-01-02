using System;
using System.Collections.Generic;
using TandC.FlyBall.Common;
using UnityEngine;

namespace TandC.FlyBall
{
    public interface IDataManager
    {
        UserLocalData CachedUserLocalData { get; set; }
        void StartLoadCache();
        void SaveAllCache();
        void SaveCache(Enumerators.CacheDataType type);
        Sprite GetSpriteFromTexture(Texture2D texture);
        event Action EndLoadCache;
    }
}