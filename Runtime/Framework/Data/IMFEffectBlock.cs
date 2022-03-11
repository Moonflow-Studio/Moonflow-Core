using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 封装效果逻辑
/// </summary>
public interface IMFEffectBlock
{
   public void SetEnable(bool enable);
   public bool GetEnable();
}
