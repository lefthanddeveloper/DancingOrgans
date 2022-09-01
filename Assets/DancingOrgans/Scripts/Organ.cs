using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DancingOrgans
{
	public class Organ : MonoBehaviour
    {
        [SerializeField] protected TargetFrequency targetFrequency;
		[SerializeField] protected float indivisualMultiplier = 1f;
		[SerializeField] private Vector2 scaleRange = new Vector2(0, 1.25f);


        protected IVisualizer visualizer;
		protected float buffer;
		private float smoothVel = 0f;

        public virtual void Init(IVisualizer visualizer)
		{
            this.visualizer = visualizer;
		}

		private void Update()
		{
			ProcessVisualization();
		}
		
		private void ProcessVisualization()
		{
			float curFreq = visualizer.GetFrequencyBand(targetFrequency) * indivisualMultiplier;

			if(curFreq > buffer)
			{
				buffer = Mathf.SmoothDamp(buffer, curFreq, ref smoothVel, Time.deltaTime * 5f);
			}
			else
			{
				buffer = Mathf.SmoothDamp(buffer, curFreq, ref smoothVel, Time.deltaTime * 10f);
			}

			//buffer = Mathf.Clamp(0f, scaleRange.x, scaleRange.y);

			float scale = scaleRange.x + buffer;
			scale = Mathf.Clamp(scale, scaleRange.x, scaleRange.y);
			this.transform.localScale = Vector3.one * scale;
		}

	}

}
