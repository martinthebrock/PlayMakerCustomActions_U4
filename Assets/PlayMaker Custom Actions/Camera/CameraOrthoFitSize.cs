﻿// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Fit an Aspect Ratio inside the screen. No Cropping will occur, use CameraOrthoFillSize to crop and fill screen")]
	public class CameraOrthoFitSize : FsmStateAction
	{
		[Tooltip("The Camera to control. Leave to none to use the MainCamera")]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault camera;
		
		[Tooltip("The width to fit")]
		public FsmFloat targetWidth;

		[Tooltip("The height to fit")]
		public FsmFloat targetHeight;

		public bool everyFrame;

		GameObject _go;
		Camera _camera;
		
		public override void Reset()
		{
			camera = new FsmOwnerDefault();
			camera.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			camera.GameObject = new FsmGameObject(){UseVariable=true};

			targetWidth = null;
			targetHeight = null;

			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoFit();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoFit();
		}
		
		void DoFit()
		{
			_go = Fsm.GetOwnerDefaultTarget(camera);
			
			if (_go!=null)
			{
				_camera = _go.GetComponent<Camera>();
			}
			
			if (_camera == null)
			{
				_camera = Camera.main;
			}

			float targetAspect = targetWidth.Value / targetHeight.Value;

			float windowAspect = (float)Screen.width / (float)Screen.height;

			// greater, we fit the height, if it's less we fit the width;
			float heightToBeSeen = windowAspect>targetAspect?targetHeight.Value:targetWidth.Value/windowAspect;

		//	UnityEngine.Debug.Log("windowAspect: "+windowAspect+" targetAspect : "+targetAspect+" HeightTobeSeen"+heightToBeSeen);

			_camera.orthographicSize = 	heightToBeSeen * 0.5f;
		}
	}
}
