namespace CORE.APP.Models
{
    public abstract class Response
    {
        /// Defined as virtual to allow overriding in derived classes.
        public virtual int Id { get; set; }

        public virtual string Guid { get; set; }

        protected Response(int id)
        {
            Id = id;
        }

        protected Response()
        {
        }
    }
}