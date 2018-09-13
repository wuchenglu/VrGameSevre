using System.Threading;

namespace ControlConterServe.tool
{
    public delegate void  ExecutorDelegateEvent();
    public class ExecutorPool
    {
        private static ExecutorPool _instance;

        public static ExecutorPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance=new ExecutorPool();
                }
                return _instance;
            } 
        }

        Mutex _mutex=new Mutex();

        public void Executor(ExecutorDelegateEvent e)
        {
            lock (this)
            {
                _mutex.WaitOne();
                e();
                _mutex.ReleaseMutex();
            }
        }
    }
}
