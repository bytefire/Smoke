using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// The base domain model class.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets the identifying id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The date and time the model was created.
        /// </summary>
        public DateTime CreatedOn
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        protected BaseModel()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }

    }
}