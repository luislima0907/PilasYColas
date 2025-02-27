namespace PilasYColas;

public interface IQueueService
{
    void Enqueue(string message);
    string Dequeue();
    bool IsEmpty();
    int Count();
    void PrintQueue();
}