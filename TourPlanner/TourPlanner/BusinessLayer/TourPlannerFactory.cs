namespace TourPlanner.BusinessLayer
{
    public static class TourPlannerFactory
    {
        private static ITourPlannerFactory instance;

        public static ITourPlannerFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new TourPlannerFactoryImpl();
            }

            return instance;
        }
    }
}
