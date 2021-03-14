using System;
using System.Collections.Generic;

namespace AuraUtilities
{
    public class Locator
    {
        /// <summary>
        /// Gets the <see cref="Locator"/> current instance.
        /// </summary>
        public static Locator Current
        {
            get;
            private set;
        }

        static Locator()
        {
            Current = new Locator();
        }

        private Dictionary<object, object> cache = new();

        private Locator()
        {
        }

        /// <inheritdoc/>
        public T? GetService<T>() where T : class
        {
            if (cache.ContainsKey(typeof(T)))
            {
                return (T)cache[typeof(T)];
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public Locator RegisterService<T>(T t) where T : class
        {
            if (!cache.ContainsKey(typeof(T)))
            {
                cache.Add(typeof(T), t);
                return this;
            }
            else
            {
                throw new NotImplementedException("The Service is already registred.");
            }
        }
    }
}