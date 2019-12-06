using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasksCanvasController : MonoBehaviour
{
   public GameObject targetPanel;

   public void EnableTargetPanel(bool enable)
   {
      targetPanel.SetActive(enable);
   }
}
