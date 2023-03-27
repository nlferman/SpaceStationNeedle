using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Cinemachine;

using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace SSN
{
    public class CameraManager : MonoBehaviour
    {
		private static CameraManager _instance;
		public static CameraManager Instance
		{
			get
			{
				if(_instance == null)
					_instance = FindFirstObjectByType<CameraManager>();

				return _instance;
			}
		}
		[field: SerializeField] public CinemachineBrain CMBrain { get; private set; }
		[field: Header("Game Cameras")]
		[field: SerializeField] public Camera MainCamera { get; private set; }
		[field: SerializeField] public List<Camera> OverlayCameras { get; private set; } = new();

		[field: Header("Cinemachine Cameras")]
		[field: SerializeField] public CinemachineVirtualCamera CurrentCamera { get; private set; }
		[field: SerializeField] public CinemachineVirtualCamera PlayerCamera { get; private set; }
		[field: SerializeField] public CinemachineVirtualCamera DroneCamera { get; private set; }

		[SerializeField] private List<CinemachineVirtualCamera> CMCameras = new();

		[field: Header("Camera Manager Properties")]
		[field: SerializeField] public bool IsCameraLocked { get; private set; }
		private List<CinemachineVirtualCamera> UnactiveVMCameras = new();
		#region Starting Lifecycle Functions

		private void Awake()
		{
			if(Instance != null && Instance != this)
			{
				Destroy(this);
				return;
			}

			CMBrain = GetComponent<CinemachineBrain>();
			MainCamera = GetComponent<Camera>();
		}

		private void Start()
		{
			FindAllActiveCMCamera();
		}
		private void AddOverlayCameras()
		{
			if(OverlayCameras == null) return;
		}
		private void FindAllActiveCMCamera()
		{
			if(CinemachineCore.Instance.VirtualCameraCount <= 0)
				Debug.LogError("There were no cinemachines found inside of the scene.");

			for(int x = 0; x < CinemachineCore.Instance.VirtualCameraCount; x++)
			{
				CMCameras.Add(CinemachineCore.Instance.GetVirtualCamera(x) as CinemachineVirtualCamera);
			}
		}
		#endregion
		public void AddCMCamera(CinemachineVirtualCamera newCMCamera)
		{
			CMCameras.Add(newCMCamera);
		}
		public void ChangeActiveCMCamera(CinemachineVirtualCamera activeVCamera)
		{
			if(activeVCamera == CurrentCamera) return;

			if(activeVCamera == null)
			{
				Debug.LogError("The camera being passed into the ChangeActiveCamera function is null.", gameObject);
				return;
			}

			UnactiveVMCameras = CMCameras.Where(vCam => vCam != activeVCamera).ToList();

			UnactiveVMCameras.ForEach(vCam =>
			{
				vCam.Priority = 1;
			});

			CurrentCamera = activeVCamera;
			CurrentCamera.Priority = 5;
		}

		public static List<Camera> GetCameraStack()
		{
			return new List<Camera>();
		}

		public static List<Camera> GetOverLayCameras()
		{
			return Instance.MainCamera.GetUniversalAdditionalCameraData().cameraStack.
				Where((cam => cam.GetUniversalAdditionalCameraData().renderType == CameraRenderType.Overlay)).ToList();
		}

		public static void AddCameraToStack()
		{

		}
		public static List<Camera> GetBaseCameras()
		{
			return new List<Camera>();
		}

		public void ToggleCameraLock()
		{
			IsCameraLocked = !IsCameraLocked;
		}

		public void SetCameraLock(bool isLocked)
		{

		}
	}
}
