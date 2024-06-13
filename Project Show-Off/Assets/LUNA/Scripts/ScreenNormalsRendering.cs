using UnityEngine;
namespace LUNA.Scripts{
    [RequireComponent(typeof(Camera))]
    public class ScreenNormalsRendering : MonoBehaviour{
        public Shader ScreenNormalsShader;

        private Camera sceneCamera;
        private Camera normalCamera;
        private RenderTexture screenNormalTexture;
        private static readonly int CameraNormalsTexture = Shader.PropertyToID("_CameraNormalsTexture");
        private GameObject normalCameraContainer;
        
        // Start is called before the first frame update
        private void Start(){
            sceneCamera = GetComponent<Camera>();

            screenNormalTexture = new RenderTexture(sceneCamera.pixelWidth, sceneCamera.pixelHeight, 24);
            Shader.SetGlobalTexture(CameraNormalsTexture, screenNormalTexture);
        }
    }
}