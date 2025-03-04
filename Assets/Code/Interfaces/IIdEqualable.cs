using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIdEqualable
{
    public int Id { get; }
    public bool IdEquals(int id);
}
