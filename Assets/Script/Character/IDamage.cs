using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[��G�Ȃǂ̃_���[�W���󂯂邱�Ƃ��ł��镨���g�����ʏ���
/// </summary>
public interface IDamage
{
    /// <summary>
    /// �_���[�W���󂯂�֐�
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    public void GetDamage(int damage);
}
