namespace PurpleFlowerCore
{
    public class WaitNode: IProcessNode
    {
        private readonly float _waitTime;
        private float _currentTime;
        public WaitNode(float time)
        {
            _waitTime = time;
        }
        public bool Update(float deltaTime)
        {
            _currentTime += deltaTime;
            return _currentTime > _waitTime;
        }

        public void ReSet()
        {
            _currentTime = 0;
        }

        public static implicit operator WaitNode(float time)
        {
            return new WaitNode(time);
        }
    }
}