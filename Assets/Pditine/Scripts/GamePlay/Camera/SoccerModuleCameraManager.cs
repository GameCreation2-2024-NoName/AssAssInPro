// using System.Collections;
// using Pditine.GamePlay.UI;
// using UnityEngine;
//
// namespace Pditine.GamePlay.Camera
// {
//     public class SoccerModuleCameraManager : CameraManagerBase
//     {
//         private float _cameraTargetSize;
//         [SerializeField] private float originCameraSize = 250;
//         [SerializeField] [Range(0, 1)] private float speed;
//         public void Start()
//         {
//             UIManager.Instance.HideUI();
//             _cameraTargetSize = mainCamera.m_Lens.OrthographicSize;
//             mainCamera.m_Lens.OrthographicSize = originCameraSize;
//             StartCoroutine(DoZoomCamera());
//         }
//
//         private IEnumerator DoZoomCamera()
//         {
//             while (Mathf.Abs(mainCamera.m_Lens.OrthographicSize-_cameraTargetSize)>0.01f)
//             {
//                 mainCamera.m_Lens.OrthographicSize =
//                     Mathf.Lerp(mainCamera.m_Lens.OrthographicSize, _cameraTargetSize, speed);
//                 yield return new WaitForSecondsRealtime(0.01f);
//             }
//             mainCamera.m_Lens.OrthographicSize = _cameraTargetSize;
//             UIManager.Instance.ShowUI();
//         }
//         public override void OnCollidePLayerAss(int id)
//         {
//             base.OnCollidePLayerAss(id);
//         }
//     }
// }