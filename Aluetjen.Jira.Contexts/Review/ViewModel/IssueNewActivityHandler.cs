using System;
using System.Collections.Generic;
using Aluetjen.Jira.Contexts.Tracking.Events;

namespace Aluetjen.Jira.Contexts.Review.ViewModel
{
    public class ReviewNewActivityHandler : IHandleMessages<NewActivityEvent>
    {
        public IDocumentStore Store { get; set; }

        public void Handle(NewActivityEvent message)
        {
            return;

            var issue = new Issue
                            {
                                Key = message.Key,
                                Description = "A description of the issue",
                                Summary = "An activity has been created for this issue so, we need to update it!",
                                Comments = new List<Comment>
                                               {
                                                   new Comment
                                                       {
                                                           PostedOn = new DateTime(2012, 5, 16),
                                                           User = "Brian Flynn",
                                                           Description =
                                                               "For anyone else who's interested, the concept of the administrative channel is mentioned just after the 35 minute mark in the video. The video then goes on to walk through the gateway demo and answer some questions from the audience. Thanks Andreas."
                                                       },
                                                   new Comment
                                                       {
                                                           PostedOn = new DateTime(2012, 5, 16),
                                                           User = "Brian Simmons",
                                                           Description =
                                                               "For anyone else who's interested, the concept of the administrative channel is mentioned just after the 35 minute mark in the video. The video then goes on to walk through the gateway demo and answer some questions from the audience. Thanks Andreas."
                                                       },
                                                   new Comment
                                                       {
                                                           PostedOn = new DateTime(2012, 5, 16),
                                                           User = "Francisca Gutierrez",
                                                           Description =
                                                               "For anyone else who's interested, the concept of the administrative channel is mentioned just after the 35 minute mark in the video. The video then goes on to walk through the gateway demo and answer some questions from the audience. Thanks Andreas."
                                                       }
                                               }
                            };

            Store.Store(issue);
        }
    }
}
