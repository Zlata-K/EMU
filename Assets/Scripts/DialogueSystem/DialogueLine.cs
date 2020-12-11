﻿using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DialogueSystem
{
    using TMPro;
    
    public class DialogueLine : Dialogue
    {
        private TextMeshProUGUI textHolder;
        private Image imageHolder;
        [Header ("Text")]
        [SerializeField] private string input;
        [SerializeField] private float delay;
        [Header ("Image")]
        [SerializeField] private Sprite sprite;
        
        private void Awake()
        {
            imageHolder = gameObject.transform.parent.parent.GetChild(0).GetComponent<Image>();
            textHolder = GetComponent<TextMeshProUGUI>();
            textHolder.text = "";
            
            gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            imageHolder.sprite = sprite;
            imageHolder.preserveAspect = true;
            Debug.Log(input);
            StartCoroutine(WriteText(input, textHolder, delay));
        }
    }

}