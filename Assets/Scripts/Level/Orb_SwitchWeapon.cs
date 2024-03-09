public class Orb_SwitchWeapon : Orb
{
    protected override void PlayEffect()
    {
        WeaponManager.I.EquipRandomWeapon();
    }
}