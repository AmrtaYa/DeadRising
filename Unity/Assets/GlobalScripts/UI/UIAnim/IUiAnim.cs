using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace MiYue
{
	public interface IUiAnim
	{
		void PlayAnim<T>(Control ctrl)where T: MaskableGraphic;
	}
}

