using System;
using System.Collections.Generic;
using TandC.FlyBall.Common;
using UnityEngine;

namespace TandC.Localization
{
	[CreateAssetMenu(fileName = "LocalizationData", menuName = "TancC/LocalizationData", order = 2)]
	public class LocalizationData : ScriptableObject
	{
		[SerializeField]
		public List<LocalizationLanguageData> languages = new List<LocalizationLanguageData>();

		public Enumerators.Language defaultLanguage;

		[Serializable]
		public class LocalizationLanguageData
		{
			public Enumerators.Language language;
			[SerializeField]
			public List<LocalizationDataInfo> localizedTexts = new List<LocalizationDataInfo>();
		}

		[Serializable]
		public class LocalizationDataInfo
		{
			public string key;
			[TextArea(1, 9999)]
			public string value;
		}
	}
}