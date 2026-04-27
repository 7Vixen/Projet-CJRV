using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeMoney_HowToHealthSystem_1_Final {

    public class CameraFollow : MonoBehaviour {

        public static CameraFollow instance;

        private Camera myCamera;
        private Func<Vector3> GetCameraFollowPosition;
        private float zoomOrtho;
        private float cameraMoveSpeedMultiplier;

        public void Setup(float zoomOrtho, float cameraMoveSpeedMultiplier, Func<Vector3> GetCameraFollowPosition) {
            this.zoomOrtho = zoomOrtho;
            this.cameraMoveSpeedMultiplier = cameraMoveSpeedMultiplier;
            myCamera = transform.GetComponent<Camera>();
            SetGetCameraFollowPosition(GetCameraFollowPosition);
        }
        public void SetGetCameraFollowPosition(Func<Vector3> GetCameraFollowPosition) {
            this.GetCameraFollowPosition = GetCameraFollowPosition;
        }

        private void Update() {
            HandleZoomButtons();
            HandleCameraMove();
            HandleCameraZoom();
        }

        private void HandleCameraMove() {
            Vector3 cameraFollowPos = GetCameraFollowPosition();

            cameraFollowPos.z = transform.position.z;
            Vector3 cameraMoveDir = (cameraFollowPos - transform.position).normalized;
            float dist = Vector3.Distance(cameraFollowPos, transform.position);
            float cameraMoveSpeed = dist;
            cameraMoveSpeed *= cameraMoveSpeedMultiplier;

            if (dist > 0f) {
                Vector3 mainCameraNewPos = transform.position + (cameraMoveDir * cameraMoveSpeed) * Time.deltaTime;

                // Test Overshoot
                float distAfter = Vector3.Distance(cameraFollowPos, transform.position);
                if (distAfter > dist) {
                    // Overshot
                    transform.position = cameraFollowPos;
                }

                transform.position = mainCameraNewPos;
            }
        }

        private void HandleCameraZoom() {
            float orthoMove = (zoomOrtho - myCamera.orthographicSize) * 10f * Time.deltaTime;
            myCamera.orthographicSize += orthoMove;
            if (orthoMove > 0) {
                // Positive movement
                if (myCamera.orthographicSize > zoomOrtho) {
                    // Overshot
                    myCamera.orthographicSize = zoomOrtho;
                }
            }
            if (orthoMove < 0) {
                // Negative movement
                if (myCamera.orthographicSize < zoomOrtho) {
                    // Overshot
                    myCamera.orthographicSize = zoomOrtho;
                }
            }
        }

        private void HandleZoomButtons() {
            if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
                ZoomOut();
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
                ZoomIn();
            }
        }

        private void ZoomIn() {
            zoomOrtho -= 40f;
            if (zoomOrtho < 60f) zoomOrtho = 60f;
        }
        private void ZoomOut() {
            zoomOrtho += 40f;
            if (zoomOrtho > 400f) zoomOrtho = 400f;
        }
    }

}