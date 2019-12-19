using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasksCanvasController : MonoBehaviour
{
   public GameObject targetPanel;
   public Text textPanel;

   private void Awake()
   {
      textPanel.gameObject.SetActive(false);
   }

   public void EnableTargetPanel(bool enable)
   {
      targetPanel.SetActive(enable);
   }

   public void EnableTextPanel(bool enable)
   {
      textPanel.gameObject.SetActive(enable);
   }

   public void UpdateTextPanel(string text)
   {
      textPanel.text = text;
   }
   
   
}
