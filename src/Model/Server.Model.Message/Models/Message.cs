namespace odec.Server.Model.Message
{
    /// <summary>
    /// Message
    /// </summary>
    public class Message :MessageTemplate<int>
    {
        /// <summary>
        /// Identity of user sended that message
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User sended that message
        /// </summary>
        public User.User User { get; set; }

    }
}
