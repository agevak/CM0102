using System.Windows.Forms;

public static class ControlExtensions
{
    public static void InvokeIfRequired(this Control control, MethodInvoker action)
    {
        if (control.InvokeRequired) control.EndInvoke(control.BeginInvoke(action));
        else action();
    }
}
