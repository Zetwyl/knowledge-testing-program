using System;
using System.Windows.Forms;


namespace MyQuiz.WinForms.Extensions
{
    public static class FormExtensions
    {
        // Для потокобезопасности
        public static void SafeInvoke(this Form form, Action action)
        {
            if (form.InvokeRequired) form.Invoke(action);
            else action();
        }

        // Для мгновенного обновления прогресс-бара
        public static void SetValueInstant(this ProgressBar pb, int value)
        {
            if (value >= pb.Maximum) { pb.Value = pb.Maximum; pb.Value = pb.Maximum - 1; pb.Value = pb.Maximum; return; }
            pb.Value = value + 1;
            pb.Value = value;
        }
    }
}