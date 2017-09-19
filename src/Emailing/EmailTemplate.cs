using System;

namespace Emailing
{
    public class EmailTemplate
    {
        /// <summary>
        /// Gets or sets the receiver.
        /// </summary>
        /// <value>
        /// The receiver.
        /// </value>
        public String Receiver { get; set; }
        
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The receiver.
        /// </value>
        public String ReceiverName { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public String Subject { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public String Content { get; set; }
    }
}