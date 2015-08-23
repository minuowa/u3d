using UnityEngine;
using System.Collections;

public interface IMove
{
    bool IsMoving();
    void EndMove();
    bool Done();
}
