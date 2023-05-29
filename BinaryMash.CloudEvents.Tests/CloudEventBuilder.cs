

namespace BinaryMash.CloudEvents.Tests;

using CloudNative.CloudEvents.Extensions;

public class CloudEventBuilder
{
    
    public CloudEvent BuildMinimal()
    {
        ProjectCreated projectCreated = new ()
        {
            Id = Guid.Parse("9902d6a2-114f-42f2-8a95-9decbd89d0cb"),
            Name = "Some project"
        };
        
        return new CloudEvent()
        {
            // required
            Id = Guid.Parse("edc52a33-7210-4c2b-a32b-aab9b9cb344d").ToString(),
            Source = new Uri($"https://binarymash/some-system/projects/{projectCreated.Id}"),
            Type = "binarymash.some-system.project-created",

            // optional
            Data = projectCreated,
        };
    }

    public CloudEvent BuildMinimalWithPartitionExtension()
    {
        ProjectCreated projectCreated = new ()
        {
            Id = Guid.Parse("9902d6a2-114f-42f2-8a95-9decbd89d0cb"),
            Name = "Some project"
        };
        
        return new CloudEvent()
        {
            // required
            Id = Guid.Parse("edc52a33-7210-4c2b-a32b-aab9b9cb344d").ToString(),
            Source = new Uri($"https://binarymash/some-system/projects/{projectCreated.Id}"),
            Type = "binarymash.some-system.project-created",

            // optional
            Data = projectCreated,
        }.SetPartitionKey(projectCreated.Id.ToString());
    }    

    public CloudEvent BuildComplex()
    {
        ProjectCreated projectCreated = new ()
        {
            Id = Guid.Parse("9902d6a2-114f-42f2-8a95-9decbd89d0cb"),
            Name = "Some project"
        };
        
        return new CloudEvent()
        {
            // required
            Id = Guid.Parse("edc52a33-7210-4c2b-a32b-aab9b9cb344d").ToString(),
            Source = new Uri($"https://binarymash/some-system/projects/{projectCreated.Id}"),
//            SpecVersion = CloudEventsSpecVersion.V1_0, // read-only
            Type = "binarymash.some-system.project-created",

            // optional
//            DataContentType = "application/json",
            Data = projectCreated,

  //          DataSchema,
  //          ExtensionAttributes,
  //          Subject = ,
  //          Time = new DateTimeOffset(2023, 04, 23, 15, 22, 01, TimeSpan.FromHours(1)),
        };
    }
}
