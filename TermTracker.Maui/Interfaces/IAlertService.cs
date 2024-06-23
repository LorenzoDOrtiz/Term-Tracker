using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermTracker.Maui.Interfaces;
public interface IAlertService
{
    Task ShowToast(string text);
}
