using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class GrumpyWoman : MonoBehaviour
{
    [Serializable]
    public struct Frame
    {
        public Material material;
        public Mesh mesh;
    }

    public Frame[] frames;
    [SerializeField] private int currentFrame;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    public bool isGrumpy = true;
    
    private void Start()
    {
        StartCoroutine(Animation());
    }

    public IEnumerator Animation()
    {
        // Velhinha sai de casa
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            float speed = isGrumpy ? 1.5f : 1f;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            SetScale(Vector3.Lerp(new Vector3(0.15f, 0.15f, 0.15f), new Vector3(1, 1, 1), (t-0.20f)*5/4));
            yield return null;
        }
        
        // Velhinha dá com a vassoura/bengala
        Quaternion initialRotation = meshRenderer.transform.rotation;
        float initialTime = Time.time;

        for (var i = 0; i < 2; i++)
        {
            for (float t = 0; t < 1; t += 4 * Time.deltaTime)
            {
                if (isGrumpy)
                    meshRenderer.transform.Rotate(0, 60f * Time.deltaTime, 0);
                SetFrame((int) ((Time.time - initialTime) * 12));
                yield return null;
            }

            for (float t = 0; t < 1; t += 2 * Time.deltaTime)
            {
                if (isGrumpy)
                    meshRenderer.transform.Rotate(0, -60f * Time.deltaTime, 0);
                SetFrame((int) ((Time.time - initialTime) * 12));
                yield return null;
            }

            for (float t = 0; t < 1; t += 4 * Time.deltaTime)
            {
                if (isGrumpy)
                    meshRenderer.transform.Rotate(0, 60f * Time.deltaTime, 0);
                SetFrame((int) ((Time.time - initialTime) * 12));
                yield return null;
            }
        }
        
        for (float t = 0; t < 1; t += 2 * Time.deltaTime)
        {
            if (isGrumpy)
                meshRenderer.transform.Rotate(0, 180f * 2f * Time.deltaTime, 0);
            yield return null;
        }

        meshRenderer.transform.rotation = initialRotation;
        if (isGrumpy) meshRenderer.transform.Rotate(0, 180f, 0);
        
        // Velhinha regressa a casa
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            float speed = -1.5f;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            SetScale(Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0.15f, 0.15f, 0.15f), (t-0.25f)*4/3));
            yield return null;
        }
        
        UnityEngine.Object.Destroy(this.gameObject);
    }

    private void SetScale(Vector3 scale)
    {
        if (meshRenderer != null)
            meshRenderer.transform.localScale = scale;
    }

    private void SetFrame(int frame)
    {
        if (frames.Length == 0)
            return;

        currentFrame = frame % frames.Length;

        if (meshFilter == null)
            return;

        meshFilter.mesh = frames[currentFrame].mesh;
        if (meshRenderer != null)
            meshRenderer.material = frames[currentFrame].material;
    }
    
}