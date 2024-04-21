using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorPallet", menuName = "ColorPallet")]
public class ColorPallet : ScriptableObject
{
   public Color titleColor; 
   public Color backgroundColor;
   public Color waterColorColor;
   public Color rippleColor;
   public Color emitterColor; 
}
