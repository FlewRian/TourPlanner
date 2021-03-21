namespace TourPlanner
{
    class TourAddWindowFactory : IWindowFactory
    {
        public static void CreateWindow()
        {
            TourAddWindow view = new TourAddWindow();
            view.Show();
        }

        void IWindowFactory.CreateWindow()
        {
            CreateWindow();
        }
    }

}
