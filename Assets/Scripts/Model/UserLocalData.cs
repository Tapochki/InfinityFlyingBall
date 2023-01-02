using System;
using System.Collections.Generic;
using TandC.FlyBall.Common;

namespace TandC.FlyBall
{
    public class UserLocalData
    {
        public Enumerators.Language appLanguage;

        public UserLocalData()
        {
            Reset();
        }

        public void Reset()
        {
            appLanguage = Enumerators.Language.NONE;
        }
    }
}