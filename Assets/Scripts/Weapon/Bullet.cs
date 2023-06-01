using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    
    [SerializeField]
    protected SO_BulletData _bulletData;
    public virtual SO_BulletData BulletData
    {
        get { return _bulletData; }
        set { _bulletData = value; }
    }
    
    //[field: SerializeField]
    //public virtual SO_BulletData BulletData { get; set; } // virtual so that we can no modify how our bullet data behaves
}
