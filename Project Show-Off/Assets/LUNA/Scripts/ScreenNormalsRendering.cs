using UnityEngine;
namespace LUNA.Scripts{
    [RequireComponent(typeof(Camera))]
    public class ScreenNormalsRendering : MonoBehaviour{
        public Shader ScreenNormalsShader;

        private Camera sceneCamera;
        private Camera normalCamera;
        private RenderTexture screenNormalTexture;
        
        // Start is called before the first frame update
        private void Start(){
        }
    }
}