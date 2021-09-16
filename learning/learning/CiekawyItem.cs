// -----------------------------------------------------------------------
// <copyright file="CiekawyItem.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.CustomItems.API.Spawn;
using Exiled.Events.EventArgs;
using MEC;
using Mistaken.API.Extensions;
using Mistaken.API.GUI;
using UnityEngine;

namespace Mistaken.learning
{
    internal class CiekawyItem : CustomWeapon
    {
        public override Modifiers Modifiers { get; set; }

        public override float Damage { get; set; }

        public override uint Id { get; set; } = 10;

        public override string Name { get; set; } = "Nazwa";

        public override string Description { get; set; } = "yes";

        public override float Weight { get; set; } = 0.1f;

        public override SpawnProperties SpawnProperties { get; set; }

        public override ItemType Type { get; set; } = ItemType.GunCOM15;

        public override byte ClipSize { get; set; } = 69;

        public override Pickup Spawn(Vector3 position, Item item)
        {
            var pickup = base.Spawn(position, item);
            pickup.Scale = new Vector3(2, 2, 2);
            return pickup;
        }

        public override Pickup Spawn(Vector3 position)
        {
            var pickup = base.Spawn(position);
            pickup.Scale = new Vector3(2, 2, 2);
            return pickup;
        }

        protected override void OnShot(ShotEventArgs ev)
        {
            if (UnityEngine.Random.Range(0, 100) > 79)
            {
                ev.Target.ArtificialHealth += 10;
                ev.Target.EnableEffect<CustomPlayerEffects.Invigorated>(5);
            }
        }

        protected override void ShowSelectedMessage(Player player)
        {
            NazwaPluginu.Instance.RunCoroutine(this.UpdateInterface(player));
        }

        private IEnumerator<float> UpdateInterface(Player player)
        {
            yield return Timing.WaitForSeconds(0.1f);
            while (this.Check(player.CurrentItem))
            {
                player.SetGUI("Nazwa", PseudoGUIPosition.BOTTOM, "Obecnie trzymasz <color=yellow>Nazwa</color>");
                yield return Timing.WaitForSeconds(1f);
            }

            player.SetGUI("Nazwa", PseudoGUIPosition.BOTTOM, null);
        }
    }
}
