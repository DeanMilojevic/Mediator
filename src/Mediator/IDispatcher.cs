using System.Threading.Tasks;

namespace Mediator
{
    public interface IDispatcher
    {
        Task Send(ICommand command);

        Task<T> Send<T>(IQuery<T> query);
    }
}
