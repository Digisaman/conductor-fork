using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Models;

namespace Conductor.Domain.Models
{
    public class Definition
    {
        public string Id { get; set; }

        public int Version { get; set; }

        public string Description { get; set; }

        //public string DataType { get; set; }

        public WorkflowErrorHandling DefaultErrorBehavior { get; set; }

        public TimeSpan? DefaultErrorRetryInterval { get; set; }

        public List<Step> Steps { get; set; } = new List<Step>();

        /// <summary>
        /// used to store the graphical representation of the workflow
        /// </summary>
        public string Shape { get; set; }

        /// <summary>
        /// ClientId of the App in IdentityServer on which the API's are called, used for authentication
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// ClientSecret of the App in IdentityServer on which the API's are called, used for authentication
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// ApiScope of the App in IdentityServer on which the API's are called, used for authorization
        /// </summary>
        public string ApiScope { get; set; }

    }
}
