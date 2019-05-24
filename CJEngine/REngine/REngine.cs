using RDotNet;

namespace CJEngine
{
    public static class REngineClass
    {
        private static REngine engine;
        public static void Initialise()
        {
           engine = REngine.GetInstance();
        }

        public static REngine GetREngine()
        {
            return engine;
        }

        public static void DestroyEngine()
        {
            engine.Dispose();
        }
    }
}
