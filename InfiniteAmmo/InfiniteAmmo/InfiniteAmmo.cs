using System;
using InventorySystem.Items.Firearms;
using Qurre;
using Qurre.API;
using Qurre.API.Events;

namespace InfiniteAmmo
{
    public class InfiniteAmmo : Plugin
    {
        public override string Developer => "KoToXleB#4663";
        public override string Name => "InfiniteAmmo";
        public override Version Version => new Version(1, 0, 0);
        public override void Enable() => RegisterEvents();
        public override void Disable() => UnregisterEvents();
        public Config CustomConfig { get; private set; }

        public void RegisterEvents()
        {
            CustomConfig = new Config();
            CustomConfigs.Add(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Player.Shooting += OnShooting;

            if (CustomConfig.RemoveAmmo)
            {
                Qurre.Events.Player.DropAmmo += OnDropAmmo;
                Qurre.Events.Map.CreatePickup += OnCreatePickup;
            }
        }
        public void UnregisterEvents()
        {
            CustomConfigs.Remove(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Player.Shooting -= OnShooting;

            if (CustomConfig.RemoveAmmo)
            {
                Qurre.Events.Player.DropAmmo += OnDropAmmo;
                Qurre.Events.Map.CreatePickup += OnCreatePickup;
            }
        }
        public void OnShooting(ShootingEvent ev)
        {
            if (CustomConfig.InfiniteMode)
            {
                var item = ev.Shooter.ItemTypeInHand;
                if (item == ItemType.GunCOM15 || item == ItemType.GunCOM18 || item == ItemType.GunCrossvec || item == ItemType.GunFSP9) ev.Shooter.Ammo9++;
                else if (item == ItemType.GunE11SR) ev.Shooter.Ammo556++;
                else if (item == ItemType.GunRevolver) ev.Shooter.Ammo44Cal++;
                else if (item == ItemType.GunShotgun) ev.Shooter.Ammo12Gauge++;
                else if (item == ItemType.GunAK || item == ItemType.GunLogicer) ev.Shooter.Ammo762++;
            }
            else
            {
                var arm = ev.Shooter.ItemInHand.Base as Firearm;
                arm.Status = new FirearmStatus(255, arm.Status.Flags, arm.Status.Attachments);
            }
        }
        public void OnDropAmmo(DropAmmoEvent ev) => ev.Allowed = false;
        public void OnCreatePickup(CreatePickupEvent ev)
        {
            if (ev.Info.ItemId == ItemType.Ammo12gauge || ev.Info.ItemId == ItemType.Ammo44cal ||
                ev.Info.ItemId == ItemType.Ammo556x45 || ev.Info.ItemId == ItemType.Ammo762x39 ||
                ev.Info.ItemId == ItemType.Ammo9x19 || ev.Info.ItemId == ItemType.Radio) ev.Allowed = false;
        }
    }
}
