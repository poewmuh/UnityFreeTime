namespace GameFolder.Gameplay.Networking
{
    public class SessionUser
    {
        public ulong clientId;

        public SessionUser(ulong clientId)
        {
            this.clientId = clientId;
        }
    }
}