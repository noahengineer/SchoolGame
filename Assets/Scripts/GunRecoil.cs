using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 TargetRoation;

    // hipfire recoil
    [SerializeField]
    private float recoilX;
    [SerializeField]
    private float recoilY;
    [SerializeField]
    private float recoilZ;

    //settings
    [SerializeField]
    private float snappiness;
    [SerializeField]
    private float returnSpeed;
    [SerializeField]
    private float aimRecoilMultiplier;

    Aim aim;

    private void Start()
    {
        aim = GetComponentInChildren<Aim>();
    }

    private void Update()
    {
        TargetRoation = Vector3.Lerp(TargetRoation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, TargetRoation, snappiness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        if (aim.isAming)
            TargetRoation += new Vector3(recoilX * aimRecoilMultiplier, Random.Range(-recoilY * aimRecoilMultiplier, recoilY * aimRecoilMultiplier), Random.Range(-recoilZ * aimRecoilMultiplier, recoilZ * aimRecoilMultiplier));
        else
            TargetRoation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
