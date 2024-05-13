﻿namespace Beter.TestingTools.IntegrationTests.Helpers
{
    public static class WaitHelper
    {
        public static async Task WaitForCondition(Func<Task<bool>> condition, int delayMilliseconds = 3000, int maxRetries = 100)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                if (await condition())
                {
                    return;
                }

                await Task.Delay(delayMilliseconds);
            }

            throw new TimeoutException("Response was not processed within the specified time.");
        }
    }
}
