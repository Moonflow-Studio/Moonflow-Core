using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 封装效果逻辑
/// </summary>
public interface IMFEffectBlock
{
   /// <summary>
   /// 设置Effect生效状态
   /// </summary>
   /// <param name="enable">设置状态</param>
   public void SetEnable(bool enable);
   /// <summary>
   /// 获取Effect生效状态
   /// </summary>
   /// <returns>返回生效状态</returns>
   public bool GetEnable();
}
