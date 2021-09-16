// -----------------------------------------------------------------------
// <copyright file="NazwaPluginu.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Mistaken.API;
using Mistaken.API.Diagnostics;

namespace Mistaken.learning
{
    internal class NazwaPluginu : Module
    {
        public NazwaPluginu(IPlugin<IConfig> plugin)
            : base(plugin)
        {
            Instance = this;
        }

        public override string Name => nameof(NazwaPluginu);

        public override void OnDisable()
        {
            Exiled.Events.Handlers.Player.Dying -= this.Handle<Exiled.Events.EventArgs.DyingEventArgs>((ev) => this.Player_Dying(ev));
        }

        public override void OnEnable()
        {
            Exiled.Events.Handlers.Player.Dying += this.Handle<Exiled.Events.EventArgs.DyingEventArgs>((ev) => this.Player_Dying(ev));
        }

        public static Dictionary<Player, int> classDKills = new Dictionary<Player, int>();

        internal static NazwaPluginu Instance { get; private set; }

        private void Player_Dying(Exiled.Events.EventArgs.DyingEventArgs ev)
        {
            if (!classDKills.ContainsKey(ev.Killer))
            {
                classDKills.Add(ev.Killer, 0);
            }

            if (ev.Target.Role == RoleType.ClassD && ev.Killer.Role == RoleType.FacilityGuard)
            {
                classDKills[ev.Killer] += 1;
                CustomInfoHandler.Set(ev.Killer, "zbrodnia", $"<color=#c722c4>uwu: {classDKills[ev.Killer]}</color>");
            }
        }
    }
}
