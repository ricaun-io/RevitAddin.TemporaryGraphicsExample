namespace RevitAddin.TemporaryGraphicsExample.Revit
{
    public class RevitRibbonController
    {
        /// <summary>
        /// RibbonControl
        /// </summary>
        public static UIFramework.RevitRibbonControl RibbonControl => UIFramework.RevitRibbonControl.RibbonControl;
        /// <summary>
        /// Enable
        /// </summary>
        public static void ApplicationIdle() => UIFramework.RevitRibbonControl.RibbonControl.Dispatcher.Invoke(NoneMethod, System.Windows.Threading.DispatcherPriority.ApplicationIdle);
        private static void NoneMethod() { }
    }
}
