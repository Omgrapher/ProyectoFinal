using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoFinal
{
    public static class SweetAlertUtils
    {
        public static string ShowAlert(string title, string text, string icon = "info", string confirmButtonText = "OK")
        {
            var sb = new StringBuilder();
            sb.AppendLine("Swal.fire({");
            sb.AppendLine($"  title: '{title}',");
            sb.AppendLine($"  text: '{text}',");
            sb.AppendLine($"  icon: '{icon}',");
            sb.AppendLine($"  confirmButtonText: '{confirmButtonText}'");
            sb.AppendLine("});");

            return sb.ToString();
        }

        public static string ShowConfirm(string title, string text, string confirmButtonText = "Yes, Confirm", string cancelButtonText = "Cancel", string successTitle = "Success", string successText = "Se realizó correctamente")
        {
            var sb = new StringBuilder();
            sb.AppendLine("Swal.fire({");
            sb.AppendLine($"  title: '{title}',");
            sb.AppendLine($"  text: '{text}',");
            sb.AppendLine("  icon: 'warning',");
            sb.AppendLine("  showCancelButton: true,");
            sb.AppendLine("  confirmButtonColor: '#3085d6',");
            sb.AppendLine("  cancelButtonColor: '#d33',");
            sb.AppendLine($"  confirmButtonText: '{confirmButtonText}',");
            sb.AppendLine($"  cancelButtonText: '{cancelButtonText}'");
            sb.AppendLine("}).then(result => {");
            sb.AppendLine("  if (result.isConfirmed) {");
            sb.AppendLine("    Swal.fire({");
            sb.AppendLine($"      title: '{successTitle}',");
            sb.AppendLine($"      text: '{successText}',");
            sb.AppendLine("      icon: 'success',");
            sb.AppendLine("      confirmButtonText: 'OK'");
            sb.AppendLine("    });");
            sb.AppendLine("  }");
            sb.AppendLine("});");

            return sb.ToString();
        }

        public static string ShowSuccess(string title, string text)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Swal.fire({");
            sb.AppendLine($"  title: '{title}',");
            sb.AppendLine($"  text: '{text}',");
            sb.AppendLine("  icon: 'success',");
            sb.AppendLine("  confirmButtonText: 'OK'");
            sb.AppendLine("});");

            return sb.ToString();
        }

        public static string ShowError(string title, string text)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Swal.fire({");
            sb.AppendLine($"  title: '{title}',");
            sb.AppendLine($"  text: '{text}',");
            sb.AppendLine("  icon: 'error',");
            sb.AppendLine("  confirmButtonText: 'OK'");
            sb.AppendLine("});");

            return sb.ToString();
        }
    }
}