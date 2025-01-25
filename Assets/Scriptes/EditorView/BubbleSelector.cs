using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BubbleSelector : MonoBehaviour, IPointerClickHandler{
    public GameObject prefab;
    public TextMeshProUGUI textMeshPro;

    public void OnPointerClick(PointerEventData eventData){
        Cursor.Instance.SetPrefab(prefab);
    }

    void OnValidate(){
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshPro != null && prefab != null){
            textMeshPro.text = prefab.GetComponent<Bubble>().prix + "L";
        }
    }
}