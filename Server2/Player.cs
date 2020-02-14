using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Player
    {

        SocketModel socketPlayer;
        bool playerStatus;
        int numberOfCard;

        public SocketModel SocketPlayer
        {
            get
            {
                return socketPlayer;
            }

            set
            {
                socketPlayer = value;
            }
        }

        public bool PlayerStatus
        {
            get
            {
                return playerStatus;
            }

            set
            {
                playerStatus = value;
            }
        }

        public int NumberOfCard
        {
            get
            {
                return numberOfCard;
            }

            set
            {
                numberOfCard = value;
            }
        }

        public Player(SocketModel s)
        {
            SocketPlayer = s;
            PlayerStatus = false;
        }

    }
}
