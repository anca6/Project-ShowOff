using UnityEngine;
namespace LUNA.Scripts{
    [RequireComponent(typeof(Camera))]
    public class ScreenNormalsRendering : MonoBehaviour{
        public Shader ScreenNormalsShader;

        private Camera sceneCamera;
        private Camera normalsCamera;
        private RenderTexture screenNormalsTexture;
        private static readonly int CameraNormalsTexture = Shader.PropertyToID("_CameraNormalsTexture");
        private GameObject normalsCameraContainer;
        
        // Start is called before the first frame update
        private void Start(){
            sceneCamera = GetComponent<Camera>();

            screenNormalsTexture = new RenderTexture(sceneCamera.pixelWidth, sceneCamera.pixelHeight, 24);
            Shader.SetGlobalTexture(CameraNormalsTexture, screenNormalsTexture);
        }
    }
}