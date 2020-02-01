namespace Mediator.AspNetCore.Example.Queries
{
    public class SampleQuery : IQuery<SampleQueryResult>
    {
        public string Value { get; set; }
    }
}
