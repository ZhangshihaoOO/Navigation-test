using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scene
{
    public class SceneTest3 : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
    {
        public Transform target;
        public Transform ai;
        public Seeker aiSeeker;

        public AstarPath aStar;

        public Transform ui;
        public GameObject test;

        private const float ZoomSpeed = 200f; // 缩放速度
        private const float MinZoom = 128; // 最小缩放值
        private const float MaxZoom = 366; // 最大缩放值

        private const float DragSpeed = 400f; // 拖动速度
        private const float WorldWidth = 1280f / 2; // 世界宽度
        private const float WorldHeight = 731f / 2; // 世界高度

        private bool isDrag;

        // private void OnValidate()
        // {
        //     List<Vector2> pathPoints = new List<Vector2>();
        //     float step = 1f / MaxStep;
        //     foreach (var tmp in splines)
        //     {
        //         Vector2 beginPosition = tmp.transform.localPosition;
        //         for (int i = 0; i <= MaxStep; i++)
        //         {
        //             Vector3 worldPos = tmp.Interpolate(i * step);
        //             pathPoints.Add(new Vector2(worldPos.x, worldPos.y) + beginPosition);
        //         }
        //     }
        //
        //     // 设置多边形碰撞器的路径
        //     polygonCollider2D.pathCount = 1;
        //     polygonCollider2D.SetPath(0, pathPoints.ToArray());
        // }

        private void Awake()
        {
            //根据六边形地图初始化ui效果
            GridGraph gridGraph = aStar.data.gridGraph;
            foreach (var graphNode in gridGraph.nodes)
            {
                GameObject obj = Instantiate(test, ui);
                obj.transform.localPosition = (Vector3)graphNode.position;
                obj.name = graphNode.NodeIndex.ToString();
                obj.SetActive(false);
            }
        }

        private void Start()
        {
            //网格ui动画
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDrag = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Camera.main != null)
            {
                float deltaX = -eventData.delta.x * DragSpeed * Time.deltaTime;
                float deltaY = -eventData.delta.y * DragSpeed * Time.deltaTime;
                Vector3 newPosition = Camera.main.transform.localPosition + new Vector3(deltaX, deltaY, 0);
                newPosition = ClampCameraLocalPosition(newPosition);
                Camera.main.transform.localPosition = newPosition;
            }
        }

        private Vector3 ClampCameraLocalPosition(Vector3 targetPosition)
        {
            Camera cam = Camera.main;
            if (cam != null)
            {
                float camHeight = cam.orthographicSize;
                float camWidth = camHeight * cam.aspect;

                float minX = -WorldWidth + camWidth;
                float maxX = WorldWidth - camWidth;
                float minY = -WorldHeight + camHeight;
                float maxY = WorldHeight - camHeight;

                targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
            }

            return targetPosition;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isDrag)
            {
                isDrag = false;
                return;
            }

            foreach (Transform tmp in lastActiveUiArray)
            {
                tmp.gameObject.SetActive(false);
            }
            lastActiveUiArray.Clear();
            if (currentPath != null)
            {
                
            }

            
            if (Camera.main != null) target.localPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        }

        private uint lastEndNodeIndex = int.MaxValue;
        private readonly List<Transform> lastActiveUiArray = new();
        private Path currentPath;

        private void Update()
        {
            float scrollData = Input.GetAxis("Mouse ScrollWheel");
            var main = Camera.main;
            if (main)
            {
                if (scrollData != 0)
                {
                    var orthographicSize = main.orthographicSize;
                    orthographicSize -= scrollData * ZoomSpeed;
                    main.orthographicSize = orthographicSize;
                    main.orthographicSize = Mathf.Clamp(orthographicSize, MinZoom, MaxZoom);
                }
                // 获取鼠标在屏幕上的位置
                Vector3 mousePosition = Input.mousePosition;
                // 将鼠标位置转换为世界坐标
                Vector3 worldPosition = main.ScreenToWorldPoint(mousePosition);
                
                var graphNode = aStar.data.gridGraph.GetNearest(worldPosition).node;
                if (graphNode != null) 
                {
                    uint index = graphNode.NodeIndex;
                    if (index != lastEndNodeIndex)
                    {
                        lastEndNodeIndex = index;
                        foreach (Transform tmp in lastActiveUiArray)
                        {
                            tmp.gameObject.SetActive(false);
                        }
                        lastActiveUiArray.Clear();
                        aiSeeker.StartPath(ai.localPosition, worldPosition, path =>
                        {
                            currentPath = path;
                            foreach (var node in path.path)
                            {
                                Transform tmp = ui.Find(node.NodeIndex.ToString());
                                if (tmp)
                                {
                                    tmp.gameObject.SetActive(true);
                                    lastActiveUiArray.Add(tmp);
                                }
                            }
                        });
                    }
                }
            }
        }
    }
}