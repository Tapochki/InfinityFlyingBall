namespace TandC.FlyBall.Common
{
    public class Enumerators
    {
        public enum AppState
        {
            NONE,
            APP_INIT_LOADING,
            MAIN_MENU,
        }

        public enum ButtonState
        {
            ACTIVE,
            DEFAULT
        }

        public enum SceneType
        {
            MAIN_MENU,
        }

        public enum SoundType : int
        {
            //  CLICK,
            //  OTHER,
            //   BACKGROUND,
        }

        public enum NotificationType
        {
            LOG,
            ERROR,
            WARNING,

            MESSAGE
        }

        public enum Language
        {
            NONE,
            EN,
            RU,
            UK
        }

        public enum ScreenOrientationMode
        {
            PORTRAIT,
            LANDSCAPE
        }

        public enum CacheDataType
        {
            USER_LOCAL_DATA,
            USER_RECORDS_DATA
        }

        public enum NotificationButtonState
        {
            ACTIVE,
            INACTIVE
        }
    }
}