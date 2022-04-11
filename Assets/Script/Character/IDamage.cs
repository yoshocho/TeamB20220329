using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーや敵などのダメージを受けることができる物が使う共通処理
/// </summary>
public interface IDamage
{
    /// <summary>
    /// ダメージを受ける関数
    /// </summary>
    /// <param name="damage">ダメージ数</param>
    public void GetDamage(int damage);
}
