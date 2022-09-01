using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DancingOrgans
{
    public class OrganVisualizer : Visualizer
    {
		private Organ[] organs;

		private void Awake()
		{
			organs = GetComponentsInChildren<Organ>();
		}

		protected override void Start()
		{
			base.Start();

			foreach(var organ in organs)
			{
				organ.Init(this);
			}
		}
    }

}
