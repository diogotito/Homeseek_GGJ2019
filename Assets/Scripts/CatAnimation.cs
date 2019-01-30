using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    [Serializable]
    public struct Frame
    {
        public Mesh mesh;
        public Material material;
    }

    public Frame[] frames;
    [SerializeField] private int currentFrame;

    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float accumulatedProgress;

    public void Update()
    {
        currentFrame = ((int) accumulatedProgress) % frames.Length;
        meshFilter.mesh = frames[currentFrame].mesh;
        meshRenderer.material = frames[currentFrame].material;
    }

    private void Start()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Progress(float howMuch)
    {
        accumulatedProgress += howMuch;
    }
}
