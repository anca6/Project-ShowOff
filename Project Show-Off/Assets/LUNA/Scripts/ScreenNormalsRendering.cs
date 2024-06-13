using UnityEngine;
namespace LUNA.Scripts{
    [RequireComponent(typeof(Camera))]
    public class ScreenNormalsRendering : MonoBehaviour{
        public Shader ScreenNormalsShader;
        
        // Start is called before the first frame update
        private void Start(){
        }
    }
}