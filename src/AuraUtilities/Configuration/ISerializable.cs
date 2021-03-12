using System;
using System.Collections.Generic;
using System.Text;

namespace AuraUtilities.Configuration
{
    public interface ISerializable<TNoSerializableObject>
    {
        public TNoSerializableObject CovertToNormal();
    }
}
