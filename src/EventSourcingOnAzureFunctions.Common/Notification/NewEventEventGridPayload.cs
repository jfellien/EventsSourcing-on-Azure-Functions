﻿using EventSourcingOnAzureFunctions.Common.EventSourcing.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingOnAzureFunctions.Common.Notification
{
    /// <summary>
    /// Event Grid message payload for a new event notification
    /// </summary>
    public class NewEventEventGridPayload
        : IEventStreamIdentity
    {

        /// <summary>
        /// The notification instance identifier (for logical idempotency checking)
        /// </summary>
        [JsonProperty(PropertyName = "notificationId")]
        public string NotificationId { get; set; }

        /// <summary>
        /// The domain in which the new entity was created
        /// </summary>
        [JsonProperty(PropertyName = "domainName")]
        public string DomainName { get; set; }

        /// <summary>
        /// The type of entity that was created 
        /// </summary>
        [JsonProperty(PropertyName = "entityTypeName")]
        public string EntityTypeName { get; set; }

        /// <summary>
        /// The unique identifier of the new entity instance that was created
        /// </summary>
        [JsonProperty(PropertyName = "instanceKey")]
        public string InstanceKey { get; set; }

        /// <summary>
        /// Comments that can be passed for logging / diagnostic or other purpose
        /// </summary>
        [JsonProperty(PropertyName = "commentary")]
        public string Commentary { get; set; }

        /// <summary>
        /// The type of the event that was appended to the event stream for this entity
        /// </summary>
        [JsonProperty(PropertyName = "eventType")]
        public string EventType { get; set; }

        /// <summary>
        /// The sequence number of the just-added event
        /// </summary>
        [JsonProperty(PropertyName = "sequenceNumber")]
        public int SequenceNumber { get; set; }

        /// <summary>
        /// Empty constructor for serialisation
        /// </summary>
        public NewEventEventGridPayload()
        {
        }


        public static NewEventEventGridPayload Create(IEventStreamIdentity newEntity,
                string eventType,
                int sequenceNumber,
                string notificationId = @"",
                string commentary = @"")
        {

            if (null == newEntity)
            {
                throw new ArgumentNullException(nameof(newEntity));
            }

            if (string.IsNullOrWhiteSpace(eventType))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

            // Default the notification id if not are provided
            if (string.IsNullOrEmpty(notificationId))
            {
                notificationId = Guid.NewGuid().ToString("N");
            }

            return new NewEventEventGridPayload()
            {
                DomainName = newEntity.DomainName,
                EntityTypeName = newEntity.EntityTypeName,
                InstanceKey = newEntity.InstanceKey,
                NotificationId = notificationId,
                Commentary = commentary,
                EventType = eventType,
                SequenceNumber = sequenceNumber 
            };

        }
    }
}
