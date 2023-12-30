using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using System.Reactive.Disposables;
using IntelVault.Worker.model;

namespace IntelVault.Worker;

public class PoolRequests : IObservable<OpenSourceRequest>, IDisposable
{
    private ConcurrentQueue<OpenSourceRequest> _requests = new ConcurrentQueue<OpenSourceRequest>();
    public bool IsRunning { get; private set; }
    private readonly List<IObserver<OpenSourceRequest>>? _observers = new();
    private CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    public void AddRequest(OpenSourceRequest openSourceRequest)
    {

        if (_observers != null)
            foreach (var observer in _observers.ToList())
            {
                if (openSourceRequest != null)
                {
                    _requests.Enqueue(openSourceRequest);
                    try
                    {
                        observer.OnNext(openSourceRequest);

                    }
                    catch (Exception ex)
                    {
                        observer.OnError(ex);
                    }

                }
            }

    }








    public IDisposable Subscribe(IObserver<OpenSourceRequest> observer)
    {
        if (_observers != null && !_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return Disposable.Create(() => _observers?.Remove(observer));
    }


    public void Dispose()
    {
        throw new NotImplementedException();
    }
}