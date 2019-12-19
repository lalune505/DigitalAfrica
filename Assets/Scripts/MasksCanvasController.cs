using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasksCanvasController : MonoBehaviour
{
   public GameObject targetPanel;
   public GameObject textPanel;

   public void EnableTargetPanel(bool enable)
   {
      targetPanel.SetActive(enable);
   }

   public void EnableTextPanel(bool enable)
   {
      textPanel.SetActive(enable);
   }
   
   
}
