//控制层，主角出现的相关控制

using UnityEngine;
using System.Collections;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_HeroShow : BaseControl {

		private void Start() {
			HeroShowParticleEffect();
		}


		/// <summary>
		/// 主角登场特效
		/// </summary>
		public void HeroShowParticleEffect() {
			StartCoroutine(base.LoadParticalEffect("ParticleProps/Hero_Show", this.transform, Vector3.zero, 3f));
		}

	}
}