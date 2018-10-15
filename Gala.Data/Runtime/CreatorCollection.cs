using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Galatea.AI.Abstract;

namespace Gala.Data.Runtime
{
    internal class CreatorCollection : KeyedCollection<string, ICreator>
    {
        protected override string GetKeyForItem(ICreator item)
        {
            return item.CreatorName;
        }

        public ICreator Fetch(string creatorName, string typeName)
        {
            if (!this.Contains(creatorName))
            {
                switch (typeName)
                {
                    case "Galatea.Runtime.Services.User":
                        this.Add(new Galatea.Runtime.Services.User(creatorName));
                        break;

                    default:
                        throw new Galatea.TeaArgumentException("CREATOR NOT EXIST!"); // TODO
                }
            }
            else
            {
                // Check Type against existing
                if (this[creatorName].GetType().FullName != typeName)
                {
                    throw new Galatea.TeaArgumentException("WRONG CREATOR TYPE");   // TODO
                }
            }

            return this[creatorName];
        }
    }
}