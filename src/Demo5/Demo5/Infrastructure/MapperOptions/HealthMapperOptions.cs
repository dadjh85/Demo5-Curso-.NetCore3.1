
namespace Demo5.Infrastructure.MapperOptions
{
    public class HealthMapperOptions
    {
        public DiskHealthOptions DiskConfig { get; set; }
        public MemoryHealthOptions MemoryConfig { get; set; }
        public DatabaseHealthOptions SqlServer { get; set; }
    }

    public class DiskHealthOptions
    {
        public string Name { get; set; }
        public int MinimunSize { get; set; }
    }

    public class MemoryHealthOptions
    {
        public string Name { get; set; }
        public long MaximunMemorySize { get; set; }
    }

    public class DatabaseHealthOptions
    {
        public string Name { get; set; }
    }
}
