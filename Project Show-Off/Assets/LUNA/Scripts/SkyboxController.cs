namespace Luna.Scripts{
    using UnityEngine;
    public class SkyboxController : MonoBehaviour{
        public Transform Sun;
        private static readonly int SunDirection = Shader.PropertyToID("_SunDirection");
    }
}