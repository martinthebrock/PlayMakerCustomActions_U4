// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// Action made by : Sebaslive
// Forum link : http://hutonggames.com/playmakerforum/index.php?topic=10953.0
// Edited by : DjayDino
/*--- __ECO__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets a bool to true if Transform has changed.")]
	public class TransformHasChanged : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to check if transform has changed.")]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
		public FsmEvent changedEvent;

		public override void Reset()
		{
			gameObject = null;
			storeResult = null;
			changedEvent = null;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			go.transform.hasChanged = false;
            DoTransformHasChanged();		
		}

		public override void OnUpdate()
		{
			DoTransformHasChanged();
		}

        void DoTransformHasChanged()
		{
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;
            if (go.transform.hasChanged)
            {
				if (storeResult != null)
				{
                storeResult.Value = true;
				}
				Fsm.Event(changedEvent);
            }
            
            if (!go.transform.hasChanged)
            {
                storeResult.Value = false;
            }
            go.transform.hasChanged = false;
		}
		
	}
}
