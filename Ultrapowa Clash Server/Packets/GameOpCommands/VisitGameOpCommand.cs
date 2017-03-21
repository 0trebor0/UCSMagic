using System;
using UCS.Core;
using UCS.Core.Network;
using UCS.Logic;
using UCS.Packets.Messages.Server;

namespace UCS.Packets.GameOpCommands
{
    internal class VisitGameOpCommand : GameOpCommand
    {
        readonly string[] m_vArgs;

        public VisitGameOpCommand(string[] args)
        {
            m_vArgs = args;
            SetRequiredAccountPrivileges(0);
        }

        public override async void Execute(Level level)
        {
            if (level.Avatar.AccountPrivileges >= GetRequiredAccountPrivileges())
            {
                if (m_vArgs.Length >= 2)
                {
                    try
                    {
                        var id = Convert.ToInt64(m_vArgs[1]);
                        var l = await ResourcesManager.GetPlayer(id);
                        if (l != null)
                        {
                            l.Tick();
                            Processor.Send(new VisitedHomeDataMessage(level.Client, l, level));
                        }
                        else
                        {
                            GlobalChatLineMessage _MSG = new GlobalChatLineMessage(level.Client);
                            _MSG.PlayerName = "Ultrapowa Clash Server";
                            _MSG.LeagueId = 22;
                            _MSG.Message = "Player doesn't exists!";
                            _MSG.Send();
                        }
                    }
                    catch 
                    {
                    }
                }
            }
            else
            {
                SendCommandFailedMessage(level.Client);
            }
        }
    }
}
