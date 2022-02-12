using UnityEngine;

namespace DefaultNamespace
{
    public class exp:MonoBehaviour
    {
        public void Awake()
        {
            Sprite atlas = MFLoadManager.Load<Sprite>("Assets/spritePath/...", "expSprite.png", InitAtlas);
        }

        public void InitAtlas(object atlasAsset)
        {
            Sprite atlas = atlasAsset as Sprite;
            //执行一些对atlas的初始化
        }

        public void Update()
        {
            //执行一些对atlas的操作
        }
    }
}