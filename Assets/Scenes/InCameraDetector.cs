using UnityEngine;
using UnityEngine.Animations.Rigging;

public class InCameraDetector : MonoBehaviour
{
   [SerializeField] private Camera playerCamera;
   [SerializeField] private MoveCamera ghostFollowObject;
   private Plane[] cameraFrustum;
   private Collider meshCollider;

   private void Start()
   {
      meshCollider = GetComponent<Collider>();
   }

   private void Update()
   {
      var bounds = meshCollider.bounds;
      cameraFrustum = GeometryUtility.CalculateFrustumPlanes(playerCamera);
      if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
      {
         // dikh raha
         ghostFollowObject.enabled = false;
      }
      else
      {
         // nahi dikh raha
         ghostFollowObject.enabled = true;
      }
   }
}
