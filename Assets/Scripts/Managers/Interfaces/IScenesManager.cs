namespace TandC.FlyBall
{
    public interface IScenesManager
    {
        bool IsLoadedScene { get; set; }
        void ChangeScene(Common.Enumerators.SceneType sceneType); 
    }
}