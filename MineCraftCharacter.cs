using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MineCraftCharacter : MonoBehaviour {

    public MeshFilter head;
    public MeshFilter body;
    public MeshFilter rleg;
    public MeshFilter rarm;
    public MeshFilter lleg;
    public MeshFilter larm;

    public float Size = 1.0f;


    private void Start()
    {
        //Body
        body.transform.localPosition = Vector3.zero;
        //Head  
        head.transform.position = Vector3.zero + new Vector3(0f, (Size/16f) + (Size/64f), 0f);
        //Rleg              
        rleg.transform.position = Vector3.zero + new Vector3(Size/64f, -(Size / 16f) - ((Size / 64f) * 2), 0f);
        //Rarm                              
        rarm.transform.position = Vector3.zero + new Vector3((Size/32f) + (Size / 64f), 0f, 0f);
        //Lleg                   
        lleg.transform.position = Vector3.zero + new Vector3(-(Size / 64f), -(Size / 16f) - ((Size / 64f) * 2), 0f);
        //Larm                          
        larm.transform.position = Vector3.zero + new Vector3(-(Size / 32f) - (Size / 64f), 0f, 0f);

        head.mesh = Head(head.transform.position, Size);
        body.mesh = Body(body.transform.position, Size);
        rleg.mesh = RLeg(rleg.transform.position, Size);
        rarm.mesh = RArm(rarm.transform.position, Size);
        lleg.mesh = LLeg(lleg.transform.position, Size);
        larm.mesh = LArm(larm.transform.position, Size);

    }

 
    Vector3[] Cube(Vector3 position, float x, float y, float z)
    {
        List<Vector3> vertices = new List<Vector3>();

        // Front = v0 v1 v2 v3
        vertices.Add(new Vector3(position.x - (x / 2f), position.y - (y / 2f), position.z - (z / 2f)));
        vertices.Add(new Vector3(position.x - (x / 2f), position.y + (y / 2f), position.z - (z / 2f)));
        vertices.Add(new Vector3(position.x + (x / 2f), position.y + (y / 2f), position.z - (z / 2f)));
        vertices.Add(new Vector3(position.x + (x / 2f), position.y - (y / 2f), position.z - (z / 2f)));
        // Top = v4 v5 v6 v7                                                                      
        vertices.Add(new Vector3(position.x - (x / 2f), position.y + (y / 2f), position.z - (z / 2f)));
        vertices.Add(new Vector3(position.x - (x / 2f), position.y + (y / 2f), position.z + (z / 2f)));
        vertices.Add(new Vector3(position.x + (x / 2f), position.y + (y / 2f), position.z + (z / 2f)));
        vertices.Add(new Vector3(position.x + (x / 2f), position.y + (y / 2f), position.z - (z / 2f)));
        // Bottom = v8 v9 v10 v11                                                                 
        vertices.Add(new Vector3(position.x - (x / 2f), position.y - (y / 2f), position.z - (z / 2f)));
        vertices.Add(new Vector3(position.x - (x / 2f), position.y - (y / 2f), position.z + (z / 2f)));
        vertices.Add(new Vector3(position.x + (x / 2f), position.y - (y / 2f), position.z + (z / 2f)));
        vertices.Add(new Vector3(position.x + (x / 2f), position.y - (y / 2f), position.z - (z / 2f)));
        // Left = v12 v13 v14 v15                                                                 
        vertices.Add(new Vector3(position.x - (x / 2f), position.y - (y / 2f), position.z + (z / 2f)));
        vertices.Add(new Vector3(position.x - (x / 2f), position.y + (y / 2f), position.z + (z / 2f)));
        vertices.Add(new Vector3(position.x - (x / 2f), position.y + (y / 2f), position.z - (z / 2f)));
        vertices.Add(new Vector3(position.x - (x / 2f), position.y - (y / 2f), position.z - (z / 2f)));
        // Right = v16 v17 v18 v19                                                                
        vertices.Add(new Vector3(position.x + (x / 2f), position.y - (y / 2f), position.z - (z / 2f)));
        vertices.Add(new Vector3(position.x + (x / 2f), position.y + (y / 2f), position.z - (z / 2f)));
        vertices.Add(new Vector3(position.x + (x / 2f), position.y + (y / 2f), position.z + (z / 2f)));
        vertices.Add(new Vector3(position.x + (x / 2f), position.y - (y / 2f), position.z + (z / 2f)));
        // Back = v20 v21 v22 v23                                                                 
        vertices.Add(new Vector3(position.x + (x / 2f), position.y - (y / 2f), position.z + (z / 2f)));
        vertices.Add(new Vector3(position.x + (x / 2f), position.y + (y / 2f), position.z + (z / 2f)));
        vertices.Add(new Vector3(position.x - (x / 2f), position.y + (y / 2f), position.z + (z / 2f)));
        vertices.Add(new Vector3(position.x - (x / 2f), position.y - (y / 2f), position.z + (z / 2f)));

        return vertices.ToArray();
    }
    int[] Triangle(Vector3[] vertices)
    {
        List<int> triangle = new List<int>() {
            //front
            0, 1, 2,
            2, 3, 0,
            //top
            4, 5, 6,
            6, 7, 4,
            //bottom
            10, 9, 8,
            8, 11, 10,
            //left
            12, 13, 14,
            14, 15, 12,
            //right
            16, 17, 18,
            18, 19, 16,
            //back
            20, 21, 22,
            22, 23, 20
        };
        return triangle.ToArray();
    }

    enum UV_ANCHOR { Head = 0,RLeg, Body, RArm, LLeg, LArm }
    Vector2[] UV(UV_ANCHOR anchor)
    {
        List<Vector2> uv = new List<Vector2>();

        switch (anchor) {
            case UV_ANCHOR.Head:
                // Front
                uv.Add(new Vector2((1f / 8f), 1f - (1f / 8f) * 2f ));
                uv.Add(new Vector2((1f / 8f), 1f - (1f / 8f) ));
                uv.Add(new Vector2((1f / 8f) * 2f, 1f - (1f / 8f) ));
                uv.Add(new Vector2((1f / 8f) * 2f, 1f - (1f / 8f) * 2f ));
                // Top
                uv.Add(new Vector2((1f / 8f), 1f - (1f / 8f) ));
                uv.Add(new Vector2((1f / 8f), 1f ));
                uv.Add(new Vector2((1f / 8f) * 2, 1f ));
                uv.Add(new Vector2((1f / 8f) * 2, 1f - (1f / 8f) ));
                // Bottom
                uv.Add(new Vector2((1f / 8f) * 2f, 1f - (1f / 8f)));
                uv.Add(new Vector2((1f / 8f) * 2f, 1f));
                uv.Add(new Vector2((1f / 8f) * 3f, 1f));
                uv.Add(new Vector2((1f / 8f) * 3f, 1f - (1f / 8f)));
                // Left
                uv.Add(new Vector2((1f / 8f) * 2f, 1f - (1f / 8f) * 2f));
                uv.Add(new Vector2((1f / 8f) * 2f, 1f - (1f / 8f)));
                uv.Add(new Vector2((1f / 8f) * 3f, 1f - (1f / 8f)));
                uv.Add(new Vector2((1f / 8f) * 3f, 1f - (1f / 8f) * 2f));
                // Right
                uv.Add(new Vector2(0f, 1f - (1f / 8f) * 2f));
                uv.Add(new Vector2(0f, 1f - (1f / 8f)));
                uv.Add(new Vector2((1f / 8f), 1f - (1f / 8f)));
                uv.Add(new Vector2((1f / 8f), 1f - (1f / 8f) * 2f));
                // Back
                uv.Add(new Vector2((1f / 8f) * 3f, 1f - (1f / 8f) * 2f));
                uv.Add(new Vector2((1f / 8f) * 3f, 1f - (1f / 8f)));
                uv.Add(new Vector2((1f / 8f) * 4f, 1f - (1f / 8f)));
                uv.Add(new Vector2((1f / 8f) * 4f, 1f - (1f / 8f) * 2f));
                break;

            case UV_ANCHOR.RLeg:
                // Front
                uv.Add(new Vector2((1f / 16f), 0.5f));
                uv.Add(new Vector2((1f / 16f), 0.5f + (1f / 8f) + (1f / 16f) ));
                uv.Add(new Vector2((1f / 8f), 0.5f + (1f / 8f) + (1f / 16f) ));
                uv.Add(new Vector2((1f / 8f), 0.5f ));
                // Top
                uv.Add(new Vector2((1f / 16f), 1f - (1f / 4f) - (1f / 16f) ));
                uv.Add(new Vector2((1f / 16f), 1f - (1f / 4f) ));
                uv.Add(new Vector2((1f / 8f), 1f - (1f / 4f) ));
                uv.Add(new Vector2((1f / 8f), 1f - (1f / 4f) - (1f / 16f)));
                // Bottom
                uv.Add(new Vector2((1f / 8f), 1f - (1f / 4f) - (1f / 16f)));
                uv.Add(new Vector2((1f / 8f), 1f - (1f / 4f)));
                uv.Add(new Vector2((1f / 8f) + (1f / 16f), 1f - (1f / 4f)));
                uv.Add(new Vector2((1f / 8f) + (1f / 16f), 1f - (1f / 4f) - (1f / 16f)));
                // Left
                uv.Add(new Vector2((1f / 8f), 0.5f));
                uv.Add(new Vector2((1f / 8f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 8f) + (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 8f) + (1f / 16f), 0.5f));
                // Right
                uv.Add(new Vector2(0f, 0.5f));
                uv.Add(new Vector2(0f, 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 16f), 0.5f));
                // Back
                uv.Add(new Vector2((1f / 8f) + (1f / 16f), 0.5f));
                uv.Add(new Vector2((1f / 8f) + (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f), 0.5f));
                break;

            case UV_ANCHOR.Body:
                // Front
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), 0.5f));
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f - (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f - (1f / 16f), 0.5f));
                // Top
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), 1f - (1f / 4f) - (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), 1f - (1f / 4f)));
                uv.Add(new Vector2(0.5f - (1f / 16f), 1f - (1f / 4f)));
                uv.Add(new Vector2(0.5f - (1f / 16f), 1f - (1f / 4f) - (1f / 16f)));
                // Bottom
                uv.Add(new Vector2(0.5f - (1f / 16f), 1f - (1f / 4f) - (1f / 16f)));
                uv.Add(new Vector2(0.5f - (1f / 16f), 1f - (1f / 4f)));
                uv.Add(new Vector2(0.5f + (1f / 16f), 1f - (1f / 4f)));
                uv.Add(new Vector2(0.5f + (1f / 16f), 1f - (1f / 4f) - (1f / 16f)));
                // Left
                uv.Add(new Vector2(0.5f + (1f / 16f), 0.5f));
                uv.Add(new Vector2(0.5f + (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 8f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 8f), 0.5f));
                // Right
                uv.Add(new Vector2((1f / 4f), 0.5f));
                uv.Add(new Vector2((1f / 4f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), 0.5f));
                // Back
                uv.Add(new Vector2(0.5f - (1f / 16f), 0.5f));
                uv.Add(new Vector2(0.5f - (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 16f), 0.5f));
                break;

            case UV_ANCHOR.RArm:
                // Front
                uv.Add(new Vector2(0.5f + (1f / 8f) + (1f / 16f), 0.5f));
                uv.Add(new Vector2(0.5f + (1f / 8f) + (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 4f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 4f), 0.5f));
                // Top
                uv.Add(new Vector2(0.5f + (1f / 8f) + (1f / 16f), 1f - (1f / 4f) - (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 8f) + (1f / 16f), 1f - (1f / 4f)));
                uv.Add(new Vector2(0.5f + (1f / 4f), 1f - (1f / 4f)));
                uv.Add(new Vector2(0.5f + (1f / 4f), 1f - (1f / 4f) - (1f / 16f)));
                // Bottom
                uv.Add(new Vector2(0.5f + (1f / 4f), 1f - (1f / 4f) - (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 4f), 1f - (1f / 4f)));
                uv.Add(new Vector2(0.5f + (1f / 4f) + (1f / 16f), 1f - (1f / 4f)));
                uv.Add(new Vector2(0.5f + (1f / 4f) + (1f / 16f), 1f - (1f / 4f) - (1f / 16f)));
                // Left
                uv.Add(new Vector2(0.5f + (1f / 4f), 0.5f));
                uv.Add(new Vector2(0.5f + (1f / 4f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 4f) + (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 4f) + (1f / 16f), 0.5f));
                // Right
                uv.Add(new Vector2(0.5f + (1f / 8f), 0.5f));
                uv.Add(new Vector2(0.5f + (1f / 8f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 8f) + (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f + (1f / 8f) + (1f / 16f), 0.5f));
                // Back
                uv.Add(new Vector2(0.5f + (1f / 4f) + (1f / 16f), 0.5f));
                uv.Add(new Vector2(0.5f + (1f / 4f) + (1f / 16f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(1f - (1f / 8f), 0.5f + (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(1f - (1f / 8f), 0.5f));
                
                break;

            case UV_ANCHOR.LLeg:
                // Front
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), 0f));
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 8f) * 3f, (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 8f) * 3f, 0f));
                // Top
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), (1f / 4f) - (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), (1f / 4f)));
                uv.Add(new Vector2((1f / 8f) * 3f, (1f / 4f)));
                uv.Add(new Vector2((1f / 8f) * 3f, (1f / 4f) - (1f / 16f)));
                // Bottom
                uv.Add(new Vector2((1f / 8f) * 3f, (1f / 4f) - (1f / 16f)));
                uv.Add(new Vector2((1f / 8f) * 3f, (1f / 4f)));
                uv.Add(new Vector2(0.5f - (1f / 16f), (1f / 4f)));
                uv.Add(new Vector2(0.5f - (1f / 16f), (1f / 4f) - (1f / 16f)));
                // Left
                uv.Add(new Vector2((1f / 8f) * 3f, 0f));
                uv.Add(new Vector2((1f / 8f) * 3f, (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f - (1f / 16f), (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f - (1f / 16f), 0f));
                // Right
                uv.Add(new Vector2((1f / 4f), 0f));
                uv.Add(new Vector2((1f / 4f), (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 16f), 0f));
                // Back
                uv.Add(new Vector2(0.5f - (1f / 16f), 0f));
                uv.Add(new Vector2(0.5f - (1f / 16f), (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f, (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2(0.5f, 0f));
                break;

            case UV_ANCHOR.LArm:
                // Front
                uv.Add(new Vector2((1f / 2f) + (1f / 16f), 0f));
                uv.Add(new Vector2((1f / 2f) + (1f / 16f), (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 8f) * 3f, (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 8f) * 3f, 0f));
                // Top                         
                uv.Add(new Vector2((1f / 2f) + (1f / 16f), (1f / 4f) - (1f / 16f)));
                uv.Add(new Vector2((1f / 2f) + (1f / 16f), (1f / 4f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 8f) * 3f, (1f / 4f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 8f) * 3f, (1f / 4f) - (1f / 16f)));
                // Bottom                      
                uv.Add(new Vector2((1f / 4f) + (1f / 8f) * 3f, (1f / 4f) - (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + (1f / 8f) * 3f, (1f / 4f)));
                uv.Add(new Vector2((1f / 4f) + 0.5f - (1f / 16f), (1f / 4f)));
                uv.Add(new Vector2((1f / 4f) + 0.5f - (1f / 16f), (1f / 4f) - (1f / 16f)));
                // Left                        
                uv.Add(new Vector2((1f / 4f) + (1f / 8f) * 3f, 0f));
                uv.Add(new Vector2((1f / 4f) + (1f / 8f) * 3f, (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + 0.5f - (1f / 16f), (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + 0.5f - (1f / 16f), 0f));
                // Right                       
                uv.Add(new Vector2((1f / 2f), 0f));
                uv.Add(new Vector2((1f / 2f), (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 2f) + (1f / 16f), (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 2f) + (1f / 16f), 0f));
                // Back                        
                uv.Add(new Vector2((1f / 4f) + 0.5f - (1f / 16f), 0f));
                uv.Add(new Vector2((1f / 4f) + 0.5f - (1f / 16f), (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + 0.5f, (1f / 8f) + (1f / 16f)));
                uv.Add(new Vector2((1f / 4f) + 0.5f, 0f));
                break;

        };

        return uv.ToArray();
    }

    Mesh Head(Vector3 pos, float size)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = Cube(pos, size/8f, size/8f, size/8f);
        int[] triangles = Triangle(vertices);
        Vector2[] uv = UV(UV_ANCHOR.Head);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        return mesh;
    }
    Mesh Body(Vector3 pos, float size)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = Cube(pos, size / 8f, size / 8f + size / 16f, size / 16f);
        int[] triangles = Triangle(vertices);
        Vector2[] uv = UV(UV_ANCHOR.Body);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        return mesh;
    }
    Mesh RLeg(Vector3 pos, float size)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = Cube(pos, size / 16f, size / 8f + size / 16f, size / 16f);
        int[] triangles = Triangle(vertices);
        Vector2[] uv = UV(UV_ANCHOR.RLeg);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        return mesh;
    }
    Mesh RArm(Vector3 pos, float size)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = Cube(pos, size / 16f, size / 8f + size / 16f, size / 16f);
        int[] triangles = Triangle(vertices);
        Vector2[] uv = UV(UV_ANCHOR.RArm);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        return mesh;
    }
    Mesh LLeg(Vector3 pos, float size)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = Cube(pos, size / 16f, size / 8f + size / 16f, size / 16f);
        int[] triangles = Triangle(vertices);
        Vector2[] uv = UV(UV_ANCHOR.LLeg);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        return mesh;
    }
    Mesh LArm(Vector3 pos, float size)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = Cube(pos, size / 16f, size / 8f + size / 16f, size / 16f);
        int[] triangles = Triangle(vertices);
        Vector2[] uv = UV(UV_ANCHOR.LArm);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        return mesh;
    }
}
