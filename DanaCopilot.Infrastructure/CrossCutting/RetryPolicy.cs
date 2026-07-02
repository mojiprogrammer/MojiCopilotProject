namespace DanaCopilot.Infrastructure.CrossCutting
{
    public class RetryPolicy : IRetryPolicy
    {
        public async Task ExecuteAsync(Func<Task> action)
        {
            int retry = 3;

            while (retry-- > 0)
            {
                try
                {
                    await action();
                    return;
                }
                catch
                {
                    await Task.Delay(500);
                }
            }
        }
    }
}
