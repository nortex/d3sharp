﻿/*
 * Copyright (C) 2011 D3Sharp Project
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using D3Sharp.Net.BNet.Packets;

namespace D3Sharp.Net.BNet
{
    public sealed class BnetServer : Server
    {
        public BnetServer()
        {
            this.OnConnect += BnetServer_OnConnect;
            this.OnDisconnect += (sender, e) => Logger.Trace("Client disconnected: {0}", e.Connection.ToString());
            this.DataReceived += (sender, e) => BNetRouter.Route(e);
            this.DataSent += (sender, e) => { };
        }

        void BnetServer_OnConnect(object sender, ConnectionEventArgs e)
        {
            Logger.Trace("Bnet-Client connected: {0}", e.Connection.ToString());
            e.Connection.Client = new BNetClient(e.Connection);
        }

        public override void Run()
        {
            if (!this.Listen(Config.Instance.BindIP, Config.Instance.Port)) return;
            Logger.Info("Bnet-Server is listening on {0}:{1}...", Config.Instance.BindIP, Config.Instance.Port);
        }
    }
}
