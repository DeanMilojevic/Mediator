using System.Threading.Tasks;

namespace Mediator
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task Handle(T command);
    }
}
