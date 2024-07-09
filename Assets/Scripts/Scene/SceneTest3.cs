using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scene
{
    public class SceneTest3 : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
    {
        public Transform target;
        public Transform ai;

        [Header("Curvy Test")] public PolygonCollider2D polygonCollider2D;
        public CurvySpline[] splines;

        private const float ZoomSpeed = 200f; // 缩放速度
        private const float MinZoom = 128; // 最小缩放值
        private const float MaxZoom = 512; // 最大缩放值

        private const float DragSpeed = 400f; // 拖动速度
        private const float WorldWidth = 1280f / 2; // 世界宽度
        private const float WorldHeight = 731f / 2; // 世界高度

        private const int MaxStep = 240;

        private bool isDrag;

        private void OnValidate()
        {
            List<Vector2> pathPoints = new List<Vector2>();
            float step = 1f / MaxStep;
            foreach (var tmp in splines)
            {
                Vector2 beginPosition = tmp.transform.localPosition;
                for (int i = 0; i <= MaxStep; i++)
                {
                    Vector3 worldPos = tmp.Interpolate(i * step);
                    pathPoints.Add(new Vector2(worldPos.x, worldPos.y) + beginPosition);
                }
            }

            // 设置多边形碰撞器的路径
            polygonCollider2D.pathCount = 1;
            polygonCollider2D.SetPath(0, pathPoints.ToArray());
        }

        private void Awake()
        {
            target.localPosition = ai.localPosition;
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

            if (Camera.main != null) target.localPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        }

        private void Update()
        {
            float scrollData = Input.GetAxis("Mouse ScrollWheel");
            var main = Camera.main;
            if (scrollData != 0 && main)
            {
                var orthographicSize = main.orthographicSize;
                orthographicSize -= scrollData * ZoomSpeed;
                main.orthographicSize = orthographicSize;
                main.orthographicSize = Mathf.Clamp(orthographicSize, MinZoom, MaxZoom);
            }
        }
    }
}