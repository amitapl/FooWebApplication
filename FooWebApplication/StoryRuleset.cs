using Story.Core;
using Story.Core.Handlers;
using Story.Core.Rules;
using Story.Ext.Handlers;
using System;

namespace FooWebApplication
{
    public class StoryRuleset : Ruleset<IStory, IStoryHandler>
    {
        public StoryRuleset()
        {
            IStoryHandler storyHandler = StoryHandlers.DefaultTraceHandler;

            // Add the azure table storage story handler only if the connection string exists
            // It should be set as a connection string called "StoryTableStorage"
            var azureTableStorageConfiguration = new AzureTableStorageHandlerConfiguration();
            if (azureTableStorageConfiguration.ConnectionString != null)
            {
                storyHandler =
                    storyHandler.Compose(new AzureTableStorageHandler("AzureTable", azureTableStorageConfiguration));
            }

            Rules.Add(
                new PredicateRule(
                    story => story.IsRoot(),
                    story => storyHandler));
        }
    }
}
