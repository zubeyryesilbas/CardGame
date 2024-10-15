using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
   public abstract void Show(int effectValue);
   public abstract void Hide();
}
