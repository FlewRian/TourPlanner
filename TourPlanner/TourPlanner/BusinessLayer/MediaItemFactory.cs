namespace TourPlanner.BusinessLayer
{
    public static class MediaItemFactory
    {
        private static IMediaItemFactory instance;

        public static IMediaItemFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new MediaItemFactoryImpl();
            }

            return instance;
        }
    }
}
