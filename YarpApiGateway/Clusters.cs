using Yarp.ReverseProxy.Configuration;

namespace YarpApiGateway;

public static class Clusters
{
    public static ClusterConfig[] GetClusters()
    {
        return new[]
        {
            new ClusterConfig
            {
                ClusterId = "StudentService",
                Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        "StudentService", new DestinationConfig
                            { Address = "http://localhost:8000/" }
                    }
                }
            },
            new ClusterConfig
            {
                ClusterId = "CourseService",
                Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                {
                    {
                        "CourseService", new DestinationConfig
                            { Address = "http://localhost:6000/" }
                    }
                }
            }
        };
    }
}