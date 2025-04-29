namespace player
{
    using UnityEngine;
    public partial class Player : MonoBehaviour
    {
        private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
        private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
        void UpdateMethod()
        {
            if (currentState != previousState)
            {
                Debug.Log($"State changed: {previousState} => {currentState}");
                TriggerAnimation(currentState, previousState);
                SetAnimation(currentState, previousState);
                previousState = currentState;
            }
        }
    }
}
