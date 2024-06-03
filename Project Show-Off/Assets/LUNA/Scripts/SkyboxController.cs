namespace Luna.Scripts{
    using UnityEngine;
    public class SkyboxController{
        public Transform Sun;
        private static readonly int SunDirection = Shader.PropertyToID("_SunDirection");
    }
}