using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Kebabify.Web.Common
{
    [Serializable]
    public class KebabifyException : Exception
    {
        public KebabifyException()
        {
        }

        public KebabifyException(string message)
            : base(message)
        {
        }

        public KebabifyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected KebabifyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        public string ResourceReferenceProperty { get; set; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }
    }
}
